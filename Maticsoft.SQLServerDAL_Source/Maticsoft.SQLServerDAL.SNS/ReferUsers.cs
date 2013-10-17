namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ReferUsers : IReferUsers
    {
        public int Add(Maticsoft.Model.SNS.ReferUsers model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_ReferUsers(");
            builder.Append("TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead)");
            builder.Append(" values (");
            builder.Append("@TagetID,@Type,@ReferUserID,@ReferNickName,@CreatedDate,@IsRead)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ReferUserID", SqlDbType.Int, 4), new SqlParameter("@ReferNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRead", SqlDbType.Bit, 1) };
            cmdParms[0].Value = model.TagetID;
            cmdParms[1].Value = model.Type;
            cmdParms[2].Value = model.ReferUserID;
            cmdParms[3].Value = model.ReferNickName;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.IsRead;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.ReferUsers DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.ReferUsers users = new Maticsoft.Model.SNS.ReferUsers();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    users.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["TagetID"] != null) && (row["TagetID"].ToString() != ""))
                {
                    users.TagetID = int.Parse(row["TagetID"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    users.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["ReferUserID"] != null) && (row["ReferUserID"].ToString() != ""))
                {
                    users.ReferUserID = int.Parse(row["ReferUserID"].ToString());
                }
                if (row["ReferNickName"] != null)
                {
                    users.ReferNickName = row["ReferNickName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    users.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["IsRead"] == null) || !(row["IsRead"].ToString() != ""))
                {
                    return users;
                }
                if ((row["IsRead"].ToString() == "1") || (row["IsRead"].ToString().ToLower() == "true"))
                {
                    users.IsRead = true;
                    return users;
                }
                users.IsRead = false;
            }
            return users;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_ReferUsers ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_ReferUsers ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead ");
            builder.Append(" FROM SNS_ReferUsers ");
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
            builder.Append(" ID,TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead ");
            builder.Append(" FROM SNS_ReferUsers ");
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
            builder.Append(")AS Row, T.*  from SNS_ReferUsers T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.ReferUsers GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,TagetID,Type,ReferUserID,ReferNickName,CreatedDate,IsRead from SNS_ReferUsers ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.ReferUsers();
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
            builder.Append("select count(1) FROM SNS_ReferUsers ");
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

        public int GetReferNotReadCountByType(int UserId, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_ReferUsers ");
            if (UserId > 0)
            {
                builder.Append(string.Concat(new object[] { "where ReferUserID=", UserId, " and Type=", Type, " and IsRead='False'" }));
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.SNS.ReferUsers model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_ReferUsers set ");
            builder.Append("TagetID=@TagetID,");
            builder.Append("Type=@Type,");
            builder.Append("ReferUserID=@ReferUserID,");
            builder.Append("ReferNickName=@ReferNickName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("IsRead=@IsRead");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ReferUserID", SqlDbType.Int, 4), new SqlParameter("@ReferNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TagetID;
            cmdParms[1].Value = model.Type;
            cmdParms[2].Value = model.ReferUserID;
            cmdParms[3].Value = model.ReferNickName;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.IsRead;
            cmdParms[6].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateReferStateToRead(int UserID, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_ReferUsers set ");
            builder.Append("IsRead='True'");
            builder.Append(string.Concat(new object[] { " where ReferUserID=", UserID, " and Type=", Type }));
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

