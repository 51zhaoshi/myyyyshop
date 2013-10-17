namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ScoreDetails
    {
        private readonly IScoreDetails dal = DAShopProducts.CreateScoreDetails();

        public bool Add(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ScoreDetails> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ScoreDetails> list = new List<Maticsoft.Model.Shop.Products.ScoreDetails>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ScoreDetails item = new Maticsoft.Model.Shop.Products.ScoreDetails();
                    if ((dt.Rows[i]["ScoreId"] != null) && (dt.Rows[i]["ScoreId"].ToString() != ""))
                    {
                        item.ScoreId = int.Parse(dt.Rows[i]["ScoreId"].ToString());
                    }
                    if ((dt.Rows[i]["ReviewId"] != null) && (dt.Rows[i]["ReviewId"].ToString() != ""))
                    {
                        item.ReviewId = int.Parse(dt.Rows[i]["ReviewId"].ToString());
                    }
                    if ((dt.Rows[i]["UserId"] != null) && (dt.Rows[i]["UserId"].ToString() != ""))
                    {
                        item.UserId = new int?(int.Parse(dt.Rows[i]["UserId"].ToString()));
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = new long?(long.Parse(dt.Rows[i]["ProductId"].ToString()));
                    }
                    if ((dt.Rows[i]["Score"] != null) && (dt.Rows[i]["Score"].ToString() != ""))
                    {
                        item.Score = new int?(int.Parse(dt.Rows[i]["Score"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ScoreId)
        {
            return this.dal.Delete(ScoreId);
        }

        public bool DeleteList(string ScoreIdlist)
        {
            return this.dal.DeleteList(ScoreIdlist);
        }

        public bool Exists(int ScoreId)
        {
            return this.dal.Exists(ScoreId);
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.ScoreDetails GetModel(int ScoreId)
        {
            return this.dal.GetModel(ScoreId);
        }

        public Maticsoft.Model.Shop.Products.ScoreDetails GetModelByCache(int ScoreId)
        {
            string cacheKey = "ScoreDetailsModel-" + ScoreId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ScoreId);
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
            return (Maticsoft.Model.Shop.Products.ScoreDetails) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ScoreDetails> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetScore(int? ReviewId)
        {
            return this.dal.GetScore(ReviewId);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            return this.dal.Update(model);
        }
    }
}

