namespace Maticsoft.Web.Admin.SNS.StarType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0xba;
        protected int Act_DelData = 0xbc;
        protected int Act_DeleteList = 0xbd;
        protected int Act_UpdateData = 0xbb;
        private StarType bll = new StarType();
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal7;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[6].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("TypeName like '%{0}%'", this.txtKeyword.Text.Trim());
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
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除达人类型(id=" + selIDlist + ")成功", this);
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除达人类型(id=" + selIDlist + ")失败", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
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

        protected string GetStatus(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (Globals.SafeInt(target.ToString(), 0))
            {
                case 0:
                    return "未审核";

                case 1:
                    return "已审核";

                case 2:
                    return "审核未通过";
            }
            return "未审核";
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int typeID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(typeID);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xb9;
            }
        }
    }
}

