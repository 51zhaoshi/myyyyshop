namespace Maticsoft.Web.Admin.Accounts
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Email;
    using Maticsoft.Email.Model;
    using Maticsoft.Model;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class SendEmail : PageBaseAdmin
    {
        protected int Act_UpdateData = 0xa5;
        protected Button btnNext;
        protected Button btnTestSend;
        protected DropDownList DropUserType;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected RadioButton RadioButton1;
        protected RadioButton RadioButton2;
        protected RequiredFieldValidator rfvTitle;
        protected TextBox txtContent;
        protected TextBox txtKeyword;
        protected TextBox txtTitle;

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                MessageBox.ShowFailTip(this, SysManage.ErrorSubjectNotNull);
            }
            else if (string.IsNullOrWhiteSpace(this.txtContent.Text))
            {
                MessageBox.ShowFailTip(this, SysManage.ErrorContentNotNull);
            }
            else
            {
                User user = new User();
                if ((this.RadioButton2.Checked && string.IsNullOrWhiteSpace(this.txtKeyword.Text)) && !user.HasUserByUserName(this.txtKeyword.Text))
                {
                    MessageBox.ShowFailTip(this, SysManage.ErrorUserInexistence);
                }
                else if (this.RadioButton1.Checked)
                {
                    if (EmailManage.PushQueue(this.DropUserType.SelectedValue, "", this.txtTitle.Text, this.txtContent.Text, ""))
                    {
                        MessageBox.ShowSuccessTip(this, SysManage.TooltipSentSuccessfully, "SendEmail.aspx");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, SysManage.TooltipSentFailed, "SendEmail.aspx");
                    }
                }
                else if (EmailManage.PushQueue("", this.txtKeyword.Text, this.txtTitle.Text, this.txtContent.Text, ""))
                {
                    MessageBox.ShowSuccessTip(this, SysManage.TooltipSentSuccessfully, "SendEmail.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, SysManage.TooltipSentFailed, "SendEmail.aspx");
                }
            }
        }

        protected void btnTestSend_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.MailConfig model = new Maticsoft.BLL.MailConfig().GetModel(base.CurrentUser.UserID);
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.CMS);
            string webName = set.WebName;
            string str2 = string.Format(SysManage.EmailBodyTemplate, webName);
            EmailQueue queue = new EmailQueue {
                EmailTo = model.Mailaddress,
                EmailSubject = string.Format(SysManage.EmailSubjectTemplate, webName),
                EmailFrom = model.Mailaddress,
                EmailBody = str2,
                EmailPriority = 0,
                IsBodyHtml = false,
                NextTryTime = DateTime.Now
            };
            if (EmailManage.PushQueue(queue))
            {
                MessageBox.ShowSuccessTip(this, SysManage.TooltipNoteToCheck);
            }
            else
            {
                MessageBox.ShowFailTip(this, SysManage.TooltipRetryAfter);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    this.btnNext.Visible = false;
                }
                if (!string.IsNullOrWhiteSpace(base.Request.Params["content"]))
                {
                    this.txtContent.Text = base.Server.UrlDecode(base.Request.Params["content"]);
                }
                if (!string.IsNullOrWhiteSpace(base.Request.Params["title"]))
                {
                    this.txtTitle.Text = base.Server.UrlDecode(base.Request.Params["title"]);
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xa4;
            }
        }
    }
}

