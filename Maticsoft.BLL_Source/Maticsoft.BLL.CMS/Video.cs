namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class Video
    {
        private readonly IVideo dal = DACMS.CreateVideo();

        public int Add(Maticsoft.Model.CMS.Video model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.CMS.Video> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Video> list = new List<Maticsoft.Model.CMS.Video>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.Video item = new Maticsoft.Model.CMS.Video();
                    if ((dt.Rows[i]["VideoID"] != null) && (dt.Rows[i]["VideoID"].ToString() != ""))
                    {
                        item.VideoID = int.Parse(dt.Rows[i]["VideoID"].ToString());
                    }
                    if ((dt.Rows[i]["Title"] != null) && (dt.Rows[i]["Title"].ToString() != ""))
                    {
                        item.Title = dt.Rows[i]["Title"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["AlbumID"] != null) && (dt.Rows[i]["AlbumID"].ToString() != ""))
                    {
                        item.AlbumID = new int?(int.Parse(dt.Rows[i]["AlbumID"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedUserID"] != null) && (dt.Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        item.CreatedUserID = int.Parse(dt.Rows[i]["CreatedUserID"].ToString());
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                    }
                    if ((dt.Rows[i]["LastUpdateUserID"] != null) && (dt.Rows[i]["LastUpdateUserID"].ToString() != ""))
                    {
                        item.LastUpdateUserID = new int?(int.Parse(dt.Rows[i]["LastUpdateUserID"].ToString()));
                    }
                    if ((dt.Rows[i]["LastUpdateDate"] != null) && (dt.Rows[i]["LastUpdateDate"].ToString() != ""))
                    {
                        item.LastUpdateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["LastUpdateDate"].ToString()));
                    }
                    if ((dt.Rows[i]["Sequence"] != null) && (dt.Rows[i]["Sequence"].ToString() != ""))
                    {
                        item.Sequence = int.Parse(dt.Rows[i]["Sequence"].ToString());
                    }
                    if ((dt.Rows[i]["VideoClassID"] != null) && (dt.Rows[i]["VideoClassID"].ToString() != ""))
                    {
                        item.VideoClassID = new int?(int.Parse(dt.Rows[i]["VideoClassID"].ToString()));
                    }
                    if ((dt.Rows[i]["IsRecomend"] != null) && (dt.Rows[i]["IsRecomend"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsRecomend"].ToString() == "1") || (dt.Rows[i]["IsRecomend"].ToString().ToLower() == "true"))
                        {
                            item.IsRecomend = true;
                        }
                        else
                        {
                            item.IsRecomend = false;
                        }
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbImageUrl"] != null) && (dt.Rows[i]["ThumbImageUrl"].ToString() != ""))
                    {
                        item.ThumbImageUrl = dt.Rows[i]["ThumbImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["NormalImageUrl"] != null) && (dt.Rows[i]["NormalImageUrl"].ToString() != ""))
                    {
                        item.NormalImageUrl = dt.Rows[i]["NormalImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["TotalTime"] != null) && (dt.Rows[i]["TotalTime"].ToString() != ""))
                    {
                        item.TotalTime = new int?(int.Parse(dt.Rows[i]["TotalTime"].ToString()));
                    }
                    if ((dt.Rows[i]["TotalComment"] != null) && (dt.Rows[i]["TotalComment"].ToString() != ""))
                    {
                        item.TotalComment = int.Parse(dt.Rows[i]["TotalComment"].ToString());
                    }
                    if ((dt.Rows[i]["TotalFav"] != null) && (dt.Rows[i]["TotalFav"].ToString() != ""))
                    {
                        item.TotalFav = int.Parse(dt.Rows[i]["TotalFav"].ToString());
                    }
                    if ((dt.Rows[i]["TotalUp"] != null) && (dt.Rows[i]["TotalUp"].ToString() != ""))
                    {
                        item.TotalUp = int.Parse(dt.Rows[i]["TotalUp"].ToString());
                    }
                    if ((dt.Rows[i]["Reference"] != null) && (dt.Rows[i]["Reference"].ToString() != ""))
                    {
                        item.Reference = int.Parse(dt.Rows[i]["Reference"].ToString());
                    }
                    if ((dt.Rows[i]["Tags"] != null) && (dt.Rows[i]["Tags"].ToString() != ""))
                    {
                        item.Tags = dt.Rows[i]["Tags"].ToString();
                    }
                    if ((dt.Rows[i]["VideoUrl"] != null) && (dt.Rows[i]["VideoUrl"].ToString() != ""))
                    {
                        item.VideoUrl = dt.Rows[i]["VideoUrl"].ToString();
                    }
                    if ((dt.Rows[i]["UrlType"] != null) && (dt.Rows[i]["UrlType"].ToString() != ""))
                    {
                        item.UrlType = int.Parse(dt.Rows[i]["UrlType"].ToString());
                    }
                    if ((dt.Rows[i]["VideoFormat"] != null) && (dt.Rows[i]["VideoFormat"].ToString() != ""))
                    {
                        item.VideoFormat = dt.Rows[i]["VideoFormat"].ToString();
                    }
                    if ((dt.Rows[i]["Domain"] != null) && (dt.Rows[i]["Domain"].ToString() != ""))
                    {
                        item.Domain = dt.Rows[i]["Domain"].ToString();
                    }
                    if ((dt.Rows[i]["Grade"] != null) && (dt.Rows[i]["Grade"].ToString() != ""))
                    {
                        item.Grade = int.Parse(dt.Rows[i]["Grade"].ToString());
                    }
                    if ((dt.Rows[i]["Attachment"] != null) && (dt.Rows[i]["Attachment"].ToString() != ""))
                    {
                        item.Attachment = dt.Rows[i]["Attachment"].ToString();
                    }
                    if ((dt.Rows[i]["Privacy"] != null) && (dt.Rows[i]["Privacy"].ToString() != ""))
                    {
                        item.Privacy = int.Parse(dt.Rows[i]["Privacy"].ToString());
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = int.Parse(dt.Rows[i]["State"].ToString());
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    if ((dt.Rows[i]["PvCount"] != null) && (dt.Rows[i]["PvCount"].ToString() != ""))
                    {
                        item.PvCount = int.Parse(dt.Rows[i]["PvCount"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int VideoID)
        {
            return this.dal.Delete(VideoID);
        }

        public bool DeleteList(string VideoIDlist)
        {
            return this.dal.DeleteList(VideoIDlist);
        }

        public bool Exists(int VideoID)
        {
            return this.dal.Exists(VideoID);
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

        public List<Maticsoft.Model.CMS.Video> GetListByPage(int startIndex, int endIndex, out int toalCount)
        {
            toalCount = this.GetRecordCount(" State=5  ");
            DataSet set = this.dal.GetListByPage(" State=5 ", " VideoID desc ", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListEx(string strWhere, string orderby)
        {
            return this.dal.GetListEx(strWhere, orderby);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public int GetMaxSequence()
        {
            return this.dal.GetMaxSequence();
        }

        public Maticsoft.Model.CMS.Video GetModel(int VideoID)
        {
            return this.dal.GetModel(VideoID);
        }

        public Maticsoft.Model.CMS.Video GetModelByCache(int VideoID)
        {
            string cacheKey = "VideoModel-" + VideoID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(VideoID);
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
            return (Maticsoft.Model.CMS.Video) cache;
        }

        public Maticsoft.Model.CMS.Video GetModelEx(int VideoID)
        {
            return this.dal.GetModelEx(VideoID);
        }

        public List<Maticsoft.Model.CMS.Video> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Video> GetRecModelList(int top)
        {
            DataSet set = this.dal.GetList(top, " State=5 and IsRecomend=1 ", " VideoID desc ");
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.CMS.Video model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }
    }
}

