namespace Maticsoft.BLL.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Advertisement
    {
        private readonly IAdvertisement dal = DASettings.CreateAdvertisement();

        public bool Add(Maticsoft.Model.Settings.Advertisement model)
        {
            return this.dal.Add(model);
        }

        public string CreateCodeTag(int Aid, int ContentType)
        {
            DataSet set = this.GetTransitionImgByCache(Aid, ContentType, null);
            if (set == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            DataTable table = set.Tables[0];
            if (table.Rows.Count <= 0)
            {
                return "";
            }
            foreach (DataRow row in set.Tables[0].Rows)
            {
                builder.Append(row["AdvHtml"]);
            }
            return builder.ToString();
        }

        public string CreateFlashTag(int Aid, int ContentType)
        {
            DataSet set = this.GetTransitionImgByCache(Aid, ContentType, null);
            if (set != null)
            {
                StringBuilder builder = new StringBuilder();
                DataTable table = set.Tables[0];
                if (table.Rows.Count > 1)
                {
                    foreach (DataRow row in set.Tables[0].Rows)
                    {
                        builder.Append(" <li><a target=\"_blank\" href=\"" + row["NavigateUrl"] + "\">");
                        builder.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\"");
                        builder.Append(string.Concat(new object[] { "width=\"", row["Width"], "\" height=\"", row["Height"], "\"  >" }));
                        builder.Append("<param name=\"wmode\" value=\"opaque\" /><param name=\"quality\" value=\"high\" />");
                        builder.Append("<param name=\"movie\" value=\"" + row["FileUrl"] + "\" />");
                        builder.Append(string.Concat(new object[] { "<embed src=\"", row["FileUrl"], "\" allowfullscreen=\"true\" quality=\"high\" width=\"", row["Width"], "\" height=\"", row["Height"], "\" align=\"middle\" wmode=\"transparent\" allowscriptaccess=\"always\" type=\"application/x-shockwave-flash\"></embed></object></a></li>" }));
                    }
                    return builder.ToString();
                }
                if (table.Rows.Count == 1)
                {
                    builder.Append(" <tr><td><a target=\"_blank\" href=\"" + table.Rows[0]["NavigateUrl"] + "\">");
                    builder.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\"");
                    builder.Append(string.Concat(new object[] { "width=\"", table.Rows[0]["Width"], "\" height=\"", table.Rows[0]["Height"], "\"  ></a></li>" }));
                    builder.Append("<param name=\"wmode\" value=\"opaque\" /><param name=\"quality\" value=\"high\" />");
                    builder.Append("<param name=\"movie\" value=\"" + table.Rows[0]["FileUrl"] + "\" />");
                    builder.Append(string.Concat(new object[] { "<embed src=\"", table.Rows[0]["FileUrl"], "\" allowfullscreen=\"true\" quality=\"high\" width=\"", table.Rows[0]["Width"], "\" height=\"", table.Rows[0]["Height"], "\" align=\"middle\" wmode=\"transparent\" allowscriptaccess=\"always\" type=\"application/x-shockwave-flash\"></embed></object></td></tr>" }));
                    return builder.ToString();
                }
            }
            return "";
        }

        public string CreatePicTag(int Aid, int ContentType, bool type, int? Num, int? snsAD)
        {
            DataSet set = this.GetTransitionImgByCache(Aid, ContentType, Num);
            if (set != null)
            {
                StringBuilder builder = new StringBuilder();
                DataTable table = set.Tables[0];
                if (type)
                {
                    foreach (DataRow row in set.Tables[0].Rows)
                    {
                        builder.Append(string.Concat(new object[] { "<td><a target=\"_blank\" href=\"", row["NavigateUrl"], "\"><img src=\"", row["FileUrl"], "\" style=\"border: none;\" width=\"", row["Width"], "\" height=\"", row["Height"], "\"/></a></td>" }));
                    }
                    return builder.ToString();
                }
                if (snsAD.HasValue && (snsAD.Value == 3))
                {
                    if ((table.Rows == null) || (table.Rows.Count == 0))
                    {
                        return "";
                    }
                    StringBuilder builder2 = new StringBuilder();
                    StringBuilder builder3 = new StringBuilder();
                    StringBuilder builder4 = new StringBuilder();
                    int num = 0;
                    foreach (DataRow row2 in set.Tables[0].Rows)
                    {
                        if (num == 0)
                        {
                            builder2.Append(string.Concat(new object[] { "<td class=\"active\" id=\"t", num, "\" onmouseover=\"Mea(", num, ");clearAuto();\" onmouseout=\"setAuto();\"valign=\"middle\" align=\"center\"> </td>" }));
                            builder3.Append(string.Concat(new object[] { "<div style=\"display: block\"><a target=\"_blank\" href=\"", row2["NavigateUrl"], "\" > <img src=\"", row2["FileUrl"], "\" /></a></div>" }));
                            builder4.Append(string.Concat(new object[] { "<div > <div class=\" conau_left\"><div class=\"p2\"><a target=\"_blank\" href=\"", row2["NavigateUrl"], "\">", row2["AlternateText"], "</a></div></div> </div>" }));
                        }
                        else
                        {
                            builder2.Append(string.Concat(new object[] { "<td width=\"8\"></td><td class=\"bg\" id=\"t", num, "\" onmouseover=\"Mea(", num, ");clearAuto();\" onmouseout=\"setAuto();\"valign=\"middle\" align=\"center\"> </td> " }));
                            builder3.Append(string.Concat(new object[] { "<div style=\"display: none\"><a  target=\"_blank\" href=\"", row2["NavigateUrl"], "\" > <img src=\"", row2["FileUrl"], "\" /></a></div>" }));
                            builder4.Append(string.Concat(new object[] { "<div style=\"display: none\"> <div class=\" conau_left\"><div class=\"p2\"><a target=\"_blank\" href=\"", row2["NavigateUrl"], "\">", row2["AlternateText"], "</a></div></div> </div>" }));
                        }
                        num++;
                    }
                    builder.Append("   <div id=\"focus\" total=\"" + num + "\"><div class=\"lunbo\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"left\"><tr>");
                    builder.Append(builder2).Append("  </tr> </table></div>").Append("<div id=\"au\">").Append(builder3).Append("  </div><div id=\"no\"> </div><div id=\"conau\">");
                    builder.Append(builder4).Append("  </div> </div>");
                    return builder.ToString();
                }
                if (table.Rows.Count > 1)
                {
                    foreach (DataRow row3 in set.Tables[0].Rows)
                    {
                        if (snsAD.HasValue && (snsAD == 2))
                        {
                            builder.Append(" <li><a href=\"" + row3["FileUrl"] + "\">");
                            builder.Append("<img border=\"0\" src=\"" + row3["FileUrl"] + "\"");
                            builder.Append(string.Concat(new object[] { "width=\"", row3["Width"], "\" height=\"", Convert.ToInt32(row3["Height"]) - 0x38, "\"  ></a></li>" }));
                        }
                        else
                        {
                            builder.Append(" <li><a target=\"_blank\" href=\"" + row3["NavigateUrl"] + "\">");
                            builder.Append("<img border=\"0\" src=\"" + row3["FileUrl"] + "\"");
                            builder.Append(string.Concat(new object[] { "width=\"", row3["Width"], "\" height=\"", row3["Height"], "\"  ></a></li>" }));
                        }
                    }
                    return builder.ToString();
                }
                if (table.Rows.Count == 1)
                {
                    builder.Append(" <tr><td><a target=\"_blank\" href=\"" + table.Rows[0]["NavigateUrl"] + "\">");
                    builder.Append("<img border=\"0\" src=\"" + table.Rows[0]["FileUrl"] + "\"");
                    builder.Append(string.Concat(new object[] { "width=\"", table.Rows[0]["Width"], "\" height=\"", table.Rows[0]["Height"], "\"  ></a></td></tr>" }));
                    return builder.ToString();
                }
            }
            return "";
        }

        public string CreateTextTag(int Aid, int ContentType)
        {
            DataSet set = this.GetTransitionImgByCache(Aid, ContentType, null);
            if (set == null)
            {
                return "";
            }
            if (set.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in set.Tables[0].Rows)
            {
                builder.Append(string.Concat(new object[] { "<li><a href=\"", row["NavigateUrl"], "\" target=\"_blank\"> ", row["AlternateText"], "</a></li>" }));
            }
            return builder.ToString();
        }

        public bool Delete(int AdvertisementId)
        {
            return this.dal.Delete(AdvertisementId);
        }

        public bool DeleteList(string AdvertisementIdlist)
        {
            return this.dal.DeleteList(AdvertisementIdlist);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<int> GetContentType(int AdPositionId)
        {
            DataSet contentType = this.dal.GetContentType(AdPositionId);
            List<int> list = null;
            if (contentType != null)
            {
                list = new List<int>();
                foreach (DataRow row in contentType.Tables[0].Rows)
                {
                    list.Add(Convert.ToInt32(row["ContentType"]));
                }
            }
            return list;
        }

        public string GetDefindCode(int Aid)
        {
            DataSet defindCode = this.dal.GetDefindCode(Aid);
            if (defindCode == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            DataTable table = defindCode.Tables[0];
            if (table.Rows.Count <= 0)
            {
                return "";
            }
            foreach (DataRow row in defindCode.Tables[0].Rows)
            {
                builder.Append(row["AdvHtml"]);
            }
            return builder.ToString();
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(new int?(Top), strWhere, filedOrder);
        }

        public List<Maticsoft.Model.Settings.Advertisement> GetListByAidCache(int Aid)
        {
            string cacheKey = "GetListByAidCache-" + Aid;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    DataSet list = this.dal.GetList(string.Format("  State=1 AND   AdvPositionId={0}", Aid));
                    cache = this.dal.DataTableToList(list.Tables[0]);
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
            return (List<Maticsoft.Model.Settings.Advertisement>) cache;
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxSequence()
        {
            return this.dal.GetMaxSequence();
        }

        public Maticsoft.Model.Settings.Advertisement GetModel(int AdvertisementId)
        {
            return this.dal.GetModel(AdvertisementId);
        }

        public Maticsoft.Model.Settings.Advertisement GetModelByAdvPositionId(int AdvPositionId)
        {
            return this.dal.GetModelByAdvPositionId(AdvPositionId);
        }

        public Maticsoft.Model.Settings.Advertisement GetModelByCache(int AdvertisementId)
        {
            string cacheKey = "AdvertisementModel-" + AdvertisementId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AdvertisementId);
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
            return (Maticsoft.Model.Settings.Advertisement) cache;
        }

        public List<Maticsoft.Model.Settings.Advertisement> GetModelList(int Aid)
        {
            int? top = 3;
            string strWhere = string.Format(" State={0} AND ContentType={1}  AND  AdvPositionId={2}", 1, 1, Aid);
            DataSet set = this.dal.GetList(top, strWhere, "");
            return this.dal.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Settings.Advertisement> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.dal.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetTransitionImg(int Aid, int ContentType, int? Num)
        {
            return this.dal.GetTransitionImg(Aid, ContentType, Num);
        }

        public DataSet GetTransitionImgByCache(int Aid, int ContentType, int? Num)
        {
            string cacheKey = string.Concat(new object[] { "GetTransitionImgByCache-", Aid, ContentType, Num.HasValue ? Num.Value : 0 });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetTransitionImg(Aid, ContentType, Num);
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
            return (DataSet) cache;
        }

        public int IsExist(int AdvPositionId, int contentType)
        {
            return this.dal.IsExist(AdvPositionId, contentType);
        }

        public DataSet SelectInfoByContentType(int ContentType)
        {
            return this.dal.SelectInfoByContentType(ContentType);
        }

        public bool Update(Maticsoft.Model.Settings.Advertisement model)
        {
            return this.dal.Update(model);
        }
    }
}

