namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;

    public class PhotoReGenHandler : IHttpHandler
    {
        private Maticsoft.BLL.CMS.Photo photoBll = new Maticsoft.BLL.CMS.Photo();
        public const string POLL_KEY_DATA = "DATA";
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_STATUS_ERROR = "ERROR";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        private Maticsoft.BLL.SysManage.TaskQueue taskBll = new Maticsoft.BLL.SysManage.TaskQueue();
        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;

        protected void ContinueTask(HttpContext context)
        {
            TaskList = this.taskBll.GetContinueTask(3);
            JsonObject obj2 = new JsonObject();
            obj2.Put("STATUS", "SUCCESS");
            Maticsoft.Model.SysManage.TaskQueue queue = (TaskList.Count == 0) ? null : TaskList.First<Maticsoft.Model.SysManage.TaskQueue>();
            if (queue == null)
            {
                this.taskBll.DeleteTask(3);
                obj2.Put("DATA", 0);
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Put("DATA", queue.ID);
                context.Response.Write(obj2.ToString());
            }
        }

        protected void DeleteTask()
        {
            this.taskBll.DeleteTask(3);
        }

        protected void GenerateImage(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue model = TaskList.FirstOrDefault<Maticsoft.Model.SysManage.TaskQueue>(c => c.ID == TaskId);
            if (model != null)
            {
                this.ReGenPhoto(model.TaskId);
                model.RunDate = new DateTime?(DateTime.Now);
                model.Status = 1;
                this.taskBll.Update(model);
            }
            context.Response.Write(obj2.ToString());
        }

        public static string GetAbsolutePath(string path)
        {
            return ("/" + path.Replace(HttpContext.Current.Request.PhysicalApplicationPath, "").Replace(@"\", "/"));
        }

        protected void HttpToGen(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" State=1", new object[0]);
            if (!string.IsNullOrWhiteSpace(context.Request.Form["From"]) && PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                builder.AppendFormat(" and  CreatedDate >'" + context.Request.Form["From"] + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["To"]) && PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                builder.AppendFormat(" and CreatedDate <'" + context.Request.Form["To"] + "' ", new object[0]);
            }
            List<int> listToReGen = this.photoBll.GetListToReGen(builder.ToString());
            this.taskBll.DeleteTask(3);
            TaskList = new List<Maticsoft.Model.SysManage.TaskQueue>();
            if ((listToReGen != null) && (listToReGen.Count > 0))
            {
                Maticsoft.Model.SysManage.TaskQueue model = null;
                int num = 1;
                foreach (int num2 in listToReGen)
                {
                    if (!(from c in TaskList select c.TaskId).Contains<int>(num2))
                    {
                        model = new Maticsoft.Model.SysManage.TaskQueue {
                            ID = num,
                            TaskId = num2,
                            Status = 0,
                            Type = 3
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
                    if (!(str2 == "HttpToGen"))
                    {
                        if (str2 == "GenerateImage")
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
                        this.HttpToGen(context);
                    }
                }
                return;
            Label_0075:
                this.GenerateImage(context);
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

        protected void ReGenPhoto(int phottoId)
        {
            Maticsoft.Model.CMS.Photo modelByCache = this.photoBll.GetModelByCache(phottoId);
            if (modelByCache != null)
            {
                bool flag = false;
                string imageUrl = modelByCache.ImageUrl;
                if (File.Exists(HttpContext.Current.Server.MapPath(imageUrl)) && !imageUrl.StartsWith("http://"))
                {
                    new FileInfo(HttpContext.Current.Server.MapPath(imageUrl));
                    MakeThumbnailMode w = MakeThumbnailMode.W;
                    string str2 = imageUrl.Substring(imageUrl.LastIndexOf('/') + 1, (imageUrl.Length - imageUrl.LastIndexOf('/')) - 1);
                    if (!modelByCache.ThumbImageUrl.Contains("{0}"))
                    {
                        flag = true;
                        string path = "/Upload/CMS/Images/PhotosThumbs/" + DateTime.Now.ToString("yyyyMMdd");
                        if (Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                        }
                        modelByCache.ThumbImageUrl = path + "/{0}" + str2;
                    }
                    List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, Maticsoft.Components.MvcApplication.ThemeName);
                    try
                    {
                        if ((thumSizeList != null) && (thumSizeList.Count > 0))
                        {
                            foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                            {
                                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(imageUrl), HttpContext.Current.Server.MapPath(string.Format(modelByCache.ThumbImageUrl, size.ThumName)), size.ThumWidth, size.ThumHeight, w, ImageFormat.Jpeg, InterpolationMode.High, SmoothingMode.HighQuality);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        LogHelp.AddErrorLog(string.Format("CMS：{0}重新生成缩略图时发生异常:{1}", imageUrl, exception.StackTrace), "", "重新生成缩略图时发生异常");
                    }
                    try
                    {
                        if (flag)
                        {
                            this.photoBll.Update(modelByCache);
                        }
                    }
                    catch (Exception exception2)
                    {
                        LogHelp.AddErrorLog(string.Format("CMS：{0}重新生成缩略图更新到数据库时发生异常:{1}", imageUrl, exception2.StackTrace), "", "重新生成缩略图时发生异常");
                    }
                }
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

