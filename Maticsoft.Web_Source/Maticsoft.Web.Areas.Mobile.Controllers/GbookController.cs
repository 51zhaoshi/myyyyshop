namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class GbookController : MobileControllerBase
    {
        public ActionResult AjaxGbook(FormCollection Fm)
        {
            string str = InjectionFilter.Filter(Fm["Title"]);
            string str2 = InjectionFilter.Filter(Fm["Content"]);
            string str3 = InjectionFilter.Filter(Fm["Email"]);
            if ((string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(str2)) || string.IsNullOrWhiteSpace(str3))
            {
                return base.Content("false");
            }
            Maticsoft.BLL.Members.Guestbook guestbook = new Maticsoft.BLL.Members.Guestbook();
            Maticsoft.Model.Members.Guestbook model = new Maticsoft.Model.Members.Guestbook {
                CreatedDate = DateTime.Now,
                CreatorEmail = str3,
                Description = str2,
                Title = str,
                Status = 0,
                ToUserID = -1,
                ParentID = 0,
                CreateNickName = str3
            };
            if (guestbook.Add(model) <= 0)
            {
                return base.Content("false");
            }
            return base.Content("true");
        }

        public ActionResult Index(string viewName = "Index", int top = 6)
        {
            List<Maticsoft.Model.Members.Guestbook> modelList = new Maticsoft.BLL.Members.Guestbook().GetModelList(top);
            return base.View(viewName, modelList);
        }
    }
}

