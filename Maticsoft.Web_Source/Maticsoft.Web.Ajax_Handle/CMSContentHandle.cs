namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Json;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Web;

    public class CMSContentHandle : IHttpHandler
    {
        private Maticsoft.BLL.CMS.VideoClass videocate = new Maticsoft.BLL.CMS.VideoClass();

        private void ContentAttachmentUpload(HttpRequest Request, HttpResponse Response)
        {
            HttpPostedFile file = Request.Files["Filedata"];
            Response.Charset = "utf-8";
            string valueByCache = ConfigSystem.GetValueByCache("UploadAttachmentPath");
            if (file != null)
            {
                string path = HttpContext.Current.Server.MapPath("/" + valueByCache);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str3 = Path.GetExtension(file.FileName).ToLower();
                string str4 = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) + str3;
                string filename = path + str4;
                JsonObject obj2 = new JsonObject();
                try
                {
                    file.SaveAs(filename);
                    obj2.Accumulate("Status", "OK");
                    obj2.Accumulate("SavePath", valueByCache + str4);
                    Response.Write("1|" + obj2.ToString());
                }
                catch (Exception)
                {
                    obj2.Accumulate("Status", "Failed");
                    obj2.Accumulate("ErrorMessage", "ERROR501，请联系管理员！");
                    Response.Write("0|" + obj2.ToString());
                }
            }
            else
            {
                JsonObject obj3 = new JsonObject();
                obj3.Accumulate("Status", "Failed");
                obj3.Accumulate("ErrorMessage", "ERROR502，请联系管理员！");
                Response.Write("0|" + obj3.ToString());
            }
        }

        private void ContentTypeAdd(HttpRequest Request, HttpResponse Response)
        {
            Maticsoft.BLL.CMS.ClassType type = new Maticsoft.BLL.CMS.ClassType();
            Maticsoft.Model.CMS.ClassType model = null;
            if (!string.IsNullOrWhiteSpace(Request.Params["ClassTypeName"]) && !string.IsNullOrWhiteSpace(Request.Params["ClassTypeID"]))
            {
                string s = Request.Params["ClassTypeID"];
                model = new Maticsoft.Model.CMS.ClassType {
                    ClassTypeID = int.Parse(s),
                    ClassTypeName = Request.Params["ClassTypeName"]
                };
                if (type.Update(model))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ADDFAILED");
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.Params["ClassTypeName"]))
            {
                model = new Maticsoft.Model.CMS.ClassType {
                    ClassTypeName = Request.Params["ClassTypeName"]
                };
                if (type.Add(model))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("EDITFAILED");
                }
            }
            else
            {
                Response.Write("FAILED");
            }
        }

        private void DeleteAttachment(HttpRequest Request, HttpResponse Response)
        {
            Response.Charset = "utf-8";
            if (!string.IsNullOrWhiteSpace(Request.Params["ContentID"]))
            {
                int contentID = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["ContentID"]))
                {
                    contentID = Globals.SafeInt(Request.Params["ContentID"], 0);
                }
                Maticsoft.BLL.CMS.Content content = new Maticsoft.BLL.CMS.Content();
                Maticsoft.Model.CMS.Content model = content.GetModel(contentID);
                if (model != null)
                {
                    model.Attachment = null;
                    if (content.Update(model))
                    {
                        Response.Write("SUCCESS");
                    }
                    else
                    {
                        Response.Write("FAILED");
                    }
                }
                else
                {
                    Response.Write("FAILED");
                }
            }
        }

        private void GetChildNode(HttpContext context)
        {
            string text = context.Request.Params["ParentId"];
            JsonObject obj2 = new JsonObject();
            int parentCategoryId = Globals.SafeInt(text, 0);
            DataSet categorysByParentIdDs = this.videocate.GetCategorysByParentIdDs(parentCategoryId);
            if (categorysByParentIdDs.Tables[0].Rows.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                obj2.Accumulate("DATA", categorysByParentIdDs.Tables[0]);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetDepthNode(HttpContext context)
        {
            List<Maticsoft.Model.CMS.VideoClass> categorysByDepth;
            JsonArray data;
            int videoClassID = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (videoClassID > 0)
            {
                Maticsoft.Model.CMS.VideoClass model = this.videocate.GetModel(videoClassID);
                categorysByDepth = this.videocate.GetCategorysByDepth(model.Depth);
            }
            else
            {
                categorysByDepth = this.videocate.GetCategorysByDepth(1);
            }
            if (categorysByDepth.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                data = new JsonArray();
                categorysByDepth.ForEach(delegate (Maticsoft.Model.CMS.VideoClass info) {
                    data.Add(new JsonObject(new string[] { "ClassID", "ClassName" }, new object[] { info.VideoClassID, info.VideoClassName }));
                });
                obj2.Accumulate("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetParentNode(HttpContext context)
        {
            int videoClassID = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            DataSet set = this.videocate.GetList("");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.CMS.VideoClass model = this.videocate.GetModel(videoClassID);
                if (model != null)
                {
                    string[] strArray = model.Path.TrimEnd(new char[] { '|' }).Split(new char[] { '|' });
                    if (strArray.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            DataRow[] item = null;
                            if (i == 0)
                            {
                                item = table.Select("ParentID=0");
                            }
                            else
                            {
                                item = table.Select("ParentID=" + strArray[i]);
                            }
                            if (item.Length > 0)
                            {
                                list.Add(item);
                            }
                        }
                        obj2.Accumulate("STATUS", "OK");
                        obj2.Accumulate("DATA", list);
                        obj2.Accumulate("PARENT", strArray);
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "NODATA");
                        context.Response.Write(obj2.ToString());
                        return;
                    }
                }
            }
            context.Response.Write(obj2.ToString());
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            switch (request.Params["action"])
            {
                case "Add":
                    this.ContentTypeAdd(request, response);
                    return;

                case "uploadico":
                {
                    string valueByCache = ConfigSystem.GetValueByCache("UploadImagePath");
                    this.UploadPic(request, response, valueByCache);
                    return;
                }
                case "uploadAttachment":
                    this.ContentAttachmentUpload(request, response);
                    return;

                case "DeleteAttachment":
                    this.DeleteAttachment(request, response);
                    return;

                case "uploadSwf":
                    this.VideoAction(request, response);
                    return;

                case "BrandsLogo":
                {
                    string strFileUrl = ConfigSystem.GetValueByCache("BrandsLogo");
                    this.UploadPic(request, response, strFileUrl);
                    return;
                }
                case "GetChildNode":
                    this.GetChildNode(context);
                    return;

                case "GetDepthNode":
                    this.GetDepthNode(context);
                    return;

                case "GetParentNode":
                    this.GetParentNode(context);
                    return;
            }
        }

        private void UploadPic(HttpRequest Request, HttpResponse Response, string strFileUrl)
        {
            HttpPostedFile file = Request.Files["Filedata"];
            if (file != null)
            {
                string path = HttpContext.Current.Server.MapPath("/" + strFileUrl);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str2 = Path.GetExtension(file.FileName).ToLower();
                string str3 = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) + str2;
                string filename = path + str3;
                JsonObject obj2 = new JsonObject();
                try
                {
                    file.SaveAs(filename);
                    obj2.Accumulate("Status", "OK");
                    obj2.Accumulate("SavePath", strFileUrl + str3);
                    Response.Write("1|" + obj2.ToString());
                }
                catch (Exception)
                {
                    obj2.Accumulate("Status", "Failed");
                    obj2.Accumulate("ErrorMessage", "ERROR401，请联系管理员！");
                    Response.Write("0|" + obj2.ToString());
                }
            }
            else
            {
                JsonObject obj3 = new JsonObject();
                obj3.Accumulate("Status", "Failed");
                obj3.Accumulate("ErrorMessage", "ERROR402，请联系管理员！");
                Response.Write("0|" + obj3.ToString());
            }
        }

        private void VideoAction(HttpRequest Request, HttpResponse Response)
        {
            HttpPostedFile postFile = Request.Files["Filedata"];
            Response.Charset = "utf-8";
            ConvertVideo video = new ConvertVideo();
            VideoModel model = new VideoModel();
            string valueByCache = ConfigSystem.GetValueByCache("UploadVideoUrl");
            JsonObject obj2 = new JsonObject();
            if (video.UploadVideo(postFile, false, valueByCache, null, false, false, out model, ".swf"))
            {
                obj2.Accumulate("Status", "OK");
                obj2.Accumulate("SavePath", model.SavePath);
                Response.Write("1|" + obj2.ToString());
            }
            else
            {
                obj2.Accumulate("Status", "Failed");
                obj2.Accumulate("ErrorMessage", video.errorMessage);
                Response.Write("0|" + obj2.ToString());
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

