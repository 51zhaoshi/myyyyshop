namespace Maticsoft.BLL.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class SEORelationManage
    {
        private static Maticsoft.BLL.Settings.SEORelation manage = new Maticsoft.BLL.Settings.SEORelation();

        private static List<Maticsoft.Model.Settings.SEORelation> DataTableToList(DataRow[] dr)
        {
            List<Maticsoft.Model.Settings.SEORelation> list = new List<Maticsoft.Model.Settings.SEORelation>();
            int length = dr.Length;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    Maticsoft.Model.Settings.SEORelation item = new Maticsoft.Model.Settings.SEORelation();
                    if ((dr[i]["RelationID"] != null) && (dr[i]["RelationID"].ToString() != ""))
                    {
                        item.RelationID = int.Parse(dr[i]["RelationID"].ToString());
                    }
                    if ((dr[i]["KeyName"] != null) && (dr[i]["KeyName"].ToString() != ""))
                    {
                        item.KeyName = dr[i]["KeyName"].ToString();
                    }
                    if ((dr[i]["LinkURL"] != null) && (dr[i]["LinkURL"].ToString() != ""))
                    {
                        item.LinkURL = dr[i]["LinkURL"].ToString();
                    }
                    if ((dr[i]["IsCMS"] != null) && (dr[i]["IsCMS"].ToString() != ""))
                    {
                        if ((dr[i]["IsCMS"].ToString() == "1") || (dr[i]["IsCMS"].ToString().ToLower() == "true"))
                        {
                            item.IsCMS = true;
                        }
                        else
                        {
                            item.IsCMS = false;
                        }
                    }
                    if ((dr[i]["IsShop"] != null) && (dr[i]["IsShop"].ToString() != ""))
                    {
                        if ((dr[i]["IsShop"].ToString() == "1") || (dr[i]["IsShop"].ToString().ToLower() == "true"))
                        {
                            item.IsShop = true;
                        }
                        else
                        {
                            item.IsShop = false;
                        }
                    }
                    if ((dr[i]["IsSNS"] != null) && (dr[i]["IsSNS"].ToString() != ""))
                    {
                        if ((dr[i]["IsSNS"].ToString() == "1") || (dr[i]["IsSNS"].ToString().ToLower() == "true"))
                        {
                            item.IsSNS = true;
                        }
                        else
                        {
                            item.IsSNS = false;
                        }
                    }
                    if ((dr[i]["IsComment"] != null) && (dr[i]["IsComment"].ToString() != ""))
                    {
                        if ((dr[i]["IsComment"].ToString() == "1") || (dr[i]["IsComment"].ToString().ToLower() == "true"))
                        {
                            item.IsComment = true;
                        }
                        else
                        {
                            item.IsComment = false;
                        }
                    }
                    if ((dr[i]["CreatedDate"] != null) && (dr[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dr[i]["CreatedDate"].ToString()));
                    }
                    if ((dr[i]["IsActive"] != null) && (dr[i]["IsActive"].ToString() != ""))
                    {
                        if ((dr[i]["IsActive"].ToString() == "1") || (dr[i]["IsActive"].ToString().ToLower() == "true"))
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

        public static string FilterStr(string value, bool isIgnoreCase, Maticsoft.Model.Settings.SEORelation model)
        {
            string str = value;
            if (!string.IsNullOrWhiteSpace(str))
            {
                List<Maticsoft.Model.Settings.SEORelation> modelList = GetModelList(StrWhere(model));
                if ((modelList == null) || (modelList.Count <= 0))
                {
                    return str;
                }
                for (int i = 0; i < modelList.Count; i++)
                {
                    if (isIgnoreCase)
                    {
                        str = Regex.Replace(str, modelList[i].KeyName, string.Format("<a href='" + modelList[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", modelList[i].KeyName), RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        str = str.Replace(modelList[i].KeyName, string.Format("<a href='" + modelList[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", modelList[i].KeyName));
                    }
                }
            }
            return str;
        }

        public static string FilterStr(string value, bool isIgnoreCase, bool IsCMS, bool IsShop, bool IsSNS, bool IsComment, int FlagId, string FlagTitle)
        {
            string str = value;
            if (!string.IsNullOrWhiteSpace(str))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(" IsActive=1 ");
                string cacheKey = string.Format("SEORelationList-{0}-{1}", FlagTitle, FlagId);
                if (IsCMS)
                {
                    builder.AppendFormat(" AND IsCMS=1 ", new object[0]);
                }
                if (IsShop)
                {
                    builder.AppendFormat(" AND IsShop=1 ", new object[0]);
                }
                if (IsSNS)
                {
                    builder.AppendFormat(" AND IsSNS=1 ", new object[0]);
                }
                if (IsComment)
                {
                    builder.AppendFormat(" AND IsComment=1 ", new object[0]);
                }
                object cache = DataCache.GetCache(cacheKey);
                if (cache != null)
                {
                    return (string) cache;
                }
                List<Maticsoft.Model.Settings.SEORelation> modelList = GetModelList(builder.ToString());
                if ((modelList != null) && (modelList.Count > 0))
                {
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        if (isIgnoreCase)
                        {
                            str = Regex.Replace(str, modelList[i].KeyName, string.Format("<a href='" + modelList[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", modelList[i].KeyName), RegexOptions.IgnoreCase);
                        }
                        else
                        {
                            str = str.Replace(modelList[i].KeyName, string.Format("<a href='" + modelList[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", modelList[i].KeyName));
                        }
                    }
                }
                int num2 = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                DataCache.SetCache(cacheKey, str, DateTime.Now.AddMinutes((double) num2), TimeSpan.Zero);
            }
            return str;
        }

        private static List<Maticsoft.Model.Settings.SEORelation> GetModelList(string strWhere)
        {
            string cacheKey = "SEORelationList";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                DataSet list = manage.GetList("");
                if ((list != null) && (list.Tables[0].Rows.Count > 0))
                {
                    int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                    DataCache.SetCache(cacheKey, list, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    return DataTableToList(list.Tables[0].Select(strWhere));
                }
                return null;
            }
            DataSet set2 = (DataSet) cache;
            if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
            {
                return DataTableToList(set2.Tables[0].Select(strWhere));
            }
            return null;
        }

        private static string StrWhere(Maticsoft.Model.Settings.SEORelation model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" IsActive=1 ");
            if (model != null)
            {
                if (model.IsCMS)
                {
                    builder.AppendFormat(" AND IsCMS=1 ", new object[0]);
                }
                if (model.IsShop)
                {
                    builder.AppendFormat(" AND IsShop=1 ", new object[0]);
                }
                if (model.IsSNS)
                {
                    builder.AppendFormat(" AND IsSNS=1 ", new object[0]);
                }
                if (model.IsComment)
                {
                    builder.AppendFormat(" AND IsComment=1 ", new object[0]);
                }
            }
            return builder.ToString();
        }
    }
}

