namespace Maticsoft.BLL.SNS
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using Maticsoft.TaoBao.Request;
    using Maticsoft.TaoBao.Response;
    using Maticsoft.ViewModel.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class Products
    {
        private readonly IProducts dal = DASNS.CreateProducts();

        public long Add(Maticsoft.Model.SNS.Products model)
        {
            return this.dal.Add(model);
        }

        private string BindstrWhere(ProductQuery queryobj)
        {
            StringBuilder builder = new StringBuilder();
            if (((queryobj.CategoryID.HasValue && (queryobj.CategoryID.Value != 0)) && !queryobj.IsTopCategory) && (queryobj.CategoryID > 0))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat(" CategoryID={0}", queryobj.CategoryID.Value);
            }
            else if (queryobj.CategoryID > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat(" CategoryID in(select CategoryID from SNS_Categories where Path like '{0}%')", queryobj.CategoryID);
            }
            if (!string.IsNullOrEmpty(queryobj.Keywords))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat("( ProductName like '%{0}%' or Tags like '%{0}%')", queryobj.Keywords);
            }
            if (queryobj.IsRecomend.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat(" IsRecomend={0}", queryobj.IsRecomend.Value);
            }
            if (queryobj.MaxPrice.HasValue && (queryobj.MaxPrice.Value != 0M))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat(" Price<{0}", queryobj.MaxPrice.Value);
            }
            if (queryobj.MinPrice.HasValue && (queryobj.MinPrice.Value != 0M))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat(" Price>{0}", queryobj.MinPrice.Value);
            }
            if (!string.IsNullOrEmpty(queryobj.Tags))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat(" Tags like '%{0}%'", queryobj.Tags);
            }
            if (!string.IsNullOrEmpty(queryobj.Color) && (queryobj.Color != "all"))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" And ");
                }
                builder.AppendFormat(" Color='{0}'", queryobj.Color);
            }
            if (builder.Length > 0)
            {
                builder.Append(" And ");
            }
            builder.AppendFormat(" Status<>{0}", 0);
            return builder.ToString();
        }

        public List<Maticsoft.Model.SNS.Products> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Products> list = new List<Maticsoft.Model.SNS.Products>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Products item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long ProductID)
        {
            return this.dal.Delete(ProductID);
        }

        public bool DeleteEX(int ProductID)
        {
            return this.dal.DeleteEX(ProductID);
        }

        public bool DeleteList(string ProductIDlist)
        {
            return this.dal.DeleteList(ProductIDlist);
        }

        public DataSet DeleteListEx(string Ids, out int Result, bool IsSendMess = false, int SendUserID = 1)
        {
            List<int> productUserIds = this.GetProductUserIds(Ids);
            DataSet set = this.dal.DeleteListEx(Ids, out Result);
            if ((Result > 0) && IsSendMess)
            {
                SiteMessage message = new SiteMessage();
                foreach (int num in productUserIds)
                {
                    message.AddMessageByUser(SendUserID, num, "商品删除", "您分享的商品涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
                }
            }
            return set;
        }

        public bool DeleteListEX(string ProductIds)
        {
            return this.dal.DeleteListEX(ProductIds);
        }

        public List<Maticsoft.Model.SNS.Products> ExcelToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Products> list = new List<Maticsoft.Model.SNS.Products>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Products item = new Maticsoft.Model.SNS.Products();
                    if ((dt.Rows[i][1] != null) && (dt.Rows[i][1].ToString() != ""))
                    {
                        item.NormalImageUrl = dt.Rows[i][1].ToString();
                        item.ThumbImageUrl = dt.Rows[i][1].ToString() + "_300x300.jpg";
                    }
                    if ((dt.Rows[i][2] != null) && (dt.Rows[i][2].ToString() != ""))
                    {
                        item.ProductName = dt.Rows[i][2].ToString();
                    }
                    if ((dt.Rows[i][3] != null) && (dt.Rows[i][3].ToString() != ""))
                    {
                        item.Price = new decimal?(Globals.SafeDecimal(dt.Rows[i][3].ToString(), (decimal) -1M));
                    }
                    if ((dt.Rows[i][4] != null) && (dt.Rows[i][4].ToString() != ""))
                    {
                        item.ProductUrl = dt.Rows[i][4].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Exists(long ProductID)
        {
            return this.dal.Exists(ProductID);
        }

        public bool Exsit(long originalID, int type)
        {
            return this.dal.Exsit(originalID, type);
        }

        public bool Exsit(string ProductName, int Uid)
        {
            return this.dal.Exsit(ProductName, Uid);
        }

        public bool ExsitUrl(string ProductUrl)
        {
            return this.dal.ExsitUrl(ProductUrl);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.Products> GetCacheProductByPage(ProductQuery Query, int StartIndex, int EndIndex)
        {
            string cacheKey = string.Concat(new object[] { "CacheProductByPage-", Query.Tags, Query.QueryType, Query.MinPrice, Query.MaxPrice, Query.Keywords, Query.CategoryID, Query.Order, Query.IsTopCategory, Query, StartIndex, EndIndex });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetProductByPage(Query, StartIndex, EndIndex);
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
            return (List<Maticsoft.Model.SNS.Products>) cache;
        }

        public int GetCacheProductCount(ProductQuery Query)
        {
            string cacheKey = string.Concat(new object[] { "CacheProductCount-", Query.Tags, Query.QueryType, Query.MinPrice, Query.MaxPrice, Query.Keywords, Query.CategoryID, Query.Order, Query.IsTopCategory, Query });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetProductCount(Query);
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
            return (int) cache;
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

        public DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageEx(strWhere, CateId, orderby, startIndex, endIndex);
        }

        public DataSet GetListEx(string strWhere, int CateId)
        {
            return this.dal.GetListEx(strWhere, CateId);
        }

        public List<int> GetListToStatic(string strWhere)
        {
            DataSet listToStatic = this.dal.GetListToStatic(strWhere);
            List<int> list = new List<int>();
            if ((listToStatic != null) && (listToStatic.Tables.Count > 0))
            {
                for (int i = 0; i < listToStatic.Tables[0].Rows.Count; i++)
                {
                    if ((listToStatic.Tables[0].Rows[i]["ProductID"] != null) && (listToStatic.Tables[0].Rows[i]["ProductID"].ToString() != ""))
                    {
                        list.Add(int.Parse(listToStatic.Tables[0].Rows[i]["ProductID"].ToString()));
                    }
                }
            }
            return list;
        }

        public Maticsoft.Model.SNS.Products GetModel(long ProductID)
        {
            return this.dal.GetModel(ProductID);
        }

        public Maticsoft.Model.SNS.Products GetModelByCache(long ProductID)
        {
            string cacheKey = "ProductsModel-" + ProductID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ProductID);
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
            return (Maticsoft.Model.SNS.Products) cache;
        }

        public List<Maticsoft.Model.SNS.Products> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public string GetOrderby(string Order)
        {
            string str;
            StringBuilder builder = new StringBuilder();
            string str2 = Order;
            if (str2 != null)
            {
                if (!(str2 == "popular"))
                {
                    if (str2 == "new")
                    {
                        str = " ProductID ";
                        goto Label_0052;
                    }
                    if (str2 == "hot")
                    {
                        str = " CommentCount ";
                        goto Label_0052;
                    }
                }
                else
                {
                    str = " FavouriteCount ";
                    goto Label_0052;
                }
            }
            str = " FavouriteCount ";
        Label_0052:
            builder.AppendFormat(" Order by {0} Desc", str);
            return builder.ToString();
        }

        public List<Maticsoft.Model.SNS.Products> GetProductByPage(ProductQuery Query, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetProductByPage(this.BindstrWhere(Query), this.GetOrderby(Query.Order), StartIndex, EndIndex).Tables[0]);
        }

        public int GetProductCount(ProductQuery Query)
        {
            return this.GetRecordCount(this.BindstrWhere(Query));
        }

        public int GetProductDate(int userId, string nick, int cid, string keyword, string area = "", int page_no = 1, int page_size = 40, string shop_type = "all", int start_coupon_rate = 0, int end_coupon_rate = 0, int start_commission_rate = 0, int end_commission_rate = 0, string start_credit = "", string end_credit = "")
        {
            ITopClient topClient = TaoBaoConfig.GetTopClient();
            TaobaokeItemsCouponGetRequest request = new TaobaokeItemsCouponGetRequest();
            int num = 0;
            if (cid > 0)
            {
                request.Cid = new long?((long) cid);
            }
            request.PageNo = new long?((long) page_no);
            request.PageSize = new long?((long) page_size);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                request.Keyword = keyword;
            }
            if (!string.IsNullOrWhiteSpace(area))
            {
                request.Area = area;
            }
            if (start_coupon_rate > 0)
            {
                request.StartCouponRate = new long?((long) start_coupon_rate);
            }
            if (end_coupon_rate > 0)
            {
                request.EndCouponRate = new long?((long) end_coupon_rate);
            }
            if (start_commission_rate > 0)
            {
                request.StartCommissionRate = new long?((long) start_commission_rate);
            }
            if (end_commission_rate > 0)
            {
                request.EndCommissionRate = new long?((long) end_commission_rate);
            }
            if (!string.IsNullOrWhiteSpace(start_credit))
            {
                request.StartCredit = start_credit;
            }
            if (!string.IsNullOrWhiteSpace(end_credit))
            {
                request.EndCredit = end_credit;
            }
            request.ShopType = shop_type;
            request.Sort = "commissionRate_desc";
            request.Fields = "num_iid,title,pic_url,price,click_url";
            TaobaokeItemsCouponGetResponse response = topClient.Execute<TaobaokeItemsCouponGetResponse>(request);
            if (response.TaobaokeItems.Count > 0)
            {
                Maticsoft.Model.SNS.Products model = null;
                Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
                Maticsoft.BLL.SNS.CategorySource source = new Maticsoft.BLL.SNS.CategorySource();
                foreach (TaobaokeItem item in response.TaobaokeItems)
                {
                    if (this.Exsit(item.NumIid, 3))
                    {
                        continue;
                    }
                    model = new Maticsoft.Model.SNS.Products();
                    Maticsoft.Model.SNS.CategorySource source2 = source.GetModel(3, Convert.ToInt32(cid));
                    model.CategoryID = (source2 != null) ? source2.SnsCategoryId : 0;
                    model.NormalImageUrl = item.PicUrl;
                    model.ThumbImageUrl = item.PicUrl;
                    model.Price = new decimal?(Globals.SafeDecimal(item.Price, (decimal) 0M));
                    model.ProductName = this.NoHTML(item.Title);
                    model.ProductUrl = item.ClickUrl;
                    model.CreateUserID = userId;
                    model.CreatedNickName = nick;
                    model.ProductSourceID = 3;
                    model.OriginalID = new long?(item.NumIid);
                    model.SourceType = 1;
                    model.CreatedDate = DateTime.Now;
                    string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
                    if (!string.IsNullOrEmpty(valueByCache))
                    {
                        model.Status = Globals.SafeInt(valueByCache, 1);
                    }
                    else
                    {
                        model.Status = 1;
                    }
                    ItemGetRequest request2 = new ItemGetRequest {
                        Fields = "props_name",
                        NumIid = new long?(item.NumIid)
                    };
                    string propsName = topClient.Execute<ItemGetResponse>(request2).Item.PropsName;
                    model.Tags = tags.GetTagStr(propsName);
                    if (this.Add(model) > 0L)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public List<TaobaokeItem> GetProductDates(int cid, string keyword, string area = "", int page_no = 1, int page_size = 40, string sort = "price_desc", int start_commission_rate = 0, int end_commission_rate = 0, string start_credit = "", string end_credit = "", int start_commissionNum = 0, int end_commissionNum = 0, int start_coupon_rate = 0, int end_coupon_rate = 0)
        {
            List<TaobaokeItem> list = new List<TaobaokeItem>();
            ITopClient topClient = TaoBaoConfig.GetTopClient();
            TaobaokeItemsCouponGetRequest request = new TaobaokeItemsCouponGetRequest();
            if (cid > 0)
            {
                request.Cid = new long?((long) cid);
            }
            request.PageSize = new long?((long) page_size);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                request.Keyword = keyword;
            }
            if (!string.IsNullOrWhiteSpace(area))
            {
                request.Area = area;
            }
            if (start_coupon_rate > 0)
            {
                request.StartCouponRate = new long?((long) start_coupon_rate);
            }
            if (end_coupon_rate > 0)
            {
                request.EndCouponRate = new long?((long) end_coupon_rate);
            }
            if (start_commission_rate > 0)
            {
                request.StartCommissionRate = new long?((long) start_commission_rate);
            }
            if (end_commission_rate > 0)
            {
                request.EndCommissionRate = new long?((long) end_commission_rate);
            }
            if (!string.IsNullOrWhiteSpace(start_credit))
            {
                request.StartCredit = start_credit;
            }
            if (!string.IsNullOrWhiteSpace(end_credit))
            {
                request.EndCredit = end_credit;
            }
            if (start_commissionNum > 0)
            {
                request.StartCommissionNum = new long?((long) start_commissionNum);
            }
            if (end_commissionNum > 0)
            {
                request.EndCommissionNum = new long?((long) end_commissionNum);
            }
            request.Sort = sort;
            request.Fields = "num_iid,title,pic_url,price,click_url";
            for (int i = 1; i <= page_no; i++)
            {
                request.PageNo = new long?((long) i);
                TaobaokeItemsCouponGetResponse response = topClient.Execute<TaobaokeItemsCouponGetResponse>(request);
                if (response.TaobaokeItems.Count > 0)
                {
                    list.AddRange(response.TaobaokeItems);
                }
            }
            return list;
        }

        public string GetProductImageUrl(long Pid)
        {
            try
            {
                ITopClient topClient = TaoBaoConfig.GetTopClient();
                TaobaokeItemsDetailGetRequest request = new TaobaokeItemsDetailGetRequest {
                    Fields = "item_img.url,pic_url,title",
                    NumIids = Pid.ToString()
                };
                TaobaokeItemsDetailGetResponse response = topClient.Execute<TaobaokeItemsDetailGetResponse>(request);
                if (response.TaobaokeItemDetails.Count < 1)
                {
                    ItemGetRequest request2 = new ItemGetRequest {
                        Fields = "num_iid,title,price,num_iid,title,cid,nick,desc,location,price,post_fee,express_fee,ems_fee,freight_payer,item_img.url,click_url,shop_click_url,num,props_name,detail_url,pic_url",
                        NumIid = new long?(Pid)
                    };
                    ItemGetResponse response2 = topClient.Execute<ItemGetResponse>(request2);
                    if (response2.Item != null)
                    {
                        return (response2.Item.PicUrl + "|" + this.NoHTML(response2.Item.Title));
                    }
                    return "No";
                }
                return (response.TaobaokeItemDetails[0].Item.PicUrl + "|" + this.NoHTML(response.TaobaokeItemDetails[0].Item.Title));
            }
            catch (Exception)
            {
                return "No";
            }
        }

        public List<Maticsoft.Model.SNS.Products> GetProductListByCid(int Cid)
        {
            return this.GetModelList("CategoryID=" + Cid);
        }

        public Maticsoft.Model.SNS.Products GetProductModel(Maticsoft.Model.SNS.Products PModel)
        {
            Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
            ITopClient topClient = TaoBaoConfig.GetTopClient();
            TaobaokeItemsDetailGetRequest request = new TaobaokeItemsDetailGetRequest {
                Fields = "num_iid,title,price,num_iid,title,cid,nick,desc,price,post_fee,express_fee,ems_fee,item_img.url,click_url,shop_click_url,num,props_name,detail_url,pic_url",
                NumIids = PModel.ProductID.ToString()
            };
            TaobaokeItemsDetailGetResponse response = topClient.Execute<TaobaokeItemsDetailGetResponse>(request);
            Maticsoft.BLL.SNS.CategorySource source = new Maticsoft.BLL.SNS.CategorySource();
            Item item = new Item();
            item = (response.TaobaokeItemDetails.Count > 0) ? response.TaobaokeItemDetails[0].Item : null;
            PModel.ProductUrl = (response.TaobaokeItemDetails.Count > 0) ? response.TaobaokeItemDetails[0].ClickUrl : "";
            if (response.TaobaokeItemDetails.Count < 1)
            {
                ItemGetRequest request2 = new ItemGetRequest {
                    Fields = "num_iid,title,price,num_iid,title,cid,nick,desc,price,item_img.url,click_url,shop_click_url,num,props_name,detail_url,pic_url",
                    NumIid = new long?(PModel.ProductID)
                };
                item = topClient.Execute<ItemGetResponse>(request2).Item;
                PModel.ProductUrl = item.DetailUrl;
            }
            Maticsoft.Model.SNS.CategorySource model = source.GetModel(3, Convert.ToInt32(item.Cid));
            PModel.CategoryID = (model != null) ? model.SnsCategoryId : 0;
            PModel.NormalImageUrl = item.PicUrl;
            PModel.ThumbImageUrl = item.PicUrl;
            PModel.Price = new decimal?(Globals.SafeDecimal(item.Price, (decimal) 0M));
            PModel.ProductName = this.NoHTML(item.Title);
            PModel.ProductSourceID = 3;
            PModel.CreatedDate = DateTime.Now;
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
            if (!string.IsNullOrEmpty(valueByCache))
            {
                PModel.Status = Globals.SafeInt(valueByCache, 1);
            }
            else
            {
                PModel.Status = 1;
            }
            ItemGetRequest request3 = new ItemGetRequest {
                Fields = "props_name",
                NumIid = new long?(PModel.ProductID)
            };
            string propsName = topClient.Execute<ItemGetResponse>(request3).Item.PropsName;
            PModel.Tags = tags.GetTagStr(propsName);
            return PModel;
        }

        public string GetProductUrl(long productId)
        {
            return this.dal.GetProductUrl(productId);
        }

        public string GetProductUrlByCache(long productId)
        {
            string cacheKey = "GetProductUrlByCache-" + productId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetProductUrl(productId);
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
            return cache.ToString();
        }

        public List<int> GetProductUserIds(string ids)
        {
            DataSet productUserIds = this.dal.GetProductUserIds(ids);
            List<int> list = new List<int>();
            if ((productUserIds != null) && (productUserIds.Tables.Count > 0))
            {
                for (int i = 0; i < productUserIds.Tables[0].Rows.Count; i++)
                {
                    if ((productUserIds.Tables[0].Rows[i]["CreateUserID"] != null) && (productUserIds.Tables[0].Rows[i]["CreateUserID"].ToString() != ""))
                    {
                        list.Add(int.Parse(productUserIds.Tables[0].Rows[i]["CreateUserID"].ToString()));
                    }
                }
            }
            return list;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountEx(string strWhere, int CateId)
        {
            return this.dal.GetRecordCountEx(strWhere, CateId);
        }

        public List<Product> GetTaoDataList(int cid, string keyword, int page_no = 1, int page_size = 40, int vertical_market = 3, string market_id = "1", string status = "0,3")
        {
            List<Product> list = new List<Product>();
            ITopClient topClient = TaoBaoConfig.GetTopClient();
            ProductsSearchRequest request = new ProductsSearchRequest();
            if (cid > 0)
            {
                request.Cid = new long?((long) cid);
            }
            request.PageSize = new long?((long) page_size);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                request.Q = keyword;
            }
            request.Status = status;
            if (vertical_market > 0)
            {
                request.VerticalMarket = new long?((long) vertical_market);
            }
            request.Fields = "product_id,cid,price,name,pic_url";
            for (int i = 1; i <= page_no; i++)
            {
                request.PageNo = new long?((long) i);
                ProductsSearchResponse response = topClient.Execute<ProductsSearchResponse>(request);
                if (response.Products.Count > 0)
                {
                    list.AddRange(response.Products);
                }
            }
            return list;
        }

        public TargetDetail GetTargetAssiationInfo(int pid)
        {
            TargetDetail detail = new TargetDetail();
            UsersExp exp = new UsersExp();
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbumDetail detail2 = new Maticsoft.BLL.SNS.UserAlbumDetail();
            new Maticsoft.BLL.SNS.Photos();
            detail.Product = this.GetModel((long) pid);
            this.UpdatePvCount(pid);
            detail.UserModel = exp.GetUsersExpModel(detail.Userid);
            detail.UserAlums = albums.GetUserAlbum(1, detail.TargetId, detail.Userid);
            if (detail.UserAlums != null)
            {
                detail.CovorImageList = detail2.GetThumbImageByAlbum(detail.UserAlums.AlbumID, -1);
            }
            return detail;
        }

        public int ImportData(int userid, int albumId, int cid, List<TaobaokeItem> list, bool ReRepeat = true)
        {
            int num = 0;
            ITopClient topClient = TaoBaoConfig.GetTopClient();
            if (list.Count > 0)
            {
                Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
                Maticsoft.BLL.SNS.CategorySource source = new Maticsoft.BLL.SNS.CategorySource();
                Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User(userid);
                if (user == null)
                {
                    return 0;
                }
                Maticsoft.Model.SNS.Products model = null;
                foreach (TaobaokeItem item in list)
                {
                    if (ReRepeat)
                    {
                        if (!this.Exsit(item.NumIid, 3))
                        {
                            model = new Maticsoft.Model.SNS.Products();
                            Maticsoft.Model.SNS.CategorySource source2 = source.GetModel(3, Convert.ToInt32(cid));
                            model.CategoryID = (source2 != null) ? source2.SnsCategoryId : 0;
                            model.NormalImageUrl = item.PicUrl;
                            model.ThumbImageUrl = item.PicUrl;
                            model.Price = new decimal?(Globals.SafeDecimal(item.Price, (decimal) 0M));
                            model.ProductName = this.NoHTML(item.Title);
                            model.ProductUrl = item.ClickUrl;
                            model.CreateUserID = user.UserID;
                            model.CreatedNickName = user.NickName;
                            model.ProductSourceID = 3;
                            model.OriginalID = new long?(item.NumIid);
                            model.SourceType = 1;
                            model.CreatedDate = DateTime.Now;
                            string str = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
                            if (!string.IsNullOrEmpty(str))
                            {
                                model.Status = Globals.SafeInt(str, 1);
                            }
                            else
                            {
                                model.Status = 1;
                            }
                            ItemGetRequest request = new ItemGetRequest {
                                Fields = "props_name",
                                NumIid = new long?(item.NumIid)
                            };
                            string des = topClient.Execute<ItemGetResponse>(request).Item.PropsName;
                            model.Tags = tags.GetTagStr(des);
                            long num2 = this.Add(model);
                            if (num2 > 0L)
                            {
                                Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
                                Maticsoft.Model.SNS.UserAlbumDetail detail2 = new Maticsoft.Model.SNS.UserAlbumDetail {
                                    AlbumID = albumId,
                                    TargetID = (int) num2,
                                    Type = 1,
                                    AlbumUserId = userid
                                };
                                detail.AddEx(detail2);
                                num++;
                            }
                        }
                        continue;
                    }
                    model = new Maticsoft.Model.SNS.Products();
                    Maticsoft.Model.SNS.CategorySource source3 = source.GetModel(3, Convert.ToInt32(cid));
                    model.CategoryID = (source3 != null) ? source3.SnsCategoryId : 0;
                    model.NormalImageUrl = item.PicUrl;
                    model.ThumbImageUrl = item.PicUrl;
                    model.Price = new decimal?(Globals.SafeDecimal(item.Price, (decimal) 0M));
                    model.ProductName = this.NoHTML(item.Title);
                    model.ProductUrl = item.ClickUrl;
                    model.CreateUserID = user.UserID;
                    model.CreatedNickName = user.NickName;
                    model.ProductSourceID = 3;
                    model.OriginalID = new long?(item.NumIid);
                    model.SourceType = 1;
                    model.CreatedDate = DateTime.Now;
                    string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
                    if (!string.IsNullOrEmpty(valueByCache))
                    {
                        model.Status = Globals.SafeInt(valueByCache, 1);
                    }
                    else
                    {
                        model.Status = 1;
                    }
                    ItemGetRequest request2 = new ItemGetRequest {
                        Fields = "props_name",
                        NumIid = new long?(item.NumIid)
                    };
                    string propsName = topClient.Execute<ItemGetResponse>(request2).Item.PropsName;
                    model.Tags = tags.GetTagStr(propsName);
                    long num3 = this.Add(model);
                    if (num3 > 0L)
                    {
                        Maticsoft.BLL.SNS.UserAlbumDetail detail3 = new Maticsoft.BLL.SNS.UserAlbumDetail();
                        Maticsoft.Model.SNS.UserAlbumDetail detail4 = new Maticsoft.Model.SNS.UserAlbumDetail {
                            AlbumID = albumId,
                            TargetID = (int) num3,
                            Type = 1,
                            AlbumUserId = userid
                        };
                        detail3.AddEx(detail4);
                        num++;
                    }
                }
            }
            return num;
        }

        public int ImportExcelData(int userid, int albumId, int categoryId, DataTable dt)
        {
            int num = 0;
            List<Maticsoft.Model.SNS.Products> list = this.ExcelToList(dt);
            if (list.Count > 0)
            {
                Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User(userid);
                foreach (Maticsoft.Model.SNS.Products products in list)
                {
                    if (this.ExsitUrl(products.ProductUrl.Trim()))
                    {
                        continue;
                    }
                    products.CreateUserID = user.UserID;
                    products.CreatedNickName = user.NickName;
                    products.ProductSourceID = 3;
                    products.SourceType = 1;
                    products.CreatedDate = DateTime.Now;
                    products.Price = products.Price.HasValue ? products.Price : -1;
                    products.CategoryID = new int?(categoryId);
                    string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
                    if (!string.IsNullOrEmpty(valueByCache))
                    {
                        products.Status = Globals.SafeInt(valueByCache, 1);
                    }
                    else
                    {
                        products.Status = 1;
                    }
                    long num2 = this.Add(products);
                    if (num2 > 0L)
                    {
                        Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
                        Maticsoft.Model.SNS.UserAlbumDetail model = new Maticsoft.Model.SNS.UserAlbumDetail {
                            AlbumID = albumId,
                            TargetID = (int) num2,
                            Type = 1,
                            AlbumUserId = userid
                        };
                        detail.AddEx(model);
                        num++;
                    }
                }
            }
            return num;
        }

        public string NoHTML(string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "\x00a1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "\x00a2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "\x00a3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "\x00a9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        public bool Update(Maticsoft.Model.SNS.Products model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCateList(string ProductIds, int CateId)
        {
            return this.dal.UpdateCateList(ProductIds, CateId);
        }

        public bool UpdateClickCount(int ProuductId)
        {
            return this.dal.UpdateClickCount(ProuductId);
        }

        public bool UpdateEX(int ProductId, int CateId)
        {
            return this.dal.UpdateEX(ProductId, CateId);
        }

        public bool UpdatePvCount(int pid)
        {
            return this.dal.UpdatePvCount(pid);
        }

        public bool UpdateRecomend(int ProductId, int Recomend)
        {
            return this.dal.UpdateRecomend(ProductId, Recomend);
        }

        public bool UpdateRecomendList(string ProductIds, int Recomend)
        {
            return this.dal.UpdateRecomendList(ProductIds, Recomend);
        }

        public bool UpdateRecommandState(int id, int State)
        {
            return this.dal.UpdateRecommandState(id, State);
        }

        public bool UpdateStaticUrl(int productId, string staticUrl)
        {
            return this.dal.UpdateStaticUrl(productId, staticUrl);
        }

        public bool UpdateStatus(int ProductId, int Status)
        {
            return this.dal.UpdateStatus(ProductId, Status);
        }

        public List<Maticsoft.Model.SNS.Products> UserUploadPhotoList(int ablumId)
        {
            DataSet set = this.dal.UserUploadProductsImage(ablumId);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                return this.DataTableToList(set.Tables[0]);
            }
            return null;
        }
    }
}

