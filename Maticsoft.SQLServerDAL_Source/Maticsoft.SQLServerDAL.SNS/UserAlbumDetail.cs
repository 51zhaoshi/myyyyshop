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

    public class UserAlbumDetail : IUserAlbumDetail
    {
        public int Add(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserAlbumDetail(");
            builder.Append("AlbumID,TargetID,Type,Description)");
            builder.Append(" values (");
            builder.Append("@AlbumID,@TargetID,@Type,@Description)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@AlbumUserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumID;
            cmdParms[1].Value = model.TargetID;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.AlbumUserId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddEx(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserAlbumDetail(");
            builder.Append("AlbumID,TargetID,Type,Description,AlbumUserId)");
            builder.Append(" values (");
            builder.Append("@AlbumID,@TargetID,@Type,@Description,@AlbumUserId)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@AlbumUserId", SqlDbType.Int, 4) };
            para[0].Value = model.AlbumID;
            para[1].Value = model.TargetID;
            para[2].Value = model.Type;
            para[3].Value = model.Description;
            para[4].Value = model.AlbumUserId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update SNS_UserAlbums set ");
            builder2.Append("PhotoCount=PhotoCount+1 ");
            builder2.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.AlbumID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public Maticsoft.Model.SNS.UserAlbumDetail DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserAlbumDetail detail = new Maticsoft.Model.SNS.UserAlbumDetail();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    detail.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["AlbumID"] != null) && (row["AlbumID"].ToString() != ""))
                {
                    detail.AlbumID = int.Parse(row["AlbumID"].ToString());
                }
                if ((row["TargetID"] != null) && (row["TargetID"].ToString() != ""))
                {
                    detail.TargetID = int.Parse(row["TargetID"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    detail.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Description"] != null)
                {
                    detail.Description = row["Description"].ToString();
                }
            }
            return detail;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbumDetail ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int AlbumID, int TargetID, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbumDetail ");
            builder.Append(" where AlbumID=@AlbumID and TargetID=@TargetID and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            cmdParms[1].Value = TargetID;
            cmdParms[2].Value = Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int AlbumID, int TargetId, int Type)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbumDetail ");
            builder.Append(" where TargetID=@TargetID and Type=@Type and AlbumID=@AlbumID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            para[0].Value = TargetId;
            para[1].Value = Type;
            para[2].Value = AlbumID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update SNS_UserAlbums set PhotoCount=PhotoCount-1");
            builder2.Append(" where AlbumID=@AlbumID ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = TargetId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbumDetail ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int AlbumID, int TargetID, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserAlbumDetail");
            builder.Append(" where AlbumID=@AlbumID and TargetID=@TargetID and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            cmdParms[1].Value = TargetID;
            cmdParms[2].Value = Type;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAlbumImgListByPage(int albumID, string orderby, int startIndex, int endIndex, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from ");
            switch (type)
            {
                case 0:
                    builder.Append(" (select uad.ID,p.PhotoID TargetID,p.PhotoName TargetName,p.Description Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,0 price,0 Type ");
                    builder.AppendFormat("  FROM      SNS_UserAlbumDetail uad   INNER JOIN SNS_Photos p ON uad.TargetID = p.PhotoID AND uad.Type = 0  AND uad.AlbumID = {0}", albumID);
                    break;

                case 1:
                    builder.Append(" (select uad.ID,p.ProductID TargetID,p.ProductName TargetName,p.ShareDescription Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,p.Price,1 Type");
                    builder.AppendFormat(" FROM   SNS_UserAlbumDetail uad  INNER JOIN SNS_Products p ON uad.TargetID = p.ProductID AND uad.Type = 1 AND uad.AlbumID = {0}", albumID);
                    break;

                default:
                    builder.Append(" (select uad.ID,p.ProductID TargetID,p.ProductName TargetName,p.ShareDescription Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,p.Price,1 Type");
                    builder.AppendFormat(" FROM   SNS_UserAlbumDetail uad  INNER JOIN SNS_Products p ON uad.TargetID = p.ProductID AND uad.Type = 1 AND uad.AlbumID = {0}", albumID);
                    builder.Append(" union");
                    builder.Append(" select uad.ID,p.PhotoID TargetID,p.PhotoName TargetName,p.Description Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,0 price,0 Type ");
                    builder.AppendFormat("  FROM      SNS_UserAlbumDetail uad   INNER JOIN SNS_Photos p ON uad.TargetID = p.PhotoID AND uad.Type = 0  AND uad.AlbumID = {0}", albumID);
                    break;
            }
            builder.Append("  )T ");
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,AlbumID,TargetID,Type,Description ");
            builder.Append(" FROM SNS_UserAlbumDetail ");
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
            builder.Append(" ID,AlbumID,TargetID,Type,Description ");
            builder.Append(" FROM SNS_UserAlbumDetail ");
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
            builder.Append(")AS Row, T.*  from SNS_UserAlbumDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.UserAlbumDetail GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,AlbumID,TargetID,Type,Description from SNS_UserAlbumDetail ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.UserAlbumDetail();
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
            builder.Append("select count(1) FROM SNS_UserAlbumDetail ");
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

        public int GetRecordCount4AlbumImgByAlbumID(int albumID, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select count(1) FROM SNS_UserAlbumDetail  where  AlbumID={0}", albumID);
            if (type != -1)
            {
                builder.AppendFormat(" and type={0}", type);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public List<string> GetThumbImageByAlbum(int AlbumID, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT TOP 9 A.ThumbImageUrl FROM    ( ");
            switch (type)
            {
                case 0:
                    builder.AppendFormat(" SELECT TOP 9 p.ThumbImageUrl ,  p.PhotoID ID  FROM      SNS_UserAlbumDetail d ,SNS_Photos p WHERE     d.Type = 0 AND d.AlbumID = {0} AND d.TargetID = p.PhotoID", AlbumID);
                    break;

                case 1:
                    builder.AppendFormat(" SELECT TOP 9 p.ThumbImageUrl , p.ProductID ID FROM      SNS_UserAlbumDetail d ,SNS_Products p WHERE     d.Type = 1 AND d.AlbumID = {0} AND d.TargetID = p.ProductID", AlbumID);
                    break;

                default:
                    builder.AppendFormat(" SELECT TOP 9 p.ThumbImageUrl ,  p.PhotoID ID  FROM      SNS_UserAlbumDetail d ,SNS_Photos p WHERE     d.Type = 0 AND d.AlbumID = {0} AND d.TargetID = p.PhotoID", AlbumID);
                    builder.Append(" UNION ");
                    builder.AppendFormat(" SELECT TOP 9 p.ThumbImageUrl , p.ProductID ID FROM      SNS_UserAlbumDetail d ,SNS_Products p WHERE     d.Type = 1 AND d.AlbumID = {0} AND d.TargetID = p.ProductID", AlbumID);
                    break;
            }
            builder.Append("        ) A ORDER BY A.ID DESC  ");
            DataSet set = DbHelperSQL.Query(builder.ToString());
            List<string> list = new List<string>();
            if ((set.Tables.Count > 0) && (set.Tables[0] != null))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if (row != null)
                    {
                        list.Add(row["ThumbImageUrl"].ToString().Replace("300x300", "60x60"));
                    }
                }
            }
            return list;
        }

        public bool Update(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbumDetail set ");
            builder.Append("Description=@Description");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Description;
            cmdParms[1].Value = model.ID;
            cmdParms[2].Value = model.AlbumID;
            cmdParms[3].Value = model.TargetID;
            cmdParms[4].Value = model.Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

