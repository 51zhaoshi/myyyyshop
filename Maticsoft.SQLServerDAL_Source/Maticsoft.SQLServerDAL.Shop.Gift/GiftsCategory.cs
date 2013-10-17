namespace Maticsoft.SQLServerDAL.Shop.Gift
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Gift;
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class GiftsCategory : IGiftsCategory
    {
        public int Add(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_GiftsCategory(");
            builder.Append("ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren)");
            builder.Append(" values (");
            builder.Append("@ParentCategoryId,@Name,@Depth,@Path,@DisplaySequence,@Description,@Theme,@HasChildren)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@Theme", SqlDbType.NVarChar, 200), new SqlParameter("@HasChildren", SqlDbType.Bit, 1) };
            cmdParms[0].Value = model.ParentCategoryId;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.Depth;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.DisplaySequence;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.Theme;
            cmdParms[7].Value = model.HasChildren;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            int id = this.Add(model);
            return ((id > 0) && this.UpdatePathAndDepth(id, model.ParentCategoryId.Value));
        }

        public bool Delete(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_GiftsCategory ");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteCategory(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_GiftsCategory ");
            builder.Append(" where path like (select Shop_GiftsCategory.Path from Shop_GiftsCategory where CategoryID=@CategoryID)+'%'");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CategoryIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_GiftsCategory ");
            builder.Append(" where CategoryID in (" + CategoryIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_GiftsCategory");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetCategoryList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren ");
            builder.Append(" FROM Shop_GiftsCategory ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere + " ORDER BY path");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren ");
            builder.Append(" FROM Shop_GiftsCategory ");
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
            builder.Append(" CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren ");
            builder.Append(" FROM Shop_GiftsCategory ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.CategoryID desc");
            }
            builder.Append(")AS Row, T.*  from Shop_GiftsCategory T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("CategoryID", "Shop_GiftsCategory");
        }

        public Maticsoft.Model.Shop.Gift.GiftsCategory GetModel(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryID,ParentCategoryId,Name,Depth,Path,DisplaySequence,Description,Theme,HasChildren from Shop_GiftsCategory ");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            Maticsoft.Model.Shop.Gift.GiftsCategory category = new Maticsoft.Model.Shop.Gift.GiftsCategory();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["CategoryID"] != null) && (set.Tables[0].Rows[0]["CategoryID"].ToString() != ""))
            {
                category.CategoryID = int.Parse(set.Tables[0].Rows[0]["CategoryID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ParentCategoryId"] != null) && (set.Tables[0].Rows[0]["ParentCategoryId"].ToString() != ""))
            {
                category.ParentCategoryId = new int?(int.Parse(set.Tables[0].Rows[0]["ParentCategoryId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Name"] != null) && (set.Tables[0].Rows[0]["Name"].ToString() != ""))
            {
                category.Name = set.Tables[0].Rows[0]["Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Depth"] != null) && (set.Tables[0].Rows[0]["Depth"].ToString() != ""))
            {
                category.Depth = int.Parse(set.Tables[0].Rows[0]["Depth"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Path"] != null) && (set.Tables[0].Rows[0]["Path"].ToString() != ""))
            {
                category.Path = set.Tables[0].Rows[0]["Path"].ToString();
            }
            if ((set.Tables[0].Rows[0]["DisplaySequence"] != null) && (set.Tables[0].Rows[0]["DisplaySequence"].ToString() != ""))
            {
                category.DisplaySequence = int.Parse(set.Tables[0].Rows[0]["DisplaySequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                category.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Theme"] != null) && (set.Tables[0].Rows[0]["Theme"].ToString() != ""))
            {
                category.Theme = set.Tables[0].Rows[0]["Theme"].ToString();
            }
            if ((set.Tables[0].Rows[0]["HasChildren"] != null) && (set.Tables[0].Rows[0]["HasChildren"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["HasChildren"].ToString() == "1") || (set.Tables[0].Rows[0]["HasChildren"].ToString().ToLower() == "true"))
                {
                    category.HasChildren = true;
                    return category;
                }
                category.HasChildren = false;
            }
            return category;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_GiftsCategory ");
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

        public bool SwapSequence(int CategoryId, SwapSequenceIndex zIndex)
        {
            return true;
        }

        public bool Update(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_GiftsCategory set ");
            builder.Append("ParentCategoryId=@ParentCategoryId,");
            builder.Append("Name=@Name,");
            builder.Append("Depth=@Depth,");
            builder.Append("Path=@Path,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Description=@Description,");
            builder.Append("Theme=@Theme,");
            builder.Append("HasChildren=@HasChildren");
            builder.Append(" where CategoryID=@CategoryID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@Theme", SqlDbType.NVarChar, 200), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ParentCategoryId;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.Depth;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.DisplaySequence;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.Theme;
            cmdParms[7].Value = model.HasChildren;
            cmdParms[8].Value = model.CategoryID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCategory(Maticsoft.Model.Shop.Gift.GiftsCategory model)
        {
            return (this.Update(model) && this.UpdatePathAndDepth(model.CategoryID, model.ParentCategoryId.Value));
        }

        public bool UpdatePathAndDepth(int id, int parentid)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo();
            if (parentid == 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update Shop_GiftsCategory set ");
                builder.Append("Depth=@Depth,");
                builder.Append("Path=@Path,");
                builder.Append("HasChildren='false'");
                builder.Append(" where CategoryID=@CategoryID");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
                parameterArray[0].Value = 1;
                parameterArray[1].Value = id;
                parameterArray[2].Value = id;
                item = new CommandInfo(builder.ToString(), parameterArray);
                cmdList.Add(item);
            }
            else
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("update Shop_GiftsCategory set ");
                builder2.Append("Depth=(select Shop_GiftsCategory.depth from Shop_GiftsCategory where CategoryID=@ParentCategoryID)+1,");
                builder2.Append("Path=(select Shop_GiftsCategory.Path from Shop_GiftsCategory where CategoryID=@ParentCategoryID)+@Path,");
                builder2.Append("HasChildren='true'");
                builder2.Append(" where CategoryID=@CategoryID");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@ParentCategoryID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
                parameterArray2[0].Value = "|" + id;
                parameterArray2[1].Value = parentid;
                parameterArray2[2].Value = id;
                item = new CommandInfo(builder2.ToString(), parameterArray2);
                cmdList.Add(item);
            }
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("UPDATE Shop_GiftsCategory set");
            builder3.Append(" Depth=(select Shop_GiftsCategory.depth from Shop_GiftsCategory where CategoryID=@CategoryID)+1,");
            builder3.Append(" Path=(select Shop_GiftsCategory.Path from Shop_GiftsCategory where CategoryID=@CategoryID)+@Path ");
            builder3.Append("where ParentCategoryId=@CategoryID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@ParentCategoryID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            para[0].Value = "|" + id;
            para[1].Value = parentid;
            para[2].Value = id;
            item = new CommandInfo(builder3.ToString(), para);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }
    }
}

