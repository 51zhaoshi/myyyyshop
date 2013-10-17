namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.CMS;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;

    public class HttpToStaticHandler : IHttpHandler
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        public const string POLL_KEY_DATA = "DATA";
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_STATUS_ERROR = "ERROR";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        private Maticsoft.BLL.SysManage.TaskQueue taskBll = new Maticsoft.BLL.SysManage.TaskQueue();
        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;

        protected void ContinueTask(HttpContext context)
        {
            TaskList = this.taskBll.GetContinueTask(0);
            JsonObject obj2 = new JsonObject();
            obj2.Put("STATUS", "SUCCESS");
            Maticsoft.Model.SysManage.TaskQueue queue = TaskList.First<Maticsoft.Model.SysManage.TaskQueue>();
            obj2.Put("DATA", queue.ID);
            context.Response.Write(obj2.ToString());
        }

        protected void DeleteTask()
        {
            this.taskBll.DeleteArticle();
        }

        protected void GenerateHtml(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string valueByCache = ConfigSystem.GetValueByCache("MainArea");
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue model = TaskList.FirstOrDefault<Maticsoft.Model.SysManage.TaskQueue>(c => c.ID == TaskId);
            if (model != null)
            {
                string str2 = "";
                string str3 = PageSetting.GetCMSUrl(model.TaskId, "CMS", ApplicationKeyType.CMS);
                if (valueByCache == "CMS")
                {
                    str2 = "/Article/Details/" + model.TaskId;
                }
                else
                {
                    str2 = "/CMS/Article/Details/" + model.TaskId;
                }
                if (!string.IsNullOrWhiteSpace(str2) && !string.IsNullOrWhiteSpace(str3))
                {
                    if (Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(str2, str3))
                    {
                        model.RunDate = new DateTime?(DateTime.Now);
                        model.Status = 1;
                        this.taskBll.Update(model);
                        obj2.Put("STATUS", "SUCCESS");
                    }
                    else
                    {
                        obj2.Put("STATUS", "FAILED");
                    }
                }
                context.Response.Write(obj2.ToString());
            }
        }

        protected void HttpToStatic(HttpContext context)
        {
            IDictionaryEnumerator enumerator = context.Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key.ToString());
            }
            foreach (string str in list)
            {
                context.Cache.Remove(str);
            }
            JsonObject obj2 = new JsonObject();
            StringBuilder builder = new StringBuilder();
            int num = Globals.SafeInt(context.Request.Form["Cid"], 0);
            builder.AppendFormat(" State=0", new object[0]);
            if (num > 0)
            {
                builder.AppendFormat(" and ClassID =" + num, new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["From"]) && PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                builder.AppendFormat(" and  CreatedDate >'" + context.Request.Form["From"] + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["To"]) && PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                builder.AppendFormat(" and CreatedDate <'" + context.Request.Form["To"] + "' ", new object[0]);
            }
            List<Maticsoft.Model.CMS.Content> modelList = this.bll.GetModelList(builder.ToString());
            this.taskBll.DeleteArticle();
            TaskList = new List<Maticsoft.Model.SysManage.TaskQueue>();
            if ((modelList != null) && (modelList.Count > 0))
            {
                Maticsoft.Model.SysManage.TaskQueue model = null;
                int num2 = 1;
                foreach (Maticsoft.Model.CMS.Content content in modelList)
                {
                    model = new Maticsoft.Model.SysManage.TaskQueue {
                        ID = num2,
                        TaskId = content.ContentID,
                        Status = 0,
                        Type = 0
                    };
                    if (!this.taskBll.Add(model))
                    {
                        break;
                    }
                    TaskList.Add(model);
                    num2++;
                }
            }
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", modelList.Count);
            context.Response.Write(obj2.ToString());
        }

        public void ProcessRequest(HttpContext context)
        {
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                string str2 = str;
                if (str2 != null)
                {
                    if (!(str2 == "HttpToStatic"))
                    {
                        if (str2 == "GenerateHtml")
                        {
                            goto Label_0075;
                        }
                        if (str2 == "DeleteTask")
                        {
                            goto Label_007E;
                        }
                        if (str2 == "ContinueTask")
                        {
                            goto Label_0086;
                        }
                    }
                    else
                    {
                        this.HttpToStatic(context);
                    }
                }
                return;
            Label_0075:
                this.GenerateHtml(context);
                return;
            Label_007E:
                this.DeleteTask();
                return;
            Label_0086:
                this.ContinueTask(context);
            }
            catch (Exception exception)
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("STATUS", "SUCCESS");
                obj2.Put("DATA", exception);
                context.Response.Write(obj2.ToString());
            }
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

