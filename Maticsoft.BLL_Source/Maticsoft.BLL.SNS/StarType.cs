namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class StarType
    {
        private readonly IStarType dal = DASNS.CreateStarType();

        public int Add(Maticsoft.Model.SNS.StarType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.StarType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.StarType> list = new List<Maticsoft.Model.SNS.StarType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.StarType item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int TypeID)
        {
            return this.dal.Delete(TypeID);
        }

        public bool DeleteList(string TypeIDlist)
        {
            return this.dal.DeleteList(TypeIDlist);
        }

        public bool Exists(string TypeName)
        {
            return this.dal.Exists(TypeName);
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

        public Maticsoft.Model.SNS.StarType GetModel(int TypeID)
        {
            return this.dal.GetModel(TypeID);
        }

        public Maticsoft.Model.SNS.StarType GetModelByCache(int TypeID)
        {
            string cacheKey = "StarTypeModel-" + TypeID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TypeID);
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
            return (Maticsoft.Model.SNS.StarType) cache;
        }

        public List<Maticsoft.Model.SNS.StarType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.StarType model)
        {
            return this.dal.Update(model);
        }
    }
}

