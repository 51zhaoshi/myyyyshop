namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.CMS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class ArticleController : MobileControllerBase
    {
        private Maticsoft.BLL.CMS.ContentClass classContBll = new Maticsoft.BLL.CMS.ContentClass();
        private Maticsoft.BLL.CMS.Content contBll = new Maticsoft.BLL.CMS.Content();

        public ActionResult AjaxSupport(FormCollection Fm)
        {
            int contentID = Globals.SafeInt(Fm["contid"], 0);
            if (contentID <= 0)
            {
                return base.Content("false");
            }
            if (!this.contBll.UpdateTotalSupport(contentID))
            {
                return base.Content("false");
            }
            return base.Content("true");
        }

        public ActionResult ArticleList(int? classid)
        {
            if (classid.HasValue)
            {
                Maticsoft.Model.CMS.ContentClass modelByCache = this.classContBll.GetModelByCache(classid.Value);
                if (modelByCache != null)
                {
                    IPageSetting setting = PageSetting.GetContentClassSetting(modelByCache, "CMSSelf", ApplicationKeyType.CMS);
                    ((dynamic) base.ViewBag).Title = setting.Title;
                    ((dynamic) base.ViewBag).Keywords = setting.Keywords;
                    ((dynamic) base.ViewBag).Description = setting.Description;
                    List<Maticsoft.Model.CMS.Content> modelList = this.contBll.GetModelList(classid.Value, 0);
                    return base.View(modelList);
                }
            }
            return base.RedirectToAction("Index", "Home", "Mobile");
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                int contentID = id.Value;
                Maticsoft.Model.CMS.Content modelByCache = this.contBll.GetModelByCache(contentID);
                if (modelByCache != null)
                {
                    IPageSetting setting = PageSetting.GetArticleSetting(modelByCache, "CMS", ApplicationKeyType.CMS);
                    ((dynamic) base.ViewBag).Title = setting.Title;
                    ((dynamic) base.ViewBag).Keywords = setting.Keywords;
                    ((dynamic) base.ViewBag).Description = setting.Description;
                    int prevID = this.contBll.GetPrevID(contentID, modelByCache.ClassID);
                    int nextID = this.contBll.GetNextID(contentID, modelByCache.ClassID);
                    this.contBll.UpdatePV(contentID);
                    ((dynamic) base.ViewBag).AClassName = this.classContBll.GetAClassnameById(modelByCache.ClassID);
                    ((dynamic) base.ViewBag).PreUrl = (prevID > 0) ? ("/m/Article/Details/" + prevID) : "#";
                    ((dynamic) base.ViewBag).NextUrl = (nextID > 0) ? ("/m/Article/Details/" + nextID) : "#";
                    return base.View(modelByCache);
                }
            }
            return base.RedirectToAction("ArticleList", "Article", "Mobile");
        }

        public ActionResult News(int? classID, string viewName = "News", int topclass = 3)
        {
            string str;
            List<Maticsoft.Model.CMS.ContentClass> model = this.classContBll.GetModelList(topclass, classID, out str);
            ((dynamic) base.ViewBag).ClassName = str;
            ((dynamic) base.ViewBag).classID = classID;
            ((dynamic) base.ViewBag).tel = ConfigSystem.GetValueByCache("CompanyTelephone");
            return base.View(viewName, model);
        }

        public PartialViewResult NewsContent(int top, int classID, string imageurl = "", string viewName = "_NewsContent", string cname = "")
        {
            List<Maticsoft.Model.CMS.Content> model = this.contBll.GetModelList(classID, top, imageurl);
            ((dynamic) base.ViewBag).ClassName = cname;
            return this.PartialView(viewName, model);
        }

        public ActionResult NewsDetail(int? ContID, string viewName = "NewsDetail")
        {
            string className = "";
            Maticsoft.Model.CMS.Content modelByCache = this.contBll.GetModelByCache(ContID, out className);
            ((dynamic) base.ViewBag).ClassName = className;
            ((dynamic) base.ViewBag).tel = ConfigSystem.GetValueByCache("CompanyTelephone");
            int aclassid = -1;
            if (modelByCache != null)
            {
                ((dynamic) base.ViewBag).AclassName = this.classContBll.GetAClassnameById(modelByCache.ClassID, out aclassid);
                ((dynamic) base.ViewBag).AclassID = aclassid;
            }
            return base.View(viewName, modelByCache);
        }

        public ActionResult NewsList(int classID = -1, int pageIndex = 1, string viewName = "NewsList")
        {
            ((dynamic) base.ViewBag).classID = classID;
            ((dynamic) base.ViewBag).classname = this.classContBll.GetClassnameById(classID);
            int aclassid = -1;
            ((dynamic) base.ViewBag).AclassName = this.classContBll.GetAClassnameById(classID, out aclassid);
            ((dynamic) base.ViewBag).AclassID = aclassid;
            ((dynamic) base.ViewBag).newslistaction = "NewsList";
            int pageSize = 6;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int recordCount = this.contBll.GetRecordCount("  State=0  and ClassID= " + classID);
            ((dynamic) base.ViewBag).pageIndex = pageIndex;
            ((dynamic) base.ViewBag).totalPage = (int) Math.Ceiling((double) (((double) recordCount) / ((double) pageSize)));
            if (recordCount < 1)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.Model.CMS.Content> model = new PagedList<Maticsoft.Model.CMS.Content>(this.contBll.GetListByPage(classID, startIndex, endIndex), pageIndex, pageSize, recordCount);
            return base.View(viewName, model);
        }

        public ActionResult NewsListTwo(int classID = -1, int pageIndex = 1, int topclass = 3, string imageurl = "", string viewName = "NewsList")
        {
            int aclassid = -1;
            ((dynamic) base.ViewBag).AclassName = this.classContBll.GetAClassnameById(classID, out aclassid);
            ((dynamic) base.ViewBag).AclassID = aclassid;
            ((dynamic) base.ViewBag).classID = classID;
            ((dynamic) base.ViewBag).classname = this.classContBll.GetClassnameById(classID);
            ((dynamic) base.ViewBag).topclass = topclass;
            ((dynamic) base.ViewBag).newslistaction = "NewsListTwo";
            int pageSize = 6;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            PagedList<Maticsoft.Model.CMS.Content> model = new PagedList<Maticsoft.Model.CMS.Content>(this.contBll.GetListByPage(classID, startIndex, endIndex, imageurl, topclass, out totalItemCount), pageIndex, pageSize, totalItemCount);
            ((dynamic) base.ViewBag).pageIndex = pageIndex;
            ((dynamic) base.ViewBag).totalPage = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            return base.View(viewName, model);
        }

        public ActionResult SingleArticleDetail(int classID = -1, string viewName = "NewsDetail")
        {
            string className = "";
            if (classID <= 0)
            {
                return base.View(viewName);
            }
            Maticsoft.Model.CMS.Content modelByClassIDByCache = this.contBll.GetModelByClassIDByCache(classID, out className);
            ((dynamic) base.ViewBag).ClassName = className;
            ((dynamic) base.ViewBag).tel = ConfigSystem.GetValueByCache("CompanyTelephone");
            return base.View(viewName, modelByClassIDByCache);
        }
    }
}

