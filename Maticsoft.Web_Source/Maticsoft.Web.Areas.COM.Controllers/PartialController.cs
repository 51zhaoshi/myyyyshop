namespace Maticsoft.Web.Areas.COM.Controllers
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class PartialController : Maticsoft.Web.Controllers.ControllerBase
    {
        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult PollOptions(int? topicId, bool isCheckBox)
        {
            if (!topicId.HasValue)
            {
                return base.View();
            }
            List<Options> modelList = new Options().GetModelList(string.Format("TopicID={0}", topicId.Value));
            if ((modelList == null) || (modelList.Count <= 0))
            {
                return base.View();
            }
            ((dynamic) base.ViewBag).IsCheckBox = isCheckBox;
            return base.View(modelList);
        }
    }
}

