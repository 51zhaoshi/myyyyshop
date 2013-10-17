namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class VideoAlbum
    {
        private readonly IVideoAlbum dal = DACMS.CreateVideoAlbum();

        public int Add(Maticsoft.Model.CMS.VideoAlbum model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.CMS.VideoAlbum> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.VideoAlbum> list = new List<Maticsoft.Model.CMS.VideoAlbum>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.VideoAlbum item = new Maticsoft.Model.CMS.VideoAlbum();
                    if ((dt.Rows[i]["AlbumID"] != null) && (dt.Rows[i]["AlbumID"].ToString() != ""))
                    {
                        item.AlbumID = int.Parse(dt.Rows[i]["AlbumID"].ToString());
                    }
                    if ((dt.Rows[i]["AlbumName"] != null) && (dt.Rows[i]["AlbumName"].ToString() != ""))
                    {
                        item.AlbumName = dt.Rows[i]["AlbumName"].ToString();
                    }
                    if ((dt.Rows[i]["CoverVideo"] != null) && (dt.Rows[i]["CoverVideo"].ToString() != ""))
                    {
                        item.CoverVideo = dt.Rows[i]["CoverVideo"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
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
                    if ((dt.Rows[i]["LastUpdatedDate"] != null) && (dt.Rows[i]["LastUpdatedDate"].ToString() != ""))
                    {
                        item.LastUpdatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["LastUpdatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = int.Parse(dt.Rows[i]["State"].ToString());
                    }
                    if ((dt.Rows[i]["Sequence"] != null) && (dt.Rows[i]["Sequence"].ToString() != ""))
                    {
                        item.Sequence = int.Parse(dt.Rows[i]["Sequence"].ToString());
                    }
                    if ((dt.Rows[i]["Privacy"] != null) && (dt.Rows[i]["Privacy"].ToString() != ""))
                    {
                        item.Privacy = new int?(int.Parse(dt.Rows[i]["Privacy"].ToString()));
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

        public bool Delete(int AlbumID)
        {
            return this.dal.Delete(AlbumID);
        }

        public bool DeleteList(string AlbumIDlist)
        {
            return this.dal.DeleteList(AlbumIDlist);
        }

        public bool Exists(int AlbumID)
        {
            return this.dal.Exists(AlbumID);
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

        public Maticsoft.Model.CMS.VideoAlbum GetModel(int AlbumID)
        {
            return this.dal.GetModel(AlbumID);
        }

        public Maticsoft.Model.CMS.VideoAlbum GetModelByCache(int AlbumID)
        {
            string cacheKey = "VideoAlbumModel-" + AlbumID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AlbumID);
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
            return (Maticsoft.Model.CMS.VideoAlbum) cache;
        }

        public Maticsoft.Model.CMS.VideoAlbum GetModelEx(int AlbumID)
        {
            return this.dal.GetModelEx(AlbumID);
        }

        public List<Maticsoft.Model.CMS.VideoAlbum> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.CMS.VideoAlbum model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }
    }
}

