namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class RegionRec : IRegionRec
    {
        public int Add(Maticsoft.Model.Ms.RegionRec model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_RegionRec(");
            builder.Append("RegionId,RegionName,DisplaySequence,Type)");
            builder.Append(" values (");
            builder.Append("@RegionId,@RegionName,@DisplaySequence,@Type)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@RegionName", SqlDbType.NVarChar, 100), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.SmallInt, 2) };
            cmdParms[0].Value = model.RegionId;
            cmdParms[1].Value = model.RegionName;
            cmdParms[2].Value = model.DisplaySequence;
            cmdParms[3].Value = model.Type;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Ms.RegionRec DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.RegionRec rec = new Maticsoft.Model.Ms.RegionRec();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    rec.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    rec.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["RegionName"] != null)
                {
                    rec.RegionName = row["RegionName"].ToString();
                }
                if ((row["DisplaySequence"] != null) && (row["DisplaySequence"].ToString() != ""))
                {
                    rec.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    rec.Type = int.Parse(row["Type"].ToString());
                }
            }
            return rec;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_RegionRec ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int regionId, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_RegionRec ");
            builder.Append(" where RegionId=@RegionId and Type=@Type");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = regionId;
            cmdParms[1].Value = type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_RegionRec ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_RegionRec");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,RegionId,RegionName,DisplaySequence,Type ");
            builder.Append(" FROM Ms_RegionRec ");
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
            builder.Append(" ID,RegionId,RegionName,DisplaySequence,Type ");
            builder.Append(" FROM Ms_RegionRec ");
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
            builder.Append(")AS Row, T.*  from Ms_RegionRec T ");
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
            return DbHelperSQL.GetMaxID("ID", "Ms_RegionRec");
        }

        public Maticsoft.Model.Ms.RegionRec GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,RegionId,RegionName,DisplaySequence,Type from Ms_RegionRec ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.Ms.RegionRec();
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
            builder.Append("select count(1) FROM Ms_RegionRec ");
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

        public bool Update(Maticsoft.Model.Ms.RegionRec model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_RegionRec set ");
            builder.Append("RegionId=@RegionId,");
            builder.Append("RegionName=@RegionName,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Type=@Type");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@RegionName", SqlDbType.NVarChar, 100), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RegionId;
            cmdParms[1].Value = model.RegionName;
            cmdParms[2].Value = model.DisplaySequence;
            cmdParms[3].Value = model.Type;
            cmdParms[4].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

