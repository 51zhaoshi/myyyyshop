namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.ViewModel.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    public class TagType
    {
        private readonly ITagType dal = DASNS.CreateTagType();

        public int Add(Maticsoft.Model.SNS.TagType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.TagType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.TagType> list = new List<Maticsoft.Model.SNS.TagType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.TagType item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
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

        public bool Exists(string TypeName)
        {
            return this.dal.Exists(TypeName);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetAllListEX()
        {
            return this.dal.GetAllListEX();
        }

        public List<CType> GetCacheTagListByCid(int Cid)
        {
            string cacheKey = "CacheTagList-" + Cid;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetTagListByCid(Cid, 0);
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
            return (List<CType>) cache;
        }

        public List<CType> GetCacheTagListByCidEx(int Cid)
        {
            string cacheKey = "CacheTagListEx-" + Cid;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetTagListByCidEx(Cid);
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
            return (List<CType>) cache;
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

        public Maticsoft.Model.SNS.TagType GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.TagType GetModelByCache(int ID)
        {
            string cacheKey = "TagTypeModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
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
            return (Maticsoft.Model.SNS.TagType) cache;
        }

        public List<Maticsoft.Model.SNS.TagType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string Keywords, int Cid = -1)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(Keywords))
            {
                builder.Append(" TypeName like '" + Keywords + "'");
            }
            if (Cid >= 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  Cid>=0");
            }
            return this.dal.GetList(0, builder.ToString(), "");
        }

        public List<CType> GetTagListByCid(int Cid, int Top = 0)
        {
            List<CType> list = new List<CType>();
            Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
            foreach (Maticsoft.Model.SNS.TagType type in this.GetModelList(" Cid=" + Cid))
            {
                CType item = new CType {
                    MTagType = type
                };
                List<Maticsoft.Model.SNS.Tags> list3 = new List<Maticsoft.Model.SNS.Tags>();
                list3 = tags.DataTableToList(tags.GetList(Top, "TypeId=" + type.ID, "").Tables[0]);
                item.Taglist = list3;
                list.Add(item);
            }
            return list;
        }

        public List<CType> GetTagListByCidEx(int Cid)
        {
            List<CType> list = new List<CType>();
            Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
            foreach (Maticsoft.Model.SNS.TagType type in this.GetModelList(" Cid=" + Cid))
            {
                CType item = new CType {
                    MTagType = type
                };
                List<Maticsoft.Model.SNS.Tags> list3 = new List<Maticsoft.Model.SNS.Tags>();
                list3 = tags.DataTableToList(tags.GetListByPage("TypeId=" + type.ID, "", 1, 5).Tables[0]);
                item.Taglist = list3;
                list.Add(item);
            }
            return list;
        }

        public int GetTagsTypeId(string TagName)
        {
            List<Maticsoft.Model.SNS.TagType> modelList = this.GetModelList("TypeName='" + TagName + "'");
            if ((modelList != null) && (modelList.Count > 0))
            {
                return modelList[0].ID;
            }
            Maticsoft.Model.SNS.TagType model = new Maticsoft.Model.SNS.TagType {
                TypeName = TagName,
                Cid = -1
            };
            return this.Add(model);
        }

        public bool RelationSNSCate(int tagTypeId, int SNSCategoryId)
        {
            return this.dal.RelationSNSCate(tagTypeId, SNSCategoryId);
        }

        public bool Update(Maticsoft.Model.SNS.TagType model)
        {
            return this.dal.Update(model);
        }
    }
}

