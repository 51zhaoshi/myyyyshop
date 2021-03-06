namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserFavAlbum
    {
        private readonly IUserFavAlbum dal = DASNS.CreateUserFavAlbum();

        public int Add(Maticsoft.Model.SNS.UserFavAlbum model)
        {
            return this.dal.Add(model);
        }

        public bool CheckIsFav(int AlbumId, int UserId)
        {
            return (this.GetRecordCount(string.Concat(new object[] { "AlbumID=", AlbumId, " and UserID=", UserId })) > 0);
        }

        public List<Maticsoft.Model.SNS.UserFavAlbum> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserFavAlbum> list = new List<Maticsoft.Model.SNS.UserFavAlbum>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserFavAlbum item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool Delete(int AlbumID, int UserID)
        {
            return this.dal.Delete(AlbumID, UserID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(int AlbumID, int UserID)
        {
            return this.dal.Exists(AlbumID, UserID);
        }

        public int FavAlbum(int AlbumId, int UserId)
        {
            return this.dal.FavAlbum(AlbumId, UserId);
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

        public Maticsoft.Model.SNS.UserFavAlbum GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.UserFavAlbum GetModelByCache(int ID)
        {
            string cacheKey = "UserFavAlbumModel-" + ID;
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
            return (Maticsoft.Model.SNS.UserFavAlbum) cache;
        }

        public List<Maticsoft.Model.SNS.UserFavAlbum> GetModelList(string strWhere)
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
            return this.dal.GetList(0, string.Format(" Tags like '%{0}%'", Keywords), "");
        }

        public int UnFavAlbum(int AlbumId, int UserId)
        {
            return this.dal.UnFavAlbum(AlbumId, UserId);
        }

        public bool Update(Maticsoft.Model.SNS.UserFavAlbum model)
        {
            return this.dal.Update(model);
        }
    }
}

