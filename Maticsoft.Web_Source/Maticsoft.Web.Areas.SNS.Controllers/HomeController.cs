namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class HomeController : SNSControllerBase
    {
        private Maticsoft.BLL.SNS.Groups bllGroups = new Maticsoft.BLL.SNS.Groups();
        private Maticsoft.BLL.SNS.Categories CateBll = new Maticsoft.BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.Comments ComBll = new Maticsoft.BLL.SNS.Comments();
        private Maticsoft.BLL.SNS.Photos PhotoBll = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.SNS.Products ProductBll = new Maticsoft.BLL.SNS.Products();
        private ProductQuery ProQuery = new ProductQuery();
        private Maticsoft.BLL.SNS.HotWords SearchBll = new Maticsoft.BLL.SNS.HotWords();
        private Maticsoft.BLL.SNS.TagType TagTypeBll = new Maticsoft.BLL.SNS.TagType();

        [HttpPost]
        public ActionResult AjaxAddImage(FormCollection Fm)
        {
            bool flag = true;
            Maticsoft.BLL.SNS.Posts posts = new Maticsoft.BLL.SNS.Posts();
            Maticsoft.Model.SNS.Posts post = new Maticsoft.Model.SNS.Posts();
            string str = Fm["List"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                foreach (JsonObject obj2 in JsonConvert.Import<JsonArray>(str))
                {
                    string address = obj2["ImageUrl"].ToString();
                    string str3 = InjectionFilter.Filter(obj2["ShareDec"].ToString());
                    int ablumId = Globals.SafeInt(obj2["AlbumId"].ToString(), 0);
                    post.Description = str3;
                    post.CreatedDate = DateTime.Now;
                    post.CreatedNickName = base.currentUser.NickName;
                    post.CreatedUserID = base.currentUser.UserID;
                    post.Type = 1;
                    post.UserIP = base.Request.UserHostAddress;
                    string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay");
                    string path = "";
                    string imgname = this.CreateIDCode() + ".jpg";
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        if (valueByCache != "1")
                        {
                            string str7 = "/Upload/SNS/Images/Photos/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                            string str8 = "/Upload/SNS/Images/PhotosThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                            if (!Directory.Exists(base.Server.MapPath(str7)))
                            {
                                Directory.CreateDirectory(base.Server.MapPath(str7));
                            }
                            if (!Directory.Exists(base.Server.MapPath(str8)))
                            {
                                Directory.CreateDirectory(base.Server.MapPath(str8));
                            }
                            path = str7 + imgname;
                            client.DownloadFile(address, base.Server.MapPath(path));
                        }
                        else
                        {
                            byte[] buffer = client.DownloadData(address);
                            if ((buffer == null) || (buffer.Length == 0))
                            {
                                flag = false;
                            }
                            string fileName = this.CreateIDCode() + ".jpg";
                            if (UpYunManager.UploadExecute(buffer, fileName, ApplicationKeyType.SNS, out path))
                            {
                                post.ImageUrl = path + "|" + path;
                                post = posts.AddPost(post, ablumId, -1L, 0, "", "", "", true);
                            }
                        }
                    }
                    if (valueByCache != "1")
                    {
                        string thumbImagePath = "";
                        this.MakeThumbnail(imgname, out thumbImagePath);
                        post.ImageUrl = path + "|" + thumbImagePath;
                        post = posts.AddPost(post, ablumId, -1L, 0, "", "", "", true);
                    }
                }
                if (flag)
                {
                    return base.Json(new { Data = true });
                }
            }
            return base.Json(new { Data = false });
        }

        [ChildActionOnly]
        public PartialViewResult AlbumPart(int Top = 4)
        {
            List<AlbumIndex> model = new Maticsoft.BLL.SNS.UserAlbums().GetListForIndex(0, Top, 1, base.UserAlbumDetailType);
            return this.PartialView("AlbumPart", model);
        }

        [ChildActionOnly]
        public PartialViewResult CategoryDetailPart(int topcid, string Name, int topcate = 5, int top = 12)
        {
            Maticsoft.BLL.SNS.Categories categories = new Maticsoft.BLL.SNS.Categories();
            categories.GetMenuByCategory(-1);
            ProductCategory cateListByParentIdEx = categories.GetCateListByParentIdEx(topcid);
            cateListByParentIdEx.TagsList = this.TagTypeBll.GetTagListByCid(topcid, topcate);
            ProductQuery query = new ProductQuery {
                CategoryID = new int?(topcid),
                IsTopCategory = true,
                Order = "popular"
            };
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("HomeGetValueType");
            if (valueByCache != null)
            {
                int num = 1;
                if (valueByCache == num.ToString())
                {
                    query.IsRecomend = 1;
                }
            }
            cateListByParentIdEx.CurrentCateName = Name;
            return this.PartialView("CategoryDetailPart", cateListByParentIdEx);
        }

        public PartialViewResult CMSArticle(int ClassId = 0, Maticsoft.Model.CMS.EnumHelper.ContentRec Rec = 0, int Top = 6, string ViewName = "_CMSArticle")
        {
            List<Maticsoft.Model.CMS.Content> model = new Maticsoft.BLL.CMS.Content().GetRecList(ClassId, Rec, Top, true);
            return this.PartialView(ViewName, model);
        }

        public ActionResult CollectionJS()
        {
            ((dynamic) base.ViewBag).HostName = Globals.DomainFullName;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        public string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        public PartialViewResult GroupPart(int Top = 4, string ViewName = "GroupPart")
        {
            List<Maticsoft.Model.SNS.Groups> model = this.bllGroups.GetTopList(Top, "IsRecommand=2", "TopicCount desc");
            return this.PartialView(ViewName, model);
        }

        public PartialViewResult GroupTopic(int Top = 12, string ViewName = "_GroupTopic")
        {
            List<Maticsoft.Model.SNS.GroupTopics> recTopics = new Maticsoft.BLL.SNS.GroupTopics().GetRecTopics(Top);
            return this.PartialView(ViewName, recTopics);
        }

        public PartialViewResult HotComment(int ClassId = 0, int Top = 10, string comType = "day", string ViewName = "_HotComment")
        {
            List<Maticsoft.Model.CMS.Content> hotCom = new Maticsoft.BLL.CMS.Content().GetHotCom(comType, Top);
            return this.PartialView(ViewName, hotCom);
        }

        public ActionResult Index()
        {
            if (((Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic") == "true") && System.IO.File.Exists(base.Server.MapPath("/index.html"))) && string.IsNullOrWhiteSpace(base.Request.Params["RequestType"]))
            {
                return base.File("/index.html", "text/html");
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View("Index");
        }

        public PartialViewResult IndexAd(int id = 0x26, string ViewName = "_IndexAd")
        {
            List<Advertisement> listByAidCache = new Advertisement().GetListByAidCache(id);
            return this.PartialView(ViewName, listByAidCache);
        }

        public PartialViewResult IndexUser(string ViewName = "_IndexUser")
        {
            return this.PartialView(ViewName, base.currentUser);
        }

        public void MakeThumbnail(string imgname, out string thumbImagePath)
        {
            string str = "/Upload/SNS/Images/Photos/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            string str2 = "/Upload/SNS/Images/PhotosThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, "");
            if ((thumSizeList != null) && (thumSizeList.Count > 0))
            {
                foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                {
                    ImageTools.MakeThumbnail(base.Server.MapPath(str + imgname), base.Server.MapPath(str2 + size.ThumName + imgname), size.ThumWidth, size.ThumHeight, MakeThumbnailMode.W, InterpolationMode.High, SmoothingMode.HighQuality);
                }
            }
            thumbImagePath = str2 + "{0}" + imgname;
        }

        [ChildActionOnly]
        public PartialViewResult MenuCategory(int Top = -1, string ViewName = "MenuCategory")
        {
            List<Maticsoft.Model.SNS.Categories> menuByCategory = new Maticsoft.BLL.SNS.Categories().GetMenuByCategory(Top);
            return this.PartialView(ViewName, menuByCategory);
        }

        [OutputCache(VaryByParam="none", Duration=300)]
        public PartialViewResult MoreProductPart()
        {
            List<ProductCategory> childByMenu = this.CateBll.GetChildByMenu();
            return base.PartialView(childByMenu);
        }

        [ChildActionOnly]
        public PartialViewResult PhotoPart(int Top = 12, int Type = -1, string ViewName = "PhotoPart")
        {
            List<Maticsoft.Model.SNS.Photos> topPhotoList = new Maticsoft.BLL.SNS.Photos().GetTopPhotoList(Top, Type);
            if ((topPhotoList != null) && (topPhotoList.Count > 0))
            {
                string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                foreach (Maticsoft.Model.SNS.Photos photos2 in topPhotoList)
                {
                    if (valueByCache != "true")
                    {
                        photos2.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + photos2.PhotoID);
                        continue;
                    }
                    photos2.StaticUrl = string.IsNullOrWhiteSpace(photos2.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + photos2.PhotoID)) : photos2.StaticUrl;
                }
            }
            return this.PartialView(ViewName, topPhotoList);
        }

        public PartialViewResult ProductList(int Cid = 0, int Top = 12, string ViewName = "ProductList")
        {
            ProductQuery query = new ProductQuery {
                CategoryID = new int?(Cid),
                IsTopCategory = true,
                Order = "popular"
            };
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("HomeGetValueType");
            if (valueByCache != null)
            {
                int num = 1;
                if (valueByCache == num.ToString())
                {
                    query.IsRecomend = 1;
                }
            }
            List<Maticsoft.Model.SNS.Products> model = this.ProductBll.GetProductByPage(query, 1, Top);
            if ((model != null) && (model.Count > 0))
            {
                string str2 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                foreach (Maticsoft.Model.SNS.Products products in model)
                {
                    if (str2 != "true")
                    {
                        products.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID);
                        continue;
                    }
                    products.StaticUrl = string.IsNullOrWhiteSpace(products.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID)) : products.StaticUrl;
                }
            }
            return this.PartialView(ViewName, model);
        }

        public ActionResult ScrollPost(string ViewName = "ScrollPost", int top = 10, Maticsoft.Model.SNS.EnumHelper.PostContentType PostType = 2)
        {
            List<Maticsoft.Model.SNS.Posts> scrollPost = new Maticsoft.BLL.SNS.Posts().GetScrollPost(top, PostType);
            return base.View(ViewName, scrollPost);
        }

        public ActionResult ShareImage()
        {
            string str = "";
            CollectImages model = new CollectImages();
            if (base.CurrentUser != null)
            {
                model.AlbumList = new Maticsoft.BLL.SNS.UserAlbums().GetUserAblumsByUserID(base.currentUser.UserID);
            }
            if (!string.IsNullOrWhiteSpace(base.Request.QueryString["pics[]"]))
            {
                string[] values = base.Request.QueryString.GetValues("pics[]");
                int num = 0;
                foreach (string str2 in values)
                {
                    if (num == 0)
                    {
                        str = "?pics[]=" + str2;
                    }
                    else
                    {
                        str = str + "&pics[]=" + str2;
                    }
                    ImageMessage item = new ImageMessage {
                        ImageUrl = str2.Substring(0, str2.IndexOf("----")),
                        ImageAlt = str2.Substring(str2.IndexOf("----") + 4)
                    };
                    model.ImageList.Add(item);
                    num++;
                }
            }
            base.Session["ReturnUrl"] = (((dynamic) base.ViewBag).BasePath + "Home/ShareImage") + str;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(model);
        }

        public PartialViewResult StarRec(int StarType = 0, int Top = 4, string ViewName = "StarRec")
        {
            List<Maticsoft.ViewModel.SNS.StarRank> starRankList = new Maticsoft.BLL.SNS.StarRank().GetStarRankList(StarType, Top);
            return this.PartialView(ViewName, starRankList);
        }

        public PartialViewResult TagsList(int Cid, int Top = -1, string ViewName = "TagsList")
        {
            List<CType> tagListByCid = this.TagTypeBll.GetTagListByCid(Cid, Top);
            ((dynamic) base.ViewBag).CurrentCid = Cid;
            return this.PartialView(ViewName, tagListByCid);
        }

        public PartialViewResult UserBlog(int Top = 8, string ViewName = "_UserBlog")
        {
            List<Maticsoft.Model.SNS.UserBlog> recBlogList = new Maticsoft.BLL.SNS.UserBlog().GetRecBlogList(Top);
            return this.PartialView(ViewName, recBlogList);
        }

        public PartialViewResult UseTry()
        {
            return base.PartialView("UseTry");
        }
    }
}

