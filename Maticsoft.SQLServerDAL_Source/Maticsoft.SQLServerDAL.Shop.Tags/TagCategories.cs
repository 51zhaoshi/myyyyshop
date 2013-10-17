namespace Maticsoft.SQLServerDAL.Shop.Tags
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Tags;
    using Maticsoft.Model.Shop.Tags;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class TagCategories : ITagCategories
    {
        public int Add(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_TagCategories(");
            builder.Append("CategoryName,ParentCategoryId,DisplaySequence,Depth,Path,Meta_Title,Meta_Description,Meta_Keywords,HasChildren,Status,Remark)");
            builder.Append(" VALUES (");
            builder.Append("@CategoryName,@ParentCategoryId,@DisplaySequence,@Depth,@Path,@Meta_Title,@Meta_Description,@Meta_Keywords,@HasChildren,@Status,@Remark)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryName", SqlDbType.NVarChar, 50), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 500), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.CategoryName;
            cmdParms[1].Value = model.ParentCategoryId;
            cmdParms[2].Value = model.DisplaySequence;
            cmdParms[3].Value = model.Depth;
            cmdParms[4].Value = model.Path;
            cmdParms[5].Value = model.Meta_Title;
            cmdParms[6].Value = model.Meta_Description;
            cmdParms[7].Value = model.Meta_Keywords;
            cmdParms[8].Value = model.HasChildren;
            cmdParms[9].Value = model.Status;
            cmdParms[10].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool CreateCategory(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryName", SqlDbType.NVarChar, 50), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200) };
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.ParentCategoryId;
            parameters[2].Value = model.Meta_Title;
            parameters[3].Value = model.Meta_Description;
            parameters[4].Value = model.Meta_Keywords;
            parameters[5].Value = model.HasChildren;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.Remark;
            DbHelperSQL.RunProcedure("sp_Shop_TagCategories_Create", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_TagCategories ");
            builder.Append(" WHERE ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_TagCategories ");
            builder.Append(" WHERE ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet DeleteTagCategories(int ID, out int Result)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = ID;
            DataSet set = DbHelperSQL.RunProcedure("sp_Shop_TagCategories_Delete", parameters, "tb", out Result);
            if (Result == 1)
            {
                return set;
            }
            return null;
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_TagCategories");
            builder.Append(" WHERE ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,CategoryName,ParentCategoryId,DisplaySequence,Depth,Path,Meta_Title,Meta_Description,Meta_Keywords,HasChildren,Status,Remark ");
            builder.Append(" FROM Shop_TagCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY DisplaySequence  ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" ID,CategoryName,ParentCategoryId,DisplaySequence,Depth,Path,Meta_Title,Meta_Description,Meta_Keywords,HasChildren,Status,Remark ");
            builder.Append(" FROM Shop_TagCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(filedOrder.Trim()))
            {
                builder.Append(" ORDER BY " + filedOrder);
            }
            else
            {
                builder.Append(" ORDER BY ID");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.ID desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_TagCategories T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Tags.TagCategories GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ID,CategoryName,ParentCategoryId,DisplaySequence,Depth,Path,Meta_Title,Meta_Description,Meta_Keywords,HasChildren,Status,Remark FROM Shop_TagCategories ");
            builder.Append(" WHERE ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Model.Shop.Tags.TagCategories categories = new Maticsoft.Model.Shop.Tags.TagCategories();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ID"] != null) && (set.Tables[0].Rows[0]["ID"].ToString() != ""))
            {
                categories.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CategoryName"] != null) && (set.Tables[0].Rows[0]["CategoryName"].ToString() != ""))
            {
                categories.CategoryName = set.Tables[0].Rows[0]["CategoryName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ParentCategoryId"] != null) && (set.Tables[0].Rows[0]["ParentCategoryId"].ToString() != ""))
            {
                categories.ParentCategoryId = new int?(int.Parse(set.Tables[0].Rows[0]["ParentCategoryId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DisplaySequence"] != null) && (set.Tables[0].Rows[0]["DisplaySequence"].ToString() != ""))
            {
                categories.DisplaySequence = int.Parse(set.Tables[0].Rows[0]["DisplaySequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Depth"] != null) && (set.Tables[0].Rows[0]["Depth"].ToString() != ""))
            {
                categories.Depth = int.Parse(set.Tables[0].Rows[0]["Depth"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Path"] != null) && (set.Tables[0].Rows[0]["Path"].ToString() != ""))
            {
                categories.Path = set.Tables[0].Rows[0]["Path"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Title"] != null) && (set.Tables[0].Rows[0]["Meta_Title"].ToString() != ""))
            {
                categories.Meta_Title = set.Tables[0].Rows[0]["Meta_Title"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Description"] != null) && (set.Tables[0].Rows[0]["Meta_Description"].ToString() != ""))
            {
                categories.Meta_Description = set.Tables[0].Rows[0]["Meta_Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Keywords"] != null) && (set.Tables[0].Rows[0]["Meta_Keywords"].ToString() != ""))
            {
                categories.Meta_Keywords = set.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            }
            if ((set.Tables[0].Rows[0]["HasChildren"] != null) && (set.Tables[0].Rows[0]["HasChildren"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["HasChildren"].ToString() == "1") || (set.Tables[0].Rows[0]["HasChildren"].ToString().ToLower() == "true"))
                {
                    categories.HasChildren = true;
                }
                else
                {
                    categories.HasChildren = false;
                }
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                categories.Status = new int?(int.Parse(set.Tables[0].Rows[0]["Status"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Remark"] != null) && (set.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                categories.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            }
            return categories;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_TagCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool TagCategoriesSequence(int ID, SequenceIndex Index)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int), new SqlParameter("@Index", SqlDbType.Int) };
            parameters[0].Value = ID;
            parameters[1].Value = (int) Index;
            DbHelperSQL.RunProcedure("sp_Shop_TagCategories_SwapSequence", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Update(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_TagCategories SET ");
            builder.Append("CategoryName=@CategoryName,");
            builder.Append("ParentCategoryId=@ParentCategoryId,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Depth=@Depth,");
            builder.Append("Path=@Path,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("HasChildren=@HasChildren,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark");
            builder.Append(" WHERE ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryName", SqlDbType.NVarChar, 50), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 500), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CategoryName;
            cmdParms[1].Value = model.ParentCategoryId;
            cmdParms[2].Value = model.DisplaySequence;
            cmdParms[3].Value = model.Depth;
            cmdParms[4].Value = model.Path;
            cmdParms[5].Value = model.Meta_Title;
            cmdParms[6].Value = model.Meta_Description;
            cmdParms[7].Value = model.Meta_Keywords;
            cmdParms[8].Value = model.HasChildren;
            cmdParms[9].Value = model.Status;
            cmdParms[10].Value = model.Remark;
            cmdParms[11].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

