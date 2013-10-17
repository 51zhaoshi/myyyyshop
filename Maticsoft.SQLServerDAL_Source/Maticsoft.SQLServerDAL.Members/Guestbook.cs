namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Guestbook : IGuestbook
    {
        public int Add(Maticsoft.Model.Members.Guestbook model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_Guestbook(");
            builder.Append("CreateUserID,CreateNickName,ToUserID,ToNickName,CreatorUserIP,Title,Description,CreatedDate,CreatorEmail,CreatorRegion,CreatorCompany,CreatorPageSite,CreatorPhone,CreatorQQ,CreatorMsn,CreatorSex,HandlerNickName,HandlerUserID,HandlerDate,Privacy,ReplyCount,ReplyDescription,ParentID,Status)");
            builder.Append(" values (");
            builder.Append("@CreateUserID,@CreateNickName,@ToUserID,@ToNickName,@CreatorUserIP,@Title,@Description,@CreatedDate,@CreatorEmail,@CreatorRegion,@CreatorCompany,@CreatorPageSite,@CreatorPhone,@CreatorQQ,@CreatorMsn,@CreatorSex,@HandlerNickName,@HandlerUserID,@HandlerDate,@Privacy,@ReplyCount,@ReplyDescription,@ParentID,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreateNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ToUserID", SqlDbType.Int, 4), new SqlParameter("@ToNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorUserIP", SqlDbType.NVarChar, 20), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatorEmail", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorRegion", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorCompany", SqlDbType.NVarChar, 100), new SqlParameter("@CreatorPageSite", SqlDbType.NVarChar, 200), new SqlParameter("@CreatorPhone", SqlDbType.NVarChar, 20), new SqlParameter("@CreatorQQ", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorMsn", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorSex", SqlDbType.Bit, 1), 
                new SqlParameter("@HandlerNickName", SqlDbType.NVarChar, 50), new SqlParameter("@HandlerUserID", SqlDbType.Int, 4), new SqlParameter("@HandlerDate", SqlDbType.DateTime), new SqlParameter("@Privacy", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@ReplyDescription", SqlDbType.NVarChar, 500), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.CreateUserID;
            cmdParms[1].Value = model.CreateNickName;
            cmdParms[2].Value = model.ToUserID;
            cmdParms[3].Value = model.ToNickName;
            cmdParms[4].Value = model.CreatorUserIP;
            cmdParms[5].Value = model.Title;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.CreatorEmail;
            cmdParms[9].Value = model.CreatorRegion;
            cmdParms[10].Value = model.CreatorCompany;
            cmdParms[11].Value = model.CreatorPageSite;
            cmdParms[12].Value = model.CreatorPhone;
            cmdParms[13].Value = model.CreatorQQ;
            cmdParms[14].Value = model.CreatorMsn;
            cmdParms[15].Value = model.CreatorSex;
            cmdParms[0x10].Value = model.HandlerNickName;
            cmdParms[0x11].Value = model.HandlerUserID;
            cmdParms[0x12].Value = model.HandlerDate;
            cmdParms[0x13].Value = model.Privacy;
            cmdParms[20].Value = model.ReplyCount;
            cmdParms[0x15].Value = model.ReplyDescription;
            cmdParms[0x16].Value = model.ParentID;
            cmdParms[0x17].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Members.Guestbook DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.Guestbook guestbook = new Maticsoft.Model.Members.Guestbook();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    guestbook.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["CreateUserID"] != null) && (row["CreateUserID"].ToString() != ""))
                {
                    guestbook.CreateUserID = new int?(int.Parse(row["CreateUserID"].ToString()));
                }
                if ((row["CreateNickName"] != null) && (row["CreateNickName"].ToString() != ""))
                {
                    guestbook.CreateNickName = row["CreateNickName"].ToString();
                }
                if ((row["ToUserID"] != null) && (row["ToUserID"].ToString() != ""))
                {
                    guestbook.ToUserID = new int?(int.Parse(row["ToUserID"].ToString()));
                }
                if ((row["ToNickName"] != null) && (row["ToNickName"].ToString() != ""))
                {
                    guestbook.ToNickName = row["ToNickName"].ToString();
                }
                if ((row["CreatorUserIP"] != null) && (row["CreatorUserIP"].ToString() != ""))
                {
                    guestbook.CreatorUserIP = row["CreatorUserIP"].ToString();
                }
                if ((row["Title"] != null) && (row["Title"].ToString() != ""))
                {
                    guestbook.Title = row["Title"].ToString();
                }
                if ((row["Description"] != null) && (row["Description"].ToString() != ""))
                {
                    guestbook.Description = row["Description"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    guestbook.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CreatorEmail"] != null) && (row["CreatorEmail"].ToString() != ""))
                {
                    guestbook.CreatorEmail = row["CreatorEmail"].ToString();
                }
                if ((row["CreatorRegion"] != null) && (row["CreatorRegion"].ToString() != ""))
                {
                    guestbook.CreatorRegion = row["CreatorRegion"].ToString();
                }
                if ((row["CreatorCompany"] != null) && (row["CreatorCompany"].ToString() != ""))
                {
                    guestbook.CreatorCompany = row["CreatorCompany"].ToString();
                }
                if ((row["CreatorPageSite"] != null) && (row["CreatorPageSite"].ToString() != ""))
                {
                    guestbook.CreatorPageSite = row["CreatorPageSite"].ToString();
                }
                if ((row["CreatorPhone"] != null) && (row["CreatorPhone"].ToString() != ""))
                {
                    guestbook.CreatorPhone = row["CreatorPhone"].ToString();
                }
                if ((row["CreatorQQ"] != null) && (row["CreatorQQ"].ToString() != ""))
                {
                    guestbook.CreatorQQ = row["CreatorQQ"].ToString();
                }
                if ((row["CreatorMsn"] != null) && (row["CreatorMsn"].ToString() != ""))
                {
                    guestbook.CreatorMsn = row["CreatorMsn"].ToString();
                }
                if ((row["CreatorSex"] != null) && (row["CreatorSex"].ToString() != ""))
                {
                    if ((row["CreatorSex"].ToString() == "1") || (row["CreatorSex"].ToString().ToLower() == "true"))
                    {
                        guestbook.CreatorSex = true;
                    }
                    else
                    {
                        guestbook.CreatorSex = false;
                    }
                }
                if ((row["HandlerNickName"] != null) && (row["HandlerNickName"].ToString() != ""))
                {
                    guestbook.HandlerNickName = row["HandlerNickName"].ToString();
                }
                if ((row["HandlerUserID"] != null) && (row["HandlerUserID"].ToString() != ""))
                {
                    guestbook.HandlerUserID = new int?(int.Parse(row["HandlerUserID"].ToString()));
                }
                if ((row["HandlerDate"] != null) && (row["HandlerDate"].ToString() != ""))
                {
                    guestbook.HandlerDate = new DateTime?(DateTime.Parse(row["HandlerDate"].ToString()));
                }
                if ((row["Privacy"] != null) && (row["Privacy"].ToString() != ""))
                {
                    guestbook.Privacy = new int?(int.Parse(row["Privacy"].ToString()));
                }
                if ((row["ReplyCount"] != null) && (row["ReplyCount"].ToString() != ""))
                {
                    guestbook.ReplyCount = int.Parse(row["ReplyCount"].ToString());
                }
                if ((row["ReplyDescription"] != null) && (row["ReplyDescription"].ToString() != ""))
                {
                    guestbook.ReplyDescription = row["ReplyDescription"].ToString();
                }
                if ((row["ParentID"] != null) && (row["ParentID"].ToString() != ""))
                {
                    guestbook.ParentID = new int?(int.Parse(row["ParentID"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    guestbook.Status = new int?(int.Parse(row["Status"].ToString()));
                }
            }
            return guestbook;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_Guestbook ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_Guestbook ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_Guestbook");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,CreateUserID,CreateNickName,ToUserID,ToNickName,CreatorUserIP,Title,Description,CreatedDate,CreatorEmail,CreatorRegion,CreatorCompany,CreatorPageSite,CreatorPhone,CreatorQQ,CreatorMsn,CreatorSex,HandlerNickName,HandlerUserID,HandlerDate,Privacy,ReplyCount,ReplyDescription,ParentID,Status ");
            builder.Append(" FROM SA_Guestbook ");
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
            builder.Append(" ID,CreateUserID,CreateNickName,ToUserID,ToNickName,CreatorUserIP,Title,Description,CreatedDate,CreatorEmail,CreatorRegion,CreatorCompany,CreatorPageSite,CreatorPhone,CreatorQQ,CreatorMsn,CreatorSex,HandlerNickName,HandlerUserID,HandlerDate,Privacy,ReplyCount,ReplyDescription,ParentID,Status ");
            builder.Append(" FROM SA_Guestbook ");
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
            builder.Append(")AS Row, T.*  from SA_Guestbook T ");
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
            return DbHelperSQL.GetMaxID("ID", "SA_Guestbook");
        }

        public Maticsoft.Model.Members.Guestbook GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,CreateUserID,CreateNickName,ToUserID,ToNickName,CreatorUserIP,Title,Description,CreatedDate,CreatorEmail,CreatorRegion,CreatorCompany,CreatorPageSite,CreatorPhone,CreatorQQ,CreatorMsn,CreatorSex,HandlerNickName,HandlerUserID,HandlerDate,Privacy,ReplyCount,ReplyDescription,ParentID,Status from SA_Guestbook ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.Members.Guestbook();
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
            builder.Append("select count(1) FROM SA_Guestbook ");
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

        public bool Update(Maticsoft.Model.Members.Guestbook model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Guestbook set ");
            builder.Append("CreateUserID=@CreateUserID,");
            builder.Append("CreateNickName=@CreateNickName,");
            builder.Append("ToUserID=@ToUserID,");
            builder.Append("ToNickName=@ToNickName,");
            builder.Append("CreatorUserIP=@CreatorUserIP,");
            builder.Append("Title=@Title,");
            builder.Append("Description=@Description,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatorEmail=@CreatorEmail,");
            builder.Append("CreatorRegion=@CreatorRegion,");
            builder.Append("CreatorCompany=@CreatorCompany,");
            builder.Append("CreatorPageSite=@CreatorPageSite,");
            builder.Append("CreatorPhone=@CreatorPhone,");
            builder.Append("CreatorQQ=@CreatorQQ,");
            builder.Append("CreatorMsn=@CreatorMsn,");
            builder.Append("CreatorSex=@CreatorSex,");
            builder.Append("HandlerNickName=@HandlerNickName,");
            builder.Append("HandlerUserID=@HandlerUserID,");
            builder.Append("HandlerDate=@HandlerDate,");
            builder.Append("Privacy=@Privacy,");
            builder.Append("ReplyCount=@ReplyCount,");
            builder.Append("ReplyDescription=@ReplyDescription,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreateNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ToUserID", SqlDbType.Int, 4), new SqlParameter("@ToNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorUserIP", SqlDbType.NVarChar, 20), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatorEmail", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorRegion", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorCompany", SqlDbType.NVarChar, 100), new SqlParameter("@CreatorPageSite", SqlDbType.NVarChar, 200), new SqlParameter("@CreatorPhone", SqlDbType.NVarChar, 20), new SqlParameter("@CreatorQQ", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorMsn", SqlDbType.NVarChar, 50), new SqlParameter("@CreatorSex", SqlDbType.Bit, 1), 
                new SqlParameter("@HandlerNickName", SqlDbType.NVarChar, 50), new SqlParameter("@HandlerUserID", SqlDbType.Int, 4), new SqlParameter("@HandlerDate", SqlDbType.DateTime), new SqlParameter("@Privacy", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@ReplyDescription", SqlDbType.NVarChar, 500), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.CreateUserID;
            cmdParms[1].Value = model.CreateNickName;
            cmdParms[2].Value = model.ToUserID;
            cmdParms[3].Value = model.ToNickName;
            cmdParms[4].Value = model.CreatorUserIP;
            cmdParms[5].Value = model.Title;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.CreatorEmail;
            cmdParms[9].Value = model.CreatorRegion;
            cmdParms[10].Value = model.CreatorCompany;
            cmdParms[11].Value = model.CreatorPageSite;
            cmdParms[12].Value = model.CreatorPhone;
            cmdParms[13].Value = model.CreatorQQ;
            cmdParms[14].Value = model.CreatorMsn;
            cmdParms[15].Value = model.CreatorSex;
            cmdParms[0x10].Value = model.HandlerNickName;
            cmdParms[0x11].Value = model.HandlerUserID;
            cmdParms[0x12].Value = model.HandlerDate;
            cmdParms[0x13].Value = model.Privacy;
            cmdParms[20].Value = model.ReplyCount;
            cmdParms[0x15].Value = model.ReplyDescription;
            cmdParms[0x16].Value = model.ParentID;
            cmdParms[0x17].Value = model.Status;
            cmdParms[0x18].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

