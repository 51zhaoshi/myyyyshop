namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.DEncrypt;
    using Maticsoft.Components;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.OAuth;
    using Maticsoft.OAuth.Http.Converters.Json;
    using Maticsoft.OAuth.Json;
    using Maticsoft.OAuth.Rest.Client;
    using Maticsoft.OAuth.v2;
    using Maticsoft.ViewModel.Shop;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Components.Setting.Shop;
    using Maticsoft.Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Web.UI;

    public class AccountController : Maticsoft.Web.Controllers.ControllerBase
    {
        private readonly string _redirectUrl = ("http://" + Globals.DomainFullName + "/Shop/Account/Callback");
        private readonly IOAuth2ServiceProvider<ITaoLe> _taoLeProvider = new TaoLeServiceProvider("2ffe56f6a33275cc5c264b1f0c1683", "9d4fd6e1090ce15f6668f63d132ea3");
        private const string OAuth2ApiId = "2ffe56f6a33275cc5c264b1f0c1683";
        private const string OAuth2ApiSecret = "9d4fd6e1090ce15f6668f63d132ea3";
        private const string SESSION_KEY_ACCESSGRANT = "AccessGrant";
        private const string SESSION_KEY_OAUTH2TRY = "OAuth2CallbackTry";
        private User userBusManage = new User();
        private UsersExp userExpManage = new UsersExp();
        private Maticsoft.BLL.Members.Users userManage = new Maticsoft.BLL.Members.Users();

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
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login"))
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
            int num = new Maticsoft.BLL.Members.PointsDetail().AddPoints("Login", user.UserID, "登录操作", "");
            return base.Content("1|" + num.ToString());
        }

        public ActionResult Callback(string code)
        {
            AccessGrant result;
            JsonValue value2;
            if (string.IsNullOrWhiteSpace(code))
            {
                return this.Redirect("/");
            }
            ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
            ServicePointManager.Expect100Continue = false;
            try
            {
                result = this._taoLeProvider.OAuthOperations.ExchangeForAccessAsync(code, this._redirectUrl, null).Result;
            }
            catch (AggregateException exception)
            {
                if (base.Session["OAuth2CallbackTry"] == null)
                {
                    base.Session["OAuth2CallbackTry"] = 3;
                }
                int num = Globals.SafeInt(base.Session["OAuth2CallbackTry"].ToString(), -1);
                HttpResponseException innerException = exception.InnerExceptions[0].InnerException as HttpResponseException;
                if (innerException != null)
                {
                    LogHelp.AddErrorLog(innerException.GetResponseBodyAsString(), innerException.StackTrace, "OAuth2 Callback HttpResponseException Try:" + num);
                }
                if (num > 0)
                {
                    base.Session["OAuth2CallbackTry"] = --num;
                    return base.RedirectToAction("SignIn", "Account", new { area = "Shop", viewname = "SignIn" });
                }
                base.Session.Remove("OAuth2CallbackTry");
                return this.Redirect("/");
            }
            ITaoLe api = this._taoLeProvider.GetApi(result);
            try
            {
                value2 = api.GetUserProfileAsync().Result;
            }
            catch (Exception exception3)
            {
                LogHelp.AddErrorLog(exception3.Message, exception3.StackTrace, "OAuth2 GetUserProfileAsync Exception");
                return this.Redirect("/");
            }
            return this.CallbackUserInfo(value2, result.RefreshToken);
        }

        private ActionResult CallbackUserInfo(JsonValue userInfoJson, string token)
        {
            if (userInfoJson == null)
            {
                string loginfo = "CallbackUserInfo: userInfoJson IS NULL";
                LogHelp.AddErrorLog(loginfo, loginfo, "OAuth2 CallbackUserInfo");
                return this.Redirect("/");
            }
            Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
            string nickName = userInfoJson.GetValue<string>("nickname");
            string str3 = userInfoJson.GetValue<string>("username");
            int num = userInfoJson.GetValue<int>("id");
            string userName = string.Format("Maticsoft_{0}", num);
            User user = new User();
            if (user.HasUserByUserName(userName))
            {
                User user2 = new User(AccountsPrincipal.ValidateLogin(userName, userName));
                FormsAuthentication.SetAuthCookie(userName, false);
                base.Session[Globals.SESSIONKEY_USER] = user2;
                base.Session["Style"] = user2.Style;
                new Maticsoft.BLL.Members.PointsDetail().AddPoints("Login", user2.UserID, "登录操作", "");
                if (base.Session["returnPage"] != null)
                {
                    string url = base.Session["returnPage"].ToString();
                    base.Session["returnPage"] = null;
                    return this.Redirect(url);
                }
                return base.RedirectToAction("Index", "UserCenter", new { area = "Shop" });
            }
            User user3 = new User();
            if (user3.HasUserByNickName(nickName))
            {
                nickName = nickName + "_" + num;
            }
            user.UserName = userName;
            user.Email = str3;
            user.Password = AccountsPrincipal.EncryptPassword(userName + "9d4fd6e1090ce15f6668f63d132ea3");
            user.Activity = true;
            user.UserType = "UU";
            user.NickName = nickName;
            user.Style = 1;
            user.User_dateCreate = DateTime.Now;
            user.User_cLang = "zh-CN";
            int num2 = user.Create();
            if (num2 <= 0)
            {
                return this.Redirect("/");
            }
            UsersExp model = new UsersExp {
                UserID = num2,
                Email = str3,
                Gravatar = string.Format("/{0}/User/Gravatar/{1}", Maticsoft.Components.MvcApplication.UploadFolder, num2),
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
            if (!model.AddUsersExp(model))
            {
                users.Delete(num2);
                new UsersExp().DeleteUsersExp(num2);
                return this.Redirect("/");
            }
            User user4 = new User(AccountsPrincipal.ValidateLogin(userName, userName));
            FormsAuthentication.SetAuthCookie(userName, false);
            base.Session[Globals.SESSIONKEY_USER] = user4;
            base.Session["Style"] = user4.Style;
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_Register_IsBind") == "1")
            {
                Maticsoft.BLL.Members.UserBind bind = new Maticsoft.BLL.Members.UserBind();
                Maticsoft.Model.Members.UserBind bind2 = new Maticsoft.Model.Members.UserBind {
                    Comment = false,
                    iHome = false,
                    MediaID = -1,
                    MediaNickName = nickName,
                    TokenAccess = token,
                    UserId = num2,
                    GroupTopic = false
                };
                if (bind.Add(bind2) == 0)
                {
                    return this.Redirect("/");
                }
            }
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
            valueByCache = string.IsNullOrEmpty(valueByCache) ? "/Upload/User/Gravatar/Default.jpg" : valueByCache;
            string str8 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
            str8 = string.IsNullOrEmpty(str8) ? "/Upload/User/Gravatar/" : str8;
            string str9 = base.ControllerContext.HttpContext.Server.MapPath("/");
            if (System.IO.File.Exists(str9 + valueByCache))
            {
                System.IO.File.Copy(str9 + valueByCache, string.Concat(new object[] { str9, str8, user4.UserID, ".jpg" }), true);
            }
            new Maticsoft.BLL.Members.PointsDetail().AddPoints("Register", num2, "注册成功", "");
            Maticsoft.Model.SNS.UserAlbums albums = new Maticsoft.Model.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbums albums2 = new Maticsoft.BLL.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            albums.AlbumName = "默认专辑";
            albums.CreatedDate = DateTime.Now;
            albums.CreatedNickName = user4.NickName;
            albums.CreatedUserID = user4.UserID;
            albums2.AddEx(albums, 1);
            ship.GiveUserFellow(user4.UserID);
            return base.RedirectToAction("Index", "UserCenter", new { area = "Shop" });
        }

        protected string EmailSuffix(string sUserID)
        {
            int userID = Globals.SafeInt(Globals.UrlDecode(sUserID), 0);
            Maticsoft.Model.Members.Users model = new Maticsoft.BLL.Members.Users().GetModel(userID);
            if (model != null)
            {
                return model.Email;
            }
            return "";
        }

        public string EmailUrl(string email)
        {
            string str2 = email.Substring(email.LastIndexOf('@') + 1);
            if (str2.Contains("gmail"))
            {
                str2 = "google.com";
            }
            return ("http://mail." + str2);
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

        public ActionResult FindPwd()
        {
            if ((base.CurrentUser != null) && (base.CurrentUser.UserType != "AA"))
            {
                return base.RedirectToAction("Index", "UserCenter");
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "找回密码" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        [HttpPost]
        public ActionResult FindPwd(FormCollection collection)
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "找回密码" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            string userName = collection["Email"].Trim();
            base.ViewData["Email"] = userName;
            if ((base.Session["CheckCode"] != null) && (base.Session["CheckCode"].ToString() != ""))
            {
                if (base.Session["CheckCode"].ToString().ToLower() != collection["CheckCode"].Trim().ToLower())
                {
                    base.ModelState.AddModelError("Error", "验证码错误！");
                    base.Session["CheckCode"] = null;
                    return base.View(base.ViewData["Email"]);
                }
                base.Session["CheckCode"] = null;
                User user = new User(userName);
                if (string.IsNullOrWhiteSpace(user.NickName))
                {
                    base.ModelState.AddModelError("Error", "该邮箱用户不存在！");
                    return base.View(base.ViewData["Email"]);
                }
                try
                {
                    this.SendEmail(user.UserName, userName, 1);
                    return base.RedirectToAction("FindPwdEmail", "Account", new { email = base.ViewData["Email"] });
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("Error", "邮件发送过程中出现网络异常，请稍后再试！");
                }
            }
            return base.View(base.ViewData["Email"]);
        }

        public ActionResult FindPwdEmail(string email)
        {
            ((dynamic) base.ViewBag).Email = email;
            ((dynamic) base.ViewBag).EmailUrl = this.EmailUrl(email);
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "找回密码邮箱验证" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
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

        public ActionResult Index(FormCollection form)
        {
            return base.RedirectToAction("SignIn", "Account", new { area = "Shop", viewname = "SignIn" });
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
        public JsonResult IsExistPhone(string phone)
        {
            bool data = !this.userBusManage.HasUserByPhone(phone);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location=OutputCacheLocation.None, NoStore=true)]
        public JsonResult IsExistUserName(string userName)
        {
            bool data = !this.userBusManage.HasUserByUserName(userName);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(string returnUrl)
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).WebName = set.WebName;
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login"))
            {
                return base.RedirectToAction("TurnOff", "Error");
            }
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                ((dynamic) base.ViewBag).returnUrl = returnUrl;
            }
            if ((base.HttpContext.User.Identity.IsAuthenticated && (base.CurrentUser != null)) && (base.CurrentUser.UserType != "AA"))
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                return base.RedirectToAction("Index", "UserCenter");
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
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login"))
            {
                return base.RedirectToAction("TurnOff", "Error");
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
            new Maticsoft.BLL.Members.PointsDetail().AddPoints("Login", user.UserID, "登录操作", "");
            if (base.CurrentThemeName == "M1")
            {
                ShoppingCartHelper.LoadShoppingCart(user.UserID);
            }
            returnUrl = base.Server.UrlDecode(returnUrl);
            if (((base.Url.IsLocalUrl(returnUrl) && (returnUrl.Length > 1)) && (returnUrl.StartsWith("/") && !returnUrl.StartsWith("//"))) && !returnUrl.StartsWith(@"/\"))
            {
                return this.Redirect(returnUrl);
            }
            return base.RedirectToAction("Index", "UserCenter");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            base.Session.Remove(Globals.SESSIONKEY_USER);
            base.Session.Clear();
            base.Session.Abandon();
            return base.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "注册" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            int inviteuid = -1;
            if (!string.IsNullOrWhiteSpace(base.Request.Form["inviteid"]))
            {
                inviteuid = Globals.SafeInt(Hex16.Decode(base.Request.Form["inviteid"]), -1);
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register"))
            {
                return base.RedirectToAction("TurnOff", "Error");
            }
            if (!base.ModelState.IsValid)
            {
                goto Label_066D;
            }
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
            bool boolValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_RegisterEmailCheck");
            User user = new User {
                UserName = model.Email,
                NickName = model.NickName,
                Password = AccountsPrincipal.EncryptPassword(model.Password),
                Phone = model.Phone,
                Email = model.Email
            };
            if (boolValueByCache)
            {
                user.Activity = true;
            }
            else
            {
                user.Activity = false;
            }
            user.UserType = "UU";
            user.Style = 1;
            user.User_dateCreate = DateTime.Now;
            user.User_cLang = "zh-CN";
            int num2 = user.Create();
            if (num2 == -100)
            {
                base.ModelState.AddModelError("Message", ErrorCodeToString(MembershipCreateStatus.DuplicateUserName));
                goto Label_066D;
            }
            UsersExp exp = new UsersExp {
                UserID = num2,
                BirthdayVisible = 0,
                BirthdayIndexVisible = false,
                Gravatar = string.Format("/{0}/User/Gravatar/{1}", Maticsoft.Components.MvcApplication.UploadFolder, num2),
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
                LastPostTime = new DateTime?(DateTime.Now),
                NickName = model.NickName
            };
            if (!exp.AddExp(exp, inviteuid))
            {
                this.userManage.Delete(num2);
                this.userExpManage.DeleteUsersExp(num2);
                base.ModelState.AddModelError("Message", "注册失败！");
                return base.View(model);
            }
            base.Session["SMSCode"] = null;
            base.Session["SMS_DATE"] = DateTime.MinValue;
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
            valueByCache = string.IsNullOrEmpty(valueByCache) ? "/Upload/User/Gravatar/Default.jpg" : valueByCache;
            string str3 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
            str3 = string.IsNullOrEmpty(str3) ? "/Upload/User/Gravatar/" : str3;
            string str4 = base.ControllerContext.HttpContext.Server.MapPath("/");
            if (System.IO.File.Exists(str4 + valueByCache))
            {
                System.IO.File.Copy(str4 + valueByCache, string.Concat(new object[] { str4, str3, num2, ".jpg" }), true);
            }
            if (!boolValueByCache)
            {
                try
                {
                    this.SendEmail(model.Email, model.Email, 0);
                    return base.RedirectToAction("RegisterSuccess", "Account", new { email = model.Email });
                }
                catch (Exception exception)
                {
                    Maticsoft.Model.SysManage.ErrorLog log = new Maticsoft.Model.SysManage.ErrorLog {
                        Loginfo = exception.Message,
                        StackTrace = exception.StackTrace,
                        Url = base.Request.Url.AbsoluteUri
                    };
                    Maticsoft.BLL.SysManage.ErrorLog.Add(log);
                    base.ModelState.AddModelError("", "邮件发送过程中出现网络异常，请稍后再试！");
                    goto Label_0661;
                }
            }
            FormsAuthentication.SetAuthCookie(model.Email, false);
            new Maticsoft.BLL.Members.PointsDetail().AddPoints("Register", num2, "注册成功", "");
        Label_0661:
            return this.Redirect("/UserCenter/Personal");
        Label_066D:
            return base.View(model);
        }

        public ActionResult Register(string id)
        {
            ((dynamic) base.ViewBag).InviteID = id;
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).WebName = set.WebName;
            bool boolValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
            ((dynamic) base.ViewBag).SMSIsOpen = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");
            if (boolValueByCache)
            {
                return base.RedirectToAction("TurnOff", "Error");
            }
            if ((base.CurrentUser != null) && (base.CurrentUser.UserType != "AA"))
            {
                return base.RedirectToAction("Index", "UserCenter");
            }
            WebSiteSet set2 = new WebSiteSet(ApplicationKeyType.System);
            RegisterModel model = new RegisterModel {
                UserAgreement = set2.RegistStatement
            };
            ((dynamic) base.ViewBag).Seconds = 0;
            if ((base.Session["SMS_DATE"] != null) && !string.IsNullOrWhiteSpace(base.Session["SMS_DATE"].ToString()))
            {
                DateTime time = Globals.SafeDateTime(base.Session["SMS_DATE"].ToString(), DateTime.MinValue);
                if (time != DateTime.MinValue)
                {
                    TimeSpan span = (TimeSpan) (time.AddSeconds(60.0) - DateTime.Now);
                    ((dynamic) base.ViewBag).Seconds = (int) span.TotalSeconds;
                }
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "注册" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(model);
        }

        public ActionResult RegisterSuccess(string email)
        {
            ((dynamic) base.ViewBag).Email = email;
            ((dynamic) base.ViewBag).EmailUrl = this.EmailUrl(email);
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "注册成功" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        [HttpPost]
        public void SendEmail(FormCollection collection)
        {
            if (!string.IsNullOrWhiteSpace(collection["Email"]))
            {
                User user = new User(collection["Email"]);
                int type = -1;
                if (!string.IsNullOrWhiteSpace(collection["Type"]) && PageValidate.IsNumber(collection["Type"]))
                {
                    type = Globals.SafeInt(collection["Type"], -1);
                }
                if (!string.IsNullOrWhiteSpace(user.NickName))
                {
                    this.SendEmail(user.UserName, user.Email, type);
                    base.Response.ContentType = "application/text";
                    base.Response.Write("success");
                }
            }
        }

        protected bool SendEmail(string username, string email, int type)
        {
            EmailTemplet templet = new EmailTemplet();
            switch (type)
            {
                case 0:
                    return templet.SendRegisterEmail(username, email);

                case 1:
                    return templet.SendFindPwdEmail(username, email);
            }
            return false;
        }

        public ActionResult SendSMS(FormCollection Fm)
        {
            string str = Fm["Phone"];
            if (string.IsNullOrWhiteSpace(str))
            {
                return base.Content("False");
            }
            int num = new Random().Next(0x186a0, 0xf423f);
            string content = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Content").Replace("{SMSCode}", num.ToString());
            base.Session["SMSCode"] = num;
            base.Session["SMS_DATE"] = DateTime.Now;
            string[] numbers = new string[] { str };
            if (!SMSHelper.SendSMS(content, numbers, 5))
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

        public ActionResult SignIn()
        {
            OAuth2Parameters parameters2 = new OAuth2Parameters {
                RedirectUrl = this._redirectUrl
            };
            OAuth2Parameters parameters = parameters2;
            return this.Redirect(this._taoLeProvider.OAuthOperations.BuildAuthorizeUrl(GrantType.AuthorizationCode, parameters));
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

        public ActionResult ValidateEmail()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "邮箱验证成功" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            string keyValue = base.Request.QueryString["SecretKey"];
            Maticsoft.BLL.SysManage.VerifyMail mail = new Maticsoft.BLL.SysManage.VerifyMail();
            Maticsoft.Model.SysManage.VerifyMail model = mail.GetModel(keyValue);
            if (((!string.IsNullOrEmpty(keyValue) && mail.Exists(keyValue)) && ((model != null) && model.ValidityType.HasValue)) && (model.ValidityType.Value == 0))
            {
                switch (model.Status)
                {
                    case 0:
                    {
                        TimeSpan span = (TimeSpan) (DateTime.Now - model.CreatedDate);
                        if (span.TotalHours > 24.0)
                        {
                            model.Status = 2;
                            mail.Update(model);
                            ((dynamic) base.ViewBag).Msg = "注册验证已过期！";
                        }
                        User user = new User(model.UserName);
                        if (user != null)
                        {
                            user.UpdateActivity(user.UserID, true);
                            ((dynamic) base.ViewBag).Email = user.Email;
                        }
                        model.Status = 1;
                        mail.Update(model);
                        ((dynamic) base.ViewBag).Msg = "Success";
                        ((dynamic) base.ViewBag).email = model.UserName;
                        goto Label_058A;
                    }
                    case 1:
                        model.Status = 2;
                        mail.Update(model);
                        ((dynamic) base.ViewBag).Msg = "注册验证已通过！";
                        goto Label_058A;

                    case 2:
                        ((dynamic) base.ViewBag).Msg = "注册验证已过期！";
                        goto Label_058A;
                }
                ((dynamic) base.ViewBag).Msg = "无效的邮箱验证码！";
            }
            else
            {
                ((dynamic) base.ViewBag).Msg = "无效的邮箱验证码！";
            }
        Label_058A:
            return base.View();
        }

        public ActionResult VerifiyCode(FormCollection Fm)
        {
            if ((base.Session["SMSCode"] == null) || string.IsNullOrWhiteSpace(base.Session["SMSCode"].ToString()))
            {
                return base.Content("False");
            }
            string str = Fm["SMSCode"];
            if (!(str == base.Session["SMSCode"].ToString()))
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

        public ActionResult VerifyPassword()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "找回密码" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            string str = base.Request.QueryString["SecretKey"];
            if (!string.IsNullOrEmpty(str))
            {
                Maticsoft.BLL.SysManage.VerifyMail mail = new Maticsoft.BLL.SysManage.VerifyMail();
                if (mail.Exists(str))
                {
                    Maticsoft.Model.SysManage.VerifyMail model = mail.GetModel(str);
                    if (((model != null) && model.ValidityType.HasValue) && (model.ValidityType.Value == 1))
                    {
                        if (model.Status == 0)
                        {
                            TimeSpan span = (TimeSpan) (DateTime.Now - model.CreatedDate);
                            if (span.TotalHours > 24.0)
                            {
                                model.Status = 2;
                                mail.Update(model);
                                ((dynamic) base.ViewBag).Msg = "找回密码的验证码已过期！";
                                base.ModelState.AddModelError("Error", "找回密码的验证码已过期！");
                            }
                            User user = new User(model.UserName);
                            if (user != null)
                            {
                                ((dynamic) base.ViewBag).Email = user.Email;
                            }
                            model.Status = 1;
                            mail.Update(model);
                            ((dynamic) base.ViewBag).Msg = "Success";
                        }
                        else if (model.Status == 1)
                        {
                            model.Status = 2;
                            mail.Update(model);
                            ((dynamic) base.ViewBag).Msg = "找回密码的验证码已通过邮箱验证！";
                            base.ModelState.AddModelError("Error", "找回密码的验证码已通过邮箱验证！");
                        }
                        else if (model.Status == 2)
                        {
                            ((dynamic) base.ViewBag).Msg = "找回密码的验证码已过期！";
                            base.ModelState.AddModelError("Error", "找回密码的验证码已过期！");
                        }
                        else
                        {
                            ((dynamic) base.ViewBag).Msg = "无效的邮箱验证码！";
                            base.ModelState.AddModelError("Error", "无效的邮箱验证码！");
                        }
                    }
                }
            }
            return base.View();
        }

        [HttpPost]
        public ActionResult VerifyPassword(FormCollection collection)
        {
            if (string.IsNullOrWhiteSpace(collection["Email"]) || string.IsNullOrWhiteSpace(collection["NewPwd"]))
            {
                return base.View();
            }
            string userName = collection["Email"].Trim();
            string str2 = collection["NewPwd"];
            User user = new User(userName);
            if (string.IsNullOrWhiteSpace(str2))
            {
                base.ModelState.AddModelError("Error", "该用户不存在！");
                return base.View();
            }
            user.Password = AccountsPrincipal.EncryptPassword(PageValidate.InputText(str2, 30));
            if (!user.Update())
            {
                base.ModelState.AddModelError("Error", "密码重置失败，请检查输入的信息是否正确或者联系管理员！");
                return base.View();
            }
            AccountsPrincipal.ValidateLogin(userName, str2);
            FormsAuthentication.SetAuthCookie(userName, false);
            base.Session[Globals.SESSIONKEY_USER] = user;
            base.Session["Style"] = user.Style;
            new Maticsoft.BLL.Members.PointsDetail().AddPoints("Login", user.UserID, "登录操作", "");
            if (base.Session["returnPage"] != null)
            {
                string url = base.Session["returnPage"].ToString();
                base.Session["returnPage"] = null;
                return this.Redirect(url);
            }
            return base.RedirectToAction("Index", "UserCenter");
        }

        internal class AcceptAllCertificatePolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert, WebRequest wRequest, int certProb)
            {
                return true;
            }
        }

        public interface ITaoLe : IApiBinding
        {
            Task<JsonValue> GetUserProfileAsync();

            IRestOperations RestOperations { get; }
        }

        public class TaoLeOAuth2Template : OAuth2Template
        {
            public TaoLeOAuth2Template(string clientId, string clientSecret) : base(clientId, clientSecret, "http://www.beitaichufang.com/api/v1/oauth2/authorize/", "https://www.beitaichufang.com/api/v1/oauth2/token/", false)
            {
            }

            protected override Task<AccessGrant> PostForAccessGrantAsync(string accessTokenUrl, NameValueCollection request)
            {
                return base.RestTemplate.PostForObjectAsync<NameValueCollection>(accessTokenUrl, request, new object[0]).ContinueWith<AccessGrant>(delegate (Task<NameValueCollection> task) {
                    string s = task.Result["expires"];
                    return new AccessGrant(task.Result["access_token"], null, null, (s != null) ? new int?(int.Parse(s)) : null, null);
                }, TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        public class TaoLeServiceProvider : AbstractOAuth2ServiceProvider<AccountController.ITaoLe>
        {
            public TaoLeServiceProvider(string clientId, string clientSecret) : base(new OAuth2Template(clientId, clientSecret, "http://www.beitaichufang.com/api/v1/oauth2/authorize/", "https://www.beitaichufang.com/api/v1/oauth2/token/", true))
            {
            }

            public override AccountController.ITaoLe GetApi(AccessGrant accessGrant)
            {
                return new AccountController.TaoLeTemplate(accessGrant);
            }
        }

        public class TaoLeTemplate : AbstractOAuth2ApiBinding, AccountController.ITaoLe, IApiBinding
        {
            private static readonly Uri API_URI_BASE = new Uri("https://www.beitaichufang.com/api/v1/user/");
            private const string PROFILE_PATH = "me/";

            public TaoLeTemplate(AccessGrant accessGrant) : base(accessGrant)
            {
            }

            protected override void ConfigureRestTemplate(RestTemplate restTemplate)
            {
                restTemplate.BaseAddress = API_URI_BASE;
            }

            protected override IList<IHttpMessageConverter> GetMessageConverters()
            {
                IList<IHttpMessageConverter> messageConverters = base.GetMessageConverters();
                messageConverters.Add(new SpringJsonHttpMessageConverter());
                return messageConverters;
            }

            protected override OAuth2Version GetOAuth2Version()
            {
                return OAuth2Version.Bearer;
            }

            public Task<JsonValue> GetUserProfileAsync()
            {
                return base.RestTemplate.GetForObjectAsync<JsonValue>("me/", new object[0]);
            }

            public IRestOperations RestOperations
            {
                get
                {
                    return base.RestTemplate;
                }
            }
        }
    }
}

