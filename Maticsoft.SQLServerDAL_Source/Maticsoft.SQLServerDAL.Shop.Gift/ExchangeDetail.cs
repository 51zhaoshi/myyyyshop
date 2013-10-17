namespace Maticsoft.SQLServerDAL.Shop.Gift
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Gift;
    using Maticsoft.Model.Shop.Gift;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ExchangeDetail : IExchangeDetail
    {
        public int Add(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ExchangeDetail(");
            builder.Append("GiftID,UserID,OrderID,GiftName,CostScore,Status,Description,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@GiftID,@UserID,@OrderID,@GiftName,@CostScore,@Status,@Description,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GiftID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@GiftName", SqlDbType.NVarChar, 200), new SqlParameter("@CostScore", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.GiftID;
            cmdParms[1].Value = model.UserID;
            cmdParms[2].Value = model.OrderID;
            cmdParms[3].Value = model.GiftName;
            cmdParms[4].Value = model.CostScore;
            cmdParms[5].Value = model.Status;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int ExchangeDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ExchangeDetail ");
            builder.Append(" where ExchangeDetailID=@ExchangeDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ExchangeDetailID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ExchangeDetailIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ExchangeDetail ");
            builder.Append(" where ExchangeDetailID in (" + ExchangeDetailIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ExchangeDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ExchangeDetail");
            builder.Append(" where ExchangeDetailID=@ExchangeDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ExchangeDetailID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ExchangeDetailID,GiftID,UserID,OrderID,GiftName,CostScore,Status,Description,CreatedDate ");
            builder.Append(" FROM Shop_ExchangeDetail ");
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
            builder.Append(" ExchangeDetailID,GiftID,UserID,OrderID,GiftName,CostScore,Status,Description,CreatedDate ");
            builder.Append(" FROM Shop_ExchangeDetail ");
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
                builder.Append("order by T.ExchangeDetailID desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ExchangeDetail T ");
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
            return DbHelperSQL.GetMaxID("ExchangeDetailID", "Shop_ExchangeDetail");
        }

        public Maticsoft.Model.Shop.Gift.ExchangeDetail GetModel(int ExchangeDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ExchangeDetailID,GiftID,UserID,OrderID,GiftName,CostScore,Status,Description,CreatedDate from Shop_ExchangeDetail ");
            builder.Append(" where ExchangeDetailID=@ExchangeDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ExchangeDetailID;
            Maticsoft.Model.Shop.Gift.ExchangeDetail detail = new Maticsoft.Model.Shop.Gift.ExchangeDetail();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ExchangeDetailID"] != null) && (set.Tables[0].Rows[0]["ExchangeDetailID"].ToString() != ""))
            {
                detail.ExchangeDetailID = int.Parse(set.Tables[0].Rows[0]["ExchangeDetailID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["GiftID"] != null) && (set.Tables[0].Rows[0]["GiftID"].ToString() != ""))
            {
                detail.GiftID = int.Parse(set.Tables[0].Rows[0]["GiftID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
            {
                detail.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["OrderID"] != null) && (set.Tables[0].Rows[0]["OrderID"].ToString() != ""))
            {
                detail.OrderID = new int?(int.Parse(set.Tables[0].Rows[0]["OrderID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["GiftName"] != null) && (set.Tables[0].Rows[0]["GiftName"].ToString() != ""))
            {
                detail.GiftName = set.Tables[0].Rows[0]["GiftName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CostScore"] != null) && (set.Tables[0].Rows[0]["CostScore"].ToString() != ""))
            {
                detail.CostScore = int.Parse(set.Tables[0].Rows[0]["CostScore"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                detail.Status = int.Parse(set.Tables[0].Rows[0]["Status"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                detail.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                detail.CreatedDate = DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            return detail;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_ExchangeDetail ");
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

        public bool SetStatus(int detailId, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ExchangeDetail set ");
            builder.Append("Status=@Status");
            builder.Append(" where ExchangeDetailID=@ExchangeDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = status;
            cmdParms[1].Value = detailId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool SetStatusList(string detailIds, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ExchangeDetail set ");
            builder.Append("Status=@Status");
            builder.Append(" where ExchangeDetailID in (" + detailIds + ")");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ExchangeDetail set ");
            builder.Append("GiftID=@GiftID,");
            builder.Append("UserID=@UserID,");
            builder.Append("OrderID=@OrderID,");
            builder.Append("GiftName=@GiftName,");
            builder.Append("CostScore=@CostScore,");
            builder.Append("Status=@Status,");
            builder.Append("Description=@Description,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where ExchangeDetailID=@ExchangeDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GiftID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@GiftName", SqlDbType.NVarChar, 200), new SqlParameter("@CostScore", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.GiftID;
            cmdParms[1].Value = model.UserID;
            cmdParms[2].Value = model.OrderID;
            cmdParms[3].Value = model.GiftName;
            cmdParms[4].Value = model.CostScore;
            cmdParms[5].Value = model.Status;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.ExchangeDetailID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

