namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ClassType
    {
        private readonly IClassType dal = DataAccess<IClassType>.Create("CMS.ClassType");

        public bool Add(Maticsoft.Model.CMS.ClassType model)
        {
            return this.dal.Add(model);
        }

        public bool Delete(int ClassTypeID)
        {
            return this.dal.Delete(ClassTypeID);
        }

        public bool DeleteList(string ClassTypeIDlist)
        {
            return this.dal.DeleteList(ClassTypeIDlist);
        }

        public bool Exists(int ClassTypeID)
        {
            return this.dal.Exists(ClassTypeID);
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.CMS.ClassType GetModel(int ClassTypeID)
        {
            return this.dal.GetModel(ClassTypeID);
        }

        public Maticsoft.Model.CMS.ClassType GetModelByCache(int ClassTypeID)
        {
            string cacheKey = "ClassTypeModel-" + ClassTypeID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ClassTypeID);
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
            return (Maticsoft.Model.CMS.ClassType) cache;
        }

        public List<Maticsoft.Model.CMS.ClassType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            if (DataSetTools.DataSetIsNull(list))
            {
                return null;
            }
            return this.dal.DataTableToList(list.Tables[0]);
        }

        public bool Update(Maticsoft.Model.CMS.ClassType model)
        {
            return this.dal.Update(model);
        }
    }
}

