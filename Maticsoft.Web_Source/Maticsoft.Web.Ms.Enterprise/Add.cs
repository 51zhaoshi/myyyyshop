namespace Maticsoft.Web.Ms.Enterprise
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.Ms.Enterprise bll = new Maticsoft.BLL.Ms.Enterprise();
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
                        MessageBox.ShowServerBusyTip(this, "企业名称不能为空！");
                    }
                    else if (name.Length > 100)
                    {
                        MessageBox.ShowServerBusyTip(this, "企业名称请控制在1~100字符！");
                    }
                    else if (this.bll.Exists(name))
                    {
                        MessageBox.ShowServerBusyTip(this, "该企业名称已经被注册，请更换企业名称再操作！");
                    }
                    else
                    {
                        int enterpriseID = 0;
                        try
                        {
                            Maticsoft.Model.Ms.Enterprise model = new Maticsoft.Model.Ms.Enterprise {
                                Name = name,
                                Introduction = this.txtIntroduction.Text,
                                RegisteredCapital = new int?(Globals.SafeInt(this.txtRegisteredCapital.Text, 0)),
                                TelPhone = this.txtTelPhone.Text,
                                CellPhone = this.txtCellPhone.Text,
                                ContactMail = this.txtContactMail.Text,
                                RegionID = new int?(this.RegionID.Region_iID),
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
                            model.EnteRank = new int?(Globals.SafeInt(this.dropEnteRank.SelectedValue, 0));
                            model.EnteClassID = new int?(Globals.SafeInt(this.dropEnteClassID.SelectedValue, 0));
                            model.CompanyType = new int?(Globals.SafeInt(this.dropCompanyType.SelectedValue, 0));
                            model.BusinessLicense = this.txtBusinessLicense.Text;
                            model.TaxNumber = this.txtTaxNumber.Text;
                            model.AccountBank = this.txtAccountBank.Text;
                            model.AccountInfo = this.txtAccountInfo.Text;
                            model.ServicePhone = this.txtServicePhone.Text;
                            model.QQ = this.txtQQ.Text;
                            model.MSN = this.txtMSN.Text;
                            model.Status = new int?(Globals.SafeInt(this.radlStatus.SelectedValue, 0));
                            model.CreatedDate = new DateTime?(DateTime.Now);
                            model.CreatedUserID = new int?(base.CurrentUser.UserID);
                            model.UpdatedDate = new DateTime?(DateTime.Now);
                            model.UpdatedUserID = new int?(base.CurrentUser.UserID);
                            model.Balance = Globals.SafeDecimal(this.txtBalance.Text, (decimal) 0M);
                            model.AgentID = Globals.SafeInt(this.txtAgentID.Text, 0);
                            enterpriseID = this.bll.Add(model);
                            if (enterpriseID > 0)
                            {
                                user.UserName = userName;
                                user.NickName = this.txtName.Text;
                                user.Password = AccountsPrincipal.EncryptPassword(password);
                                user.TrueName = "";
                                user.Sex = "1";
                                user.Phone = this.txtCellPhone.Text;
                                user.Email = this.txtContactMail.Text;
                                user.EmployeeID = 0;
                                user.DepartmentID = enterpriseID.ToString();
                                user.Activity = true;
                                user.UserType = "EE";
                                user.Style = 1;
                                user.User_dateCreate = DateTime.Now;
                                user.User_iCreator = base.CurrentUser.UserID;
                                user.User_dateValid = DateTime.Now;
                                user.User_cLang = "zh-CN";
                                int userID = user.Create();
                                if (userID == -100)
                                {
                                    this.bll.Delete(enterpriseID);
                                    MessageBox.ShowServerBusyTip(this, Site.TooltipUserExist);
                                }
                                else
                                {
                                    UsersExp exp = new UsersExp {
                                        UserID = userID,
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
                                        user.UserID = userID;
                                        user.Delete();
                                        exp.DeleteUsersExp(userID);
                                        this.bll.Delete(enterpriseID);
                                        MessageBox.ShowFailTip(this, "添加失败！");
                                    }
                                }
                            }
                            else
                            {
                                this.bll.Delete(enterpriseID);
                                MessageBox.ShowFailTip(this, "添加失败！");
                            }
                        }
                        catch (Exception exception)
                        {
                            this.bll.Delete(enterpriseID);
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
                return 0x13d;
            }
        }
    }
}

