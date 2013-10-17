namespace Maticsoft.SQLServerDAL
{
    using Maticsoft.DBUtility;
    using System;
    using System.Text;

    public class DALCommon
    {
        public static void SetValid(string TableName, string FieldName, int bvalid, string FielddateExpire, string WhereField, string WhereFieldValueList)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update {0} set ", TableName);
            builder.AppendFormat(" {0}={1}", FieldName, bvalid);
            if (FielddateExpire.Length > 0)
            {
                builder.AppendFormat(" ,{0}='{1}'", FielddateExpire, DateTime.Now);
            }
            builder.AppendFormat(" where {0} in ({1}) ", WhereField, WhereFieldValueList);
            DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}

