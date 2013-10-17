namespace Maticsoft.BLL.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SEORelation
    {
        private readonly ISEORelation dal = DASettings.CreateSEORelation();

        public int Add(Maticsoft.Model.Settings.SEORelation model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Settings.SEORelation> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.SEORelation> list = new List<Maticsoft.Model.Settings.SEORelation>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Settings.SEORelation item = new Maticsoft.Model.Settings.SEORelation();
                    if ((dt.Rows[i]["RelationID"] != null) && (dt.Rows[i]["RelationID"].ToString() != ""))
                    {
                        item.RelationID = int.Parse(dt.Rows[i]["RelationID"].ToString());
                    }
                    if ((dt.Rows[i]["KeyName"] != null) && (dt.Rows[i]["KeyName"].ToString() != ""))
                    {
                        item.KeyName = dt.Rows[i]["KeyName"].ToString();
                    }
                    if ((dt.Rows[i]["LinkURL"] != null) && (dt.Rows[i]["LinkURL"].ToString() != ""))
                    {
                        item.LinkURL = dt.Rows[i]["LinkURL"].ToString();
                    }
                    if ((dt.Rows[i]["IsCMS"] != null) && (dt.Rows[i]["IsCMS"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsCMS"].ToString() == "1") || (dt.Rows[i]["IsCMS"].ToString().ToLower() == "true"))
                        {
                            item.IsCMS = true;
                        }
                        else
                        {
                            item.IsCMS = false;
                        }
                    }
                    if ((dt.Rows[i]["IsShop"] != null) && (dt.Rows[i]["IsShop"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsShop"].ToString() == "1") || (dt.Rows[i]["IsShop"].ToString().ToLower() == "true"))
                        {
                            item.IsShop = true;
                        }
                        else
                        {
                            item.IsShop = false;
                        }
                    }
                    if ((dt.Rows[i]["IsSNS"] != null) && (dt.Rows[i]["IsSNS"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsSNS"].ToString() == "1") || (dt.Rows[i]["IsSNS"].ToString().ToLower() == "true"))
                        {
                            item.IsSNS = true;
                        }
                        else
                        {
                            item.IsSNS = false;
                        }
                    }
                    if ((dt.Rows[i]["IsComment"] != null) && (dt.Rows[i]["IsComment"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsComment"].ToString() == "1") || (dt.Rows[i]["IsComment"].ToString().ToLower() == "true"))
                        {
                            item.IsComment = true;
                        }
                        else
                        {
                            item.IsComment = false;
                        }
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["IsActive"] != null) && (dt.Rows[i]["IsActive"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsActive"].ToString() == "1") || (dt.Rows[i]["IsActive"].ToString().ToLower() == "true"))
                        {
                            item.IsActive = true;
                        }
                        else
                        {
                            item.IsActive = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int RelationID)
        {
            return this.dal.Delete(RelationID);
        }

        public bool DeleteList(string RelationIDlist)
        {
            return this.dal.DeleteList(RelationIDlist);
        }

        public bool Exists(int RelationID)
        {
            return this.dal.Exists(RelationID);
        }

        public bool Exists(string name)
        {
            return this.dal.Exists(name);
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

        public Maticsoft.Model.Settings.SEORelation GetModel(int RelationID)
        {
            return this.dal.GetModel(RelationID);
        }

        public Maticsoft.Model.Settings.SEORelation GetModelByCache(int RelationID)
        {
            string cacheKey = "SEORelationModel-" + RelationID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RelationID);
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
            return (Maticsoft.Model.Settings.SEORelation) cache;
        }

        public List<Maticsoft.Model.Settings.SEORelation> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Settings.SEORelation model)
        {
            return this.dal.Update(model);
        }
    }
}

