namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SKUMemberPrice
    {
        private readonly ISKUMemberPrice dal = DAShopProducts.CreateSKUMemberPrice();

        public bool Add(Maticsoft.Model.Shop.Products.SKUMemberPrice model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.SKUMemberPrice> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUMemberPrice> list = new List<Maticsoft.Model.Shop.Products.SKUMemberPrice>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.SKUMemberPrice item = new Maticsoft.Model.Shop.Products.SKUMemberPrice();
                    if ((dt.Rows[i]["SkuId"] != null) && (dt.Rows[i]["SkuId"].ToString() != ""))
                    {
                        item.SkuId = long.Parse(dt.Rows[i]["SkuId"].ToString());
                    }
                    if ((dt.Rows[i]["GradeId"] != null) && (dt.Rows[i]["GradeId"].ToString() != ""))
                    {
                        item.GradeId = int.Parse(dt.Rows[i]["GradeId"].ToString());
                    }
                    if ((dt.Rows[i]["MemberSalePrice"] != null) && (dt.Rows[i]["MemberSalePrice"].ToString() != ""))
                    {
                        item.MemberSalePrice = decimal.Parse(dt.Rows[i]["MemberSalePrice"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long SkuId, int GradeId)
        {
            return this.dal.Delete(SkuId, GradeId);
        }

        public bool Exists(long SkuId, int GradeId)
        {
            return this.dal.Exists(SkuId, GradeId);
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

        public Maticsoft.Model.Shop.Products.SKUMemberPrice GetModel(long SkuId, int GradeId)
        {
            return this.dal.GetModel(SkuId, GradeId);
        }

        public Maticsoft.Model.Shop.Products.SKUMemberPrice GetModelByCache(long SkuId, int GradeId)
        {
            string cacheKey = "SKUMemberPriceModel-" + SkuId + GradeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SkuId, GradeId);
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
            return (Maticsoft.Model.Shop.Products.SKUMemberPrice) cache;
        }

        public List<Maticsoft.Model.Shop.Products.SKUMemberPrice> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.SKUMemberPrice model)
        {
            return this.dal.Update(model);
        }
    }
}

