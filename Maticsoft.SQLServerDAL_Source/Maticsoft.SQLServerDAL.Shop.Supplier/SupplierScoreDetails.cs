namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SupplierScoreDetails : ISupplierScoreDetails
    {
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierScoreDetails model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SupplierScoreDetails(");
            builder.Append("Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark)");
            builder.Append(" values (");
            builder.Append("@Score,@ScoreType,@CreatedDate,@CreatedUserId,@Status,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Score", SqlDbType.Money, 8), new SqlParameter("@ScoreType", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.Score;
            cmdParms[1].Value = model.ScoreType;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.CreatedUserId;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierScoreDetails DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierScoreDetails details = new Maticsoft.Model.Shop.Supplier.SupplierScoreDetails();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    details.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["Score"] != null) && (row["Score"].ToString() != ""))
                {
                    details.Score = decimal.Parse(row["Score"].ToString());
                }
                if ((row["ScoreType"] != null) && (row["ScoreType"].ToString() != ""))
                {
                    details.ScoreType = int.Parse(row["ScoreType"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    details.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CreatedUserId"] != null) && (row["CreatedUserId"].ToString() != ""))
                {
                    details.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    details.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Remark"] != null)
                {
                    details.Remark = row["Remark"].ToString();
                }
            }
            return details;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierScoreDetails ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierScoreDetails ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SupplierScoreDetails");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark ");
            builder.Append(" FROM Shop_SupplierScoreDetails ");
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
            builder.Append(" ID,Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark ");
            builder.Append(" FROM Shop_SupplierScoreDetails ");
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SupplierScoreDetails T ");
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
            return DbHelperSQL.GetMaxID("ID", "Shop_SupplierScoreDetails");
        }

        public Maticsoft.Model.Shop.Supplier.SupplierScoreDetails GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,Score,ScoreType,CreatedDate,CreatedUserId,Status,Remark from Shop_SupplierScoreDetails ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.Shop.Supplier.SupplierScoreDetails();
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
            builder.Append("select count(1) FROM Shop_SupplierScoreDetails ");
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

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierScoreDetails model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SupplierScoreDetails set ");
            builder.Append("Score=@Score,");
            builder.Append("ScoreType=@ScoreType,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserId=@CreatedUserId,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Score", SqlDbType.Money, 8), new SqlParameter("@ScoreType", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Score;
            cmdParms[1].Value = model.ScoreType;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.CreatedUserId;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.Remark;
            cmdParms[6].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

