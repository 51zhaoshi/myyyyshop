namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SearchWordTop : ISearchWordTop
    {
        public int Add(Maticsoft.Model.SNS.SearchWordTop model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_SearchWordTop(");
            builder.Append("HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status)");
            builder.Append(" values (");
            builder.Append("@HotWord,@TimeUnit,@DateStart,@DateEnd,@SearchCount,@CreatedDate,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HotWord", SqlDbType.NVarChar, 100), new SqlParameter("@TimeUnit", SqlDbType.Int, 4), new SqlParameter("@DateStart", SqlDbType.DateTime), new SqlParameter("@DateEnd", SqlDbType.DateTime), new SqlParameter("@SearchCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.HotWord;
            cmdParms[1].Value = model.TimeUnit;
            cmdParms[2].Value = model.DateStart;
            cmdParms[3].Value = model.DateEnd;
            cmdParms[4].Value = model.SearchCount;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.SearchWordTop DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.SearchWordTop top = new Maticsoft.Model.SNS.SearchWordTop();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    top.ID = int.Parse(row["ID"].ToString());
                }
                if (row["HotWord"] != null)
                {
                    top.HotWord = row["HotWord"].ToString();
                }
                if ((row["TimeUnit"] != null) && (row["TimeUnit"].ToString() != ""))
                {
                    top.TimeUnit = int.Parse(row["TimeUnit"].ToString());
                }
                if ((row["DateStart"] != null) && (row["DateStart"].ToString() != ""))
                {
                    top.DateStart = new DateTime?(DateTime.Parse(row["DateStart"].ToString()));
                }
                if ((row["DateEnd"] != null) && (row["DateEnd"].ToString() != ""))
                {
                    top.DateEnd = new DateTime?(DateTime.Parse(row["DateEnd"].ToString()));
                }
                if ((row["SearchCount"] != null) && (row["SearchCount"].ToString() != ""))
                {
                    top.SearchCount = int.Parse(row["SearchCount"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    top.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    top.Status = int.Parse(row["Status"].ToString());
                }
            }
            return top;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_SearchWordTop ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_SearchWordTop ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string HotWord)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_SearchWordTop");
            builder.Append(" where HotWord=@HotWord");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HotWord", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = HotWord;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status ");
            builder.Append(" FROM SNS_SearchWordTop ");
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
            builder.Append(" ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status ");
            builder.Append(" FROM SNS_SearchWordTop ");
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
            builder.Append(")AS Row, T.*  from SNS_SearchWordTop T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status ,ROW_NUMBER() OVER( ORDER BY ID) AS Rank ");
            builder.Append(" FROM SNS_SearchWordTop ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.SearchWordTop GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,HotWord,TimeUnit,DateStart,DateEnd,SearchCount,CreatedDate,Status from SNS_SearchWordTop ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.SearchWordTop();
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
            builder.Append("select count(1) FROM SNS_SearchWordTop ");
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

        public bool Update(Maticsoft.Model.SNS.SearchWordTop model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_SearchWordTop set ");
            builder.Append("HotWord=@HotWord,");
            builder.Append("TimeUnit=@TimeUnit,");
            builder.Append("DateStart=@DateStart,");
            builder.Append("DateEnd=@DateEnd,");
            builder.Append("SearchCount=@SearchCount,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HotWord", SqlDbType.NVarChar, 100), new SqlParameter("@TimeUnit", SqlDbType.Int, 4), new SqlParameter("@DateStart", SqlDbType.DateTime), new SqlParameter("@DateEnd", SqlDbType.DateTime), new SqlParameter("@SearchCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.HotWord;
            cmdParms[1].Value = model.TimeUnit;
            cmdParms[2].Value = model.DateStart;
            cmdParms[3].Value = model.DateEnd;
            cmdParms[4].Value = model.SearchCount;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.Status;
            cmdParms[7].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

