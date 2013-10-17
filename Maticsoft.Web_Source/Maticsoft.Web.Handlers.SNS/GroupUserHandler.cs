namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Web;

    public class GroupUserHandler : HandlerBase
    {
        private GroupUsers groupUserBll = new GroupUsers();

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
                    if (!(str2 == "RecommandUser"))
                    {
                        if (str2 == "SetAdmin")
                        {
                            goto Label_005B;
                        }
                    }
                    else
                    {
                        this.RecommandUser(context);
                    }
                }
                return;
            Label_005B:
                this.SetAdmin(context);
            }
            catch (Exception exception)
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("STATUS", "ERROR");
                obj2.Put("DATA", exception);
                context.Response.Write(obj2.ToString());
            }
        }

        private void RecommandUser(HttpContext context)
        {
            int groupID = Globals.SafeInt(context.Request.Params["GroupID"], 0);
            int userID = Globals.SafeInt(context.Request.Params["UserID"], 0);
            int recommand = Globals.SafeInt(context.Request.Params["recommand"], 0);
            JsonObject obj2 = new JsonObject();
            if (this.groupUserBll.UpdateRecommand(groupID, userID, recommand))
            {
                obj2.Accumulate("STATUS", "OK");
            }
            else
            {
                obj2.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(obj2.ToString());
        }

        private void SetAdmin(HttpContext context)
        {
            int groupID = Globals.SafeInt(context.Request.Params["GroupID"], 0);
            int userID = Globals.SafeInt(context.Request.Params["UserID"], 0);
            int role = Globals.SafeInt(context.Request.Params["Role"], 0);
            JsonObject obj2 = new JsonObject();
            if (this.groupUserBll.UpdateRole(groupID, userID, role))
            {
                obj2.Accumulate("STATUS", "OK");
            }
            else
            {
                obj2.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(obj2.ToString());
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

