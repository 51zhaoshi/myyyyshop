namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Pay;
    using Maticsoft.BLL.Shop;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Shipping;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.DEncrypt;
    using Maticsoft.Components;
    using Maticsoft.Components.Setting;
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.Pay;
    using Maticsoft.Model.Shop;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Model;
    using Maticsoft.ViewModel.UserCenter;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class UserCenterController : ShopControllerBaseUser
    {
        private readonly Maticsoft.BLL.Shop.Order.Orders _orderManage = new Maticsoft.BLL.Shop.Order.Orders();
        private readonly Maticsoft.BLL.Pay.BalanceDetails balanDetaBll = new Maticsoft.BLL.Pay.BalanceDetails();
        private readonly Maticsoft.BLL.Pay.BalanceDrawRequest balanDrawBll = new Maticsoft.BLL.Pay.BalanceDrawRequest();
        private readonly Maticsoft.BLL.Members.SiteMessage bllSM = new Maticsoft.BLL.Members.SiteMessage();
        private readonly Maticsoft.BLL.Members.PointsDetail detailBll = new Maticsoft.BLL.Members.PointsDetail();
        private readonly Maticsoft.BLL.Members.UserInvite inviteBll = new Maticsoft.BLL.Members.UserInvite();
        private readonly Maticsoft.BLL.Pay.RechargeRequest rechargeBll = new Maticsoft.BLL.Pay.RechargeRequest();
        private readonly Maticsoft.BLL.Members.UserBind userBind = new Maticsoft.BLL.Members.UserBind();
        private readonly UsersExp userEXBll = new UsersExp();

        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                return base.Content("False");
            }
            int num = Globals.SafeInt(Fm["ProductId"], 0);
            string productName = Fm["ProductName"];
            string str2 = InjectionFilter.SqlFilter(Fm["Content"]);
            Maticsoft.BLL.Shop.Products.ProductReviews reviews = new Maticsoft.BLL.Shop.Products.ProductReviews();
            Maticsoft.Model.Shop.Products.ProductReviews model = new Maticsoft.Model.Shop.Products.ProductReviews {
                CreatedDate = DateTime.Now,
                Status = 0,
                UserId = base.CurrentUser.UserID,
                UserName = base.CurrentUser.NickName,
                UserEmail = base.CurrentUser.Email,
                ParentId = 0,
                ProductId = num,
                ReviewText = str2
            };
            bool boolValueByCache = ConfigSystem.GetBoolValueByCache("Shop_Create_Post");
            if (!reviews.AddEx(model, productName, boolValueByCache))
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

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
                UserId = base.CurrentUser.UserID,
                UserName = base.CurrentUser.NickName,
                UserEmail = base.CurrentUser.Email,
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
            if (favorite.Exists((long) num, base.CurrentUser.UserID, 1))
            {
                return base.Content("Rep");
            }
            Maticsoft.Model.Shop.Favorite model = new Maticsoft.Model.Shop.Favorite {
                CreatedDate = DateTime.Now,
                TargetId = num,
                Type = 1,
                UserId = base.CurrentUser.UserID
            };
            if (favorite.Add(model) <= 1)
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

        public ActionResult AjaxDraw(FormCollection Fm)
        {
            decimal num = Globals.SafeDecimal(Fm["Amount"], (decimal) 0M);
            int num2 = Globals.SafeInt(Fm["Type"], -1);
            string str = InjectionFilter.Filter(Fm["BankCard"]);
            if (((num > 0M) && (num2 > 0)) && !string.IsNullOrWhiteSpace(str))
            {
                string str2 = "";
                string str3 = "";
                if (num2 == 1)
                {
                    str2 = InjectionFilter.Filter(Fm["TrueName"]);
                    str3 = InjectionFilter.Filter(Fm["BankName"]);
                    if (string.IsNullOrWhiteSpace(str2) || string.IsNullOrWhiteSpace(str3))
                    {
                        return base.Content("no");
                    }
                }
                if (num <= this.balanDrawBll.GetBalanceDraw(base.CurrentUser.UserID))
                {
                    Maticsoft.Model.Pay.BalanceDrawRequest model = new Maticsoft.Model.Pay.BalanceDrawRequest {
                        Amount = num,
                        BankCard = str,
                        CardTypeID = num2,
                        RequestStatus = 1,
                        RequestTime = DateTime.Now
                    };
                    if (num2 == 1)
                    {
                        model.BankName = str3;
                        model.TrueName = str2;
                    }
                    model.UserID = base.CurrentUser.UserID;
                    if (this.balanDrawBll.AddEx(model))
                    {
                        return base.Content("ok");
                    }
                }
            }
            return base.Content("no");
        }

        [HttpPost]
        public ActionResult AjAxPReview(FormCollection fm)
        {
            string str = fm["PReviewjson"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                int num4;
                Maticsoft.BLL.Shop.Products.ProductReviews reviews = new Maticsoft.BLL.Shop.Products.ProductReviews();
                List<Maticsoft.Model.Shop.Products.ProductReviews> modelList = new List<Maticsoft.Model.Shop.Products.ProductReviews>();
                JsonArray array = JsonConvert.Import<JsonArray>(str);
                long orderId = -1L;
                foreach (JsonObject obj2 in array)
                {
                    long num2 = Globals.SafeInt(obj2["pid"].ToString(), -1);
                    orderId = Globals.SafeInt(obj2["orderId"].ToString(), -1);
                    string str2 = InjectionFilter.Filter(obj2["contentval"].ToString());
                    string str3 = Globals.SafeString(obj2["imagesurlPath"].ToString(), "");
                    string str4 = Globals.SafeString(obj2["imagesurlName"].ToString(), "");
                    string str5 = InjectionFilter.Filter(obj2["attribute"].ToString());
                    string str6 = InjectionFilter.Filter(obj2["sku"].ToString());
                    if (((num2 <= 0L) || (orderId <= 0L)) || string.IsNullOrWhiteSpace(str2))
                    {
                        return base.Content("false");
                    }
                    Maticsoft.Model.Shop.Products.ProductReviews item = new Maticsoft.Model.Shop.Products.ProductReviews {
                        Attribute = str5,
                        CreatedDate = DateTime.Now,
                        OrderId = orderId,
                        ProductId = num2,
                        ReviewText = str2,
                        SKU = str6,
                        Status = 0,
                        UserEmail = base.currentUser.Email,
                        UserId = base.currentUser.UserID,
                        UserName = base.currentUser.UserName,
                        ParentId = 0
                    };
                    if (!string.IsNullOrWhiteSpace(str3) && !string.IsNullOrWhiteSpace(str4))
                    {
                        string virtualPath = string.Format("/Upload/Shop/ProductReviews/{0}/", DateTime.Now.ToString("yyyyMM"));
                        string path = base.Request.MapPath(virtualPath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string[] strArray = str3.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] strArray2 = str4.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        if (strArray.Length != strArray2.Length)
                        {
                            throw new ArgumentOutOfRangeException("路径与文件名长度不匹配！");
                        }
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            File.Move(base.Request.MapPath(strArray[i]), path + strArray2[i]);
                        }
                        item.ImagesPath = virtualPath + "{0}";
                        item.ImagesNames = string.Join("|", strArray2);
                    }
                    modelList.Add(item);
                }
                if ((modelList.Count > 0) && reviews.AddEx(modelList, orderId, out num4))
                {
                    return base.Content(num4.ToString());
                }
            }
            return base.Content("false");
        }

        public ActionResult AjaxRecharge(FormCollection Fm)
        {
            if (!string.IsNullOrWhiteSpace(Fm["rechargmoney"]) && !string.IsNullOrWhiteSpace(Fm["payid"]))
            {
                int modeId = Globals.SafeInt(Fm["payid"], 0);
                decimal num2 = Globals.SafeDecimal(Fm["rechargmoney"], (decimal) 0M);
                if ((modeId > 0) && (num2 > 0M))
                {
                    Maticsoft.Model.Pay.RechargeRequest model = new Maticsoft.Model.Pay.RechargeRequest();
                    PaymentModeInfo paymentModeById = PaymentModeManage.GetPaymentModeById(modeId);
                    if (paymentModeById == null)
                    {
                        return base.Content("No");
                    }
                    model.PaymentGateway = paymentModeById.Gateway;
                    model.PaymentTypeId = modeId;
                    model.RechargeBlance = num2;
                    model.Status = 0;
                    model.TradeDate = DateTime.Now;
                    model.Tradetype = 1;
                    model.UserId = base.CurrentUser.UserID;
                    long num3 = this.rechargeBll.Add(model);
                    if (num3 > 0L)
                    {
                        return base.Content(num3.ToString());
                    }
                }
            }
            return base.Content("No");
        }

        public ActionResult Balance(string viewName = "Balance")
        {
            ((dynamic) base.ViewBag).Activity = base.CurrentUser.Activity ? "有效" : "无效";
            ((dynamic) base.ViewBag).Balance = this.userEXBll.GetUserBalance(base.CurrentUser.UserID);
            ((dynamic) base.ViewBag).BalanceDraw = this.balanDrawBll.GetBalanceDraw(base.CurrentUser.UserID);
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "账户余额" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName);
        }

        public PartialViewResult BalanceDetList(int pageIndex = 1, string viewName = "_BalanceDetList")
        {
            int pageSize = 10;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int recordCount = this.balanDetaBll.GetRecordCount(" UserId =" + base.CurrentUser.UserID);
            if (recordCount < 1)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Pay.BalanceDetails> model = new PagedList<Maticsoft.Model.Pay.BalanceDetails>(this.balanDetaBll.GetListByPage(" UserId = " + base.CurrentUser.UserID, startIndex, endIndex), pageIndex, pageSize, recordCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
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

        [HttpPost]
        public ActionResult CancelOrder(FormCollection Fm)
        {
            Maticsoft.BLL.Shop.Order.Orders orders = new Maticsoft.BLL.Shop.Order.Orders();
            long orderId = Globals.SafeLong(Fm["OrderId"], (long) 0L);
            Maticsoft.Model.Shop.Order.OrderInfo model = orders.GetModel(orderId);
            if (((model != null) && (model.BuyerID == base.CurrentUser.UserID)) && OrderManage.CancelOrder(model, base.currentUser))
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

        [HttpPost]
        public ActionResult DelShippAddress(int id)
        {
            if (id < 1)
            {
                return base.Content("NOID");
            }
            Maticsoft.BLL.Shop.Shipping.ShippingAddress address = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = address.GetModel(id);
            if ((model != null) && (base.CurrentUser.UserID == model.UserId))
            {
                return base.Content(address.Delete(id) ? "OK" : "NO");
            }
            return base.Content("ERROR");
        }

        public ActionResult Draw(string viewName = "Draw")
        {
            ((dynamic) base.ViewBag).Balance = this.userEXBll.GetUserBalance(base.CurrentUser.UserID);
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "申请提现" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName);
        }

        public PartialViewResult DrawDetList(int pageIndex = 1, string viewName = "_DrawDetList")
        {
            int pageSize = 10;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int recordCount = this.balanDrawBll.GetRecordCount(" UserId =" + base.CurrentUser.UserID);
            if (recordCount < 1)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Pay.BalanceDrawRequest> model = new PagedList<Maticsoft.Model.Pay.BalanceDrawRequest>(this.balanDrawBll.GetListByPage(" UserId= " + base.CurrentUser.UserID, startIndex, endIndex), pageIndex, pageSize, recordCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
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

        public PartialViewResult FavorList(int pageIndex = 1, string viewName = "_FavorList")
        {
            Maticsoft.BLL.Shop.Favorite favorite = new Maticsoft.BLL.Shop.Favorite();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" favo.UserId ={0} and favo.Type= {1} ", base.CurrentUser.UserID, 1);
            int pageSize = 10;
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

        public ActionResult Gravatar()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "修改头像" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
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
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "修改头像" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            try
            {
                if (base.CurrentUser != null)
                {
                    UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
                    if (usersModel != null)
                    {
                        usersModel.Gravatar = collection["Gravatar"];
                        if (this.userEXBll.UpdateUsersExp(usersModel))
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
            PagedList<Maticsoft.Model.Members.SiteMessage> model = this.bllSM.GetAllReceiveMsgListByMvcPage(base.CurrentUser.UserID, -1, pageSize, page.Value);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        public ActionResult Index(string viewName = "Index")
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).WebName = set.WebName;
            UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
            if (usersModel == null)
            {
                return base.RedirectToAction("Login", "Account");
            }
            ((dynamic) base.ViewBag).privatecount = new Maticsoft.BLL.Members.SiteMessage().GetReceiveMsgNotReadCount(base.CurrentUser.UserID, -1);
            ((dynamic) base.ViewBag).Unpaid = new Maticsoft.BLL.Shop.Order.Orders().GetPaymentStatusCounts(base.CurrentUser.UserID, 0);
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "个人中心" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName, usersModel);
        }

        public PartialViewResult InviteList(int pageIndex = 1, string viewName = "_InviteList")
        {
            int pageSize = 5;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int recordCount = this.inviteBll.GetRecordCount(" InviteUserId=" + base.CurrentUser.UserID);
            if (recordCount < 1)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Members.UserInvite> model = new PagedList<Maticsoft.Model.Members.UserInvite>(this.inviteBll.GetListByPage(" InviteUserId=" + base.CurrentUser.UserID, startIndex, endIndex), pageIndex, pageSize, recordCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        public ActionResult LeftMenu(string viewName = "_LeftMenu")
        {
            ((dynamic) base.ViewBag).privatecount = new Maticsoft.BLL.Members.SiteMessage().GetReceiveMsgNotReadCount(base.CurrentUser.UserID, -1);
            return base.View(viewName);
        }

        public ActionResult MyFavor(string viewName = "MyFavor")
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "我的收藏" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName);
        }

        public ActionResult MyInvite()
        {
            new Content();
            ((dynamic) base.ViewBag).Url = "Account/Register/" + Hex16.Encode(base.CurrentUser.UserID.ToString());
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).WebName = set.WebName;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "我的邀请" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        public PartialViewResult OrderAction(long OrderId = -1L, string viewName = "_ActionList")
        {
            List<Maticsoft.Model.Shop.Order.OrderAction> modelList = new Maticsoft.BLL.Shop.Order.OrderAction().GetModelList(" OrderId=" + OrderId);
            return this.PartialView(viewName, modelList);
        }

        public ActionResult OrderInfo(long id = -1L, string viewname = "OrderInfo")
        {
            Maticsoft.Model.Shop.Order.OrderInfo modelInfo = this._orderManage.GetModelInfo(id);
            if ((modelInfo == null) || (modelInfo.BuyerID != base.CurrentUser.UserID))
            {
                return (ActionResult) this.Redirect(((dynamic) base.ViewBag).BasePath + "UserCenter/Orders");
            }
            if (modelInfo.OrderStatus == -1)
            {
                viewname = "OrderCanceled";
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "查看订单详细信息" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewname, modelInfo);
        }

        public PartialViewResult OrderList(int pageIndex = 1, string viewName = "_OrderList")
        {
            Action<Maticsoft.Model.Shop.Order.OrderInfo> action = null;
            Maticsoft.BLL.Shop.Order.Orders orders = new Maticsoft.BLL.Shop.Order.Orders();
            Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();
            int pageSize = 10;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            string strWhere = " BuyerID=" + base.CurrentUser.UserID + " AND OrderType=1";
            totalItemCount = orders.GetRecordCount(strWhere);
            if (totalItemCount < 1)
            {
                return base.PartialView(viewName);
            }
            List<Maticsoft.Model.Shop.Order.OrderInfo> items = orders.GetListByPageEX(strWhere, "", startIndex, endIndex);
            if ((items != null) && (items.Count > 0))
            {
                foreach (Maticsoft.Model.Shop.Order.OrderInfo info in items)
                {
                    if (info.HasChildren && (info.PaymentStatus > 1))
                    {
                        info.SubOrders = orders.GetModelList(" ParentOrderId=" + info.OrderId);
                        if (action == null)
                        {
                            action = delegate (Maticsoft.Model.Shop.Order.OrderInfo info) {
                                info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId);
                            };
                        }
                        info.SubOrders.ForEach(action);
                        continue;
                    }
                    info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId);
                }
            }
            PagedList<Maticsoft.Model.Shop.Order.OrderInfo> model = new PagedList<Maticsoft.Model.Shop.Order.OrderInfo>(items, pageIndex, pageSize, totalItemCount);
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
            int pageSize = 10;
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
            if ((base.HttpContext.User.Identity.IsAuthenticated && (base.CurrentUser != null)) && (base.CurrentUser.UserType != "AA"))
            {
                UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
                if (usersModel != null)
                {
                    return base.View(usersModel);
                }
            }
            return base.RedirectToAction("Login", "Account");
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

        public ActionResult PReview(long id = -1L, string viewName = "PReview")
        {
            Maticsoft.Model.Shop.Order.OrderInfo modelInfo = this._orderManage.GetModelInfo(id);
            if (((modelInfo == null) || (modelInfo.BuyerID != base.CurrentUser.UserID)) || (modelInfo.IsReviews || (modelInfo.OrderStatus != 2)))
            {
                return (ActionResult) this.Redirect(((dynamic) base.ViewBag).BasePath + "UserCenter/Orders");
            }
            List<Maticsoft.Model.Shop.Order.OrderItems> orderItems = modelInfo.OrderItems;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "评论" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName, orderItems);
        }

        public ActionResult ReadMsg(int? MsgID)
        {
            if (MsgID.HasValue)
            {
                Maticsoft.Model.Members.SiteMessage modelByCache = this.bllSM.GetModelByCache(MsgID.Value);
                if (modelByCache != null)
                {
                    if (!modelByCache.ReceiverIsRead)
                    {
                        this.bllSM.SetReceiveMsgAlreadyRead(modelByCache.ID);
                    }
                    return base.View(modelByCache);
                }
            }
            return base.RedirectToAction("Inbox");
        }

        public ActionResult Recharge(string viewName = "Recharge")
        {
            ((dynamic) base.ViewBag).UserName = base.CurrentUser.UserName;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "账户充值" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            List<PaymentModeInfo> model = (from o in PaymentModeManage.GetPaymentModes()
                where o.AllowRecharge
                select o).ToList<PaymentModeInfo>();
            return base.View(viewName, model);
        }

        public ActionResult RechargeConfirm(int? id, string viewName = "RechargeConfirm")
        {
            if (id.HasValue)
            {
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
                ((dynamic) base.ViewBag).Title = pageSetting.Title;
                ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
                ((dynamic) base.ViewBag).Description = pageSetting.Description;
                Maticsoft.Model.Pay.RechargeRequest modelByCache = this.rechargeBll.GetModelByCache((long) id.Value);
                if (modelByCache != null)
                {
                    ((dynamic) base.ViewBag).RechargeId = id;
                    ((dynamic) base.ViewBag).RechargeBlance = modelByCache.RechargeBlance;
                    return base.View(viewName);
                }
            }
            return (ActionResult) this.Redirect(((dynamic) base.ViewBag).BasePath + "UserCenter/Recharge");
        }

        public PartialViewResult RechargeList(int pageIndex = 1, string viewName = "_RechargeList")
        {
            int pageSize = 10;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int recordCount = this.rechargeBll.GetRecordCount(" UserId =" + base.CurrentUser.UserID);
            if (recordCount < 1)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Pay.RechargeRequest> model = new PagedList<Maticsoft.Model.Pay.RechargeRequest>(this.rechargeBll.GetRechargeListByPage(" UserId= " + base.CurrentUser.UserID, startIndex, endIndex), pageIndex, pageSize, recordCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
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

        public ActionResult ShippAddress(int id = -1, string viewName = "ShippAddress")
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "我的收货地址" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            Maticsoft.BLL.Shop.Shipping.ShippingAddress address = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = new Maticsoft.Model.Shop.Shipping.ShippingAddress();
            if (id > 0)
            {
                model = address.GetModel(id);
            }
            return base.View(viewName, model);
        }

        public ActionResult ShippAddressList(string viewName = "ShippAddressList")
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "我的收货地址" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            List<Maticsoft.Model.Shop.Shipping.ShippingAddress> modelList = new Maticsoft.BLL.Shop.Shipping.ShippingAddress().GetModelList(" UserId=" + base.CurrentUser.UserID);
            return base.View(viewName, modelList);
        }

        [HttpPost]
        public ActionResult SubmitShippAddress(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            if ((base.CurrentUser != null) && (model != null))
            {
                Maticsoft.BLL.Shop.Shipping.ShippingAddress address = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
                if (model.ShippingId > 0)
                {
                    if (address.Update(model))
                    {
                        return base.Content("OK");
                    }
                    return base.Content("NO");
                }
                model.UserId = base.CurrentUser.UserID;
                model.ShippingId = address.Add(model);
                if (model.ShippingId > 1)
                {
                    return base.Content("OK");
                }
            }
            return base.Content("NO");
        }

        public ActionResult SysInfo(int? page)
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "系统消息" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            PagedList<Maticsoft.Model.Members.SiteMessage> model = this.bllSM.GetAllSystemMsgListByMvcPage(base.CurrentUser.UserID, -1, base.CurrentUser.UserType, pageSize, page.Value);
            foreach (Maticsoft.Model.Members.SiteMessage message in model)
            {
                if (!message.ReceiverIsRead)
                {
                    this.bllSM.SetSystemMsgStateToAlreadyRead(message.ID, base.CurrentUser.UserID, base.CurrentUser.UserType);
                }
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserCenter/_SysInfoList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/UserCenter/SysInfo.cshtml", model);
        }

        public ActionResult TestWeiBo()
        {
            return base.View();
        }

        [HttpPost, ValidateInput(false)]
        public void UpdateUserInfo(FormCollection collection)
        {
            if ((!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null)) || (base.CurrentUser.UserType == "AA"))
            {
                this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                UsersExpModel usersModel = this.userEXBll.GetUsersModel(base.CurrentUser.UserID);
                if (usersModel == null)
                {
                    base.RedirectToAction("Login", "Account");
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

        public ActionResult UserBind()
        {
            ((dynamic) base.ViewBag).Title = "会员中心—帐号绑定";
            UserBindList listEx = this.userBind.GetListEx(base.CurrentUser.UserID);
            return base.View(listEx);
        }
    }
}

