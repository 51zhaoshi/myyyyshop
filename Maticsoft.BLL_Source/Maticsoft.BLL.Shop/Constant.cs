namespace Maticsoft.BLL.Shop
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop;
    using Maticsoft.Model.Shop;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Constant
    {
        private readonly IConstant dal = DataAccess<IConstant>.Create("Shop.Constant");

        public bool Add(Maticsoft.Model.Shop.Constant model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Constant> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Constant> list = new List<Maticsoft.Model.Shop.Constant>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Constant item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete()
        {
            return this.dal.Delete();
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

        public Maticsoft.Model.Shop.Constant GetModel()
        {
            return this.dal.GetModel();
        }

        public Maticsoft.Model.Shop.Constant GetModelByCache()
        {
            string cacheKey = "ConstantModel-";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel();
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Shop.Constant) cache;
        }

        public List<Maticsoft.Model.Shop.Constant> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Constant model)
        {
            return this.dal.Update(model);
        }
    }
}

