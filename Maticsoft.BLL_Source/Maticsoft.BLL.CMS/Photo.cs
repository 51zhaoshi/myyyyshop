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

    public class Photo
    {
        private readonly IPhoto dal = DACMS.CreatePhoto();

        public int Add(Maticsoft.Model.CMS.Photo model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.CMS.Photo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Photo> list = new List<Maticsoft.Model.CMS.Photo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.Photo item = new Maticsoft.Model.CMS.Photo();
                    if ((dt.Rows[i]["PhotoID"] != null) && (dt.Rows[i]["PhotoID"].ToString() != ""))
                    {
                        item.PhotoID = int.Parse(dt.Rows[i]["PhotoID"].ToString());
                    }
                    if ((dt.Rows[i]["PhotoName"] != null) && (dt.Rows[i]["PhotoName"].ToString() != ""))
                    {
                        item.PhotoName = dt.Rows[i]["PhotoName"].ToString();
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["AlbumID"] != null) && (dt.Rows[i]["AlbumID"].ToString() != ""))
                    {
                        item.AlbumID = int.Parse(dt.Rows[i]["AlbumID"].ToString());
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = int.Parse(dt.Rows[i]["State"].ToString());
                    }
                    if ((dt.Rows[i]["CreatedUserID"] != null) && (dt.Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        item.CreatedUserID = int.Parse(dt.Rows[i]["CreatedUserID"].ToString());
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                    }
                    if ((dt.Rows[i]["PVCount"] != null) && (dt.Rows[i]["PVCount"].ToString() != ""))
                    {
                        item.PVCount = int.Parse(dt.Rows[i]["PVCount"].ToString());
                    }
                    if ((dt.Rows[i]["ClassID"] != null) && (dt.Rows[i]["ClassID"].ToString() != ""))
                    {
                        item.ClassID = int.Parse(dt.Rows[i]["ClassID"].ToString());
                    }
                    if ((dt.Rows[i]["ThumbImageUrl"] != null) && (dt.Rows[i]["ThumbImageUrl"].ToString() != ""))
                    {
                        item.ThumbImageUrl = dt.Rows[i]["ThumbImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["NormalImageUrl"] != null) && (dt.Rows[i]["NormalImageUrl"].ToString() != ""))
                    {
                        item.NormalImageUrl = dt.Rows[i]["NormalImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["Sequence"] != null) && (dt.Rows[i]["Sequence"].ToString() != ""))
                    {
                        item.Sequence = new int?(int.Parse(dt.Rows[i]["Sequence"].ToString()));
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
                    if ((dt.Rows[i]["CommentCount"] != null) && (dt.Rows[i]["CommentCount"].ToString() != ""))
                    {
                        item.CommentCount = new int?(int.Parse(dt.Rows[i]["CommentCount"].ToString()));
                    }
                    if ((dt.Rows[i]["FavouriteCount"] != null) && (dt.Rows[i]["FavouriteCount"].ToString() != ""))
                    {
                        item.FavouriteCount = int.Parse(dt.Rows[i]["FavouriteCount"].ToString());
                    }
                    if ((dt.Rows[i]["Tags"] != null) && (dt.Rows[i]["Tags"].ToString() != ""))
                    {
                        item.Tags = dt.Rows[i]["Tags"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int PhotoID)
        {
            return this.dal.Delete(PhotoID);
        }

        public bool DeleteList(string PhotoIDlist, out DataSet imageList)
        {
            return this.dal.DeleteList(PhotoIDlist, out imageList);
        }

        public bool Exists(int PhotoID)
        {
            return this.dal.Exists(PhotoID);
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

        public List<Maticsoft.Model.CMS.Photo> GetListAroundPhotoId(int Top, int PhotoId, int ClassId)
        {
            return this.DataTableToList(this.dal.GetListAroundPhotoId(Top, PhotoId, ClassId).Tables[0]);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public List<Maticsoft.Model.CMS.Photo> GetListModelByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.DataTableToList(this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex).Tables[0]);
        }

        public List<int> GetListToReGen(string strWhere)
        {
            DataSet listToReGen = this.dal.GetListToReGen(strWhere);
            List<int> list = new List<int>();
            if ((listToReGen != null) && (listToReGen.Tables.Count > 0))
            {
                for (int i = 0; i < listToReGen.Tables[0].Rows.Count; i++)
                {
                    if ((listToReGen.Tables[0].Rows[i]["PhotoID"] != null) && (listToReGen.Tables[0].Rows[i]["PhotoID"].ToString() != ""))
                    {
                        list.Add(int.Parse(listToReGen.Tables[0].Rows[i]["PhotoID"].ToString()));
                    }
                }
            }
            return list;
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public int GetMaxSequence()
        {
            return this.dal.GetMaxSequence();
        }

        public Maticsoft.Model.CMS.Photo GetModel(int PhotoID)
        {
            return this.dal.GetModel(PhotoID);
        }

        public Maticsoft.Model.CMS.Photo GetModelByCache(int PhotoID)
        {
            string cacheKey = "PhotoModel-" + PhotoID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PhotoID);
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
            return (Maticsoft.Model.CMS.Photo) cache;
        }

        public List<Maticsoft.Model.CMS.Photo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.CMS.Photo model)
        {
            return this.dal.Update(model);
        }

        public bool UpdatePhotoAlbum(int AlbumID, int newAlbumId)
        {
            return this.dal.UpdatePhotoAlbum(AlbumID, newAlbumId);
        }
    }
}

