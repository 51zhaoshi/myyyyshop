namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.ViewModel.CMS;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Web.UI;

    public class AccountController : CMSControllerBase
    {
        private User userBusManage = new User();
        private UsersExp userExpManage = new UsersExp();
        private Users userManage = new Users();

        [HttpPost]
        public ActionResult AjaxLogin(string UserName, string UserPwd)
        {
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

        public ActionResult CheckUserState()
        {
            if (base.currentUser != null)
            {
                return base.Content("Yes");
            }
            return base.Content("No");
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

        public ActionResult GetCurrentUser()
        {
            if (base.currentUser == null)
            {
                return base.Content("No");
            }
            string content = string.IsNullOrWhiteSpace(base.currentUser.NickName) ? base.currentUser.UserName : base.currentUser.NickName;
            return base.Content(content);
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
            if (base.CurrentUser != null)
            {
                return base.RedirectToAction("Index", "Home", new { area = "CMS" });
            }
            ConfigSystem.GetBoolValueByCache("System_Close_Login");
            ((dynamic) base.ViewBag).Title = "登录";
            return base.View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            ((dynamic) base.ViewBag).Title = "登录";
            if (!base.ModelState.IsValid)
            {
                return base.View(model);
            }
            AccountsPrincipal existingPrincipal = AccountsPrincipal.ValidateLogin(model.Email, model.Password);
            if (existingPrincipal == null)
            {
                base.ModelState.AddModelError("Message", "用户名或密码不正确, 请重新输入!");
                return base.View(model);
            }
            User user = new User(existingPrincipal);
            if (!user.Activity)
            {
                base.ModelState.AddModelError("Message", "对不起，该帐号已被冻结，请联系管理员！");
                return base.View(model);
            }
            base.HttpContext.User = existingPrincipal;
            FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
            base.Session[Globals.SESSIONKEY_USER] = user;
            int num = new PointsDetail().AddPoints("Login", user.UserID, "登录操作", "");
            if ((base.Session["ReturnUrl"] != null) && !string.IsNullOrWhiteSpace(base.Session["ReturnUrl"].ToString()))
            {
                returnUrl = base.Session["ReturnUrl"].ToString();
                base.Session.Remove("ReturnUrl");
                return this.Redirect(returnUrl);
            }
            if (((base.Url.IsLocalUrl(returnUrl) && (returnUrl.Length > 1)) && (returnUrl.StartsWith("/") && !returnUrl.StartsWith("//"))) && !returnUrl.StartsWith(@"/\"))
            {
                return this.Redirect(returnUrl);
            }
            base.TempData["pointer"] = num;
            return base.RedirectToAction("Index", "Home", new { area = "CMS" });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            base.Session.Remove(Globals.SESSIONKEY_USER);
            base.Session.Clear();
            base.Session.Abandon();
            return base.RedirectToAction("Index", "Home", new { area = "CMS" });
        }

        public ActionResult Register()
        {
            if (base.CurrentUser != null)
            {
                base.RedirectToAction("Index", "Home", new { area = "CMS" });
            }
            ((dynamic) base.ViewBag).Title = "注册";
            return base.View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            ((dynamic) base.ViewBag).Title = "注册";
            if (base.ModelState.IsValid)
            {
                int num = new User { UserName = model.Email, NickName = model.NickName, Password = AccountsPrincipal.EncryptPassword(model.Password), Email = model.Email, Activity = true, UserType = "UU", Style = 1, User_dateCreate = DateTime.Now, User_cLang = "zh-CN" }.Create();
                if (num != -100)
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
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    new PointsDetail().AddPoints("Register", num, "注册成功", "");
                    string valueByCache = ConfigSystem.GetValueByCache("DefaultGravatar");
                    valueByCache = string.IsNullOrEmpty(valueByCache) ? "/Upload/User/Gravatar/Default.jpg" : valueByCache;
                    string str2 = ConfigSystem.GetValueByCache("TargetGravatarFile");
                    str2 = string.IsNullOrEmpty(str2) ? "/Upload/User/Gravatar/" : str2;
                    string str3 = base.ControllerContext.HttpContext.Server.MapPath("/");
                    if (File.Exists(str3 + valueByCache))
                    {
                        File.Copy(str3 + valueByCache, string.Concat(new object[] { str3, str2, num, ".jpg" }), true);
                    }
                    return base.RedirectToAction("Index", "Home", new { area = "CMS" });
                }
                base.ModelState.AddModelError("Message", ErrorCodeToString(MembershipCreateStatus.DuplicateUserName));
            }
            return base.View(model);
        }

        public ActionResult ToBind()
        {
            string str = base.Request["pName"];
            if (string.IsNullOrWhiteSpace(str))
            {
                goto Label_041C;
            }
            string url = (string) (((dynamic) base.ViewBag).BasePath + "social/qq");
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "QZone"))
                {
                    if (str3 == "Sina")
                    {
                        url = (string) (((dynamic) base.ViewBag).BasePath + "social/sina");
                        goto Label_040C;
                    }
                }
                else
                {
                    url = (string) (((dynamic) base.ViewBag).BasePath + "social/qq");
                    goto Label_040C;
                }
            }
            url = (string) (((dynamic) base.ViewBag).BasePath + "social/sina");
        Label_040C:
            HttpContext.Current.Response.Redirect(url);
        Label_041C:
            return base.RedirectToAction("UserBind", "UserCenter");
        }
    }
}

