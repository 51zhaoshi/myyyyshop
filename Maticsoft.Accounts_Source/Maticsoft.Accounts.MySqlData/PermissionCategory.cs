namespace Maticsoft.Accounts.MySqlData
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Text;

    public class PermissionCategory : IPermissionCategory
    {
        public int Create(string description)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_Description", MySqlDbType.VarChar, 50) };
            parameters[0].Value = description;
            return DbHelperMySQL.RunProcedure("sp_Accounts_CreatePermissionCategory", parameters, out num);
        }

        public bool Delete(int CategoryID)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 4) };
            parameters[0].Value = CategoryID;
            DbHelperMySQL.RunProcedure("sp_Accounts_DeletePermissionCategory", parameters, out num);
            return (num == 1);
        }

        public bool ExistsPerm(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_Permissions");
            builder.Append(" where CategoryID=?CategoryID");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?CategoryID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = CategoryID;
            return DbHelperMySQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetCategoryList()
        {
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                return set;
            }
        }

        public DataSet GetPermissionsInCategory(int categoryId)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 4) };
            parameters[0].Value = categoryId;
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionsInCategory", parameters, "Categories"))
            {
                return set;
            }
        }

        public DataRow Retrieve(int categoryId)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 4) };
            parameters[0].Value = categoryId;
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategoryDetails", parameters, "Categories"))
            {
                return set.Tables[0].Rows[0];
            }
        }
    }
}

