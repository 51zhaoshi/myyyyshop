namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OnLineShopPhoto
    {
        private readonly IOnLineShopPhoto dal = DASNS.CreateOnLineShopPhoto();

        public bool Add(Maticsoft.Model.SNS.OnLineShopPhoto model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.OnLineShopPhoto> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.OnLineShopPhoto> list = new List<Maticsoft.Model.SNS.OnLineShopPhoto>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.OnLineShopPhoto item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int PhotoID, int ProductID)
        {
            return this.dal.Delete(PhotoID, ProductID);
        }

        public bool Exists(int PhotoID, int ProductID)
        {
            return this.dal.Exists(PhotoID, ProductID);
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

        public Maticsoft.Model.SNS.OnLineShopPhoto GetModel(int PhotoID, int ProductID)
        {
            return this.dal.GetModel(PhotoID, ProductID);
        }

        public Maticsoft.Model.SNS.OnLineShopPhoto GetModelByCache(int PhotoID, int ProductID)
        {
            string cacheKey = "OnLineShopPhotoModel-" + PhotoID + ProductID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PhotoID, ProductID);
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
            return (Maticsoft.Model.SNS.OnLineShopPhoto) cache;
        }

        public List<Maticsoft.Model.SNS.OnLineShopPhoto> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.OnLineShopPhoto model)
        {
            return this.dal.Update(model);
        }
    }
}

