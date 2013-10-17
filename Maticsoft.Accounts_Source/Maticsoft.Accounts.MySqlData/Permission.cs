namespace Maticsoft.Accounts.MySqlData
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Text;

    public class Permission : IPermission
    {
        public int Create(int categoryID, string description)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 8), new MySqlParameter("?_Description", MySqlDbType.VarChar, 50) };
            parameters[0].Value = categoryID;
            parameters[1].Value = description;
            return DbHelperMySQL.RunProcedure("sp_Accounts_CreatePermission", parameters, out num);
        }

        public bool Delete(int id)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 4) };
            parameters[0].Value = id;
            DbHelperMySQL.RunProcedure("sp_Accounts_DeletePermission", parameters, out num);
            return (num == 1);
        }

        public DataSet GetNoPermissionList(int roleId)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_RoleID", MySqlDbType.VarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                DbHelperMySQL.RunProcedure("sp_Accounts_GetNoPermissionList", parameters, set, "Permissions");
                DataRelation relation = new DataRelation("PermissionCategories", set.Tables["Categories"].Columns["CategoryID"], set.Tables["Permissions"].Columns["CategoryID"], true);
                set.Relations.Add(relation);
                DataColumn[] columnArray = new DataColumn[] { set.Tables["Categories"].Columns["CategoryID"] };
                DataColumn[] columnArray2 = new DataColumn[] { set.Tables["Permissions"].Columns["PermissionID"] };
                set.Tables["Categories"].PrimaryKey = columnArray;
                set.Tables["Permissions"].PrimaryKey = columnArray2;
                return set;
            }
        }

        public DataSet GetPermissionList()
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_RoleID", MySqlDbType.VarChar, 4) };
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, set, "Permissions");
                DataRelation relation = new DataRelation("PermissionCategories", set.Tables["Categories"].Columns["CategoryID"], set.Tables["Permissions"].Columns["CategoryID"], true);
                set.Relations.Add(relation);
                DataColumn[] columnArray = new DataColumn[] { set.Tables["Categories"].Columns["CategoryID"] };
                DataColumn[] columnArray2 = new DataColumn[] { set.Tables["Permissions"].Columns["PermissionID"] };
                set.Tables["Categories"].PrimaryKey = columnArray;
                set.Tables["Permissions"].PrimaryKey = columnArray2;
                return set;
            }
        }

        public DataSet GetPermissionList(int roleId)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_RoleID", MySqlDbType.VarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, set, "Permissions");
                DataRelation relation = new DataRelation("PermissionCategories", set.Tables["Categories"].Columns["CategoryID"], set.Tables["Permissions"].Columns["CategoryID"], true);
                set.Relations.Add(relation);
                DataColumn[] columnArray = new DataColumn[] { set.Tables["Categories"].Columns["CategoryID"] };
                DataColumn[] columnArray2 = new DataColumn[] { set.Tables["Permissions"].Columns["PermissionID"] };
                set.Tables["Categories"].PrimaryKey = columnArray;
                set.Tables["Permissions"].PrimaryKey = columnArray2;
                return set;
            }
        }

        public DataSet Retrieve(int permissionId)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 4) };
            parameters[0].Value = permissionId;
            return DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionDetails", parameters, "Permissions");
        }

        public bool Update(int PermissionID, string description)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 8), new MySqlParameter("?_Description", MySqlDbType.VarChar, 50) };
            parameters[0].Value = PermissionID;
            parameters[1].Value = description;
            DbHelperMySQL.RunProcedure("sp_Accounts_UpdatePermission", parameters, out num);
            return (num == 1);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Permissions set ");
            builder.AppendFormat(" CategoryID={0}", CategoryID);
            builder.AppendFormat(" where PermissionID in({0})", PermissionIDlist);
            DbHelperMySQL.ExecuteSql(builder.ToString());
        }
    }
}

