namespace Maticsoft.Web.Admin.Shop.Consultations
{
    using Maticsoft.BLL;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Email;
    using Maticsoft.Email.Model;
    using Maticsoft.Model;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductConsults bll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chbSendEmail;
        protected CheckBox chkIsStatus;
        protected Literal Literal2;
        protected TextBox txtReplyText;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string text = this.txtReplyText.Text;
            bool flag = this.chkIsStatus.Checked;
            Maticsoft.Model.Shop.Products.ProductConsults model = this.bll.GetModel(this.ConsultationId);
            model.ConsultationId = this.ConsultationId;
            model.ReplyDate = new DateTime?(DateTime.Now);
            model.IsReply = true;
            model.ReplyText = text;
            model.ReplyUserId = base.CurrentUser.UserID;
            model.ReplyUserName = base.CurrentUser.UserName;
            model.Status = flag ? 1 : 0;
            if (this.bll.Update(model))
            {
                if (this.chbSendEmail.Checked)
                {
                    this.SendEmail(model.UserEmail);
                }
                MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
            }
            else
            {
                MessageBox.ShowFailTip(this, "网络异常，请稍后再试");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.ConsultationId > 0))
            {
                this.ShowInfo(this.ConsultationId);
            }
        }

        private void SendEmail(string email)
        {
            Maticsoft.Model.MailConfig model = new Maticsoft.BLL.MailConfig().GetModel();
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            string webName = set.WebName;
            string str2 = string.Format("您对商品【{0}】的咨询有了新的回复，请及时查看！", webName);
            EmailQueue queue = new EmailQueue {
                EmailTo = model.Mailaddress,
                EmailSubject = string.Format("{0}回复通知", webName),
                EmailFrom = model.Mailaddress,
                EmailBody = str2,
                EmailPriority = 0,
                IsBodyHtml = false,
                NextTryTime = DateTime.Now
            };
            EmailManage.PushQueue(queue);
        }

        private void ShowInfo(int ConsultationId)
        {
            Maticsoft.Model.Shop.Products.ProductConsults consults = new Maticsoft.Model.Shop.Products.ProductConsults();
            this.txtReplyText.Text = consults.ReplyText;
            this.chkIsStatus.Checked = consults.Status == 1;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x195;
            }
        }

        public int ConsultationId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

