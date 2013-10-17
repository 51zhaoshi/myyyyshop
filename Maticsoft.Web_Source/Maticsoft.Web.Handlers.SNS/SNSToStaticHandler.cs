namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;

    public class SNSToStaticHandler : IHttpHandler
    {
        private Photos photoBll = new Photos();
        public const string POLL_KEY_DATA = "DATA";
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_STATUS_ERROR = "ERROR";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        private Products productBll = new Products();
        private Maticsoft.BLL.SysManage.TaskQueue taskBll = new Maticsoft.BLL.SysManage.TaskQueue();
        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        protected void ContinueTask(HttpContext context)
        {
            int type = Globals.SafeInt(context.Request.Form["Type"], -1);
            TaskList = this.taskBll.GetContinueTask(type);
            JsonObject obj2 = new JsonObject();
            obj2.Put("STATUS", "SUCCESS");
            Maticsoft.Model.SysManage.TaskQueue queue = (TaskList.Count == 0) ? null : TaskList.First<Maticsoft.Model.SysManage.TaskQueue>();
            if (queue == null)
            {
                this.taskBll.DeleteTask(type);
                obj2.Put("DATA", 0);
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Put("DATA", queue.ID);
                context.Response.Write(obj2.ToString());
            }
        }

        protected void DeleteTask(HttpContext context)
        {
            string virtualRequestUrl = "";
            if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
            {
                virtualRequestUrl = "/Home/Index?RequestType=1";
            }
            else
            {
                virtualRequestUrl = "/SNS/Home/Index?RequestType=1";
            }
            Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(virtualRequestUrl, "/index.html");
            this.taskBll.DeleteTask(4);
            this.taskBll.DeleteTask(5);
        }

        protected void GenerateHtml(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue model = TaskList.FirstOrDefault<Maticsoft.Model.SysManage.TaskQueue>(c => c.ID == TaskId);
            int num = Globals.SafeInt(context.Request.Form["Type"], -1);
            string str = "";
            if ((num == 4) && (model != null))
            {
                string productUrl = PageSetting.GetProductUrl((long) model.TaskId);
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    str = "/Product/Detail/" + model.TaskId;
                }
                else
                {
                    str = "/SNS/Product/Detail/" + model.TaskId;
                }
                if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(productUrl))
                {
                    if (Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(str, productUrl))
                    {
                        model.RunDate = new DateTime?(DateTime.Now);
                        model.Status = 1;
                        this.taskBll.Update(model);
                        this.productBll.UpdateStaticUrl(model.TaskId, productUrl);
                        obj2.Put("STATUS", "SUCCESS");
                    }
                    else
                    {
                        obj2.Put("STATUS", "FAILED");
                    }
                }
            }
            if ((num == 5) && (model != null))
            {
                string photoUrl = PageSetting.GetPhotoUrl(model.TaskId);
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    str = "/Photo/Detail/" + model.TaskId;
                }
                else
                {
                    str = "/SNS/Photo/Detail/" + model.TaskId;
                }
                if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(photoUrl))
                {
                    if (Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(str, photoUrl))
                    {
                        model.RunDate = new DateTime?(DateTime.Now);
                        model.Status = 1;
                        this.taskBll.Update(model);
                        this.photoBll.UpdateStaticUrl(model.TaskId, photoUrl);
                        obj2.Put("STATUS", "SUCCESS");
                    }
                    else
                    {
                        obj2.Put("STATUS", "FAILED");
                    }
                }
            }
            context.Response.Write(obj2.ToString());
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
            switch (Globals.SafeInt(context.Request.Form["Type"], -1))
            {
                case 4:
                    this.ProductToStatic(context);
                    break;

                case 5:
                    this.PhotoToStatic(context);
                    break;
            }
        }

        protected void PhotoToStatic(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(context.Request.Form["From"]) && PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append("and");
                }
                builder.AppendFormat("   CreatedDate >'" + context.Request.Form["From"] + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["To"]) && PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append("and");
                }
                builder.AppendFormat(" CreatedDate <'" + Globals.SafeDateTime(context.Request.Form["To"], DateTime.Now).AddDays(1.0).ToString("yyyy-MM-dd") + "' ", new object[0]);
            }
            List<int> listToReGen = this.photoBll.GetListToReGen(builder.ToString());
            this.taskBll.DeleteTask(5);
            TaskList = new List<Maticsoft.Model.SysManage.TaskQueue>();
            if ((listToReGen != null) && (listToReGen.Count > 0))
            {
                Maticsoft.Model.SysManage.TaskQueue model = null;
                int num = 1;
                foreach (int num2 in listToReGen)
                {
                    model = new Maticsoft.Model.SysManage.TaskQueue {
                        ID = num,
                        TaskId = num2,
                        Status = 0,
                        Type = 5
                    };
                    if (!this.taskBll.Add(model))
                    {
                        break;
                    }
                    TaskList.Add(model);
                    num++;
                }
            }
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", TaskList.Count);
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
                            goto Label_0087;
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
                this.DeleteTask(context);
                return;
            Label_0087:
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

        protected void ProductToStatic(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(context.Request.Form["From"]) && PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append("and");
                }
                builder.AppendFormat("   CreatedDate >'" + context.Request.Form["From"] + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["To"]) && PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append("and");
                }
                builder.AppendFormat(" CreatedDate <'" + Globals.SafeDateTime(context.Request.Form["To"], DateTime.Now).AddDays(1.0).ToString("yyyy-MM-dd") + "' ", new object[0]);
            }
            List<int> listToStatic = this.productBll.GetListToStatic(builder.ToString());
            this.taskBll.DeleteTask(4);
            TaskList = new List<Maticsoft.Model.SysManage.TaskQueue>();
            if ((listToStatic != null) && (listToStatic.Count > 0))
            {
                Maticsoft.Model.SysManage.TaskQueue model = null;
                int num = 1;
                foreach (int num2 in listToStatic)
                {
                    if (!(from c in TaskList select c.TaskId).Contains<int>(num2))
                    {
                        model = new Maticsoft.Model.SysManage.TaskQueue {
                            ID = num,
                            TaskId = num2,
                            Status = 0,
                            Type = 4
                        };
                        if (!this.taskBll.Add(model))
                        {
                            break;
                        }
                        TaskList.Add(model);
                        num++;
                    }
                }
            }
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", TaskList.Count);
            context.Response.Write(obj2.ToString());
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

