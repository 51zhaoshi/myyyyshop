namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PhotoClass
    {
        private readonly IPhotoClass dal = DACMS.CreatePhotoClass();

        public void Add(Maticsoft.Model.CMS.PhotoClass model)
        {
            this.dal.Add(model);
        }

        public List<Maticsoft.Model.CMS.PhotoClass> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.PhotoClass> list = new List<Maticsoft.Model.CMS.PhotoClass>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.PhotoClass item = new Maticsoft.Model.CMS.PhotoClass();
                    if ((dt.Rows[i]["ClassID"] != null) && (dt.Rows[i]["ClassID"].ToString() != ""))
                    {
                        item.ClassID = int.Parse(dt.Rows[i]["ClassID"].ToString());
                    }
                    if ((dt.Rows[i]["ClassName"] != null) && (dt.Rows[i]["ClassName"].ToString() != ""))
                    {
                        item.ClassName = dt.Rows[i]["ClassName"].ToString();
                    }
                    if ((dt.Rows[i]["ParentId"] != null) && (dt.Rows[i]["ParentId"].ToString() != ""))
                    {
                        item.ParentId = new int?(int.Parse(dt.Rows[i]["ParentId"].ToString()));
                    }
                    if ((dt.Rows[i]["Sequence"] != null) && (dt.Rows[i]["Sequence"].ToString() != ""))
                    {
                        item.Sequence = new int?(int.Parse(dt.Rows[i]["Sequence"].ToString()));
                    }
                    if ((dt.Rows[i]["Path"] != null) && (dt.Rows[i]["Path"].ToString() != ""))
                    {
                        item.Path = dt.Rows[i]["Path"].ToString();
                    }
                    if ((dt.Rows[i]["Depth"] != null) && (dt.Rows[i]["Depth"].ToString() != ""))
                    {
                        item.Depth = new int?(int.Parse(dt.Rows[i]["Depth"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ClassID)
        {
            return this.dal.Delete(ClassID);
        }

        public bool DeleteList(string ClassIDlist)
        {
            return this.dal.DeleteList(ClassIDlist);
        }

        public bool Exists(int ClassID)
        {
            return this.dal.Exists(ClassID);
        }

        public bool ExistsByClassName(string ClassName)
        {
            return this.dal.ExistsByClassName(ClassName);
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

        public int GetMaxSequence()
        {
            return this.dal.GetMaxSequence();
        }

        public Maticsoft.Model.CMS.PhotoClass GetModel(int ClassID)
        {
            return this.dal.GetModel(ClassID);
        }

        public Maticsoft.Model.CMS.PhotoClass GetModelByCache(int ClassID)
        {
            string cacheKey = "PhotoClassModel-" + ClassID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ClassID);
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
            return (Maticsoft.Model.CMS.PhotoClass) cache;
        }

        public List<Maticsoft.Model.CMS.PhotoClass> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.PhotoClass> GetTopList(int Top, string strWhere, string filedOrder)
        {
            return this.DataTableToList(this.dal.GetList(Top, strWhere, filedOrder).Tables[0]);
        }

        public bool Update(Maticsoft.Model.CMS.PhotoClass model)
        {
            return this.dal.Update(model);
        }
    }
}

