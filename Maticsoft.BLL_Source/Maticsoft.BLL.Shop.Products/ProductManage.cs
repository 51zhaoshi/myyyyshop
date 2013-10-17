namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class ProductManage
    {
        private static readonly IProductService service = DAShopProducts.CreateProductService();

        public static bool AddProduct(Maticsoft.Model.Shop.Products.ProductInfo productInfo, out long ProductId)
        {
            return service.AddProduct(productInfo, out ProductId);
        }

        private static List<ProductCompareServer> DataTableToList(DataTable dt)
        {
            List<ProductCompareServer> list = null;
            int count = dt.Rows.Count;
            if (count > 0)
            {
                list = new List<ProductCompareServer>();
                for (int i = 0; i < count; i++)
                {
                    ProductCompareServer item = new ProductCompareServer();
                    if ((dt.Rows[i]["AttName"] != null) && (dt.Rows[i]["AttName"].ToString() != ""))
                    {
                        item.AttrName = dt.Rows[i]["AttName"].ToString();
                    }
                    if ((dt.Rows[i]["Product1"] != null) && (dt.Rows[i]["Product1"].ToString() != ""))
                    {
                        item.Product1 = dt.Rows[i]["Product1"].ToString();
                    }
                    if ((dt.Rows[i]["Product2"] != null) && (dt.Rows[i]["Product2"].ToString() != ""))
                    {
                        item.Product2 = dt.Rows[i]["Product2"].ToString();
                    }
                    if ((dt.Rows[i]["Product3"] != null) && (dt.Rows[i]["Product3"].ToString() != ""))
                    {
                        item.Product3 = dt.Rows[i]["Product3"].ToString();
                    }
                    if ((dt.Rows[i]["Product4"] != null) && (dt.Rows[i]["Product4"].ToString() != ""))
                    {
                        item.Product4 = dt.Rows[i]["Product4"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<ProductCompareServer> GetCompareProudctBasicInfo(string ids)
        {
            DataSet compareProudctBasicInfo = service.GetCompareProudctBasicInfo(ids);
            if ((compareProudctBasicInfo != null) && (compareProudctBasicInfo.Tables.Count > 0))
            {
                return DataTableToList(compareProudctBasicInfo.Tables[0]);
            }
            return null;
        }

        public static List<ProductCompareServer> GetCompareProudctInfo(string ids)
        {
            DataSet compareProudctInfo = service.GetCompareProudctInfo(ids);
            if ((compareProudctInfo != null) && (compareProudctInfo.Tables.Count > 0))
            {
                return DataTableToList(compareProudctInfo.Tables[0]);
            }
            return null;
        }

        public static bool ModifyProduct(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            return service.ModifyProduct(productInfo);
        }
    }
}

