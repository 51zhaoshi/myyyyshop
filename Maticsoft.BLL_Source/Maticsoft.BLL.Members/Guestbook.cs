namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Guestbook
    {
        private readonly IGuestbook dal = DataAccess<IGuestbook>.Create("Members.Guestbook");

        public int Add(Maticsoft.Model.Members.Guestbook model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.Guestbook> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.Guestbook> list = new List<Maticsoft.Model.Members.Guestbook>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.Guestbook item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public Maticsoft.Model.Members.Guestbook GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Members.Guestbook GetModelByCache(int ID)
        {
            string cacheKey = "GuestbookModel-" + ID;
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
            return (Maticsoft.Model.Members.Guestbook) cache;
        }

        public List<Maticsoft.Model.Members.Guestbook> GetModelList(int top)
        {
            DataSet set = this.dal.GetList(top, " Status=1 ", " ID desc ");
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Members.Guestbook> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Members.Guestbook model)
        {
            return this.dal.Update(model);
        }
    }
}

