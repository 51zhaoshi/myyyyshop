namespace Maticsoft.Web.Admin.SNS.ReportType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x97;
        protected int Act_DelData = 0x99;
        protected int Act_DeleteList = 0x9a;
        protected int Act_UpdateData = 0x98;
        private ReportType bll = new ReportType();
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl LiAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected LinkButton ltbnDelete;
        protected DropDownList rdoable;
        protected TextBox txtKeyword;

        public void BindData()
        {
            int status = Globals.SafeInt(this.rdoable.SelectedValue, -1);
            this.gridView.DataSetSource = this.bll.GetSearchList(status, this.txtKeyword.Text.Trim());
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

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName == "Status") && (e.CommandArgument != null))
            {
                int num = 0;
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                num = Globals.SafeInt(strArray[0], 0);
                if (Globals.SafeInt(strArray[1], 0) == 1)
                {
                    this.bll.UpdateList(0, num.ToString());
                    this.gridView.OnBind();
                }
                else
                {
                    this.bll.UpdateList(1, num.ToString());
                    this.gridView.OnBind();
                }
            }
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
                LinkButton button = (LinkButton) e.Row.FindControl("LinkButton1");
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    button.Visible = false;
                }
                HyperLink link = (HyperLink) e.Row.FindControl("lkEdit");
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    link.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.bll.Delete(iD))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除举报类型(id=" + iD + ")成功", this);
            }
            this.gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除举报类型(id=" + selIDlist + ")成功", this);
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除举报类型(id=" + selIDlist + ")失败", this);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.ltbnDelete.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.LiAdd.Visible = false;
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
                return 150;
            }
        }
    }
}

