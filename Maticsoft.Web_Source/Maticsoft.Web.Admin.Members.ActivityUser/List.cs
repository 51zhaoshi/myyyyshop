namespace Maticsoft.Web.Admin.Members.ActivityUser
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected Button btnActivity;
        protected Button btnSearch;
        protected Button btnUnActivity;
        protected DropDownList dropActive;
        protected DropDownList dropUnActive;
        protected GridViewEx gridView;
        protected Literal Literal2;
        private UserRank rankBll = new UserRank();
        protected TextBox txtKeyword;
        private Users user = new Users();

        public void BindData()
        {
            DataSet searchList = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.Append(" NickName like '%" + this.txtKeyword.Text + "%' ");
            }
            if (Globals.SafeInt(this.dropActive.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  DATEDIFF(month,LastLoginTime,GETDATE())<" + this.dropActive.SelectedValue + " ");
            }
            if (Globals.SafeInt(this.dropUnActive.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  DATEDIFF(month,LastLoginTime,GETDATE())>" + this.dropUnActive.SelectedValue + " ");
            }
            searchList = this.user.GetSearchList("UU", builder.ToString());
            this.gridView.DataSetSource = searchList;
        }

        protected void btnActivity_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.user.UpdateActiveStatus(selIDlist, 1))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnUnActivity_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.user.UpdateActiveStatus(selIDlist, 0))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                this.gridView.OnBind();
            }
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
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                AccountsPrincipal existingPrincipal = new AccountsPrincipal(Globals.SafeInt(strArray[0], 0));
                User user = new User(existingPrincipal);
                bool flag = Globals.SafeBool(strArray[1], false);
                user.Activity = !flag;
                user.Update();
                this.gridView.OnBind();
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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
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
                return 0xd0;
            }
        }
    }
}

