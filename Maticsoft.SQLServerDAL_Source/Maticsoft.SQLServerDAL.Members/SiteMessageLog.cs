namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SiteMessageLog : ISiteMessageLog
    {
        public int Add(Maticsoft.Model.Members.SiteMessageLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_SiteMessageLog(");
            builder.Append("MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName)");
            builder.Append(" values (");
            builder.Append("@MessageID,@MessageType,@MessageState,@ReceiverID,@Ext1,@Ext2,@ReceiverUserName)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MessageID", SqlDbType.Int, 4), new SqlParameter("@MessageType", SqlDbType.NVarChar, 50), new SqlParameter("@MessageState", SqlDbType.NVarChar, 50), new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@Ext1", SqlDbType.NVarChar, 300), new SqlParameter("@Ext2", SqlDbType.NVarChar, 300), new SqlParameter("@ReceiverUserName", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.MessageID;
            cmdParms[1].Value = model.MessageType;
            cmdParms[2].Value = model.MessageState;
            cmdParms[3].Value = model.ReceiverID;
            cmdParms[4].Value = model.Ext1;
            cmdParms[5].Value = model.Ext2;
            cmdParms[6].Value = model.ReceiverUserName;
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
            builder.Append("delete from SA_SiteMessageLog ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_SiteMessageLog ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_SiteMessageLog");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName ");
            builder.Append(" FROM SA_SiteMessageLog ");
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
            builder.Append(" ID,MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName ");
            builder.Append(" FROM SA_SiteMessageLog ");
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
            builder.Append(")AS Row, T.*  from SA_SiteMessageLog T ");
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
            return DbHelperSQL.GetMaxID("ID", "SA_SiteMessageLog");
        }

        public Maticsoft.Model.Members.SiteMessageLog GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,MessageID,MessageType,MessageState,ReceiverID,Ext1,Ext2,ReceiverUserName from SA_SiteMessageLog ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Model.Members.SiteMessageLog log = new Maticsoft.Model.Members.SiteMessageLog();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ID"] != null) && (set.Tables[0].Rows[0]["ID"].ToString() != ""))
            {
                log.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["MessageID"] != null) && (set.Tables[0].Rows[0]["MessageID"].ToString() != ""))
            {
                log.MessageID = new int?(int.Parse(set.Tables[0].Rows[0]["MessageID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["MessageType"] != null) && (set.Tables[0].Rows[0]["MessageType"].ToString() != ""))
            {
                log.MessageType = set.Tables[0].Rows[0]["MessageType"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MessageState"] != null) && (set.Tables[0].Rows[0]["MessageState"].ToString() != ""))
            {
                log.MessageState = set.Tables[0].Rows[0]["MessageState"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ReceiverID"] != null) && (set.Tables[0].Rows[0]["ReceiverID"].ToString() != ""))
            {
                log.ReceiverID = new int?(int.Parse(set.Tables[0].Rows[0]["ReceiverID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Ext1"] != null) && (set.Tables[0].Rows[0]["Ext1"].ToString() != ""))
            {
                log.Ext1 = set.Tables[0].Rows[0]["Ext1"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Ext2"] != null) && (set.Tables[0].Rows[0]["Ext2"].ToString() != ""))
            {
                log.Ext2 = set.Tables[0].Rows[0]["Ext2"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ReceiverUserName"] != null) && (set.Tables[0].Rows[0]["ReceiverUserName"].ToString() != ""))
            {
                log.ReceiverUserName = set.Tables[0].Rows[0]["ReceiverUserName"].ToString();
            }
            return log;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SA_SiteMessageLog ");
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

        public bool Update(Maticsoft.Model.Members.SiteMessageLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_SiteMessageLog set ");
            builder.Append("MessageID=@MessageID,");
            builder.Append("MessageType=@MessageType,");
            builder.Append("MessageState=@MessageState,");
            builder.Append("ReceiverID=@ReceiverID,");
            builder.Append("Ext1=@Ext1,");
            builder.Append("Ext2=@Ext2,");
            builder.Append("ReceiverUserName=@ReceiverUserName");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MessageID", SqlDbType.Int, 4), new SqlParameter("@MessageType", SqlDbType.NVarChar, 50), new SqlParameter("@MessageState", SqlDbType.NVarChar, 50), new SqlParameter("@ReceiverID", SqlDbType.Int, 4), new SqlParameter("@Ext1", SqlDbType.NVarChar, 300), new SqlParameter("@Ext2", SqlDbType.NVarChar, 300), new SqlParameter("@ReceiverUserName", SqlDbType.NVarChar, 100), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.MessageID;
            cmdParms[1].Value = model.MessageType;
            cmdParms[2].Value = model.MessageState;
            cmdParms[3].Value = model.ReceiverID;
            cmdParms[4].Value = model.Ext1;
            cmdParms[5].Value = model.Ext2;
            cmdParms[6].Value = model.ReceiverUserName;
            cmdParms[7].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

