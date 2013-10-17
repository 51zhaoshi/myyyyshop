namespace Maticsoft.BLL.Shop.Gift
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Gift;
    using Maticsoft.Model.Shop.Gift;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Gifts
    {
        private readonly IGifts dal = DAShopGifts.CreateGifts();

        public int Add(Maticsoft.Model.Shop.Gift.Gifts model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Gift.Gifts> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Gift.Gifts> list = new List<Maticsoft.Model.Shop.Gift.Gifts>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Gift.Gifts item = new Maticsoft.Model.Shop.Gift.Gifts();
                    if ((dt.Rows[i]["GiftId"] != null) && (dt.Rows[i]["GiftId"].ToString() != ""))
                    {
                        item.GiftId = int.Parse(dt.Rows[i]["GiftId"].ToString());
                    }
                    if ((dt.Rows[i]["CategoryID"] != null) && (dt.Rows[i]["CategoryID"].ToString() != ""))
                    {
                        item.CategoryID = int.Parse(dt.Rows[i]["CategoryID"].ToString());
                    }
                    if ((dt.Rows[i]["Name"] != null) && (dt.Rows[i]["Name"].ToString() != ""))
                    {
                        item.Name = dt.Rows[i]["Name"].ToString();
                    }
                    if ((dt.Rows[i]["ShortDescription"] != null) && (dt.Rows[i]["ShortDescription"].ToString() != ""))
                    {
                        item.ShortDescription = dt.Rows[i]["ShortDescription"].ToString();
                    }
                    if ((dt.Rows[i]["Unit"] != null) && (dt.Rows[i]["Unit"].ToString() != ""))
                    {
                        item.Unit = dt.Rows[i]["Unit"].ToString();
                    }
                    if ((dt.Rows[i]["Weight"] != null) && (dt.Rows[i]["Weight"].ToString() != ""))
                    {
                        item.Weight = int.Parse(dt.Rows[i]["Weight"].ToString());
                    }
                    if ((dt.Rows[i]["LongDescription"] != null) && (dt.Rows[i]["LongDescription"].ToString() != ""))
                    {
                        item.LongDescription = dt.Rows[i]["LongDescription"].ToString();
                    }
                    if ((dt.Rows[i]["Title"] != null) && (dt.Rows[i]["Title"].ToString() != ""))
                    {
                        item.Title = dt.Rows[i]["Title"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Description"] != null) && (dt.Rows[i]["Meta_Description"].ToString() != ""))
                    {
                        item.Meta_Description = dt.Rows[i]["Meta_Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Keywords"] != null) && (dt.Rows[i]["Meta_Keywords"].ToString() != ""))
                    {
                        item.Meta_Keywords = dt.Rows[i]["Meta_Keywords"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailsUrl"] != null) && (dt.Rows[i]["ThumbnailsUrl"].ToString() != ""))
                    {
                        item.ThumbnailsUrl = dt.Rows[i]["ThumbnailsUrl"].ToString();
                    }
                    if ((dt.Rows[i]["InFocusImageUrl"] != null) && (dt.Rows[i]["InFocusImageUrl"].ToString() != ""))
                    {
                        item.InFocusImageUrl = dt.Rows[i]["InFocusImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["CostPrice"] != null) && (dt.Rows[i]["CostPrice"].ToString() != ""))
                    {
                        item.CostPrice = new decimal?(decimal.Parse(dt.Rows[i]["CostPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["MarketPrice"] != null) && (dt.Rows[i]["MarketPrice"].ToString() != ""))
                    {
                        item.MarketPrice = new decimal?(decimal.Parse(dt.Rows[i]["MarketPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["SalePrice"] != null) && (dt.Rows[i]["SalePrice"].ToString() != ""))
                    {
                        item.SalePrice = new decimal?(decimal.Parse(dt.Rows[i]["SalePrice"].ToString()));
                    }
                    if ((dt.Rows[i]["Stock"] != null) && (dt.Rows[i]["Stock"].ToString() != ""))
                    {
                        item.Stock = new int?(int.Parse(dt.Rows[i]["Stock"].ToString()));
                    }
                    if ((dt.Rows[i]["NeedPoint"] != null) && (dt.Rows[i]["NeedPoint"].ToString() != ""))
                    {
                        item.NeedPoint = int.Parse(dt.Rows[i]["NeedPoint"].ToString());
                    }
                    if ((dt.Rows[i]["NeedGrade"] != null) && (dt.Rows[i]["NeedGrade"].ToString() != ""))
                    {
                        item.NeedGrade = int.Parse(dt.Rows[i]["NeedGrade"].ToString());
                    }
                    if ((dt.Rows[i]["SaleCounts"] != null) && (dt.Rows[i]["SaleCounts"].ToString() != ""))
                    {
                        item.SaleCounts = int.Parse(dt.Rows[i]["SaleCounts"].ToString());
                    }
                    if ((dt.Rows[i]["CreateDate"] != null) && (dt.Rows[i]["CreateDate"].ToString() != ""))
                    {
                        item.CreateDate = DateTime.Parse(dt.Rows[i]["CreateDate"].ToString());
                    }
                    if ((dt.Rows[i]["Enabled"] != null) && (dt.Rows[i]["Enabled"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["Enabled"].ToString() == "1") || (dt.Rows[i]["Enabled"].ToString().ToLower() == "true"))
                        {
                            item.Enabled = true;
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int GiftId)
        {
            return this.dal.Delete(GiftId);
        }

        public bool DeleteList(string GiftIdlist)
        {
            return this.dal.DeleteList(GiftIdlist);
        }

        public bool Exists(int GiftId)
        {
            return this.dal.Exists(GiftId);
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

        public Maticsoft.Model.Shop.Gift.Gifts GetModel(int GiftId)
        {
            return this.dal.GetModel(GiftId);
        }

        public Maticsoft.Model.Shop.Gift.Gifts GetModelByCache(int GiftId)
        {
            string cacheKey = "GiftsModel-" + GiftId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(GiftId);
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
            return (Maticsoft.Model.Shop.Gift.Gifts) cache;
        }

        public List<Maticsoft.Model.Shop.Gift.Gifts> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Gift.Gifts model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStock(int giftid, int stock)
        {
            return this.dal.UpdateStock(giftid, stock);
        }
    }
}

