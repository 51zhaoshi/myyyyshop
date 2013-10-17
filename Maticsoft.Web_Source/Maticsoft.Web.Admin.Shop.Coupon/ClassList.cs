namespace Maticsoft.Web.Admin.Shop.Coupon
{
    using Maticsoft.BLL.Shop.Coupon;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ClassList : PageBaseAdmin
    {
        protected int Act_AddData = 0x19c;
        protected int Act_DelData = 0x19e;
        protected int Act_UpdateData = 0x19d;
        protected Button btnSaveEq;
        protected Button Button1;
        private CouponClass classBll = new CouponClass();
        protected GridViewEx gridView;
        protected Label Label1;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected TextBox txtKeyword;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            string text = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                builder.AppendFormat(" Name Like '%{0}%'", text);
            }
            DataSet set = this.classBll.GetList(-1, builder.ToString(), " Sequence");
            if (set != null)
            {
                this.gridView.DataSetSource = set;
            }
        }

        protected void btnSaveEq_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                int cid = Globals.SafeInt(this.gridView.DataKeys[i].Values[0].ToString(), 0);
                int seq = Globals.SafeInt(((TextBox) this.gridView.Rows[i].Cells[0].Controls[1]).Text, 0);
                this.classBll.UpdateSeqByCid(cid, seq);
            }
            this.gridView.OnBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DoCallback()
        {
            string str3;
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            if (((str3 = str) != null) && (str3 == "UpdateSeqNum"))
            {
                s = this.UpdateSeqNum();
            }
            else
            {
                s = this.UpdateSeqNum();
            }
            base.Response.Write(s);
            base.Response.End();
        }

        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target, 0))
                {
                    case 0:
                        return "不启用";

                    case 1:
                        return "启用";
                }
            }
            return str;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("btnModify");
                    control.Visible = false;
                }
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int classId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            CouponRule rule = new CouponRule();
            if (rule.GetRecordCount(" ClassId=" + classId) > 0)
            {
                MessageBox.ShowFailTip(this, "该分类下有优惠券数据！");
            }
            else
            {
                this.classBll.Delete(classId);
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        private string UpdateSeqNum()
        {
            JsonObject obj2 = new JsonObject();
            int cid = Globals.SafeInt(base.Request.Form["ClassId"], 0);
            int seq = Globals.SafeInt(base.Request.Form["UpdateValue"], 0);
            if ((cid == 0) || (seq == 0))
            {
                obj2.Put("STATUS", "FAILED");
            }
            else if (this.classBll.UpdateSeqByCid(cid, seq))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x199;
            }
        }
    }
}

