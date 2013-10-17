namespace Maticsoft.Web.Ms.Enterprise
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Label lblAccountBank;
        protected Label lblAccountInfo;
        protected Label lblAddress;
        protected Label lblAgentID;
        protected Label lblArtiPerson;
        protected Label lblBalance;
        protected Label lblBusinessLicense;
        protected Label lblCellPhone;
        protected Label lblCompanyType;
        protected Label lblContact;
        protected Label lblContactMail;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblEnteClassName;
        protected Label lblEnteRank;
        protected Label lblEnterpriseID;
        protected Label lblEstablishedDate;
        protected Label lblFax;
        protected Label lblHomePage;
        protected Label lblIntroduction;
        protected Label lblLOGO;
        protected Label lblMSN;
        protected Label lblName;
        protected Label lblPostCode;
        protected Label lblQQ;
        protected Label lblRegisteredCapital;
        protected Label lblRemark;
        protected Label lblServicePhone;
        protected Label lblStatus;
        protected Label lblTaxNumber;
        protected Label lblTelPhone;
        protected Label lblUpdatedDate;
        protected Label lblUpdatedUserID;
        protected Label lblUserName;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Region RegionEstablishedCity;
        protected Region RegionID;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public string GetCompanyType(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return "国营企业";
                }
            }
            else
            {
                return "个体工商";
            }
            return "私营独资企业";
        }

        public string GetEnteClassName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        return "合资";

                    case "2":
                        return "独资";

                    case "3":
                        return "国有";

                    case "4":
                        return "私营";

                    case "5":
                        return "全民所有制";

                    case "6":
                        return "集体所有制";

                    case "7":
                        return "股份制";

                    case "8":
                        return "有限责任制";
                }
            }
            return str;
        }

        public string GetEnteRank(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    if (str2 == "3")
                    {
                        return "三星级";
                    }
                    if (str2 == "4")
                    {
                        return "四星级";
                    }
                    if (str2 != "5")
                    {
                        return str;
                    }
                    return "五星级";
                }
            }
            else
            {
                return "一星级";
            }
            return "二星级";
        }

        public string GetStatus(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 == "2")
                    {
                        return "冻结";
                    }
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return "删除";
                }
            }
            else
            {
                return "未审核";
            }
            return "正常";
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
            Maticsoft.Model.Ms.Enterprise model = new Maticsoft.BLL.Ms.Enterprise().GetModel(this.EnterpriseID);
            if (model != null)
            {
                this.lblEnterpriseID.Text = model.EnterpriseID.ToString();
                this.lblName.Text = model.Name;
                this.lblIntroduction.Text = model.Introduction;
                if (model.RegisteredCapital.HasValue)
                {
                    this.lblRegisteredCapital.Text = model.RegisteredCapital.ToString();
                }
                this.lblTelPhone.Text = model.TelPhone;
                this.lblCellPhone.Text = model.CellPhone;
                this.lblContactMail.Text = model.ContactMail;
                if (model.RegionID.HasValue)
                {
                    this.RegionID.Region_iID = model.RegionID.Value;
                }
                this.lblAddress.Text = model.Address;
                this.lblRemark.Text = model.Remark;
                this.lblContact.Text = model.Contact;
                this.lblUserName.Text = model.UserName;
                if (model.EstablishedDate.HasValue)
                {
                    this.lblEstablishedDate.Text = model.EstablishedDate.ToString();
                }
                if (model.EstablishedCity.HasValue)
                {
                    this.RegionEstablishedCity.Region_iID = model.EstablishedCity.Value;
                }
                this.lblLOGO.Text = model.LOGO;
                this.lblFax.Text = model.Fax;
                this.lblPostCode.Text = model.PostCode;
                this.lblHomePage.Text = model.HomePage;
                this.lblArtiPerson.Text = model.ArtiPerson;
                if (model.EnteRank.HasValue)
                {
                    this.lblEnteRank.Text = this.GetEnteRank(model.EnteRank);
                }
                if (model.EnteClassID.HasValue)
                {
                    this.lblEnteClassName.Text = this.GetEnteClassName(model.EnteClassID);
                }
                if (model.CompanyType.HasValue)
                {
                    this.lblCompanyType.Text = this.GetCompanyType(model.CompanyType);
                }
                this.lblBusinessLicense.Text = model.BusinessLicense;
                this.lblTaxNumber.Text = model.TaxNumber;
                this.lblAccountBank.Text = model.AccountBank;
                this.lblAccountInfo.Text = model.AccountInfo;
                this.lblServicePhone.Text = model.ServicePhone;
                this.lblQQ.Text = model.QQ;
                this.lblMSN.Text = model.MSN;
                if (model.Status.HasValue)
                {
                    this.lblStatus.Text = this.GetStatus(model.Status);
                }
                if (model.CreatedDate.HasValue)
                {
                    this.lblCreatedDate.Text = model.CreatedDate.ToString();
                }
                this.lblCreatedUserID.Text = model.CreatedUserName;
                if (model.UpdatedDate.HasValue)
                {
                    this.lblUpdatedDate.Text = model.UpdatedDate.ToString();
                }
                this.lblUpdatedUserID.Text = model.UpdatedUserName;
                this.lblBalance.Text = model.Balance.ToString("F2");
                this.lblAgentID.Text = model.AgentID.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 320;
            }
        }

        public int EnterpriseID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

