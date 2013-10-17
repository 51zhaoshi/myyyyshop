namespace Maticsoft.Web.Admin.Shop.Supplier.SupplierInfo
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Supplier;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
        protected Button btnCancle;
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
        protected TextBox txtEstablishedDate;
        protected TextBox txtFax;
        protected TextBox txtHomePage;
        protected TextBox txtIntroduction;
        protected TextBox txtLOGO;
        protected TextBox txtMSN;
        protected TextBox txtName;
        protected TextBox txtPassword;
        protected TextBox txtPostCode;
        protected TextBox txtQQ;
        protected TextBox txtRegisteredCapital;
        protected TextBox txtRemark;
        protected TextBox txtServicePhone;
        protected TextBox txtTaxNumber;
        protected TextBox txtTelPhone;
        protected TextBox txtUserName;
        protected UpdatePanel UpdatePanel1;
        protected UpdatePanel UpdatePanel2;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string userName = this.txtUserName.Text.Trim();
            string password = this.txtPassword.Text.Trim();
            if (userName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "用户名不能为空！");
            }
            else
            {
                User user = new User();
                if (user.HasUserByUserName(userName))
                {
                    MessageBox.ShowServerBusyTip(this, Site.TooltipUserExist);
                }
                else if (password.Length == 0)
                {
                    MessageBox.ShowServerBusyTip(this, "密码不能为空！");
                }
                else if (password.Length > 0x10)
                {
                    MessageBox.ShowServerBusyTip(this, "密码不能超过16个字符！");
                }
                else
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
                    else if (this.bll.Exists(name))
                    {
                        MessageBox.ShowServerBusyTip(this, "该供应商名称已经被注册，请更换供应商名称再操作！");
                    }
                    else
                    {
                        int supplierId = 0;
                        try
                        {
                            Maticsoft.Model.Shop.Supplier.SupplierInfo model = new Maticsoft.Model.Shop.Supplier.SupplierInfo {
                                Name = name,
                                Introduction = this.txtIntroduction.Text,
                                RegisteredCapital = new int?(Globals.SafeInt(this.txtRegisteredCapital.Text, 0)),
                                TelPhone = this.txtTelPhone.Text,
                                CellPhone = this.txtCellPhone.Text,
                                ContactMail = this.txtContactMail.Text,
                                RegionId = new int?(this.RegionID.Region_iID),
                                Address = this.txtAddress.Text,
                                Remark = this.txtRemark.Text,
                                Contact = this.txtContact.Text,
                                UserName = this.txtUserName.Text
                            };
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
                            model.CreatedDate = DateTime.Now;
                            model.CreatedUserId = base.CurrentUser.UserID;
                            model.UpdatedDate = new DateTime?(DateTime.Now);
                            model.UpdatedUserId = new int?(base.CurrentUser.UserID);
                            model.Balance = Globals.SafeDecimal(this.txtBalance.Text, (decimal) 0M);
                            model.AgentId = Globals.SafeInt(this.txtAgentID.Text, 0);
                            supplierId = this.bll.Add(model);
                            if (supplierId > 0)
                            {
                                user.UserName = userName;
                                user.NickName = this.txtName.Text;
                                user.Password = AccountsPrincipal.EncryptPassword(password);
                                user.TrueName = "";
                                user.Sex = "1";
                                user.Phone = this.txtCellPhone.Text;
                                user.Email = this.txtContactMail.Text;
                                user.EmployeeID = 0;
                                user.DepartmentID = supplierId.ToString();
                                user.Activity = true;
                                user.UserType = "SP";
                                user.Style = 1;
                                user.User_dateCreate = DateTime.Now;
                                user.User_iCreator = base.CurrentUser.UserID;
                                user.User_dateValid = DateTime.Now;
                                user.User_cLang = "zh-CN";
                                user.UserID = user.Create();
                                if (user.UserID == -100)
                                {
                                    this.bll.Delete(supplierId);
                                    MessageBox.ShowServerBusyTip(this, Site.TooltipUserExist);
                                }
                                else
                                {
                                    model.UserId = user.UserID;
                                    this.bll.Update(model);
                                    int intValueByCache = ConfigSystem.GetIntValueByCache("DefaultSuppRoleID");
                                    if (intValueByCache > 0)
                                    {
                                        user.AddToRole(intValueByCache);
                                    }
                                    UsersExp exp = new UsersExp {
                                        UserID = user.UserID,
                                        BirthdayVisible = 0,
                                        BirthdayIndexVisible = false,
                                        ConstellationVisible = 0,
                                        ConstellationIndexVisible = false,
                                        NativePlaceVisible = 0,
                                        NativePlaceIndexVisible = false,
                                        RegionId = 0,
                                        AddressVisible = 0,
                                        AddressIndexVisible = false,
                                        BodilyFormVisible = 0,
                                        BodilyFormIndexVisible = false,
                                        BloodTypeVisible = 0,
                                        BloodTypeIndexVisible = false,
                                        MarriagedVisible = 0,
                                        MarriagedIndexVisible = false,
                                        PersonalStatusVisible = 0,
                                        PersonalStatusIndexVisible = false,
                                        LastAccessIP = "",
                                        LastAccessTime = new DateTime?(DateTime.Now),
                                        LastLoginTime = DateTime.Now,
                                        LastPostTime = new DateTime?(DateTime.Now)
                                    };
                                    if (exp.AddUsersExp(exp))
                                    {
                                        MessageBox.ShowSuccessTip(this, "添加成功！", "List.aspx");
                                    }
                                    else
                                    {
                                        user.Delete();
                                        exp.DeleteUsersExp(user.UserID);
                                        this.bll.Delete(supplierId);
                                        MessageBox.ShowFailTip(this, "添加失败！");
                                    }
                                }
                            }
                            else
                            {
                                this.bll.Delete(supplierId);
                                MessageBox.ShowFailTip(this, "添加失败！");
                            }
                        }
                        catch (Exception exception)
                        {
                            this.bll.Delete(supplierId);
                            throw exception;
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x218;
            }
        }
    }
}

