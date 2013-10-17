namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SearchWordLog : ISearchWordLog
    {
        public int Add(Maticsoft.Model.SNS.SearchWordLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_SearchWordLog(");
            builder.Append("SearchWord,CreatedUserId,CreatedNickName,CreatedDate,Status)");
            builder.Append(" values (");
            builder.Append("@SearchWord,@CreatedUserId,@CreatedNickName,@CreatedDate,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SearchWord", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.SearchWord;
            cmdParms[1].Value = model.CreatedUserId;
            cmdParms[2].Value = model.CreatedNickName;
            cmdParms[3].Value = model.CreatedDate;
            cmdParms[4].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.SearchWordLog DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.SearchWordLog log = new Maticsoft.Model.SNS.SearchWordLog();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    log.ID = int.Parse(row["ID"].ToString());
                }
                if (row["SearchWord"] != null)
                {
                    log.SearchWord = row["SearchWord"].ToString();
                }
                if ((row["CreatedUserId"] != null) && (row["CreatedUserId"].ToString() != ""))
                {
                    log.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    log.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    log.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    log.Status = int.Parse(row["Status"].ToString());
                }
            }
            return log;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_SearchWordLog ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_SearchWordLog ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool GetHotHotWordssList(int Top)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DELETE SNS_SearchWordTop INSERT INTO SNS_SearchWordTop(HotWord,SearchCount,TimeUnit,Status,CreatedDate) SELECT TOP {0} SearchWord,COUNT(SearchWord) AS COUNT,0,1,GETDATE() FROM SNS_SearchWordLog  GROUP BY SearchWord ORDER BY COUNT desc ", Top);
            if (DbHelperSQL.ExecuteSql(builder.ToString()) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,SearchWord,CreatedUserId,CreatedNickName,CreatedDate,Status ");
            builder.Append(" FROM SNS_SearchWordLog ");
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
            builder.Append(" ID,SearchWord,CreatedUserId,CreatedNickName,CreatedDate,Status ");
            builder.Append(" FROM SNS_SearchWordLog ");
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
            builder.Append(")AS Row, T.*  from SNS_SearchWordLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.SearchWordLog GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,SearchWord,CreatedUserId,CreatedNickName,CreatedDate,Status from SNS_SearchWordLog ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.SearchWordLog();
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
            builder.Append("select count(1) FROM SNS_SearchWordLog ");
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

        public bool Update(Maticsoft.Model.SNS.SearchWordLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_SearchWordLog set ");
            builder.Append("SearchWord=@SearchWord,");
            builder.Append("CreatedUserId=@CreatedUserId,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SearchWord", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.SearchWord;
            cmdParms[1].Value = model.CreatedUserId;
            cmdParms[2].Value = model.CreatedNickName;
            cmdParms[3].Value = model.CreatedDate;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

