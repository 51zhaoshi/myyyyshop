namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class GuestBook
    {
        private readonly IGuestBook dal = DASNS.CreateGuestBook();

        public int Add(Maticsoft.Model.SNS.GuestBook model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.GuestBook> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GuestBook> list = new List<Maticsoft.Model.SNS.GuestBook>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.GuestBook item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int GuestBookID)
        {
            return this.dal.Delete(GuestBookID);
        }

        public bool DeleteList(string GuestBookIDlist)
        {
            return this.dal.DeleteList(GuestBookIDlist);
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

        public Maticsoft.Model.SNS.GuestBook GetModel(int GuestBookID)
        {
            return this.dal.GetModel(GuestBookID);
        }

        public Maticsoft.Model.SNS.GuestBook GetModelByCache(int GuestBookID)
        {
            string cacheKey = "GuestBookModel-" + GuestBookID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(GuestBookID);
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
            return (Maticsoft.Model.SNS.GuestBook) cache;
        }

        public List<Maticsoft.Model.SNS.GuestBook> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.GuestBook model)
        {
            return this.dal.Update(model);
        }
    }
}

