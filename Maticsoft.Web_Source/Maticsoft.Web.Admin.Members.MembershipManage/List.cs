namespace Maticsoft.Web.Admin.Members.MembershipManage
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
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
        protected int Act_BatActive = 0xb7;
        protected int Act_BatUnActive = 0xb8;
        protected Button btnActivity;
        protected Button btnSearch;
        protected Button btnUnActivity;
        protected DropDownList dropType;
        protected GridViewEx gridView;
        protected Literal Literal2;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
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
            if (!string.IsNullOrEmpty(this.txtBeginTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,User_dateCreate)>='" + this.txtBeginTime.Text.Trim() + "' ");
            }
            if (Globals.SafeInt(this.dropType.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  Activity=" + this.dropType.SelectedValue + " ");
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,User_dateCreate)<='" + this.txtEndTime.Text.Trim() + "' ");
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

        protected string GetGravatar(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            string str2 = ConfigSystem.GetValue("SNSGravatarPath");
            if (string.IsNullOrEmpty(str2))
            {
                str2 = "/Upload/User/Gravatar/";
            }
            int num = Globals.SafeInt(target.ToString(), 0);
            return (str2 + num + ".jpg");
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_BatActive)) && (base.GetPermidByActID(base.Act_AddData) != -1))
                {
                    this.btnActivity.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_BatUnActive)) && (base.GetPermidByActID(this.Act_BatUnActive) != -1))
                {
                    this.btnUnActivity.Visible = false;
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
                return 0xb6;
            }
        }
    }
}

