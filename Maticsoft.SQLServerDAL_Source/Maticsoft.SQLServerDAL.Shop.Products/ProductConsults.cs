namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductConsults : IProductConsults
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductConsults model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ProductConsults(");
            builder.Append("TypeId,UserId,ProductId,UserName,UserEmail,ConsultationText,CreatedDate,ReplyDate,IsReply,ReplyText,ReplyUserId,ReplyUserName,Status,Recomend)");
            builder.Append(" values (");
            builder.Append("@TypeId,@UserId,@ProductId,@UserName,@UserEmail,@ConsultationText,@CreatedDate,@ReplyDate,@IsReply,@ReplyText,@ReplyUserId,@ReplyUserName,@Status,@Recomend)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 50), new SqlParameter("@ConsultationText", SqlDbType.NVarChar, -1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ReplyDate", SqlDbType.DateTime), new SqlParameter("@IsReply", SqlDbType.Bit, 1), new SqlParameter("@ReplyText", SqlDbType.NVarChar, -1), new SqlParameter("@ReplyUserId", SqlDbType.Int, 4), new SqlParameter("@ReplyUserName", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Recomend", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.ProductId;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.UserEmail;
            cmdParms[5].Value = model.ConsultationText;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.ReplyDate;
            cmdParms[8].Value = model.IsReply;
            cmdParms[9].Value = model.ReplyText;
            cmdParms[10].Value = model.ReplyUserId;
            cmdParms[11].Value = model.ReplyUserName;
            cmdParms[12].Value = model.Status;
            cmdParms[13].Value = model.Recomend;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Products.ProductConsults DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.ProductConsults consults = new Maticsoft.Model.Shop.Products.ProductConsults();
            if (row != null)
            {
                if ((row["ConsultationId"] != null) && (row["ConsultationId"].ToString() != ""))
                {
                    consults.ConsultationId = int.Parse(row["ConsultationId"].ToString());
                }
                if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                {
                    consults.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    consults.UserId = int.Parse(row["UserId"].ToString());
                }
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    consults.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["UserName"] != null)
                {
                    consults.UserName = row["UserName"].ToString();
                }
                if (row["UserEmail"] != null)
                {
                    consults.UserEmail = row["UserEmail"].ToString();
                }
                if (row["ConsultationText"] != null)
                {
                    consults.ConsultationText = row["ConsultationText"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    consults.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["ReplyDate"] != null) && (row["ReplyDate"].ToString() != ""))
                {
                    consults.ReplyDate = new DateTime?(DateTime.Parse(row["ReplyDate"].ToString()));
                }
                if ((row["IsReply"] != null) && (row["IsReply"].ToString() != ""))
                {
                    if ((row["IsReply"].ToString() == "1") || (row["IsReply"].ToString().ToLower() == "true"))
                    {
                        consults.IsReply = true;
                    }
                    else
                    {
                        consults.IsReply = false;
                    }
                }
                if (row["ReplyText"] != null)
                {
                    consults.ReplyText = row["ReplyText"].ToString();
                }
                if ((row["ReplyUserId"] != null) && (row["ReplyUserId"].ToString() != ""))
                {
                    consults.ReplyUserId = int.Parse(row["ReplyUserId"].ToString());
                }
                if (row["ReplyUserName"] != null)
                {
                    consults.ReplyUserName = row["ReplyUserName"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    consults.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["Recomend"] != null) && (row["Recomend"].ToString() != ""))
                {
                    consults.Recomend = int.Parse(row["Recomend"].ToString());
                }
            }
            return consults;
        }

        public bool Delete(int ConsultationId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ProductConsults ");
            builder.Append(" where ConsultationId=@ConsultationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ConsultationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ConsultationId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ConsultationIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ProductConsults ");
            builder.Append(" where ConsultationId in (" + ConsultationIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ConsultationId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ProductConsults");
            builder.Append(" where ConsultationId=@ConsultationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ConsultationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ConsultationId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ConsultationId,TypeId,UserId,ProductId,UserName,UserEmail,ConsultationText,CreatedDate,ReplyDate,IsReply,ReplyText,ReplyUserId,ReplyUserName,Status,Recomend ");
            builder.Append(" FROM Shop_ProductConsults ");
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
            builder.Append(" ConsultationId,TypeId,UserId,ProductId,UserName,UserEmail,ConsultationText,CreatedDate,ReplyDate,IsReply,ReplyText,ReplyUserId,ReplyUserName,Status,Recomend ");
            builder.Append(" FROM Shop_ProductConsults ");
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
                builder.Append("order by T.ConsultationId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ProductConsults T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ConsultationId", "Shop_ProductConsults");
        }

        public Maticsoft.Model.Shop.Products.ProductConsults GetModel(int ConsultationId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ConsultationId,TypeId,UserId,ProductId,UserName,UserEmail,ConsultationText,CreatedDate,ReplyDate,IsReply,ReplyText,ReplyUserId,ReplyUserName,Status,Recomend from Shop_ProductConsults ");
            builder.Append(" where ConsultationId=@ConsultationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ConsultationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ConsultationId;
            new Maticsoft.Model.Shop.Products.ProductConsults();
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
            builder.Append("select count(1) FROM Shop_ProductConsults ");
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

        public bool Update(Maticsoft.Model.Shop.Products.ProductConsults model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ProductConsults set ");
            builder.Append("TypeId=@TypeId,");
            builder.Append("UserId=@UserId,");
            builder.Append("ProductId=@ProductId,");
            builder.Append("UserName=@UserName,");
            builder.Append("UserEmail=@UserEmail,");
            builder.Append("ConsultationText=@ConsultationText,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("ReplyDate=@ReplyDate,");
            builder.Append("IsReply=@IsReply,");
            builder.Append("ReplyText=@ReplyText,");
            builder.Append("ReplyUserId=@ReplyUserId,");
            builder.Append("ReplyUserName=@ReplyUserName,");
            builder.Append("Status=@Status,");
            builder.Append("Recomend=@Recomend");
            builder.Append(" where ConsultationId=@ConsultationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 50), new SqlParameter("@ConsultationText", SqlDbType.NVarChar, -1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ReplyDate", SqlDbType.DateTime), new SqlParameter("@IsReply", SqlDbType.Bit, 1), new SqlParameter("@ReplyText", SqlDbType.NVarChar, -1), new SqlParameter("@ReplyUserId", SqlDbType.Int, 4), new SqlParameter("@ReplyUserName", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Recomend", SqlDbType.Int, 4), new SqlParameter("@ConsultationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.ProductId;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.UserEmail;
            cmdParms[5].Value = model.ConsultationText;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.ReplyDate;
            cmdParms[8].Value = model.IsReply;
            cmdParms[9].Value = model.ReplyText;
            cmdParms[10].Value = model.ReplyUserId;
            cmdParms[11].Value = model.ReplyUserName;
            cmdParms[12].Value = model.Status;
            cmdParms[13].Value = model.Recomend;
            cmdParms[14].Value = model.ConsultationId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatusList(string ids, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ProductConsults set ");
            builder.Append("Status=@Status");
            builder.Append(" where ConsultationId in (" + ids + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

