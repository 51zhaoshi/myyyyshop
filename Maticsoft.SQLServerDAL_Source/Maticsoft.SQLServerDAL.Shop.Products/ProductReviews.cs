namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductReviews : IProductReviews
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductReviews model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ProductReviews(");
            builder.Append("ProductId,UserId,ReviewText,UserName,UserEmail,CreatedDate,ParentId,Status,OrderId,SKU,Attribute,ImagesPath,ImagesNames)");
            builder.Append(" values (");
            builder.Append("@ProductId,@UserId,@ReviewText,@UserName,@UserEmail,@CreatedDate,@ParentId,@Status,@OrderId,@SKU,@Attribute,@ImagesPath,@ImagesNames)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ReviewText", SqlDbType.NVarChar, -1), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@ImagesPath", SqlDbType.NVarChar, 300), new SqlParameter("@ImagesNames", SqlDbType.NVarChar, 500) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.ReviewText;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.UserEmail;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.ParentId;
            cmdParms[7].Value = model.Status;
            cmdParms[8].Value = model.OrderId;
            cmdParms[9].Value = model.SKU;
            cmdParms[10].Value = model.Attribute;
            cmdParms[11].Value = model.ImagesPath;
            cmdParms[12].Value = model.ImagesNames;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddEx(List<Maticsoft.Model.Shop.Products.ProductReviews> modelList, long OrderId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            for (int i = 0; i < modelList.Count; i++)
            {
                Maticsoft.Model.Shop.Products.ProductReviews reviews = modelList[i];
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into Shop_ProductReviews(");
                builder.Append("ProductId,UserId,ReviewText,UserName,UserEmail,CreatedDate,ParentId,Status,OrderId,SKU,Attribute,ImagesPath,ImagesNames)");
                builder.Append(" values (");
                builder.Append("@ProductId,@UserId,@ReviewText,@UserName,@UserEmail,@CreatedDate,@ParentId,@Status,@OrderId,@SKU,@Attribute,@ImagesPath,@ImagesNames)");
                builder.Append(";select @@IDENTITY");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ReviewText", SqlDbType.NVarChar, -1), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@ImagesPath", SqlDbType.NVarChar, 300), new SqlParameter("@ImagesNames", SqlDbType.NVarChar, 500) };
                parameterArray[0].Value = reviews.ProductId;
                parameterArray[1].Value = reviews.UserId;
                parameterArray[2].Value = reviews.ReviewText;
                parameterArray[3].Value = reviews.UserName;
                parameterArray[4].Value = reviews.UserEmail;
                parameterArray[5].Value = reviews.CreatedDate;
                parameterArray[6].Value = reviews.ParentId;
                parameterArray[7].Value = reviews.Status;
                parameterArray[8].Value = reviews.OrderId;
                parameterArray[9].Value = reviews.SKU;
                parameterArray[10].Value = reviews.Attribute;
                parameterArray[11].Value = reviews.ImagesPath;
                parameterArray[12].Value = reviews.ImagesNames;
                cmdList.Add(new CommandInfo(builder.ToString(), parameterArray));
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Shop_Orders set ");
            builder2.Append("IsReviews=@IsReviews");
            builder2.Append(" where OrderId=@OrderId");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@IsReviews", SqlDbType.Bit, 1), new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            para[0].Value = true;
            para[1].Value = OrderId;
            cmdList.Add(new CommandInfo(builder2.ToString(), para));
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool AuditComment(string ids, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductReviews ");
            builder.AppendFormat("SET Status ={0}  ", status);
            builder.AppendFormat("WHERE ReviewId IN({0}) ", ids);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public Maticsoft.Model.Shop.Products.ProductReviews DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.ProductReviews reviews = new Maticsoft.Model.Shop.Products.ProductReviews();
            if (row != null)
            {
                if ((row["ReviewId"] != null) && (row["ReviewId"].ToString() != ""))
                {
                    reviews.ReviewId = int.Parse(row["ReviewId"].ToString());
                }
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    reviews.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    reviews.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["ReviewText"] != null)
                {
                    reviews.ReviewText = row["ReviewText"].ToString();
                }
                if (row["UserName"] != null)
                {
                    reviews.UserName = row["UserName"].ToString();
                }
                if (row["UserEmail"] != null)
                {
                    reviews.UserEmail = row["UserEmail"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    reviews.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["ParentId"] != null) && (row["ParentId"].ToString() != ""))
                {
                    reviews.ParentId = int.Parse(row["ParentId"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    reviews.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["OrderId"] != null) && (row["OrderId"].ToString() != ""))
                {
                    reviews.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["SKU"] != null)
                {
                    reviews.SKU = row["SKU"].ToString();
                }
                if (row["Attribute"] != null)
                {
                    reviews.Attribute = row["Attribute"].ToString();
                }
                if (row["ImagesPath"] != null)
                {
                    reviews.ImagesPath = row["ImagesPath"].ToString();
                }
                if (row["ImagesNames"] != null)
                {
                    reviews.ImagesNames = row["ImagesNames"].ToString();
                }
            }
            return reviews;
        }

        public bool Delete(int ReviewId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ProductReviews ");
            builder.Append(" where ReviewId=@ReviewId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReviewId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReviewId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ReviewIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ProductReviews ");
            builder.Append(" where ReviewId in (" + ReviewIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ReviewId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ProductReviews");
            builder.Append(" where ReviewId=@ReviewId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReviewId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReviewId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(int? Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.*,b.Score FROM Shop_ProductReviews A ");
            builder.Append("LEFT JOIN Shop_ScoreDetails B ON A.ReviewId = B.ReviewId ");
            if (Status.HasValue)
            {
                builder.AppendFormat("WHERE Status ={0} ", Status.Value);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReviewId,ProductId,UserId,ReviewText,UserName,UserEmail,CreatedDate,ParentId,Status,OrderId,SKU,Attribute,ImagesPath,ImagesNames ");
            builder.Append(" FROM Shop_ProductReviews ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ReviewId,ProductId,UserId,ReviewText,UserName,UserEmail,CreatedDate,ParentId,Status,OrderId,SKU,Attribute,ImagesPath,ImagesNames ");
            builder.Append(" FROM Shop_ProductReviews ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.ReviewId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ProductReviews T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListsProdRev(int? Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT prorev.*,orderitems.ThumbnailsUrl as ThumbnailsUrl, orderitems.Name as ProductName  FROM  Shop_ProductReviews  AS  prorev    ");
            builder.Append(" inner JOIN  Shop_OrderItems  AS  orderitems   ON prorev.SKU = orderitems.SKU  and  prorev.OrderId = orderitems.OrderId ");
            if (Status.HasValue)
            {
                builder.AppendFormat("WHERE Status ={0} ", Status.Value);
            }
            builder.AppendFormat(" ORDER BY prorev.ReviewId DESC  ", new object[0]);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ReviewId", "Shop_ProductReviews");
        }

        public Maticsoft.Model.Shop.Products.ProductReviews GetModel(int ReviewId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ReviewId,ProductId,UserId,ReviewText,UserName,UserEmail,CreatedDate,ParentId,Status,OrderId,SKU,Attribute,ImagesPath,ImagesNames from Shop_ProductReviews ");
            builder.Append(" where ReviewId=@ReviewId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReviewId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReviewId;
            new Maticsoft.Model.Shop.Products.ProductReviews();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_ProductReviews ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductReviews model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ProductReviews set ");
            builder.Append("ProductId=@ProductId,");
            builder.Append("UserId=@UserId,");
            builder.Append("ReviewText=@ReviewText,");
            builder.Append("UserName=@UserName,");
            builder.Append("UserEmail=@UserEmail,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("ParentId=@ParentId,");
            builder.Append("Status=@Status,");
            builder.Append("OrderId=@OrderId,");
            builder.Append("SKU=@SKU,");
            builder.Append("Attribute=@Attribute,");
            builder.Append("ImagesPath=@ImagesPath,");
            builder.Append("ImagesNames=@ImagesNames");
            builder.Append(" where ReviewId=@ReviewId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ReviewText", SqlDbType.NVarChar, -1), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@ImagesPath", SqlDbType.NVarChar, 300), new SqlParameter("@ImagesNames", SqlDbType.NVarChar, 500), new SqlParameter("@ReviewId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.ReviewText;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.UserEmail;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.ParentId;
            cmdParms[7].Value = model.Status;
            cmdParms[8].Value = model.OrderId;
            cmdParms[9].Value = model.SKU;
            cmdParms[10].Value = model.Attribute;
            cmdParms[11].Value = model.ImagesPath;
            cmdParms[12].Value = model.ImagesNames;
            cmdParms[13].Value = model.ReviewId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

