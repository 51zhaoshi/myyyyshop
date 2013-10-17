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

    public class UserAlbums
    {
        private readonly IUserAlbums dal = DASNS.CreateUserAlbums();

        public int Add(Maticsoft.Model.SNS.UserAlbums model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(Maticsoft.Model.SNS.UserAlbums model, int TypeId)
        {
            return this.dal.AddEx(model, TypeId);
        }

        public List<Maticsoft.Model.SNS.UserAlbums> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserAlbums> list = new List<Maticsoft.Model.SNS.UserAlbums>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserAlbums item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int AlbumID)
        {
            return this.dal.Delete(AlbumID);
        }

        public bool DeleteAblumAction(int albumId)
        {
            return this.dal.DeleteAblumAction(albumId);
        }

        public bool DeleteEx(int AlbumID, int UserId)
        {
            List<Maticsoft.Model.SNS.UserAlbumsType> modelList = new Maticsoft.BLL.SNS.UserAlbumsType().GetModelList(string.Concat(new object[] { "AlbumsID=", AlbumID, " and AlbumsUserID=", UserId }));
            if (modelList.Count > 0)
            {
                return this.dal.DeleteEx(AlbumID, modelList[0].TypeID, modelList[0].AlbumsUserID.Value);
            }
            return this.dal.DeleteEx(AlbumID, 0, UserId);
        }

        public bool DeleteList(string AlbumIDlist)
        {
            return this.dal.DeleteList(AlbumIDlist);
        }

        public bool Exists(int CreatedUserID, string AlbumName)
        {
            return this.dal.Exists(CreatedUserID, AlbumName);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetCountByKeyWard(string KeyWord)
        {
            string strWhere = "";
            if (!string.IsNullOrEmpty(KeyWord))
            {
                strWhere = "AlbumName like '%" + KeyWord + "%'";
            }
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public List<AlbumIndex> GetListByKeyWord(string KeyWord, string orderby, int startIndex, int endIndex, int type = -1)
        {
            DataSet set;
            List<AlbumIndex> list = new List<AlbumIndex>();
            string strWhere = "";
            if (!string.IsNullOrEmpty(KeyWord))
            {
                strWhere = "AlbumName like '%" + KeyWord + "%'";
            }
            string str2 = orderby;
            if (str2 != null)
            {
                if (!(str2 == "popular"))
                {
                    if (str2 == "new")
                    {
                        orderby = "CreatedDate";
                        goto Label_0079;
                    }
                    if (str2 == "hot")
                    {
                        orderby = "CommentsCount";
                        goto Label_0079;
                    }
                }
                else
                {
                    orderby = "FavouriteCount";
                    goto Label_0079;
                }
            }
            orderby = "FavouriteCount";
        Label_0079:
            set = this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            List<Maticsoft.Model.SNS.UserAlbums> list2 = this.DataTableToList(set.Tables[0]);
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            foreach (Maticsoft.Model.SNS.UserAlbums albums in list2)
            {
                AlbumIndex item = new AlbumIndex(albums) {
                    TopImages = detail.GetThumbImageByAlbum(albums.AlbumID, type)
                };
                list.Add(item);
            }
            return list;
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public List<AlbumIndex> GetListByUserId(int UserId, int type = -1)
        {
            List<AlbumIndex> list = new List<AlbumIndex>();
            List<Maticsoft.Model.SNS.UserAlbums> modelList = this.GetModelList("CreatedUserID=" + UserId);
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            foreach (Maticsoft.Model.SNS.UserAlbums albums in modelList)
            {
                AlbumIndex item = new AlbumIndex(albums) {
                    TopImages = detail.GetThumbImageByAlbum(albums.AlbumID, type)
                };
                list.Add(item);
            }
            return list;
        }

        public List<AlbumIndex> GetListForIndex(int TypeID, int Top, int RecommandType = -1, int type = -1)
        {
            List<AlbumIndex> list = new List<AlbumIndex>();
            DataSet set = this.dal.GetListForIndex(TypeID, Top, "FavouriteCount desc", RecommandType);
            List<Maticsoft.Model.SNS.UserAlbums> list2 = this.DataTableToList(set.Tables[0]);
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            foreach (Maticsoft.Model.SNS.UserAlbums albums in list2)
            {
                AlbumIndex item = new AlbumIndex(albums) {
                    TopImages = detail.GetThumbImageByAlbum(albums.AlbumID, type)
                };
                list.Add(item);
            }
            return list;
        }

        public List<AlbumIndex> GetListForIndexEx(int TypeID, int type = -1)
        {
            List<AlbumIndex> list = new List<AlbumIndex>();
            DataSet set = this.dal.GetListForIndexEx(TypeID, 8, "FavouriteCount desc");
            List<Maticsoft.Model.SNS.UserAlbums> list2 = this.DataTableToList(set.Tables[0]);
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            foreach (Maticsoft.Model.SNS.UserAlbums albums in list2)
            {
                AlbumIndex item = new AlbumIndex(albums) {
                    TopImages = detail.GetThumbImageByAlbum(albums.AlbumID, type)
                };
                list.Add(item);
            }
            return list;
        }

        public List<AlbumIndex> GetListForPage(int TypeID, string orderby, int startIndex, int endIndex, int type = -1)
        {
            List<AlbumIndex> list = new List<AlbumIndex>();
            DataSet set = this.dal.GetListForPage(TypeID, orderby, startIndex, endIndex);
            List<Maticsoft.Model.SNS.UserAlbums> list2 = this.DataTableToList(set.Tables[0]);
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            foreach (Maticsoft.Model.SNS.UserAlbums albums in list2)
            {
                AlbumIndex item = new AlbumIndex(albums) {
                    TopImages = detail.GetThumbImageByAlbum(albums.AlbumID, type)
                };
                list.Add(item);
            }
            return list;
        }

        public List<AlbumIndex> GetListForPageEx(int TypeID, string orderby, int startIndex, int endIndex, int type = -1)
        {
            List<AlbumIndex> list = new List<AlbumIndex>();
            DataSet set = this.dal.GetListForPageEx(TypeID, orderby, startIndex, endIndex);
            List<Maticsoft.Model.SNS.UserAlbums> list2 = this.DataTableToList(set.Tables[0]);
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            foreach (Maticsoft.Model.SNS.UserAlbums albums in list2)
            {
                AlbumIndex item = new AlbumIndex(albums) {
                    TopImages = detail.GetThumbImageByAlbum(albums.AlbumID, type)
                };
                list.Add(item);
            }
            return list;
        }

        public Maticsoft.Model.SNS.UserAlbums GetModel(int AlbumID)
        {
            return this.dal.GetModel(AlbumID);
        }

        public Maticsoft.Model.SNS.UserAlbums GetModelByCache(int AlbumID)
        {
            string cacheKey = "UserAlbumsModel-" + AlbumID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AlbumID);
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
            return (Maticsoft.Model.SNS.UserAlbums) cache;
        }

        public List<Maticsoft.Model.SNS.UserAlbums> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(int TypeID)
        {
            return this.dal.GetRecordCount(TypeID);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string Keywords)
        {
            return this.dal.GetList(0, string.Format(" AlbumName like '%{0}%'", Keywords), "");
        }

        public List<Maticsoft.Model.SNS.UserAlbums> GetUserAblumsByUserID(int UserID)
        {
            return this.GetModelList("CreatedUserID=" + UserID);
        }

        public DataSet GetUserAblumSearchList(string Keywords)
        {
            return this.dal.GetList(0, Keywords, "");
        }

        public Maticsoft.Model.SNS.UserAlbums GetUserAlbum(int type, int pid, int UserId)
        {
            return this.dal.GetUserAlbum(type, pid, UserId);
        }

        public List<Maticsoft.Model.SNS.UserAlbums> GetUserAlbumsByUserId(int top, int UserID)
        {
            return this.DataTableToList(this.dal.GetListByPage("CreatedUserID=" + UserID, "PhotoCount desc", 0, 9).Tables[0]);
        }

        public List<AlbumIndex> GetUserFavAlbum(int UserId, int type = -1)
        {
            List<AlbumIndex> list = new List<AlbumIndex>();
            DataSet userFavAlbum = this.dal.GetUserFavAlbum(UserId);
            List<Maticsoft.Model.SNS.UserAlbums> list2 = this.DataTableToList(userFavAlbum.Tables[0]);
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            foreach (Maticsoft.Model.SNS.UserAlbums albums in list2)
            {
                AlbumIndex item = new AlbumIndex(albums) {
                    TopImages = detail.GetThumbImageByAlbum(albums.AlbumID, type)
                };
                list.Add(item);
            }
            return list;
        }

        public bool Update(Maticsoft.Model.SNS.UserAlbums model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCommentCount(int ablumId)
        {
            return this.dal.UpdateCommentCount(ablumId);
        }

        public bool UpdateEx(Maticsoft.Model.SNS.UserAlbums model)
        {
            return this.dal.UpdateEx(model);
        }

        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            return this.dal.UpdateIsRecommand(IsRecommand, IdList);
        }

        public bool UpdatePhotoCount()
        {
            return this.dal.UpdatePhotoCount();
        }

        public bool UpdatePvCount(int AlbumId)
        {
            return this.dal.UpdatePvCount(AlbumId);
        }

        public bool UpdateRecommand(int ablumId, EnumHelper.RecommendType recommendType)
        {
            return this.dal.UpdateRecommand(ablumId, recommendType);
        }
    }
}

