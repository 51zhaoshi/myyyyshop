namespace Maticsoft.Web.Controls
{
    using Maticsoft.Common;
    using Maticsoft.Payment.BLL;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ShowPaymentMode : UserControl
    {
        protected HiddenField hfShowPaymentModeSelect;
        protected Repeater rptPaymentMode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.IsEnableBalance = Globals.SafeDecimal(this.Balance, (decimal) 0M) >= this.TotalDeposits;
                this.rptPaymentMode.DataSource = PaymentModeManage.GetPaymentModes();
                this.rptPaymentMode.DataBind();
            }
        }

        public string Balance
        {
            get
            {
                if (this.ViewState["Balance"] != null)
                {
                    return this.ViewState["Balance"].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Balance"] = value;
            }
        }

        protected bool IsEnableBalance
        {
            get
            {
                return ((this.ViewState["IsEnableBalance"] != null) && Globals.SafeBool(this.ViewState["IsEnableBalance"].ToString(), false));
            }
            set
            {
                this.ViewState["IsEnableBalance"] = value;
            }
        }

        public int SelectValue
        {
            get
            {
                return Globals.SafeInt(this.hfShowPaymentModeSelect.Value, 1);
            }
            set
            {
                this.hfShowPaymentModeSelect.Value = value.ToString();
            }
        }

        public bool ShowBalanceMode
        {
            get
            {
                return ((this.ViewState["ShowRechangeMode"] != null) && Globals.SafeBool(this.ViewState["ShowRechangeMode"].ToString(), false));
            }
            set
            {
                this.ViewState["ShowRechangeMode"] = value;
            }
        }

        protected decimal TotalDeposits
        {
            get
            {
                if (this.ViewState["TotalDeposits"] != null)
                {
                    return (decimal) this.ViewState["TotalDeposits"];
                }
                return 0M;
            }
            set
            {
                this.ViewState["TotalDeposits"] = value;
            }
        }
    }
}

