namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserAlbumsType : IUserAlbumsType
    {
        public bool Add(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserAlbumsType(");
            builder.Append("AlbumsID,TypeID,AlbumsUserID)");
            builder.Append(" values (");
            builder.Append("@AlbumsID,@TypeID,@AlbumsUserID)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumsID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4), new SqlParameter("@AlbumsUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumsID;
            cmdParms[1].Value = model.TypeID;
            cmdParms[2].Value = model.AlbumsUserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.SNS.UserAlbumsType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserAlbumsType type = new Maticsoft.Model.SNS.UserAlbumsType();
            if (row != null)
            {
                if ((row["AlbumsID"] != null) && (row["AlbumsID"].ToString() != ""))
                {
                    type.AlbumsID = int.Parse(row["AlbumsID"].ToString());
                }
                if ((row["TypeID"] != null) && (row["TypeID"].ToString() != ""))
                {
                    type.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if ((row["AlbumsUserID"] != null) && (row["AlbumsUserID"].ToString() != ""))
                {
                    type.AlbumsUserID = new int?(int.Parse(row["AlbumsUserID"].ToString()));
                }
            }
            return type;
        }

        public bool Delete(int AlbumsID, int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbumsType ");
            builder.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumsID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumsID;
            cmdParms[1].Value = TypeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int AlbumsID, int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserAlbumsType");
            builder.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumsID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumsID;
            cmdParms[1].Value = TypeID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AlbumsID,TypeID,AlbumsUserID ");
            builder.Append(" FROM SNS_UserAlbumsType ");
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
            builder.Append(" AlbumsID,TypeID,AlbumsUserID ");
            builder.Append(" FROM SNS_UserAlbumsType ");
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
                builder.Append("order by T.TypeID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserAlbumsType T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.UserAlbumsType GetModel(int AlbumsID, int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AlbumsID,TypeID,AlbumsUserID from SNS_UserAlbumsType ");
            builder.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumsID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumsID;
            cmdParms[1].Value = TypeID;
            new Maticsoft.Model.SNS.UserAlbumsType();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Maticsoft.Model.SNS.UserAlbumsType GetModelByUserId(int AlbumsID, int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AlbumsID,TypeID,AlbumsUserID from SNS_UserAlbumsType ");
            builder.Append(" where AlbumsID=@AlbumsID and AlbumsUserID=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumsID", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumsID;
            cmdParms[1].Value = UserId;
            new Maticsoft.Model.SNS.UserAlbumsType();
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
            builder.Append("select count(1) FROM SNS_UserAlbumsType ");
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

        public bool Update(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbumsType set ");
            builder.Append("AlbumsUserID=@AlbumsUserID");
            builder.Append(" where AlbumsID=@AlbumsID and TypeID=@TypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumsUserID", SqlDbType.Int, 4), new SqlParameter("@AlbumsID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumsUserID;
            cmdParms[1].Value = model.AlbumsID;
            cmdParms[2].Value = model.TypeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateType(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbumsType set ");
            builder.Append("TypeID=@TypeID");
            builder.Append(" where AlbumsID=@AlbumsID and AlbumsUserID=@AlbumsUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumsUserID", SqlDbType.Int, 4), new SqlParameter("@AlbumsID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumsUserID;
            cmdParms[1].Value = model.AlbumsID;
            cmdParms[2].Value = model.TypeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

