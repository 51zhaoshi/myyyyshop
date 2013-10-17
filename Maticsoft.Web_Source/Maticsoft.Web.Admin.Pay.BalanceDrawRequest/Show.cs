namespace Maticsoft.Web.Admin.Pay.BalanceDrawRequest
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Pay;
    using Maticsoft.Common;
    using Maticsoft.Model.Pay;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected DropDownList dropStatus;
        protected Label lblAmount;
        protected Label lblBankCard;
        protected Label lblBankName;
        protected Label lblJournalNumber;
        protected Label lblRequestTime;
        protected Label lblTrueName;
        protected Label lblUserID;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radioCardType;
        public string strid = "";
        protected TextBox txtRemark;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.JournalNumber > 0)
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
            Maticsoft.Model.Pay.BalanceDrawRequest model = new Maticsoft.BLL.Pay.BalanceDrawRequest().GetModel((long) this.JournalNumber);
            if (model != null)
            {
                this.lblJournalNumber.Text = model.JournalNumber.ToString();
                this.lblRequestTime.Text = model.RequestTime.ToString("yyyy-MM-dd hh:mm:ss");
                this.lblAmount.Text = model.Amount.ToString("F");
                this.lblUserID.Text = new Users().GetUserName(model.UserID);
                this.lblTrueName.Text = model.TrueName;
                this.lblBankName.Text = model.BankName;
                this.lblBankCard.Text = model.BankCard;
                this.radioCardType.SelectedValue = model.CardTypeID.ToString();
                this.dropStatus.SelectedValue = model.RequestStatus.ToString();
                this.txtRemark.Text = model.Remark;
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
                return 0x2ae;
            }
        }

        private int JournalNumber
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

