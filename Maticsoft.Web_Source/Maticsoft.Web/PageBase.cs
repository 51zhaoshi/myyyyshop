namespace Maticsoft.Web
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.SysManage;
    using Resources;
    using System;
    using System.Data.SqlClient;

    public class PageBase : PageBaseAbs
    {
        public PageBase() : base(new PageBaseOption(), new PageBaseMessageTip())
        {
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

        protected override void PageError(object sender, EventArgs e)
        {
            string s = "";
            Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog();
            Exception lastError = base.Server.GetLastError();
            if (lastError is SqlException)
            {
                SqlException exception2 = (SqlException) lastError;
                if (exception2 != null)
                {
                    string sqlExceptionMessage = this.GetSqlExceptionMessage(exception2.Number);
                    if (exception2.Number == 0x223)
                    {
                        string str3 = s;
                        s = str3 + "<h1 class=\"SystemTip\">" + Site.ErrorSystemTip + "</h1><br/> <font class=\"ErrorPageText\">" + sqlExceptionMessage + "</font>";
                    }
                    else
                    {
                        string str4 = s;
                        s = str4 + "<h1 class=\"ErrorMessage\">" + Site.ErrorSystemTip + "</h1><hr/> 该信息已被系统记录，请稍后重试或与管理员联系。<br/>错误信息： <font class=\"ErrorPageText\">" + sqlExceptionMessage + "</font>";
                        model.Loginfo = sqlExceptionMessage;
                        model.StackTrace = lastError.ToString();
                        model.Url = base.Request.Url.AbsoluteUri;
                    }
                }
            }
            else
            {
                string str5 = s;
                s = str5 + "<h1 class=\"ErrorMessage\">" + Site.ErrorSystemTip + "</h1><hr/> 该信息已被系统记录，请稍后重试或与管理员联系。<br/>错误信息： <font class=\"ErrorPageText\">" + lastError.Message.ToString() + "<hr/><b>Stack Trace:</b><br/>" + lastError.ToString() + "</font>";
                model.Loginfo = lastError.Message;
                model.StackTrace = lastError.ToString();
                model.Url = base.Request.Url.AbsoluteUri;
            }
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            base.Response.Write(s);
            base.Server.ClearError();
        }
    }
}

