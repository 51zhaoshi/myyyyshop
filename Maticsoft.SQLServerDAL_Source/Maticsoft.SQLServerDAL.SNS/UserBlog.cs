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

    public class UserBlog : IUserBlog
    {
        public int Add(Maticsoft.Model.SNS.UserBlog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserBlog(");
            builder.Append("Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@Title,@Summary,@Description,@UserID,@UserName,@LinkUrl,@Status,@Keywords,@Recomend,@Attachment,@Remark,@PvCount,@TotalComment,@TotalFav,@TotalShare,@Meta_Title,@Meta_Description,@Meta_Keywords,@SeoUrl,@StaticUrl,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Summary", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@LinkUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@Recomend", SqlDbType.Int, 4), new SqlParameter("@Attachment", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@TotalComment", SqlDbType.Int, 4), new SqlParameter("@TotalFav", SqlDbType.Int, 4), new SqlParameter("@TotalShare", SqlDbType.Int, 4), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 100), 
                new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 500), new SqlParameter("@CreatedDate", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.Summary;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.UserID;
            cmdParms[4].Value = model.UserName;
            cmdParms[5].Value = model.LinkUrl;
            cmdParms[6].Value = model.Status;
            cmdParms[7].Value = model.Keywords;
            cmdParms[8].Value = model.Recomend;
            cmdParms[9].Value = model.Attachment;
            cmdParms[10].Value = model.Remark;
            cmdParms[11].Value = model.PvCount;
            cmdParms[12].Value = model.TotalComment;
            cmdParms[13].Value = model.TotalFav;
            cmdParms[14].Value = model.TotalShare;
            cmdParms[15].Value = model.Meta_Title;
            cmdParms[0x10].Value = model.Meta_Description;
            cmdParms[0x11].Value = model.Meta_Keywords;
            cmdParms[0x12].Value = model.SeoUrl;
            cmdParms[0x13].Value = model.StaticUrl;
            cmdParms[20].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.UserBlog DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserBlog blog = new Maticsoft.Model.SNS.UserBlog();
            if (row != null)
            {
                if ((row["BlogID"] != null) && (row["BlogID"].ToString() != ""))
                {
                    blog.BlogID = int.Parse(row["BlogID"].ToString());
                }
                if (row["Title"] != null)
                {
                    blog.Title = row["Title"].ToString();
                }
                if (row["Summary"] != null)
                {
                    blog.Summary = row["Summary"].ToString();
                }
                if (row["Description"] != null)
                {
                    blog.Description = row["Description"].ToString();
                }
                if ((row["UserID"] != null) && (row["UserID"].ToString() != ""))
                {
                    blog.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["UserName"] != null)
                {
                    blog.UserName = row["UserName"].ToString();
                }
                if (row["LinkUrl"] != null)
                {
                    blog.LinkUrl = row["LinkUrl"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    blog.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Keywords"] != null)
                {
                    blog.Keywords = row["Keywords"].ToString();
                }
                if ((row["Recomend"] != null) && (row["Recomend"].ToString() != ""))
                {
                    blog.Recomend = int.Parse(row["Recomend"].ToString());
                }
                if (row["Attachment"] != null)
                {
                    blog.Attachment = row["Attachment"].ToString();
                }
                if (row["Remark"] != null)
                {
                    blog.Remark = row["Remark"].ToString();
                }
                if ((row["PvCount"] != null) && (row["PvCount"].ToString() != ""))
                {
                    blog.PvCount = int.Parse(row["PvCount"].ToString());
                }
                if ((row["TotalComment"] != null) && (row["TotalComment"].ToString() != ""))
                {
                    blog.TotalComment = int.Parse(row["TotalComment"].ToString());
                }
                if ((row["TotalFav"] != null) && (row["TotalFav"].ToString() != ""))
                {
                    blog.TotalFav = int.Parse(row["TotalFav"].ToString());
                }
                if ((row["TotalShare"] != null) && (row["TotalShare"].ToString() != ""))
                {
                    blog.TotalShare = int.Parse(row["TotalShare"].ToString());
                }
                if (row["Meta_Title"] != null)
                {
                    blog.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    blog.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    blog.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["SeoUrl"] != null)
                {
                    blog.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["StaticUrl"] != null)
                {
                    blog.StaticUrl = row["StaticUrl"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    blog.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return blog;
        }

        public bool Delete(int BlogID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserBlog ");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            cmdParms[0].Value = BlogID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int BlogID)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserBlog ");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            para[0].Value = BlogID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from SNS_Posts where type=4 and TargetId=@TargetId ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = BlogID;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from SNS_Comments where type=4 and TargetId=@TargetId  ");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray3[0].Value = BlogID;
            item = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool DeleteList(string BlogIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserBlog ");
            builder.Append(" where BlogID in (" + BlogIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int BlogID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserBlog");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            cmdParms[0].Value = BlogID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetActiveUser(int top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (top > 0)
            {
                builder.Append(" top " + top);
            }
            builder.Append(" UserID,UserName FROM dbo.SNS_UserBlog WHERE Status=1 GROUP BY UserID,UserName ORDER BY COUNT(UserID) DESC");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BlogID,Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate ");
            builder.Append(" FROM SNS_UserBlog ");
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
            builder.Append(" BlogID,Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate ");
            builder.Append(" FROM SNS_UserBlog ");
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
                builder.Append("order by T.BlogID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserBlog T ");
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
            return DbHelperSQL.GetMaxID("BlogID", "SNS_UserBlog");
        }

        public Maticsoft.Model.SNS.UserBlog GetModel(int BlogID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 BlogID,Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,StaticUrl,CreatedDate from SNS_UserBlog ");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            cmdParms[0].Value = BlogID;
            new Maticsoft.Model.SNS.UserBlog();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetPvCount(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PvCount from SNS_UserBlog ");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_UserBlog ");
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

        public bool Update(Maticsoft.Model.SNS.UserBlog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserBlog set ");
            builder.Append("Title=@Title,");
            builder.Append("Summary=@Summary,");
            builder.Append("Description=@Description,");
            builder.Append("UserID=@UserID,");
            builder.Append("UserName=@UserName,");
            builder.Append("LinkUrl=@LinkUrl,");
            builder.Append("Status=@Status,");
            builder.Append("Keywords=@Keywords,");
            builder.Append("Recomend=@Recomend,");
            builder.Append("Attachment=@Attachment,");
            builder.Append("Remark=@Remark,");
            builder.Append("PvCount=@PvCount,");
            builder.Append("TotalComment=@TotalComment,");
            builder.Append("TotalFav=@TotalFav,");
            builder.Append("TotalShare=@TotalShare,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("StaticUrl=@StaticUrl,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Summary", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@LinkUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@Recomend", SqlDbType.Int, 4), new SqlParameter("@Attachment", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@TotalComment", SqlDbType.Int, 4), new SqlParameter("@TotalFav", SqlDbType.Int, 4), new SqlParameter("@TotalShare", SqlDbType.Int, 4), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 100), 
                new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 500), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@BlogID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.Summary;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.UserID;
            cmdParms[4].Value = model.UserName;
            cmdParms[5].Value = model.LinkUrl;
            cmdParms[6].Value = model.Status;
            cmdParms[7].Value = model.Keywords;
            cmdParms[8].Value = model.Recomend;
            cmdParms[9].Value = model.Attachment;
            cmdParms[10].Value = model.Remark;
            cmdParms[11].Value = model.PvCount;
            cmdParms[12].Value = model.TotalComment;
            cmdParms[13].Value = model.TotalFav;
            cmdParms[14].Value = model.TotalShare;
            cmdParms[15].Value = model.Meta_Title;
            cmdParms[0x10].Value = model.Meta_Description;
            cmdParms[0x11].Value = model.Meta_Keywords;
            cmdParms[0x12].Value = model.SeoUrl;
            cmdParms[0x13].Value = model.StaticUrl;
            cmdParms[20].Value = model.CreatedDate;
            cmdParms[0x15].Value = model.BlogID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCommentCount(int id)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserBlog set ");
            builder.Append("TotalComment=TotalComment+1 ");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            para[0].Value = id;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update SNS_Posts Set CommentCount=CommentCount+1 where type=4 and  TargetId=@TargetId ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = id;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool UpdateFavCount(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserBlog set ");
            builder.Append("TotalFav=TotalFav+1 ");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdatePvCount(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserBlog set ");
            builder.Append("PvCount=PvCount+1 ");
            builder.Append(" where BlogID=@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRec(int id, int Rec)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserBlog set ");
            builder.Append("Recomend=@Recomend ");
            builder.Append(" where BlogID =@BlogID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Recomend", SqlDbType.Int, 4), new SqlParameter("@BlogID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Rec;
            cmdParms[1].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecList(string ids, int Rec)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserBlog set ");
            builder.Append("Recomend=@Recomend ");
            builder.Append(" where BlogID in (" + ids + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Recomend", SqlDbType.Int, 4) };
            cmdParms[0].Value = Rec;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatusList(string ids, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserBlog set ");
            builder.Append("Status=@Status ");
            builder.Append(" where BlogID in (" + ids + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = Status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

