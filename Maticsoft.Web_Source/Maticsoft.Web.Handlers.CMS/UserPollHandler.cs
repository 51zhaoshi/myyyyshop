namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Json;
    using Maticsoft.Model.Poll;
    using System;
    using System.Web;

    public class UserPollHandler
    {
        public const string POLL_KEY_DATA = "DATA";
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_STATUS_ERROR = "ERROR";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_SUCCESS = "SUCCESS";

        public void ProcessRequest(HttpContext context)
        {
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                string str2;
                if (((str2 = str) != null) && (str2 == "userPoll"))
                {
                    this.UserPoll(context);
                }
            }
            catch (Exception exception)
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("STATUS", "ERROR");
                obj2.Put("DATA", exception);
                context.Response.Write(obj2.ToString());
            }
        }

        private string UserPoll(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            int num = Convert.ToInt32(context.Request.Form["UID"]);
            string str = context.Request.Form["Option"];
            string str2 = context.Request.Form["FID"];
            if (context.Request.Cookies["vote" + str2] != null)
            {
                HttpCookie cookie = context.Request.Cookies["vote" + str2];
                if ((cookie.Values["voteid"].ToString() != "") || (cookie.Values["voteid"].ToString() != null))
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            Maticsoft.BLL.Poll.UserPoll poll = new Maticsoft.BLL.Poll.UserPoll();
            string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Maticsoft.Model.Poll.UserPoll model = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[0x5f], StringSplitOptions.RemoveEmptyEntries);
                model.CreatTime = new DateTime?(DateTime.Now);
                model.TopicID = new int?(int.Parse(strArray2[0]));
                model.UserID = num;
                model.UserIP = context.Request.UserHostAddress;
                model.OptionID = new int?(int.Parse(strArray2[1]));
                poll.Add(model);
            }
            obj2.Put("STATUS", "SUCCESS");
            return obj2.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

