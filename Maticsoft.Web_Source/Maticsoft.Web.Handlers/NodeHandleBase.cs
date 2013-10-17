namespace Maticsoft.Web.Handlers
{
    using Maticsoft.Common;
    using Maticsoft.Json;
    using System;
    using System.Web;

    public class NodeHandleBase : IHttpHandler
    {
        private JsonObject GetChildNode(HttpContext context)
        {
            string text1 = context.Request.Params["ParentId"];
            return new JsonObject();
        }

        private JsonObject GetDepthNode(HttpContext context)
        {
            Globals.SafeInt(context.Request.Params["NodeId"], 0);
            return new JsonObject();
        }

        private JsonObject GetParentNode(HttpContext context)
        {
            Globals.SafeInt(context.Request.Params["NodeId"], 0);
            return new JsonObject();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str = context.Request.Params["action"];
            JsonObject depthNode = null;
            string str2 = str;
            if (str2 != null)
            {
                if (!(str2 == "GetChildNode"))
                {
                    if (str2 == "GetDepthNode")
                    {
                        depthNode = this.GetDepthNode(context);
                    }
                    else if (str2 == "GetParentNode")
                    {
                        depthNode = this.GetParentNode(context);
                    }
                }
                else
                {
                    depthNode = this.GetChildNode(context);
                }
            }
            if (depthNode == null)
            {
                depthNode = new JsonObject();
            }
            context.Response.Write(depthNode.ToString());
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

