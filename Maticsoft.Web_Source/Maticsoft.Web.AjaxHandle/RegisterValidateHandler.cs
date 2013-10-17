namespace Maticsoft.Web.AjaxHandle
{
    using Maticsoft.Accounts.Data;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Json;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;

    public class RegisterValidateHandler : IHttpHandler
    {
        private Regions bll = new Regions();

        private void CheckEmail(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Email"]) && PageValidate.IsEmail(Request.Params["Email"]))
            {
                string userEmail = Request.Params["Email"];
                if (new Users().ExistsByEmail(userEmail))
                {
                    Response.Write("COUNTREG");
                }
                else
                {
                    Response.Write("CANREG");
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }

        private void CheckPhone(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["PhoneNumber"]) && PageValidate.IsPhone(Request.Params["PhoneNumber"]))
            {
                string phone = Request.Params["PhoneNumber"];
                if (new Users().ExistByPhone(phone))
                {
                    Response.Write("COUNTREG");
                }
                else
                {
                    Response.Write("CANREG");
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }

        private void CheckUser(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["UserName"]))
            {
                string userName = Request.Params["UserName"];
                if (new User().HasUserByUserName(userName))
                {
                    Response.Write("COUNTREG");
                }
                else
                {
                    Response.Write("CANREG");
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }

        private void getAreas(HttpRequest Request, HttpResponse Response)
        {
            int iParentId = Globals.SafeInt(Request.Params["CityID"], 0);
            DataSet disByParentId = this.bll.GetDisByParentId(iParentId);
            if (disByParentId != null)
            {
                if (disByParentId.Tables[0].Rows.Count > 0)
                {
                    string s = ToJson(disByParentId);
                    Response.Write(s);
                }
                else
                {
                    Response.Write("");
                }
            }
            else
            {
                Response.Write("");
            }
        }

        private void getCitys(HttpRequest Request, HttpResponse Response)
        {
            int iParentId = Globals.SafeInt(Request.Params["ProvinceID"], 0);
            DataSet disByParentId = this.bll.GetDisByParentId(iParentId);
            if (disByParentId != null)
            {
                if (disByParentId.Tables[0].Rows.Count > 0)
                {
                    string s = ToJson(disByParentId);
                    Response.Write(s);
                }
                else
                {
                    Response.Write("");
                }
            }
            else
            {
                Response.Write("");
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            switch (request.Params["action"])
            {
                case "CheckUser":
                    this.CheckUser(request, response);
                    return;

                case "CheckPhone":
                    this.CheckPhone(request, response);
                    return;

                case "CheckEmil":
                    this.CheckEmail(request, response);
                    return;

                case "ValidateEmil":
                    this.ValidateEmil(request, response);
                    return;

                case "VideoAction":
                    this.VideoAction(request, response);
                    return;

                case "getCitys":
                    this.getCitys(request, response);
                    return;

                case "getAreas":
                    this.getAreas(request, response);
                    return;
            }
        }

        public static string ToJson(DataSet dataSet)
        {
            string str = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                string str2 = str;
                str = str2 + "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            return (str.TrimEnd(new char[] { ',' }) + "}");
        }

        public static string ToJson(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            DataRowCollection rows = dt.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                builder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = dt.Columns[j].ColumnName;
                    string format = rows[i][j].ToString();
                    Type dataType = dt.Columns[j].DataType;
                    builder.Append("\"" + columnName + "\":");
                    format = string.Format(format, dataType);
                    if (j < (dt.Columns.Count - 1))
                    {
                        builder.Append("'" + format + "',");
                    }
                    else
                    {
                        builder.Append("'" + format + "'");
                    }
                }
                builder.Append("},");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        private void ValidateEmil(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Email"]) && PageValidate.IsEmail(Request.Params["Email"]))
            {
                Maticsoft.BLL.SysManage.VerifyMail mail = new Maticsoft.BLL.SysManage.VerifyMail();
                Maticsoft.Model.SysManage.VerifyMail model = new Maticsoft.Model.SysManage.VerifyMail();
                string recipient = Request.Params["Email"];
                string str2 = Request.Params["WebTitle"];
                string str3 = Guid.NewGuid().ToString().Replace("-", "");
                string str4 = Request.Params["userName"];
                model.UserName = str4;
                model.KeyValue = str3;
                model.CreatedDate = DateTime.Now;
                model.Status = 0;
                mail.Add(model);
                string valueByCache = ConfigSystem.GetValueByCache("EmailValidity");
                string str6 = valueByCache + "?uid=" + str4 + "&code=" + str3;
                string body = "亲爱的【" + str2 + "】用户，请您在七天内点击（或复制到浏览器地址栏）以下连接完成邮箱验证: <a href=" + str6 + ">" + str6 + "</a>";
                try
                {
                    MailSender.Send(recipient, str2 + "用户邮箱验证", body);
                    Response.Write("SENDSUCCESS");
                }
                catch (Exception)
                {
                    Response.Write("SENDERROR");
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }

        private void VideoAction(HttpRequest Request, HttpResponse Response)
        {
            HttpPostedFile postFile = Request.Files["Filedata"];
            Response.Charset = "utf-8";
            ConvertVideo video = new ConvertVideo();
            VideoModel model = new VideoModel();
            string valueByCache = ConfigSystem.GetValueByCache("EnteServiceItemVideoUrl");
            JsonObject obj2 = new JsonObject();
            if (video.UploadVideo(postFile, true, valueByCache, null, true, true, out model, ".flv"))
            {
                obj2.Accumulate("Status", "OK");
                obj2.Accumulate("SavePath", model.SavePath);
                obj2.Accumulate("ImgPath", model.ImgPath);
                obj2.Accumulate("VideoSpan", model.VideoSpan);
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

