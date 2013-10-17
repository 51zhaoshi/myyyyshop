namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.Shop;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Web.UI;

    public class AccountController : MobileControllerBase
    {
        private User userBusManage = new User();
        private UsersExp userExpManage = new UsersExp();
        private Users userManage = new Users();

        [HttpPost]
        public ActionResult AjaxIsLogin()
        {
            if (base.currentUser == null)
            {
                return base.Content("False");
            }
            if (base.currentUser.UserType == "AA")
            {
                return base.Content("AA");
            }
            return base.Content("True");
        }

        [HttpPost]
        public ActionResult AjaxLogin(string UserName, string UserPwd)
        {
            if (ConfigSystem.GetBoolValueByCache("System_Close_Login"))
            {
                return base.Content("-1");
            }
            if (!base.ModelState.IsValid)
            {
                return base.Content("0");
            }
            AccountsPrincipal existingPrincipal = AccountsPrincipal.ValidateLogin(UserName, UserPwd);
            if (existingPrincipal == null)
            {
                return base.Content("0");
            }
            User user = new User(existingPrincipal);
            if (!user.Activity)
            {
                base.ModelState.AddModelError("Message", "对不起，该帐号已被冻结，请联系管理员！");
            }
            base.HttpContext.User = existingPrincipal;
            FormsAuthentication.SetAuthCookie(UserName, true);
            base.Session[Globals.SESSIONKEY_USER] = user;
            int num = new PointsDetail().AddPoints("Login", user.UserID, "登录操作", "");
            return base.Content("1|" + num.ToString());
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidPassword:
                    return "提供的密码无效。请输入有效的密码值。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "提供的密码取回答案无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidEmail:
                    return "提供的电子邮件地址无效。请检查该值并重试。";

                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在。请输入不同的用户名。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "该电子邮件地址的用户名已存在。请输入不同的电子邮件地址。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
            return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
        }

        [HttpPost]
        public void HasEmail(FormCollection collection)
        {
            User user = new User();
            if (!string.IsNullOrWhiteSpace(collection["Email"]))
            {
                base.Response.ContentType = "application/text";
                if (user.HasUserByEmail(collection["Email"].Trim()))
                {
                    base.Response.Write("true");
                }
                else
                {
                    base.Response.Write("false");
                }
            }
        }

        public ActionResult Index()
        {
            return base.View();
        }

        [OutputCache(Location=OutputCacheLocation.None, NoStore=true)]
        public JsonResult IsExistEmail(string email)
        {
            bool data = !this.userBusManage.HasUserByEmail(email);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location=OutputCacheLocation.None, NoStore=true)]
        public JsonResult IsExistNickName(string nickName)
        {
            bool data = !this.userBusManage.HasUserByNickName(nickName);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location=OutputCacheLocation.None, NoStore=true)]
        public JsonResult IsExistUserName(string userName)
        {
            bool data = !this.userBusManage.HasUserByUserName(userName);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            if (ConfigSystem.GetBoolValueByCache("System_Close_Login"))
            {
                return this.Redirect("/m/Error/TurnOff");
            }
            string str = base.Request.QueryString["returnUrl"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                ((dynamic) base.ViewBag).returnUrl = str;
            }
            if ((base.HttpContext.User.Identity.IsAuthenticated && (base.CurrentUser != null)) && (base.CurrentUser.UserType != "AA"))
            {
                return this.Redirect("/m/u");
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "登录" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ConfigSystem.GetBoolValueByCache("System_Close_Login"))
            {
                return this.Redirect("/m/Error/TurnOff");
            }
            if (!base.ModelState.IsValid)
            {
                return base.View(model);
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "登录" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            AccountsPrincipal existingPrincipal = AccountsPrincipal.ValidateLogin(model.UserName, model.Password);
            if (existingPrincipal == null)
            {
                base.ModelState.AddModelError("Message", "用户名或密码不正确, 请重新输入!");
                return base.View(model);
            }
            User user = new User(existingPrincipal);
            if (!user.Activity)
            {
                base.ModelState.AddModelError("Message", "对不起，该帐号已被冻结或未激活，请联系管理员！");
                return base.View(model);
            }
            base.HttpContext.User = existingPrincipal;
            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            base.Session[Globals.SESSIONKEY_USER] = user;
            new PointsDetail().AddPoints("Login", user.UserID, "登录操作", "");
            if (base.CurrentThemeName == "M1")
            {
                ShoppingCartHelper.LoadShoppingCart(user.UserID);
            }
            returnUrl = base.Server.UrlDecode(returnUrl);
            if (((base.Url.IsLocalUrl(returnUrl) && (returnUrl.Length > 1)) && (returnUrl.StartsWith("/") && !returnUrl.StartsWith("//"))) && !returnUrl.StartsWith(@"/\"))
            {
                return this.Redirect(returnUrl);
            }
            return this.Redirect("/m/u");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            base.Session.Remove(Globals.SESSIONKEY_USER);
            base.Session.Clear();
            base.Session.Abandon();
            return this.Redirect("/m");
        }

        public ActionResult Register()
        {
            if (ConfigSystem.GetBoolValueByCache("System_Close_Register"))
            {
                return this.Redirect("/m/Error/TurnOff");
            }
            if ((base.CurrentUser != null) && (base.CurrentUser.UserType != "AA"))
            {
                return this.Redirect("/m/u");
            }
            RegisterModel model = new RegisterModel();
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "注册" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "注册" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (ConfigSystem.GetBoolValueByCache("System_Close_Register"))
            {
                return this.Redirect("/m/Error/TurnOff");
            }
            if (base.ModelState.IsValid)
            {
                if (this.userBusManage.HasUserByNickName(model.NickName))
                {
                    ((dynamic) base.ViewBag).hasnickname = "昵称已被抢先使用，换一个试试";
                    return base.View(model);
                }
                if (this.userBusManage.HasUserByEmail(model.Email))
                {
                    ((dynamic) base.ViewBag).hasemail = "该邮箱已被注册";
                    return base.View(model);
                }
                int num = new User { UserName = model.Email, NickName = model.NickName, Password = AccountsPrincipal.EncryptPassword(model.Password), Email = model.Email, Activity = true, UserType = "UU", Style = 1, User_dateCreate = DateTime.Now, User_cLang = "zh-CN" }.Create();
                if (num == -100)
                {
                    base.ModelState.AddModelError("Message", ErrorCodeToString(MembershipCreateStatus.DuplicateUserName));
                }
                else
                {
                    UsersExp exp = new UsersExp {
                        UserID = num,
                        BirthdayVisible = 0,
                        BirthdayIndexVisible = false,
                        Gravatar = string.Format("/{0}/User/Gravatar/{1}", MvcApplication.UploadFolder, num),
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
                    if (!exp.AddUsersExp(exp))
                    {
                        this.userManage.Delete(num);
                        this.userExpManage.DeleteUsersExp(num);
                        base.ModelState.AddModelError("Message", "注册失败！");
                        return base.View(model);
                    }
                    string valueByCache = ConfigSystem.GetValueByCache("DefaultGravatar");
                    valueByCache = string.IsNullOrEmpty(valueByCache) ? "/Upload/User/Gravatar/Default.jpg" : valueByCache;
                    string str2 = ConfigSystem.GetValueByCache("TargetGravatarFile");
                    str2 = string.IsNullOrEmpty(str2) ? "/Upload/User/Gravatar/" : str2;
                    string str3 = base.ControllerContext.HttpContext.Server.MapPath("/");
                    if (File.Exists(str3 + valueByCache))
                    {
                        File.Copy(str3 + valueByCache, string.Concat(new object[] { str3, str2, num, ".jpg" }), true);
                    }
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    new PointsDetail().AddPoints("Register", num, "注册成功", "");
                    return this.Redirect("/m/u/Personal");
                }
            }
            return base.View(model);
        }

        public ActionResult UserAgreement()
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.System);
            RegisterModel model = new RegisterModel {
                UserAgreement = set.RegistStatement
            };
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "注册协议" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(model);
        }
    }
}

