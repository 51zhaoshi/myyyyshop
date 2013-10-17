namespace Maticsoft.Web.Service
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.Web;
    using System.Web.Script.Services;
    using System.Web.Services;

    [WebService(Namespace="http://tempuri.org/"), WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1), ToolboxItem(false), ScriptService]
    public class GetData : WebService
    {
        private bool CheckHtmlCode(ref string tmpStr, string value)
        {
            tmpStr = HttpUtility.HtmlDecode(tmpStr);
            return tmpStr.StartsWith(value, true, CultureInfo.CurrentCulture);
        }

        [WebMethod]
        public string GetEnteName(string prefixText, int limit)
        {
            if (string.IsNullOrWhiteSpace(prefixText))
            {
                return string.Empty;
            }
            string strEnteName = this.HtmlEncode(prefixText);
            DataSet enteName = new Enterprise().GetEnteName(strEnteName, limit);
            JsonArray array = new JsonArray();
            if (enteName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < enteName.Tables[0].Rows.Count; i++)
                {
                    string tmpStr = enteName.Tables[0].Rows[i]["Name"].ToString();
                    if (this.CheckHtmlCode(ref tmpStr, prefixText))
                    {
                        JsonObject obj2 = new JsonObject();
                        obj2.Accumulate("name", tmpStr);
                        array.Add(obj2);
                    }
                }
            }
            return array.ToString();
        }

        [WebMethod]
        public string GetUserName(string prefixText, int limit)
        {
            if (string.IsNullOrWhiteSpace(prefixText))
            {
                return string.Empty;
            }
            string strUName = this.HtmlEncode(prefixText);
            DataSet userName = new UsersExp().GetUserName(strUName, limit);
            JsonArray array = new JsonArray();
            if (userName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < userName.Tables[0].Rows.Count; i++)
                {
                    string tmpStr = userName.Tables[0].Rows[i]["UserName"].ToString();
                    if (this.CheckHtmlCode(ref tmpStr, prefixText))
                    {
                        JsonObject obj2 = new JsonObject();
                        obj2.Accumulate("name", tmpStr);
                        array.Add(obj2);
                    }
                }
            }
            return array.ToString();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private string HtmlEncode(string prefixText)
        {
            return InjectionFilter.QuoteFilter(HttpUtility.HtmlEncode(prefixText));
        }
    }
}

