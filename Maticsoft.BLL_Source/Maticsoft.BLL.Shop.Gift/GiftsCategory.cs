namespace Maticsoft.BLL.Shop.Gift
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Gift;
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class GiftsCategory
    {
        private readonly IGiftsCategory dal = DAShopGifts.CreateGiftsCategory();

        public int Add(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            return this.dal.Add(model);
        }

        public bool AddCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            return this.dal.AddCategory(model);
        }

        public List<Maticsoft.Model.Shop.Gift.GiftsCategory> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Gift.GiftsCategory> list = new List<Maticsoft.Model.Shop.Gift.GiftsCategory>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Gift.GiftsCategory item = new Maticsoft.Model.Shop.Gift.GiftsCategory();
                    if ((dt.Rows[i]["CategoryID"] != null) && (dt.Rows[i]["CategoryID"].ToString() != ""))
                    {
                        item.CategoryID = int.Parse(dt.Rows[i]["CategoryID"].ToString());
                    }
                    if ((dt.Rows[i]["ParentCategoryId"] != null) && (dt.Rows[i]["ParentCategoryId"].ToString() != ""))
                    {
                        item.ParentCategoryId = new int?(int.Parse(dt.Rows[i]["ParentCategoryId"].ToString()));
                    }
                    if ((dt.Rows[i]["Name"] != null) && (dt.Rows[i]["Name"].ToString() != ""))
                    {
                        item.Name = dt.Rows[i]["Name"].ToString();
                    }
                    if ((dt.Rows[i]["Depth"] != null) && (dt.Rows[i]["Depth"].ToString() != ""))
                    {
                        item.Depth = int.Parse(dt.Rows[i]["Depth"].ToString());
                    }
                    if ((dt.Rows[i]["Path"] != null) && (dt.Rows[i]["Path"].ToString() != ""))
                    {
                        item.Path = dt.Rows[i]["Path"].ToString();
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["Theme"] != null) && (dt.Rows[i]["Theme"].ToString() != ""))
                    {
                        item.Theme = dt.Rows[i]["Theme"].ToString();
                    }
                    if ((dt.Rows[i]["HasChildren"] != null) && (dt.Rows[i]["HasChildren"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["HasChildren"].ToString() == "1") || (dt.Rows[i]["HasChildren"].ToString().ToLower() == "true"))
                        {
                            item.HasChildren = true;
                        }
                        else
                        {
                            item.HasChildren = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int CategoryID)
        {
            return this.dal.Delete(CategoryID);
        }

        public bool DeleteCategory(int categoryId)
        {
            return this.dal.DeleteCategory(categoryId);
        }

        public bool DeleteList(string CategoryIDlist)
        {
            return this.dal.DeleteList(CategoryIDlist);
        }

        public bool Exists(int CategoryID)
        {
            return this.dal.Exists(CategoryID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetCategoryList(string strWhere)
        {
            return this.dal.GetCategoryList(strWhere);
        }

        public List<Maticsoft.Model.Shop.Gift.GiftsCategory> GetCategorysByDepth(int depth)
        {
            return this.GetModelList("Depth = " + depth);
        }

        public DataSet GetCategorysByParentId(int parentCategoryId)
        {
            return this.GetList("ParentCategoryId = " + parentCategoryId);
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

        public Maticsoft.Model.Shop.Gift.GiftsCategory GetModel(int CategoryID)
        {
            return this.dal.GetModel(CategoryID);
        }

        public Maticsoft.Model.Shop.Gift.GiftsCategory GetModelByCache(int CategoryID)
        {
            string cacheKey = "GiftsCategoryModel-" + CategoryID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CategoryID);
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
            return (Maticsoft.Model.Shop.Gift.GiftsCategory) cache;
        }

        public List<Maticsoft.Model.Shop.Gift.GiftsCategory> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool IsExistedGift(int categoryid)
        {
            Maticsoft.BLL.Shop.Gift.Gifts gifts = new Maticsoft.BLL.Shop.Gift.Gifts();
            return (gifts.GetRecordCount("CategoryID=" + categoryid) > 0);
        }

        public bool SwapSequence(int CategoryId, SwapSequenceIndex zIndex)
        {
            return this.dal.SwapSequence(CategoryId, zIndex);
        }

        public bool Update(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            return this.dal.UpdateCategory(model);
        }
    }
}

