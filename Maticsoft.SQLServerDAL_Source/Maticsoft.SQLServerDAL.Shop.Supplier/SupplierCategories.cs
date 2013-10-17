namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SupplierCategories : ISupplierCategories
    {
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SupplierCategories(");
            builder.Append("Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark)");
            builder.Append(" values (");
            builder.Append("@Name,@DisplaySequence,@Meta_Title,@Meta_Description,@Meta_Keywords,@Description,@ParentCategoryId,@Depth,@Path,@ImageUrl,@Theme,@HasChildren,@SeoUrl,@SeoImageAlt,@SeoImageTitle,@CreatedUserId,@SupplierId,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), 
                new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.Meta_Title;
            cmdParms[3].Value = model.Meta_Description;
            cmdParms[4].Value = model.Meta_Keywords;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.ParentCategoryId;
            cmdParms[7].Value = model.Depth;
            cmdParms[8].Value = model.Path;
            cmdParms[9].Value = model.ImageUrl;
            cmdParms[10].Value = model.Theme;
            cmdParms[11].Value = model.HasChildren;
            cmdParms[12].Value = model.SeoUrl;
            cmdParms[13].Value = model.SeoImageAlt;
            cmdParms[14].Value = model.SeoImageTitle;
            cmdParms[15].Value = model.CreatedUserId;
            cmdParms[0x10].Value = model.SupplierId;
            cmdParms[0x11].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierCategories DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierCategories categories = new Maticsoft.Model.Shop.Supplier.SupplierCategories();
            if (row != null)
            {
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    categories.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["Name"] != null)
                {
                    categories.Name = row["Name"].ToString();
                }
                if ((row["DisplaySequence"] != null) && (row["DisplaySequence"].ToString() != ""))
                {
                    categories.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["Meta_Title"] != null)
                {
                    categories.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    categories.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    categories.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["Description"] != null)
                {
                    categories.Description = row["Description"].ToString();
                }
                if ((row["ParentCategoryId"] != null) && (row["ParentCategoryId"].ToString() != ""))
                {
                    categories.ParentCategoryId = new int?(int.Parse(row["ParentCategoryId"].ToString()));
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    categories.Depth = int.Parse(row["Depth"].ToString());
                }
                if (row["Path"] != null)
                {
                    categories.Path = row["Path"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    categories.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["Theme"] != null)
                {
                    categories.Theme = row["Theme"].ToString();
                }
                if ((row["HasChildren"] != null) && (row["HasChildren"].ToString() != ""))
                {
                    if ((row["HasChildren"].ToString() == "1") || (row["HasChildren"].ToString().ToLower() == "true"))
                    {
                        categories.HasChildren = true;
                    }
                    else
                    {
                        categories.HasChildren = false;
                    }
                }
                if (row["SeoUrl"] != null)
                {
                    categories.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["SeoImageAlt"] != null)
                {
                    categories.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if (row["SeoImageTitle"] != null)
                {
                    categories.SeoImageTitle = row["SeoImageTitle"].ToString();
                }
                if ((row["CreatedUserId"] != null) && (row["CreatedUserId"].ToString() != ""))
                {
                    categories.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    categories.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["Remark"] != null)
                {
                    categories.Remark = row["Remark"].ToString();
                }
            }
            return categories;
        }

        public bool Delete(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierCategories ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierCategories ");
            builder.Append(" where CategoryId in (" + CategoryIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SupplierCategories");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryId,Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark ");
            builder.Append(" FROM Shop_SupplierCategories ");
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
            builder.Append(" CategoryId,Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark ");
            builder.Append(" FROM Shop_SupplierCategories ");
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
            builder.Append(")AS Row, T.*  from Shop_SupplierCategories T ");
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
            return DbHelperSQL.GetMaxID("CategoryId", "Shop_SupplierCategories");
        }

        public Maticsoft.Model.Shop.Supplier.SupplierCategories GetModel(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryId,Name,DisplaySequence,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,ImageUrl,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle,CreatedUserId,SupplierId,Remark from Shop_SupplierCategories ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            new Maticsoft.Model.Shop.Supplier.SupplierCategories();
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
            builder.Append("select count(1) FROM Shop_SupplierCategories ");
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

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SupplierCategories set ");
            builder.Append("Name=@Name,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("Description=@Description,");
            builder.Append("ParentCategoryId=@ParentCategoryId,");
            builder.Append("Depth=@Depth,");
            builder.Append("Path=@Path,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("Theme=@Theme,");
            builder.Append("HasChildren=@HasChildren,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("SeoImageAlt=@SeoImageAlt,");
            builder.Append("SeoImageTitle=@SeoImageTitle,");
            builder.Append("CreatedUserId=@CreatedUserId,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("Remark=@Remark");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), 
                new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@CategoryId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.Meta_Title;
            cmdParms[3].Value = model.Meta_Description;
            cmdParms[4].Value = model.Meta_Keywords;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.ParentCategoryId;
            cmdParms[7].Value = model.Depth;
            cmdParms[8].Value = model.Path;
            cmdParms[9].Value = model.ImageUrl;
            cmdParms[10].Value = model.Theme;
            cmdParms[11].Value = model.HasChildren;
            cmdParms[12].Value = model.SeoUrl;
            cmdParms[13].Value = model.SeoImageAlt;
            cmdParms[14].Value = model.SeoImageTitle;
            cmdParms[15].Value = model.CreatedUserId;
            cmdParms[0x10].Value = model.SupplierId;
            cmdParms[0x11].Value = model.Remark;
            cmdParms[0x12].Value = model.CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

