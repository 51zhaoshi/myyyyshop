namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class CommentController : MobileControllerBase
    {
        public ActionResult AjaxComment(FormCollection Fm)
        {
            int num = Globals.SafeInt(Fm["id"], 0);
            int num2 = Globals.SafeInt(Fm["typeid"], 0);
            string str = InjectionFilter.Filter(Fm["cont"]);
            string str2 = InjectionFilter.Filter(Fm["username"]);
            if (((num <= 0) || (num2 <= 0)) || (string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(str2)))
            {
                return base.Content("false");
            }
            Maticsoft.BLL.CMS.Comment comment = new Maticsoft.BLL.CMS.Comment();
            Maticsoft.Model.CMS.Comment model = new Maticsoft.Model.CMS.Comment {
                CreatedDate = DateTime.Now,
                CreatedNickName = str2,
                Description = str,
                State = false,
                ParentID = 0,
                TypeID = num2,
                IsRead = false,
                ContentId = new int?(num),
                CreatedUserID = -1
            };
            if (comment.AddTran(model) <= 0)
            {
                return base.Content("false");
            }
            return base.Content("true");
        }

        public ActionResult Index(int top = 6, int id = -1, int typeid = 3, string viewName = "Index")
        {
            ((dynamic) base.ViewBag).contentid = id;
            ((dynamic) base.ViewBag).typeid = typeid;
            List<Maticsoft.Model.CMS.Comment> model = new Maticsoft.BLL.CMS.Comment().GetModelList(top, id, typeid);
            return base.View(viewName, model);
        }
    }
}

