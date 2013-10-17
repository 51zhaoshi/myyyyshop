namespace Maticsoft.BLL.Shop.Tags
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Tags;
    using Maticsoft.Model.Shop.Tags;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    public class TagCategories
    {
        private readonly ITagCategories dal = DAShopProducts.CreateTagCategories();

        public int Add(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            return this.dal.Add(model);
        }

        public bool CreateCategory(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            return this.dal.CreateCategory(model);
        }

        public List<Maticsoft.Model.Shop.Tags.TagCategories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Tags.TagCategories> list = new List<Maticsoft.Model.Shop.Tags.TagCategories>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Tags.TagCategories item = new Maticsoft.Model.Shop.Tags.TagCategories();
                    if ((dt.Rows[i]["ID"] != null) && (dt.Rows[i]["ID"].ToString() != ""))
                    {
                        item.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    }
                    if ((dt.Rows[i]["CategoryName"] != null) && (dt.Rows[i]["CategoryName"].ToString() != ""))
                    {
                        item.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    }
                    if ((dt.Rows[i]["ParentCategoryId"] != null) && (dt.Rows[i]["ParentCategoryId"].ToString() != ""))
                    {
                        item.ParentCategoryId = new int?(int.Parse(dt.Rows[i]["ParentCategoryId"].ToString()));
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["Depth"] != null) && (dt.Rows[i]["Depth"].ToString() != ""))
                    {
                        item.Depth = int.Parse(dt.Rows[i]["Depth"].ToString());
                    }
                    if ((dt.Rows[i]["Path"] != null) && (dt.Rows[i]["Path"].ToString() != ""))
                    {
                        item.Path = dt.Rows[i]["Path"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Title"] != null) && (dt.Rows[i]["Meta_Title"].ToString() != ""))
                    {
                        item.Meta_Title = dt.Rows[i]["Meta_Title"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Description"] != null) && (dt.Rows[i]["Meta_Description"].ToString() != ""))
                    {
                        item.Meta_Description = dt.Rows[i]["Meta_Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Keywords"] != null) && (dt.Rows[i]["Meta_Keywords"].ToString() != ""))
                    {
                        item.Meta_Keywords = dt.Rows[i]["Meta_Keywords"].ToString();
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
                    if ((dt.Rows[i]["Status"] != null) && (dt.Rows[i]["Status"].ToString() != ""))
                    {
                        item.Status = new int?(int.Parse(dt.Rows[i]["Status"].ToString()));
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public DataSet DeleteTagCategories(int ID, out int Result)
        {
            return this.dal.DeleteTagCategories(ID, out Result);
        }

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.Shop.Tags.TagCategories> GetCategorysByParentId(int parentCategoryId)
        {
            return this.GetModelList("ParentCategoryId = " + parentCategoryId);
        }

        public string GetFullNameByCache(int categoryId)
        {
            Maticsoft.Model.Shop.Tags.TagCategories modelByCache = this.GetModelByCache(categoryId);
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
                    strArray2[i] = modelByCache.CategoryName;
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

        public Maticsoft.Model.Shop.Tags.TagCategories GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Shop.Tags.TagCategories GetModelByCache(int ID)
        {
            string cacheKey = "TagCategoriesModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
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
            return (Maticsoft.Model.Shop.Tags.TagCategories) cache;
        }

        public List<Maticsoft.Model.Shop.Tags.TagCategories> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string Keywords, int Cid = -1)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(Keywords))
            {
                builder.Append(" CategoryName like '" + Keywords + "'");
            }
            if (Cid >= 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  Depth>=0");
            }
            return this.dal.GetList(0, builder.ToString(), "");
        }

        public bool TagCategoriesSequence(int ID, SequenceIndex Index)
        {
            return this.dal.TagCategoriesSequence(ID, Index);
        }

        public bool Update(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            return this.dal.Update(model);
        }
    }
}

