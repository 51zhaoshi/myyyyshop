namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.Common.Video;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ContentClass : IContentClass
    {
        public int Add(Maticsoft.Model.CMS.ContentClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_ContentClass(");
            builder.Append("ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar)");
            builder.Append(" values (");
            builder.Append("@ClassName,@ClassIndex,@Sequence,@ParentId,@State,@AllowSubclass,@AllowAddContent,@ImageUrl,@Description,@Keywords,@ClassTypeID,@ClassModel,@PageModelName,@CreatedDate,@CreatedUserID,@Path,@Depth,@Remark,@Meta_Title,@Meta_Description,@Meta_Keywords,@SeoUrl,@SeoImageAlt,@SeoImageTitle,@IndexChar)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ClassName", SqlDbType.NVarChar, 50), new SqlParameter("@ClassIndex", SqlDbType.NVarChar, 50), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@AllowSubclass", SqlDbType.Bit, 1), new SqlParameter("@AllowAddContent", SqlDbType.Bit, 1), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@ClassTypeID", SqlDbType.Int, 4), new SqlParameter("@ClassModel", SqlDbType.SmallInt, 2), new SqlParameter("@PageModelName", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@IndexChar", SqlDbType.NVarChar, 200)
             };
            cmdParms[0].Value = model.ClassName;
            cmdParms[1].Value = model.ClassIndex;
            cmdParms[2].Value = model.Sequence;
            cmdParms[3].Value = model.ParentId;
            cmdParms[4].Value = model.State;
            cmdParms[5].Value = model.AllowSubclass;
            cmdParms[6].Value = model.AllowAddContent;
            cmdParms[7].Value = model.ImageUrl;
            cmdParms[8].Value = model.Description;
            cmdParms[9].Value = model.Keywords;
            cmdParms[10].Value = model.ClassTypeID;
            cmdParms[11].Value = model.ClassModel;
            cmdParms[12].Value = model.PageModelName;
            cmdParms[13].Value = model.CreatedDate;
            cmdParms[14].Value = model.CreatedUserID;
            cmdParms[15].Value = model.Path;
            cmdParms[0x10].Value = model.Depth;
            cmdParms[0x11].Value = model.Remark;
            cmdParms[0x12].Value = model.Meta_Title;
            cmdParms[0x13].Value = model.Meta_Description;
            cmdParms[20].Value = model.Meta_Keywords;
            cmdParms[0x15].Value = model.SeoUrl;
            cmdParms[0x16].Value = model.SeoImageAlt;
            cmdParms[0x17].Value = model.SeoImageTitle;
            cmdParms[0x18].Value = model.IndexChar;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddExt(Maticsoft.Model.CMS.ContentClass model)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@ClassName", SqlDbType.NVarChar, 50), new SqlParameter("@ClassIndex", SqlDbType.NVarChar, 50), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@AllowSubclass", SqlDbType.Bit, 1), new SqlParameter("@AllowAddContent", SqlDbType.Bit, 1), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@ClassTypeID", SqlDbType.Int, 4), new SqlParameter("@ClassModel", SqlDbType.SmallInt, 2), new SqlParameter("@PageModelName", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@IndexChar", SqlDbType.NVarChar, 200)
             };
            parameters[0].Value = model.ClassName;
            parameters[1].Value = model.ClassIndex;
            parameters[2].Value = model.Sequence;
            parameters[3].Value = model.ParentId;
            parameters[4].Value = model.State;
            parameters[5].Value = model.AllowSubclass;
            parameters[6].Value = model.AllowAddContent;
            parameters[7].Value = model.ImageUrl;
            parameters[8].Value = model.Description;
            parameters[9].Value = model.Keywords;
            parameters[10].Value = model.ClassTypeID;
            parameters[11].Value = model.ClassModel;
            parameters[12].Value = model.PageModelName;
            parameters[13].Value = model.CreatedUserID;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.Meta_Title;
            parameters[0x10].Value = model.Meta_Description;
            parameters[0x11].Value = model.Meta_Keywords;
            parameters[0x12].Value = model.SeoUrl;
            parameters[0x13].Value = model.SeoImageAlt;
            parameters[20].Value = model.SeoImageTitle;
            parameters[0x15].Value = model.IndexChar;
            DbHelperSQL.RunProcedure("sp_CMS_ContentClass_Create", parameters, out num);
            return (num > 0);
        }

        public Maticsoft.Model.CMS.ContentClass DataRowToModel(DataRow row)
        {
            Maticsoft.Model.CMS.ContentClass class2 = new Maticsoft.Model.CMS.ContentClass();
            if (row != null)
            {
                if ((row["ClassID"] != null) && (row["ClassID"].ToString() != ""))
                {
                    class2.ClassID = int.Parse(row["ClassID"].ToString());
                }
                if (row["ClassName"] != null)
                {
                    class2.ClassName = row["ClassName"].ToString();
                }
                if (row["ClassIndex"] != null)
                {
                    class2.ClassIndex = row["ClassIndex"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    class2.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["ParentId"] != null) && (row["ParentId"].ToString() != ""))
                {
                    class2.ParentId = new int?(int.Parse(row["ParentId"].ToString()));
                }
                if ((row["State"] != null) && (row["State"].ToString() != ""))
                {
                    class2.State = int.Parse(row["State"].ToString());
                }
                if ((row["AllowSubclass"] != null) && (row["AllowSubclass"].ToString() != ""))
                {
                    if ((row["AllowSubclass"].ToString() == "1") || (row["AllowSubclass"].ToString().ToLower() == "true"))
                    {
                        class2.AllowSubclass = true;
                    }
                    else
                    {
                        class2.AllowSubclass = false;
                    }
                }
                if ((row["AllowAddContent"] != null) && (row["AllowAddContent"].ToString() != ""))
                {
                    if ((row["AllowAddContent"].ToString() == "1") || (row["AllowAddContent"].ToString().ToLower() == "true"))
                    {
                        class2.AllowAddContent = true;
                    }
                    else
                    {
                        class2.AllowAddContent = false;
                    }
                }
                if (row["ImageUrl"] != null)
                {
                    class2.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["Description"] != null)
                {
                    class2.Description = row["Description"].ToString();
                }
                if (row["Keywords"] != null)
                {
                    class2.Keywords = row["Keywords"].ToString();
                }
                if ((row["ClassTypeID"] != null) && (row["ClassTypeID"].ToString() != ""))
                {
                    class2.ClassTypeID = int.Parse(row["ClassTypeID"].ToString());
                }
                if ((row["ClassModel"] != null) && (row["ClassModel"].ToString() != ""))
                {
                    class2.ClassModel = int.Parse(row["ClassModel"].ToString());
                }
                if (row["PageModelName"] != null)
                {
                    class2.PageModelName = row["PageModelName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    class2.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    class2.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["Path"] != null)
                {
                    class2.Path = row["Path"].ToString();
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    class2.Depth = new int?(int.Parse(row["Depth"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    class2.Remark = row["Remark"].ToString();
                }
                if (row["Meta_Title"] != null)
                {
                    class2.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    class2.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    class2.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["SeoUrl"] != null)
                {
                    class2.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["SeoImageAlt"] != null)
                {
                    class2.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if (row["SeoImageTitle"] != null)
                {
                    class2.SeoImageTitle = row["SeoImageTitle"].ToString();
                }
                if (row["IndexChar"] != null)
                {
                    class2.IndexChar = row["IndexChar"].ToString();
                }
            }
            return class2;
        }

        public bool Delete(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_ContentClass ");
            builder.Append(" where ClassID=@ClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteCategory(int categoryId)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int), new SqlParameter("@Status", SqlDbType.Int) };
            parameters[0].Value = categoryId;
            parameters[1].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure("sp_CMS_Category_Delete", parameters, out rowsAffected);
            return (((int) parameters[1].Value) > 0);
        }

        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_ContentClass ");
            builder.Append(" where ClassID in (" + ClassIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_ContentClass");
            builder.Append(" where ClassID=@ClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClassID,ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar ");
            builder.Append(" FROM CMS_ContentClass ");
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
            builder.Append(" ClassID,ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar ");
            builder.Append(" FROM CMS_ContentClass ");
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
                builder.Append("order by T.ClassID desc");
            }
            builder.Append(")AS Row, T.*  from CMS_ContentClass T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByView(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT *  FROM View_ContentClass ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByView(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" * ");
            builder.Append(" FROM View_ContentClass ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" ORDER BY " + filedOrder);
            }
            else
            {
                builder.Append(" ORDER BY ClassID ASC ");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ClassID", "CMS_ContentClass");
        }

        public Maticsoft.Model.CMS.ContentClass GetModel(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ClassID,ClassName,ClassIndex,Sequence,ParentId,State,AllowSubclass,AllowAddContent,ImageUrl,Description,Keywords,ClassTypeID,ClassModel,PageModelName,CreatedDate,CreatedUserID,Path,Depth,Remark,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,IndexChar from CMS_ContentClass ");
            builder.Append(" where ClassID=@ClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            new Maticsoft.Model.CMS.ContentClass();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public string GetNamePathByPath(string path)
        {
            path = path.Replace("|", ",");
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM CMS_ContentClass ");
            builder.Append("WHERE ClassID in (" + path + ")");
            DataSet set = DbHelperSQL.Query(builder.ToString());
            string str = "";
            if ((set != null) && (set.Tables.Count > 0))
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    DataRow row = set.Tables[0].Rows[i];
                    if (i == 0)
                    {
                        str = row["ClassName"].ToString();
                    }
                    else
                    {
                        str = str + "/" + row["ClassName"].ToString();
                    }
                }
            }
            return str;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_ContentClass ");
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

        public DataSet GetTreeList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM CMS_ContentClass ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY ParentId,Sequence ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int SwapCategorySequence(int ContentClassId, SwapSequenceIndex zIndex)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int), new SqlParameter("@ZIndex", SqlDbType.Int) };
            parameters[0].Value = ContentClassId;
            parameters[1].Value = (int) zIndex;
            return DbHelperSQL.RunProcedure("sp_CMS_SwapContentClassSequence", parameters, out num);
        }

        public bool Update(Maticsoft.Model.CMS.ContentClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_ContentClass set ");
            builder.Append("ClassName=@ClassName,");
            builder.Append("ClassIndex=@ClassIndex,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("ParentId=@ParentId,");
            builder.Append("State=@State,");
            builder.Append("AllowSubclass=@AllowSubclass,");
            builder.Append("AllowAddContent=@AllowAddContent,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("Description=@Description,");
            builder.Append("Keywords=@Keywords,");
            builder.Append("ClassTypeID=@ClassTypeID,");
            builder.Append("ClassModel=@ClassModel,");
            builder.Append("PageModelName=@PageModelName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("Path=@Path,");
            builder.Append("Depth=@Depth,");
            builder.Append("Remark=@Remark,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("SeoImageAlt=@SeoImageAlt,");
            builder.Append("SeoImageTitle=@SeoImageTitle,");
            builder.Append("IndexChar=@IndexChar");
            builder.Append(" where ClassID=@ClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ClassName", SqlDbType.NVarChar, 50), new SqlParameter("@ClassIndex", SqlDbType.NVarChar, 50), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@AllowSubclass", SqlDbType.Bit, 1), new SqlParameter("@AllowAddContent", SqlDbType.Bit, 1), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@ClassTypeID", SqlDbType.Int, 4), new SqlParameter("@ClassModel", SqlDbType.SmallInt, 2), new SqlParameter("@PageModelName", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@IndexChar", SqlDbType.NVarChar, 200), new SqlParameter("@ClassID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.ClassName;
            cmdParms[1].Value = model.ClassIndex;
            cmdParms[2].Value = model.Sequence;
            cmdParms[3].Value = model.ParentId;
            cmdParms[4].Value = model.State;
            cmdParms[5].Value = model.AllowSubclass;
            cmdParms[6].Value = model.AllowAddContent;
            cmdParms[7].Value = model.ImageUrl;
            cmdParms[8].Value = model.Description;
            cmdParms[9].Value = model.Keywords;
            cmdParms[10].Value = model.ClassTypeID;
            cmdParms[11].Value = model.ClassModel;
            cmdParms[12].Value = model.PageModelName;
            cmdParms[13].Value = model.CreatedDate;
            cmdParms[14].Value = model.CreatedUserID;
            cmdParms[15].Value = model.Path;
            cmdParms[0x10].Value = model.Depth;
            cmdParms[0x11].Value = model.Remark;
            cmdParms[0x12].Value = model.Meta_Title;
            cmdParms[0x13].Value = model.Meta_Description;
            cmdParms[20].Value = model.Meta_Keywords;
            cmdParms[0x15].Value = model.SeoUrl;
            cmdParms[0x16].Value = model.SeoImageAlt;
            cmdParms[0x17].Value = model.SeoImageTitle;
            cmdParms[0x18].Value = model.IndexChar;
            cmdParms[0x19].Value = model.ClassID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE CMS_ContentClass set " + strWhere);
            builder.Append(" WHERE ClassID IN(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

