namespace Maticsoft.BLL.Shop.Tags
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Tags;
    using Maticsoft.Model.Shop.Tags;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Tags
    {
        private readonly ITags dal = DAShopProducts.CreateTags();

        public int Add(Maticsoft.Model.Shop.Tags.Tags model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Tags.Tags> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Tags.Tags> list = new List<Maticsoft.Model.Shop.Tags.Tags>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Tags.Tags item = new Maticsoft.Model.Shop.Tags.Tags();
                    if ((dt.Rows[i]["TagID"] != null) && (dt.Rows[i]["TagID"].ToString() != ""))
                    {
                        item.TagID = int.Parse(dt.Rows[i]["TagID"].ToString());
                    }
                    if ((dt.Rows[i]["TagCategoryId"] != null) && (dt.Rows[i]["TagCategoryId"].ToString() != ""))
                    {
                        item.TagCategoryId = int.Parse(dt.Rows[i]["TagCategoryId"].ToString());
                    }
                    if ((dt.Rows[i]["TagName"] != null) && (dt.Rows[i]["TagName"].ToString() != ""))
                    {
                        item.TagName = dt.Rows[i]["TagName"].ToString();
                    }
                    if ((dt.Rows[i]["IsRecommand"] != null) && (dt.Rows[i]["IsRecommand"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsRecommand"].ToString() == "1") || (dt.Rows[i]["IsRecommand"].ToString().ToLower() == "true"))
                        {
                            item.IsRecommand = true;
                        }
                        else
                        {
                            item.IsRecommand = false;
                        }
                    }
                    if ((dt.Rows[i]["Status"] != null) && (dt.Rows[i]["Status"].ToString() != ""))
                    {
                        item.Status = int.Parse(dt.Rows[i]["Status"].ToString());
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
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int TagID)
        {
            return this.dal.Delete(TagID);
        }

        public bool DeleteList(string TagIDlist)
        {
            return this.dal.DeleteList(TagIDlist);
        }

        public bool Exists(int TagID)
        {
            return this.dal.Exists(TagID);
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

        public DataSet GetListEx(string strWhere)
        {
            return this.dal.GetListEx(0, strWhere, "");
        }

        public Maticsoft.Model.Shop.Tags.Tags GetModel(int TagID)
        {
            return this.dal.GetModel(TagID);
        }

        public Maticsoft.Model.Shop.Tags.Tags GetModelByCache(int TagID)
        {
            string cacheKey = "TagsModel-" + TagID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TagID);
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
            return (Maticsoft.Model.Shop.Tags.Tags) cache;
        }

        public List<Maticsoft.Model.Shop.Tags.Tags> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Tags.Tags model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateIsRecommand(string IsRecommand, string IdList)
        {
            return this.dal.UpdateIsRecommand(IsRecommand, IdList);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            return this.dal.UpdateStatus(Status, IdList);
        }
    }
}

