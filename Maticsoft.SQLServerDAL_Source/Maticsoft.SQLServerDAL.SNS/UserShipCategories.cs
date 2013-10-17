namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserShipCategories : IUserShipCategories
    {
        public int Add(Maticsoft.Model.SNS.UserShipCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserShipCategories(");
            builder.Append("CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy)");
            builder.Append(" values (");
            builder.Append("@CreatedUserID,@CategoryName,@Description,@Sequence,@LastUpdatedDate,@CreatedDate,@Privacy)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CategoryName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2) };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CategoryName;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.Sequence;
            cmdParms[4].Value = model.LastUpdatedDate;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.Privacy;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.UserShipCategories DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserShipCategories categories = new Maticsoft.Model.SNS.UserShipCategories();
            if (row != null)
            {
                if ((row["CategoryID"] != null) && (row["CategoryID"].ToString() != ""))
                {
                    categories.CategoryID = int.Parse(row["CategoryID"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    categories.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CategoryName"] != null)
                {
                    categories.CategoryName = row["CategoryName"].ToString();
                }
                if (row["Description"] != null)
                {
                    categories.Description = row["Description"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    categories.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["LastUpdatedDate"] != null) && (row["LastUpdatedDate"].ToString() != ""))
                {
                    categories.LastUpdatedDate = new DateTime?(DateTime.Parse(row["LastUpdatedDate"].ToString()));
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    categories.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Privacy"] != null) && (row["Privacy"].ToString() != ""))
                {
                    categories.Privacy = int.Parse(row["Privacy"].ToString());
                }
            }
            return categories;
        }

        public bool Delete(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserShipCategories ");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CategoryIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserShipCategories ");
            builder.Append(" where CategoryID in (" + CategoryIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserShipCategories");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryID,CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy ");
            builder.Append(" FROM SNS_UserShipCategories ");
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
            builder.Append(" CategoryID,CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy ");
            builder.Append(" FROM SNS_UserShipCategories ");
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
                builder.Append("order by T.CategoryID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserShipCategories T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.UserShipCategories GetModel(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryID,CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy from SNS_UserShipCategories ");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            new Maticsoft.Model.SNS.UserShipCategories();
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
            builder.Append("select count(1) FROM SNS_UserShipCategories ");
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

        public bool Update(Maticsoft.Model.SNS.UserShipCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserShipCategories set ");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CategoryName=@CategoryName,");
            builder.Append("Description=@Description,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("LastUpdatedDate=@LastUpdatedDate,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Privacy=@Privacy");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CategoryName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CategoryName;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.Sequence;
            cmdParms[4].Value = model.LastUpdatedDate;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.Privacy;
            cmdParms[7].Value = model.CategoryID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

