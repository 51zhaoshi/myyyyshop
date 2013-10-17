namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Comment : IComment
    {
        public int Add(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_Comment(");
            builder.Append("ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName)");
            builder.Append(" values (");
            builder.Append("@ContentId,@Description,@CreatedDate,@CreatedUserID,@ReplyCount,@ParentID,@TypeID,@State,@IsRead,@CreatedNickName)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentId", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.SmallInt, 2), new SqlParameter("@State", SqlDbType.Bit, 1), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.ContentId;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.ReplyCount;
            cmdParms[5].Value = model.ParentID;
            cmdParms[6].Value = model.TypeID;
            cmdParms[7].Value = model.State;
            cmdParms[8].Value = model.IsRead;
            cmdParms[9].Value = model.CreatedNickName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int AddEx(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_Comment(");
            builder.Append("ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName)");
            builder.Append(" values (");
            builder.Append("@ContentId,@Description,@CreatedDate,@CreatedUserID,@ReplyCount,@ParentID,@TypeID,@State,@IsRead,@CreatedNickName)");
            builder.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ContentId", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.SmallInt, 2), new SqlParameter("@State", SqlDbType.Bit, 1), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 200), new SqlParameter("@ReturnValue", SqlDbType.Int) };
            para[0].Value = model.ContentId;
            para[1].Value = model.Description;
            para[2].Value = model.CreatedDate;
            para[3].Value = model.CreatedUserID;
            para[4].Value = model.ReplyCount;
            para[5].Value = model.ParentID;
            para[6].Value = model.TypeID;
            para[7].Value = model.State;
            para[8].Value = model.IsRead;
            para[9].Value = model.CreatedNickName;
            para[10].Direction = ParameterDirection.Output;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update  CMS_Content ");
            builder2.Append(" Set TotalComment=TotalComment+1 ");
            builder2.Append(" where ContentID=@ContentID ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.ContentId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            DbHelperSQL.ExecuteSqlTran(cmdList);
            return (int) para[10].Value;
        }

        public int AddTran(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_Comment(");
            builder.Append("ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName)");
            builder.Append(" values (");
            builder.Append("@ContentId,@Description,@CreatedDate,@CreatedUserID,@ReplyCount,@ParentID,@TypeID,@State,@IsRead,@CreatedNickName)");
            builder.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ContentId", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.SmallInt, 2), new SqlParameter("@State", SqlDbType.Bit, 1), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 200), new SqlParameter("@ReturnValue", SqlDbType.Int) };
            para[0].Value = model.ContentId;
            para[1].Value = model.Description;
            para[2].Value = model.CreatedDate;
            para[3].Value = model.CreatedUserID;
            para[4].Value = model.ReplyCount;
            para[5].Value = model.ParentID;
            para[6].Value = model.TypeID;
            para[7].Value = model.State;
            para[8].Value = model.IsRead;
            para[9].Value = model.CreatedNickName;
            para[10].Direction = ParameterDirection.Output;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            switch (model.TypeID)
            {
                case 2:
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("Update  CMS_Video ");
                    builder2.Append(" Set TotalComment=TotalComment+1 ");
                    builder2.Append(" where VideoID=@VideoID ");
                    SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@VideoID", SqlDbType.Int, 4) };
                    parameterArray2[0].Value = model.ContentId;
                    CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
                    cmdList.Add(info2);
                    break;
                }
                case 3:
                {
                    StringBuilder builder3 = new StringBuilder();
                    builder3.Append("Update  CMS_Content ");
                    builder3.Append(" Set TotalComment=TotalComment+1 ");
                    builder3.Append(" where ContentID=@ContentID ");
                    SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
                    parameterArray3[0].Value = model.ContentId;
                    CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
                    cmdList.Add(info3);
                    break;
                }
            }
            DbHelperSQL.ExecuteSqlTran(cmdList);
            return (int) para[10].Value;
        }

        public Maticsoft.Model.CMS.Comment DataRowToModel(DataRow row)
        {
            Maticsoft.Model.CMS.Comment comment = new Maticsoft.Model.CMS.Comment();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    comment.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["ContentId"] != null) && (row["ContentId"].ToString() != ""))
                {
                    comment.ContentId = new int?(int.Parse(row["ContentId"].ToString()));
                }
                if (row["Description"] != null)
                {
                    comment.Description = row["Description"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    comment.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    comment.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if ((row["ReplyCount"] != null) && (row["ReplyCount"].ToString() != ""))
                {
                    comment.ReplyCount = int.Parse(row["ReplyCount"].ToString());
                }
                if ((row["ParentID"] != null) && (row["ParentID"].ToString() != ""))
                {
                    comment.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if ((row["TypeID"] != null) && (row["TypeID"].ToString() != ""))
                {
                    comment.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if ((row["State"] != null) && (row["State"].ToString() != ""))
                {
                    if ((row["State"].ToString() == "1") || (row["State"].ToString().ToLower() == "true"))
                    {
                        comment.State = true;
                    }
                    else
                    {
                        comment.State = false;
                    }
                }
                if ((row["IsRead"] != null) && (row["IsRead"].ToString() != ""))
                {
                    if ((row["IsRead"].ToString() == "1") || (row["IsRead"].ToString().ToLower() == "true"))
                    {
                        comment.IsRead = true;
                    }
                    else
                    {
                        comment.IsRead = false;
                    }
                }
                if (row["CreatedNickName"] != null)
                {
                    comment.CreatedNickName = row["CreatedNickName"].ToString();
                }
            }
            return comment;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_Comment ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_Comment ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_Comment");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName ");
            builder.Append(" FROM CMS_Comment ");
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
            builder.Append(" ID,ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName ");
            builder.Append(" FROM CMS_Comment ");
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
            builder.Append(")AS Row, T.*  from CMS_Comment T ");
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
            builder.Append(" *,CMS_Content.Title ");
            builder.Append(" FROM CMS_Comment LEFT JOIN CMS_Content ON CMS_Comment.ContentId = CMS_Content.ContentID");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "CMS_Comment");
        }

        public Maticsoft.Model.CMS.Comment GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,ContentId,Description,CreatedDate,CreatedUserID,ReplyCount,ParentID,TypeID,State,IsRead,CreatedNickName from CMS_Comment ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.CMS.Comment();
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
            builder.Append("select count(1) FROM CMS_Comment ");
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

        public bool Update(Maticsoft.Model.CMS.Comment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Comment set ");
            builder.Append("ContentId=@ContentId,");
            builder.Append("Description=@Description,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("ReplyCount=@ReplyCount,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("TypeID=@TypeID,");
            builder.Append("State=@State,");
            builder.Append("IsRead=@IsRead,");
            builder.Append("CreatedNickName=@CreatedNickName");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentId", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.SmallInt, 2), new SqlParameter("@State", SqlDbType.Bit, 1), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 200), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ContentId;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.ReplyCount;
            cmdParms[5].Value = model.ParentID;
            cmdParms[6].Value = model.TypeID;
            cmdParms[7].Value = model.State;
            cmdParms[8].Value = model.IsRead;
            cmdParms[9].Value = model.CreatedNickName;
            cmdParms[10].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, int state)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Comment set ");
            builder.Append("State=@State");
            builder.Append(" where ID in (" + IDlist + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@State", SqlDbType.Bit, 1) };
            cmdParms[0].Value = state;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

