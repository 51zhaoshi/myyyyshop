namespace Maticsoft.Web.Admin.AdvertisePosition
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0xa7;
        protected int Act_DelData = 0xa9;
        protected int Act_DeleteList = 170;
        protected int Act_UpdateData = 0xa8;
        private AdvertisePosition bll = new AdvertisePosition();
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtKeyword;

        public string AdPositionTag(object objPID, object objW, object objH)
        {
            if (objPID == null)
            {
                return "";
            }
            if (string.IsNullOrWhiteSpace(objPID.ToString()))
            {
                return "";
            }
            if ((objW != null) && (objH != null))
            {
                return HttpUtility.HtmlEncode("<script src=\"js/showads.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = 'c=" + objPID.ToString() + "&a=0'; </script>  ");
            }
            return HttpUtility.HtmlEncode("<script src=\"js/showads.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = 'c=" + objPID.ToString() + "a=0'; </script>  ");
        }

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[8].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[9].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text))
            {
                builder.AppendFormat("AdvPositionName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected string ConvertShowType(object obj)
        {
            if (obj != null)
            {
                switch (obj.ToString())
                {
                    case "0":
                        return "纵向平铺";

                    case "1":
                        return "横向平铺";

                    case "2":
                        return "层叠显示";

                    case "3":
                        return "交替显示";

                    case "4":
                        return "自定义广告位";
                }
            }
            return "未定义的显示类型";
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
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
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                Literal literal = (Literal) e.Row.FindControl("SetAdContent");
                object obj2 = DataBinder.Eval(e.Row.DataItem, "ShowType");
                object obj3 = DataBinder.Eval(e.Row.DataItem, "AdvPositionId");
                if ((obj2 != null) && (Globals.SafeInt(obj2.ToString(), -1) != 4))
                {
                    literal.Text = string.Format("<a href=\"../Advertisement/SingleList.aspx?id={0}\">设置广告内容</a>&nbsp;&nbsp;", obj3);
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int advPositionId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(advPositionId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xa6;
            }
        }
    }
}

