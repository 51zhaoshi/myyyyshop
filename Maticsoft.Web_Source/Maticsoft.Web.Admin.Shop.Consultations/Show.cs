namespace Maticsoft.Web.Admin.Shop.Consultations
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected CheckBox chkIsReply;
        protected CheckBox chkIsStatus;
        protected Label lblConsultationId;
        protected Label lblConsultationText;
        protected Label lblCreatedDate;
        protected Label lblProductId;
        protected Label lblReplyDate;
        protected Label lblReplyText;
        protected Label lblReplyUserId;
        protected Label lblState;
        protected Label lblUserEmail;
        protected Label lblUserId;
        protected Label lblUserName;
        protected Literal Literal2;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int consultationId = Convert.ToInt32(this.strid);
                this.ShowInfo(consultationId);
            }
        }

        private void ShowInfo(int ConsultationId)
        {
            Maticsoft.Model.Shop.Products.ProductConsults model = new Maticsoft.BLL.Shop.Products.ProductConsults().GetModel(ConsultationId);
            this.lblConsultationId.Text = model.ConsultationId.ToString();
            this.lblUserId.Text = model.UserId.ToString();
            this.lblProductId.Text = model.ProductId.ToString();
            this.lblUserName.Text = model.UserName;
            this.lblUserEmail.Text = model.UserEmail;
            this.lblConsultationText.Text = model.ConsultationText;
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblReplyDate.Text = model.ReplyDate.ToString();
            this.chkIsReply.Checked = model.IsReply;
            this.lblReplyText.Text = model.ReplyText;
            this.lblReplyUserId.Text = model.ReplyUserId.ToString();
            this.chkIsStatus.Checked = model.Status == 1;
            this.lblUserId.Text = new User().GetTrueNameByCache(model.UserId);
            Maticsoft.Model.Shop.Products.ProductInfo info2 = new Maticsoft.BLL.Shop.Products.ProductInfo().GetModel(model.ProductId);
            this.lblProductId.Text = info2.ProductName;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x196;
            }
        }
    }
}

