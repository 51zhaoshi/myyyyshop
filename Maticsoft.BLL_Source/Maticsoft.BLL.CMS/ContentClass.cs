namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class ContentClass
    {
        private readonly IContentClass dal = DataAccess<IContentClass>.Create("CMS.ContentClass");

        public int Add(Maticsoft.Model.CMS.ContentClass model)
        {
            return this.dal.Add(model);
        }

        public bool AddExt(Maticsoft.Model.CMS.ContentClass model)
        {
            return this.dal.AddExt(model);
        }

        public List<Maticsoft.Model.CMS.ContentClass> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.ContentClass> list = new List<Maticsoft.Model.CMS.ContentClass>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.ContentClass item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ClassID)
        {
            return this.dal.Delete(ClassID);
        }

        public bool DeleteCategory(int categoryId)
        {
            return this.dal.DeleteCategory(categoryId);
        }

        public bool DeleteList(string ClassIDlist)
        {
            return this.dal.DeleteList(ClassIDlist);
        }

        public bool Exists(int ClassID)
        {
            return this.dal.Exists(ClassID);
        }

        public string GetAClassnameById(int id)
        {
            Maticsoft.Model.CMS.ContentClass modelByCache = this.GetModelByCache(id);
            if (modelByCache == null)
            {
                return "此栏目已不存在";
            }
            if ((modelByCache.ParentId != 0) && modelByCache.ParentId.HasValue)
            {
                int classID = Convert.ToInt32(modelByCache.ParentId);
                modelByCache = this.GetModel(classID);
            }
            return modelByCache.ClassName;
        }

        public string GetAClassnameById(int id, out int Aclassid)
        {
            Aclassid = -1;
            Maticsoft.Model.CMS.ContentClass modelByCache = this.GetModelByCache(id);
            if (modelByCache == null)
            {
                return "此栏目已不存在";
            }
            if (modelByCache.ParentId == 0)
            {
                Aclassid = modelByCache.ClassID;
                return modelByCache.ClassName;
            }
            if (modelByCache.ParentId.HasValue)
            {
                int classID = Convert.ToInt32(modelByCache.ParentId);
                modelByCache = this.GetModel(classID);
            }
            Aclassid = modelByCache.ClassID;
            return modelByCache.ClassName;
        }

        public static List<Maticsoft.Model.CMS.ContentClass> GetAllClass()
        {
            string cacheKey = "ContentClass-GetAllClass";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = new Maticsoft.BLL.CMS.ContentClass().GetModelList("");
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
            return (List<Maticsoft.Model.CMS.ContentClass>) cache;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetClassIdById(int id)
        {
            Maticsoft.BLL.CMS.ContentClass class2 = new Maticsoft.BLL.CMS.ContentClass();
            Maticsoft.Model.CMS.ContentClass modelByCache = class2.GetModelByCache(id);
            if (modelByCache == null)
            {
                return 0;
            }
            if ((modelByCache.ParentId != 0) && modelByCache.ParentId.HasValue)
            {
                int classID = Convert.ToInt32(modelByCache.ParentId);
                modelByCache = class2.GetModel(classID);
            }
            return modelByCache.ClassID;
        }

        public string GetClassnameById(int id)
        {
            Maticsoft.Model.CMS.ContentClass modelByCache = this.GetModelByCache(id);
            if (modelByCache != null)
            {
                return modelByCache.ClassName;
            }
            return "此栏目已不存在";
        }

        public string GetClassUrl(int classId)
        {
            Maticsoft.Model.CMS.ContentClass modelByCache = this.GetModelByCache(classId);
            if (modelByCache == null)
            {
                return "";
            }
            switch (ConfigSystem.GetIntValueByCache("CMS_Static_ClassRule"))
            {
                case 0:
                    return modelByCache.Path.Replace("|", "/");

                case 1:
                    return this.GetPingYinUrl(modelByCache.Path);

                case 2:
                    return this.GetCustomUrl(modelByCache.Path);
            }
            return modelByCache.Path.Replace("|", "/");
        }

        public string GetCustomUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return "";
            }
            string[] strArray = path.Split(new char[] { ',' });
            string str = "";
            int num = 0;
            foreach (string str2 in strArray)
            {
                Maticsoft.Model.CMS.ContentClass modelByCache = this.GetModelByCache(Globals.SafeInt(str2, 0));
                if (modelByCache == null)
                {
                    return "";
                }
                if (num == 0)
                {
                    str = string.IsNullOrWhiteSpace(modelByCache.IndexChar) ? modelByCache.ClassID.ToString() : modelByCache.IndexChar;
                }
                else
                {
                    str = str + "/" + (string.IsNullOrWhiteSpace(modelByCache.IndexChar) ? modelByCache.ClassID.ToString() : modelByCache.IndexChar);
                }
            }
            return str;
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

        public DataSet GetListByView(string strWhere)
        {
            return this.dal.GetListByView(strWhere);
        }

        public DataSet GetListByView(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetListByView(Top, strWhere, filedOrder);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.CMS.ContentClass GetModel(int ClassID)
        {
            return this.dal.GetModel(ClassID);
        }

        public Maticsoft.Model.CMS.ContentClass GetModelByCache(int ClassID)
        {
            string cacheKey = "ContentClassModel-" + ClassID;
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
            return (Maticsoft.Model.CMS.ContentClass) cache;
        }

        public Maticsoft.Model.CMS.ContentClass GetModelEx(int classId)
        {
            Maticsoft.Model.CMS.ContentClass model = this.GetModel(classId);
            if (model != null)
            {
                model.NamePath = this.GetNamePathByPath(model.Path);
            }
            return model;
        }

        public Maticsoft.Model.CMS.ContentClass GetModelExCache(int classId)
        {
            string cacheKey = "GetModelExCache-" + classId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelEx(classId);
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
            return (Maticsoft.Model.CMS.ContentClass) cache;
        }

        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(int classid, out Maticsoft.Model.CMS.ContentClass classmodel)
        {
            int classIdById = this.GetClassIdById(classid);
            classmodel = this.GetModelByCache(classIdById);
            return this.GetModelList(string.Format(" ParentId={0}", classIdById));
        }

        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(int Top, int? classid, out string classname)
        {
            classname = "此栏目不存在";
            if (!classid.HasValue)
            {
                return null;
            }
            classname = new Maticsoft.BLL.CMS.ContentClass().GetClassnameById(classid.Value);
            List<Maticsoft.Model.CMS.ContentClass> list = this.GetModelList(Top, string.Format("  State=0  and  ParentId in ({0})", classid.Value), " Sequence ");
            if ((list != null) && (list.Count > 0))
            {
                return list;
            }
            return this.GetModelList(1, string.Format("  State=0  and  ClassID ={0}", classid.Value), " Sequence ");
        }

        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(int Top, string strWhere, string filedOrder)
        {
            DataSet set = this.dal.GetList(Top, strWhere, filedOrder);
            return this.DataTableToList(set.Tables[0]);
        }

        public string GetNamePathByPath(string path)
        {
            return this.dal.GetNamePathByPath(path);
        }

        public string GetPingYinUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return "";
            }
            string[] strArray = path.Split(new char[] { ',' });
            string str = "";
            int num = 0;
            foreach (string str2 in strArray)
            {
                Maticsoft.Model.CMS.ContentClass modelByCache = this.GetModelByCache(Globals.SafeInt(str2, 0));
                if (modelByCache == null)
                {
                    return "";
                }
                if (num == 0)
                {
                    str = PinyinHelper.GetPinyin(modelByCache.ClassName).ToLower();
                }
                else
                {
                    str = str + "/" + PinyinHelper.GetPinyin(modelByCache.ClassName).ToLower();
                }
            }
            return str;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetTreeList(string strWhere)
        {
            return this.dal.GetTreeList(strWhere);
        }

        public int SwapCategorySequence(int ContentClassId, SwapSequenceIndex zIndex)
        {
            return this.dal.SwapCategorySequence(ContentClassId, zIndex);
        }

        public bool Update(Maticsoft.Model.CMS.ContentClass model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }
    }
}

