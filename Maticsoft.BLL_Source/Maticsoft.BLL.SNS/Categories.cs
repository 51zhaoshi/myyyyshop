namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.ViewModel.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Categories
    {
        private readonly ICategories dal = DASNS.CreateCategories();

        public int Add(Maticsoft.Model.SNS.Categories model)
        {
            return this.dal.Add(model);
        }

        public bool AddCategories(Maticsoft.Model.SNS.Categories model)
        {
            return this.dal.AddCategories(model);
        }

        public bool AddCategory(Maticsoft.Model.SNS.Categories model)
        {
            return this.dal.AddCategory(model);
        }

        public List<Maticsoft.Model.SNS.Categories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Categories> list = new List<Maticsoft.Model.SNS.Categories>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Categories item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool DeleteCategory(int categoryId)
        {
            return this.dal.DeleteCategory(categoryId);
        }

        public bool DeleteList(string CategoryIdlist)
        {
            return this.dal.DeleteList(CategoryIdlist);
        }

        public bool Exists(int CategoryId)
        {
            return this.dal.Exists(CategoryId);
        }

        public List<Maticsoft.Model.SNS.Categories> GetAllCateByCache(int type)
        {
            string cacheKey = "GetAllCateByCache-" + type;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelList(string.Format(" type={0} ORDER BY Sequence ASC", type));
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.Categories>) cache;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public static List<Maticsoft.Model.SNS.Categories> GetAllList(int type)
        {
            string cacheKey = "GetAllList-" + type;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = new Maticsoft.BLL.SNS.Categories().GetAllCateByCache(type);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.Categories>) cache;
        }

        public ProductCategory GetCacheCateListByParentId(int ParentID)
        {
            string cacheKey = "CacheCateList-" + ParentID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetCateListByParentId(ParentID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (ProductCategory) cache;
        }

        public ProductCategory GetCacheCateListByParentIdEx(int ParentID)
        {
            string cacheKey = "CacheCateListEx-" + ParentID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetCateListByParentIdEx(ParentID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (ProductCategory) cache;
        }

        public DataSet GetCategoryList(string strWhere)
        {
            return this.dal.GetCategoryList(strWhere);
        }

        public List<Maticsoft.Model.SNS.Categories> GetCategoryList(int parentId, int type)
        {
            return this.GetModelList(string.Format("  ParentID ={0} and type={1} ORDER BY Sequence ASC", parentId, type));
        }

        public List<Maticsoft.Model.SNS.Categories> GetCategorysByDepth(int depth, int type)
        {
            return this.GetModelList(string.Concat(new object[] { "Depth = ", depth, " and type=", type }));
        }

        public DataSet GetCategorysByParentId(int parentCategoryId)
        {
            return this.GetList("ParentID = " + parentCategoryId);
        }

        public ProductCategory GetCateListByParentId(int ParentID)
        {
            ProductCategory category = new ProductCategory();
            if (ParentID != 0)
            {
                Maticsoft.Model.SNS.Categories model = this.GetModel(ParentID);
                category.CurrentCateName = (model == null) ? "暂无" : model.Name;
                category.CurrentCid = ParentID;
                foreach (Maticsoft.Model.SNS.Categories categories2 in this.GetModelList("ParentID=" + ParentID))
                {
                    SonCategory item = new SonCategory {
                        ParentModel = categories2,
                        Grandson = this.GetModelList("ParentID=" + categories2.CategoryId)
                    };
                    category.SonList.Add(item);
                }
            }
            return category;
        }

        public ProductCategory GetCateListByParentIdEx(int ParentID)
        {
            ProductCategory category = new ProductCategory();
            if (ParentID != 0)
            {
                Maticsoft.Model.SNS.Categories model = this.GetModel(ParentID);
                category.CurrentCateName = (model == null) ? "暂无" : model.Name;
                category.CurrentCid = ParentID;
                foreach (Maticsoft.Model.SNS.Categories categories2 in this.GetModelList("ParentID=" + ParentID))
                {
                    SonCategory item = new SonCategory {
                        ParentModel = categories2,
                        Grandson = this.DataTableToList(this.GetListByPage("ParentID=" + categories2.CategoryId, "", 1, 5).Tables[0])
                    };
                    category.SonList.Add(item);
                }
            }
            return category;
        }

        public List<ProductCategory> GetChildByMenu()
        {
            List<Maticsoft.Model.SNS.Categories> menuByCategory = this.GetMenuByCategory(-1);
            List<ProductCategory> list2 = new List<ProductCategory>();
            foreach (Maticsoft.Model.SNS.Categories categories in menuByCategory)
            {
                ProductCategory item = new ProductCategory {
                    ParentModel = categories
                };
                List<Maticsoft.Model.SNS.Categories> childrenListById = this.GetChildrenListById(categories.CategoryId);
                item.ChildList = childrenListById.Take<Maticsoft.Model.SNS.Categories>(10).ToList<Maticsoft.Model.SNS.Categories>();
                list2.Add(item);
            }
            return list2;
        }

        public List<Maticsoft.Model.SNS.Categories> GetChildList(int parentId, int top, int type)
        {
            string cacheKey = string.Concat(new object[] { "GetChildList-", parentId, top, type });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    DataSet set = this.dal.GetList(top, string.Format(" ParentID ={0} and type={1}  ", parentId, type), "Sequence ASC");
                    cache = this.DataTableToList(set.Tables[0]);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.Categories>) cache;
        }

        public List<Maticsoft.Model.SNS.Categories> GetChildrenListById(int Cid)
        {
            return this.GetModelList("Depth=3 AND Path LIKE '" + Cid + "|%' ");
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

        public List<Maticsoft.Model.SNS.Categories> GetListByParentId(int parentCategoryId)
        {
            return this.GetModelList("ParentID = " + parentCategoryId);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public List<Maticsoft.Model.SNS.Categories> GetMenuByCategory(int Top = -1)
        {
            string cacheKey = "GetMenuByCategory-" + Top;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    DataSet set = this.GetList(Top, " ParentID=0 and IsMenu=1 and  Type=0", " Sequence");
                    cache = this.DataTableToList(set.Tables[0]);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.Categories>) cache;
        }

        public Maticsoft.Model.SNS.Categories GetModel(int CategoryId)
        {
            return this.dal.GetModel(CategoryId);
        }

        public Maticsoft.Model.SNS.Categories GetModel(string Name)
        {
            List<Maticsoft.Model.SNS.Categories> modelList = this.GetModelList("Name='" + Name + "'");
            if (modelList.Count > 0)
            {
                return modelList[0];
            }
            return null;
        }

        public Maticsoft.Model.SNS.Categories GetModelByCache(int CategoryId)
        {
            string cacheKey = "CategoriesModel-" + CategoryId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CategoryId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.Categories) cache;
        }

        public List<Maticsoft.Model.SNS.Categories> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Categories> GetPhotoMenuCategoryList()
        {
            return this.GetModelList("Type = 1 and  IsMenu=1 ");
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetTopCidByChildCid(int Cid)
        {
            Maticsoft.Model.SNS.Categories model = new Maticsoft.Model.SNS.Categories();
            model = this.GetModel(Cid);
            if (((model == null) || (model.ParentID == 0)) || (model.Depth == 1))
            {
                return Cid;
            }
            string[] strArray = model.Path.Split(new char[] { '|' });
            if (strArray.Length > 0)
            {
                return Globals.SafeInt(strArray[0], 0);
            }
            return 0;
        }

        public string GetTopNameByCid(int Cid)
        {
            Maticsoft.Model.SNS.Categories model = new Maticsoft.Model.SNS.Categories();
            model = this.GetModel(Cid);
            if ((model.ParentID == 0) || (model.Depth == 1))
            {
                if (model != null)
                {
                    return model.Name;
                }
                return "暂无分类";
            }
            string[] strArray = model.Path.Split(new char[] { '|' });
            if (strArray.Length > 0)
            {
                model = this.GetModel(Globals.SafeInt(strArray[0], 0));
                if (model != null)
                {
                    return model.Name;
                }
            }
            return "暂无分类";
        }

        public string GetTopNameByCid(string Name)
        {
            Maticsoft.Model.SNS.Categories model = new Maticsoft.Model.SNS.Categories();
            model = this.GetModel(Name);
            if ((model.ParentID == 0) || (model.Depth == 1))
            {
                if (model != null)
                {
                    return model.Name;
                }
                return "暂无分类";
            }
            string[] strArray = model.Path.Split(new char[] { '|' });
            if (strArray.Length > 0)
            {
                model = this.GetModel(Globals.SafeInt(strArray[0], 0));
                if (model != null)
                {
                    return model.Name;
                }
            }
            return "暂无分类";
        }

        public string GetUrlById(int Id)
        {
            string str = "other";
            Maticsoft.Model.SNS.Categories model = this.GetModel(Id);
            if (model != null)
            {
                string[] strArray = model.Path.Split(new char[] { '|' });
                int num = 0;
                foreach (string str2 in strArray)
                {
                    Maticsoft.Model.SNS.Categories modelByCache = this.GetModelByCache(Globals.SafeInt(str2, 0));
                    if (modelByCache != null)
                    {
                        if (num == 0)
                        {
                            str = PinyinHelper.GetPinyin(modelByCache.Name).ToLower();
                        }
                        else
                        {
                            str = str + "/" + PinyinHelper.GetPinyin(modelByCache.Name).ToLower();
                        }
                    }
                    num++;
                }
            }
            return str;
        }

        public string GetUrlByIdCache(int Id)
        {
            string cacheKey = "GetUrlByPathCache-" + Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetUrlById(Id);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return cache.ToString();
        }

        public bool IsExistedCate(int categoryid)
        {
            Maticsoft.BLL.SNS.Categories categories = new Maticsoft.BLL.SNS.Categories();
            return (categories.GetRecordCount("CategoryID=" + categoryid) > 0);
        }

        public bool SwapCategorySequence(int CategoryId, EnumHelper.SwapSequenceIndex zIndex)
        {
            return this.dal.SwapCategorySequence(CategoryId, zIndex);
        }

        public bool Update(Maticsoft.Model.SNS.Categories model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCategory(Maticsoft.Model.SNS.Categories model)
        {
            return this.dal.UpdateCategory(model);
        }
    }
}

