namespace Maticsoft.Web
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Web;
    using System.Web.UI;

    public static class LogHelp
    {
        public static void AddErrorLog(string Loginfo, string StackTrace, string ClassName)
        {
            Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                Loginfo = Loginfo,
                StackTrace = "",
                Url = ClassName
            };
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);
        }

        public static void AddErrorLog(string Loginfo, string StackTrace, HttpRequest request)
        {
            Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                Loginfo = Loginfo,
                StackTrace = StackTrace,
                Url = request.Url.AbsoluteUri
            };
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);
        }

        public static void AddErrorLog(string Loginfo, string StackTrace, Page page)
        {
            Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                Loginfo = Loginfo,
                StackTrace = StackTrace,
                Url = page.Request.Url.AbsoluteUri
            };
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);
        }

        public static void AddUserLog(string Username, string UserType, string OPInfo)
        {
            Maticsoft.Model.SysManage.UserLog model = new Maticsoft.Model.SysManage.UserLog {
                OPInfo = OPInfo,
                Url = "",
                UserIP = "",
                UserName = Username,
                UserType = UserType
            };
            Maticsoft.BLL.SysManage.UserLog.LogUserAdd(model);
        }

        public static void AddUserLog(string Username, string UserType, string OPInfo, HttpRequest request)
        {
            Maticsoft.Model.SysManage.UserLog model = new Maticsoft.Model.SysManage.UserLog {
                OPInfo = OPInfo,
                Url = request.Url.AbsoluteUri,
                UserIP = request.UserHostAddress,
                UserName = Username,
                UserType = UserType
            };
            Maticsoft.BLL.SysManage.UserLog.LogUserAdd(model);
        }

        public static void AddUserLog(string Username, string UserType, string OPInfo, Page page)
        {
            Maticsoft.Model.SysManage.UserLog model = new Maticsoft.Model.SysManage.UserLog {
                OPInfo = OPInfo,
                Url = page.Request.Url.AbsoluteUri,
                UserIP = page.Request.UserHostAddress,
                UserName = Username,
                UserType = UserType
            };
            Maticsoft.BLL.SysManage.UserLog.LogUserAdd(model);
        }
    }
}

