namespace Maticsoft.Web.AjaxHandle
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Map.Handler;
    using Maticsoft.Map.Model;
    using System;
    using System.Web;

    public class MapHandle : MapHandlerBase
    {
        protected override bool CheckUser(HttpContext context)
        {
            return (context.User.Identity.IsAuthenticated && (context.Session[Globals.SESSIONKEY_ENTERPRISE] != null));
        }

        protected override void ProcessAction(string actionName, HttpContext context)
        {
            string str;
            if (((str = actionName) != null) && (str == "SetDepartmentMap"))
            {
                this.SetDepartmentMap(context);
            }
        }

        protected void SetDepartmentMap(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            User user = context.Session[Globals.SESSIONKEY_ENTERPRISE] as User;
            if (user != null)
            {
                int num = Globals.SafeInt(context.Request.Params["DepartmentId"], 0);
                string str = context.Request.Params["MarkersLongitude"];
                string str2 = context.Request.Params["MarkersDimension"];
                string target = context.Request.Params["PointerTitle"];
                string content = context.Request.Params["PointerContent"];
                string str5 = context.Request.Params["PointImg"];
                int num2 = Globals.SafeInt(context.Request.Params["MapId"], 0);
                if (num < 1)
                {
                    obj2.Accumulate("ERROR", "NOENTERPRISEID");
                    context.Response.Write(obj2.ToString());
                }
                else
                {
                    MapInfo model = new MapInfo {
                        UserId = user.UserID,
                        DepartmentId = num,
                        MarkersLongitude = str,
                        MarkersDimension = str2,
                        PointerTitle = Globals.HtmlEncode(target),
                        PointerContent = Globals.HtmlEncodeForSpaceWrap(content)
                    };
                    if (!string.IsNullOrWhiteSpace(str5))
                    {
                        model.PointImg = str5;
                    }
                    if (num2 < 1)
                    {
                        model.MapId = base.mapInfoManage.Add(model);
                    }
                    else
                    {
                        model.MapId = num2;
                        base.mapInfoManage.Update(model);
                    }
                    obj2.Accumulate("STATUS", "OK");
                    obj2.Accumulate("DATA", model);
                    context.Response.Write(obj2.ToString());
                }
            }
        }
    }
}

