namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MultiLanguage : IMultiLanguage
    {
        public int Add(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang, string MultiLang_cValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_MultiLang_det(");
            builder.Append("MultiLang_cField,MultiLang_iPKValue,MultiLang_cLang,MultiLang_cValue)");
            builder.Append(" values (");
            builder.Append("@MultiLang_cField,@MultiLang_iPKValue,@MultiLang_cLang,@MultiLang_cValue)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar, 50), new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int), new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar, 10), new SqlParameter("@MultiLang_cValue", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = MultiLang_cField;
            cmdParms[1].Value = MultiLang_iPKValue;
            cmdParms[2].Value = MultiLang_cLang;
            cmdParms[3].Value = MultiLang_cValue;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public void Delete(int MultiLang_iID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_MultiLang_det ");
            builder.Append(" where MultiLang_iID=@MultiLang_iID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_iID", SqlDbType.Int, 4) };
            cmdParms[0].Value = MultiLang_iID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_MultiLang_det");
            builder.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_iPKValue=@MultiLang_iPKValue and  MultiLang_cLang=@MultiLang_cLang");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar, 50), new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int), new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar, 10) };
            cmdParms[0].Value = MultiLang_cField;
            cmdParms[1].Value = MultiLang_iPKValue;
            cmdParms[2].Value = MultiLang_cLang;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public string GetDefaultLangCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Language_cCode ");
            builder.Append(" FROM SA_Language_mstr ");
            builder.Append(" where Language_bDefault= 1");
            DataSet set = DbHelperSQL.Query(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0]["Language_cCode"].ToString();
            }
            return null;
        }

        public DataSet GetLangListByValue(string MultiLang_cField, int MultiLang_iPKValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MultiLang_iID,MultiLang_cLang,MultiLang_cValue ");
            builder.Append(" FROM SA_MultiLang_det ");
            builder.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_iPKValue=@MultiLang_iPKValue ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar, 50), new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int) };
            cmdParms[0].Value = MultiLang_cField;
            cmdParms[1].Value = MultiLang_iPKValue;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetLanguageList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SA_Language_mstr ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetLanguageName(string Language_cCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Language_cName ");
            builder.Append(" FROM SA_Language_mstr ");
            builder.Append(" where Language_cCode= @Language_cCode");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Language_cCode", SqlDbType.NVarChar, 10) };
            cmdParms[0].Value = Language_cCode;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0]["Language_cName"].ToString();
            }
            return null;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SA_MultiLang_det ");
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
            builder.Append(" * ");
            builder.Append(" FROM SA_MultiLang_det ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetModel(int MultiLang_iID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from SA_MultiLang_det ");
            builder.Append(" where MultiLang_iID=@MultiLang_iID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_iID", SqlDbType.Int, 4) };
            cmdParms[0].Value = MultiLang_iID;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0]["MultiLang_cValue"].ToString();
            }
            return null;
        }

        public string GetModel(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from SA_MultiLang_det ");
            builder.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_iPKValue=@MultiLang_iPKValue and  MultiLang_cLang=@MultiLang_cLang");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar, 50), new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int), new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar, 10) };
            cmdParms[0].Value = MultiLang_cField;
            cmdParms[1].Value = MultiLang_iPKValue;
            cmdParms[2].Value = MultiLang_cLang;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0]["MultiLang_cValue"].ToString();
            }
            return null;
        }

        public DataSet GetValueListByLang(string MultiLang_cField, string MultiLang_cLang)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MultiLang_iID,MultiLang_iPKValue,MultiLang_cValue ");
            builder.Append(" FROM SA_MultiLang_det ");
            builder.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_cLang=@MultiLang_cLang ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar, 50), new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar, 10) };
            cmdParms[0].Value = MultiLang_cField;
            cmdParms[1].Value = MultiLang_cLang;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public void Update(int MultiLang_iID, string MultiLang_cValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_MultiLang_det set ");
            builder.Append("MultiLang_cValue=@MultiLang_cValue");
            builder.Append(" where MultiLang_iID=@MultiLang_iID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MultiLang_iID", SqlDbType.Int, 4), new SqlParameter("@MultiLang_cValue", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = MultiLang_iID;
            cmdParms[1].Value = MultiLang_cValue;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

