namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductQA : IProductQA
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductQA model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ProductQA(");
            builder.Append("ParentId,ProductId,UserId,UserName,Question,State,CreatedDate,ReplyContent,ReplyDate,ReplyUserId,ReplyUserName)");
            builder.Append(" values (");
            builder.Append("@ParentId,@ProductId,@UserId,@UserName,@Question,@State,@CreatedDate,@ReplyContent,@ReplyDate,@ReplyUserId,@ReplyUserName)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Question", SqlDbType.NVarChar), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ReplyContent", SqlDbType.NVarChar), new SqlParameter("@ReplyDate", SqlDbType.DateTime), new SqlParameter("@ReplyUserId", SqlDbType.Int, 4), new SqlParameter("@ReplyUserName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.ParentId;
            cmdParms[1].Value = model.ProductId;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.Question;
            cmdParms[5].Value = model.State;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.ReplyContent;
            cmdParms[8].Value = model.ReplyDate;
            cmdParms[9].Value = model.ReplyUserId;
            cmdParms[10].Value = model.ReplyUserName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int QAId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ProductQA ");
            builder.Append(" where QAId=@QAId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@QAId", SqlDbType.Int, 4) };
            cmdParms[0].Value = QAId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string QAIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ProductQA ");
            builder.Append(" where QAId in (" + QAIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int QAId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ProductQA");
            builder.Append(" where QAId=@QAId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@QAId", SqlDbType.Int, 4) };
            cmdParms[0].Value = QAId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select QAId,ParentId,ProductId,UserId,UserName,Question,State,CreatedDate,ReplyContent,ReplyDate,ReplyUserId,ReplyUserName ");
            builder.Append(" FROM Shop_ProductQA ");
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
            builder.Append(" QAId,ParentId,ProductId,UserId,UserName,Question,State,CreatedDate,ReplyContent,ReplyDate,ReplyUserId,ReplyUserName ");
            builder.Append(" FROM Shop_ProductQA ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.QAId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ProductQA T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("QAId", "Shop_ProductQA");
        }

        public Maticsoft.Model.Shop.Products.ProductQA GetModel(int QAId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 QAId,ParentId,ProductId,UserId,UserName,Question,State,CreatedDate,ReplyContent,ReplyDate,ReplyUserId,ReplyUserName from Shop_ProductQA ");
            builder.Append(" where QAId=@QAId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@QAId", SqlDbType.Int, 4) };
            cmdParms[0].Value = QAId;
            Maticsoft.Model.Shop.Products.ProductQA tqa = new Maticsoft.Model.Shop.Products.ProductQA();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["QAId"] != null) && (set.Tables[0].Rows[0]["QAId"].ToString() != ""))
            {
                tqa.QAId = int.Parse(set.Tables[0].Rows[0]["QAId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ParentId"] != null) && (set.Tables[0].Rows[0]["ParentId"].ToString() != ""))
            {
                tqa.ParentId = new int?(int.Parse(set.Tables[0].Rows[0]["ParentId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                tqa.ProductId = int.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserId"] != null) && (set.Tables[0].Rows[0]["UserId"].ToString() != ""))
            {
                tqa.UserId = int.Parse(set.Tables[0].Rows[0]["UserId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserName"] != null) && (set.Tables[0].Rows[0]["UserName"].ToString() != ""))
            {
                tqa.UserName = set.Tables[0].Rows[0]["UserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Question"] != null) && (set.Tables[0].Rows[0]["Question"].ToString() != ""))
            {
                tqa.Question = set.Tables[0].Rows[0]["Question"].ToString();
            }
            if ((set.Tables[0].Rows[0]["State"] != null) && (set.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                tqa.State = int.Parse(set.Tables[0].Rows[0]["State"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                tqa.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ReplyContent"] != null) && (set.Tables[0].Rows[0]["ReplyContent"].ToString() != ""))
            {
                tqa.ReplyContent = set.Tables[0].Rows[0]["ReplyContent"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ReplyDate"] != null) && (set.Tables[0].Rows[0]["ReplyDate"].ToString() != ""))
            {
                tqa.ReplyDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["ReplyDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ReplyUserId"] != null) && (set.Tables[0].Rows[0]["ReplyUserId"].ToString() != ""))
            {
                tqa.ReplyUserId = new int?(int.Parse(set.Tables[0].Rows[0]["ReplyUserId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ReplyUserName"] != null) && (set.Tables[0].Rows[0]["ReplyUserName"].ToString() != ""))
            {
                tqa.ReplyUserName = set.Tables[0].Rows[0]["ReplyUserName"].ToString();
            }
            return tqa;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_ProductQA ");
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

        public bool SetStatus(string ids, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ProductQA set ");
            builder.Append("State=@State");
            builder.Append(" where QAId in (" + ids + ")");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@State", SqlDbType.Int, 4) };
            cmdParms[0].Value = status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductQA model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ProductQA set ");
            builder.Append("ParentId=@ParentId,");
            builder.Append("ProductId=@ProductId,");
            builder.Append("UserId=@UserId,");
            builder.Append("UserName=@UserName,");
            builder.Append("Question=@Question,");
            builder.Append("State=@State,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("ReplyContent=@ReplyContent,");
            builder.Append("ReplyDate=@ReplyDate,");
            builder.Append("ReplyUserId=@ReplyUserId,");
            builder.Append("ReplyUserName=@ReplyUserName");
            builder.Append(" where QAId=@QAId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Question", SqlDbType.NVarChar), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ReplyContent", SqlDbType.NVarChar), new SqlParameter("@ReplyDate", SqlDbType.DateTime), new SqlParameter("@ReplyUserId", SqlDbType.Int, 4), new SqlParameter("@ReplyUserName", SqlDbType.NVarChar, 50), new SqlParameter("@QAId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ParentId;
            cmdParms[1].Value = model.ProductId;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.Question;
            cmdParms[5].Value = model.State;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.ReplyContent;
            cmdParms[8].Value = model.ReplyDate;
            cmdParms[9].Value = model.ReplyUserId;
            cmdParms[10].Value = model.ReplyUserName;
            cmdParms[11].Value = model.QAId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

