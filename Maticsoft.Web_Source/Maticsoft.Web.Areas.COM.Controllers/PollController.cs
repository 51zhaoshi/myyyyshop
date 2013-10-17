namespace Maticsoft.Web.Areas.COM.Controllers
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Poll;
    using Maticsoft.Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    public class PollController : Maticsoft.Web.Controllers.ControllerBase
    {
        private Maticsoft.BLL.Poll.Forms formManage = new Maticsoft.BLL.Poll.Forms();
        private Maticsoft.BLL.Poll.Options optionManage = new Maticsoft.BLL.Poll.Options();
        private Maticsoft.BLL.Poll.Topics topicManage = new Maticsoft.BLL.Poll.Topics();

        public ActionResult Poll(int? fid)
        {
            if (!fid.HasValue)
            {
                return base.View("error");
            }
            PollActionHelper model = new PollActionHelper();
            Maticsoft.Model.Poll.Forms forms = this.formManage.GetModel(fid.Value);
            ((dynamic) base.ViewBag).FID = fid.Value;
            if (forms == null)
            {
                return base.View("error");
            }
            model.FormsHelper = forms;
            List<Maticsoft.Model.Poll.Topics> modelList = this.topicManage.GetModelList(string.Format("FormID={0}", fid.Value));
            if ((modelList != null) && (modelList.Count > 0))
            {
                model.TopicsHelper = modelList;
            }
            if ((model.TopicsHelper != null) && (model.FormsHelper != null))
            {
                return base.View(model);
            }
            return base.View();
        }

        [HttpPost]
        public void SubmitPoll(FormCollection collection)
        {
            JsonObject obj2 = new JsonObject();
            if (base.Request.Cookies["vote" + collection["FID"]] != null)
            {
                obj2.Accumulate("STATUS", "805");
                obj2.Accumulate("DATA", "您已经投过票，请不要重复投票！");
            }
            else
            {
                Maticsoft.Model.Poll.UserPoll model = new Maticsoft.Model.Poll.UserPoll();
                Maticsoft.BLL.Poll.UserPoll poll2 = new Maticsoft.BLL.Poll.UserPoll();
                model.UserID = Globals.SafeInt(collection["UID"], 0);
                model.UserIP = base.Request.UserHostAddress;
                string str = collection["Option"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int num = 0;
                    foreach (string str2 in strArray)
                    {
                        string[] strArray2 = str2.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                        model.TopicID = new int?(Globals.SafeInt(strArray2[0], -1));
                        model.OptionID = new int?(Globals.SafeInt(strArray2[1], -1));
                        poll2.Add(model);
                        num++;
                    }
                    if (num == strArray.Length)
                    {
                        HttpCookie cookie = new HttpCookie("vote" + collection["FID"]);
                        cookie.Values.Add("voteid", collection["FID"]);
                        cookie.Expires = DateTime.Now.AddHours(240.0);
                        base.Response.Cookies.Add(cookie);
                        obj2.Accumulate("STATUS", "800");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "804");
                    }
                }
                else
                {
                    obj2.Accumulate("STATUS", "804");
                }
            }
            base.Response.Write(obj2.ToString());
        }
    }
}

