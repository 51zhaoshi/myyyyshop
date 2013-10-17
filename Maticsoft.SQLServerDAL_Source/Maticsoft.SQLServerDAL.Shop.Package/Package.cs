namespace Maticsoft.SQLServerDAL.Shop.Package
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Package;
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Package : IPackage
    {
        public int Add(Maticsoft.Model.Shop.Package.Package model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Package(");
            builder.Append("CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark)");
            builder.Append(" values (");
            builder.Append("@CategoryId,@Name,@Description,@PhotoUrl,@NormalPhotoUrl,@ThumbPhotoUrl,@CreatedDate,@Status,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@NormalPhotoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@ThumbPhotoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8) };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.PhotoUrl;
            cmdParms[4].Value = model.NormalPhotoUrl;
            cmdParms[5].Value = model.ThumbPhotoUrl;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Status;
            cmdParms[8].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Package.Package DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Package.Package package = new Maticsoft.Model.Shop.Package.Package();
            if (row != null)
            {
                if ((row["PackageId"] != null) && (row["PackageId"].ToString() != ""))
                {
                    package.PackageId = int.Parse(row["PackageId"].ToString());
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    package.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["Name"] != null) && (row["Name"].ToString() != ""))
                {
                    package.Name = row["Name"].ToString();
                }
                if ((row["Description"] != null) && (row["Description"].ToString() != ""))
                {
                    package.Description = row["Description"].ToString();
                }
                if ((row["PhotoUrl"] != null) && (row["PhotoUrl"].ToString() != ""))
                {
                    package.PhotoUrl = row["PhotoUrl"].ToString();
                }
                if ((row["NormalPhotoUrl"] != null) && (row["NormalPhotoUrl"].ToString() != ""))
                {
                    package.NormalPhotoUrl = row["NormalPhotoUrl"].ToString();
                }
                if ((row["ThumbPhotoUrl"] != null) && (row["ThumbPhotoUrl"].ToString() != ""))
                {
                    package.ThumbPhotoUrl = row["ThumbPhotoUrl"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    package.CreatedDate = new DateTime?(DateTime.Parse(row["CreatedDate"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    package.Status = new int?(int.Parse(row["Status"].ToString()));
                }
                if ((row["Remark"] != null) && (row["Remark"].ToString() != ""))
                {
                    package.Remark = row["Remark"].ToString();
                }
            }
            return package;
        }

        public bool Delete(int PackageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Package ");
            builder.Append(" where PackageId=@PackageId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = PackageId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string PackageIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Package ");
            builder.Append(" where PackageId in (" + PackageIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int PackageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Package");
            builder.Append(" where PackageId=@PackageId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = PackageId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PackageId,CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark ");
            builder.Append(" FROM Shop_Package ");
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
            builder.Append(" PackageId,CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark ");
            builder.Append(" FROM Shop_Package ");
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
                builder.Append("order by T.PackageId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Package T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select p1.PackageId,p2.NAME CategoryName,p1.NAME PackageName,p1.description as description,p1.PhotoUrl,p1.CreatedDate,p1.Remark from Shop_Package p1 left join Shop_PackageCategory p2 on p1.CategoryId=p2.CategoryId ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PackageId", "Shop_Package");
        }

        public Maticsoft.Model.Shop.Package.Package GetModel(int PackageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 PackageId,CategoryId,Name,Description,PhotoUrl,NormalPhotoUrl,ThumbPhotoUrl,CreatedDate,Status,Remark from Shop_Package ");
            builder.Append(" where PackageId=@PackageId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = PackageId;
            new Maticsoft.Model.Shop.Package.Package();
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
            builder.Append("select count(1) FROM Shop_Package ");
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

        public bool Update(Maticsoft.Model.Shop.Package.Package model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Package set ");
            builder.Append("CategoryId=@CategoryId,");
            builder.Append("Name=@Name,");
            builder.Append("Description=@Description,");
            builder.Append("PhotoUrl=@PhotoUrl,");
            builder.Append("NormalPhotoUrl=@NormalPhotoUrl,");
            builder.Append("ThumbPhotoUrl=@ThumbPhotoUrl,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark");
            builder.Append(" where PackageId=@PackageId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@NormalPhotoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@ThumbPhotoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.PhotoUrl;
            cmdParms[4].Value = model.NormalPhotoUrl;
            cmdParms[5].Value = model.ThumbPhotoUrl;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Status;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.PackageId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

