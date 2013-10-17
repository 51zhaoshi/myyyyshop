namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class VerifyMail : IVerifyMail
    {
        public bool Add(Maticsoft.Model.SysManage.VerifyMail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_VerifyMail(");
            builder.Append("UserName,KeyValue,CreatedDate,Status,ValidityType)");
            builder.Append(" values (");
            builder.Append("@UserName,@KeyValue,@CreatedDate,@Status,@ValidityType)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@KeyValue", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ValidityType", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.KeyValue;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.ValidityType;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(string KeyValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_VerifyMail ");
            builder.Append(" where KeyValue=@KeyValue ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyValue", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = KeyValue;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string KeyValuelist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_VerifyMail ");
            builder.Append(" where KeyValue in (" + KeyValuelist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string KeyValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_VerifyMail");
            builder.Append(" where KeyValue=@KeyValue ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyValue", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = KeyValue;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserName,KeyValue,CreatedDate,Status,ValidityType ");
            builder.Append(" FROM Accounts_VerifyMail ");
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
            builder.Append(" UserName,KeyValue,CreatedDate,Status,ValidityType ");
            builder.Append(" FROM Accounts_VerifyMail ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.KeyValue desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_VerifyMail T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SysManage.VerifyMail GetModel(string KeyValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserName,KeyValue,CreatedDate,Status,ValidityType from Accounts_VerifyMail ");
            builder.Append(" where KeyValue=@KeyValue ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyValue", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = KeyValue;
            Maticsoft.Model.SysManage.VerifyMail mail = new Maticsoft.Model.SysManage.VerifyMail();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["UserName"] != null) && (set.Tables[0].Rows[0]["UserName"].ToString() != ""))
            {
                mail.UserName = set.Tables[0].Rows[0]["UserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["KeyValue"] != null) && (set.Tables[0].Rows[0]["KeyValue"].ToString() != ""))
            {
                mail.KeyValue = set.Tables[0].Rows[0]["KeyValue"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                mail.CreatedDate = DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                mail.Status = int.Parse(set.Tables[0].Rows[0]["Status"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ValidityType"] != null) && (set.Tables[0].Rows[0]["ValidityType"].ToString() != ""))
            {
                mail.ValidityType = new int?(int.Parse(set.Tables[0].Rows[0]["ValidityType"].ToString()));
            }
            return mail;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Accounts_VerifyMail ");
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

        public bool Update(Maticsoft.Model.SysManage.VerifyMail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_VerifyMail set ");
            builder.Append("UserName=@UserName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status,");
            builder.Append("ValidityType=@ValidityType");
            builder.Append(" where KeyValue=@KeyValue ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ValidityType", SqlDbType.Int, 4), new SqlParameter("@KeyValue", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.ValidityType;
            cmdParms[4].Value = model.KeyValue;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

