namespace Maticsoft.Web.Admin.Members.UserInvite
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Label lblCreatedDate;
        protected Label lblInviteId;
        protected Label lblInviteNick;
        protected Label lblInviteUserId;
        protected Label lblIsNew;
        protected Label lblIsRebate;
        protected Label lblRebateDesc;
        protected Label lblRemark;
        protected Label lblUserId;
        protected Label lblUserNick;
        protected Literal Literal2;
        protected Literal Literal3;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.InviteId > 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！", "list.aspx");
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Members.UserInvite model = new Maticsoft.BLL.Members.UserInvite().GetModel(this.InviteId);
            if (model != null)
            {
                this.lblInviteId.Text = model.InviteId.ToString();
                this.lblUserId.Text = model.UserId.ToString();
                this.lblUserNick.Text = model.UserNick;
                this.lblInviteUserId.Text = model.InviteUserId.ToString();
                this.lblInviteNick.Text = model.InviteNick;
                this.lblIsRebate.Text = model.IsRebate ? "是" : "否";
                this.lblIsNew.Text = model.IsNew ? "是" : "否";
                this.lblCreatedDate.Text = model.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss");
                this.lblRemark.Text = model.Remark;
                this.lblRebateDesc.Text = model.RebateDesc;
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！", "list.aspx");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x2b1;
            }
        }

        private int InviteId
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

