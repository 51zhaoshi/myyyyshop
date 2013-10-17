namespace Maticsoft.SQLServerDAL.Settings
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class FilterWords : IFilterWords
    {
        public int Add(Maticsoft.Model.Settings.FilterWords model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_FilterWords(");
            builder.Append("WordPattern,ActionType,RepalceWord)");
            builder.Append(" values (");
            builder.Append("@WordPattern,@ActionType,@RepalceWord)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WordPattern", SqlDbType.NVarChar, 100), new SqlParameter("@ActionType", SqlDbType.Int, 4), new SqlParameter("@RepalceWord", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.WordPattern;
            cmdParms[1].Value = model.ActionType;
            cmdParms[2].Value = model.RepalceWord;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Settings.FilterWords DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Settings.FilterWords words = new Maticsoft.Model.Settings.FilterWords();
            if (row != null)
            {
                if ((row["FilterId"] != null) && (row["FilterId"].ToString() != ""))
                {
                    words.FilterId = int.Parse(row["FilterId"].ToString());
                }
                if (row["WordPattern"] != null)
                {
                    words.WordPattern = row["WordPattern"].ToString();
                }
                if ((row["ActionType"] != null) && (row["ActionType"].ToString() != ""))
                {
                    words.ActionType = int.Parse(row["ActionType"].ToString());
                }
                if (row["RepalceWord"] != null)
                {
                    words.RepalceWord = row["RepalceWord"].ToString();
                }
            }
            return words;
        }

        public bool Delete(int FilterId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_FilterWords ");
            builder.Append(" where FilterId=@FilterId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FilterId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FilterId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string FilterIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_FilterWords ");
            builder.Append(" where FilterId in (" + FilterIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int FilterId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_FilterWords");
            builder.Append(" where FilterId=@FilterId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FilterId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FilterId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public Maticsoft.Model.Settings.FilterWords GetByWordPattern(string wordPattern)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FilterId,WordPattern,ActionType,RepalceWord from SA_FilterWords ");
            builder.Append(" where WordPattern='@WordPattern'");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WordPattern", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = wordPattern;
            new Maticsoft.Model.Settings.FilterWords();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FilterId,WordPattern,ActionType,RepalceWord ");
            builder.Append(" FROM SA_FilterWords ");
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
            builder.Append(" FilterId,WordPattern,ActionType,RepalceWord ");
            builder.Append(" FROM SA_FilterWords ");
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
                builder.Append("order by T.FilterId desc");
            }
            builder.Append(")AS Row, T.*  from SA_FilterWords T ");
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
            return DbHelperSQL.GetMaxID("FilterId", "SA_FilterWords");
        }

        public Maticsoft.Model.Settings.FilterWords GetModel(int FilterId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FilterId,WordPattern,ActionType,RepalceWord from SA_FilterWords ");
            builder.Append(" where FilterId=@FilterId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FilterId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FilterId;
            new Maticsoft.Model.Settings.FilterWords();
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
            builder.Append("select count(1) FROM SA_FilterWords ");
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

        public bool Update(Maticsoft.Model.Settings.FilterWords model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_FilterWords set ");
            builder.Append("WordPattern=@WordPattern,");
            builder.Append("ActionType=@ActionType,");
            builder.Append("RepalceWord=@RepalceWord");
            builder.Append(" where FilterId=@FilterId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WordPattern", SqlDbType.NVarChar, 100), new SqlParameter("@ActionType", SqlDbType.Int, 4), new SqlParameter("@RepalceWord", SqlDbType.NVarChar, 100), new SqlParameter("@FilterId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.WordPattern;
            cmdParms[1].Value = model.ActionType;
            cmdParms[2].Value = model.RepalceWord;
            cmdParms[3].Value = model.FilterId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateActionType(string ids, int type, string replace)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_FilterWords set ");
            builder.Append("ActionType=@ActionType,RepalceWord=@RepalceWord");
            builder.Append(" where FilterId in (" + ids + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionType", SqlDbType.Int, 4), new SqlParameter("@RepalceWord", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = type;
            cmdParms[1].Value = replace;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

