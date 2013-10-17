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

    public class Comments : IComments
    {
        public DataSet AblumComment(int ablumId, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM( ");
            builder.Append("SELECT C.CommentID,C.CreatedNickName,C.CreatedDate,c.Description,C.UserIP ,C.Type,C.TargetId,PP.ThumbImageUrl,AlbumID FROM SNS_Comments C, ");
            builder.Append("(SELECT P.PhotoID, U.CreatedUserID,P.ThumbImageUrl,U.AlbumID FROM SNS_Photos P , ");
            builder.Append("(SELECT TargetID FROM SNS_UserAlbumDetail WHERE AlbumID = @AlbumID AND Type = 0 ) UAD , ");
            builder.Append("(SELECT CreatedUserID,AlbumID FROM SNS_UserAlbums WHERE AlbumID = @AlbumID ) U ");
            builder.Append("WHERE P.PhotoID = UAD.TargetID AND P.CreatedUserID = U.CreatedUserID)PP ");
            builder.Append("WHERE C.TargetId=PP.PhotoID  AND C.Type=3");
            builder.Append("UNION ALL ");
            builder.Append("SELECT C.CommentID,C.CreatedNickName,C.CreatedDate,c.Description,C.UserIP,C.Type,C.TargetId,PP.ThumbImageUrl,AlbumID FROM SNS_Comments C, ");
            builder.Append("(SELECT P.ProductID , U.CreatedUserID,P.ThumbImageUrl,U.AlbumID FROM SNS_Products P , ");
            builder.Append("(SELECT TargetID FROM SNS_UserAlbumDetail WHERE AlbumID = @AlbumID AND Type = 1 ) UAD , ");
            builder.Append("(SELECT CreatedUserID,AlbumID FROM SNS_UserAlbums WHERE AlbumID =@AlbumID ) U ");
            builder.Append("WHERE P.ProductID = UAD.TargetID AND P.CreateUserID = U.CreatedUserID)PP ");
            builder.Append("WHERE C.TargetId=PP.ProductID AND C.Type=3)A ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.AppendFormat("WHERE A.CreatedNickName LIKE '%{0}%'", strWhere);
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ablumId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public int Add(Maticsoft.Model.SNS.Comments model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Comments(");
            builder.Append("Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@Type,@TargetId,@ParentID,@CreatedUserID,@CreatedNickName,@HasReferUser,@Description,@IsRead,@Status,@ReplyCount,@UserIP,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@HasReferUser", SqlDbType.Bit, 1), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Type;
            cmdParms[1].Value = model.TargetId;
            cmdParms[2].Value = model.ParentID;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedNickName;
            cmdParms[5].Value = model.HasReferUser;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.IsRead;
            cmdParms[8].Value = model.Status;
            cmdParms[9].Value = model.ReplyCount;
            cmdParms[10].Value = model.UserIP;
            cmdParms[11].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int AddEx(Maticsoft.Model.SNS.Comments model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Comments(");
            builder.Append("Status,ReplyCount,UserIP,CreatedDate,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead)");
            builder.Append(" values (");
            builder.Append("@Status,@ReplyCount,@UserIP,@CreatedDate,@Type,@TargetId,@ParentID,@CreatedUserID,@CreatedNickName,@HasReferUser,@Description,@IsRead)");
            builder.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@HasReferUser", SqlDbType.Bit, 1), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@ReturnValue", SqlDbType.Int) };
            para[0].Value = model.Status;
            para[1].Value = model.ReplyCount;
            para[2].Value = model.UserIP;
            para[3].Value = model.CreatedDate;
            para[4].Value = model.Type;
            para[5].Value = model.TargetId;
            para[6].Value = model.ParentID;
            para[7].Value = model.CreatedUserID;
            para[8].Value = model.CreatedNickName;
            para[9].Value = model.HasReferUser;
            para[10].Value = model.Description;
            para[11].Value = model.IsRead;
            para[12].Direction = ParameterDirection.Output;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            if ((model.Type == 1) || (model.Type == 2))
            {
                StringBuilder builder2 = new StringBuilder();
                if (model.Type == 1)
                {
                    builder2.Append("Update  SNS_Photos ");
                }
                else
                {
                    builder2.Append("Update  SNS_Products ");
                }
                builder2.Append(" Set CommentCount=CommentCount+1 ");
                if (model.Type == 1)
                {
                    builder2.Append(" where PhotoID=@TargetId");
                }
                else
                {
                    builder2.Append(" where ProductID=@TargetId");
                }
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
                parameterArray2[0].Value = model.TargetId;
                CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
                cmdList.Add(info2);
                StringBuilder builder3 = new StringBuilder();
                builder3.Append("Update  SNS_Posts ");
                builder3.Append(" Set CommentCount=CommentCount+1 ");
                builder3.Append(" where TargetId=@TargetId and Type=@Type");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
                parameterArray3[0].Value = model.TargetId;
                parameterArray3[1].Value = model.Type;
                CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
                cmdList.Add(info3);
            }
            else
            {
                StringBuilder builder4 = new StringBuilder();
                builder4.Append("Update  SNS_Posts ");
                builder4.Append(" Set CommentCount=CommentCount+1 ");
                builder4.Append(" where PostID=@TargetId and Type=@Type");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
                parameterArray4[0].Value = model.TargetId;
                parameterArray4[1].Value = model.Type;
                CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
                cmdList.Add(info4);
            }
            DbHelperSQL.ExecuteSqlTran(cmdList);
            return (int) para[12].Value;
        }

        public Maticsoft.Model.SNS.Comments DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Comments comments = new Maticsoft.Model.SNS.Comments();
            if (row != null)
            {
                if ((row["CommentID"] != null) && (row["CommentID"].ToString() != ""))
                {
                    comments.CommentID = int.Parse(row["CommentID"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    comments.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["TargetId"] != null) && (row["TargetId"].ToString() != ""))
                {
                    comments.TargetId = int.Parse(row["TargetId"].ToString());
                }
                if ((row["ParentID"] != null) && (row["ParentID"].ToString() != ""))
                {
                    comments.ParentID = new int?(int.Parse(row["ParentID"].ToString()));
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    comments.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    comments.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["HasReferUser"] != null) && (row["HasReferUser"].ToString() != ""))
                {
                    if ((row["HasReferUser"].ToString() == "1") || (row["HasReferUser"].ToString().ToLower() == "true"))
                    {
                        comments.HasReferUser = true;
                    }
                    else
                    {
                        comments.HasReferUser = false;
                    }
                }
                if (row["Description"] != null)
                {
                    comments.Description = row["Description"].ToString();
                }
                if ((row["IsRead"] != null) && (row["IsRead"].ToString() != ""))
                {
                    if ((row["IsRead"].ToString() == "1") || (row["IsRead"].ToString().ToLower() == "true"))
                    {
                        comments.IsRead = true;
                    }
                    else
                    {
                        comments.IsRead = false;
                    }
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    comments.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["ReplyCount"] != null) && (row["ReplyCount"].ToString() != ""))
                {
                    comments.ReplyCount = int.Parse(row["ReplyCount"].ToString());
                }
                if (row["UserIP"] != null)
                {
                    comments.UserIP = row["UserIP"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    comments.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return comments;
        }

        public bool Delete(int CommentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Comments ");
            builder.Append(" where CommentID=@CommentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CommentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CommentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int TargetId, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Comments ");
            builder.Append(" where TargetId=@TargetId  AND Type=@Type");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = TargetId;
            cmdParms[1].Value = Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteComment(int ablumId, int commentId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM SNS_Comments  ");
            builder.Append(" WHERE CommentID=@CommentID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@CommentID", SqlDbType.Int, 4) };
            para[0].Value = commentId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("UPDATE SNS_UserAlbums SET CommentsCount=(CASE WHEN CommentsCount > 0 THEN CommentsCount-1 ELSE 0 END ) ");
            builder2.Append(" WHERE AlbumID=@AlbumID ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = ablumId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool DeleteList(string CommentIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Comments ");
            builder.Append(" where CommentID in (" + CommentIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteListEx(string CommentIDlist)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TargetIds ", SqlDbType.NVarChar), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = CommentIDlist;
            DbHelperSQL.RunProcedure("sp_SNS_CommentDeleteAction", parameters, out num);
            return (num > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CommentID,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate ");
            builder.Append(" FROM SNS_Comments ");
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
            builder.Append(" CommentID,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate ");
            builder.Append(" FROM SNS_Comments ");
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
                builder.Append("order by T.CommentID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Comments T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Comments GetModel(int CommentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CommentID,Type,TargetId,ParentID,CreatedUserID,CreatedNickName,HasReferUser,Description,IsRead,Status,ReplyCount,UserIP,CreatedDate from SNS_Comments ");
            builder.Append(" where CommentID=@CommentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CommentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CommentID;
            new Maticsoft.Model.SNS.Comments();
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
            builder.Append("select count(1) FROM SNS_Comments ");
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

        public bool Update(Maticsoft.Model.SNS.Comments model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Comments set ");
            builder.Append("Type=@Type,");
            builder.Append("TargetId=@TargetId,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("HasReferUser=@HasReferUser,");
            builder.Append("Description=@Description,");
            builder.Append("IsRead=@IsRead,");
            builder.Append("Status=@Status,");
            builder.Append("ReplyCount=@ReplyCount,");
            builder.Append("UserIP=@UserIP,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where CommentID=@CommentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@HasReferUser", SqlDbType.Bit, 1), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CommentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Type;
            cmdParms[1].Value = model.TargetId;
            cmdParms[2].Value = model.ParentID;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedNickName;
            cmdParms[5].Value = model.HasReferUser;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.IsRead;
            cmdParms[8].Value = model.Status;
            cmdParms[9].Value = model.ReplyCount;
            cmdParms[10].Value = model.UserIP;
            cmdParms[11].Value = model.CreatedDate;
            cmdParms[12].Value = model.CommentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

