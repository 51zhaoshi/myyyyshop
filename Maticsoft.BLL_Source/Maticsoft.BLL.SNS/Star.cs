namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using Maticsoft.ViewModel.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Web;

    public class Star
    {
        private readonly IStar dal = DASNS.CreateStar();

        public int Add(Maticsoft.Model.SNS.Star model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.Star> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Star> list = new List<Maticsoft.Model.SNS.Star>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Star item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool DeleteListEx(string IDlist)
        {
            DataSet ds = new DataSet();
            if (!this.dal.DeleteList(IDlist, out ds))
            {
                return false;
            }
            if (ds != null)
            {
                List<Maticsoft.Model.SNS.Star> list = this.DataTableToList(ds.Tables[0]);
                if (list != null)
                {
                    foreach (Maticsoft.Model.SNS.Star star in list)
                    {
                        if (!string.IsNullOrEmpty(star.UserGravatar))
                        {
                            FileManage.DeleteFile(HttpContext.Current.Server.MapPath(star.UserGravatar));
                        }
                    }
                }
            }
            return true;
        }

        public bool Exists(int UserID, int TypeID)
        {
            return this.dal.Exists(UserID, TypeID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetCountByType(int TypeId)
        {
            if (TypeId == 0)
            {
                return this.dal.GetRecordCount(" ");
            }
            return this.dal.GetRecordCount(" TypeId=" + TypeId);
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

        public List<ViewStar> GetListForPage(int TypeId, string orderby, int startIndex, int endIndex, int CurrentUserId)
        {
            List<ViewStar> list = new List<ViewStar>();
            DataSet set = new DataSet();
            if (TypeId == 0)
            {
                set = this.dal.GetListByPage(" Status=1", orderby, startIndex, endIndex);
            }
            else
            {
                set = this.dal.GetListByPage("Status=1 and TypeId=" + TypeId, orderby, startIndex, endIndex);
            }
            List<Maticsoft.Model.SNS.Star> list2 = this.DataTableToList(set.Tables[0]);
            UsersExp exp = new UsersExp();
            foreach (Maticsoft.Model.SNS.Star star2 in list2)
            {
                ViewStar item = new ViewStar(star2);
                UsersExpModel usersExpModel = exp.GetUsersExpModel(star2.UserID);
                if (usersExpModel != null)
                {
                    item.FansCount = usersExpModel.FansCount.Value;
                    item.FavouritesCount = usersExpModel.FavoritedCount.Value;
                    item.ProductsCount = usersExpModel.ProductsCount.Value;
                    item.Singature = usersExpModel.Singature;
                }
                list.Add(item);
                if (CurrentUserId != 0)
                {
                    Maticsoft.BLL.SNS.UserShip bllUserShip = new Maticsoft.BLL.SNS.UserShip();
                    List<Maticsoft.Model.SNS.UserShip> shipList = bllUserShip.GetModelList("ActiveUserID = " + CurrentUserId);
                    list.ForEach(delegate (ViewStar item) {
                        item.IsFellow = bllUserShip.UserIsFellow(item.UserID, shipList);
                    });
                }
            }
            return list;
        }

        public Maticsoft.Model.SNS.Star GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.Star GetModelByCache(int ID)
        {
            string cacheKey = "StarModel-" + ID;
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
            return (Maticsoft.Model.SNS.Star) cache;
        }

        public List<Maticsoft.Model.SNS.Star> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<ViewStar> GetStarNewList(int StarType, int Top = 10)
        {
            List<ViewStar> list = new List<ViewStar>();
            DataSet set = new DataSet();
            if (StarType == 0)
            {
                set = this.dal.GetList(Top, "   Status=1", " CreatedDate desc");
            }
            else
            {
                set = this.dal.GetList(Top, "   Status=1 and TypeID=" + StarType, " CreatedDate desc");
            }
            List<Maticsoft.Model.SNS.Star> list2 = this.DataTableToList(set.Tables[0]);
            UsersExp exp = new UsersExp();
            foreach (Maticsoft.Model.SNS.Star star2 in list2)
            {
                ViewStar item = new ViewStar(star2);
                UsersExpModel usersExpModel = exp.GetUsersExpModel(star2.UserID);
                if (usersExpModel != null)
                {
                    item.FansCount = usersExpModel.FansCount.Value;
                    item.FavouritesCount = usersExpModel.FavoritedCount.Value;
                    item.ProductsCount = usersExpModel.ProductsCount.Value;
                    item.Singature = usersExpModel.Singature;
                    item.IsFellow = usersExpModel.IsFellow;
                }
                list.Add(item);
            }
            return list;
        }

        public bool IsExists(int userId, int typeId)
        {
            return this.dal.IsExists(userId, typeId);
        }

        public bool IsStar(int userId)
        {
            return this.dal.IsStar(userId);
        }

        public DataSet StarName(int userId)
        {
            return this.dal.StarName(userId);
        }

        public bool Update(Maticsoft.Model.SNS.Star model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStateList(string IDlist, int status)
        {
            return this.dal.UpdateStateList(IDlist, status);
        }
    }
}

