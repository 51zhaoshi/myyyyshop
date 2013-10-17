namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Tags
    {
        private readonly ITags dal = DASNS.CreateTags();

        public int Add(Maticsoft.Model.SNS.Tags model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.Tags> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Tags> list = new List<Maticsoft.Model.SNS.Tags>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Tags item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int TagID)
        {
            return this.dal.Delete(TagID);
        }

        public bool DeleteList(string TagIDlist)
        {
            return this.dal.DeleteList(TagIDlist);
        }

        public bool Exists(int TypeId, string TagName)
        {
            return this.dal.Exists(TypeId, TagName);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.Tags> GetHotTags(int top)
        {
            return this.DataTableToList(this.dal.GetHotTags(top).Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Tags> GetList(int TypeId)
        {
            return this.DataTableToList(this.dal.GetList("Status=1 and TypeId=" + TypeId).Tables[0]);
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

        public DataSet GetListEx(string strWhere)
        {
            return this.dal.GetListEx(0, strWhere, "");
        }

        public Maticsoft.Model.SNS.Tags GetModel(int TagID)
        {
            return this.dal.GetModel(TagID);
        }

        public Maticsoft.Model.SNS.Tags GetModelByCache(int TagID)
        {
            string cacheKey = "TagsModel-" + TagID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TagID);
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
            return (Maticsoft.Model.SNS.Tags) cache;
        }

        public List<Maticsoft.Model.SNS.Tags> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string Keywords)
        {
            string strWhere = "";
            if (Keywords.Length > 0)
            {
                strWhere = string.Format(" TagName like '%{0}%' ", Keywords);
            }
            return this.dal.GetListEx(0, strWhere, "");
        }

        public string GetTagStr(string Des)
        {
            string str = "";
            foreach (Match match in Regex.Matches(Des, this.GetTagUnionStrByCache()))
            {
                str = match.Value + "|";
            }
            if (!string.IsNullOrEmpty(str))
            {
                return str.TrimEnd(new char[] { '|' });
            }
            return "";
        }

        public string GetTagUnionStrByCache()
        {
            string cacheKey = "TagUnionStr";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    string[] strArray = (from item in this.GetModelList("") select item.TagName).ToArray<string>();
                    cache = string.Join("|", strArray);
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
            if (cache == null)
            {
                return "";
            }
            return cache.ToString();
        }

        public bool Update(Maticsoft.Model.SNS.Tags model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            return this.dal.UpdateIsRecommand(IsRecommand, IdList);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            return this.dal.UpdateStatus(Status, IdList);
        }
    }
}

