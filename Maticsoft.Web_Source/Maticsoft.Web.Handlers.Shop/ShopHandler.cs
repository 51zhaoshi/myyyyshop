namespace Maticsoft.Web.Handlers.Shop
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.BLL.Shop.Package;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.Shop.Sales;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web;

    public class ShopHandler : IHttpHandler
    {
        private Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.Gift.GiftsCategory giftCateBll = new Maticsoft.BLL.Shop.Gift.GiftsCategory();
        private Maticsoft.BLL.Shop.Products.ProductStationMode productStationModeBLL = new Maticsoft.BLL.Shop.Products.ProductStationMode();
        private Maticsoft.BLL.Shop.Sales.SalesRuleProduct ruleProductBll = new Maticsoft.BLL.Shop.Sales.SalesRuleProduct();
        public const string SHOP_KEY_DATA = "DATA";
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_STATUS_ERROR = "ERROR";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        private Maticsoft.BLL.SNS.Categories SNSCateBll = new Maticsoft.BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.CategorySource taoBaoCateBll = new Maticsoft.BLL.SNS.CategorySource();

        private string AddRuleProduct(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            int num = Convert.ToInt32(context.Request.Form["ProductId"]);
            int ruleId = Convert.ToInt32(context.Request.Form["RuleId"]);
            string str = context.Request.Form["ProductName"];
            if (this.ruleProductBll.Exists(ruleId, (long) num))
            {
                obj2.Put("STATUS", "Presence");
                return obj2.ToString();
            }
            Maticsoft.Model.Shop.Sales.SalesRuleProduct model = new Maticsoft.Model.Shop.Sales.SalesRuleProduct {
                ProductId = num,
                RuleId = ruleId,
                ProductName = str
            };
            if (this.ruleProductBll.Add(model))
            {
                obj2.Put("STATUS", "SUCCESS");
                obj2.Put("DATA", "Approve");
                return obj2.ToString();
            }
            obj2.Put("STATUS", "NODATA");
            return obj2.ToString();
        }

        private void DeleteBrands(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["idList"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                Maticsoft.BLL.Shop.Products.BrandInfo info = new Maticsoft.BLL.Shop.Products.BrandInfo();
                Maticsoft.BLL.Shop.Products.ProductInfo info2 = new Maticsoft.BLL.Shop.Products.ProductInfo();
                Maticsoft.BLL.Shop.Products.ProductTypeBrand brand = new Maticsoft.BLL.Shop.Products.ProductTypeBrand();
                int brandId = Globals.SafeInt(str, 0);
                if (info2.ExistsBrands(brandId))
                {
                    obj2.Put("STATUS", "FAILED");
                    obj2.Put("DATA", "该品牌正在使用中！");
                }
                if (info.DeleteList(str))
                {
                    brand.Delete(null, new int?(brandId));
                    obj2.Put("STATUS", "SUCCESS");
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                    obj2.Put("DATA", "系统忙，请稍后再试！");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
                obj2.Put("DATA", "系统忙，请稍后再试！");
            }
            context.Response.Write(obj2.ToString());
        }

        private void DeleteImage(HttpContext context)
        {
            string text = context.Request.Form["ValueId"];
            int num = Globals.SafeInt(text, 0);
            Maticsoft.BLL.Shop.Products.AttributeValue value2 = new Maticsoft.BLL.Shop.Products.AttributeValue();
            if (value2.DeleteImage((long) num))
            {
                context.Response.Write("SUCCESS");
            }
            else
            {
                context.Response.Write("FAILED");
            }
        }

        private string DeleteRuleProduct(HttpContext context)
        {
            int num = Convert.ToInt32(context.Request.Form["ProductId"]);
            int ruleId = Convert.ToInt32(context.Request.Form["RuleId"]);
            JsonObject obj2 = new JsonObject();
            if (this.ruleProductBll.Delete(ruleId, (long) num))
            {
                obj2.Put("STATUS", "SUCCESS");
                return obj2.ToString();
            }
            obj2.Put("STATUS", "NODATA");
            return obj2.ToString();
        }

        private void EditValue(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Form["ValueId"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long num = Convert.ToInt64(str);
                bool flag = new Maticsoft.BLL.Shop.Products.SKUItem().Exists(null, null, new long?(num));
                bool flag2 = new Maticsoft.BLL.Shop.Products.ProductAttribute().Exists(null, null, new long?(num));
                if (flag || flag2)
                {
                    obj2.Put("STATUS", "FAILED");
                }
                else
                {
                    Maticsoft.BLL.Shop.Products.ProductType type = new Maticsoft.BLL.Shop.Products.ProductType();
                    if (type.DeleteManage(null, null, new long?(num)))
                    {
                        obj2.Put("STATUS", "SUCCESS");
                    }
                    else
                    {
                        obj2.Put("STATUS", "FAILED");
                    }
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void GetAttributesList(HttpContext context)
        {
            JsonArray data;
            JsonObject obj2 = new JsonObject();
            Maticsoft.BLL.Shop.Products.AttributeInfo info = new Maticsoft.BLL.Shop.Products.AttributeInfo();
            string str = context.Request.Form["DataMode"];
            int num = Globals.SafeInt(context.Request.Form["ProductTypeId"], -1);
            if (string.IsNullOrWhiteSpace(str) || (num < 1))
            {
                obj2.Put("STATUS", "ERROR");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                SearchType extAttribute;
                if (str == "0")
                {
                    extAttribute = SearchType.ExtAttribute;
                }
                else
                {
                    extAttribute = SearchType.Specification;
                }
                List<Maticsoft.Model.Shop.Products.AttributeInfo> attributeInfoList = info.GetAttributeInfoList(new int?(num), extAttribute);
                data = new JsonArray();
                attributeInfoList.ForEach(delegate (Maticsoft.Model.Shop.Products.AttributeInfo info) {
                    data.Add(new JsonObject(new string[] { "AttributeId", "AttributeName", "AttributeUsageMode", "AttributeValues", "UserDefinedPic" }, new object[] { info.AttributeId, info.AttributeName, info.UsageMode, info.AttributeValues, info.UserDefinedPic }));
                });
                obj2.Put("STATUS", "SUCCESS");
                obj2.Put("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetBrandsInfo(HttpContext context)
        {
            string str = context.Request.Params["ProductTypeId"];
            string s = context.Request.Params["pageIndex"];
            string text = context.Request.Params["TabNum"];
            int num = Globals.SafeInt(text, 0);
            int result = 0;
            if (!int.TryParse(s, out result))
            {
                result = 1;
            }
            int pageSize = Globals.SafeInt(context.Request.Params["pageSize"], 1);
            int rowCount = 0;
            int pageCount = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                Maticsoft.BLL.Shop.Products.BrandInfo info = new Maticsoft.BLL.Shop.Products.BrandInfo();
                List<Maticsoft.Model.Shop.Products.BrandInfo> list = null;
                if (num == 0)
                {
                    list = info.GetListByProductTypeId(out rowCount, out pageCount, int.Parse(str), result, pageSize, 1);
                }
                else
                {
                    list = info.GetListByProductTypeId(out rowCount, out pageCount, int.Parse(str), result, pageSize, 1);
                }
                JsonObject obj2 = new JsonObject();
                JsonArray data = new JsonArray();
                list.ForEach(delegate (Maticsoft.Model.Shop.Products.BrandInfo info) {
                    data.Add(new JsonObject(new string[] { "BrandId", "BrandName", "DisplaySequence", "Logo", "Description" }, new object[] { info.BrandId, info.BrandName, info.DisplaySequence, info.Logo, info.Description }));
                });
                obj2.Put("STATUS", "SUCCESS");
                obj2.Put("DATA", data);
                obj2.Put("rowCount", rowCount);
                obj2.Put("pageCount", pageCount);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetBrandsKVList(HttpContext context)
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> brands;
            JsonObject obj2 = new JsonObject();
            Maticsoft.BLL.Shop.Products.BrandInfo info = new Maticsoft.BLL.Shop.Products.BrandInfo();
            int productTypeId = Globals.SafeInt(context.Request.Form["ProductTypeId"], -1);
            if (productTypeId < 1)
            {
                brands = info.GetBrands();
            }
            else
            {
                brands = info.GetModelListByProductTypeId(productTypeId, -1);
            }
            JsonArray data = new JsonArray();
            brands.ForEach(delegate (Maticsoft.Model.Shop.Products.BrandInfo info) {
                data.Add(new JsonObject(new string[] { "BrandId", "BrandName" }, new object[] { info.BrandId, info.BrandName }));
            });
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", data);
            context.Response.Write(obj2.ToString());
        }

        private void GetChildNode(HttpContext context)
        {
            string text = context.Request.Params["ParentId"];
            JsonObject obj2 = new JsonObject();
            int parentCategoryId = Globals.SafeInt(text, 0);
            DataSet categorysByParentIdDs = this.cateBll.GetCategorysByParentIdDs(parentCategoryId);
            if (categorysByParentIdDs.Tables[0].Rows.Count < 1)
            {
                obj2.Accumulate("STATUS", "NODATA");
                context.Response.Write(obj2.ToString());
            }
            else
            {
                obj2.Accumulate("STATUS", "OK");
                obj2.Accumulate("DATA", categorysByParentIdDs.Tables[0]);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetDepthNode(HttpContext context)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysByDepth;
            JsonArray data;
            int categoryId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (categoryId > 0)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo model = this.cateBll.GetModel(categoryId);
                categorysByDepth = this.cateBll.GetCategorysByDepth(model.Depth);
            }
            else
            {
                categorysByDepth = this.cateBll.GetCategorysByDepth(1);
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
                categorysByDepth.ForEach(delegate (Maticsoft.Model.Shop.Products.CategoryInfo info) {
                    data.Add(new JsonObject(new string[] { "ClassID", "ClassName" }, new object[] { info.CategoryId, info.Name }));
                });
                obj2.Accumulate("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetGiftChildNode(HttpContext context)
        {
            string text = context.Request.Params["ParentId"];
            JsonObject obj2 = new JsonObject();
            int parentCategoryId = Globals.SafeInt(text, 0);
            DataSet categorysByParentId = this.giftCateBll.GetCategorysByParentId(parentCategoryId);
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

        private void GetGiftDepthNode(HttpContext context)
        {
            List<Maticsoft.Model.Shop.Gift.GiftsCategory> categorysByDepth;
            JsonArray data;
            int categoryID = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            if (categoryID > 0)
            {
                Maticsoft.Model.Shop.Gift.GiftsCategory model = this.giftCateBll.GetModel(categoryID);
                categorysByDepth = this.giftCateBll.GetCategorysByDepth(model.Depth);
            }
            else
            {
                categorysByDepth = this.giftCateBll.GetCategorysByDepth(1);
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
                categorysByDepth.ForEach(delegate (Maticsoft.Model.Shop.Gift.GiftsCategory info) {
                    data.Add(new JsonObject(new string[] { "ClassID", "ClassName" }, new object[] { info.CategoryID, info.Name }));
                });
                obj2.Accumulate("DATA", data);
                context.Response.Write(obj2.ToString());
            }
        }

        private void GetGiftParentNode(HttpContext context)
        {
            int categoryID = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject obj2 = new JsonObject();
            DataSet set = this.giftCateBll.GetList("");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.Shop.Gift.GiftsCategory model = this.giftCateBll.GetModel(categoryID);
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
                                item = table.Select("ParentCategoryId=0");
                            }
                            else
                            {
                                item = table.Select("ParentCategoryId=" + strArray[i - 1]);
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

        private void GetPackageNode(HttpContext context)
        {
            Maticsoft.BLL.Shop.Package.Package package = new Maticsoft.BLL.Shop.Package.Package();
            int num = Globals.SafeInt(context.Request.Params["id"], 0);
            string str = context.Request.Params["q"];
            JsonObject obj2 = new JsonObject();
            StringBuilder builder = new StringBuilder();
            if (num > 0)
            {
                builder.Append(" CategoryId =" + num);
            }
            if (!string.IsNullOrEmpty(str))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" Name like '%" + str + "%'");
            }
            DataSet list = null;
            if (builder.Length > 0)
            {
                list = package.GetList(builder.ToString());
            }
            if ((list == null) || (list.Tables[0].Rows.Count < 1))
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
            DataSet set = this.cateBll.GetList("");
            if ((set != null) && (set.Tables.Count > 0))
            {
                DataTable table = set.Tables[0];
                Maticsoft.Model.Shop.Products.CategoryInfo model = this.cateBll.GetModel(categoryId);
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
                                item = table.Select("ParentCategoryId=0");
                            }
                            else
                            {
                                item = table.Select("ParentCategoryId=" + strArray[i - 1]);
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

        private void GetProductTypesKVList(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            List<Maticsoft.Model.Shop.Products.ProductType> productTypes = new Maticsoft.BLL.Shop.Products.ProductType().GetProductTypes();
            JsonArray data = new JsonArray();
            productTypes.ForEach(delegate (Maticsoft.Model.Shop.Products.ProductType info) {
                data.Add(new JsonObject(new string[] { "TypeId", "TypeName" }, new object[] { info.TypeId, info.TypeName }));
            });
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", data);
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
            DataSet set = this.SNSCateBll.GetList("");
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

        private string InsertProductStationMode(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int type = Convert.ToInt32(context.Request.Form["Type"]);
            if (this.productStationModeBLL.Exists(productId, type))
            {
                obj2.Put("STATUS", "Presence");
                return obj2.ToString();
            }
            Maticsoft.Model.Shop.Products.ProductStationMode model = new Maticsoft.Model.Shop.Products.ProductStationMode {
                ProductId = productId,
                DisplaySequence = (this.productStationModeBLL.GetRecordCount(string.Empty) == 0) ? 1 : (this.productStationModeBLL.GetRecordCount(string.Empty) + 1),
                Type = type
            };
            if (this.productStationModeBLL.Add(model) > 0)
            {
                obj2.Put("STATUS", "SUCCESS");
                obj2.Put("DATA", "Approve");
                return obj2.ToString();
            }
            obj2.Put("STATUS", "NODATA");
            return obj2.ToString();
        }

        private void IsExistedGift(HttpContext context)
        {
            string text = context.Request.Params["CategoryId"];
            int categoryid = Globals.SafeInt(text, -2);
            JsonObject obj2 = new JsonObject();
            if (this.giftCateBll.IsExistedGift(categoryid))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void IsExistedProduct(HttpContext context)
        {
            string text = context.Request.Params["CategoryId"];
            int category = Globals.SafeInt(text, -2);
            JsonObject obj2 = new JsonObject();
            if (this.cateBll.IsExistedProduce(category))
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

        private void IsExistSkuCode(HttpContext context)
        {
            string str = context.Request.Params["SKUCode"];
            JsonObject obj2 = new JsonObject();
            if (!string.IsNullOrWhiteSpace(str))
            {
                Maticsoft.BLL.Shop.Products.SKUInfo info = new Maticsoft.BLL.Shop.Products.SKUInfo();
                if (info.Exists(str))
                {
                    obj2.Put("STATUS", "FAILED");
                }
                else
                {
                    obj2.Put("STATUS", "SUCCESS");
                }
            }
            else
            {
                obj2.Put("STATUS", "ERROR");
            }
            context.Response.Write(obj2.ToString());
        }

        private void LoadAttributesvalues(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<Maticsoft.Model.Shop.Products.SKUItem> list = new Maticsoft.BLL.Shop.Products.SKUItem().AttributeValueInfo(productId);
                if ((list != null) && (list.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(delegate (Maticsoft.Model.Shop.Products.SKUItem info) {
                        data.Add(new JsonObject(new string[] { "SpecId", "AttributeId", "ValueId", "ImageUrl", "ValueStr", "UserDefinedPic" }, new object[] { info.SpecId, info.AttributeId, info.ValueId, info.ImageUrl, info.ValueStr, info.UserDefinedPic }));
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void LoadExistAttributes(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<AttributeHelper> list = new Maticsoft.BLL.Shop.Products.AttributeInfo().ProductAttributeInfo(productId);
                if ((list != null) && (list.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(delegate (AttributeHelper info) {
                        data.Add(new JsonObject(new string[] { "AttributeId", "ValueId", "UsageMode" }, new object[] { info.AttributeId, info.ValueId, info.UsageMode }));
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        public void ProcessRequest(HttpContext context)
        {
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            string s = "";
            try
            {
                switch (str)
                {
                    case "GetAttributesList":
                        this.GetAttributesList(context);
                        break;

                    case "EditValue":
                        this.EditValue(context);
                        break;

                    case "GetProductTypesKVList":
                        this.GetProductTypesKVList(context);
                        break;

                    case "GetBrandsKVList":
                        this.GetBrandsKVList(context);
                        break;

                    case "GetBrandsList":
                        this.GetBrandsInfo(context);
                        break;

                    case "DeleteBrands":
                        this.DeleteBrands(context);
                        break;

                    case "DeleteImage":
                        this.DeleteImage(context);
                        break;

                    case "GetChildNode":
                        this.GetChildNode(context);
                        break;

                    case "GetDepthNode":
                        this.GetDepthNode(context);
                        break;

                    case "GetParentNode":
                        this.GetParentNode(context);
                        break;

                    case "IsExistedProduct":
                        this.IsExistedProduct(context);
                        break;

                    case "GetGiftChildNode":
                        this.GetGiftChildNode(context);
                        break;

                    case "GetGiftDepthNode":
                        this.GetGiftDepthNode(context);
                        break;

                    case "GetGiftParentNode":
                        this.GetGiftParentNode(context);
                        break;

                    case "IsExistedGift":
                        this.IsExistedGift(context);
                        break;

                    case "GetSNSChildNode":
                        this.GetSNSChildNode(context);
                        break;

                    case "GetSNSDepthNode":
                        this.GetSNSDepthNode(context);
                        break;

                    case "GetSNSParentNode":
                        this.GetSNSParentNode(context);
                        break;

                    case "IsExistedSNSCate":
                        this.IsExistedSNSCate(context);
                        break;

                    case "GetTaoBaoChildNode":
                        this.GetTaoBaoChildNode(context);
                        break;

                    case "GetTaoBaoDepthNode":
                        this.GetTaoBaoDepthNode(context);
                        break;

                    case "GetTaoBaoParentNode":
                        this.GetTaoBaoParentNode(context);
                        break;

                    case "IsExistedTaoBaoCate":
                        this.IsExistedTaoBaoCate(context);
                        break;

                    case "ProductInfo":
                        this.ProductInfo(context);
                        break;

                    case "LoadExistAttributes":
                        this.LoadExistAttributes(context);
                        break;

                    case "LoadAttributesvalues":
                        this.LoadAttributesvalues(context);
                        break;

                    case "ProductSkuInfo":
                        this.ProductSkuInfo(context);
                        break;

                    case "ProductAccessoriesManage":
                        this.ProductAccessoriesManage(context);
                        break;

                    case "ProductAccessoriesValues":
                        this.ProductAccessoriesValues(context);
                        break;

                    case "RelatedProductFactory":
                        this.RelatedProductFactory(context);
                        break;

                    case "ProductIamges":
                        this.ProductIamges(context);
                        break;

                    case "GetPackage":
                        this.GetPackageNode(context);
                        break;

                    case "IsExistSkuCode":
                        this.IsExistSkuCode(context);
                        break;

                    case "InsertProductStationMode":
                        this.InsertProductStationMode(context);
                        break;

                    case "RemoveProductStationMode":
                        this.RemoveProductStationMode(context);
                        break;

                    case "SEORelation":
                        this.SEORelation(context);
                        break;

                    case "AddRuleProduct":
                        s = this.AddRuleProduct(context);
                        break;

                    case "DeleteRuleProduct":
                        s = this.DeleteRuleProduct(context);
                        break;
                }
                context.Response.Write(s);
            }
            catch (Exception exception)
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("STATUS", "ERROR");
                obj2.Put("DATA", exception);
                context.Response.Write(obj2.ToString());
            }
        }

        private void ProductAccessoriesManage(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<Maticsoft.Model.Shop.Products.ProductAccessorie> modelList = new Maticsoft.BLL.Shop.Products.ProductAccessorie().GetModelList(productId);
                if ((modelList != null) && (modelList.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    modelList.ForEach(delegate (Maticsoft.Model.Shop.Products.ProductAccessorie info) {
                        data.Add(new JsonObject(new string[] { "ProductId", "AccessoriesValueId", "AccessoriesName", "MaxQuantity", "MinQuantity", "DiscountType", "DiscountAmount" }, new object[] { info.ProductId, info.AccessoriesValueId, info.AccessoriesName, info.MaxQuantity, info.MinQuantity, info.DiscountType, info.DiscountAmount }));
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void ProductAccessoriesValues(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<Maticsoft.Model.Shop.Products.AccessoriesValue> list = new Maticsoft.BLL.Shop.Products.AccessoriesValue().AccessoriesByProductId(productId);
                if ((list != null) && (list.Count > 0))
                {
                    StringBuilder strAccValues = new StringBuilder();
                    list.ForEach(delegate (Maticsoft.Model.Shop.Products.AccessoriesValue info) {
                        strAccValues.Append(info.ProductAccessoriesSKU);
                        strAccValues.Append(",");
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", strAccValues.ToString());
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void ProductIamges(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<Maticsoft.Model.Shop.Products.ProductImage> modelList = new Maticsoft.BLL.Shop.Products.ProductImage().GetModelList(productId);
                if ((modelList != null) && (modelList.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    modelList.ForEach(delegate (Maticsoft.Model.Shop.Products.ProductImage info) {
                        data.Add(new JsonObject(new string[] { "ProductImageId", "ProductId", "ImageUrl", "ThumbnailUrl1", "ThumbnailUrl2" }, new object[] { info.ProductImageId, info.ProductId, info.ImageUrl, info.ThumbnailUrl1, info.ThumbnailUrl2 }));
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void ProductInfo(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = new Maticsoft.BLL.Shop.Products.ProductInfo().GetModelList(productId);
                if ((modelList != null) && (modelList.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    modelList.ForEach(delegate (Maticsoft.Model.Shop.Products.ProductInfo info) {
                        data.Add(new JsonObject(new string[] { 
                            "CategoryId", "TypeId", "ProductId", "BrandId", "ProductName", "ProductCode", "EnterpriseId", "RegionId", "ShortDescription", "Unit", "Description", "Title", "Meta_Description", "Meta_Keywords", "DisplaySequence", "MarketPrice", 
                            "HasSKU", "ImageUrl", "ThumbnailUrl1", "MaxQuantity", "MinQuantity", "SaleStatus"
                         }, new object[] { 
                            info.CategoryId, info.TypeId, info.ProductId, info.BrandId, info.ProductName, info.ProductCode, info.SupplierId, info.RegionId, info.ShortDescription, info.Unit, info.Description, info.Meta_Title, info.Meta_Description, info.Meta_Keywords, info.DisplaySequence, info.MarketPrice, 
                            info.HasSKU, info.ImageUrl, info.ThumbnailUrl1, info.MaxQuantity, info.MinQuantity, info.SaleStatus
                         }));
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void ProductSkuInfo(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<Maticsoft.Model.Shop.Products.SKUInfo> productSkuInfo = new Maticsoft.BLL.Shop.Products.SKUInfo().GetProductSkuInfo(productId);
                if ((productSkuInfo != null) && (productSkuInfo.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    productSkuInfo.ForEach(delegate (Maticsoft.Model.Shop.Products.SKUInfo info) {
                        data.Add(new JsonObject(new string[] { "SkuId", "ProductId", "SKU", "Weight", "Stock", "AlertStock", "CostPrice", "SalePrice", "Upselling" }, new object[] { info.SkuId, info.ProductId, info.SKU, info.Weight, info.Stock, info.AlertStock, info.CostPrice, info.SalePrice, info.Upselling }));
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private void RelatedProductFactory(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                long productId = Globals.SafeLong(str, (long) (-1L));
                List<Maticsoft.Model.Shop.Products.RelatedProduct> modelList = new Maticsoft.BLL.Shop.Products.RelatedProduct().GetModelList(productId);
                if ((modelList != null) && (modelList.Count > 0))
                {
                    StringBuilder strReleatedInfo = new StringBuilder();
                    modelList.ForEach(delegate (Maticsoft.Model.Shop.Products.RelatedProduct info) {
                        strReleatedInfo.Append(info.RelatedId);
                        strReleatedInfo.Append(",");
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", strReleatedInfo.ToString());
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
        }

        private string RemoveProductStationMode(HttpContext context)
        {
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int type = Convert.ToInt32(context.Request.Form["Type"]);
            JsonObject obj2 = new JsonObject();
            if (this.productStationModeBLL.Delete(productId, type))
            {
                obj2.Put("STATUS", "SUCCESS");
                return obj2.ToString();
            }
            obj2.Put("STATUS", "NODATA");
            return obj2.ToString();
        }

        private void SEORelation(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Params["IsCMS"];
            string str2 = context.Request.Params["IsShop"];
            string str3 = context.Request.Params["IsSNS"];
            string str4 = context.Request.Params["IsComment"];
            StringBuilder builder = new StringBuilder();
            builder.Append(" IsActive=1 ");
            if (!string.IsNullOrWhiteSpace(str) && bool.Parse(str))
            {
                builder.AppendFormat(" AND IsCMS=1 ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(str2) && bool.Parse(str2))
            {
                builder.AppendFormat(" AND IsShop=1 ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(str3) && bool.Parse(str3))
            {
                builder.AppendFormat(" AND IsSNS=1 ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(str4) && bool.Parse(str4))
            {
                builder.AppendFormat(" AND IsComment=1 ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(builder.ToString()))
            {
                List<Maticsoft.Model.Settings.SEORelation> modelList = new Maticsoft.BLL.Settings.SEORelation().GetModelList(builder.ToString());
                if ((modelList != null) && (modelList.Count > 0))
                {
                    JsonArray data = new JsonArray();
                    modelList.ForEach(delegate (Maticsoft.Model.Settings.SEORelation info) {
                        data.Add(new JsonObject(new string[] { "KeyName", "LinkURL" }, new object[] { info.KeyName, info.LinkURL }));
                    });
                    obj2.Put("STATUS", "SUCCESS");
                    obj2.Put("DATA", data);
                }
                else
                {
                    obj2.Put("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            context.Response.Write(obj2.ToString());
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

