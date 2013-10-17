namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class GuestBook : IGuestBook
    {
        public int Add(Maticsoft.Model.SNS.GuestBook model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GuestBook(");
            builder.Append("CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth)");
            builder.Append(" values (");
            builder.Append("@CreateUserID,@CreateNickName,@ToUserID,@ToNickName,@ParentID,@Description,@UserIP,@Privacy,@CreatedDate,@Email,@Path,@Depth)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreateNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ToUserID", SqlDbType.Int, 4), new SqlParameter("@ToNickName", SqlDbType.NVarChar, 100), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@Path", SqlDbType.NVarChar, 100), new SqlParameter("@Depth", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CreateUserID;
            cmdParms[1].Value = model.CreateNickName;
            cmdParms[2].Value = model.ToUserID;
            cmdParms[3].Value = model.ToNickName;
            cmdParms[4].Value = model.ParentID;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.UserIP;
            cmdParms[7].Value = model.Privacy;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.Email;
            cmdParms[10].Value = model.Path;
            cmdParms[11].Value = model.Depth;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.GuestBook DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.GuestBook book = new Maticsoft.Model.SNS.GuestBook();
            if (row != null)
            {
                if ((row["GuestBookID"] != null) && (row["GuestBookID"].ToString() != ""))
                {
                    book.GuestBookID = int.Parse(row["GuestBookID"].ToString());
                }
                if ((row["CreateUserID"] != null) && (row["CreateUserID"].ToString() != ""))
                {
                    book.CreateUserID = int.Parse(row["CreateUserID"].ToString());
                }
                if (row["CreateNickName"] != null)
                {
                    book.CreateNickName = row["CreateNickName"].ToString();
                }
                if ((row["ToUserID"] != null) && (row["ToUserID"].ToString() != ""))
                {
                    book.ToUserID = int.Parse(row["ToUserID"].ToString());
                }
                if (row["ToNickName"] != null)
                {
                    book.ToNickName = row["ToNickName"].ToString();
                }
                if ((row["ParentID"] != null) && (row["ParentID"].ToString() != ""))
                {
                    book.ParentID = new int?(int.Parse(row["ParentID"].ToString()));
                }
                if (row["Description"] != null)
                {
                    book.Description = row["Description"].ToString();
                }
                if (row["UserIP"] != null)
                {
                    book.UserIP = row["UserIP"].ToString();
                }
                if ((row["Privacy"] != null) && (row["Privacy"].ToString() != ""))
                {
                    book.Privacy = new int?(int.Parse(row["Privacy"].ToString()));
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    book.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Email"] != null)
                {
                    book.Email = row["Email"].ToString();
                }
                if (row["Path"] != null)
                {
                    book.Path = row["Path"].ToString();
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    book.Depth = new int?(int.Parse(row["Depth"].ToString()));
                }
            }
            return book;
        }

        public bool Delete(int GuestBookID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GuestBook ");
            builder.Append(" where GuestBookID=@GuestBookID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GuestBookID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GuestBookID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string GuestBookIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GuestBook ");
            builder.Append(" where GuestBookID in (" + GuestBookIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select GuestBookID,CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth ");
            builder.Append(" FROM SNS_GuestBook ");
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
            builder.Append(" GuestBookID,CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth ");
            builder.Append(" FROM SNS_GuestBook ");
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
                builder.Append("order by T.GuestBookID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_GuestBook T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.GuestBook GetModel(int GuestBookID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 GuestBookID,CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth from SNS_GuestBook ");
            builder.Append(" where GuestBookID=@GuestBookID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GuestBookID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GuestBookID;
            new Maticsoft.Model.SNS.GuestBook();
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
            builder.Append("select count(1) FROM SNS_GuestBook ");
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

        public bool Update(Maticsoft.Model.SNS.GuestBook model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GuestBook set ");
            builder.Append("CreateUserID=@CreateUserID,");
            builder.Append("CreateNickName=@CreateNickName,");
            builder.Append("ToUserID=@ToUserID,");
            builder.Append("ToNickName=@ToNickName,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Description=@Description,");
            builder.Append("UserIP=@UserIP,");
            builder.Append("Privacy=@Privacy,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Email=@Email,");
            builder.Append("Path=@Path,");
            builder.Append("Depth=@Depth");
            builder.Append(" where GuestBookID=@GuestBookID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreateNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ToUserID", SqlDbType.Int, 4), new SqlParameter("@ToNickName", SqlDbType.NVarChar, 100), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@Path", SqlDbType.NVarChar, 100), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@GuestBookID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CreateUserID;
            cmdParms[1].Value = model.CreateNickName;
            cmdParms[2].Value = model.ToUserID;
            cmdParms[3].Value = model.ToNickName;
            cmdParms[4].Value = model.ParentID;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.UserIP;
            cmdParms[7].Value = model.Privacy;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.Email;
            cmdParms[10].Value = model.Path;
            cmdParms[11].Value = model.Depth;
            cmdParms[12].Value = model.GuestBookID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

