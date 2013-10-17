namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Web;

    public class ProductImage
    {
        private readonly IProductImage dal = DAShopProducts.CreateProductImage();

        public int Add(Maticsoft.Model.Shop.Products.ProductImage model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductImage> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductImage> list = new List<Maticsoft.Model.Shop.Products.ProductImage>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductImage item = new Maticsoft.Model.Shop.Products.ProductImage();
                    if ((dt.Rows[i]["ProductImageId"] != null) && (dt.Rows[i]["ProductImageId"].ToString() != ""))
                    {
                        item.ProductImageId = int.Parse(dt.Rows[i]["ProductImageId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != ""))
                    {
                        item.ThumbnailUrl1 = dt.Rows[i]["ThumbnailUrl1"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl2"] != null) && (dt.Rows[i]["ThumbnailUrl2"].ToString() != ""))
                    {
                        item.ThumbnailUrl2 = dt.Rows[i]["ThumbnailUrl2"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl3"] != null) && (dt.Rows[i]["ThumbnailUrl3"].ToString() != ""))
                    {
                        item.ThumbnailUrl3 = dt.Rows[i]["ThumbnailUrl3"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl4"] != null) && (dt.Rows[i]["ThumbnailUrl4"].ToString() != ""))
                    {
                        item.ThumbnailUrl4 = dt.Rows[i]["ThumbnailUrl4"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl5"] != null) && (dt.Rows[i]["ThumbnailUrl5"].ToString() != ""))
                    {
                        item.ThumbnailUrl5 = dt.Rows[i]["ThumbnailUrl5"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl6"] != null) && (dt.Rows[i]["ThumbnailUrl6"].ToString() != ""))
                    {
                        item.ThumbnailUrl6 = dt.Rows[i]["ThumbnailUrl6"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl7"] != null) && (dt.Rows[i]["ThumbnailUrl7"].ToString() != ""))
                    {
                        item.ThumbnailUrl7 = dt.Rows[i]["ThumbnailUrl7"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl8"] != null) && (dt.Rows[i]["ThumbnailUrl8"].ToString() != ""))
                    {
                        item.ThumbnailUrl8 = dt.Rows[i]["ThumbnailUrl8"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ProductImageId)
        {
            return this.dal.Delete(ProductImageId);
        }

        public bool Delete(long ProductId, int ProductImageId)
        {
            return this.dal.Delete(ProductId, ProductImageId);
        }

        public bool DeleteList(string ProductImageIdlist)
        {
            return this.dal.DeleteList(ProductImageIdlist);
        }

        public bool Exists(long ProductId, int ProductImageId)
        {
            return this.dal.Exists(ProductId, ProductImageId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.ProductImage GetModel(int ProductImageId)
        {
            return this.dal.GetModel(ProductImageId);
        }

        public Maticsoft.Model.Shop.Products.ProductImage GetModelByCache(int ProductImageId)
        {
            string cacheKey = "ProductImagesModel-" + ProductImageId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ProductImageId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Shop.Products.ProductImage) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductImage> GetModelList(long productId)
        {
            DataSet list = this.dal.GetList(string.Format(" ProductId={0}", productId));
            if ((list != null) && (list.Tables.Count > 0))
            {
                return this.DataTableToList(list.Tables[0]);
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.ProductImage> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public static string MoveImage(string ImageUrl, string savePath, string saveThumbsPath)
        {
            try
            {
                if (ConfigSystem.GetValueByCache("Shop_ImageStoreWay") == "1")
                {
                    return (ImageUrl + "|" + ImageUrl);
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                    }
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(saveThumbsPath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(saveThumbsPath));
                    }
                    List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(EnumHelper.AreaType.Shop, "");
                    string str = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string path = "";
                    string str3 = "";
                    string format = saveThumbsPath + str;
                    if (File.Exists(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, ""))))
                    {
                        str3 = string.Format(savePath + str, "");
                        File.Move(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(str3));
                    }
                    if ((thumSizeList != null) && (thumSizeList.Count > 0))
                    {
                        foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                        {
                            if (File.Exists(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, size.ThumName))))
                            {
                                path = string.Format(format, size.ThumName);
                                File.Move(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, size.ThumName)), HttpContext.Current.Server.MapPath(path));
                            }
                        }
                    }
                    return (str3 + "|" + format);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        public List<Maticsoft.Model.Shop.Products.ProductImage> ProductImageDtToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductImage> list = new List<Maticsoft.Model.Shop.Products.ProductImage>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductImage item = new Maticsoft.Model.Shop.Products.ProductImage();
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != ""))
                    {
                        item.ThumbnailUrl1 = dt.Rows[i]["ThumbnailUrl1"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl2"] != null) && (dt.Rows[i]["ThumbnailUrl2"].ToString() != ""))
                    {
                        item.ThumbnailUrl2 = dt.Rows[i]["ThumbnailUrl2"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl3"] != null) && (dt.Rows[i]["ThumbnailUrl3"].ToString() != ""))
                    {
                        item.ThumbnailUrl3 = dt.Rows[i]["ThumbnailUrl3"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.Shop.Products.ProductImage> ProductImagesList(long productId)
        {
            DataSet set = this.dal.ProductImagesList(productId);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                return this.ProductImageDtToList(set.Tables[0]);
            }
            return null;
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductImage model)
        {
            return this.dal.Update(model);
        }
    }
}

