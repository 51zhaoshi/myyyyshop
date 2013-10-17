namespace Maticsoft.Web.Admin
{
    using Maticsoft.Common;
    using Maticsoft.Controls;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Configuration;
    using Maticsoft.Payment.Model;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class AddPaymentMode : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnCreate;
        protected CheckBoxList chkCurrencysList;
        protected CheckBox chkIsPercent;
        protected PayInterfaceDropDownList dropPayInterface;
        protected HtmlGenericControl dropPayInterfaceTip;
        private string errorMessage = "";
        protected HtmlTextArea fcContent;
        protected HyperLink hlinkImage;
        private PaymentModeInfo item;
        protected Label Label1;
        protected Label lblCharge;
        protected Literal lblCurrencyHelp;
        protected Label lblCurrencys;
        protected Label lblDescription;
        protected Label lblDisplaySequence;
        protected Label lblEmailAddress;
        protected Label lblGateway;
        protected Literal lblHelpSecret;
        protected Label lblimage;
        protected Label lblMerchantCode;
        protected Label lblName;
        protected Literal lblPageDesc;
        protected Literal lblPageTitle;
        protected Label lblPartner;
        protected Label lblPassWord;
        protected Label lblSecondKey;
        protected Label lblSecretKey;
        protected Literal Literal1;
        protected Literal Literal2;
        protected YesNoRadioButtonList radAllowRecharge;
        protected StatusMessage statusMessage;
        protected HtmlTableRow tblCharge;
        protected HtmlTableRow tblrCurrencys;
        protected HtmlTableRow tblrImage;
        protected HtmlTableRow tblrMerchantCode;
        protected HtmlTableRow tblrPartner;
        protected HtmlTableRow tblrPassword;
        protected HtmlTableRow tblrSecondKey;
        protected HtmlTableRow tblrSecretKey;
        protected TextBox txtCharge;
        protected HtmlGenericControl txtChargeTip;
        protected TextBox txtDisplaySequence;
        protected HtmlGenericControl txtDisplaySequenceTip;
        protected TextBox txtEmailAddress;
        protected HtmlGenericControl txtEmailAddressTip;
        protected TextBox txtMerchantCode;
        protected HtmlGenericControl txtMerchantCodeTip;
        protected TextBox txtName;
        protected HtmlGenericControl txtNameTip;
        protected TextBox txtPartner;
        protected TextBox txtPassword;
        protected TextBox txtSecondKey;
        protected TextBox txtSecretKey;
        protected ValidateTarget ValidateTargetCharge;
        protected ValidateTarget ValidateTargetMerchantCode;
        protected ValidateTarget ValidateTargetName;
        protected ValidateTarget ValidateTargetPayInterface;
        protected ValidateTarget ValidateTargetSequence;
        protected ValidateTarget validatetxtEmailAddress;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!this.IsValid)
            {
                this.ShowMsg(this.ErrorMessage, false);
            }
            else
            {
                switch (PaymentModeManage.CreatePaymentMode(this.Item))
                {
                    case PaymentModeActionStatus.Success:
                        this.ShowMsg((string) HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_Message_Create_Success"), true);
                        return;

                    case PaymentModeActionStatus.DuplicateName:
                        this.ShowMsg((string) HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_PaymentNameExitis"), false);
                        return;

                    case PaymentModeActionStatus.OutofNumber:
                        this.ShowMsg((string) HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_OutofNumber"), false);
                        return;

                    case PaymentModeActionStatus.DuplicateGateway:
                        this.ShowMsg((string) HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_GatewayExists"), false);
                        return;
                }
                this.ShowMsg((string) HttpContext.GetGlobalResourceObject("AddPaymentMode", "IDS_ErrorMessage_Create_UnKnowError"), false);
            }
        }

        private void DisplayControls()
        {
            if (string.IsNullOrWhiteSpace(this.dropPayInterface.SelectedValue))
            {
                this.HiddenAll();
            }
            else
            {
                if (((this.dropPayInterface.SelectedValue.ToLower() == "cod") || (this.dropPayInterface.SelectedValue.ToLower() == "advanceaccount")) || (this.dropPayInterface.SelectedValue.ToLower() == "bank"))
                {
                    this.radAllowRecharge.SelectedValue = false;
                    this.radAllowRecharge.Enabled = false;
                }
                else
                {
                    this.radAllowRecharge.SelectedValue = true;
                    this.radAllowRecharge.Enabled = true;
                }
                GatewayProvider provider = PayConfiguration.GetConfig().Providers[this.dropPayInterface.SelectedValue] as GatewayProvider;
                this.tblrImage.Visible = provider.Attributes["emailAddress"].ToLower() == "true";
                this.tblrSecretKey.Visible = provider.Attributes["primaryKey"].ToLower() == "true";
                this.tblrSecondKey.Visible = provider.Attributes["secondKey"].ToLower() == "true";
                this.tblrPassword.Visible = provider.Attributes["password"].ToLower() == "true";
                this.tblrPartner.Visible = provider.Attributes["partner"].ToLower() == "true";
                this.tblrCurrencys.Visible = provider.SupportedCurrencys.Count > 0;
                this.tblrMerchantCode.Visible = provider.Attributes["sellerAccount"].ToLower() == "true";
                this.tblCharge.Visible = (provider.Name != "advanceaccount") && !(provider.Name == "bank");
                this.chkCurrencysList.Items.Clear();
                foreach (string str in provider.SupportedCurrencys)
                {
                    if ((provider.Name == "advanceaccount") || (provider.Name == "bank"))
                    {
                        string str2 = "CNY";
                        this.chkCurrencysList.Items.Add(new ListItem((string) HttpContext.GetGlobalResourceObject("Resources", "Currency_" + str2), str2));
                        continue;
                    }
                    this.chkCurrencysList.Items.Add(new ListItem((string) HttpContext.GetGlobalResourceObject("Resources", "Currency_" + str), str));
                }
                if ((provider.Attributes["url"] != null) && (provider.Attributes["url"].Trim().Length > 0))
                {
                    if ((provider.Attributes["logo"] != null) && (provider.Attributes["logo"].Trim().Length > 0))
                    {
                        this.hlinkImage.NavigateUrl = provider.Attributes["url"].Replace("^", "&");
                        this.lblimage.Text = string.Format(CultureInfo.InvariantCulture, "<img src=\"{0}\" border=\"0\" />", new object[] { provider.Attributes["logo"] });
                        this.lblimage.Visible = true;
                    }
                    else
                    {
                        this.lblimage.Text = provider.Attributes["url"];
                        this.lblimage.Visible = false;
                    }
                }
                else
                {
                    this.lblimage.Visible = false;
                }
            }
        }

        private void dropPayInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayControls();
        }

        private void HiddenAll()
        {
            this.chkCurrencysList.Items.Clear();
            this.lblimage.Text = string.Empty;
            this.tblrImage.Visible = false;
            this.tblrPartner.Visible = false;
            this.tblrSecondKey.Visible = false;
            this.tblrSecretKey.Visible = false;
            this.tblrPassword.Visible = false;
            this.tblrMerchantCode.Visible = false;
            this.tblrCurrencys.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
            this.dropPayInterface.SelectedIndexChanged += new EventHandler(this.dropPayInterface_SelectedIndexChanged);
            if (!this.Page.IsPostBack)
            {
                this.dropPayInterface.DataBind();
                this.DisplayControls();
                if (this.item != null)
                {
                    this.dropPayInterface.SelectedValue = this.item.Gateway;
                    this.DisplayControls();
                    this.txtName.Text = Globals.HtmlDecode(this.item.Name);
                    this.txtMerchantCode.Text = this.item.MerchantCode;
                    this.radAllowRecharge.SelectedValue = this.item.AllowRecharge;
                    this.fcContent.Value = this.item.Description;
                    if (!string.IsNullOrWhiteSpace(this.item.EmailAddress))
                    {
                        this.txtEmailAddress.Text = Globals.HtmlDecode(this.item.EmailAddress);
                    }
                    this.txtDisplaySequence.Text = this.item.DisplaySequence.ToString(CultureInfo.InvariantCulture);
                    this.txtSecretKey.Text = this.item.SecretKey;
                    this.txtSecondKey.Text = this.item.SecondKey;
                    this.txtCharge.Text = this.item.Charge.ToString("F", CultureInfo.InvariantCulture);
                    this.chkIsPercent.Checked = this.item.IsPercent;
                    if (!string.IsNullOrWhiteSpace(this.item.Password))
                    {
                        this.txtPassword.Text = this.item.Password;
                    }
                    if (!string.IsNullOrWhiteSpace(this.item.Partner))
                    {
                        this.txtPartner.Text = Globals.HtmlDecode(this.item.Partner);
                    }
                    foreach (string str in this.item.SupportedCurrencys)
                    {
                        if (str == "")
                        {
                            this.chkCurrencysList.Items[0].Selected = true;
                        }
                        else
                        {
                            this.chkCurrencysList.Items.FindByValue(str).Selected = true;
                        }
                    }
                }
            }
            this.txtName.Focus();
        }

        protected virtual void ShowMsg(string msg, bool success)
        {
            this.ShowMsg(msg, success, false);
        }

        private void ShowMsg(string msg, bool success, bool isWarning)
        {
            this.statusMessage.Success = success;
            this.statusMessage.IsWarning = isWarning;
            this.statusMessage.Text = msg;
            this.statusMessage.Visible = true;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x15c;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
        }

        public bool IsValid
        {
            get
            {
                this.errorMessage = "";
                bool flag = true;
                if (string.IsNullOrWhiteSpace(this.dropPayInterface.SelectedValue))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_SelectPayInterface"));
                    return false;
                }
                if (this.txtName.Text.Trim().Length == 0)
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymentModeName_NotInput"));
                    flag = false;
                }
                if (this.txtName.Text.Trim().Length > 200)
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymentModeName_OutLength"));
                    flag = false;
                }
                if (this.tblrMerchantCode.Visible && (this.txtMerchantCode.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_MerchantCode_NotInput"));
                    flag = false;
                }
                if (this.tblrSecretKey.Visible && (this.txtSecretKey.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_SecretKey_NotInput"));
                    flag = false;
                }
                if (this.tblrSecondKey.Visible && (this.txtSecondKey.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_SecondKey_NotInput"));
                    flag = false;
                }
                if (this.tblrPassword.Visible && (this.txtPassword.Text.Trim().Length == 0))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_Password_NotInput"));
                    flag = false;
                }
                if (this.tblrCurrencys.Visible && (this.chkCurrencysList.SelectedIndex == -1))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymnetMode_Currency_NotSelect"));
                    flag = false;
                }
                if (this.tblCharge.Visible && (Globals.SafeDecimal(this.txtCharge.Text, (decimal) -1M) <= -1M))
                {
                    this.errorMessage = this.errorMessage + Formatter.FormatErrorMessage((string) HttpContext.GetGlobalResourceObject("PaymentModeView", "IDS_PaymentMode_Charge_Error"));
                    flag = false;
                }
                return flag;
            }
        }

        public PaymentModeInfo Item
        {
            get
            {
                GatewayProvider provider = PayConfiguration.GetConfig().Providers[this.dropPayInterface.SelectedValue] as GatewayProvider;
                PaymentModeInfo info2 = new PaymentModeInfo {
                    ModeId = Globals.SafeInt(this.Page.Request.QueryString["modeId"], -1),
                    MerchantCode = this.txtMerchantCode.Text.Trim(),
                    EmailAddress = Globals.HtmlEncode(this.txtEmailAddress.Text.Trim()),
                    SecretKey = this.txtSecretKey.Text.Trim(),
                    SecondKey = this.txtSecondKey.Text.Trim(),
                    Password = this.txtPassword.Text.Trim(),
                    Partner = Globals.HtmlEncode(this.txtPartner.Text.Trim()),
                    Name = Globals.HtmlEncode(this.txtName.Text.Trim()),
                    Description = this.fcContent.Value.Replace("\r\n", "").Replace("\r", "").Replace("\n", ""),
                    Gateway = this.dropPayInterface.SelectedValue.ToLower(),
                    DisplaySequence = Globals.SafeInt(this.txtDisplaySequence.Text.Trim(), -1),
                    AllowRecharge = this.radAllowRecharge.SelectedValue
                };
                PaymentModeInfo info = info2;
                if ((provider.Name != "advanceaccount") && (provider.Name != "bank"))
                {
                    info.Charge = Globals.SafeDecimal(this.txtCharge.Text.Trim(), (decimal) 0M);
                    info.IsPercent = this.chkIsPercent.Checked;
                }
                foreach (ListItem item in this.chkCurrencysList.Items)
                {
                    if (item.Selected)
                    {
                        info.SupportedCurrencys.Add(item.Value);
                    }
                }
                return info;
            }
            set
            {
                this.item = value;
                this.item.Gateway = this.item.Gateway.ToLower();
            }
        }
    }
}

