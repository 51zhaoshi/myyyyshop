namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class ProductReviews
    {
        private readonly IProductReviews dal = DAShopProducts.CreateProductReviews();

        public int Add(Maticsoft.Model.Shop.Products.ProductReviews model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(List<Maticsoft.Model.Shop.Products.ProductReviews> modelList, long OrderId, out int pointers)
        {
            pointers = 0;
            if ((modelList == null) || (modelList.Count <= 0))
            {
                return false;
            }
            bool flag = this.dal.AddEx(modelList, OrderId);
            if (flag)
            {
                foreach (Maticsoft.Model.Shop.Products.ProductReviews reviews in modelList)
                {
                    if (reviews != null)
                    {
                        if (string.IsNullOrWhiteSpace(reviews.ImagesNames))
                        {
                            PointsDetail detail = new PointsDetail();
                            pointers += detail.AddPoints("ProductReviews", reviews.UserId, "商品评论操作", "");
                        }
                        else
                        {
                            PointsDetail detail2 = new PointsDetail();
                            pointers += detail2.AddPoints("SingleSun", reviews.UserId, "晒单操作", "");
                        }
                    }
                }
            }
            return flag;
        }

        public bool AddEx(Maticsoft.Model.Shop.Products.ProductReviews model, string productName, bool IsPost = false)
        {
            model.Status = 1;
            if (FilterWords.ContainsModWords(model.ReviewText))
            {
                model.Status = 0;
            }
            else
            {
                model.ReviewText = FilterWords.ReplaceWords(model.ReviewText);
            }
            int num = this.dal.Add(model);
            if ((num > 0) && IsPost)
            {
                Maticsoft.BLL.SNS.Posts posts = new Maticsoft.BLL.SNS.Posts();
                Maticsoft.Model.SNS.Posts post = new Maticsoft.Model.SNS.Posts {
                    CreatedDate = DateTime.Now,
                    Description = model.ReviewText,
                    ProductName = productName,
                    TargetId = (int) model.ProductId,
                    CreatedNickName = model.UserName,
                    CreatedUserID = model.UserId,
                    Type = 0
                };
                posts.AddNormalPost(post);
                return true;
            }
            return (num > 0);
        }

        public bool AuditComment(string ids, int status)
        {
            return this.dal.AuditComment(ids, status);
        }

        public List<Maticsoft.Model.Shop.Products.ProductReviews> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductReviews> list = new List<Maticsoft.Model.Shop.Products.ProductReviews>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductReviews item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ReviewId)
        {
            return this.dal.Delete(ReviewId);
        }

        public bool DeleteList(string ReviewIdlist)
        {
            return this.dal.DeleteList(ReviewIdlist);
        }

        public bool Exists(int ReviewId)
        {
            return this.dal.Exists(ReviewId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(int? Status)
        {
            return this.dal.GetList(Status);
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

        public DataSet GetListLeftOrderItems(int? Status)
        {
            return this.dal.GetListsProdRev(Status);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.ProductReviews GetModel(int ReviewId)
        {
            return this.dal.GetModel(ReviewId);
        }

        public Maticsoft.Model.Shop.Products.ProductReviews GetModelByCache(int ReviewId)
        {
            string cacheKey = "ProductReviewsModel-" + ReviewId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ReviewId);
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
            return (Maticsoft.Model.Shop.Products.ProductReviews) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductReviews> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.Shop.Products.ProductReviews> GetReviewsByPage(long productId, string orderBy, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage("Status=1 and ProductId=" + productId, orderBy, startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductReviews model)
        {
            return this.dal.Update(model);
        }
    }
}

