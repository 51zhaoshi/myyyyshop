namespace Maticsoft.Web.Handlers.Shop
{
    using Maticsoft.Json;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Web;
    using System.Web.SessionState;

    public class RechargeHandler : HandlerBase, IRequiresSessionState
    {
        public override void ProcessRequest(HttpContext context)
        {
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                string str2;
                if (((str2 = str) != null) && (str2 == "SubmitOrder"))
                {
                    context.Response.Write(this.SubmitRecharge(context));
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

        private char SubmitRecharge(HttpContext context)
        {
            throw new NotImplementedException();
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

