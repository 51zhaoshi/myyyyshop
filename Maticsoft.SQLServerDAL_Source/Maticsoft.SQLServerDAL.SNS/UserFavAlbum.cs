namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserFavAlbum : IUserFavAlbum
    {
        public int Add(Maticsoft.Model.SNS.UserFavAlbum model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserFavAlbum(");
            builder.Append("AlbumID,UserID,Tags,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@AlbumID,@UserID,@Tags,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.AlbumID;
            cmdParms[1].Value = model.UserID;
            cmdParms[2].Value = model.Tags;
            cmdParms[3].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.UserFavAlbum DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserFavAlbum album = new Maticsoft.Model.SNS.UserFavAlbum();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    album.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["AlbumID"] != null) && (row["AlbumID"].ToString() != ""))
                {
                    album.AlbumID = int.Parse(row["AlbumID"].ToString());
                }
                if ((row["UserID"] != null) && (row["UserID"].ToString() != ""))
                {
                    album.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["Tags"] != null)
                {
                    album.Tags = row["Tags"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    album.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return album;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserFavAlbum ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int AlbumID, int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserFavAlbum ");
            builder.Append(" where AlbumID=@AlbumID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            cmdParms[1].Value = UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserFavAlbum ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int AlbumID, int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserFavAlbum");
            builder.Append(" where AlbumID=@AlbumID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            cmdParms[1].Value = UserID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public int FavAlbum(int AlbumId, int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserFavAlbum(");
            builder.Append("AlbumID,UserID,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@AlbumID,@UserID,@CreatedDate)");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            para[0].Value = AlbumId;
            para[1].Value = UserId;
            para[2].Value = DateTime.Now;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update SNS_UserAlbums set ");
            builder2.Append("FavouriteCount=FavouriteCount+1 ");
            builder2.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = AlbumId;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return DbHelperSQL.ExecuteSqlTran(cmdList);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,AlbumID,UserID,Tags,CreatedDate ");
            builder.Append(" FROM SNS_UserFavAlbum ");
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
            builder.Append(" ID,AlbumID,UserID,Tags,CreatedDate ");
            builder.Append(" FROM SNS_UserFavAlbum ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            else
            {
                builder.Append(" order by ID desc");
            }
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
            builder.Append(")AS Row, T.*  from SNS_UserFavAlbum T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" SELECT SNSFA.*,SNSUA.AlbumName,AU.NickName ");
            builder.Append(" FROM SNS_UserFavAlbum SNSFA ");
            builder.Append(" LEFT JOIN SNS_UserAlbums SNSUA ON SNSUA.AlbumID=SNSFA.AlbumID ");
            builder.Append(" LEFT JOIN Accounts_Users AU ON AU.UserID=SNSFA.UserID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            else
            {
                builder.Append(" order by ID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.UserFavAlbum GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,AlbumID,UserID,Tags,CreatedDate from SNS_UserFavAlbum ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.UserFavAlbum();
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
            builder.Append("select count(1) FROM SNS_UserFavAlbum ");
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

        public int UnFavAlbum(int AlbumId, int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserFavAlbum ");
            builder.Append(" where AlbumID=@AlbumID AND UserID=@UserID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = AlbumId;
            para[1].Value = UserId;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update SNS_UserAlbums set ");
            builder2.Append("FavouriteCount=FavouriteCount-1 ");
            builder2.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            para[0].Value = AlbumId;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return DbHelperSQL.ExecuteSqlTran(cmdList);
        }

        public bool Update(Maticsoft.Model.SNS.UserFavAlbum model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserFavAlbum set ");
            builder.Append("Tags=@Tags,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Tags;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.ID;
            cmdParms[3].Value = model.AlbumID;
            cmdParms[4].Value = model.UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

