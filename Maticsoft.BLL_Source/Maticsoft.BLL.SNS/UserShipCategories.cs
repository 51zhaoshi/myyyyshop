namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserShipCategories
    {
        private readonly IUserShipCategories dal = DASNS.CreateUserShipCategories();

        public int Add(Maticsoft.Model.SNS.UserShipCategories model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.UserShipCategories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserShipCategories> list = new List<Maticsoft.Model.SNS.UserShipCategories>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserShipCategories item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int CategoryID)
        {
            return this.dal.Delete(CategoryID);
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

        public Maticsoft.Model.SNS.UserShipCategories GetModel(int CategoryID)
        {
            return this.dal.GetModel(CategoryID);
        }

        public Maticsoft.Model.SNS.UserShipCategories GetModelByCache(int CategoryID)
        {
            string cacheKey = "UserShipCategoriesModel-" + CategoryID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CategoryID);
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
            return (Maticsoft.Model.SNS.UserShipCategories) cache;
        }

        public List<Maticsoft.Model.SNS.UserShipCategories> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.UserShipCategories model)
        {
            return this.dal.Update(model);
        }
    }
}

