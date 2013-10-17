namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;

    public class RssController : CMSControllerBase
    {
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);

        public ActionResult List()
        {
            if (this.WebSiteSet == null)
            {
                return base.View();
            }
            SyndicationFeed feed = new SyndicationFeed(Globals.HtmlDecode(this.WebSiteSet.WebTitle) + "-" + Globals.HtmlDecode(this.WebSiteSet.WebName), Globals.HtmlDecode(this.WebSiteSet.Description), new Uri(Globals.HtmlDecode(this.WebSiteSet.BaseHost)), "Maticsoft", DateTime.Now);
            DataSet rssList = new Content().GetRssList();
            if (!DataSetTools.DataSetIsNull(rssList))
            {
                List<SyndicationItem> list = new List<SyndicationItem>();
                foreach (DataRow row in rssList.Tables[0].Rows)
                {
                    string id = Globals.SafeString(row["ContentID"], "");
                    SyndicationItem item = new SyndicationItem(Globals.HtmlDecode(Globals.SafeString(row["Title"], "")), Globals.SafeString(row["Description"], ""), new Uri(string.Format(Globals.HtmlDecode(this.WebSiteSet.BaseHost) + "/Article/Details/{0}", id)), id, Globals.SafeDateTime(Globals.SafeString(row["CreatedDate"], ""), DateTime.Now));
                    list.Add(item);
                }
                feed.Items = list;
            }
            return new RssActionResult { Feed = feed };
        }
    }
}

