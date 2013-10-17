namespace Maticsoft.Web.Handlers.Shop
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Json;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Web;

    public class ProductHandler : HandlerBase
    {
        private void GetMaxSequence(HttpContext context)
        {
            ProductInfo info = new ProductInfo();
            JsonObject obj2 = new JsonObject();
            string categoryPath = context.Request.Form["CategoryPath"];
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", info.MaxSequence(categoryPath) + 1);
            context.Response.Write(obj2);
        }

        private void GetSKUByProductId(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                string str2 = str;
                if (str2 != null)
                {
                    if (!(str2 == "GetSKUByProductId"))
                    {
                        if (str2 == "MaxSequence")
                        {
                            goto Label_006B;
                        }
                    }
                    else
                    {
                        this.GetSKUByProductId(context);
                    }
                }
                return;
            Label_006B:
                this.GetMaxSequence(context);
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

