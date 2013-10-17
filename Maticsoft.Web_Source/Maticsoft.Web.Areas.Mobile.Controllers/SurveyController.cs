namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Mvc;

    public class SurveyController : MobileControllerBase
    {
        public ActionResult Index(int fid = 1, string viewName = "Index")
        {
            List<Maticsoft.Model.Poll.Topics> modelList = new Maticsoft.BLL.Poll.Topics().GetModelList(-1, fid);
            return base.View(viewName, modelList);
        }

        public PartialViewResult Options(int qnumber, int type, int topicid = -1, string viewName = "_Options")
        {
            ((dynamic) base.ViewBag).type = type;
            ((dynamic) base.ViewBag).topicid = topicid;
            ((dynamic) base.ViewBag).qnumber = qnumber;
            List<Maticsoft.Model.Poll.Options> modelList = new Maticsoft.BLL.Poll.Options().GetModelList(string.Format(" TopicID={0}", topicid));
            return this.PartialView(viewName, modelList);
        }

        public ActionResult Result(int fid = 1, string viewName = "Result")
        {
            List<Maticsoft.Model.Poll.Topics> modelList = new Maticsoft.BLL.Poll.Topics().GetModelList(-1, fid);
            return base.View(viewName, modelList);
        }

        public ActionResult ResultOptions(int tid, string viewName = "_ResultOptions")
        {
            List<Maticsoft.Model.Poll.Options> countList = new Maticsoft.BLL.Poll.Options().GetCountList(" TopicID=" + tid);
            return base.View(viewName, countList);
        }

        [HttpPost]
        public ActionResult SubmitPoll(FormCollection fm)
        {
            string str = fm["TopicIDjson"];
            if (base.Request.Cookies["votetopic"] != null)
            {
                return base.Content("isnotnull");
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                return base.Content("false");
            }
            Maticsoft.BLL.Poll.PollUsers users = new Maticsoft.BLL.Poll.PollUsers();
            Maticsoft.Model.Poll.PollUsers model = new Maticsoft.Model.Poll.PollUsers();
            int num = users.Add(model);
            if (num < 0)
            {
                return base.Content("false");
            }
            Maticsoft.Model.Poll.UserPoll poll = new Maticsoft.Model.Poll.UserPoll();
            Maticsoft.BLL.Poll.UserPoll poll2 = new Maticsoft.BLL.Poll.UserPoll();
            poll.UserIP = base.Request.UserHostAddress;
            poll.UserID = num;
            foreach (JsonObject obj2 in JsonConvert.Import<JsonArray>(str))
            {
                int num2 = Globals.SafeInt(obj2["topicid"].ToString(), 0);
                string text = obj2["topicvlaue"].ToString();
                int num3 = Globals.SafeInt(obj2["type"].ToString(), -1);
                poll.TopicID = new int?(num2);
                switch (num3)
                {
                    case 0:
                    {
                        poll.OptionID = new int?(Globals.SafeInt(text, -1));
                        poll2.Add(poll);
                        continue;
                    }
                    case 1:
                    {
                        poll.OptionIDList = text;
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            poll2.Add2(poll);
                        }
                        continue;
                    }
                    case 2:
                    {
                        continue;
                    }
                }
            }
            HttpCookie cookie = new HttpCookie("votetopic");
            cookie.Values.Add("voteid", "votetopic");
            cookie.Expires = DateTime.Now.AddHours(240.0);
            base.Response.Cookies.Add(cookie);
            return base.Content("true");
        }
    }
}

