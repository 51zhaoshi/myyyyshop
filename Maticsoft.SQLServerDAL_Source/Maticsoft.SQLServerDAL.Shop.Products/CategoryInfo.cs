namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class CategoryInfo : ICategoryInfo
    {
        public int Add(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Categories(");
            builder.Append("DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle)");
            builder.Append(" values (");
            builder.Append("@DisplaySequence,@Name,@Meta_Title,@Meta_Description,@Meta_Keywords,@Description,@ParentCategoryId,@Depth,@Path,@RewriteName,@SKUPrefix,@AssociatedProductType,@ImageUrl,@Notes1,@Notes2,@Notes3,@Notes4,@Notes5,@Theme,@HasChildren,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@RewriteName", SqlDbType.NVarChar, 50), new SqlParameter("@SKUPrefix", SqlDbType.NVarChar, 10), new SqlParameter("@AssociatedProductType", SqlDbType.Int, 4), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Notes1", SqlDbType.NText), new SqlParameter("@Notes2", SqlDbType.NText), new SqlParameter("@Notes3", SqlDbType.NText), 
                new SqlParameter("@Notes4", SqlDbType.NText), new SqlParameter("@Notes5", SqlDbType.NText), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300)
             };
            cmdParms[0].Value = model.DisplaySequence;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.Meta_Title;
            cmdParms[3].Value = model.Meta_Description;
            cmdParms[4].Value = model.Meta_Keywords;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.ParentCategoryId;
            cmdParms[7].Value = model.Depth;
            cmdParms[8].Value = model.Path;
            cmdParms[9].Value = model.RewriteName;
            cmdParms[10].Value = model.SKUPrefix;
            cmdParms[11].Value = model.AssociatedProductType;
            cmdParms[12].Value = model.ImageUrl;
            cmdParms[13].Value = model.Notes1;
            cmdParms[14].Value = model.Notes2;
            cmdParms[15].Value = model.Notes3;
            cmdParms[0x10].Value = model.Notes4;
            cmdParms[0x11].Value = model.Notes5;
            cmdParms[0x12].Value = model.Theme;
            cmdParms[0x13].Value = model.HasChildren;
            cmdParms[20].Value = model.SeoUrl;
            cmdParms[0x15].Value = model.SeoImageAlt;
            cmdParms[0x16].Value = model.SeoImageTitle;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Products.CategoryInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo info = new Maticsoft.Model.Shop.Products.CategoryInfo();
            if (row != null)
            {
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    info.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["DisplaySequence"] != null) && (row["DisplaySequence"].ToString() != ""))
                {
                    info.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["Name"] != null)
                {
                    info.Name = row["Name"].ToString();
                }
                if (row["Meta_Title"] != null)
                {
                    info.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    info.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    info.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["Description"] != null)
                {
                    info.Description = row["Description"].ToString();
                }
                if ((row["ParentCategoryId"] != null) && (row["ParentCategoryId"].ToString() != ""))
                {
                    info.ParentCategoryId = int.Parse(row["ParentCategoryId"].ToString());
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    info.Depth = int.Parse(row["Depth"].ToString());
                }
                if (row["Path"] != null)
                {
                    info.Path = row["Path"].ToString();
                }
                if (row["RewriteName"] != null)
                {
                    info.RewriteName = row["RewriteName"].ToString();
                }
                if (row["SKUPrefix"] != null)
                {
                    info.SKUPrefix = row["SKUPrefix"].ToString();
                }
                if ((row["AssociatedProductType"] != null) && (row["AssociatedProductType"].ToString() != ""))
                {
                    info.AssociatedProductType = new int?(int.Parse(row["AssociatedProductType"].ToString()));
                }
                if (row["ImageUrl"] != null)
                {
                    info.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["Notes1"] != null)
                {
                    info.Notes1 = row["Notes1"].ToString();
                }
                if (row["Notes2"] != null)
                {
                    info.Notes2 = row["Notes2"].ToString();
                }
                if (row["Notes3"] != null)
                {
                    info.Notes3 = row["Notes3"].ToString();
                }
                if (row["Notes4"] != null)
                {
                    info.Notes4 = row["Notes4"].ToString();
                }
                if (row["Notes5"] != null)
                {
                    info.Notes5 = row["Notes5"].ToString();
                }
                if (row["Theme"] != null)
                {
                    info.Theme = row["Theme"].ToString();
                }
                if ((row["HasChildren"] != null) && (row["HasChildren"].ToString() != ""))
                {
                    if ((row["HasChildren"].ToString() == "1") || (row["HasChildren"].ToString().ToLower() == "true"))
                    {
                        info.HasChildren = true;
                    }
                    else
                    {
                        info.HasChildren = false;
                    }
                }
                if (row["SeoUrl"] != null)
                {
                    info.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["SeoImageAlt"] != null)
                {
                    info.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if (row["SeoImageTitle"] != null)
                {
                    info.SeoImageTitle = row["SeoImageTitle"].ToString();
                }
            }
            return info;
        }

        public bool Delete(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Categories ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet DeleteCategory(int categoryId, out int Result)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = categoryId;
            DataSet set = DbHelperSQL.RunProcedure("sp_Shop_Category_Delete", parameters, "tb", out Result);
            if (Result == 1)
            {
                return set;
            }
            return null;
        }

        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Categories ");
            builder.Append(" where CategoryId in (" + CategoryIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DisplaceCategory(int FromCategoryId, int ToCategoryId)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@FromCategoryId", SqlDbType.Int), new SqlParameter("@ToCategory", SqlDbType.Int) };
            parameters[0].Value = FromCategoryId;
            parameters[1].Value = ToCategoryId;
            DbHelperSQL.RunProcedure("sp_Shop_DisplaceCategory", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Exists(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Categories");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        private CommandInfo GenerateCategory(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Categories(");
            builder.Append("DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle)");
            builder.Append(" values (");
            builder.Append("@DisplaySequence,@Name,@Meta_Title,@Meta_Description,@Meta_Keywords,@Description,@ParentCategoryId,@Depth,@Path,@RewriteName,@SKUPrefix,@AssociatedProductType,@ImageUrl,@Notes1,@Notes2,@Notes3,@Notes4,@Notes5,@Theme,@HasChildren,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@RewriteName", SqlDbType.NVarChar, 50), new SqlParameter("@SKUPrefix", SqlDbType.NVarChar, 10), new SqlParameter("@AssociatedProductType", SqlDbType.Int, 4), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Notes1", SqlDbType.NText), new SqlParameter("@Notes2", SqlDbType.NText), new SqlParameter("@Notes3", SqlDbType.NText), 
                new SqlParameter("@Notes4", SqlDbType.NText), new SqlParameter("@Notes5", SqlDbType.NText), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300)
             };
            para[0].Value = model.DisplaySequence;
            para[1].Value = model.Name;
            para[2].Value = model.Meta_Title;
            para[3].Value = model.Meta_Description;
            para[4].Value = model.Meta_Keywords;
            para[5].Value = model.Description;
            para[6].Value = model.ParentCategoryId;
            para[7].Value = model.Depth;
            para[8].Value = "";
            para[9].Value = model.RewriteName;
            para[10].Value = model.SKUPrefix;
            para[11].Value = model.AssociatedProductType;
            para[12].Value = model.ImageUrl;
            para[13].Value = model.Notes1;
            para[14].Value = model.Notes2;
            para[15].Value = model.Notes3;
            para[0x10].Value = model.Notes4;
            para[0x11].Value = model.Notes5;
            para[0x12].Value = model.Theme;
            para[0x13].Value = model.HasChildren;
            para[20].Value = model.SeoUrl;
            para[0x15].Value = model.SeoImageAlt;
            para[0x16].Value = model.SeoImageTitle;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public DataSet GetCategoryListByPath(string path)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT C.* FROM F_SplitToInt('{0}','|') I ", path);
            builder.Append("LEFT JOIN Shop_Categories C ON I.UnitInt=C.CategoryId ");
            builder.Append("ORDER BY C.Depth ASC ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetDepthByCid(int parentId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT Depth FROM Shop_Categories WHERE CategoryId=@ParentCategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = parentId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryId,DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle ");
            builder.Append(" FROM Shop_Categories ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere, bool IsOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT CategoryId,Name,DisplaySequence,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,Meta_Title,ImageUrl ");
            builder.Append(" FROM Shop_Categories ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (IsOrder)
            {
                builder.Append(" ORDER BY  path ");
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
            builder.Append(" CategoryId,DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle ");
            builder.Append(" FROM Shop_Categories ");
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
            builder.Append(")AS Row, T.*  from Shop_Categories T ");
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
            return DbHelperSQL.GetMaxID("CategoryId", "Shop_Categories");
        }

        public int GetMaxSeqByCid(int parentId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT MAX(DisplaySequence) FROM Shop_Categories WHERE ParentCategoryId=@ParentCategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = parentId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Products.CategoryInfo GetModel(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryId,DisplaySequence,Name,Meta_Title,Meta_Description,Meta_Keywords,Description,ParentCategoryId,Depth,Path,RewriteName,SKUPrefix,AssociatedProductType,ImageUrl,Notes1,Notes2,Notes3,Notes4,Notes5,Theme,HasChildren,SeoUrl,SeoImageAlt,SeoImageTitle from Shop_Categories ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            new Maticsoft.Model.Shop.Products.CategoryInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetNameByPid(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Name ");
            builder.Append(" FROM Shop_Categories sp LEFT JOIN Shop_ProductCategories  spc ON sp.CategoryId=spc.CategoryId ");
            builder.Append(" WHERE spc.ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = productId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public string GetNamePathByPath(string path)
        {
            path = path.Replace("|", ",");
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Shop_Categories ");
            builder.Append("WHERE CategoryId in (" + path + ")");
            DataSet set = DbHelperSQL.Query(builder.ToString());
            string str = "";
            if ((set != null) && (set.Tables.Count > 0))
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    DataRow row = set.Tables[0].Rows[i];
                    if (i == 0)
                    {
                        str = row["Name"].ToString();
                    }
                    else
                    {
                        str = str + "/" + row["Name"].ToString();
                    }
                }
            }
            return str;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_Categories ");
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

        public bool IsExisted(int parentId, string name, int categoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Categories");
            builder.Append(" where ParentCategoryId=@ParentCategoryId and Name=@Name");
            if (categoryId > 0)
            {
                builder.Append("  and CategoryId<>@CategoryId");
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = parentId;
            cmdParms[1].Value = categoryId;
            cmdParms[2].Value = name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool IsExistedProduce(int category)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM Shop_Products ");
            builder.Append(" WHERE CategoryId = @CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int) };
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            return ((single != null) && (Convert.ToInt32(single) > 0));
        }

        public bool SwapCategorySequence(int CategoryId, SwapSequenceIndex zIndex)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int), new SqlParameter("@ZIndex", SqlDbType.Int) };
            parameters[0].Value = CategoryId;
            parameters[1].Value = (int) zIndex;
            DbHelperSQL.RunProcedure("sp_Shop_Category_SwapSequence", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Update(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Categories set ");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Name=@Name,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("Description=@Description,");
            builder.Append("ParentCategoryId=@ParentCategoryId,");
            builder.Append("Depth=@Depth,");
            builder.Append("Path=@Path,");
            builder.Append("RewriteName=@RewriteName,");
            builder.Append("SKUPrefix=@SKUPrefix,");
            builder.Append("AssociatedProductType=@AssociatedProductType,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("Notes1=@Notes1,");
            builder.Append("Notes2=@Notes2,");
            builder.Append("Notes3=@Notes3,");
            builder.Append("Notes4=@Notes4,");
            builder.Append("Notes5=@Notes5,");
            builder.Append("Theme=@Theme,");
            builder.Append("HasChildren=@HasChildren,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("SeoImageAlt=@SeoImageAlt,");
            builder.Append("SeoImageTitle=@SeoImageTitle");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@RewriteName", SqlDbType.NVarChar, 50), new SqlParameter("@SKUPrefix", SqlDbType.NVarChar, 10), new SqlParameter("@AssociatedProductType", SqlDbType.Int, 4), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Notes1", SqlDbType.NText), new SqlParameter("@Notes2", SqlDbType.NText), new SqlParameter("@Notes3", SqlDbType.NText), 
                new SqlParameter("@Notes4", SqlDbType.NText), new SqlParameter("@Notes5", SqlDbType.NText), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@CategoryId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.DisplaySequence;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.Meta_Title;
            cmdParms[3].Value = model.Meta_Description;
            cmdParms[4].Value = model.Meta_Keywords;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.ParentCategoryId;
            cmdParms[7].Value = model.Depth;
            cmdParms[8].Value = model.Path;
            cmdParms[9].Value = model.RewriteName;
            cmdParms[10].Value = model.SKUPrefix;
            cmdParms[11].Value = model.AssociatedProductType;
            cmdParms[12].Value = model.ImageUrl;
            cmdParms[13].Value = model.Notes1;
            cmdParms[14].Value = model.Notes2;
            cmdParms[15].Value = model.Notes3;
            cmdParms[0x10].Value = model.Notes4;
            cmdParms[0x11].Value = model.Notes5;
            cmdParms[0x12].Value = model.Theme;
            cmdParms[0x13].Value = model.HasChildren;
            cmdParms[20].Value = model.SeoUrl;
            cmdParms[0x15].Value = model.SeoImageAlt;
            cmdParms[0x16].Value = model.SeoImageTitle;
            cmdParms[0x17].Value = model.CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCategory(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar), new SqlParameter("@MetaDescription", SqlDbType.NVarChar), new SqlParameter("@MetaKeywords", SqlDbType.NVarChar), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@RewriteName", SqlDbType.NVarChar), new SqlParameter("@SKUPrefix", SqlDbType.NVarChar), new SqlParameter("@AssociatedProductType", SqlDbType.Int), new SqlParameter("@Meta_Title", SqlDbType.NVarChar), new SqlParameter("@ImageUrl", SqlDbType.NVarChar), new SqlParameter("@CategoryId", SqlDbType.Int) };
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Meta_Description;
            parameters[2].Value = model.Meta_Keywords;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.RewriteName;
            parameters[5].Value = model.SKUPrefix;
            parameters[6].Value = model.AssociatedProductType;
            parameters[7].Value = model.Meta_Title;
            parameters[8].Value = model.ImageUrl;
            parameters[9].Value = model.CategoryId;
            DbHelperSQL.RunProcedure("sp_cc_Category_Update", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool UpdateDepthAndPath(int Cid, int Depth, string Path)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Categories set Path=@Path, Depth=@Depth WHERE CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = Path;
            cmdParms[1].Value = Depth;
            cmdParms[2].Value = Cid;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateHasChild(int cid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Categories set HasChildren=1 WHERE CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = cid;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdatePath(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Categories set Path=@Path WHERE CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Path;
            cmdParms[1].Value = model.CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateSeqByCid(int Seq, int Cid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Categories set DisplaySequence=@DisplaySequence WHERE CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = Seq;
            cmdParms[1].Value = Cid;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

