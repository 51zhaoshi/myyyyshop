namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class FindPassWord : UserControl
    {
        private string _errorUrl;
        private string _skipUrl;
        protected Button btnSendEmail;
        private string strWebSiteTitle;
        private Maticsoft.BLL.SysManage.VerifyMail vmbll = new Maticsoft.BLL.SysManage.VerifyMail();
        private Maticsoft.Model.SysManage.VerifyMail vmmodel = new Maticsoft.Model.SysManage.VerifyMail();

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(base.Request.Params["uid"]) && !string.IsNullOrWhiteSpace(base.Request.Params["email"]))
            {
                string recipient = base.Request.Params["email"];
                string str2 = base.Request.Params["uid"];
                string str3 = Guid.NewGuid().ToString().Replace("-", "");
                this.vmmodel.UserName = str2;
                this.vmmodel.KeyValue = str3;
                this.vmmodel.CreatedDate = DateTime.Now;
                this.vmmodel.Status = 0;
                this.vmmodel.ValidityType = 1;
                if (this.vmbll.Add(this.vmmodel))
                {
                    try
                    {
                        string str5 = ConfigSystem.GetValueByCache("GetPwdUrl") + "?keyvalue=" + str3;
                        string body = "亲爱的【" + this.StrWebSiteTitle + "】用户，请您在七天内点击（或复制到浏览器地址栏）以下连接进行密码重置: <a href=" + str5 + ">【" + str5 + "】</a>";
                        MailSender.Send(recipient, this.StrWebSiteTitle + "找回密码", body);
                        base.Response.Redirect(this.SkipUrl);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.ShowAndRedirect(this.Page, exception.Message, this.ErrorUrl);
                    }
                }
            }
        }

        public string ErrorUrl
        {
            get
            {
                return this._errorUrl;
            }
            set
            {
                this._errorUrl = value;
            }
        }

        public string SkipUrl
        {
            get
            {
                return this._skipUrl;
            }
            set
            {
                this._skipUrl = value;
            }
        }

        public string StrWebSiteTitle
        {
            get
            {
                return this.strWebSiteTitle;
            }
            set
            {
                this.strWebSiteTitle = value;
            }
        }
    }
}

