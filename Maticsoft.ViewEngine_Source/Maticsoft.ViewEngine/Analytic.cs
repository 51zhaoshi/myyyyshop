namespace Maticsoft.ViewEngine
{
    using Maticsoft.Components;
    using Maticsoft.Web;
    using System;
    using System.Web.Mvc;

    internal static class Analytic
    {
        private static string _productInfo;
        private static readonly string DATA = Hex16.Decode("0050006F007700650072006500640020006200790020003C006100200068007200650066003D00220068007400740070003A002F002F007700770077002E006D00610074006900630073006F00660074002E0063006F006D002F00220020007400610072006700650074003D0022005F0062006C0061006E006B00220020007300740079006C0065003D00220063006F006C006F0072003A00200023003300330033003B00220020003E007B0034007D003C002F0061003E0020007B0030007D002000A900200032003000310031002D007B0033007D0020004D00610074006900630073006F0066007400200049006E0063002E");

        internal static void CreateBegin(ControllerContext controllerContext, AreaRoute areaRoute)
        {
            string str2;
            string str = controllerContext.RouteData.Values["action"] as string;
            if (((str2 = str) != null) && (str2 == "Footer"))
            {
                switch (areaRoute)
                {
                    case AreaRoute.CMS:
                    case AreaRoute.SNS:
                        controllerContext.HttpContext.Response.Output.WriteLine("<div id='footer' class='footer'>");
                        return;

                    case AreaRoute.Shop:
                        controllerContext.HttpContext.Response.Output.WriteLine("<div id='ft' >");
                        return;
                }
            }
        }

        internal static void CreateEnd(ControllerContext controllerContext, AreaRoute areaRoute)
        {
            string str2;
            string str = controllerContext.RouteData.Values["action"] as string;
            if (((str2 = str) != null) && (str2 == "Footer"))
            {
                if (string.IsNullOrEmpty(_productInfo))
                {
                    _productInfo = MvcApplication.ProductInfo.Replace(" ", "");
                }
                switch (areaRoute)
                {
                    case AreaRoute.CMS:
                        controllerContext.HttpContext.Response.Output.WriteLine("<div class=\"bot_menu\">{0}<br/>{1}</div>", MvcApplication.IsAuthorize ? string.Format("{0} {1}", MvcApplication.WebPowerBy, MvcApplication.WebRecord) : string.Format("<div class=\"bot_menu\" >" + DATA + "</div><div class=\"bot_menu\"> {1} {2}</div>", new object[] { MvcApplication.Version, MvcApplication.WebPowerBy, MvcApplication.WebRecord, DateTime.Now.Year, _productInfo }), MvcApplication.PageFootJs);
                        return;

                    case AreaRoute.Shop:
                        controllerContext.HttpContext.Response.Output.WriteLine("<div class='copyright'>{0}<br/>{1}</div>", MvcApplication.IsAuthorize ? string.Format("<p> <span class='mr15'>{0}</span> <span>{1}</span></p>", MvcApplication.WebPowerBy, MvcApplication.WebRecord) : string.Format("<p> <span class='mr15'>{1}</span> <span>{2}</span></p><p><span class='mr15'>" + DATA + "</span></p>", new object[] { MvcApplication.Version, MvcApplication.WebPowerBy, MvcApplication.WebRecord, DateTime.Now.Year, _productInfo }), MvcApplication.PageFootJs);
                        return;

                    case AreaRoute.SNS:
                        controllerContext.HttpContext.Response.Output.WriteLine("<div class='clear'></div><div class='footer_bot' style='margin-top: -23px;margin-bottom: 23px'>{0}<br/>{1}<div class='clear'></div></div></div>", MvcApplication.IsAuthorize ? string.Format("{0}<br/>{1}", MvcApplication.WebPowerBy, MvcApplication.WebRecord) : string.Format("<div style=\"float: left;margin-left: 33px;text-align: left;\">" + DATA + "</div><div style=\"float: right;margin-right: 33px;text-align: right;\"> {1} <br /> {2}</div>", new object[] { MvcApplication.Version + "<br/>", MvcApplication.WebPowerBy, MvcApplication.WebRecord, DateTime.Now.Year, _productInfo }), MvcApplication.PageFootJs);
                        return;
                }
            }
        }
    }
}

