namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class CategoryInfo
    {
        private readonly ICategoryInfo dal = DAShopProducts.CreateCategoryInfo();

        public int Add(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            return this.dal.Add(model);
        }

        public bool CreateCategory(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo info = this.GetModel(model.ParentCategoryId);
            if (info != null)
            {
                model.Depth = info.Depth + 1;
            }
            else
            {
                model.Depth = 1;
            }
            model.DisplaySequence = this.GetMaxSeqByCid(model.ParentCategoryId) + 1;
            model.Path = "";
            model.CategoryId = this.dal.Add(model);
            if (model.CategoryId <= 0)
            {
                return false;
            }
            if (info != null)
            {
                this.UpdateHasChild(info.CategoryId);
                model.Path = info.Path + "|" + model.CategoryId;
            }
            else
            {
                model.Path = model.CategoryId.ToString();
            }
            return this.dal.UpdatePath(model);
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> list = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.CategoryInfo item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int CategoryId)
        {
            return this.dal.Delete(CategoryId);
        }

        public DataSet DeleteCategory(int categoryId, out int Result)
        {
            return this.dal.DeleteCategory(categoryId, out Result);
        }

        public bool DeleteList(string CategoryIdlist)
        {
            return this.dal.DeleteList(CategoryIdlist);
        }

        public bool DisplaceCategory(int FromCategoryId, int ToCategoryId)
        {
            return this.dal.DisplaceCategory(FromCategoryId, ToCategoryId);
        }

        public bool Exists(int CategoryId)
        {
            return this.dal.Exists(CategoryId);
        }

        public static List<Maticsoft.Model.Shop.Products.CategoryInfo> GetAllCateList()
        {
            string cacheKey = "GetAllCateList-CateList";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    Maticsoft.BLL.Shop.Products.CategoryInfo info = new Maticsoft.BLL.Shop.Products.CategoryInfo();
                    DataSet set = info.GetList(-1, "", " DisplaySequence");
                    cache = info.DataTableToList(set.Tables[0]);
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
            return (List<Maticsoft.Model.Shop.Products.CategoryInfo>) cache;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetCategoryListByPath(string path)
        {
            string cacheKey = "GetCategoryListByPath-" + path;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    DataSet categoryListByPath = this.dal.GetCategoryListByPath(path);
                    if (categoryListByPath != null)
                    {
                        cache = this.DataTableToList(categoryListByPath.Tables[0]);
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.Shop.Products.CategoryInfo>) cache;
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetCategorysByDepth(int depth)
        {
            return this.GetModelList("Depth = " + depth);
        }

        public DataSet GetCategorysByDepthDs(int depth)
        {
            return this.GetModelDs("Depth = " + depth);
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetCategorysByParentId(int parentCategoryId, int Top = -1)
        {
            DataSet set = this.GetList(Top, "ParentCategoryId = " + parentCategoryId, " DisplaySequence");
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetCategorysByParentIdDs(int parentCategoryId)
        {
            return this.GetModelDs("ParentCategoryId = " + parentCategoryId);
        }

        public DataSet GetCateList(string strWhere, bool IsOrder)
        {
            return this.dal.GetList(strWhere, IsOrder);
        }

        public int GetDepthByCid(int parentId)
        {
            return this.dal.GetDepthByCid(parentId);
        }

        public string GetFullNameByCache(int categoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo modelByCache = this.GetModelByCache(categoryId);
            if (modelByCache == null)
            {
                return null;
            }
            string[] strArray = modelByCache.Path.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            int length = strArray.Length;
            string[] strArray2 = new string[length];
            for (int i = 0; i < length; i++)
            {
                modelByCache = this.GetModelByCache(Globals.SafeInt(strArray[i], 0));
                if (modelByCache != null)
                {
                    strArray2[i] = modelByCache.Name;
                }
            }
            return string.Join(" &raquo; ", strArray2);
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

        public int GetMaxSeqByCid(int parentId)
        {
            return this.dal.GetMaxSeqByCid(parentId);
        }

        public Maticsoft.Model.Shop.Products.CategoryInfo GetModel(int CategoryId)
        {
            return this.dal.GetModel(CategoryId);
        }

        public Maticsoft.Model.Shop.Products.CategoryInfo GetModelByCache(int CategoryId)
        {
            string cacheKey = "CategoryInfoModel-" + CategoryId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CategoryId);
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
            return (Maticsoft.Model.Shop.Products.CategoryInfo) cache;
        }

        public DataSet GetModelDs(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public Maticsoft.Model.Shop.Products.CategoryInfo GetModelEx(int CategoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo model = this.GetModel(CategoryId);
            if (model != null)
            {
                model.NamePath = this.GetNamePathByPath(model.Path);
            }
            return model;
        }

        public Maticsoft.Model.Shop.Products.CategoryInfo GetModelExCache(int CategoryId)
        {
            string cacheKey = "GetModelExCache-" + CategoryId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelEx(CategoryId);
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
            return (Maticsoft.Model.Shop.Products.CategoryInfo) cache;
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public string GetNameByPid(long productId)
        {
            DataSet nameByPid = this.dal.GetNameByPid(productId);
            List<Maticsoft.Model.Shop.Products.CategoryInfo> list = this.NameTableToList(nameByPid.Tables[0]);
            if ((list == null) || (list.Count <= 0))
            {
                return "";
            }
            return string.Join(",", (IEnumerable<string>) (from c in list select c.Name));
        }

        public string GetNamePathByPath(string path)
        {
            return this.dal.GetNamePathByPath(path);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool IsExisted(int parentId, string name, int categoryId = 0)
        {
            return this.dal.IsExisted(parentId, name, categoryId);
        }

        public bool IsExistedProduce(int category)
        {
            return this.dal.IsExistedProduce(category);
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> MainCategoryList(int? parentId)
        {
            string strWhere = string.Format(" ParentCategoryId={0} ORDER BY DisplaySequence ASC", parentId.HasValue ? parentId.Value : 0);
            DataSet list = this.dal.GetList(strWhere);
            if ((list != null) && (list.Tables[0].Rows.Count > 0))
            {
                return this.DataTableToList(list.Tables[0]);
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> NameTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> list = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.CategoryInfo item = new Maticsoft.Model.Shop.Products.CategoryInfo();
                    if ((dt.Rows[i]["Name"] != null) && (dt.Rows[i]["Name"].ToString() != ""))
                    {
                        item.Name = dt.Rows[i]["Name"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool SwapCategorySequence(int CategoryId, SwapSequenceIndex zIndex)
        {
            return this.dal.SwapCategorySequence(CategoryId, zIndex);
        }

        public bool Update(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCategory(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo info = this.GetModel(model.ParentCategoryId);
            string path = model.Path;
            if (info != null)
            {
                model.Depth = info.Depth + 1;
                model.Path = info.Path + "|" + model.CategoryId;
            }
            else
            {
                model.Depth = 1;
                model.Path = model.CategoryId.ToString();
            }
            if (!this.Update(model))
            {
                return false;
            }
            if (info != null)
            {
                this.UpdateHasChild(info.CategoryId);
            }
            List<Maticsoft.Model.Shop.Products.CategoryInfo> collection = (from c in this.GetModelList(" Path Like '" + path + "|%'")
                orderby c.Depth
                select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> source = new List<Maticsoft.Model.Shop.Products.CategoryInfo> {
                model
            };
            source.AddRange(collection);
            using (List<Maticsoft.Model.Shop.Products.CategoryInfo>.Enumerator enumerator = collection.GetEnumerator())
            {
                Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
                Maticsoft.Model.Shop.Products.CategoryInfo item;
                while (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    if (predicate == null)
                    {
                        predicate = c => c.CategoryId == item.ParentCategoryId;
                    }
                    Maticsoft.Model.Shop.Products.CategoryInfo info2 = source.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                    if (info2 != null)
                    {
                        item.Depth = info2.Depth + 1;
                        item.Path = info2.Path + "|" + item.CategoryId;
                    }
                    else
                    {
                        item.Depth = 1;
                        item.Path = item.CategoryId.ToString();
                    }
                    this.UpdateDepthAndPath(item.CategoryId, item.Depth, item.Path);
                }
            }
            return true;
        }

        public bool UpdateDepthAndPath(int Cid, int Depth, string Path)
        {
            return this.dal.UpdateDepthAndPath(Cid, Depth, Path);
        }

        public bool UpdateHasChild(int cid)
        {
            return this.dal.UpdateHasChild(cid);
        }

        public bool UpdateSeqByCid(int Seq, int Cid)
        {
            return this.dal.UpdateSeqByCid(Seq, Cid);
        }
    }
}

