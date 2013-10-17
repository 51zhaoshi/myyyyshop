namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;

    public class CategoriesHandler : HandlerBase
    {
        public List<Maticsoft.Model.SNS.Categories> CategoriesList;
        private Maticsoft.BLL.SNS.Categories PhotoCateBll = new Maticsoft.BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.Products ProductsBll = new Maticsoft.BLL.SNS.Products();
        private Maticsoft.BLL.SNS.Categories SNSCateBll = new Maticsoft.BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.CategorySource taoBaoCateBll = new Maticsoft.BLL.SNS.CategorySource();

        private void BindNode(int parentid, string blank)
        {
            foreach (Maticsoft.Model.SNS.Categories categories in this.SNSCateBll.GetListByParentId(parentid))
            {
                categories.Name = blank + "『" + categories.Name + "』";
                string str = blank + "─";
                this.CategoriesList.Add(categories);
                this.BindNode(categories.CategoryId, str);
            }
        }

        private void DeleteCategory(HttpContext context)
        {
            string str = context.Request.Params["CID"];
            JsonObject obj2 = new JsonObject();
            if (!string.IsNullOrWhiteSpace(str))
            {
                this.SNSCateBll.DeleteCategory(Globals.SafeInt(str, 0));
                obj2.Put("STATUS", "Success");
            }
            else
            {
                obj2.Put("STATUS", "Error");
            }
            context.Response.Write(obj2.ToString());
        }

        public void GetCategoryInfo(HttpContext context)
        {
            Func<Maticsoft.Model.SNS.Categories, bool> predicate = null;
            string categoryId = context.Request.Params["CID"];
            int type = Globals.SafeInt(context.Request.Params["Type"], 0);
            JsonObject obj2 = new JsonObject();
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                if (predicate == null)
                {
                    predicate = c => c.ParentID == Globals.SafeInt(categoryId, 0);
                }
                List<Maticsoft.Model.SNS.Categories> list2 = this.SNSCateBll.GetAllCateByCache(type).Where<Maticsoft.Model.SNS.Categories>(predicate).ToList<Maticsoft.Model.SNS.Categories>();
                if ((list2 != null) && (list2.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    list2.ForEach(delegate (Maticsoft.Model.SNS.Categories info) {
                        data.Add(new JsonObject(new string[] { "CategoryId", "Name", "ParentID", "HasChildren" }, new object[] { info.CategoryId, info.Name, info.ParentID, info.HasChildren }));
                    });
                    obj2.Put("STATUS", "Success");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "Fail");
                }
            }
            else
            {
                obj2.Put("STATUS", "Error");
            }
            context.Response.Write(obj2.ToString());
        }

        private void GetPhotoChildNode(HttpContext context)
        {
            string text = context.Request.Params["ParentId"];
            JsonObject obj2 = new JsonObject();
            int parentCategoryId = Globals.SafeInt(text, 0);
            DataSet categorysByParentId = this.PhotoCateBll.GetCategorysByParentId(parentCategoryId);
            if (categorysByParentId.Tables[0].Rows.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                obj2.Accumulate("DATA", categorysByParentId.Tables[0]);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetPhotoDepthNode(HttpContext context)
        {
            List<Maticsoft.Model.SNS.Categories> categorysByDepth;
            JsonArray data;
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (categoryId > 0)
            {
                Maticsoft.Model.SNS.Categories model = this.PhotoCateBll.GetModel(categoryId);
                categorysByDepth = this.PhotoCateBll.GetCategorysByDepth(model.Depth, 1);
            }
            else
            {
                categorysByDepth = this.PhotoCateBll.GetCategorysByDepth(1, 1);
            }
            if (categorysByDepth.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                data = new JsonArray();
                categorysByDepth.ForEach(delegate (Maticsoft.Model.SNS.Categories info) {
                    data.Add(new JsonObject(new string[] { "ClassID", "ClassName" }, new object[] { info.CategoryId, info.Name }));
                });
                obj2.Accumulate("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetPhotoParentNode(HttpContext context)
        {
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            DataSet set = this.PhotoCateBll.GetList(" Type=1 and Status=1");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.SNS.Categories model = this.PhotoCateBll.GetModel(categoryId);
                if (model != null)
                {
                    string[] strArray = model.Path.TrimEnd(new char[] { '|' }).Split(new char[] { '|' });
                    if (strArray.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strArray.Length; i++)
                        {
                            DataRow[] item = null;
                            if (i == 0)
                            {
                                item = table.Select("ParentId=0");
                            }
                            else
                            {
                                item = table.Select("ParentId=" + strArray[i - 1]);
                            }
                            if (item.Length > 0)
                            {
                                list.Add(item);
                            }
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

        private void GetSNSChildNode(HttpContext context)
        {
            string text = context.Request.Params["ParentId"];
            JsonObject obj2 = new JsonObject();
            int parentCategoryId = Globals.SafeInt(text, 0);
            DataSet categorysByParentId = this.SNSCateBll.GetCategorysByParentId(parentCategoryId);
            if (categorysByParentId.Tables[0].Rows.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                obj2.Accumulate("DATA", categorysByParentId.Tables[0]);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetSNSDepthNode(HttpContext context)
        {
            List<Maticsoft.Model.SNS.Categories> categorysByDepth;
            JsonArray data;
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (categoryId > 0)
            {
                Maticsoft.Model.SNS.Categories model = this.SNSCateBll.GetModel(categoryId);
                categorysByDepth = this.SNSCateBll.GetCategorysByDepth(model.Depth, 0);
            }
            else
            {
                categorysByDepth = this.SNSCateBll.GetCategorysByDepth(1, 0);
            }
            if (categorysByDepth.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                data = new JsonArray();
                categorysByDepth.ForEach(delegate (Maticsoft.Model.SNS.Categories info) {
                    data.Add(new JsonObject(new string[] { "ClassID", "ClassName" }, new object[] { info.CategoryId, info.Name }));
                });
                obj2.Accumulate("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetSNSParentNode(HttpContext context)
        {
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            DataSet set = this.SNSCateBll.GetList("Status=1 and Type=0");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.SNS.Categories model = this.SNSCateBll.GetModel(categoryId);
                if (model != null)
                {
                    string[] strArray = model.Path.TrimEnd(new char[] { '|' }).Split(new char[] { '|' });
                    if (strArray.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strArray.Length; i++)
                        {
                            DataRow[] item = null;
                            if (i == 0)
                            {
                                item = table.Select("ParentId=0");
                            }
                            else
                            {
                                item = table.Select("ParentId=" + strArray[i - 1]);
                            }
                            if (item.Length > 0)
                            {
                                list.Add(item);
                            }
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

        private void GetSNSProductNodes(HttpContext context)
        {
            JsonArray data;
            Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            this.CategoriesList = new List<Maticsoft.Model.SNS.Categories>();
            List<Maticsoft.Model.SNS.Categories> categorysByDepth = this.SNSCateBll.GetCategorysByDepth(1, 0);
            if (categorysByDepth.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                foreach (Maticsoft.Model.SNS.Categories categories in categorysByDepth)
                {
                    categories.Name = "╋" + categories.Name;
                    this.CategoriesList.Add(categories);
                    string blank = "├";
                    this.BindNode(categories.CategoryId, blank);
                }
                obj2.Accumulate("STATUS", "OK");
                data = new JsonArray();
                this.CategoriesList.ForEach(delegate (Maticsoft.Model.SNS.Categories info) {
                    data.Add(new JsonObject(new string[] { "ClassID", "ClassName" }, new object[] { info.CategoryId, info.Name }));
                });
                obj2.Accumulate("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetTaoBaoChildNode(HttpContext context)
        {
            string text = context.Request.Params["ParentId"];
            JsonObject obj2 = new JsonObject();
            int parentCategoryId = Globals.SafeInt(text, 0);
            DataSet categorysByParentId = this.taoBaoCateBll.GetCategorysByParentId(parentCategoryId);
            if (categorysByParentId.Tables[0].Rows.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                obj2.Accumulate("DATA", categorysByParentId.Tables[0]);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetTaoBaoDepthNode(HttpContext context)
        {
            List<Maticsoft.Model.SNS.CategorySource> categorysByDepth;
            JsonArray data;
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (categoryId > 0)
            {
                Maticsoft.Model.SNS.CategorySource model = this.taoBaoCateBll.GetModel(3, categoryId);
                categorysByDepth = this.taoBaoCateBll.GetCategorysByDepth(model.Depth);
            }
            else
            {
                categorysByDepth = this.taoBaoCateBll.GetCategorysByDepth(1);
            }
            if (categorysByDepth.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                data = new JsonArray();
                categorysByDepth.ForEach(delegate (Maticsoft.Model.SNS.CategorySource info) {
                    data.Add(new JsonObject(new string[] { "ClassID", "ClassName" }, new object[] { info.CategoryId, info.Name }));
                });
                obj2.Accumulate("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetTaoBaoParentNode(HttpContext context)
        {
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            DataSet set = this.taoBaoCateBll.GetList("");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.SNS.CategorySource model = this.taoBaoCateBll.GetModel(3, categoryId);
                if (model != null)
                {
                    string[] strArray = model.Path.TrimEnd(new char[] { '|' }).Split(new char[] { '|' });
                    if (strArray.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strArray.Length; i++)
                        {
                            DataRow[] item = null;
                            if (i == 0)
                            {
                                item = table.Select("ParentId=0");
                            }
                            else
                            {
                                item = table.Select("ParentId=" + strArray[i - 1]);
                            }
                            if (item.Length > 0)
                            {
                                list.Add(item);
                            }
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

        private void IsExistedPhotoCate(HttpContext context)
        {
            string text = context.Request.Params["CategoryId"];
            int categoryid = Globals.SafeInt(text, -2);
            JsonObject obj2 = new JsonObject();
            if (this.PhotoCateBll.IsExistedCate(categoryid))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void IsExistedSNSCate(HttpContext context)
        {
            string text = context.Request.Params["CategoryId"];
            int categoryid = Globals.SafeInt(text, -2);
            JsonObject obj2 = new JsonObject();
            if (this.SNSCateBll.IsExistedCate(categoryid))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void IsExistedTaoBaoCate(HttpContext context)
        {
            string text = context.Request.Params["CategoryId"];
            int categoryid = Globals.SafeInt(text, -2);
            JsonObject obj2 = new JsonObject();
            if (this.taoBaoCateBll.IsExistedCate(categoryid))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
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
                switch (str)
                {
                    case "GetSNSChildNode":
                        this.GetSNSChildNode(context);
                        return;

                    case "GetSNSDepthNode":
                        this.GetSNSDepthNode(context);
                        return;

                    case "GetSNSParentNode":
                        this.GetSNSParentNode(context);
                        return;

                    case "IsExistedSNSCate":
                        this.IsExistedSNSCate(context);
                        return;

                    case "GetSNSProductNodes":
                        this.GetSNSProductNodes(context);
                        return;

                    case "SetCategory":
                        this.SetCategory(context);
                        return;

                    case "GetCategoryInfo":
                        this.GetCategoryInfo(context);
                        return;

                    case "DeleteCategory":
                        this.DeleteCategory(context);
                        return;

                    case "GetTaoBaoChildNode":
                        this.GetTaoBaoChildNode(context);
                        return;

                    case "GetTaoBaoDepthNode":
                        this.GetTaoBaoDepthNode(context);
                        return;

                    case "GetTaoBaoParentNode":
                        this.GetTaoBaoParentNode(context);
                        return;

                    case "IsExistedTaoBaoCate":
                        this.IsExistedTaoBaoCate(context);
                        return;

                    case "GetPhotoChildNode":
                        this.GetPhotoChildNode(context);
                        return;

                    case "GetPhotoDepthNode":
                        this.GetPhotoDepthNode(context);
                        return;

                    case "GetPhotoParentNode":
                        this.GetPhotoParentNode(context);
                        return;

                    case "IsExistedPhotoCate":
                        this.IsExistedPhotoCate(context);
                        return;
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

        private void SetCategory(HttpContext context)
        {
            int productId = Globals.SafeInt(context.Request.Params["ProductId"], 0);
            int cateId = Globals.SafeInt(context.Request.Params["CategoryId"], 0);
            JsonObject obj2 = new JsonObject();
            if (((productId > 0) && (cateId > 0)) && this.ProductsBll.UpdateEX(productId, cateId))
            {
                obj2.Accumulate("STATUS", "OK");
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

