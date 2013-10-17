namespace Maticsoft.Web.Controllers
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components;
    using Maticsoft.Model.SysManage;
    using Resources;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Mvc;

    public abstract class ControllerBase : ControllerBaseAbs
    {
        protected ControllerBase() : base(new PageBaseOption())
        {
        }

        protected override void ControllerException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            string content = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">";
            content = (content + string.Format("<title>系统发生错误 MaticsoftFK {0}{1}</title>", MvcApplication.Version, MvcApplication.IsAuthorize ? "" : ControllerBaseAbs.P_DATA) + "<style>body{\tfont-family: 'Microsoft Yahei', Verdana, arial, sans-serif;\tfont-size:14px;}a{text-decoration:none;color:#174B73;}a:hover{ text-decoration:none;color:#FF6600;}h2{\tborder-bottom:1px solid #DDD;\tpadding:8px 0;    font-size:25px;}.title{\tmargin:4px 0;\tcolor:#F60;\tfont-weight:bold;}.message,#trace{\tpadding:1em;\tborder:solid 1px #000;\tmargin:10px 0;\tbackground:#FFD;\tline-height:150%;}.message{\tbackground:#FFD;\tcolor:#2E2E2E;\t\tborder:1px solid #E0E0E0;}#trace{\tbackground:#E7F7FF;\tborder:1px solid #E0E0E0;\tcolor:#535353;\tword-wrap: break-word;}.notice{    padding:10px;\tmargin:5px;\tcolor:#666;\tbackground:#FCFCFC;\tborder:1px solid #E0E0E0;}.red{\tcolor:red;\tfont-weight:bold;}</style></head>") + "<body><div class=\"notice\"><h2>系统发生错误 </h2>" + "<div>您可以选择 [ <a href=\"javascript:location.reload();\" >重试</a> ] [ <a href=\"javascript:history.back()\">返回</a> ] 或者 [ <a target=\"_blank\" href=\"http://bbs.maticsoft.com/\">去官方论坛找找答案</a> ]</div>";
            Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog();
            Exception e = filterContext.Exception;
            HttpContextBase httpContext = filterContext.HttpContext;
            httpContext.Response.Clear();
            StackTrace trace = new StackTrace(e, true);
            int fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
            int fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();
            string fileName = trace.GetFrame(0).GetFileName();
            object obj2 = content;
            content = string.Concat(new object[] { obj2, "<p><strong>错误位置:</strong>　File: <span class=\"red\">", fileName, "</span>　Line: <span class=\"red\">", fileLineNumber, "</span> Column: <span class=\"red\">", fileColumnNumber, "</span></p>" }) + "<p class=\"title\">[ 错误信息 ]</p>";
            if (e is SqlException)
            {
                SqlException exception2 = (SqlException) e;
                if (exception2 != null)
                {
                    string sqlExceptionMessage = this.GetSqlExceptionMessage(exception2.Number);
                    if (exception2.Number == 0x223)
                    {
                        content = content + "<p class=\"message\">" + sqlExceptionMessage + "</p>";
                    }
                    else
                    {
                        content = content + "<p class=\"message\">" + sqlExceptionMessage + "</p>";
                        model.Loginfo = sqlExceptionMessage;
                        model.StackTrace = e.ToString();
                        model.Url = httpContext.Request.Url.AbsoluteUri;
                    }
                }
            }
            else
            {
                content = (((content + "<p class=\"message\">" + e.Message + "</p>") + "<p class=\"title\">[ StackTrace ]</p><p id=\"trace\">" + e.StackTrace + "</p></div>") + string.Format("<div align=\"center\" style=\"color:#FF3300;margin:5pt;font-family:Verdana\"> MaticsoftFK <sup style=\"color:gray;font-size:9pt\">{0}</sup>", MvcApplication.Version)) + "<span style=\"color:silver\"> { Building &amp; OOP MVC Maticsoft Framework } -- [ WE CAN DO IT JUST HAPPY WORKING ]</span></div>" + "</body><style type=\"text/css\"></style></html>";
                model.Loginfo = e.Message;
                model.StackTrace = e.ToString();
                model.Url = httpContext.Request.Url.AbsoluteUri;
            }
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            if (!base.HttpContext.IsDebuggingEnabled && e.TargetSite.ToString().StartsWith("System.Web.Mvc.ViewEngineResult FindView"))
            {
                filterContext.Result = new HttpNotFoundResult();
                httpContext.Server.ClearError();
            }
            else
            {
                filterContext.Result = base.Content(content);
                httpContext.Server.ClearError();
            }
        }

        private string GetSqlExceptionMessage(int number)
        {
            string errorMessageSQL = Site.ErrorMessageSQL;
            switch (number)
            {
                case 0x11:
                    return Site.ErrorMessageSQL17;

                case 0x223:
                    return Site.ErrorMessageSQL547;

                case 0x4b5:
                    return Site.ErrorMessageSQL1205;

                case 0xfdc:
                    return Site.ErrorMessageSQL4060;

                case 0x4818:
                    return Site.ErrorMessageSQL18456;

                case 0xa29:
                    return Site.ErrorMessageSQL2601;

                case 0xa43:
                    return Site.ErrorMessageSQL2627;
            }
            return Site.ErrorMessageSQL;
        }
    }
}

