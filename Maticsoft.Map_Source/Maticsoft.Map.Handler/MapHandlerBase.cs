namespace Maticsoft.Map.Handler
{
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Map.BLL;
    using Maticsoft.Map.Model;
    using System;
    using System.Web;
    using System.Web.SessionState;

    public abstract class MapHandlerBase : IHttpHandler, IRequiresSessionState
    {
        protected MapInfoManage mapInfoManage = new MapInfoManage();

        protected MapHandlerBase()
        {
        }

        protected abstract bool CheckUser(HttpContext context);
        protected virtual void GetDepartmentMapById(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            int departmentId = Globals.SafeInt(context.Request.Params["DepartmentId"], 0);
            if (departmentId < 1)
            {
                obj2.Accumulate("ERROR", "NOENTERPRISEID");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                MapInfo modelByDepartmentId = this.mapInfoManage.GetModelByDepartmentId(departmentId);
                if (modelByDepartmentId == null)
                {
                    obj2.Accumulate("STATUS", "NODATA");
                    context.Response.Write(obj2.ToString());
                }
                else
                {
                    obj2.Accumulate("STATUS", "OK");
                    obj2.Accumulate("DATA", modelByDepartmentId);
                    context.Response.Write(obj2.ToString());
                }
            }
        }

        protected abstract void ProcessAction(string actionName, HttpContext context);
        public void ProcessRequest(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            context.Response.ContentType = "application/json";
            if (!this.CheckUser(context))
            {
                context.Response.Write("{\"STATUS\":\"NOLONGIN\"}");
                obj2.Accumulate("STATUS", "NOLONGIN");
            }
            else
            {
                try
                {
                    string actionName = context.Request["Action"];
                    string str2 = actionName;
                    if (str2 == null)
                    {
                        goto Label_009E;
                    }
                    if (!(str2 == "CkeckLogin"))
                    {
                        if (str2 == "GetDepartmentMapById")
                        {
                            goto Label_0095;
                        }
                        goto Label_009E;
                    }
                    obj2.Accumulate("STATUS", "OK");
                    context.Response.Write(obj2.ToString());
                    return;
                Label_0095:
                    this.GetDepartmentMapById(context);
                    return;
                Label_009E:
                    this.ProcessAction(actionName, context);
                }
                catch (Exception exception)
                {
                    obj2.Accumulate("ERROR", exception.Message.Replace("\"", "'"));
                    context.Response.Write(obj2.ToString());
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

