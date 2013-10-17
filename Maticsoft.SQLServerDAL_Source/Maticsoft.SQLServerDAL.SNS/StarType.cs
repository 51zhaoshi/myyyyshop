namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class StarType : IStarType
    {
        public int Add(Maticsoft.Model.SNS.StarType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_StarType(");
            builder.Append("TypeName,CheckRule,Remark,Status)");
            builder.Append(" values (");
            builder.Append("@TypeName,@CheckRule,@Remark,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 100), new SqlParameter("@CheckRule", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.CheckRule;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.StarType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.StarType type = new Maticsoft.Model.SNS.StarType();
            if (row != null)
            {
                if ((row["TypeID"] != null) && (row["TypeID"].ToString() != ""))
                {
                    type.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if (row["TypeName"] != null)
                {
                    type.TypeName = row["TypeName"].ToString();
                }
                if (row["CheckRule"] != null)
                {
                    type.CheckRule = row["CheckRule"].ToString();
                }
                if (row["Remark"] != null)
                {
                    type.Remark = row["Remark"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    type.Status = new int?(int.Parse(row["Status"].ToString()));
                }
            }
            return type;
        }

        public bool Delete(int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_StarType ");
            builder.Append(" where TypeID=@TypeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TypeIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_StarType ");
            builder.Append(" where TypeID in (" + TypeIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TypeName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_StarType");
            builder.Append(" where TypeName=@TypeName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = TypeName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeID,TypeName,CheckRule,Remark,Status ");
            builder.Append(" FROM SNS_StarType ");
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
            builder.Append(" TypeID,TypeName,CheckRule,Remark,Status ");
            builder.Append(" FROM SNS_StarType ");
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
                builder.Append("order by T.TypeID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_StarType T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.StarType GetModel(int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TypeID,TypeName,CheckRule,Remark,Status from SNS_StarType ");
            builder.Append(" where TypeID=@TypeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeID;
            new Maticsoft.Model.SNS.StarType();
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
            builder.Append("select count(1) FROM SNS_StarType ");
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

        public bool Update(Maticsoft.Model.SNS.StarType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_StarType set ");
            builder.Append("TypeName=@TypeName,");
            builder.Append("CheckRule=@CheckRule,");
            builder.Append("Remark=@Remark,");
            builder.Append("Status=@Status");
            builder.Append(" where TypeID=@TypeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 100), new SqlParameter("@CheckRule", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@TypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.CheckRule;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.TypeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

