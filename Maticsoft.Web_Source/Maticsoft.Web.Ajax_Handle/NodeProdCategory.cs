namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;

    public class NodeProdCategory : IHttpHandler
    {
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryBLL = new Maticsoft.BLL.Shop.Products.CategoryInfo();

        private void GetChildNode(HttpContext context)
        {
            string str = context.Request.Params["CategoryId"];
            JsonObject obj2 = new JsonObject();
            DataSet list = this.categoryBLL.GetList("ParentCategoryId=" + (string.IsNullOrWhiteSpace(str) ? "-1" : str));
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

        private void GetDepthNode(HttpContext context)
        {
            DataSet list;
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (categoryId > 0)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo model = this.categoryBLL.GetModel(categoryId);
                list = this.categoryBLL.GetList("Depth=" + model.Depth);
            }
            else
            {
                list = this.categoryBLL.GetList("Depth=1");
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
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            DataSet set = this.categoryBLL.GetList("");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.Shop.Products.CategoryInfo model = this.categoryBLL.GetModel(categoryId);
                if (model != null)
                {
                    string[] strArray = model.Path.TrimEnd(new char[] { '|' }).Split(new char[] { '|' });
                    if (strArray.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        foreach (string str in strArray)
                        {
                            DataRow[] item = table.Select(" ParentCategoryId=" + str);
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

