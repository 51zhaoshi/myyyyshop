namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Shop;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Json;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.Shop;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class UserCenterController : MobileControllerBaseUser
    {
        private Maticsoft.BLL.Members.SiteMessage bllSM = new Maticsoft.BLL.Members.SiteMessage();
        private Maticsoft.BLL.Members.PointsDetail detailBll = new Maticsoft.BLL.Members.PointsDetail();
        private UsersExp userEXBll = new UsersExp();

        public ActionResult AjaxAddConsult(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                return base.Content("False");
            }
            int num = Globals.SafeInt(Fm["ProductId"], 0);
            string str = InjectionFilter.SqlFilter(Fm["Content"]);
            Maticsoft.BLL.Shop.Products.ProductConsults consults = new Maticsoft.BLL.Shop.Products.ProductConsults();
            Maticsoft.Model.Shop.Products.ProductConsults model = new Maticsoft.Model.Shop.Products.ProductConsults {
                CreatedDate = DateTime.Now,
                TypeId = 0,
                Status = 0,
                UserId = base.currentUser.UserID,
                UserName = base.currentUser.NickName,
                UserEmail = base.currentUser.Email,
                IsReply = false,
                Recomend = 0,
                ProductId = num,
                ConsultationText = str
            };
            if (consults.Add(model) <= 1)
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

        public ActionResult AjaxAddFav(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                return base.Content("False");
            }
            int num = Globals.SafeInt(Fm["ProductId"], 0);
            Maticsoft.BLL.Shop.Favorite favorite = new Maticsoft.BLL.Shop.Favorite();
            if (favorite.Exists((long) num, base.currentUser.UserID, 1))
            {
                return base.Content("Rep");
            }
            Maticsoft.Model.Shop.Favorite model = new Maticsoft.Model.Shop.Favorite {
                CreatedDate = DateTime.Now,
                TargetId = num,
                Type = 1,
                UserId = base.currentUser.UserID
            };
            if (favorite.Add(model) <= 1)
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

        [HttpPost]
        public ActionResult CancelOrder(FormCollection Fm)
        {
            Maticsoft.BLL.Shop.Order.Orders orders = new Maticsoft.BLL.Shop.Order.Orders();
            long orderId = Globals.SafeLong(Fm["OrderId"], (long) 0L);
            OrderInfo model = orders.GetModel(orderId);
            if (((model != null) && (model.BuyerID == base.currentUser.UserID)) && OrderManage.CancelOrder(model, base.currentUser))
            {
                return base.Content("True");
            }
            return base.Content("False");
        }

        public ActionResult ChangePassword()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "修改密码" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        [HttpPost]
        public void CheckNickName(FormCollection collection)
        {
            JsonObject obj2 = new JsonObject();
            if ((base.HttpContext.User.Identity.IsAuthenticated && (base.CurrentUser != null)) && (base.CurrentUser.UserType != "AA"))
            {
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
            else
            {
                obj2.Accumulate("STATUS", "NOTNULL");
                base.Response.Write(obj2.ToString());
            }
        }

        [HttpPost]
        public void CheckPassword(FormCollection collection)
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

        [HttpPost]
        public void DelReceiveMsg(int MsgID)
        {
            JsonObject obj2 = new JsonObject();
            if (this.bllSM.SetReceiveMsgToDelById(MsgID) > 0)
            {
                obj2.Accumulate("STATUS", "SUCC");
            }
            else
            {
                obj2.Accumulate("STATUS", "FAIL");
            }
            base.Response.Write(obj2.ToString());
        }

        public PartialViewResult FavorList(int pageIndex = 1, string viewName = "_FavorList")
        {
            Maticsoft.BLL.Shop.Favorite favorite = new Maticsoft.BLL.Shop.Favorite();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" favo.UserId ={0} and favo.Type= {1} ", base.CurrentUser.UserID, 1);
            int pageSize = 4;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int recordCount = favorite.GetRecordCount(string.Concat(new object[] { " UserId =", base.CurrentUser.UserID, " and Type=", 1 }));
            if (recordCount < 1)
            {
                return base.PartialView(viewName);
            }
            PagedList<FavoProdModel> model = new PagedList<FavoProdModel>(favorite.GetFavoriteProductListByPage(builder.ToString(), startIndex, endIndex), pageIndex, pageSize, recordCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        public static string GetOrderType(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            Maticsoft.BLL.Shop.Order.Orders orders = new Maticsoft.BLL.Shop.Order.Orders();
            switch (orders.GetOrderType(paymentGateway, orderStatus, paymentStatus, shippingStatus))
            {
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Paying:
                    return "等待付款";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreHandle:
                    return "等待处理";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Cancel:
                    return "取消订单";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Locking:
                    return "订单锁定";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreConfirm:
                    return "等待付款确认";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Handling:
                    return "正在处理";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shipping:
                    return "配货中";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shiped:
                    return "已发货";

                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Complete:
                    return "已完成";
            }
            return "未知状态";
        }

        public string GetRuleName(string RuleAction)
        {
            Maticsoft.BLL.Members.PointsRule rule = new Maticsoft.BLL.Members.PointsRule();
            return rule.GetRuleName(RuleAction);
        }

        public ActionResult Inbox()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "收件箱" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        public PartialViewResult InboxList(int? page, string viewName = "_InboxList")
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "发件箱" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            ((dynamic) base.ViewBag).inboxpage = page;
            int pageSize = 7;
            PagedList<Maticsoft.Model.Members.SiteMessage> model = this.bllSM.GetAllReceiveMsgListByMvcPage(base.CurrentUser.UserID, pageSize, page.Value);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        public ActionResult Index()
        {
            UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
            if (usersModel == null)
            {
                return this.Redirect("/m/l/a");
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "个人中心" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(usersModel);
        }

        public ActionResult MyFavor(string viewName = "MyFavor")
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "我的收藏" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName);
        }

        public PartialViewResult OrderList(int pageIndex = 1, string viewName = "_OrderList")
        {
            Maticsoft.BLL.Shop.Order.Orders orders = new Maticsoft.BLL.Shop.Order.Orders();
            Maticsoft.BLL.Shop.Order.OrderItems items = new Maticsoft.BLL.Shop.Order.OrderItems();
            int pageSize = 8;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            totalItemCount = orders.GetRecordCount(" BuyerID=" + base.CurrentUser.UserID);
            if (totalItemCount < 1)
            {
                return base.PartialView(viewName);
            }
            List<OrderInfo> list = orders.GetListByPageEX("BuyerID=" + base.CurrentUser.UserID, "", startIndex, endIndex);
            if ((list != null) && (list.Count > 0))
            {
                foreach (OrderInfo info in list)
                {
                    info.OrderItems = items.GetModelList(" OrderId=" + info.OrderId);
                }
            }
            PagedList<OrderInfo> model = new PagedList<OrderInfo>(list, pageIndex, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        public ActionResult Orders(string viewName = "Orders")
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "我的订单-订单明细" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName);
        }

        public ActionResult Outbox(int? page)
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "发件箱" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 8;
            PagedList<Maticsoft.Model.Members.SiteMessage> model = this.bllSM.GetAllSendMsgListByMvcPage(base.CurrentUser.UserID, pageSize, page.Value);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserCenter/_OutboxList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/UserCenter/OutBox.cshtml", model);
        }

        public ActionResult Personal()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "个人资料" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
            if (usersModel != null)
            {
                return base.View(usersModel);
            }
            return this.Redirect("/m/a/l");
        }

        public ActionResult PointsDetail(int pageIndex = 1)
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "积分明细" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
            if (usersModel != null)
            {
                ((dynamic) base.ViewBag).UserInfo = usersModel.Points.HasValue ? usersModel.Points : 0;
            }
            int pageSize = 8;
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

        public ActionResult ReadMsg(int? MsgID)
        {
            if (MsgID.HasValue)
            {
                Maticsoft.Model.Members.SiteMessage modelByCache = this.bllSM.GetModelByCache(MsgID.Value);
                if (modelByCache != null)
                {
                    if (modelByCache.SenderID == -1)
                    {
                        modelByCache.SenderUserName = "管理员";
                    }
                    else
                    {
                        UsersExpModel usersExpModelByCache = null;
                        if (modelByCache.SenderID.HasValue)
                        {
                            usersExpModelByCache = this.userEXBll.GetUsersExpModelByCache(modelByCache.SenderID.Value);
                        }
                        if (usersExpModelByCache != null)
                        {
                            modelByCache.SenderUserName = usersExpModelByCache.NickName;
                        }
                    }
                    if (!modelByCache.ReceiverIsRead)
                    {
                        this.bllSM.SetReceiveMsgAlreadyRead(modelByCache.ID);
                    }
                    return base.View(modelByCache);
                }
            }
            return base.RedirectToAction("Inbox", "UserCenter");
        }

        public ActionResult RemoveFavorItem(FormCollection Fm)
        {
            if (!string.IsNullOrWhiteSpace(Fm["ItemId"]))
            {
                string text = Fm["ItemId"];
                Maticsoft.BLL.Shop.Favorite favorite = new Maticsoft.BLL.Shop.Favorite();
                int favoriteId = Globals.SafeInt(text, 0);
                if (favorite.Delete(favoriteId))
                {
                    return base.Content("Ok");
                }
            }
            return base.Content("No");
        }

        [HttpPost]
        public void ReplyMsg(int ReceiverID, string Title, string Content)
        {
            JsonObject obj2 = new JsonObject();
            Maticsoft.Model.Members.SiteMessage model = new Maticsoft.Model.Members.SiteMessage {
                Title = Title,
                Content = Content,
                SenderID = new int?(base.CurrentUser.UserID),
                ReaderIsDel = false,
                ReceiverIsRead = false,
                SenderIsDel = false,
                ReceiverID = new int?(ReceiverID),
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
            base.Response.Write(obj2.ToString());
        }

        public ActionResult SendMessage()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "发信息" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            ((dynamic) base.ViewBag).Name = base.Request.Params["name"];
            return base.View("SendMessage");
        }

        [HttpPost]
        public void SendMsg(FormCollection collection)
        {
            JsonObject obj2 = new JsonObject();
            string str = InjectionFilter.Filter(collection["NickName"]);
            string str2 = InjectionFilter.Filter(collection["Content"]);
            string str3 = InjectionFilter.Filter(collection["Content"]);
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

        [HttpPost, ValidateInput(false)]
        public void UpdateUserInfo(FormCollection collection)
        {
            JsonObject obj2 = new JsonObject();
            UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
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
            if (user.Update() && this.userEXBll.UpdateUsersExp(usersModel))
            {
                obj2.Accumulate("STATUS", "SUCC");
            }
            else
            {
                obj2.Accumulate("STATUS", "FAIL");
            }
            base.Response.Write(obj2.ToString());
        }

        [HttpPost]
        public void UpdateUserPassword(FormCollection collection)
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
}

