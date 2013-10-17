namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class HotWords : IHotWords
    {
        public int Add(Maticsoft.Model.SNS.HotWords model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_HotWords(");
            builder.Append("KeyWord,CreatedDate,IsRecommend,Sequence,Status)");
            builder.Append(" values (");
            builder.Append("@KeyWord,@CreatedDate,@IsRecommend,@Sequence,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyWord", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.KeyWord;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.IsRecommend;
            cmdParms[3].Value = model.Sequence;
            cmdParms[4].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.HotWords DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.HotWords words = new Maticsoft.Model.SNS.HotWords();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    words.ID = int.Parse(row["ID"].ToString());
                }
                if (row["KeyWord"] != null)
                {
                    words.KeyWord = row["KeyWord"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    words.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["IsRecommend"] != null) && (row["IsRecommend"].ToString() != ""))
                {
                    if ((row["IsRecommend"].ToString() == "1") || (row["IsRecommend"].ToString().ToLower() == "true"))
                    {
                        words.IsRecommend = true;
                    }
                    else
                    {
                        words.IsRecommend = false;
                    }
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    words.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    words.Status = int.Parse(row["Status"].ToString());
                }
            }
            return words;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_HotWords");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_HotWords");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_HotWords");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string KeyWord)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_HotWords");
            builder.Append(" where KeyWord=@KeyWord");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyWord", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = KeyWord;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,KeyWord,CreatedDate,IsRecommend,Sequence,Status ");
            builder.Append(" FROM SNS_HotWords");
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
            builder.Append(" ID,KeyWord,CreatedDate,IsRecommend,Sequence,Status ");
            builder.Append(" FROM SNS_HotWords");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            builder.Append(" order by ID DESC");
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
            builder.Append(")AS Row, T.*  from SNS_HotWordsT ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "SNS_HotWords");
        }

        public Maticsoft.Model.SNS.HotWords GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,KeyWord,CreatedDate,IsRecommend,Sequence,Status from SNS_HotWords");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.HotWords();
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
            builder.Append("select count(1) FROM SNS_HotWords");
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

        public bool Update(Maticsoft.Model.SNS.HotWords model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_HotWordsset ");
            builder.Append("KeyWord=@KeyWord,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("IsRecommend=@IsRecommend,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyWord", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.KeyWord;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.IsRecommend;
            cmdParms[3].Value = model.Sequence;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

