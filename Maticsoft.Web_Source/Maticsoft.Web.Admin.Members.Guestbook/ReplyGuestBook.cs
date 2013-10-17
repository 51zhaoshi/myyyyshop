namespace Maticsoft.Web.Admin.Members.Guestbook
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ReplyGuestBook : PageBaseAdmin
    {
        protected Button btnSend;
        private EmailTemplet EmailBll = new EmailTemplet();
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HiddenField hidValue;
        protected HtmlGenericControl lblTip;
        protected Literal Literal1;
        protected Literal Literal2;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox TxtReply;

        protected void btnSend_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.Members.Guestbook guestbook = new Maticsoft.BLL.Members.Guestbook();
            Maticsoft.Model.Members.Guestbook model = guestbook.GetModel(this.Id);
            try
            {
                if (model != null)
                {
                    model.ReplyDescription = this.TxtReply.Text;
                    model.Status = 1;
                    model.HandlerDate = new DateTime?(DateTime.Now);
                    model.HandlerNickName = base.CurrentUser.NickName;
                    model.HandlerUserID = new int?(base.CurrentUser.UserID);
                    if (guestbook.Update(model))
                    {
                        MessageBox.ShowSuccessTip(this, "发送成功");
                        this.lblTip.Visible = true;
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "发送失败，请检查邮件配置");
                        this.lblTip.InnerText = "出现异常，请重试";
                        this.lblTip.Visible = true;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, "发送失败，请检查邮件配置");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x11e;
            }
        }

        public int Id
        {
            get
            {
                int num = -1;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], -1);
                }
                return num;
            }
        }
    }
}

