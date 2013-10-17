namespace Maticsoft.Web.SNS.SearchWordLog
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x91;
        protected int Act_DeleteList = 0x91;
        private HotWords bllHotWords = new HotWords();
        private SearchWordLog bllSearchWordLog = new SearchWordLog();
        protected Button btnDelete;
        protected Button btnPush;
        protected Button btnSearch;
        protected CheckBox chkDelete;
        protected DropDownList dropTop;
        protected GridViewEx gridView;
        protected LinkButton lbtnDelete;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;
        protected TextBox txtPoster;
        protected TextBox txtTop;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[6].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.txtPoster.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("CreatedNickName like '%{0}%' ", this.txtPoster.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtKeyword.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("SearchWord like '%{0}%' ", this.txtKeyword.Text.Trim());
            }
            if (PageValidate.IsDateTime(this.txtBeginTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,CreatedDate)>='" + this.txtBeginTime.Text.Trim() + "' ");
            }
            if (PageValidate.IsDateTime(this.txtEndTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,CreatedDate)<='" + this.txtEndTime.Text.Trim() + "' ");
            }
            this.gridView.DataSetSource = this.bllSearchWordLog.GetList(-1, builder.ToString(), " CreatedDate desc");
        }

        protected void btnPush_Click(object sender, EventArgs e)
        {
            if (this.chkDelete.Checked)
            {
                this.bllHotWords.Delete();
            }
            int top = 0;
            if (this.dropTop.SelectedValue == "0")
            {
                top = Globals.SafeInt(this.txtTop.Text, 0);
            }
            else
            {
                top = Globals.SafeInt(this.dropTop.SelectedValue, 0);
            }
            if (this.bllSearchWordLog.GetHotHotWordssList(top))
            {
                this.txtTop.Text = "";
                this.dropTop.SelectedValue = "0";
                base.Response.Redirect("/admin/SNS/SearchWord/SearchTop.aspx");
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

        public string GetState(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "不可用";

                    case 1:
                        return "可用";

                    case 2:
                        return str;

                    case 3:
                        return "被推荐到逛宝贝的热搜词中";

                    case 4:
                        return "被推荐到搜索框下面的热搜词中";
                }
            }
            return str;
        }

        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "不可用";

                    case 1:
                        return "可用";
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
            this.bllSearchWordLog.Delete(iD);
            this.gridView.OnBind();
        }

        protected void gridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bllSearchWordLog.DeleteList(selIDlist))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除搜索日志(id=" + selIDlist + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList))) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
            {
                this.liDel.Visible = false;
                this.lbtnDelete.Visible = false;
                this.btnDelete.Visible = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x90;
            }
        }
    }
}

