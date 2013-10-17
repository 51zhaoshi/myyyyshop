namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;

    public class ImageReGenHandler : IHttpHandler
    {
        private Maticsoft.BLL.SNS.Photos photoBll = new Maticsoft.BLL.SNS.Photos();
        public const string POLL_KEY_DATA = "DATA";
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_STATUS_ERROR = "ERROR";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        private Maticsoft.BLL.SysManage.TaskQueue taskBll = new Maticsoft.BLL.SysManage.TaskQueue();
        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;

        protected void ContinueTask(HttpContext context)
        {
            TaskList = this.taskBll.GetContinueTask(2);
            JsonObject obj2 = new JsonObject();
            obj2.Put("STATUS", "SUCCESS");
            Maticsoft.Model.SysManage.TaskQueue queue = (TaskList.Count == 0) ? null : TaskList.First<Maticsoft.Model.SysManage.TaskQueue>();
            if (queue == null)
            {
                this.taskBll.DeleteTask(2);
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
            this.taskBll.DeleteTask(2);
        }

        protected void GenerateImage(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue model = TaskList.FirstOrDefault<Maticsoft.Model.SysManage.TaskQueue>(c => c.ID == TaskId);
            if (model != null)
            {
                this.ReGenImage(model.TaskId);
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
            if (!string.IsNullOrWhiteSpace(context.Request.Form["From"]) && PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append("and");
                }
                builder.AppendFormat("  CreatedDate >'" + context.Request.Form["From"] + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["To"]) && PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append("and");
                }
                builder.AppendFormat(" CreatedDate <'" + context.Request.Form["To"] + "' ", new object[0]);
            }
            List<int> listToReGen = this.photoBll.GetListToReGen(builder.ToString());
            this.taskBll.DeleteTask(2);
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
                            Type = 2
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

        protected void ReGenImage(int phottoId)
        {
            Maticsoft.Model.SNS.Photos modelByCache = this.photoBll.GetModelByCache(phottoId);
            if (modelByCache != null)
            {
                bool flag = false;
                string photoUrl = modelByCache.PhotoUrl;
                if (!photoUrl.StartsWith("http://") && File.Exists(HttpContext.Current.Server.MapPath(photoUrl)))
                {
                    new FileInfo(HttpContext.Current.Server.MapPath(photoUrl));
                    MakeThumbnailMode w = MakeThumbnailMode.W;
                    List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, Maticsoft.Components.MvcApplication.ThemeName);
                    string str2 = photoUrl.Substring(photoUrl.LastIndexOf('/') + 1, (photoUrl.Length - photoUrl.LastIndexOf('/')) - 1);
                    string str3 = photoUrl.Substring(0, photoUrl.LastIndexOf('/') + 1);
                    if (!modelByCache.ThumbImageUrl.Contains("{0}"))
                    {
                        flag = true;
                        string path = "/Upload/SNS/Images/PhotosThumbs/" + DateTime.Now.ToString("yyyyMMdd");
                        if (Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                        }
                        modelByCache.ThumbImageUrl = path + "/{0}" + str2;
                    }
                    try
                    {
                        bool boolValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_ThumbImage_AddWater");
                        string str5 = str3;
                        if (boolValueByCache)
                        {
                            str5 = str3 + "W_";
                            FileHelper.MakeWater(HttpContext.Current.Server.MapPath(photoUrl), HttpContext.Current.Server.MapPath(str5 + str2));
                        }
                        if ((thumSizeList != null) && (thumSizeList.Count > 0))
                        {
                            foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                            {
                                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(str5 + str2), HttpContext.Current.Server.MapPath(string.Format(modelByCache.ThumbImageUrl, size.ThumName)), size.ThumWidth, size.ThumHeight, w, ImageFormat.Jpeg, InterpolationMode.High, SmoothingMode.HighQuality);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        LogHelp.AddErrorLog(string.Format("SNS：{0}重新生成缩略图时发生异常:{1}", photoUrl, exception.StackTrace), "", "重新生成缩略图时发生异常");
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
                        LogHelp.AddErrorLog(string.Format("SNS：{0}重新生成缩略图更新到数据库时发生异常:{1}", photoUrl, exception2.StackTrace), "", "重新生成缩略图时发生异常");
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

