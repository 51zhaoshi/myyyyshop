namespace Maticsoft.Web.Supplier.SupplierInfo
{
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Supplier;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseSupplier
    {
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
        protected Button btnSave;
        protected DropDownList dropCompanyType;
        protected DropDownList dropEnteClassID;
        protected DropDownList dropEnteRank;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected Region RegionEstablishedCity;
        protected Region RegionID;
        protected TextBox txtAccountBank;
        protected TextBox txtAccountInfo;
        protected TextBox txtAddress;
        protected TextBox txtAgentID;
        protected TextBox txtArtiPerson;
        protected TextBox txtBalance;
        protected TextBox txtBusinessLicense;
        protected TextBox txtCellPhone;
        protected TextBox txtContact;
        protected TextBox txtContactMail;
        protected TextBox txtCreatedDate;
        protected TextBox txtCreatedUserID;
        protected TextBox txtEstablishedDate;
        protected TextBox txtFax;
        protected TextBox txtHomePage;
        protected TextBox txtIntroduction;
        protected TextBox txtLOGO;
        protected TextBox txtMSN;
        protected TextBox txtName;
        protected TextBox txtPostCode;
        protected TextBox txtQQ;
        protected TextBox txtRegisteredCapital;
        protected TextBox txtRemark;
        protected TextBox txtServicePhone;
        protected TextBox txtTaxNumber;
        protected TextBox txtTelPhone;
        protected TextBox txtUpdatedDate;
        protected TextBox txtUpdatedUserID;
        protected TextBox txtUserName;
        protected UpdatePanel UpdatePanel1;
        protected UpdatePanel UpdatePanel2;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "供应商名称不能为空！");
            }
            else if (name.Length > 100)
            {
                MessageBox.ShowServerBusyTip(this, "供应商名称请控制在1~100字符！");
            }
            else if (this.bll.Exists(name, base.SupplierId))
            {
                MessageBox.ShowServerBusyTip(this, "该供应商名称已经被注册，请更换供应商名称再操作！");
            }
            else
            {
                Maticsoft.Model.Shop.Supplier.SupplierInfo model = this.bll.GetModel(base.SupplierId);
                if (model != null)
                {
                    model.Name = name;
                    model.Introduction = this.txtIntroduction.Text;
                    model.RegisteredCapital = new int?(Globals.SafeInt(this.txtRegisteredCapital.Text, 0));
                    model.TelPhone = this.txtTelPhone.Text;
                    model.CellPhone = this.txtCellPhone.Text;
                    model.ContactMail = this.txtContactMail.Text;
                    model.RegionId = new int?(this.RegionID.Region_iID);
                    model.Address = this.txtAddress.Text;
                    model.Remark = this.txtRemark.Text;
                    model.Contact = this.txtContact.Text;
                    string text = this.txtEstablishedDate.Text;
                    if (PageValidate.IsDateTime(text))
                    {
                        model.EstablishedDate = new DateTime?(Globals.SafeDateTime(text, DateTime.Now));
                    }
                    else
                    {
                        model.EstablishedDate = null;
                    }
                    model.EstablishedCity = new int?(this.RegionEstablishedCity.Region_iID);
                    model.LOGO = this.txtLOGO.Text;
                    model.Fax = this.txtFax.Text;
                    model.PostCode = this.txtPostCode.Text;
                    model.HomePage = this.txtHomePage.Text;
                    model.ArtiPerson = this.txtArtiPerson.Text;
                    model.Rank = Globals.SafeInt(this.dropEnteRank.SelectedValue, 0);
                    model.CategoryId = Globals.SafeInt(this.dropEnteClassID.SelectedValue, 0);
                    model.CompanyType = new int?(Globals.SafeInt(this.dropCompanyType.SelectedValue, 0));
                    model.BusinessLicense = this.txtBusinessLicense.Text;
                    model.TaxNumber = this.txtTaxNumber.Text;
                    model.AccountBank = this.txtAccountBank.Text;
                    model.AccountInfo = this.txtAccountInfo.Text;
                    model.ServicePhone = this.txtServicePhone.Text;
                    model.QQ = this.txtQQ.Text;
                    model.MSN = this.txtMSN.Text;
                    model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                    model.UpdatedDate = new DateTime?(DateTime.Now);
                    model.UpdatedUserId = new int?(base.CurrentUser.UserID);
                    model.Balance = Globals.SafeDecimal(this.txtBalance.Text, (decimal) 0M);
                    model.AgentId = Globals.SafeInt(this.txtAgentID.Text, 0);
                    if (this.bll.Update(model))
                    {
                        MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "Modify.aspx");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Supplier.SupplierInfo model = this.bll.GetModel(base.SupplierId);
            if (model != null)
            {
                this.txtName.Text = model.Name;
                this.txtIntroduction.Text = model.Introduction;
                if (model.RegisteredCapital.HasValue)
                {
                    this.txtRegisteredCapital.Text = model.RegisteredCapital.ToString();
                }
                this.txtTelPhone.Text = model.TelPhone;
                this.txtCellPhone.Text = model.CellPhone;
                this.txtContactMail.Text = model.ContactMail;
                if (model.RegionId.HasValue)
                {
                    this.RegionID.Region_iID = model.RegionId.Value;
                }
                this.txtAddress.Text = model.Address;
                this.txtRemark.Text = model.Remark;
                this.txtContact.Text = model.Contact;
                if (model.EstablishedDate.HasValue)
                {
                    this.txtEstablishedDate.Text = model.EstablishedDate.Value.ToString("yyyy-MM-dd");
                }
                if (model.EstablishedCity.HasValue)
                {
                    this.RegionEstablishedCity.Region_iID = model.EstablishedCity.Value;
                }
                this.txtFax.Text = model.Fax;
                this.txtPostCode.Text = model.PostCode;
                this.txtHomePage.Text = model.HomePage;
                this.txtArtiPerson.Text = model.ArtiPerson;
                if (model.Rank > 0)
                {
                    this.dropEnteRank.SelectedValue = model.Rank.ToString();
                }
                if (model.CategoryId > 0)
                {
                    this.dropEnteClassID.SelectedValue = model.CategoryId.ToString();
                }
                if (model.CompanyType.HasValue)
                {
                    this.dropCompanyType.SelectedValue = model.CompanyType.ToString();
                }
                this.txtTaxNumber.Text = model.TaxNumber;
                this.txtAccountBank.Text = model.AccountBank;
                this.txtAccountInfo.Text = model.AccountInfo;
                this.txtServicePhone.Text = model.ServicePhone;
                this.txtQQ.Text = model.QQ;
                this.txtMSN.Text = model.MSN;
                if (model.CompanyType.HasValue)
                {
                    this.radlStatus.SelectedValue = model.Status.ToString();
                }
                this.txtBalance.Text = model.Balance.ToString("F2");
            }
        }
    }
}

