namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.Members;
    using Maticsoft.ViewModel.UserCenter;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class UserCenterController : SNSUserControllerBase
    {
        private Maticsoft.BLL.Members.SiteMessage bllSM = new Maticsoft.BLL.Members.SiteMessage();
        private UsersExp bllUE = new UsersExp();
        private Maticsoft.BLL.Members.PointsDetail detailBll = new Maticsoft.BLL.Members.PointsDetail();
        private Maticsoft.BLL.Members.UserBind userBind = new Maticsoft.BLL.Members.UserBind();
        private UsersExp userEXBll = new UsersExp();

        public ActionResult AjaxAddTags(FormCollection fc)
        {
            string str = fc["tags"];
            if ((base.currentUser != null) && !string.IsNullOrEmpty(str))
            {
                UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
                usersModel.Remark = string.IsNullOrEmpty(usersModel.Remark) ? str : (usersModel.Remark + "," + str);
                if (this.bllUE.UpdateUsersExp(usersModel))
                {
                    return base.Content("Ok");
                }
            }
            return new EmptyResult();
        }

        public ActionResult AjaxDeleTags(FormCollection fc)
        {
            Func<string, bool> predicate = null;
            string strTag = fc["tags"];
            if ((base.currentUser != null) && !string.IsNullOrEmpty(strTag))
            {
                UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
                if (predicate == null)
                {
                    predicate = tags => tags != strTag;
                }
                IEnumerable<string> values = usersModel.Remark.Split(new char[] { ',' }).Where<string>(predicate);
                usersModel.Remark = string.Join(",", values);
                if (this.bllUE.UpdateUsersExp(usersModel))
                {
                    return base.Content("Ok");
                }
            }
            return new EmptyResult();
        }

        [HttpPost]
        public void CancelBind(FormCollection collection)
        {
            if (!string.IsNullOrWhiteSpace(collection["BindId"]))
            {
                base.Response.ContentType = "application/text";
                int bindId = Globals.SafeInt(collection["BindId"], 0);
                if (this.userBind.Delete(bindId))
                {
                    base.Response.Write("success");
                }
            }
        }

        public ActionResult ChangePassword()
        {
            return base.View();
        }

        [HttpPost]
        public void CheckNickName(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                string str = collection["NickName"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
                    if (users.ExistsNickName(base.CurrentUser.UserID, str))
                    {
                        obj2.Accumulate("STATUS", "EXISTS");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "OK");
                    }
                }
                else
                {
                    obj2.Accumulate("STATUS", "NOTNULL");
                }
                base.Response.Write(obj2.ToString());
            }
        }

        [HttpPost]
        public void CheckPassword(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                string str = collection["Password"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    SiteIdentity identity = new SiteIdentity(base.CurrentUser.UserName);
                    if (identity.TestPassword(str.Trim()) == 0)
                    {
                        obj2.Accumulate("STATUS", "ERROR");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "OK");
                    }
                }
                else
                {
                    obj2.Accumulate("STATUS", "UNDEFINED");
                }
                base.Response.Write(obj2.ToString());
            }
        }

        [ValidateInput(false), HttpPost]
        public void CheckVideoUrl(FormCollection collection)
        {
            string str = collection["VideoUrl"];
            JsonObject obj2 = new JsonObject();
            if (string.IsNullOrWhiteSpace(str))
            {
                obj2.Accumulate("STATUS", "NotNull");
            }
            else
            {
                int type = this.GetType(str);
                switch (type)
                {
                    case 0:
                        obj2.Accumulate("STATUS", "Error");
                        goto Label_0101;

                    case 1:
                    {
                        YouKuInfo youKuInfo = VideoHelper.GetYouKuInfo(str);
                        if (youKuInfo != null)
                        {
                            obj2.Accumulate("STATUS", "Succ");
                            obj2.Accumulate("VideoUrl", string.Format("http://player.youku.com/player.php/sid/{0}/v.swf", youKuInfo.VidEncoded));
                            obj2.Accumulate("ImageUrl", youKuInfo.Logo);
                        }
                        else
                        {
                            obj2.Accumulate("STATUS", "Error");
                        }
                        break;
                    }
                }
                if (type == 2)
                {
                    Ku6Info info2 = VideoHelper.GetKu6Info(str);
                    if (info2 != null)
                    {
                        obj2.Accumulate("STATUS", "Succ");
                        obj2.Accumulate("VideoUrl", info2.flash);
                        obj2.Accumulate("ImageUrl", info2.coverurl);
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "Error");
                    }
                }
            }
        Label_0101:
            base.Response.Write(obj2.ToString());
        }

        public byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            byte[] buffer;
            try
            {
                using (Bitmap bitmap = new Bitmap(base.Server.MapPath(Img)))
                {
                    using (Bitmap bitmap2 = new Bitmap(Width, Height, bitmap.PixelFormat))
                    {
                        bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                        using (Graphics graphics = Graphics.FromImage(bitmap2))
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(bitmap, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, GraphicsUnit.Pixel);
                            MemoryStream stream = new MemoryStream();
                            bitmap2.Save(stream, bitmap.RawFormat);
                            buffer = stream.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return buffer;
        }

        [HttpPost]
        public void CutGravatar(FormCollection collection)
        {
            string path = "/" + MvcApplication.UploadFolder + "/User/Gravatar/";
            if ((!string.IsNullOrWhiteSpace(collection["x"]) && !string.IsNullOrWhiteSpace(collection["y"])) && ((!string.IsNullOrWhiteSpace(collection["w"]) && !string.IsNullOrWhiteSpace(collection["h"])) && !string.IsNullOrWhiteSpace(collection["filename"])))
            {
                int x = (int) Globals.SafeDecimal(collection["x"], (decimal) 0M);
                int y = (int) Globals.SafeDecimal(collection["y"], (decimal) 0M);
                int width = (int) Globals.SafeDecimal(collection["w"], (decimal) 0M);
                int height = (int) Globals.SafeDecimal(collection["h"], (decimal) 0M);
                string img = collection["filename"];
                int userID = base.currentUser.UserID;
                try
                {
                    byte[] buffer = this.Crop(img, width, height, x, y);
                    if (!Directory.Exists(base.Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(base.Server.MapPath(path));
                    }
                    FileStream stream = new FileStream(base.Server.MapPath(path + userID + ".jpg"), FileMode.Create);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                    base.Response.Write("success");
                }
                catch (Exception exception)
                {
                    base.Response.Write(exception);
                }
            }
        }

        public ActionResult Domain()
        {
            ((dynamic) base.ViewBag).Title = "个性域名 - " + ((dynamic) base.ViewBag).SiteName;
            if (base.HttpContext.User.Identity.IsAuthenticated && (base.CurrentUser != null))
            {
                UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
                if (usersModel != null)
                {
                    return base.View(usersModel);
                }
            }
            return base.RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public void ExistsNickName(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                string str = collection["NickName"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
                    if (users.ExistsNickName(str))
                    {
                        obj2.Accumulate("STATUS", "EXISTS");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "NOTEXISTS");
                    }
                }
                else
                {
                    obj2.Accumulate("STATUS", "NOTNULL");
                }
                base.Response.Write(obj2.ToString());
            }
        }

        public string GetRuleName(string RuleAction)
        {
            Maticsoft.BLL.Members.PointsRule rule = new Maticsoft.BLL.Members.PointsRule();
            return rule.GetRuleName(RuleAction);
        }

        public int GetType(string url)
        {
            int num = 0;
            if (VideoHelper.IsYouKuVideoUrl(url))
            {
                num = 1;
            }
            if (VideoHelper.IsKu6VideoUrl(url))
            {
                num = 2;
            }
            return num;
        }

        public ActionResult Gravatar()
        {
            ((dynamic) base.ViewBag).Title = "修改头像";
            if (base.CurrentUser == null)
            {
                return base.RedirectToAction("Login", "Account");
            }
            ((dynamic) base.ViewBag).UserID = base.CurrentUser.UserID;
            return base.View("Gravatar");
        }

        [HttpPost]
        public ActionResult Gravatar(FormCollection collection)
        {
            ((dynamic) base.ViewBag).Title = "修改头像";
            try
            {
                if (base.CurrentUser != null)
                {
                    UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
                    if (usersModel != null)
                    {
                        usersModel.Gravatar = collection["Gravatar"];
                        if (this.bllUE.UpdateUsersExp(usersModel))
                        {
                            return base.RedirectToAction("Personal");
                        }
                        return base.RedirectToAction("Personal");
                    }
                }
                return base.RedirectToAction("Login", "Account");
            }
            catch
            {
                return base.View();
            }
        }

        public ActionResult Inbox(int? page)
        {
            ((dynamic) base.ViewBag).Title = "收件箱";
            if (base.CurrentUser == null)
            {
                return base.RedirectToAction("Login", "Account");
            }
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 7;
            PagedList<Maticsoft.Model.Members.SiteMessage> model = this.bllSM.GetAllReceiveMsgListByMvcPage(base.CurrentUser.UserID, -1, pageSize, page.Value);
            foreach (Maticsoft.Model.Members.SiteMessage message in model)
            {
                if (!message.ReceiverIsRead)
                {
                    this.bllSM.SetReceiveMsgAlreadyRead(message.ID);
                }
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserCenter/InboxList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/UserCenter/InBox.cshtml", model);
        }

        public ActionResult Index()
        {
            return base.RedirectToAction("Personal");
        }

        public ActionResult Outbox(int? page)
        {
            ((dynamic) base.ViewBag).Title = "发件箱";
            if (base.CurrentUser == null)
            {
                return base.RedirectToAction("Login", "Account");
            }
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            PagedList<Maticsoft.Model.Members.SiteMessage> model = this.bllSM.GetAllSendMsgListByMvcPage(base.CurrentUser.UserID, pageSize, page.Value);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserCenter/OutboxList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/UserCenter/OutBox.cshtml", model);
        }

        public ActionResult Personal()
        {
            ((dynamic) base.ViewBag).Title = "个人资料";
            if (base.HttpContext.User.Identity.IsAuthenticated && (base.CurrentUser != null))
            {
                UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
                if (usersModel != null)
                {
                    return base.View(usersModel);
                }
            }
            return base.RedirectToAction("Login", "Account");
        }

        public ActionResult PointsDetail(int pageIndex = 1)
        {
            ((dynamic) base.ViewBag).Title = "积分明细";
            UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
            if (usersModel != null)
            {
                ((dynamic) base.ViewBag).UserInfo = usersModel;
            }
            int pageSize = 15;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            totalItemCount = this.detailBll.GetRecordCount(" UserID=" + base.CurrentUser.UserID);
            if (totalItemCount < 1)
            {
                return base.View();
            }
            List<Maticsoft.Model.Members.PointsDetail> items = this.detailBll.GetListByPageEX("UserID=" + base.CurrentUser.UserID, "", startIndex, endIndex);
            if ((items != null) && (items.Count > 0))
            {
                foreach (Maticsoft.Model.Members.PointsDetail detail in items)
                {
                    detail.RuleAction = this.GetRuleName(detail.RuleAction);
                }
            }
            PagedList<Maticsoft.Model.Members.PointsDetail> model = new PagedList<Maticsoft.Model.Members.PointsDetail>(items, pageIndex, pageSize, totalItemCount);
            return base.View(model);
        }

        public ActionResult SendMessage()
        {
            ((dynamic) base.ViewBag).Title = "发信息";
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                return base.RedirectToAction("Login", "Account");
            }
            ((dynamic) base.ViewBag).Name = base.Request.Params["name"];
            return base.View("SendMessage");
        }

        [HttpPost]
        public void SendMsg(FormCollection collection)
        {
            ((dynamic) base.ViewBag).Title = "发信息";
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                string str = collection["NickName"];
                string str2 = collection["Title"];
                string str3 = collection["Content"];
                if (string.IsNullOrWhiteSpace(str))
                {
                    obj2.Accumulate("STATUS", "NICKNAMENULL");
                }
                else if (string.IsNullOrWhiteSpace(str2))
                {
                    obj2.Accumulate("STATUS", "TITLENULL");
                }
                else if (string.IsNullOrWhiteSpace(str3))
                {
                    obj2.Accumulate("STATUS", "CONTENTNULL");
                }
                else
                {
                    Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
                    if (users.ExistsNickName(str))
                    {
                        int userIdByNickName = users.GetUserIdByNickName(str);
                        Maticsoft.Model.Members.SiteMessage model = new Maticsoft.Model.Members.SiteMessage {
                            Title = str2,
                            Content = str3,
                            SenderID = new int?(base.CurrentUser.UserID),
                            ReaderIsDel = false,
                            ReceiverIsRead = false,
                            SenderIsDel = false,
                            ReceiverID = new int?(userIdByNickName),
                            SendTime = new DateTime?(DateTime.Now)
                        };
                        if (this.bllSM.Add(model) > 0)
                        {
                            obj2.Accumulate("STATUS", "SUCC");
                        }
                        else
                        {
                            obj2.Accumulate("STATUS", "FAIL");
                        }
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "NICKNAMENOTEXISTS");
                    }
                }
                base.Response.Write(obj2.ToString());
            }
        }

        public ActionResult SubmitApprove()
        {
            ((dynamic) base.ViewBag).Title = "实名认证--确认并提交";
            Maticsoft.Model.Members.UsersApprove model = new Maticsoft.Model.Members.UsersApprove();
            if (base.Session["USERAPPROVE"] != null)
            {
                model = (Maticsoft.Model.Members.UsersApprove) base.Session["USERAPPROVE"];
                if (model != null)
                {
                    return base.View(model);
                }
            }
            return base.RedirectToAction("UserApprove", "UserCenter");
        }

        [HttpPost]
        public void SubmitApprove(FormCollection collection)
        {
            JsonObject obj2 = new JsonObject();
            if (base.Session["USERAPPROVE"] != null)
            {
                Maticsoft.BLL.Members.UsersApprove approve = new Maticsoft.BLL.Members.UsersApprove();
                Maticsoft.Model.Members.UsersApprove model = new Maticsoft.Model.Members.UsersApprove();
                model = (Maticsoft.Model.Members.UsersApprove) base.Session["USERAPPROVE"];
                string oldValue = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                string newValue = "/Upload/SNS/Images/ApproveImage/";
                ArrayList fileNameList = new ArrayList();
                fileNameList.Add(model.FrontView.Replace(oldValue, ""));
                fileNameList.Add(model.RearView.Replace(oldValue, ""));
                model.FrontView = model.FrontView.Replace(oldValue, newValue);
                model.RearView = model.RearView.Replace(oldValue, newValue);
                if (approve.Add(model) > 0)
                {
                    FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                    base.Session["USERAPPROVE"] = null;
                    obj2.Accumulate("STATUS", "SUCCESS");
                }
                else
                {
                    obj2.Accumulate("STATUS", "FAILE");
                }
            }
            else
            {
                obj2.Accumulate("STATUS", "FAILE");
            }
            base.Response.Write(obj2.ToString());
        }

        [HttpPost]
        public void SubmitSucc()
        {
            ((dynamic) base.ViewBag).Title = "实名认证";
            JsonObject obj2 = new JsonObject();
            Maticsoft.BLL.Members.UsersApprove approve = new Maticsoft.BLL.Members.UsersApprove();
            if (approve.DeleteByUserId(base.CurrentUser.UserID))
            {
                obj2.Accumulate("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Accumulate("STATUS", "FAILE");
            }
            base.Response.Write(obj2.ToString());
        }

        public ActionResult SubmitSucc(int? Id)
        {
            ((dynamic) base.ViewBag).Title = "实名认证";
            if (Id.HasValue)
            {
                if (Id.Value == 1)
                {
                    ((dynamic) base.ViewBag).Desc = "恭喜，您已通过实名认证";
                }
                else if (Id.Value == 0)
                {
                    ((dynamic) base.ViewBag).Desc = "恭喜，您已成功提交实名认证信息，请耐心等待网站审核！";
                }
                else
                {
                    ((dynamic) base.ViewBag).Falid = false;
                    ((dynamic) base.ViewBag).Desc = "对不起，您提交实名认证信息不符相关规定，请重新提交！";
                }
            }
            else
            {
                base.Session["USERAPPROVE"] = null;
                ((dynamic) base.ViewBag).Desc = "恭喜，您已成功提交实名认证信息，请耐心等待网站审核！";
            }
            return base.View();
        }

        public ActionResult SysInfo(int? page)
        {
            ((dynamic) base.ViewBag).Title = "系统消息";
            if (base.CurrentUser == null)
            {
                return base.RedirectToAction("Login", "Account");
            }
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 7;
            PagedList<Maticsoft.Model.Members.SiteMessage> model = this.bllSM.GetAllSystemMsgListByMvcPage(base.CurrentUser.UserID, -1, base.currentUser.UserType, pageSize, page.Value);
            foreach (Maticsoft.Model.Members.SiteMessage message in model)
            {
                if (!message.ReceiverIsRead)
                {
                    this.bllSM.SetSystemMsgStateToAlreadyRead(message.ID, base.currentUser.UserID, base.currentUser.UserType);
                }
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/SysInfoList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/UserCenter/SysInfo.cshtml", model);
        }

        public ActionResult Tags()
        {
            ((dynamic) base.ViewBag).Title = "个人标签设置";
            if (base.currentUser == null)
            {
                return new EmptyResult();
            }
            Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
            TagType type = new TagType();
            List<Maticsoft.Model.SNS.Tags> modelList = tags.GetModelList("TypeId=" + type.GetTagsTypeId("用户标签"));
            UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
            ((dynamic) base.ViewBag).UserTags = usersModel.Remark;
            return base.View(modelList);
        }

        public ActionResult TestWeiBo()
        {
            return base.View();
        }

        [ValidateInput(false), HttpPost]
        public void UpdateUserDomain(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
                if (usersModel == null)
                {
                    this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
                }
                else
                {
                    usersModel.HomePage = collection["Domain"];
                    if (this.bllUE.UpdateUsersExp(usersModel))
                    {
                        obj2.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "FAIL");
                    }
                    base.Response.Write(obj2.ToString());
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public void UpdateUserInfo(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                UsersExpModel usersModel = this.bllUE.GetUsersModel(base.CurrentUser.UserID);
                if (usersModel == null)
                {
                    base.RedirectToAction("Login");
                }
                else
                {
                    usersModel.TelPhone = collection["TelPhone"];
                    string str = collection["Birthday"];
                    if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsDateTime(str))
                    {
                        usersModel.Birthday = new DateTime?(Globals.SafeDateTime(str, DateTime.Now));
                    }
                    else
                    {
                        usersModel.Birthday = null;
                    }
                    usersModel.Constellation = collection["Constellation"];
                    usersModel.PersonalStatus = collection["PersonalStatus"];
                    usersModel.Singature = collection["Singature"];
                    usersModel.Address = collection["Address"];
                    User user = new User(base.CurrentUser.UserID) {
                        Sex = collection["Sex"],
                        Email = collection["Email"],
                        NickName = collection["NickName"],
                        Phone = collection["Phone"]
                    };
                    if (user.Update() && this.bllUE.UpdateUsersExp(usersModel))
                    {
                        obj2.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "FAIL");
                    }
                    base.Response.Write(obj2.ToString());
                }
            }
        }

        [HttpPost]
        public void UpdateUserPassword(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                string str = collection["NewPassword"];
                string str2 = collection["ConfirmPassword"];
                if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(str2))
                {
                    if (str.Trim() != str2.Trim())
                    {
                        obj2.Accumulate("STATUS", "FAIL");
                    }
                    else
                    {
                        base.currentUser.Password = AccountsPrincipal.EncryptPassword(str);
                        if (base.currentUser.Update())
                        {
                            obj2.Accumulate("STATUS", "UPDATESUCC");
                        }
                        else
                        {
                            obj2.Accumulate("STATUS", "UPDATEFAIL");
                        }
                    }
                }
                else
                {
                    obj2.Accumulate("STATUS", "UNDEFINED");
                }
                base.Response.Write(obj2.ToString());
            }
        }

        public ActionResult UserApprove(int? Id)
        {
            ((dynamic) base.ViewBag).Title = "实名认证";
            Maticsoft.BLL.Members.UsersApprove approve = new Maticsoft.BLL.Members.UsersApprove();
            Maticsoft.Model.Members.UsersApprove model = new Maticsoft.Model.Members.UsersApprove();
            model = approve.GetModelByUserID(base.CurrentUser.UserID);
            if (model != null)
            {
                return this.Redirect(string.Format("/UserCenter/SubmitSucc/{0}", model.Status));
            }
            if (Id.HasValue)
            {
                if (base.Session["USERAPPROVE"] != null)
                {
                    model = (Maticsoft.Model.Members.UsersApprove) base.Session["USERAPPROVE"];
                }
                ((dynamic) base.ViewBag).UserID = base.CurrentUser.UserID;
                return base.View(model);
            }
            base.Session["USERAPPROVE"] = null;
            ((dynamic) base.ViewBag).UserID = base.CurrentUser.UserID;
            model = new Maticsoft.Model.Members.UsersApprove {
                UserID = base.CurrentUser.UserID
            };
            return base.View(model);
        }

        [HttpPost]
        public void UserApprove(FormCollection collection)
        {
            JsonObject obj2 = new JsonObject();
            string str = collection["TrueName"];
            string str2 = collection["IdCardVal"];
            string str3 = collection["hiddenIdCardPreImage"];
            string str4 = collection["hiddenIdCardNeImage"];
            if ((!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(str2)) && (!string.IsNullOrWhiteSpace(str3) && !string.IsNullOrWhiteSpace(str4)))
            {
                Maticsoft.Model.Members.UsersApprove approve = new Maticsoft.Model.Members.UsersApprove {
                    UserID = base.CurrentUser.UserID,
                    TrueName = str,
                    IDCardNum = str2,
                    FrontView = string.Format(str3, ""),
                    RearView = string.Format(str4, ""),
                    Status = 0,
                    UserType = 0,
                    CreatedDate = DateTime.Now
                };
                base.Session["USERAPPROVE"] = approve;
                obj2.Accumulate("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Accumulate("STATUS", "FAILE");
            }
            base.Response.Write(obj2.ToString());
        }

        public ActionResult UserBind()
        {
            ((dynamic) base.ViewBag).Title = "会员中心—帐号绑定";
            UserBindList listEx = this.userBind.GetListEx(base.currentUser.UserID);
            return base.View(listEx);
        }

        public ActionResult UserRecommand()
        {
            return base.View();
        }
    }
}

