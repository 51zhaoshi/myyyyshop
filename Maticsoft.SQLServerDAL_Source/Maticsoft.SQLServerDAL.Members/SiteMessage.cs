namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SiteMessage : ISiteMessage
    {
        public int Add(Maticsoft.Model.Members.SiteMessage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into SA_SiteMessage(");
            builder.Append(" SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2)");
            builder.Append("  values (");
            builder.Append(" @SenderID,@ReceiverID,@Title,@Content,@MsgType,@SendTime,@ReadTime,@ReceiverIsRead,@SenderIsDel,@ReaderIsDel,@Ext1,@Ext2)");
            builder.Append(" ;select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4), new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@Title", SqlDbType.NVarChar, 300), new SqlParameter("@Content", SqlDbType.NVarChar), new SqlParameter("@MsgType", SqlDbType.NVarChar, 50), new SqlParameter("@SendTime", SqlDbType.DateTime), new SqlParameter("@ReadTime", SqlDbType.DateTime), new SqlParameter("@ReceiverIsRead", SqlDbType.Bit, 1), new SqlParameter("@SenderIsDel", SqlDbType.Bit, 1), new SqlParameter("@ReaderIsDel", SqlDbType.Bit, 1), new SqlParameter("@Ext1", SqlDbType.NVarChar, 300), new SqlParameter("@Ext2", SqlDbType.NVarChar, 300) };
            cmdParms[0].Value = model.SenderID;
            cmdParms[1].Value = model.ReceiverID;
            cmdParms[2].Value = model.Title;
            cmdParms[3].Value = model.Content;
            cmdParms[4].Value = model.MsgType;
            cmdParms[5].Value = model.SendTime;
            cmdParms[6].Value = model.ReadTime;
            cmdParms[7].Value = model.ReceiverIsRead;
            cmdParms[8].Value = model.SenderIsDel;
            cmdParms[9].Value = model.ReaderIsDel;
            cmdParms[10].Value = model.Ext1;
            cmdParms[11].Value = model.Ext2;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from SA_SiteMessage ");
            builder.Append("  where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from SA_SiteMessage ");
            builder.Append("  where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select count(1) from SA_SiteMessage");
            builder.Append("  where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAdminSendList(int AdminID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as ReceiverUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderIsDel='False' and SenderID=@SenderID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdminID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetAdminSendList(int AdminID, string KeyWord)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as ReceiverUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderIsDel='False' and SenderID=@SenderID");
            if (!string.IsNullOrEmpty(KeyWord))
            {
                builder.Append(" and Content like '%" + KeyWord + "%' ");
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdminID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetAdminSendListByPage(int AdminID, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID, SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as ReceiverUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderIsDel='False' and SenderID=@SenderID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdminID;
            cmdParms[1].Value = StartIndex;
            cmdParms[2].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public int GetAdminSendMsgCount(int AdminID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select COUNT(1) from SA_SiteMessage where SenderIsDel='False' and SenderID=@SenderID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdminID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetAllReceiveMsgCount(int RecevieID)
        {
            string countSql = "SELECT COUNT(1) FROM SA_SiteMessage WHERE  ReceiverID =@ReceiverID AND  ReaderIsDel = 'False' ";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4) };
            parameter[0].Value = RecevieID;
            return this.GetCountSql(countSql, parameter);
        }

        public int GetAllReceiveMsgCount(int RecevieID, int AdminID)
        {
            string countSql = "SELECT COUNT(1) FROM SA_SiteMessage WHERE  ReceiverID =@ReceiverID AND  ReaderIsDel = 'False' AND SenderID <>@SenderID";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@SenderID", SqlDbType.Int, 4) };
            parameter[0].Value = RecevieID;
            parameter[1].Value = AdminID;
            return this.GetCountSql(countSql, parameter);
        }

        public DataSet GetAllReceiveMsgList(int RecevierID, int AdminID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE  where ReceiverID=@RecevierID and ReaderIsDel='False'and SenderID<>@AdminID");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@SenderID", SqlDbType.Int, 4) };
            parameterArray[0].Value = RecevierID;
            parameterArray[1].Value = AdminID;
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetAllReceiveMsgListByPage(int RecevierID, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ReceiverID=@ReceiverID  and ReaderIsDel='False'");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = RecevierID;
            cmdParms[1].Value = StartIndex;
            cmdParms[2].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public DataSet GetAllReceiveMsgListByPage(int RecevierID, int AdminID, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ReceiverID=@ReceiverID  and ReaderIsDel='False'and SenderID<>@AdminId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = RecevierID;
            cmdParms[1].Value = AdminID;
            cmdParms[2].Value = StartIndex;
            cmdParms[3].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public DataSet GetAllSendMsgList(int SenderID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as  ReceiverUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderID=@SenderID and SenderIsDel='False'");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4) };
            parameterArray[0].Value = SenderID;
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetAllSendMsgListByPage(int SenderID, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as  ReceiverUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE SenderID=@SenderID and SenderIsDel='False'");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = SenderID;
            cmdParms[1].Value = StartIndex;
            cmdParms[2].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public int GetAllSystemMsgCount(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SA_SiteMessage where ");
            builder.Append(" ( ReceiverID = " + ReceiverID + " AND SenderID =@AdminId AND ReaderIsDel = 'False' and SendTime >= ( SELECT  User_dateCreate FROM  dbo.Accounts_Users WHERE UserID=@ReceiverID))");
            builder.Append(" Or");
            builder.Append(" ( ID NOT IN ( SELECT MessageID FROM SA_SiteMessageLog WHERE MessageState=1 AND ReceiverID =@ReceiverID AND MsgType =@UserType AND SenderID = @AdminId) AND MsgType =@UserType AND SenderID =@AdminId and SendTime >= (SELECT  User_dateCreate FROM  dbo.Accounts_Users WHERE UserID=@ReceiverID) )");
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            parameter[0].Value = ReceiverID;
            parameter[1].Value = AdminId;
            parameter[2].Value = UserType;
            return this.GetCountSql(builder.ToString(), parameter);
        }

        public DataSet GetAllSystemMsgList(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM SA_SiteMessage where ");
            builder.Append(" ( ReceiverID = " + ReceiverID + " AND SenderID =@AdminId  AND ReaderIsDel = 'False' and SendTime >= (SELECT  User_dateCreate FROM  dbo.Accounts_Users WHERE UserID=@ReceiverID))");
            builder.Append(" Or");
            builder.Append(" ( ID NOT IN ( SELECT MessageID FROM SA_SiteMessageLog WHERE MessageState=1 AND ReceiverID =@ReceiverID AND MsgType =@UserType AND SenderID =@AdminId) AND MsgType =@UserType AND SenderID = @AdminId  and SendTime >= (SELECT  User_dateCreate FROM  dbo.Accounts_Users WHERE UserID=@ReceiverID) ) ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = UserType;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetAllSystemMsgListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM SA_SiteMessage where ");
            builder.Append(" ( ReceiverID = " + ReceiverID + " AND SenderID =@AdminId AND ReaderIsDel = 'False' and SendTime >= (SELECT  User_dateCreate FROM  dbo.Accounts_Users WHERE UserID=@ReceiverID))");
            builder.Append(" Or");
            builder.Append(" ( ID NOT IN ( SELECT MessageID FROM SA_SiteMessageLog WHERE MessageState=1 AND ReceiverID =@ReceiverID AND MsgType =@UserType AND SenderID =@AdminId) AND MsgType =@UserType AND SenderID =@AdminId and SendTime >= (SELECT  User_dateCreate FROM  dbo.Accounts_Users WHERE UserID=@ReceiverID))");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = UserType;
            cmdParms[3].Value = StartIndex;
            cmdParms[4].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public int GetCountSql(string CountSql)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CountSql);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetCountSql(string CountSql, SqlParameter[] parameter)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CountSql);
            object single = DbHelperSQL.GetSingle(builder.ToString(), parameter);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2");
            builder.Append("  FROM SA_SiteMessage ");
            if (strWhere.Trim() != "")
            {
                builder.Append("  where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select ");
            if (Top > 0)
            {
                builder.Append("  top " + Top.ToString());
            }
            builder.Append("  ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,SenderUserName,ReceiverUserName ");
            builder.Append("  FROM SA_SiteMessage ");
            if (strWhere.Trim() != "")
            {
                builder.Append("  where " + strWhere);
            }
            builder.Append("  order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM ( ");
            builder.Append("  SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append(" order by T." + orderby);
            }
            else
            {
                builder.Append(" order by T.ID desc");
            }
            builder.Append(" )AS Row, T.*  from SA_SiteMessage T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append("  WHERE " + strWhere);
            }
            builder.Append("  ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetListToPageSQl(string Order, string OriginalSql)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("  SELECT * FROM (SELECT * , ROW_NUMBER() OVER (ORDER BY " + Order + ") AS row  FROM ( ");
            builder.Append(OriginalSql);
            builder.Append("  ) AS MatiTemp1 ) AS MatiTemp2 WHERE  row BETWEEN @StartIndex AND @EndIndex");
            return builder.ToString();
        }

        public string GetListToPageSQl(string Order, string OriginalSql, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("  SELECT * FROM (SELECT * , ROW_NUMBER() OVER (ORDER BY " + Order + ") AS row  FROM ( ");
            builder.Append(OriginalSql);
            builder.Append(string.Concat(new object[] { "  ) AS MatiTemp1 ) AS MatiTemp2 WHERE  row BETWEEN ", StartIndex, " AND ", EndIndex }));
            return builder.ToString();
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "SA_SiteMessage");
        }

        public Maticsoft.Model.Members.SiteMessage GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select  top 1 ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2 from SA_SiteMessage ");
            builder.Append("  where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Model.Members.SiteMessage message = new Maticsoft.Model.Members.SiteMessage();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ID"] != null) && (set.Tables[0].Rows[0]["ID"].ToString() != ""))
            {
                message.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["SenderID"] != null) && (set.Tables[0].Rows[0]["SenderID"].ToString() != ""))
            {
                message.SenderID = new int?(int.Parse(set.Tables[0].Rows[0]["SenderID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ReceiverID"] != null) && (set.Tables[0].Rows[0]["ReceiverID"].ToString() != ""))
            {
                message.ReceiverID = new int?(int.Parse(set.Tables[0].Rows[0]["ReceiverID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Title"] != null) && (set.Tables[0].Rows[0]["Title"].ToString() != ""))
            {
                message.Title = set.Tables[0].Rows[0]["Title"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Content"] != null) && (set.Tables[0].Rows[0]["Content"].ToString() != ""))
            {
                message.Content = set.Tables[0].Rows[0]["Content"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MsgType"] != null) && (set.Tables[0].Rows[0]["MsgType"].ToString() != ""))
            {
                message.MsgType = set.Tables[0].Rows[0]["MsgType"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SendTime"] != null) && (set.Tables[0].Rows[0]["SendTime"].ToString() != ""))
            {
                message.SendTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["SendTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ReadTime"] != null) && (set.Tables[0].Rows[0]["ReadTime"].ToString() != ""))
            {
                message.ReadTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["ReadTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ReceiverIsRead"] != null) && (set.Tables[0].Rows[0]["ReceiverIsRead"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["ReceiverIsRead"].ToString() == "1") || (set.Tables[0].Rows[0]["ReceiverIsRead"].ToString().ToLower() == "true"))
                {
                    message.ReceiverIsRead = true;
                }
                else
                {
                    message.ReceiverIsRead = false;
                }
            }
            if ((set.Tables[0].Rows[0]["SenderIsDel"] != null) && (set.Tables[0].Rows[0]["SenderIsDel"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["SenderIsDel"].ToString() == "1") || (set.Tables[0].Rows[0]["SenderIsDel"].ToString().ToLower() == "true"))
                {
                    message.SenderIsDel = true;
                }
                else
                {
                    message.SenderIsDel = false;
                }
            }
            if ((set.Tables[0].Rows[0]["ReaderIsDel"] != null) && (set.Tables[0].Rows[0]["ReaderIsDel"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["ReaderIsDel"].ToString() == "1") || (set.Tables[0].Rows[0]["ReaderIsDel"].ToString().ToLower() == "true"))
                {
                    message.ReaderIsDel = true;
                }
                else
                {
                    message.ReaderIsDel = false;
                }
            }
            if ((set.Tables[0].Rows[0]["Ext1"] != null) && (set.Tables[0].Rows[0]["Ext1"].ToString() != ""))
            {
                message.Ext1 = set.Tables[0].Rows[0]["Ext1"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Ext2"] != null) && (set.Tables[0].Rows[0]["Ext2"].ToString() != ""))
            {
                message.Ext2 = set.Tables[0].Rows[0]["Ext2"].ToString();
            }
            return message;
        }

        public DataSet GetReceiveMsgAlreadyReadList(int ReceiverID, int AdminId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM SA_SiteMessage where ");
            builder.Append("  ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='True'and SenderID<>@AdminId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetReceiveMsgAlreadyReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM SA_SiteMessage where ");
            builder.Append("  ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='True'and SenderID<>@AdminId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = StartIndex;
            cmdParms[3].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public int GetReceiveMsgAreadyReadCount(int ReceiverID, int AdminId)
        {
            string countSql = "select count(1) from SA_SiteMessage where ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='True'and SenderID<>@AdminId";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4) };
            parameter[0].Value = ReceiverID;
            parameter[1].Value = AdminId;
            return this.GetCountSql(countSql, parameter);
        }

        public int GetReceiveMsgNotReadCount(int ReceiverID, int AdminId)
        {
            string countSql = "select count(1) from SA_SiteMessage where ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='False'and SenderID<>@AdminId";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4) };
            parameter[0].Value = ReceiverID;
            parameter[1].Value = AdminId;
            return this.GetCountSql(countSql, parameter);
        }

        public DataSet GetReceiveMsgNotReadList(int ReceiverID, int AdminId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ");
            builder.Append("  ReceiverID=@ReceiverID  and ReaderIsDel='False'and ReceiverIsRead='False'and SenderID<>@AdminId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetReceiveMsgNotReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
            builder.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ");
            builder.Append("  ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='False'and SenderID<>@AdminId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = StartIndex;
            cmdParms[3].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select count(1) FROM SA_SiteMessage ");
            if (strWhere.Trim() != "")
            {
                builder.Append("  where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetSendMsgCount(int SenderID)
        {
            string countSql = "select count(1) from SA_SiteMessage where SenderID=@SenderID and SenderIsDel='False'";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4) };
            parameter[0].Value = SenderID;
            return this.GetCountSql(countSql, parameter);
        }

        public int GetSystemMsgAlreadyReadCount(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select count(1) from SA_SiteMessage where");
            builder.Append(" (ReceiverID =@ReceiverID AND SenderID =@AdminId  AND ReceiverIsRead = 'True' AND ReaderIsDel='False')");
            builder.Append(" Or");
            builder.Append(" (ID  IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID and MessageState='0' AND MsgType=@UserType))");
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            parameter[0].Value = ReceiverID;
            parameter[1].Value = AdminId;
            parameter[2].Value = UserType;
            return this.GetCountSql(builder.ToString(), parameter);
        }

        public DataSet GetSystemMsgAlreadyReadList(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from SA_SiteMessage where");
            builder.Append(" (ReceiverID = @ReceiverID AND SenderID =@AdminId  AND ReceiverIsRead = 'True' AND ReaderIsDel='False')");
            builder.Append(" Or");
            builder.Append(" (ID  IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID and MessageState='0' AND MsgType=@UserType))");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = UserType;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetSystemMsgAlreadyReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from SA_SiteMessage where");
            builder.Append(" (ReceiverID = @ReceiverID AND SenderID =@AdminId AND ReceiverIsRead = 'True' AND ReaderIsDel='False')");
            builder.Append(" Or");
            builder.Append(" (ID  IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID = " + ReceiverID + " and MessageState='0' AND MsgType=@UserType))");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = UserType;
            cmdParms[3].Value = StartIndex;
            cmdParms[4].Value = EndIndex;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public int GetSystemMsgNotReadCount(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select count(1) from SA_SiteMessage where MsgType=@UserType  ");
            builder.Append("and ((ReceiverID = @ReceiverID AND SenderID = @AdminId AND ReceiverIsRead = 'False' AND ReaderIsDel='False')");
            builder.Append(" Or");
            builder.Append(" (ID Not IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID AND MsgType=@UserType)))");
            builder.Append(" AND SendTime> (SELECT User_dateCreate FROM Accounts_Users WHERE UserID=@ReceiverID)");
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            parameter[0].Value = ReceiverID;
            parameter[1].Value = AdminId;
            parameter[2].Value = UserType;
            return this.GetCountSql(builder.ToString(), parameter);
        }

        public DataSet GetSystemMsgNotReadList(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from SA_SiteMessage where MsgType=@UserType and ");
            builder.Append(" ((ReceiverID =@ReceiverID AND SenderID =@AdminId AND ReceiverIsRead = 'False' AND ReaderIsDel='False')");
            builder.Append(" Or");
            builder.Append(" (ID Not IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID AND MsgType=@UserType)))");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = UserType;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetSystemMsgNotReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from SA_SiteMessage where MsgType=@UserType and ");
            builder.Append(" ((ReceiverID =@ReceiverID AND SenderID =@AdminId  AND ReceiverIsRead = 'False' AND ReaderIsDel='False')");
            builder.Append(" Or");
            builder.Append(" (ID Not IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID AND MsgType=@UserType)))");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.NVarChar), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReceiverID;
            cmdParms[1].Value = AdminId;
            cmdParms[2].Value = UserType;
            cmdParms[3].Value = StartIndex;
            cmdParms[4].Value = EndIndex;
            return DbHelperSQL.Query(this.GetListToPageSQl("SendTime Desc", builder.ToString()), cmdParms);
        }

        public int SetAdminMsgToDelById(int ID, int AdminID)
        {
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@AdminId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            cmdParms[1].Value = AdminID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage set SenderIsDel='True' where SenderID=@AdminID and ID=@ID", cmdParms);
        }

        public int SetReceiveMsgAlreadyRead(int ID)
        {
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage  set ReceiverIsRead='True' where ID=@ID", cmdParms);
        }

        public int SetReceiveMsgToDelById(int ID)
        {
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage  set  ReaderIsDel='True' where ID=@ID", cmdParms);
        }

        public int SetSendMsgToDelById(int ID)
        {
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage set SenderIsDel='True' where ID=@ID", cmdParms);
        }

        public int SetSystemMsgStateToAlreadyRead(int ID, int ReceiverID, string UserType)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int), new SqlParameter("@ReceiverID", SqlDbType.Int), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            parameters[0].Value = ID;
            parameters[1].Value = ReceiverID;
            parameters[2].Value = UserType;
            return DbHelperSQL.RunProcedure("Sp_MsgBox_SetSystemMsgStateToAlreadyRead", parameters, out num);
        }

        public int SetSystemMsgStateToDel(int ID, int ReceiverID, string UserType)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int), new SqlParameter("@ReceiverID", SqlDbType.Int), new SqlParameter("@UserType", SqlDbType.NVarChar) };
            parameters[0].Value = ID;
            parameters[1].Value = ReceiverID;
            parameters[2].Value = UserType;
            return DbHelperSQL.RunProcedure("Sp_MsgBox_SetSystemMsgStateToDel", parameters, out num);
        }

        public bool Update(Maticsoft.Model.Members.SiteMessage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update SA_SiteMessage set ");
            builder.Append(" SenderID=@SenderID,");
            builder.Append(" ReceiverID=@ReceiverID,");
            builder.Append(" Title=@Title,");
            builder.Append(" Content=@Content,");
            builder.Append(" MsgType=@MsgType,");
            builder.Append(" SendTime=@SendTime,");
            builder.Append(" ReadTime=@ReadTime,");
            builder.Append(" ReceiverIsRead=@ReceiverIsRead,");
            builder.Append(" SenderIsDel=@SenderIsDel,");
            builder.Append(" ReaderIsDel=@ReaderIsDel,");
            builder.Append(" Ext1=@Ext1,");
            builder.Append(" Ext2=@Ext2,");
            builder.Append("  where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SenderID", SqlDbType.Int, 4), new SqlParameter("@ReceiverID", SqlDbType.NChar, 10), new SqlParameter("@Title", SqlDbType.NVarChar, 300), new SqlParameter("@Content", SqlDbType.NVarChar), new SqlParameter("@MsgType", SqlDbType.NVarChar, 50), new SqlParameter("@SendTime", SqlDbType.DateTime), new SqlParameter("@ReadTime", SqlDbType.DateTime), new SqlParameter("@ReceiverIsRead", SqlDbType.Bit, 1), new SqlParameter("@SenderIsDel", SqlDbType.Bit, 1), new SqlParameter("@ReaderIsDel", SqlDbType.Bit, 1), new SqlParameter("@Ext1", SqlDbType.NVarChar, 300), new SqlParameter("@Ext2", SqlDbType.NVarChar, 300), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.SenderID;
            cmdParms[1].Value = model.ReceiverID;
            cmdParms[2].Value = model.Title;
            cmdParms[3].Value = model.Content;
            cmdParms[4].Value = model.MsgType;
            cmdParms[5].Value = model.SendTime;
            cmdParms[6].Value = model.ReadTime;
            cmdParms[7].Value = model.ReceiverIsRead;
            cmdParms[8].Value = model.SenderIsDel;
            cmdParms[9].Value = model.ReaderIsDel;
            cmdParms[10].Value = model.Ext1;
            cmdParms[11].Value = model.Ext2;
            cmdParms[12].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

