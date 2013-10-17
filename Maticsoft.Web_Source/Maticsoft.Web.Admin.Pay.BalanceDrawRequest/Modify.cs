namespace Maticsoft.Web.Admin.Pay.BalanceDrawRequest
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Pay;
    using Maticsoft.Common;
    using Maticsoft.Model.Pay;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList dropStatus;
        protected HtmlInputHidden hidColse;
        protected Label lblAmount;
        protected Label lblBankCard;
        protected Label lblBankName;
        protected Label lblRequestTime;
        protected Label lblTrueName;
        protected Label lblUserID;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radioCardType;
        protected TextBox txtRemark;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtRemark.Text.Trim().Length > 0x7d0)
            {
                msg = msg + @"备注过长！\n";
            }
            if (msg != "")
            {
                MessageBox.Show(this, msg);
            }
            else
            {
                int num = int.Parse(this.dropStatus.SelectedValue);
                string text = this.txtRemark.Text;
                Maticsoft.BLL.Pay.BalanceDrawRequest request = new Maticsoft.BLL.Pay.BalanceDrawRequest();
                Maticsoft.Model.Pay.BalanceDrawRequest modelByCache = request.GetModelByCache((long) this.JournalNumber);
                if (modelByCache != null)
                {
                    modelByCache.RequestStatus = num;
                    modelByCache.Remark = text;
                    if (request.Update(modelByCache))
                    {
                        this.hidColse.Value = "close";
                        DataCache.DeleteCache("BalanceDrawRequestModel-" + this.JournalNumber);
                        MessageBox.ShowSuccessTip(this, "保存成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "保存失败！");
                    }
                }
                else
                {
                    this.hidColse.Value = "close";
                    MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！");
                }
            }
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
                    this.hidColse.Value = "close";
                    MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！");
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Pay.BalanceDrawRequest model = new Maticsoft.BLL.Pay.BalanceDrawRequest().GetModel((long) this.JournalNumber);
            if (model != null)
            {
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
                this.hidColse.Value = "close";
                MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x2ad;
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

