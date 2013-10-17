namespace Maticsoft.BLL.Shop.Sample
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Sample;
    using Maticsoft.Model.Shop.Sample;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SampleDetail
    {
        private readonly ISampleDetail dal = DAShopSample.CreateSampleDetail();

        public int Add(Maticsoft.Model.Shop.Sample.SampleDetail model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Sample.SampleDetail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Sample.SampleDetail> list = new List<Maticsoft.Model.Shop.Sample.SampleDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Sample.SampleDetail item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
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

        public Maticsoft.Model.Shop.Sample.SampleDetail GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Shop.Sample.SampleDetail GetModelByCache(int ID)
        {
            string cacheKey = "SampleDetailModel-" + ID;
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
            return (Maticsoft.Model.Shop.Sample.SampleDetail) cache;
        }

        public List<Maticsoft.Model.Shop.Sample.SampleDetail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Sample.SampleDetail model)
        {
            return this.dal.Update(model);
        }
    }
}

