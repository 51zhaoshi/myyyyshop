namespace Maticsoft.BLL.Shop
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop;
    using Maticsoft.Model.Shop;
    using Maticsoft.ViewModel.Shop;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Favorite
    {
        private readonly IFavorite dal = DAShop.CreateFavorite();

        public int Add(Maticsoft.Model.Shop.Favorite model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Favorite> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Favorite> list = new List<Maticsoft.Model.Shop.Favorite>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Favorite item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int FavoriteId)
        {
            return this.dal.Delete(FavoriteId);
        }

        public bool DeleteList(string FavoriteIdlist)
        {
            return this.dal.DeleteList(FavoriteIdlist);
        }

        public bool Exists(int FavoriteId)
        {
            return this.dal.Exists(FavoriteId);
        }

        public bool Exists(long targetId, int userId, int type)
        {
            return this.dal.Exists(targetId, userId, type);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<FavoProdModel> GetFavoriteProductListByPage(string strWhere, int startIndex, int endIndex)
        {
            DataTable table = this.dal.GetProductListByPage(strWhere, "CreatedDate desc ", startIndex, endIndex).Tables[0];
            List<FavoProdModel> list = new List<FavoProdModel>();
            int count = table.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DataRow row = table.Rows[i];
                    FavoProdModel item = new FavoProdModel();
                    if (row != null)
                    {
                        if ((row["FavoriteId"] != null) && (row["FavoriteId"].ToString() != ""))
                        {
                            item.FavoriteId = int.Parse(row["FavoriteId"].ToString());
                        }
                        if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                        {
                            item.ProductId = long.Parse(row["ProductId"].ToString());
                        }
                        if (row["ProductName"] != null)
                        {
                            item.ProductName = row["ProductName"].ToString();
                        }
                        if ((row["SaleStatus"] != null) && (row["SaleStatus"].ToString() != ""))
                        {
                            item.SaleStatus = int.Parse(row["SaleStatus"].ToString());
                        }
                        if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                        {
                            item.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                        }
                        if (row["ThumbnailUrl1"] != null)
                        {
                            item.ThumbnailUrl1 = row["ThumbnailUrl1"].ToString();
                        }
                        list.Add(item);
                    }
                }
            }
            return list;
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

        public DataSet GetListEX(int userid, string keyword)
        {
            StringBuilder builder = new StringBuilder();
            if (userid > 0)
            {
                builder.AppendFormat("userid={0}", userid);
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    builder.AppendFormat("and Tags like '%{0}%' or Remark like '%{0}%'", keyword);
                }
            }
            else if (!string.IsNullOrWhiteSpace(keyword))
            {
                builder.AppendFormat("Tags like '%{0}%' or Remark like '%{0}%'", keyword);
            }
            else
            {
                return this.GetAllList();
            }
            return this.dal.GetList(builder.ToString());
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Favorite GetModel(int FavoriteId)
        {
            return this.dal.GetModel(FavoriteId);
        }

        public Maticsoft.Model.Shop.Favorite GetModelByCache(int FavoriteId)
        {
            string cacheKey = "FavoriteModel-" + FavoriteId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FavoriteId);
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
            return (Maticsoft.Model.Shop.Favorite) cache;
        }

        public List<Maticsoft.Model.Shop.Favorite> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public DataSet GetProductListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetProductListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Favorite model)
        {
            return this.dal.Update(model);
        }
    }
}

