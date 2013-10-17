namespace Maticsoft.SQLServerDAL.Shop.Package
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Package;
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PackageCategory : IPackageCategory
    {
        public int Add(Maticsoft.Model.Shop.Package.PackageCategory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_PackageCategory(");
            builder.Append("Name,CreatedDate,Status,Remark)");
            builder.Append(" values (");
            builder.Append("@Name,@CreatedDate,@Status,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 500) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Package.PackageCategory DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Package.PackageCategory category = new Maticsoft.Model.Shop.Package.PackageCategory();
            if (row != null)
            {
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    category.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["Name"] != null) && (row["Name"].ToString() != ""))
                {
                    category.Name = row["Name"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    category.CreatedDate = new DateTime?(DateTime.Parse(row["CreatedDate"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    category.Status = new int?(int.Parse(row["Status"].ToString()));
                }
                if ((row["Remark"] != null) && (row["Remark"].ToString() != ""))
                {
                    category.Remark = row["Remark"].ToString();
                }
            }
            return category;
        }

        public bool Delete(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_PackageCategory ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_PackageCategory ");
            builder.Append(" where CategoryId in (" + CategoryIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_PackageCategory");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryId,Name,CreatedDate,Status,Remark ");
            builder.Append(" FROM Shop_PackageCategory ");
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
            builder.Append(" CategoryId,Name,CreatedDate,Status,Remark ");
            builder.Append(" FROM Shop_PackageCategory ");
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
                builder.Append("order by T.CategoryId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_PackageCategory T ");
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
            return DbHelperSQL.GetMaxID("CategoryId", "Shop_PackageCategory");
        }

        public Maticsoft.Model.Shop.Package.PackageCategory GetModel(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryId,Name,CreatedDate,Status,Remark from Shop_PackageCategory ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            new Maticsoft.Model.Shop.Package.PackageCategory();
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
            builder.Append("select count(1) FROM Shop_PackageCategory ");
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

        public bool Update(Maticsoft.Model.Shop.Package.PackageCategory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_PackageCategory set ");
            builder.Append("Name=@Name,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

