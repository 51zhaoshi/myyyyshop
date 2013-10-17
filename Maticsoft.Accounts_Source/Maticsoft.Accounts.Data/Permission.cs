namespace Maticsoft.Accounts.Data
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Permission : IPermission
    {
        public int Create(int categoryID, string description)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 8), new SqlParameter("@Description", SqlDbType.NVarChar, 50) };
            parameters[0].Value = categoryID;
            parameters[1].Value = description;
            return DbHelperSQL.RunProcedure("sp_Accounts_CreatePermission", parameters, out num);
        }

        public bool Delete(int id)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            DbHelperSQL.RunProcedure("sp_Accounts_DeletePermission", parameters, out num);
            return (num == 1);
        }

        public DataSet GetNoPermissionList(int roleId)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                DbHelperSQL.RunProcedure("sp_Accounts_GetNoPermissionList", parameters, set, "Permissions");
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
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, set, "Permissions");
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
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[0], "Categories"))
            {
                DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, set, "Permissions");
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
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            parameters[0].Value = permissionId;
            return DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionDetails", parameters, "Permissions");
        }

        public bool Update(int PermissionID, string description)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@PermissionID", SqlDbType.Int, 8), new SqlParameter("@Description", SqlDbType.NVarChar, 50) };
            parameters[0].Value = PermissionID;
            parameters[1].Value = description;
            DbHelperSQL.RunProcedure("sp_Accounts_UpdatePermission", parameters, out num);
            return (num == 1);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Permissions set ");
            builder.AppendFormat(" CategoryID={0}", CategoryID);
            builder.AppendFormat(" where PermissionID in({0})", PermissionIDlist);
            DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}

