namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OnLineShopPhoto : IOnLineShopPhoto
    {
        public bool Add(Maticsoft.Model.SNS.OnLineShopPhoto model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_OnLineShopPhoto(");
            builder.Append("PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status)");
            builder.Append(" values (");
            builder.Append("@PhotoID,@ProductID,@CreatedUserId,@CreatedNickName,@CreatedDate,@Status)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.PhotoID;
            cmdParms[1].Value = model.ProductID;
            cmdParms[2].Value = model.CreatedUserId;
            cmdParms[3].Value = model.CreatedNickName;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.Status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.SNS.OnLineShopPhoto DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.OnLineShopPhoto photo = new Maticsoft.Model.SNS.OnLineShopPhoto();
            if (row != null)
            {
                if ((row["PhotoID"] != null) && (row["PhotoID"].ToString() != ""))
                {
                    photo.PhotoID = int.Parse(row["PhotoID"].ToString());
                }
                if ((row["ProductID"] != null) && (row["ProductID"].ToString() != ""))
                {
                    photo.ProductID = int.Parse(row["ProductID"].ToString());
                }
                if ((row["CreatedUserId"] != null) && (row["CreatedUserId"].ToString() != ""))
                {
                    photo.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    photo.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    photo.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    photo.Status = int.Parse(row["Status"].ToString());
                }
            }
            return photo;
        }

        public bool Delete(int PhotoID, int ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_OnLineShopPhoto ");
            builder.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            cmdParms[1].Value = ProductID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int PhotoID, int ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_OnLineShopPhoto");
            builder.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            cmdParms[1].Value = ProductID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status ");
            builder.Append(" FROM SNS_OnLineShopPhoto ");
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
            builder.Append(" PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status ");
            builder.Append(" FROM SNS_OnLineShopPhoto ");
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
                builder.Append("order by T.ProductID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_OnLineShopPhoto T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.OnLineShopPhoto GetModel(int PhotoID, int ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 PhotoID,ProductID,CreatedUserId,CreatedNickName,CreatedDate,Status from SNS_OnLineShopPhoto ");
            builder.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            cmdParms[1].Value = ProductID;
            new Maticsoft.Model.SNS.OnLineShopPhoto();
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
            builder.Append("select count(1) FROM SNS_OnLineShopPhoto ");
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

        public bool Update(Maticsoft.Model.SNS.OnLineShopPhoto model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_OnLineShopPhoto set ");
            builder.Append("CreatedUserId=@CreatedUserId,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status");
            builder.Append(" where PhotoID=@PhotoID and ProductID=@ProductID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@PhotoID", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CreatedUserId;
            cmdParms[1].Value = model.CreatedNickName;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.PhotoID;
            cmdParms[5].Value = model.ProductID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

