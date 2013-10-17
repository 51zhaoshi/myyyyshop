namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HotKeyword
    {
        private readonly IHotKeyword dal = DAShopProducts.CreateHotKeyword();

        public int Add(Maticsoft.Model.Shop.Products.HotKeyword model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.HotKeyword> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.HotKeyword> list = new List<Maticsoft.Model.Shop.Products.HotKeyword>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.HotKeyword item = new Maticsoft.Model.Shop.Products.HotKeyword();
                    if ((dt.Rows[i]["Id"] != null) && (dt.Rows[i]["Id"].ToString() != ""))
                    {
                        item.Id = int.Parse(dt.Rows[i]["Id"].ToString());
                    }
                    if ((dt.Rows[i]["Keywords"] != null) && (dt.Rows[i]["Keywords"].ToString() != ""))
                    {
                        item.Keywords = dt.Rows[i]["Keywords"].ToString();
                    }
                    if ((dt.Rows[i]["CategoryId"] != null) && (dt.Rows[i]["CategoryId"].ToString() != ""))
                    {
                        item.CategoryId = new int?(int.Parse(dt.Rows[i]["CategoryId"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int Id)
        {
            return this.dal.Delete(Id);
        }

        public bool DeleteList(string Idlist)
        {
            return this.dal.DeleteList(Idlist);
        }

        public bool Exists(int Id)
        {
            return this.dal.Exists(Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.Shop.Products.HotKeyword> GetKeywordsList(int Cid, int Top)
        {
            string strWhere = "";
            if (Cid > 0)
            {
                strWhere = " CategoryId=" + Cid;
            }
            DataSet set = this.GetList(Top, strWhere, " Id");
            return this.DataTableToList(set.Tables[0]);
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

        public DataSet GetListLeftjoinCategories(string strWhere)
        {
            return this.dal.GetListLeftjoinCategories(strWhere);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.HotKeyword GetModel(int Id)
        {
            return this.dal.GetModel(Id);
        }

        public Maticsoft.Model.Shop.Products.HotKeyword GetModelByCache(int Id)
        {
            string cacheKey = "HotKeywordsModel-" + Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Id);
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
            return (Maticsoft.Model.Shop.Products.HotKeyword) cache;
        }

        public List<Maticsoft.Model.Shop.Products.HotKeyword> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.HotKeyword model)
        {
            return this.dal.Update(model);
        }
    }
}

