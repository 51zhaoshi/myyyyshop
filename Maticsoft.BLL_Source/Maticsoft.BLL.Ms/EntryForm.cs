namespace Maticsoft.BLL.Ms
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class EntryForm
    {
        private readonly IEntryForm dal = DAMembers.CreateEntryForm();

        public int Add(Maticsoft.Model.Ms.EntryForm model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Ms.EntryForm> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.EntryForm> list = new List<Maticsoft.Model.Ms.EntryForm>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.EntryForm item = new Maticsoft.Model.Ms.EntryForm();
                    if ((dt.Rows[i]["Id"] != null) && (dt.Rows[i]["Id"].ToString() != ""))
                    {
                        item.Id = int.Parse(dt.Rows[i]["Id"].ToString());
                    }
                    if ((dt.Rows[i]["UserName"] != null) && (dt.Rows[i]["UserName"].ToString() != ""))
                    {
                        item.UserName = dt.Rows[i]["UserName"].ToString();
                    }
                    if ((dt.Rows[i]["Age"] != null) && (dt.Rows[i]["Age"].ToString() != ""))
                    {
                        item.Age = new int?(int.Parse(dt.Rows[i]["Age"].ToString()));
                    }
                    if ((dt.Rows[i]["Email"] != null) && (dt.Rows[i]["Email"].ToString() != ""))
                    {
                        item.Email = dt.Rows[i]["Email"].ToString();
                    }
                    if ((dt.Rows[i]["TelPhone"] != null) && (dt.Rows[i]["TelPhone"].ToString() != ""))
                    {
                        item.TelPhone = dt.Rows[i]["TelPhone"].ToString();
                    }
                    if ((dt.Rows[i]["Phone"] != null) && (dt.Rows[i]["Phone"].ToString() != ""))
                    {
                        item.Phone = dt.Rows[i]["Phone"].ToString();
                    }
                    if ((dt.Rows[i]["QQ"] != null) && (dt.Rows[i]["QQ"].ToString() != ""))
                    {
                        item.QQ = dt.Rows[i]["QQ"].ToString();
                    }
                    if ((dt.Rows[i]["MSN"] != null) && (dt.Rows[i]["MSN"].ToString() != ""))
                    {
                        item.MSN = dt.Rows[i]["MSN"].ToString();
                    }
                    if ((dt.Rows[i]["HouseAddress"] != null) && (dt.Rows[i]["HouseAddress"].ToString() != ""))
                    {
                        item.HouseAddress = dt.Rows[i]["HouseAddress"].ToString();
                    }
                    if ((dt.Rows[i]["CompanyAddress"] != null) && (dt.Rows[i]["CompanyAddress"].ToString() != ""))
                    {
                        item.CompanyAddress = dt.Rows[i]["CompanyAddress"].ToString();
                    }
                    if ((dt.Rows[i]["RegionId"] != null) && (dt.Rows[i]["RegionId"].ToString() != ""))
                    {
                        item.RegionId = new int?(int.Parse(dt.Rows[i]["RegionId"].ToString()));
                    }
                    if ((dt.Rows[i]["Sex"] != null) && (dt.Rows[i]["Sex"].ToString() != ""))
                    {
                        item.Sex = dt.Rows[i]["Sex"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["remark"] != null) && (dt.Rows[i]["remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["remark"].ToString();
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = new int?(int.Parse(dt.Rows[i]["State"].ToString()));
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

        public Maticsoft.Model.Ms.EntryForm GetModel(int Id)
        {
            return this.dal.GetModel(Id);
        }

        public Maticsoft.Model.Ms.EntryForm GetModelByCache(int Id)
        {
            string cacheKey = "EntryFormModel-" + Id;
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
            return (Maticsoft.Model.Ms.EntryForm) cache;
        }

        public List<Maticsoft.Model.Ms.EntryForm> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Ms.EntryForm model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }
    }
}

