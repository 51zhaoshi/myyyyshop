namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductQA
    {
        private readonly IProductQA dal = DAShopProducts.CreateProductQA();

        public int Add(Maticsoft.Model.Shop.Products.ProductQA model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductQA> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductQA> list = new List<Maticsoft.Model.Shop.Products.ProductQA>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductQA item = new Maticsoft.Model.Shop.Products.ProductQA();
                    if ((dt.Rows[i]["QAId"] != null) && (dt.Rows[i]["QAId"].ToString() != ""))
                    {
                        item.QAId = int.Parse(dt.Rows[i]["QAId"].ToString());
                    }
                    if ((dt.Rows[i]["ParentId"] != null) && (dt.Rows[i]["ParentId"].ToString() != ""))
                    {
                        item.ParentId = new int?(int.Parse(dt.Rows[i]["ParentId"].ToString()));
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = int.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["UserId"] != null) && (dt.Rows[i]["UserId"].ToString() != ""))
                    {
                        item.UserId = int.Parse(dt.Rows[i]["UserId"].ToString());
                    }
                    if ((dt.Rows[i]["UserName"] != null) && (dt.Rows[i]["UserName"].ToString() != ""))
                    {
                        item.UserName = dt.Rows[i]["UserName"].ToString();
                    }
                    if ((dt.Rows[i]["Question"] != null) && (dt.Rows[i]["Question"].ToString() != ""))
                    {
                        item.Question = dt.Rows[i]["Question"].ToString();
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = int.Parse(dt.Rows[i]["State"].ToString());
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["ReplyContent"] != null) && (dt.Rows[i]["ReplyContent"].ToString() != ""))
                    {
                        item.ReplyContent = dt.Rows[i]["ReplyContent"].ToString();
                    }
                    if ((dt.Rows[i]["ReplyDate"] != null) && (dt.Rows[i]["ReplyDate"].ToString() != ""))
                    {
                        item.ReplyDate = new DateTime?(DateTime.Parse(dt.Rows[i]["ReplyDate"].ToString()));
                    }
                    if ((dt.Rows[i]["ReplyUserId"] != null) && (dt.Rows[i]["ReplyUserId"].ToString() != ""))
                    {
                        item.ReplyUserId = new int?(int.Parse(dt.Rows[i]["ReplyUserId"].ToString()));
                    }
                    if ((dt.Rows[i]["ReplyUserName"] != null) && (dt.Rows[i]["ReplyUserName"].ToString() != ""))
                    {
                        item.ReplyUserName = dt.Rows[i]["ReplyUserName"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int QAId)
        {
            return this.dal.Delete(QAId);
        }

        public bool DeleteList(string QAIdlist)
        {
            return this.dal.DeleteList(QAIdlist);
        }

        public bool Exists(int QAId)
        {
            return this.dal.Exists(QAId);
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

        public DataSet GetListEX(int status)
        {
            if (status != -1)
            {
                return this.GetList("(parentid is null or parentid<0) and State=" + status);
            }
            return this.GetList("parentid is null or parentid<0");
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.ProductQA GetModel(int QAId)
        {
            return this.dal.GetModel(QAId);
        }

        public Maticsoft.Model.Shop.Products.ProductQA GetModelByCache(int QAId)
        {
            string cacheKey = "ProductQAModel-" + QAId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(QAId);
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
            return (Maticsoft.Model.Shop.Products.ProductQA) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductQA> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductQA> GetProductQAsByPage(long productId, string orderBy, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage("State=1 and ProductId=" + productId, orderBy, startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetReplyList(int ParentId, int status)
        {
            if (status != -1)
            {
                return this.GetList(string.Concat(new object[] { "parentid=", ParentId, " and State=", status }));
            }
            return this.GetList("parentid=" + ParentId);
        }

        public bool SetStatus(string ids, int status)
        {
            return this.dal.SetStatus(ids, status);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductQA model)
        {
            return this.dal.Update(model);
        }
    }
}

