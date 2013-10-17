namespace Maticsoft.Web.SNS.AlbumType
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
        protected int Act_AddData = 0x61;
        protected int Act_DelData = 0x63;
        protected int Act_DeleteList = 0x60;
        protected int Act_UpdateData = 0x62;
        protected HtmlGenericControl AddLi;
        private AlbumType bll = new AlbumType();
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HyperLink hlkadd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtKeyword;

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
            this.gridView.DataSetSource = this.bll.GetSearchList(this.txtKeyword.Text.Trim());
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除专辑类型(id=" + selIDlist + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除专辑类型(id=" + selIDlist + ")失败", this);
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

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName == "IsMenu") && (e.CommandArgument != null))
            {
                int num = 0;
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                num = Globals.SafeInt(strArray[0], 0);
                if (Globals.SafeBool(strArray[1], false))
                {
                    this.bll.UpdateIsMenu(0, num.ToString());
                    this.gridView.OnBind();
                }
                else
                {
                    this.bll.UpdateIsMenu(1, num.ToString());
                    this.gridView.OnBind();
                }
            }
            if ((e.CommandName == "MenuIsShow") && (e.CommandArgument != null))
            {
                int num2 = 0;
                string[] strArray2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                num2 = Globals.SafeInt(strArray2[0], 0);
                if (Globals.SafeBool(strArray2[1], false))
                {
                    this.bll.UpdateMenuIsShow(0, num2.ToString());
                    this.gridView.OnBind();
                }
                else
                {
                    this.bll.UpdateMenuIsShow(1, num2.ToString());
                    this.gridView.OnBind();
                }
            }
            if ((e.CommandName == "Status") && (e.CommandArgument != null))
            {
                int num3 = 0;
                string[] strArray3 = e.CommandArgument.ToString().Split(new char[] { ',' });
                num3 = Globals.SafeInt(strArray3[0], 0);
                if (Globals.SafeInt(strArray3[1], 0) == 1)
                {
                    this.bll.UpdateStatus(0, num3.ToString());
                    this.gridView.OnBind();
                }
                else
                {
                    this.bll.UpdateStatus(1, num3.ToString());
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.bll.Delete(iD))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            }
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
                    this.AddLi.Visible = false;
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
                return 0x5f;
            }
        }
    }
}

