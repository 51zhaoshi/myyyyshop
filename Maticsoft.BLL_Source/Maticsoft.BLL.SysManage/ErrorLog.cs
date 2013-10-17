namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ErrorLog
    {
        private static IErrorLog dal = DASysManage.CreateErrorLog();

        public static int Add(Maticsoft.Model.SysManage.ErrorLog model)
        {
            return dal.Add(model);
        }

        public static List<Maticsoft.Model.SysManage.ErrorLog> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SysManage.ErrorLog> list = new List<Maticsoft.Model.SysManage.ErrorLog>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SysManage.ErrorLog item = new Maticsoft.Model.SysManage.ErrorLog();
                    if (dt.Rows[i]["ID"].ToString() != "")
                    {
                        item.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    }
                    if (dt.Rows[i]["OPTime"].ToString() != "")
                    {
                        item.OPTime = DateTime.Parse(dt.Rows[i]["OPTime"].ToString());
                    }
                    item.Url = dt.Rows[i]["Url"].ToString();
                    item.Loginfo = dt.Rows[i]["Loginfo"].ToString();
                    item.StackTrace = dt.Rows[i]["StackTrace"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static void Delete(string IDList)
        {
            dal.Delete(IDList);
        }

        public static void DeleteByDate(DateTime dtDateBefore)
        {
            dal.DeleteByDate(dtDateBefore);
        }

        public static DataSet GetAllList()
        {
            return GetList(-1, "", "ID desc");
        }

        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        public static Maticsoft.Model.SysManage.ErrorLog GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static Maticsoft.Model.SysManage.ErrorLog GetModelByCache(int ID)
        {
            string cacheKey = "ErrorLogModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = dal.GetModel(ID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SysManage.ErrorLog) cache;
        }

        public static List<Maticsoft.Model.SysManage.ErrorLog> GetModelList(string strWhere)
        {
            return DataTableToList(dal.GetList(strWhere).Tables[0]);
        }

        public static void Update(Maticsoft.Model.SysManage.ErrorLog model)
        {
            dal.Update(model);
        }
    }
}

