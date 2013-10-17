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

    public class StarRank
    {
        private readonly IStarRank dal = DASNS.CreateStarRank();

        public int Add(Maticsoft.Model.SNS.StarRank model)
        {
            return this.dal.Add(model);
        }

        public bool AddCollocationRank()
        {
            return this.dal.AddCollocationRank();
        }

        public bool AddHotStarRank()
        {
            return this.dal.AddHotStarRank();
        }

        public bool AddShareProductRank()
        {
            return this.dal.AddShareProductRank();
        }

        public List<Maticsoft.Model.SNS.StarRank> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.StarRank> list = new List<Maticsoft.Model.SNS.StarRank>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.StarRank item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
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

        public Maticsoft.Model.SNS.StarRank GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.StarRank GetModelByCache(int ID)
        {
            string cacheKey = "StarRankModel-" + ID;
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
            return (Maticsoft.Model.SNS.StarRank) cache;
        }

        public List<Maticsoft.Model.SNS.StarRank> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.ViewModel.SNS.StarRank> GetStarNewList(int StarType)
        {
            List<Maticsoft.ViewModel.SNS.StarRank> list = new List<Maticsoft.ViewModel.SNS.StarRank>();
            DataSet set = new DataSet();
            if (StarType == 0)
            {
                set = this.dal.GetList(10, " RankType=1 and  Status=1", " Sequence");
            }
            else
            {
                set = this.dal.GetList(10, "  RankType=1 and  Status=1 and TypeID=" + StarType, " Sequence");
            }
            List<Maticsoft.Model.SNS.StarRank> list2 = this.DataTableToList(set.Tables[0]);
            UsersExp exp = new UsersExp();
            foreach (Maticsoft.Model.SNS.StarRank rank2 in list2)
            {
                Maticsoft.ViewModel.SNS.StarRank item = new Maticsoft.ViewModel.SNS.StarRank(rank2);
                UsersExpModel usersExpModel = exp.GetUsersExpModel(rank2.UserId);
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

        public List<Maticsoft.ViewModel.SNS.StarRank> GetStarRankList(int StarType, int top = 10)
        {
            List<Maticsoft.ViewModel.SNS.StarRank> list = new List<Maticsoft.ViewModel.SNS.StarRank>();
            DataSet set = new DataSet();
            if (StarType == 0)
            {
                set = this.dal.GetList(top, " RankType=0 and  Status=1", " Sequence");
            }
            else
            {
                set = this.dal.GetList(top, "  RankType=0 and  Status=1 and TypeID=" + StarType, " Sequence");
            }
            List<Maticsoft.Model.SNS.StarRank> list2 = this.DataTableToList(set.Tables[0]);
            UsersExp exp = new UsersExp();
            foreach (Maticsoft.Model.SNS.StarRank rank2 in list2)
            {
                Maticsoft.ViewModel.SNS.StarRank item = new Maticsoft.ViewModel.SNS.StarRank(rank2);
                UsersExpModel usersExpModel = exp.GetUsersExpModel(rank2.UserId);
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

        public List<Maticsoft.ViewModel.SNS.StarRank> HotStarList(int top = 4)
        {
            List<Maticsoft.ViewModel.SNS.StarRank> list = new List<Maticsoft.ViewModel.SNS.StarRank>();
            DataSet set = this.dal.GetList(top, " IsRecommend='true'", " Sequence");
            List<Maticsoft.Model.SNS.StarRank> list2 = this.DataTableToList(set.Tables[0]);
            UsersExp exp = new UsersExp();
            foreach (Maticsoft.Model.SNS.StarRank rank2 in list2)
            {
                Maticsoft.ViewModel.SNS.StarRank item = new Maticsoft.ViewModel.SNS.StarRank(rank2);
                UsersExpModel usersExpModel = exp.GetUsersExpModel(rank2.UserId);
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

        public bool Update(Maticsoft.Model.SNS.StarRank model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStateList(string IDlist)
        {
            return this.dal.UpdateStateList(IDlist);
        }
    }
}

