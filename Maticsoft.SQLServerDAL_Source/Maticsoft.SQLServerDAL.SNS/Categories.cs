namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Categories : ICategories
    {
        public int Add(Maticsoft.Model.SNS.Categories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Categories(");
            builder.Append("Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords)");
            builder.Append(" values (");
            builder.Append("@Name,@Description,@ParentID,@Path,@Depth,@Sequence,@HasChildren,@IsMenu,@Type,@MenuIsShow,@MenuSequence,@FontColor,@CreatedUserID,@CreatedDate,@Status,@SeoUrl,@Meta_Title,@Meta_Description,@Meta_Keywords)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 50), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@IsMenu", SqlDbType.Bit, 1), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@MenuIsShow", SqlDbType.Bit, 1), new SqlParameter("@MenuSequence", SqlDbType.Int, 4), new SqlParameter("@FontColor", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), 
                new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.ParentID;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.Depth;
            cmdParms[5].Value = model.Sequence;
            cmdParms[6].Value = model.HasChildren;
            cmdParms[7].Value = model.IsMenu;
            cmdParms[8].Value = model.Type;
            cmdParms[9].Value = model.MenuIsShow;
            cmdParms[10].Value = model.MenuSequence;
            cmdParms[11].Value = model.FontColor;
            cmdParms[12].Value = model.CreatedUserID;
            cmdParms[13].Value = model.CreatedDate;
            cmdParms[14].Value = model.Status;
            cmdParms[15].Value = model.SeoUrl;
            cmdParms[0x10].Value = model.Meta_Title;
            cmdParms[0x11].Value = model.Meta_Description;
            cmdParms[0x12].Value = model.Meta_Keywords;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddCategories(Maticsoft.Model.SNS.Categories model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasChildren", SqlDbType.Bit), new SqlParameter("@ParentCategoryId", SqlDbType.Int, 4), new SqlParameter("@MenuSequence", SqlDbType.Int, 4), new SqlParameter("@MenuIsShow", SqlDbType.Bit), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@IsMenu", SqlDbType.Bit), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@FontColor", SqlDbType.NVarChar), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8) };
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.HasChildren;
            parameters[3].Value = model.ParentID;
            parameters[4].Value = model.MenuSequence;
            parameters[5].Value = model.MenuIsShow;
            parameters[6].Value = model.CreatedUserID;
            parameters[7].Value = model.IsMenu;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.FontColor;
            parameters[11].Value = model.SeoUrl;
            parameters[12].Value = model.Meta_Title;
            parameters[13].Value = model.Meta_Description;
            parameters[14].Value = model.Meta_Keywords;
            return (DbHelperSQL.RunProcedure("sp_SNS_Category_Create", parameters, out rowsAffected) > 0);
        }

        public bool AddCategory(Maticsoft.Model.SNS.Categories model)
        {
            int id = this.Add(model);
            return ((id > 0) && this.UpdatePathAndDepth(id, model.ParentID));
        }

        public Maticsoft.Model.SNS.Categories DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Categories categories = new Maticsoft.Model.SNS.Categories();
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
                if (row["Description"] != null)
                {
                    categories.Description = row["Description"].ToString();
                }
                if ((row["ParentID"] != null) && (row["ParentID"].ToString() != ""))
                {
                    categories.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["Path"] != null)
                {
                    categories.Path = row["Path"].ToString();
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    categories.Depth = int.Parse(row["Depth"].ToString());
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    categories.Sequence = int.Parse(row["Sequence"].ToString());
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
                if ((row["IsMenu"] != null) && (row["IsMenu"].ToString() != ""))
                {
                    if ((row["IsMenu"].ToString() == "1") || (row["IsMenu"].ToString().ToLower() == "true"))
                    {
                        categories.IsMenu = true;
                    }
                    else
                    {
                        categories.IsMenu = false;
                    }
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    categories.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["MenuIsShow"] != null) && (row["MenuIsShow"].ToString() != ""))
                {
                    if ((row["MenuIsShow"].ToString() == "1") || (row["MenuIsShow"].ToString().ToLower() == "true"))
                    {
                        categories.MenuIsShow = true;
                    }
                    else
                    {
                        categories.MenuIsShow = false;
                    }
                }
                if ((row["MenuSequence"] != null) && (row["MenuSequence"].ToString() != ""))
                {
                    categories.MenuSequence = int.Parse(row["MenuSequence"].ToString());
                }
                if (row["FontColor"] != null)
                {
                    categories.FontColor = row["FontColor"].ToString();
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    categories.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    categories.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    categories.Status = int.Parse(row["Status"].ToString());
                }
                if (row["SeoUrl"] != null)
                {
                    categories.SeoUrl = row["SeoUrl"].ToString();
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
            }
            return categories;
        }

        public bool Delete(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Categories ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteCategory(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Categories ");
            builder.Append(" where path like (select SNS_Categories.Path from SNS_Categories where CategoryId=@CategoryId)+'%'");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Categories ");
            builder.Append(" where CategoryId in (" + CategoryIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Categories");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetCategoryList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SNS_Categories ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append("  ORDER BY  Sequence ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords ");
            builder.Append(" FROM SNS_Categories ");
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
            builder.Append(" CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords ");
            builder.Append(" FROM SNS_Categories ");
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
            builder.Append(")AS Row, T.*  from SNS_Categories T ");
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
            return DbHelperSQL.GetMaxID("CategoryId", "SNS_Categories");
        }

        public Maticsoft.Model.SNS.Categories GetModel(int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,FontColor,CreatedUserID,CreatedDate,Status,SeoUrl,Meta_Title,Meta_Description,Meta_Keywords from SNS_Categories ");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryId;
            new Maticsoft.Model.SNS.Categories();
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
            builder.Append("select count(1) FROM SNS_Categories ");
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

        public bool SwapCategorySequence(int CategoryId, EnumHelper.SwapSequenceIndex zIndex)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int), new SqlParameter("@ZIndex", SqlDbType.Int) };
            parameters[0].Value = CategoryId;
            parameters[1].Value = (int) zIndex;
            DbHelperSQL.RunProcedure("sp_SNS_Category_SwapSequence", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Update(Maticsoft.Model.SNS.Categories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Categories set ");
            builder.Append("Name=@Name,");
            builder.Append("Description=@Description,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Path=@Path,");
            builder.Append("Depth=@Depth,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("HasChildren=@HasChildren,");
            builder.Append("IsMenu=@IsMenu,");
            builder.Append("Type=@Type,");
            builder.Append("MenuIsShow=@MenuIsShow,");
            builder.Append("MenuSequence=@MenuSequence,");
            builder.Append("FontColor=@FontColor,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords");
            builder.Append(" where CategoryId=@CategoryId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 50), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@IsMenu", SqlDbType.Bit, 1), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@MenuIsShow", SqlDbType.Bit, 1), new SqlParameter("@MenuSequence", SqlDbType.Int, 4), new SqlParameter("@FontColor", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), 
                new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CategoryId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.ParentID;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.Depth;
            cmdParms[5].Value = model.Sequence;
            cmdParms[6].Value = model.HasChildren;
            cmdParms[7].Value = model.IsMenu;
            cmdParms[8].Value = model.Type;
            cmdParms[9].Value = model.MenuIsShow;
            cmdParms[10].Value = model.MenuSequence;
            cmdParms[11].Value = model.FontColor;
            cmdParms[12].Value = model.CreatedUserID;
            cmdParms[13].Value = model.CreatedDate;
            cmdParms[14].Value = model.Status;
            cmdParms[15].Value = model.SeoUrl;
            cmdParms[0x10].Value = model.Meta_Title;
            cmdParms[0x11].Value = model.Meta_Description;
            cmdParms[0x12].Value = model.Meta_Keywords;
            cmdParms[0x13].Value = model.CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCategory(Maticsoft.Model.SNS.Categories model)
        {
            return (this.Update(model) && this.UpdatePathAndDepth(model.CategoryId, model.ParentID));
        }

        public bool UpdatePathAndDepth(int id, int parentid)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo();
            if (parentid == 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update SNS_Categories set ");
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
                builder2.Append("update SNS_Categories set ");
                builder2.Append("Depth=(select SNS_Categories.depth from SNS_Categories where CategoryID=@ParentID)+1,");
                builder2.Append("Path=(select SNS_Categories.Path from SNS_Categories where CategoryID=@ParentID)+@Path,");
                builder2.Append("HasChildren='true'");
                builder2.Append(" where CategoryID=@CategoryID");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
                parameterArray2[0].Value = "|" + id;
                parameterArray2[1].Value = parentid;
                parameterArray2[2].Value = id;
                item = new CommandInfo(builder2.ToString(), parameterArray2);
                cmdList.Add(item);
            }
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("UPDATE SNS_Categories set");
            builder3.Append(" Depth=(select SNS_Categories.depth from SNS_Categories where CategoryID=@CategoryID)+1,");
            builder3.Append(" Path=(select SNS_Categories.Path from SNS_Categories where CategoryID=@CategoryID)+@Path ");
            builder3.Append("where ParentID=@CategoryID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            para[0].Value = "|" + id;
            para[1].Value = parentid;
            para[2].Value = id;
            item = new CommandInfo(builder3.ToString(), para);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }
    }
}

