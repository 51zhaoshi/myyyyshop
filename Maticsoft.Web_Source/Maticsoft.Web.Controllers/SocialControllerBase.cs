namespace Maticsoft.Web.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.Members.Enum;
    using Maticsoft.Model.SNS;
    using Maticsoft.OAuth.Json;
    using Maticsoft.OAuth.Rest.Client;
    using Maticsoft.OAuth.Sina;
    using Maticsoft.OAuth.Tencent.QQ;
    using Maticsoft.OAuth.Tencent.Weibo;
    using Maticsoft.OAuth.v2;
    using Maticsoft.Web;
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Security;

    public abstract class SocialControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
        private readonly string _domain = ("http://" + Globals.DomainFullName);
        private string QQAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQAppId");
        private string QQSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQSercet");
        private const string SESSION_KEY_OAUTH2STATE = "OAuth2CallbackState";
        private const string SESSION_KEY_OAUTH2TRY = "OAuth2CallbackTry";
        private string SinaAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaAppId");
        private string SinaSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaSercet");
        private readonly string STATE = ("maticsoft" + DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        private string TencentAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentAppId");
        private string TencentSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentSercet");

        protected SocialControllerBase()
        {
        }

        private ActionResult CallbackUserInfo(MediaType mediaType, AccessGrant accessGrant, string userIdOAuth, string nickNameOAuth, string emailOAuth)
        {
            if (base.CurrentUser != null)
            {
                Maticsoft.BLL.Members.UserBind bind = new Maticsoft.BLL.Members.UserBind();
                Maticsoft.Model.Members.UserBind bind2 = new Maticsoft.Model.Members.UserBind {
                    MediaID = (int) mediaType,
                    MediaNickName = nickNameOAuth,
                    MediaUserID = userIdOAuth.ToString(),
                    TokenAccess = accessGrant.AccessToken,
                    UserId = (base.CurrentUser.UserType == "AA") ? -1 : base.CurrentUser.UserID,
                    TokenAccess = accessGrant.AccessToken,
                    TokenExpireTime = accessGrant.ExpireTime,
                    Comment = true,
                    iHome = true,
                    GroupTopic = true
                };
                if (!bind.AddEx(bind2))
                {
                    return this.Redirect("/");
                }
                if (base.currentUser.UserType == "AA")
                {
                    return this.Redirect("/Admin/Accounts/UserBind.aspx");
                }
                return this.RedirectToUserBind();
            }
            string userName = string.Format("{0}_{1}", mediaType.ToString(), userIdOAuth);
            string password = userName + this.SinaSercet;
            Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
            User user = new User();
            if (user.HasUserByUserName(userName))
            {
                User user2 = new User(userName);
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
                return this.RedirectToHome();
            }
            User user3 = new User();
            string nickName = nickNameOAuth;
            while (user3.HasUserByNickName(nickName))
            {
                nickName = nickNameOAuth + "_" + Globals.GenRandomCodeFor6();
            }
            user.UserName = userName;
            user.Email = emailOAuth;
            user.Password = AccountsPrincipal.EncryptPassword(password);
            user.Activity = true;
            user.UserType = "UU";
            user.NickName = nickName;
            user.Style = 1;
            user.User_dateCreate = DateTime.Now;
            user.User_cLang = "zh-CN";
            int num = user.Create();
            if (num <= 0)
            {
                return this.Redirect("/");
            }
            UsersExp model = new UsersExp {
                UserID = num,
                Email = emailOAuth,
                Gravatar = string.Format("/{0}/User/Gravatar/{1}", Maticsoft.Components.MvcApplication.UploadFolder, num),
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
                users.Delete(num);
                new UsersExp().DeleteUsersExp(num);
                return this.Redirect("/");
            }
            User user4 = new User(userName);
            FormsAuthentication.SetAuthCookie(userName, false);
            base.Session[Globals.SESSIONKEY_USER] = user4;
            base.Session["Style"] = user4.Style;
            if (Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_Register_IsBind") == "1")
            {
                Maticsoft.BLL.Members.UserBind bind3 = new Maticsoft.BLL.Members.UserBind();
                Maticsoft.Model.Members.UserBind bind4 = new Maticsoft.Model.Members.UserBind {
                    MediaID = (int) mediaType,
                    MediaNickName = nickNameOAuth,
                    MediaUserID = userIdOAuth,
                    TokenAccess = accessGrant.AccessToken,
                    TokenExpireTime = accessGrant.ExpireTime,
                    UserId = num,
                    TokenAccess = accessGrant.AccessToken,
                    TokenExpireTime = accessGrant.ExpireTime,
                    Comment = true,
                    iHome = true,
                    GroupTopic = true
                };
                if (!bind3.AddEx(bind4))
                {
                    return this.Redirect("/");
                }
            }
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
            valueByCache = string.IsNullOrEmpty(valueByCache) ? "/Upload/User/Gravatar/Default.jpg" : valueByCache;
            string str7 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
            str7 = string.IsNullOrEmpty(str7) ? "/Upload/User/Gravatar/" : str7;
            string str8 = base.ControllerContext.HttpContext.Server.MapPath("/");
            if (File.Exists(str8 + valueByCache))
            {
                File.Copy(str8 + valueByCache, string.Concat(new object[] { str8, str7, user4.UserID, ".jpg" }), true);
            }
            new Maticsoft.BLL.Members.PointsDetail().AddPoints("Register", num, "注册成功", "");
            Maticsoft.Model.SNS.UserAlbums albums = new Maticsoft.Model.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbums albums2 = new Maticsoft.BLL.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            albums.AlbumName = "默认专辑";
            albums.CreatedDate = DateTime.Now;
            albums.CreatedNickName = user4.NickName;
            albums.CreatedUserID = user4.UserID;
            albums2.AddEx(albums, 1);
            ship.GiveUserFellow(user4.UserID);
            return this.RedirectToHome();
        }

        private bool CheckSessionState(string state)
        {
            string str = base.Session["OAuth2CallbackState"] as string;
            base.Session.Remove("OAuth2CallbackState");
            return (!string.IsNullOrWhiteSpace(str) && (str == state));
        }

        public ActionResult QQ()
        {
            if (string.IsNullOrWhiteSpace(this.QQAppId))
            {
                return base.Content("该网站尚未启用QQ登录");
            }
            IOAuth2ServiceProvider<IQConnect> provider = new QConnectServiceProvider(this.QQAppId, this.QQSercet);
            OAuth2Parameters parameters2 = new OAuth2Parameters {
                RedirectUrl = this.RedirectQQUrl,
                Scope = "get_user_info,add_t,add_pic_t",
                State = this.STATE
            };
            OAuth2Parameters parameters = parameters2;
            base.Session["OAuth2CallbackState"] = this.STATE;
            return this.Redirect(provider.OAuthOperations.BuildAuthorizeUrl(GrantType.AuthorizationCode, parameters));
        }

        public ActionResult QQCallback(string code, string state)
        {
            AccessGrant result;
            JsonValue value2;
            if (string.IsNullOrWhiteSpace(code) || !this.CheckSessionState(state))
            {
                return this.Redirect("/");
            }
            IOAuth2ServiceProvider<IQConnect> provider = new QConnectServiceProvider(this.QQAppId, this.QQSercet);
            try
            {
                result = provider.OAuthOperations.ExchangeForAccessAsync(code, this.RedirectQQUrl, null).Result;
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
                    LogHelp.AddErrorLog(innerException.GetResponseBodyAsString(), innerException.StackTrace, "OAuth2 QQCallback HttpResponseException Try:" + num);
                }
                if (num > 0)
                {
                    base.Session["OAuth2CallbackTry"] = --num;
                    return base.RedirectToAction("QQ", "Social", new { area = Maticsoft.Components.MvcApplication.GetCurrentAreaRoute(base.ControllerContext).ToString() });
                }
                base.Session.Remove("OAuth2CallbackTry");
                return this.Redirect("/");
            }
            base.Session.Remove("OAuth2CallbackTry");
            IQConnect api = provider.GetApi(result);
            try
            {
                value2 = api.GetUserProfileAsync().Result;
            }
            catch (Exception exception3)
            {
                LogHelp.AddErrorLog(exception3.Message, exception3.StackTrace, "OAuth2 QQCallback GetUserProfileAsync Exception");
                return this.Redirect("/");
            }
            if (value2 == null)
            {
                string loginfo = "GetUserProfileAsync: userInfoJson IS NULL";
                LogHelp.AddErrorLog(loginfo, loginfo, "OAuth2 QQCallback");
                return this.Redirect("/");
            }
            string userIdOAuth = result.ExtraData[0];
            string nickNameOAuth = value2.GetValue<string>("nickname");
            return this.CallbackUserInfo(MediaType.QZone, result, userIdOAuth, nickNameOAuth, null);
        }

        public abstract ActionResult RedirectToHome();
        public abstract ActionResult RedirectToUserBind();
        public ActionResult Sina()
        {
            if (string.IsNullOrWhiteSpace(this.SinaAppId))
            {
                return base.Content("该网站尚未启用新浪微博登录");
            }
            IOAuth2ServiceProvider<Maticsoft.OAuth.Sina.IWeibo> provider = new Maticsoft.OAuth.Sina.WeiboServiceProvider(this.SinaAppId, this.SinaSercet);
            OAuth2Parameters parameters2 = new OAuth2Parameters {
                RedirectUrl = this.RedirectSinaUrl,
                State = this.STATE
            };
            OAuth2Parameters parameters = parameters2;
            return this.Redirect(provider.OAuthOperations.BuildAuthorizeUrl(GrantType.AuthorizationCode, parameters));
        }

        public ActionResult SinaCallback(string code)
        {
            AccessGrant result;
            JsonValue value2;
            IOAuth2ServiceProvider<Maticsoft.OAuth.Sina.IWeibo> provider = new Maticsoft.OAuth.Sina.WeiboServiceProvider(this.SinaAppId, this.SinaSercet);
            if (string.IsNullOrWhiteSpace(code))
            {
                return this.Redirect("/");
            }
            try
            {
                result = provider.OAuthOperations.ExchangeForAccessAsync(code, this.RedirectSinaUrl, null).Result;
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
                    LogHelp.AddErrorLog(innerException.GetResponseBodyAsString(), innerException.StackTrace, "OAuth2 SinaCallback HttpResponseException Try:" + num);
                }
                if (num > 0)
                {
                    base.Session["OAuth2CallbackTry"] = --num;
                    return base.RedirectToAction("Sina", "Social", new { area = Maticsoft.Components.MvcApplication.GetCurrentAreaRoute(base.ControllerContext).ToString() });
                }
                base.Session.Remove("OAuth2CallbackTry");
                return this.Redirect("/");
            }
            Maticsoft.OAuth.Sina.IWeibo api = provider.GetApi(result);
            try
            {
                value2 = api.GetUserProfileAsync().Result;
            }
            catch (Exception exception3)
            {
                LogHelp.AddErrorLog(exception3.Message, exception3.StackTrace, "OAuth2 SinaCallback GetUserProfileAsync Exception");
                return this.Redirect("/");
            }
            if (value2 == null)
            {
                string loginfo = "GetUserProfileAsync: userInfoJson IS NULL";
                LogHelp.AddErrorLog(loginfo, loginfo, "OAuth2 SinaCallback");
                return this.Redirect("/");
            }
            long num2 = value2.GetValue<long>("id");
            string nickNameOAuth = value2.GetValue<string>("name");
            return this.CallbackUserInfo(MediaType.Sina, result, num2.ToString(), nickNameOAuth, null);
        }

        public ActionResult TaoBaoCallback()
        {
            base.Session["TaoBao_Session_Key"] = base.Request.Params["top_session"];
            if ((base.currentUser != null) && (base.currentUser.UserType == "AA"))
            {
                return this.Redirect("/Admin/Ms/TaoData/GetTaoList.aspx");
            }
            return this.Redirect("/");
        }

        public ActionResult Tencent()
        {
            if (string.IsNullOrWhiteSpace(this.TencentAppId))
            {
                return base.Content("该网站尚未启用腾讯微博登录");
            }
            IOAuth2ServiceProvider<Maticsoft.OAuth.Tencent.Weibo.IWeibo> provider = new Maticsoft.OAuth.Tencent.Weibo.WeiboServiceProvider(this.TencentAppId, this.TencentSercet);
            OAuth2Parameters parameters2 = new OAuth2Parameters {
                RedirectUrl = this.RedirectTencentUrl,
                State = this.STATE
            };
            OAuth2Parameters parameters = parameters2;
            base.Session["OAuth2CallbackState"] = this.STATE;
            return this.Redirect(provider.OAuthOperations.BuildAuthorizeUrl(GrantType.AuthorizationCode, parameters));
        }

        public ActionResult TencentCallback(string code, string state, string openid, string openkey)
        {
            AccessGrant result;
            JsonValue value2;
            if (string.IsNullOrWhiteSpace(code) || !this.CheckSessionState(state))
            {
                return this.Redirect("/");
            }
            IOAuth2ServiceProvider<Maticsoft.OAuth.Tencent.Weibo.IWeibo> provider = new Maticsoft.OAuth.Tencent.Weibo.WeiboServiceProvider(this.TencentAppId, this.TencentSercet);
            try
            {
                result = provider.OAuthOperations.ExchangeForAccessAsync(code, this.RedirectTencentUrl, null).Result;
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
                    LogHelp.AddErrorLog(innerException.GetResponseBodyAsString(), innerException.StackTrace, "OAuth2 TencentCallback HttpResponseException Try:" + num);
                }
                if (num > 0)
                {
                    base.Session["OAuth2CallbackTry"] = --num;
                    return base.RedirectToAction("Tencent", "Social", new { area = Maticsoft.Components.MvcApplication.GetCurrentAreaRoute(base.ControllerContext).ToString() });
                }
                base.Session.Remove("OAuth2CallbackTry");
                return this.Redirect("/");
            }
            base.Session.Remove("OAuth2CallbackTry");
            result = new AccessGrant(result, new string[] { openid, openkey, Globals.ClientIP });
            Maticsoft.OAuth.Tencent.Weibo.IWeibo api = provider.GetApi(result);
            try
            {
                value2 = api.GetUserProfileAsync().Result;
            }
            catch (Exception exception3)
            {
                LogHelp.AddErrorLog(exception3.Message, exception3.StackTrace, "OAuth2 TencentCallback GetUserProfileAsync Exception");
                return this.Redirect("/");
            }
            if (value2 == null)
            {
                string loginfo = "GetUserProfileAsync: userInfoJson IS NULL";
                LogHelp.AddErrorLog(loginfo, loginfo, "OAuth2 TencentCallback");
                return this.Redirect("/");
            }
            string userIdOAuth = result.ExtraData[0] + "|" + result.ExtraData[1];
            string nickNameOAuth = value2.GetValue("data").GetValue<string>("name");
            return this.CallbackUserInfo(MediaType.Tencent, result, userIdOAuth, nickNameOAuth, null);
        }

        private string RedirectQQUrl
        {
            get
            {
                return (this._domain + "/social/qqcallback");
            }
        }

        private string RedirectSinaUrl
        {
            get
            {
                return (this._domain + "/social/sinacallback");
            }
        }

        private string RedirectTencentUrl
        {
            get
            {
                return (this._domain + "/social/tencentcallback");
            }
        }
    }
}

