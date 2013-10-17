namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Components;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Web.UI;

    public class AccountController : SNSControllerBase
    {
        private User userBusManage = new User();
        private UsersExp userExpManage = new UsersExp();
        private Maticsoft.BLL.Members.Users userManage = new Maticsoft.BLL.Members.Users();

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
            if (base.CurrentUser != null)
            {
                return base.RedirectToAction("Posts", "Profile");
            }
            ((dynamic) base.ViewBag).Title = "找回密码";
            return base.View();
        }

        [HttpPost]
        public ActionResult FindPwd(FormCollection collection)
        {
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
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login"))
            {
                return base.RedirectToAction("TurnOff", "Error");
            }
            if (base.CurrentUser != null)
            {
                return base.RedirectToAction("Posts", "Profile");
            }
            ((dynamic) base.ViewBag).Title = "登录";
            return base.View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            ((dynamic) base.ViewBag).Title = "登录";
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login"))
            {
                return base.RedirectToAction("TurnOff", "Error");
            }
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
            int num = new Maticsoft.BLL.Members.PointsDetail().AddPoints("Login", user.UserID, "登录操作", "");
            if (base.CurrentThemeName == "TaoLe")
            {
                ShoppingCartHelper.LoadShoppingCart(user.UserID);
            }
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
            return base.RedirectToAction("Posts", "Profile");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            base.Session.Remove(Globals.SESSIONKEY_USER);
            base.Session.Clear();
            base.Session.Abandon();
            return base.RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register"))
            {
                return base.RedirectToAction("TurnOff", "Error");
            }
            if (base.CurrentUser != null)
            {
                return base.RedirectToAction("Posts", "Profile");
            }
            ((dynamic) base.ViewBag).Title = "注册";
            return base.View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            ((dynamic) base.ViewBag).Title = "注册";
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register"))
            {
                return base.RedirectToAction("TurnOff", "Error");
            }
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
                    new Maticsoft.BLL.Members.PointsDetail().AddPoints("Register", num, "注册成功", "");
                    Maticsoft.Model.SNS.UserAlbums albums = new Maticsoft.Model.SNS.UserAlbums();
                    Maticsoft.BLL.SNS.UserAlbums albums2 = new Maticsoft.BLL.SNS.UserAlbums();
                    Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
                    albums.AlbumName = "默认专辑";
                    albums.CreatedDate = DateTime.Now;
                    albums.CreatedNickName = model.NickName;
                    albums.CreatedUserID = num;
                    albums2.AddEx(albums, 1);
                    string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
                    valueByCache = string.IsNullOrEmpty(valueByCache) ? "/Upload/User/Gravatar/Default.jpg" : valueByCache;
                    string str2 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
                    str2 = string.IsNullOrEmpty(str2) ? "/Upload/User/Gravatar/" : str2;
                    string str3 = base.ControllerContext.HttpContext.Server.MapPath("/");
                    if (File.Exists(str3 + valueByCache))
                    {
                        File.Copy(str3 + valueByCache, string.Concat(new object[] { str3, str2, num, ".jpg" }), true);
                    }
                    ship.GiveUserFellow(num);
                    return this.Redirect("/UserCenter/Personal");
                }
                base.ModelState.AddModelError("Message", ErrorCodeToString(MembershipCreateStatus.DuplicateUserName));
            }
            return base.View(model);
        }

        [HttpPost]
        public void SendEmail(FormCollection collection)
        {
            if (!string.IsNullOrWhiteSpace(collection["Email"]))
            {
                User user = new User(collection["Email"]);
                int type = 1;
                if (!string.IsNullOrWhiteSpace(user.NickName))
                {
                    this.SendEmail(user.UserName, user.Email, type);
                    base.Response.ContentType = "application/text";
                    base.Response.Write("success");
                }
            }
        }

        protected void SendEmail(string username, string email, int type)
        {
            EmailTemplet templet = new EmailTemplet();
            if (type == 1)
            {
                templet.SendFindPwdEmail(username, email);
            }
        }

        public ActionResult SendSMS(FormCollection Fm)
        {
            string str = Fm["Phone"];
            if (string.IsNullOrWhiteSpace(str))
            {
                return base.Content("False");
            }
            int num = new Random().Next(0x186a0, 0xf423f);
            string content = "您的的手机效验码是：" + num;
            base.Session["SMSCode"] = num;
            string[] numbers = new string[] { str };
            if (!SMSHelper.SendSMS(content, numbers, 5))
            {
                return base.Content("False");
            }
            return base.Content("True");
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

        private static string ToMediaName(int MediaId)
        {
            switch (MediaId)
            {
                case 1:
                    return "Google";

                case 2:
                    return "Windows Live";

                case 3:
                    return "sina";

                case 4:
                    return "tencent";

                case 5:
                    return "sohu";

                case 6:
                    return "163";

                case 7:
                    return "renren";

                case 8:
                    return "kaixin";

                case 9:
                    return "douban";

                case 12:
                    return "yahoo";

                case 13:
                    return "QQ";

                case 0x10:
                    return "taobao";

                case 0x11:
                    return "tianya";

                case 0x12:
                    return "alipay";

                case 0x13:
                    return "baidu";
            }
            return "maticsoft";
        }

        public ActionResult VerifyPassword()
        {
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
            return base.RedirectToAction("Posts", "Profile");
        }
    }
}

