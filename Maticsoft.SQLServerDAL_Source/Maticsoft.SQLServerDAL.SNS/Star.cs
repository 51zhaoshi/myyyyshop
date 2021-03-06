namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Star : IStar
    {
        public int Add(Maticsoft.Model.SNS.Star model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Star(");
            builder.Append("UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status)");
            builder.Append(" values (");
            builder.Append("@UserID,@TypeID,@NickName,@UserGravatar,@ApplyReason,@CreatedDate,@ExpiredDate,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@UserGravatar", SqlDbType.NVarChar, 200), new SqlParameter("@ApplyReason", SqlDbType.NVarChar), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ExpiredDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.TypeID;
            cmdParms[2].Value = model.NickName;
            cmdParms[3].Value = model.UserGravatar;
            cmdParms[4].Value = model.ApplyReason;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.ExpiredDate;
            cmdParms[7].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.Star DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Star star = new Maticsoft.Model.SNS.Star();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    star.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["UserID"] != null) && (row["UserID"].ToString() != ""))
                {
                    star.UserID = int.Parse(row["UserID"].ToString());
                }
                if ((row["TypeID"] != null) && (row["TypeID"].ToString() != ""))
                {
                    star.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if (row["NickName"] != null)
                {
                    star.NickName = row["NickName"].ToString();
                }
                if (row["UserGravatar"] != null)
                {
                    star.UserGravatar = row["UserGravatar"].ToString();
                }
                if (row["ApplyReason"] != null)
                {
                    star.ApplyReason = row["ApplyReason"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    star.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["ExpiredDate"] != null) && (row["ExpiredDate"].ToString() != ""))
                {
                    star.ExpiredDate = new DateTime?(DateTime.Parse(row["ExpiredDate"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    star.Status = new int?(int.Parse(row["Status"].ToString()));
                }
            }
            return star;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Star ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Star ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteList(string IDlist, out DataSet ds)
        {
            ds = this.GetList("ID in (" + IDlist + ") ");
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Star ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int UserID, int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Star");
            builder.Append(" where UserID=@UserID and TypeID=@TypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = TypeID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status ");
            builder.Append(" FROM SNS_Star ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" ORDER BY CreatedDate DESC");
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
            builder.Append(" ID,UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status ");
            builder.Append(" FROM SNS_Star ");
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
            builder.Append(")AS Row, T.*  from SNS_Star T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Star GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,UserID,TypeID,NickName,UserGravatar,ApplyReason,CreatedDate,ExpiredDate,Status from SNS_Star ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.Star();
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
            builder.Append("select count(1) FROM SNS_Star ");
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

        public bool IsExists(int UserID, int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Star");
            builder.Append(" where UserID=@UserID and TypeID=@TypeID and Status=1 ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = TypeID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool IsStar(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM SNS_Star ");
            builder.AppendFormat("WHERE UserID={0} AND Status =1 ", userId);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return false;
            }
            return (Convert.ToInt32(single) > 0);
        }

        public DataSet StarName(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ST.TypeName FROM SNS_Star S ");
            builder.Append("LEFT JOIN SNS_StarType ST ON S.TypeID = ST.TypeID ");
            builder.AppendFormat("WHERE UserID={0} AND S.Status=1 ", userId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Maticsoft.Model.SNS.Star model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Star set ");
            builder.Append("UserID=@UserID,");
            builder.Append("TypeID=@TypeID,");
            builder.Append("NickName=@NickName,");
            builder.Append("UserGravatar=@UserGravatar,");
            builder.Append("ApplyReason=@ApplyReason,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("ExpiredDate=@ExpiredDate,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@UserGravatar", SqlDbType.NVarChar, 200), new SqlParameter("@ApplyReason", SqlDbType.NVarChar), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ExpiredDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.TypeID;
            cmdParms[2].Value = model.NickName;
            cmdParms[3].Value = model.UserGravatar;
            cmdParms[4].Value = model.ApplyReason;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.ExpiredDate;
            cmdParms[7].Value = model.Status;
            cmdParms[8].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStateList(string IDlist, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Star set ");
            builder.AppendFormat("Status={0} ", status);
            builder.AppendFormat(" where ID in ({0})", IDlist);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

