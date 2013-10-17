namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Web;

    public class PhotosHandler : HandlerBase
    {
        private Photos photoBll = new Photos();

        private void EditRecomend(HttpContext context)
        {
            int photoID = Globals.SafeInt(context.Request.Params["PhotoID"], 0);
            int recomend = Globals.SafeInt(context.Request.Params["Recomend"], 0);
            JsonObject obj2 = new JsonObject();
            if (this.photoBll.UpdateRecomend(photoID, recomend))
            {
                obj2.Accumulate("STATUS", "OK");
            }
            else
            {
                obj2.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(obj2.ToString());
        }

        private void EditStatus(HttpContext context)
        {
            int photoID = Globals.SafeInt(context.Request.Params["PhotoID"], 0);
            int status = Globals.SafeInt(context.Request.Params["Status"], 0);
            JsonObject obj2 = new JsonObject();
            if (this.photoBll.UpdateStatus(photoID, status))
            {
                obj2.Accumulate("STATUS", "OK");
            }
            else
            {
                obj2.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(obj2.ToString());
        }

        public override void ProcessRequest(HttpContext context)
        {
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                string str2 = str;
                if (str2 != null)
                {
                    if (!(str2 == "EditRecomend"))
                    {
                        if (str2 == "EditStatus")
                        {
                            goto Label_005B;
                        }
                    }
                    else
                    {
                        this.EditRecomend(context);
                    }
                }
                return;
            Label_005B:
                this.EditStatus(context);
            }
            catch (Exception exception)
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("STATUS", "ERROR");
                obj2.Put("DATA", exception);
                context.Response.Write(obj2.ToString());
            }
        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

