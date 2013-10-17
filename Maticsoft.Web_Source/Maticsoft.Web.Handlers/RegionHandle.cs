namespace Maticsoft.Web.Handlers
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;

    public class RegionHandle : IHttpHandler
    {
        private Maticsoft.BLL.Ms.Regions RegionBll = new Maticsoft.BLL.Ms.Regions();

        private void GetChildNode(HttpContext context)
        {
            string str = context.Request.Params["ParentId"];
            JsonObject obj2 = new JsonObject();
            DataSet regionName = this.RegionBll.GetRegionName(string.IsNullOrWhiteSpace(str) ? "0" : str);
            if (regionName.Tables[0].Rows.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                obj2.Accumulate("DATA", regionName.Tables[0]);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetDepthNode(HttpContext context)
        {
            DataSet list;
            int regionId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (regionId > 0)
            {
                Maticsoft.Model.Ms.Regions model = this.RegionBll.GetModel(regionId);
                list = this.RegionBll.GetList("Depth=" + model.Depth);
            }
            else
            {
                list = this.RegionBll.GetList("Depth=1");
            }
            if (list.Tables[0].Rows.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                obj2.Accumulate("DATA", list.Tables[0]);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetParentNode(HttpContext context)
        {
            int regionId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            DataSet set = this.RegionBll.GetList("");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.Ms.Regions model = this.RegionBll.GetModel(regionId);
                if (model != null)
                {
                    string[] strArray = model.Path.TrimEnd(new char[] { ',' }).Split(new char[] { ',' });
                    if (strArray.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        foreach (string str in strArray)
                        {
                            DataRow[] item = null;
                            if (str == "0")
                            {
                                item = table.Select("ParentId is null");
                            }
                            else
                            {
                                item = table.Select("ParentId=" + str);
                            }
                            list.Add(item);
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
            string str = context.Request.Params["action"];
            string str2 = str;
            if (str2 != null)
            {
                if (!(str2 == "GetChildNode"))
                {
                    if (!(str2 == "GetDepthNode"))
                    {
                        if (str2 == "GetParentNode")
                        {
                            this.GetParentNode(context);
                        }
                        return;
                    }
                }
                else
                {
                    this.GetChildNode(context);
                    return;
                }
                this.GetDepthNode(context);
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

