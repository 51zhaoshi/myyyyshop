namespace Maticsoft.Accounts.Data
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PermissionCategory : IPermissionCategory
    {
        public int Create(string description)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Description", SqlDbType.NVarChar, 50) };
            parameters[0].Value = description;
            return DbHelperSQL.RunProcedure("sp_Accounts_CreatePermissionCategory", parameters, out num);
        }

        public bool Delete(int CategoryID)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            parameters[0].Value = CategoryID;
            DbHelperSQL.RunProcedure("sp_Accounts_DeletePermissionCategory", parameters, out num);
            return (num == 1);
        }

        public bool ExistsPerm(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_Permissions");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetCategoryList()
        {
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                return set;
            }
        }

        public DataSet GetPermissionsInCategory(int categoryId)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            parameters[0].Value = categoryId;
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionsInCategory", parameters, "Categories"))
            {
                return set;
            }
        }

        public DataRow Retrieve(int categoryId)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            parameters[0].Value = categoryId;
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategoryDetails", parameters, "Categories"))
            {
                return set.Tables[0].Rows[0];
            }
        }
    }
}

