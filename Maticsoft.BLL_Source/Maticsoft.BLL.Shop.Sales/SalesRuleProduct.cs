namespace Maticsoft.BLL.Shop.Sales
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.Shop.Sales;
    using Maticsoft.ViewModel.Shop;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class SalesRuleProduct
    {
        private readonly ISalesRuleProduct dal = DAShopSales.CreateSalesRuleProduct();

        public bool Add(Maticsoft.Model.Shop.Sales.SalesRuleProduct model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Sales.SalesRuleProduct> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Sales.SalesRuleProduct> list = new List<Maticsoft.Model.Shop.Sales.SalesRuleProduct>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Sales.SalesRuleProduct item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int RuleId, long ProductId)
        {
            return this.dal.Delete(RuleId, ProductId);
        }

        public bool DeleteByRule(int RuleId)
        {
            return this.dal.DeleteByRule(RuleId);
        }

        public bool DeleteList(string idlist)
        {
            string[] strArray = idlist.Split(new char[] { ',' });
            bool flag = true;
            foreach (string str in strArray)
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    int ruleId = Globals.SafeInt(str.Split(new char[] { '|' })[0], 0);
                    long productId = Globals.SafeLong(str.Split(new char[] { '|' })[1], (long) 0L);
                    if (!this.Delete(ruleId, productId))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public bool Exists(int RuleId, long ProductId)
        {
            return this.dal.Exists(RuleId, ProductId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public void GetCartItem(ShoppingCartItem cartItem, Dictionary<Maticsoft.Model.Shop.Sales.SalesRule, List<ShoppingCartItem>> dictionary)
        {
            Maticsoft.Model.Shop.Sales.SalesRuleProduct ruleProduct = this.GetRuleProduct(cartItem.ProductId);
            if (ruleProduct != null)
            {
                Maticsoft.Model.Shop.Sales.SalesRule modelByCache = new Maticsoft.BLL.Shop.Sales.SalesRule().GetModelByCache(ruleProduct.RuleId);
                if ((modelByCache != null) && (modelByCache.Status != 0))
                {
                    cartItem.SaleDes = modelByCache.RuleName;
                    if (modelByCache.RuleMode == 0)
                    {
                        this.GetRateValue(modelByCache.RuleId, modelByCache.RuleUnit, cartItem);
                    }
                    else if (dictionary.ContainsKey(modelByCache))
                    {
                        dictionary[modelByCache].Add(cartItem);
                    }
                    else
                    {
                        List<ShoppingCartItem> list = new List<ShoppingCartItem> {
                            cartItem
                        };
                        dictionary.Add(modelByCache, list);
                    }
                }
            }
        }

        public Maticsoft.Model.Shop.Sales.SalesItem GetItemByQuantity(List<Maticsoft.Model.Shop.Sales.SalesItem> itemList, int Quantity)
        {
            itemList = (from c in itemList
                orderby c.UnitValue descending
                select c).ToList<Maticsoft.Model.Shop.Sales.SalesItem>();
            foreach (Maticsoft.Model.Shop.Sales.SalesItem item2 in itemList)
            {
                if (Quantity >= item2.UnitValue)
                {
                    return item2;
                }
            }
            return null;
        }

        public Maticsoft.Model.Shop.Sales.SalesItem GetItemByTotalPrice(List<Maticsoft.Model.Shop.Sales.SalesItem> itemList, decimal TotalPrice)
        {
            itemList = (from c in itemList
                orderby c.UnitValue descending
                select c).ToList<Maticsoft.Model.Shop.Sales.SalesItem>();
            foreach (Maticsoft.Model.Shop.Sales.SalesItem item2 in itemList)
            {
                if (TotalPrice >= item2.UnitValue)
                {
                    return item2;
                }
            }
            return null;
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

        public Maticsoft.Model.Shop.Sales.SalesRuleProduct GetModel(int RuleId, long ProductId)
        {
            return this.dal.GetModel(RuleId, ProductId);
        }

        public Maticsoft.Model.Shop.Sales.SalesRuleProduct GetModelByCache(int RuleId, long ProductId)
        {
            string cacheKey = "SalesRuleProductModel-" + RuleId + ProductId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RuleId, ProductId);
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
            return (Maticsoft.Model.Shop.Sales.SalesRuleProduct) cache;
        }

        public List<Maticsoft.Model.Shop.Sales.SalesRuleProduct> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public void GetRateValue(int ruleId, int ruleUnit, ShoppingCartItem cartItem)
        {
            List<Maticsoft.Model.Shop.Sales.SalesItem> modelList = new Maticsoft.BLL.Shop.Sales.SalesItem().GetModelList(" RuleId=" + ruleId);
            if ((modelList != null) && (modelList.Count != 0))
            {
                Maticsoft.Model.Shop.Sales.SalesItem itemByQuantity = null;
                if (ruleUnit == 0)
                {
                    itemByQuantity = this.GetItemByQuantity(modelList, cartItem.Quantity);
                }
                if (ruleUnit == 1)
                {
                    decimal totalPrice = cartItem.SellPrice * cartItem.Quantity;
                    itemByQuantity = this.GetItemByTotalPrice(modelList, totalPrice);
                }
                if (itemByQuantity != null)
                {
                    switch (modelList[0].ItemType)
                    {
                        case 0:
                            cartItem.AdjustedPrice = (cartItem.SellPrice * itemByQuantity.RateValue) / 100M;
                            return;

                        case 1:
                            cartItem.AdjustedPrice = ((cartItem.SellPrice * cartItem.Quantity) - itemByQuantity.RateValue) / cartItem.Quantity;
                            return;

                        case 2:
                            cartItem.AdjustedPrice = cartItem.SellPrice - itemByQuantity.RateValue;
                            return;
                    }
                    cartItem.AdjustedPrice = (cartItem.SellPrice * itemByQuantity.RateValue) / 100M;
                }
            }
        }

        public List<ShoppingCartItem> GetRateValueList(int ruleId, int ruleUnit, List<ShoppingCartItem> cartItems)
        {
            List<Maticsoft.Model.Shop.Sales.SalesItem> modelList = new Maticsoft.BLL.Shop.Sales.SalesItem().GetModelList(" RuleId=" + ruleId);
            if ((modelList != null) && (modelList.Count != 0))
            {
                Maticsoft.Model.Shop.Sales.SalesItem itemByQuantity = null;
                if (ruleUnit == 0)
                {
                    int quantity = ((IEnumerable<int>) (from c in cartItems select c.Quantity)).Sum();
                    itemByQuantity = this.GetItemByQuantity(modelList, quantity);
                }
                if (ruleUnit == 1)
                {
                    decimal totalPrice = ((IEnumerable<decimal>) (from c in cartItems select c.SubTotal)).Sum();
                    itemByQuantity = this.GetItemByTotalPrice(modelList, totalPrice);
                }
                if (itemByQuantity != null)
                {
                    foreach (ShoppingCartItem item3 in cartItems)
                    {
                        switch (modelList[0].ItemType)
                        {
                            case 0:
                            {
                                item3.AdjustedPrice = (item3.SellPrice * itemByQuantity.RateValue) / 100M;
                                continue;
                            }
                            case 1:
                            {
                                item3.AdjustedPrice = ((item3.SellPrice * item3.Quantity) - itemByQuantity.RateValue) / item3.Quantity;
                                continue;
                            }
                            case 2:
                            {
                                item3.AdjustedPrice = item3.SellPrice - itemByQuantity.RateValue;
                                continue;
                            }
                        }
                        item3.AdjustedPrice = (item3.SellPrice * itemByQuantity.RateValue) / 100M;
                    }
                }
            }
            return cartItems;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public Maticsoft.Model.Shop.Sales.SalesRuleProduct GetRuleProduct(long productId)
        {
            string cacheKey = "GetRuleProduct-" + productId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelList(" ProductId=" + productId)[0];
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
            return (Maticsoft.Model.Shop.Sales.SalesRuleProduct) cache;
        }

        public DataSet GetRuleProducts(int ruleId, string categoryId, string pName)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                builder.AppendFormat("AND ProductId IN (SELECT DISTINCT ProductId FROM  Shop_ProductCategories PC WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}))", categoryId);
            }
            return this.dal.GetRuleProducts(ruleId, builder.ToString());
        }

        public SalesModel GetSalesRule(long productId, int userId)
        {
            SalesModel model = new SalesModel();
            Maticsoft.Model.Shop.Sales.SalesRuleProduct ruleProduct = this.GetRuleProduct(productId);
            if (ruleProduct != null)
            {
                Maticsoft.Model.Shop.Sales.SalesRule modelByCache = new Maticsoft.BLL.Shop.Sales.SalesRule().GetModelByCache(ruleProduct.RuleId);
                if ((modelByCache == null) || (modelByCache.Status == 0))
                {
                    return model;
                }
                Maticsoft.BLL.Shop.Sales.SalesItem item = new Maticsoft.BLL.Shop.Sales.SalesItem();
                model.SalesRule = modelByCache;
                model.SalesItems = item.GetModelList(" RuleId=" + modelByCache.RuleId);
            }
            return model;
        }

        public SalesModel GetSalesRuleByCache(long productId, int userId)
        {
            string cacheKey = "GetSalesRuleByCache-" + productId + userId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetSalesRule(productId, userId);
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
            return (SalesModel) cache;
        }

        public ShoppingCartInfo GetWholeSale(ShoppingCartInfo cartInfo)
        {
            Dictionary<Maticsoft.Model.Shop.Sales.SalesRule, List<ShoppingCartItem>> dictionary = new Dictionary<Maticsoft.Model.Shop.Sales.SalesRule, List<ShoppingCartItem>>();
            foreach (ShoppingCartItem item in cartInfo.Items)
            {
                this.GetCartItem(item, dictionary);
            }
            if ((dictionary != null) && (dictionary.Count > 0))
            {
                using (Dictionary<Maticsoft.Model.Shop.Sales.SalesRule, List<ShoppingCartItem>>.Enumerator enumerator2 = dictionary.GetEnumerator())
                {
                    Predicate<ShoppingCartItem> match = null;
                    KeyValuePair<Maticsoft.Model.Shop.Sales.SalesRule, List<ShoppingCartItem>> dic;
                    while (enumerator2.MoveNext())
                    {
                        dic = enumerator2.Current;
                        Maticsoft.Model.Shop.Sales.SalesRule key = dic.Key;
                        if (key != null)
                        {
                            if (match == null)
                            {
                                match = c => dic.Value.Contains(c);
                            }
                            cartInfo.Items.RemoveAll(match);
                            foreach (ShoppingCartItem item2 in this.GetRateValueList(key.RuleId, key.RuleUnit, dic.Value))
                            {
                                item2.SaleDes = key.RuleName;
                                cartInfo.Items.Add(item2);
                            }
                        }
                    }
                }
            }
            return cartInfo;
        }

        public bool RankIsLimit(int ruleId, int userid)
        {
            List<Maticsoft.Model.Shop.Sales.SalesUserRank> modelList = new Maticsoft.BLL.Shop.Sales.SalesUserRank().GetModelList(" RuleId=" + ruleId);
            if ((modelList == null) || (modelList.Count == 0))
            {
                return true;
            }
            int userRankId = new UsersExp().GetUserRankId(userid);
            return !(from c in modelList select c.RankId).Contains<int>(userRankId);
        }

        public bool Update(Maticsoft.Model.Shop.Sales.SalesRuleProduct model)
        {
            return this.dal.Update(model);
        }
    }
}

