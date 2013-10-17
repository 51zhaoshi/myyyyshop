namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Content
    {
        private readonly IContent dal = DataAccess<IContent>.Create("CMS.Content");

        public int Add(Maticsoft.Model.CMS.Content model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.CMS.Content> ContentToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Content> list = new List<Maticsoft.Model.CMS.Content>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DataRow row = dt.Rows[i];
                    if (row != null)
                    {
                        Maticsoft.Model.CMS.Content item = new Maticsoft.Model.CMS.Content();
                        if ((row["ContentID"] != null) && (row["ContentID"].ToString() != ""))
                        {
                            item.ContentID = int.Parse(row["ContentID"].ToString());
                        }
                        if (row["Title"] != null)
                        {
                            item.Title = row["Title"].ToString();
                        }
                        if (row["SubTitle"] != null)
                        {
                            item.SubTitle = row["SubTitle"].ToString();
                        }
                        if (row["Summary"] != null)
                        {
                            item.Summary = row["Summary"].ToString();
                        }
                        if (row["Description"] != null)
                        {
                            item.Description = row["Description"].ToString();
                        }
                        if (row["ImageUrl"] != null)
                        {
                            item.ImageUrl = row["ImageUrl"].ToString();
                        }
                        if (row["ThumbImageUrl"] != null)
                        {
                            item.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                        }
                        if (row["NormalImageUrl"] != null)
                        {
                            item.NormalImageUrl = row["NormalImageUrl"].ToString();
                        }
                        if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                        {
                            item.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                        }
                        if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                        {
                            item.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                        }
                        if ((row["LastEditUserID"] != null) && (row["LastEditUserID"].ToString() != ""))
                        {
                            item.LastEditUserID = new int?(int.Parse(row["LastEditUserID"].ToString()));
                        }
                        if ((row["LastEditDate"] != null) && (row["LastEditDate"].ToString() != ""))
                        {
                            item.LastEditDate = new DateTime?(DateTime.Parse(row["LastEditDate"].ToString()));
                        }
                        if (row["LinkUrl"] != null)
                        {
                            item.LinkUrl = row["LinkUrl"].ToString();
                        }
                        if ((row["PvCount"] != null) && (row["PvCount"].ToString() != ""))
                        {
                            item.PvCount = int.Parse(row["PvCount"].ToString());
                        }
                        if ((row["State"] != null) && (row["State"].ToString() != ""))
                        {
                            item.State = int.Parse(row["State"].ToString());
                        }
                        if ((row["ClassID"] != null) && (row["ClassID"].ToString() != ""))
                        {
                            item.ClassID = int.Parse(row["ClassID"].ToString());
                        }
                        if (row["Keywords"] != null)
                        {
                            item.Keywords = row["Keywords"].ToString();
                        }
                        if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                        {
                            item.Sequence = int.Parse(row["Sequence"].ToString());
                        }
                        if ((row["IsRecomend"] != null) && (row["IsRecomend"].ToString() != ""))
                        {
                            if ((row["IsRecomend"].ToString() == "1") || (row["IsRecomend"].ToString().ToLower() == "true"))
                            {
                                item.IsRecomend = true;
                            }
                            else
                            {
                                item.IsRecomend = false;
                            }
                        }
                        if ((row["IsHot"] != null) && (row["IsHot"].ToString() != ""))
                        {
                            if ((row["IsHot"].ToString() == "1") || (row["IsHot"].ToString().ToLower() == "true"))
                            {
                                item.IsHot = true;
                            }
                            else
                            {
                                item.IsHot = false;
                            }
                        }
                        if ((row["IsColor"] != null) && (row["IsColor"].ToString() != ""))
                        {
                            if ((row["IsColor"].ToString() == "1") || (row["IsColor"].ToString().ToLower() == "true"))
                            {
                                item.IsColor = true;
                            }
                            else
                            {
                                item.IsColor = false;
                            }
                        }
                        if ((row["IsTop"] != null) && (row["IsTop"].ToString() != ""))
                        {
                            if ((row["IsTop"].ToString() == "1") || (row["IsTop"].ToString().ToLower() == "true"))
                            {
                                item.IsTop = true;
                            }
                            else
                            {
                                item.IsTop = false;
                            }
                        }
                        if (row["Attachment"] != null)
                        {
                            item.Attachment = row["Attachment"].ToString();
                        }
                        if (row["Remary"] != null)
                        {
                            item.Remary = row["Remary"].ToString();
                        }
                        if ((row["TotalComment"] != null) && (row["TotalComment"].ToString() != ""))
                        {
                            item.TotalComment = int.Parse(row["TotalComment"].ToString());
                        }
                        if ((row["TotalSupport"] != null) && (row["TotalSupport"].ToString() != ""))
                        {
                            item.TotalSupport = int.Parse(row["TotalSupport"].ToString());
                        }
                        if ((row["TotalFav"] != null) && (row["TotalFav"].ToString() != ""))
                        {
                            item.TotalFav = int.Parse(row["TotalFav"].ToString());
                        }
                        if ((row["TotalShare"] != null) && (row["TotalShare"].ToString() != ""))
                        {
                            item.TotalShare = int.Parse(row["TotalShare"].ToString());
                        }
                        if (row["BeFrom"] != null)
                        {
                            item.BeFrom = row["BeFrom"].ToString();
                        }
                        if (row["FileName"] != null)
                        {
                            item.FileName = row["FileName"].ToString();
                        }
                        if (row["Meta_Title"] != null)
                        {
                            item.Meta_Title = row["Meta_Title"].ToString();
                        }
                        if (row["Meta_Description"] != null)
                        {
                            item.Meta_Description = row["Meta_Description"].ToString();
                        }
                        if (row["Meta_Keywords"] != null)
                        {
                            item.Meta_Keywords = row["Meta_Keywords"].ToString();
                        }
                        if (row["SeoUrl"] != null)
                        {
                            item.SeoUrl = row["SeoUrl"].ToString();
                        }
                        if (row["SeoImageAlt"] != null)
                        {
                            item.SeoImageAlt = row["SeoImageAlt"].ToString();
                        }
                        if (row["SeoImageTitle"] != null)
                        {
                            item.SeoImageTitle = row["SeoImageTitle"].ToString();
                        }
                        if (row["StaticUrl"] != null)
                        {
                            item.StaticUrl = row["StaticUrl"].ToString();
                        }
                        if (row["ComCount"] != null)
                        {
                            item.ComCount = Globals.SafeInt(row["ComCount"].ToString(), 0);
                        }
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<Maticsoft.Model.CMS.Content> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Content> list = new List<Maticsoft.Model.CMS.Content>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.Content item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ContentID)
        {
            return this.dal.Delete(ContentID);
        }

        public bool DeleteList(string ContentIDlist)
        {
            return this.dal.DeleteList(ContentIDlist);
        }

        public bool Exists(int ContentID)
        {
            return this.dal.Exists(ContentID);
        }

        public bool ExistsByClassID(int ClassID)
        {
            return this.dal.ExistsByClassID(ClassID);
        }

        public bool ExistTitle(string Title)
        {
            return this.dal.ExistTitle(Title);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public string GetContentUrl(Maticsoft.Model.CMS.Content model)
        {
            if (model == null)
            {
                return "";
            }
            int intValueByCache = ConfigSystem.GetIntValueByCache("CMS_Static_ContentRule");
            switch (intValueByCache)
            {
                case 0:
                    return model.ContentID.ToString();

                case 1:
                    return (PinyinHelper.GetPinyin(model.Title) + "_" + model.ContentID);
            }
            if ((intValueByCache == 2) && !string.IsNullOrWhiteSpace(model.SeoUrl))
            {
                return (model.SeoUrl + "_" + model.ContentID);
            }
            return model.ContentID.ToString();
        }

        public List<Maticsoft.Model.CMS.Content> GetHotCom(string comType, int Top)
        {
            int diffDate = 1;
            string str = comType;
            if (str != null)
            {
                if (!(str == "day"))
                {
                    if (str == "week")
                    {
                        diffDate = 7;
                    }
                }
                else
                {
                    diffDate = 1;
                }
            }
            DataSet hotCom = this.dal.GetHotCom(diffDate, Top);
            return this.ContentToList(hotCom.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetHotComList(int ClassId, int Top = -1)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" State=0 ");
            if (ClassId > 0)
            {
                builder.AppendFormat(" and ClassID={0}", ClassId);
            }
            DataSet set = this.GetList(Top, builder.ToString(), "  TotalComment Desc");
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public List<Maticsoft.Model.CMS.Content> GetList(int? ClassID, int startIndex, int endIndex, string Keywords)
        {
            DataSet ds = this.GetListByPage(ClassID, startIndex, endIndex, Keywords);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.dal.DataTableToListEx(ds.Tables[0]);
        }

        public DataSet GetListByItem(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetListByItem(Top, strWhere, filedOrder);
        }

        public List<Maticsoft.Model.CMS.Content> GetListByPage(int classID, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage(" State=0  and ClassID= " + classID, "  CreatedDate desc   ", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetListByPage(int? ClassID, int startIndex, int endIndex, string Keywords)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("  T.State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND T.ClassID={0} ", ClassID.Value);
                }
                else
                {
                    builder.AppendFormat(" T.ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND T.Title like '%{0}%' ", Keywords);
                }
                else
                {
                    builder.AppendFormat(" T.Title like '%{0}%' ", Keywords);
                }
            }
            return this.GetListByPageEx(builder.ToString(), "", startIndex, endIndex);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public List<Maticsoft.Model.CMS.Content> GetListByPage(int classID, int startIndex, int endIndex, string ImageUrl, int topclass, out int totalCount)
        {
            StringBuilder builder = new StringBuilder();
            if (new Maticsoft.BLL.CMS.ContentClass().GetRecordCount(string.Format(" State=0  and  ParentId={0}  ", classID)) > 0)
            {
                builder.AppendFormat(" EXISTS ( SELECT TOP {0} ClassID   FROM   CMS_ContentClass AS contclas WHERE  State=0  and  ParentId={1} AND contclas.ClassID=T.ClassID  ORDER BY   Sequence  )", topclass, classID);
                if (!string.IsNullOrWhiteSpace(ImageUrl))
                {
                    builder.Append(" and  ImageUrl is not null ");
                }
                totalCount = this.dal.GetRecordCountEx(builder.ToString());
                DataSet set = this.dal.GetListByPage(builder.ToString(), "  CreatedDate desc   ", startIndex, endIndex);
                return this.DataTableToList(set.Tables[0]);
            }
            builder.AppendFormat("  State=0  and  ClassID={0} ", classID);
            if (!string.IsNullOrWhiteSpace(ImageUrl))
            {
                builder.Append(" and  ImageUrl is not null ");
            }
            totalCount = this.dal.GetRecordCountEx(builder.ToString());
            DataSet set2 = this.dal.GetListByPage(builder.ToString(), "  CreatedDate desc   ", startIndex, endIndex);
            return this.DataTableToList(set2.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetListByPage4Menu(int? ClassID, int startIndex, int endIndex, string Keywords)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("  T.State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND T.ClassID={0} ", ClassID.Value);
                }
                else
                {
                    builder.AppendFormat(" T.ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND T.Title like '%{0}%' ", Keywords);
                }
                else
                {
                    builder.AppendFormat(" T.Title like '%{0}%' ", Keywords);
                }
            }
            if (builder.Length > 0)
            {
                builder.Append(" AND ");
            }
            builder.Append(" CMCC.ClassTypeID = 0 ");
            DataSet ds = this.GetListByPageEx(builder.ToString(), "", startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.dal.DataTableToListEx(ds.Tables[0]);
        }

        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageEx(strWhere, orderby, startIndex, endIndex);
        }

        public List<Maticsoft.Model.CMS.Content> GetListByPageEx(int ClassID, int startIndex, int endIndex, string Keywords, int topclass, out int toalCount)
        {
            StringBuilder builder = new StringBuilder();
            if (new Maticsoft.BLL.CMS.ContentClass().GetRecordCount(string.Format(" State=0  and  ParentId={0}  ", ClassID)) > 0)
            {
                builder.AppendFormat(" EXISTS ( SELECT TOP {0} ClassID   FROM   CMS_ContentClass AS contclas WHERE  State=0  and  ParentId={1} AND contclas.ClassID=T.ClassID  ORDER BY   Sequence  ) AND  T.Title like '%{2}%'", topclass, ClassID, Keywords);
                toalCount = this.dal.GetRecordCountEx(builder.ToString());
                DataSet set = this.GetListByPageEx(builder.ToString(), "", startIndex, endIndex);
                return this.DataTableToList(set.Tables[0]);
            }
            builder.AppendFormat("  State=0  and  ClassID={0} ", builder);
            toalCount = this.dal.GetRecordCountEx(builder.ToString());
            DataSet set2 = this.dal.GetListByPage(builder.ToString(), "  CreatedDate desc   ", startIndex, endIndex);
            return this.DataTableToList(set2.Tables[0]);
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

        public Maticsoft.Model.CMS.Content GetModel(int ContentID)
        {
            return this.dal.GetModel(ContentID);
        }

        public Maticsoft.Model.CMS.Content GetModelByCache(int ContentID)
        {
            string cacheKey = "ContentModel-" + ContentID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ContentID);
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
            return (Maticsoft.Model.CMS.Content) cache;
        }

        public Maticsoft.Model.CMS.Content GetModelByCache(int? ContentID, out string className)
        {
            className = "";
            if (!ContentID.HasValue)
            {
                return null;
            }
            Maticsoft.BLL.CMS.ContentClass class2 = new Maticsoft.BLL.CMS.ContentClass();
            this.dal.UpdatePV(ContentID.Value);
            string cacheKey = "ContentModel-" + ContentID.Value;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ContentID.Value);
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
            Maticsoft.Model.CMS.Content content = (Maticsoft.Model.CMS.Content) cache;
            if (content != null)
            {
                className = class2.GetClassnameById(content.ClassID);
            }
            return content;
        }

        public Maticsoft.Model.CMS.Content GetModelByClassIDByCache(int ClassID)
        {
            string cacheKey = "ContentModelClassID-" + ClassID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModelByClassID(ClassID);
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
            return (Maticsoft.Model.CMS.Content) cache;
        }

        public Maticsoft.Model.CMS.Content GetModelByClassIDByCache(int ClassID, out string className)
        {
            className = new Maticsoft.BLL.CMS.ContentClass().GetClassnameById(ClassID);
            string cacheKey = "ContentModelClassID-" + ClassID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModelByClassID(ClassID);
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
            return (Maticsoft.Model.CMS.Content) cache;
        }

        public Maticsoft.Model.CMS.Content GetModelEx(int ContentID)
        {
            return this.dal.GetModelEx(ContentID);
        }

        public Maticsoft.Model.CMS.Content GetModelExByCache(int ContentID)
        {
            string cacheKey = "ContentModelEx-" + ContentID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModelEx(ContentID);
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
            return (Maticsoft.Model.CMS.Content) cache;
        }

        public List<Maticsoft.Model.CMS.Content> GetModelList()
        {
            DataSet ds = this.GetList(10, " State=0 ", " PvCount DESC ");
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetModelList(int ClassID)
        {
            return this.GetModelList(string.Format(" ClassID={0}", ClassID));
        }

        public List<Maticsoft.Model.CMS.Content> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetModelList(int ClassID, int Top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" State=0 ");
            if (ClassID > 0)
            {
                builder.AppendFormat(" and ClassID={0} ", ClassID);
            }
            DataSet set = this.dal.GetList(Top, builder.ToString(), " CreatedDate desc ");
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetModelList(int ClassID, int Top, string ImageUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" State=0 ");
            if (ClassID > 0)
            {
                builder.AppendFormat(" and ClassID={0} ", ClassID);
            }
            if (!string.IsNullOrWhiteSpace(ImageUrl))
            {
                builder.Append(" and  ImageUrl is not null ");
            }
            DataSet set = this.dal.GetList(Top, builder.ToString(), " CreatedDate desc ");
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetModelListEx(int top, int ClassID, string ImageUrl, int topclass)
        {
            StringBuilder builder = new StringBuilder();
            if (new Maticsoft.BLL.CMS.ContentClass().GetRecordCount(string.Format(" State=0  and  ParentId={0}  ", ClassID)) > 0)
            {
                builder.AppendFormat(" EXISTS ( SELECT TOP {0} ClassID   FROM   CMS_ContentClass AS contclas WHERE  State=0  and  ParentId={1} AND contclas.ClassID=cont.ClassID  ORDER BY   Sequence  )", topclass, ClassID);
                if (!string.IsNullOrWhiteSpace(ImageUrl))
                {
                    builder.Append(" and  ImageUrl is not null ");
                }
                DataSet set = this.dal.GetListEx(top, builder.ToString(), " ContentID desc  ");
                return this.DataTableToList(set.Tables[0]);
            }
            builder.AppendFormat("  State=0  and  ClassID={0} ", ClassID);
            if (!string.IsNullOrWhiteSpace(ImageUrl))
            {
                builder.Append(" and  ImageUrl is not null ");
            }
            DataSet set2 = this.dal.GetListEx(top, builder.ToString(), " ContentID desc  ");
            return this.DataTableToList(set2.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetMoreList(int classId, int contentId, int top)
        {
            string strWhere = " State=0 ";
            if (classId > 0)
            {
                strWhere = strWhere + " and ClassID= " + classId;
            }
            if (contentId > 0)
            {
                strWhere = strWhere + " and ContentID<> " + contentId;
            }
            DataSet set = this.dal.GetList(top, strWhere, " Sequence ");
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetNextID(int ContentID, int ClassId = -1)
        {
            return this.dal.GetNextID(ContentID, ClassId);
        }

        public int GetPrevID(int ContentID, int ClassId = -1)
        {
            return this.dal.GetPrevID(ContentID, ClassId);
        }

        public List<Maticsoft.Model.CMS.Content> GetRanList(int ClassID, string keyword, int Top)
        {
            DataSet set = this.dal.GetRanList(ClassID, keyword, Top);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetRecList(int ClassId, EnumHelper.ContentRec Rec, int Top = -1, bool hasImage = false)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" State=0 ");
            string str = "";
            switch (Rec)
            {
                case EnumHelper.ContentRec.Recomend:
                    str = " IsRecomend=1";
                    break;

                case EnumHelper.ContentRec.Hot:
                    str = " IsHot=1";
                    break;

                case EnumHelper.ContentRec.Color:
                    str = " IsColor=1";
                    break;

                case EnumHelper.ContentRec.Top:
                    str = " IsTop=1";
                    break;

                default:
                    str = " IsRecomend=1";
                    break;
            }
            builder.AppendFormat(" and {0}", str);
            if (ClassId > 0)
            {
                builder.AppendFormat(" and ClassID={0}", ClassId);
            }
            if (hasImage)
            {
                builder.Append(" and ImageUrl is not null");
            }
            DataSet set = this.GetList(Top, builder.ToString(), " Sequence");
            List<Maticsoft.Model.CMS.Content> list = this.DataTableToList(set.Tables[0]);
            List<Maticsoft.Model.CMS.ContentClass> allClass = Maticsoft.BLL.CMS.ContentClass.GetAllClass();
            using (List<Maticsoft.Model.CMS.Content>.Enumerator enumerator = list.GetEnumerator())
            {
                Func<Maticsoft.Model.CMS.ContentClass, bool> predicate = null;
                Maticsoft.Model.CMS.Content content;
                while (enumerator.MoveNext())
                {
                    content = enumerator.Current;
                    if (predicate == null)
                    {
                        predicate = c => c.ClassID == content.ClassID;
                    }
                    Maticsoft.Model.CMS.ContentClass class2 = allClass.FirstOrDefault<Maticsoft.Model.CMS.ContentClass>(predicate);
                    if (class2 != null)
                    {
                        content.ClassName = class2.ClassName;
                    }
                }
            }
            return list;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCount(int? ClassID, string Keywords)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND ClassID={0} ", ClassID.Value);
                }
                else
                {
                    builder.AppendFormat(" ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND Title like '%{0}%' ", Keywords);
                }
                else
                {
                    builder.AppendFormat(" Title like '%{0}%' ", Keywords);
                }
            }
            return this.GetRecordCount(builder.ToString());
        }

        public int GetRecordCount4Menu(int? ClassID, string Keywords)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" T.State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND T.ClassID={0} ", ClassID.Value);
                }
                else
                {
                    builder.AppendFormat(" T.ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (builder.Length != 0)
                {
                    builder.AppendFormat(" AND T.Title like '%{0}%' ", Keywords);
                }
                else
                {
                    builder.AppendFormat(" T.Title like '%{0}%' ", Keywords);
                }
            }
            if (builder.Length > 0)
            {
                builder.Append(" AND ");
            }
            builder.Append(" CMCC.ClassTypeID = 0 ");
            return this.dal.GetRecordCount4Menu(builder.ToString());
        }

        public DataSet GetRssList()
        {
            return this.GetList(0, " State=0 ", " CreatedDate DESC ");
        }

        public bool SetColor(int id, bool rec)
        {
            return this.dal.SetColor(id, rec);
        }

        public bool SetColorList(string ids)
        {
            return this.dal.SetColorList(ids);
        }

        public bool SetHot(int id, bool hot)
        {
            return this.dal.SetHot(id, hot);
        }

        public bool SetHotList(string ids)
        {
            return this.dal.SetHotList(ids);
        }

        public bool SetRec(int id, bool rec)
        {
            return this.dal.SetRec(id, rec);
        }

        public bool SetRecList(string ids)
        {
            return this.dal.SetRecList(ids);
        }

        public bool SetTop(int id, bool rec)
        {
            return this.dal.SetTop(id, rec);
        }

        public bool SetTopList(string ids)
        {
            return this.dal.SetTopList(ids);
        }

        public bool Update(Maticsoft.Model.CMS.Content model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateFav(int ContentID)
        {
            return this.dal.UpdateFav(ContentID);
        }

        public bool UpdateList(string IDlist, int State)
        {
            string strWhere = "State=" + State;
            return this.UpdateList(IDlist, strWhere);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }

        public bool UpdateListByIsRecomend(string IDlist, int IsRecomend)
        {
            string strWhere = "IsRecomend=" + IsRecomend;
            return this.dal.UpdateList(IDlist, strWhere);
        }

        public int UpdatePV(int ContentID)
        {
            return this.dal.UpdatePV(ContentID);
        }

        public bool UpdateTotalSupport(int ContentID)
        {
            return this.dal.UpdateTotalSupport(ContentID);
        }
    }
}

