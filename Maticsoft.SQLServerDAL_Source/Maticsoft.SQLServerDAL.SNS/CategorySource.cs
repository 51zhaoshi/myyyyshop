namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CategorySource : ICategorySource
    {
        public int Add(Maticsoft.Model.SNS.CategorySource model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_CategorySource(");
            builder.Append("SourceId,CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,CreatedUserID,CreatedDate,Status,SnsCategoryId)");
            builder.Append(" values (");
            builder.Append("@SourceId,@CategoryId,@Name,@Description,@ParentID,@Path,@Depth,@Sequence,@HasChildren,@IsMenu,@Type,@MenuIsShow,@MenuSequence,@CreatedUserID,@CreatedDate,@Status,@SnsCategoryId)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@SourceId", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 50), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@IsMenu", SqlDbType.Bit, 1), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@MenuIsShow", SqlDbType.Bit, 1), new SqlParameter("@MenuSequence", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), 
                new SqlParameter("@SnsCategoryId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.SourceId;
            cmdParms[1].Value = model.CategoryId;
            cmdParms[2].Value = model.Name;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.ParentID;
            cmdParms[5].Value = model.Path;
            cmdParms[6].Value = model.Depth;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.HasChildren;
            cmdParms[9].Value = model.IsMenu;
            cmdParms[10].Value = model.Type;
            cmdParms[11].Value = model.MenuIsShow;
            cmdParms[12].Value = model.MenuSequence;
            cmdParms[13].Value = model.CreatedUserID;
            cmdParms[14].Value = model.CreatedDate;
            cmdParms[15].Value = model.Status;
            cmdParms[0x10].Value = model.SnsCategoryId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddCategory(Maticsoft.Model.SNS.CategorySource model)
        {
            int id = this.Add(model);
            return ((id > 0) && this.UpdatePathAndDepth(id, model.ParentID));
        }

        public Maticsoft.Model.SNS.CategorySource DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.CategorySource source = new Maticsoft.Model.SNS.CategorySource();
            if (row != null)
            {
                if ((row["SourceId"] != null) && (row["SourceId"].ToString() != ""))
                {
                    source.SourceId = int.Parse(row["SourceId"].ToString());
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    source.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["Name"] != null)
                {
                    source.Name = row["Name"].ToString();
                }
                if (row["Description"] != null)
                {
                    source.Description = row["Description"].ToString();
                }
                if ((row["ParentID"] != null) && (row["ParentID"].ToString() != ""))
                {
                    source.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["Path"] != null)
                {
                    source.Path = row["Path"].ToString();
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    source.Depth = int.Parse(row["Depth"].ToString());
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    source.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["HasChildren"] != null) && (row["HasChildren"].ToString() != ""))
                {
                    if ((row["HasChildren"].ToString() == "1") || (row["HasChildren"].ToString().ToLower() == "true"))
                    {
                        source.HasChildren = true;
                    }
                    else
                    {
                        source.HasChildren = false;
                    }
                }
                if ((row["IsMenu"] != null) && (row["IsMenu"].ToString() != ""))
                {
                    if ((row["IsMenu"].ToString() == "1") || (row["IsMenu"].ToString().ToLower() == "true"))
                    {
                        source.IsMenu = true;
                    }
                    else
                    {
                        source.IsMenu = false;
                    }
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    source.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["MenuIsShow"] != null) && (row["MenuIsShow"].ToString() != ""))
                {
                    if ((row["MenuIsShow"].ToString() == "1") || (row["MenuIsShow"].ToString().ToLower() == "true"))
                    {
                        source.MenuIsShow = true;
                    }
                    else
                    {
                        source.MenuIsShow = false;
                    }
                }
                if ((row["MenuSequence"] != null) && (row["MenuSequence"].ToString() != ""))
                {
                    source.MenuSequence = int.Parse(row["MenuSequence"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    source.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    source.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    source.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["SnsCategoryId"] != null) && (row["SnsCategoryId"].ToString() != ""))
                {
                    source.SnsCategoryId = new int?(int.Parse(row["SnsCategoryId"].ToString()));
                }
            }
            return source;
        }

        public bool Delete(int SourceId, int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_CategorySource ");
            builder.Append(" where SourceId=@SourceId and CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SourceId", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SourceId;
            cmdParms[1].Value = CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteCategory(int CategoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_CategorySource ");
            builder.Append(" where path like (select SNS_CategorySource.Path from SNS_CategorySource where CategoryId=@CategoryId)+'%'");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CategoryID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int SourceId, int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_CategorySource");
            builder.Append(" where SourceId=@SourceId and CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SourceId", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SourceId;
            cmdParms[1].Value = CategoryId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetCategoryList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SNS_CategorySource ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" ORDER BY path ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SourceId,CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,CreatedUserID,CreatedDate,Status,SnsCategoryId ");
            builder.Append(" FROM SNS_CategorySource ");
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
            builder.Append(" SourceId,CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,CreatedUserID,CreatedDate,Status,SnsCategoryId ");
            builder.Append(" FROM SNS_CategorySource ");
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
            builder.Append(")AS Row, T.*  from SNS_CategorySource T ");
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
            return DbHelperSQL.GetMaxID("SourceId", "SNS_CategorySource");
        }

        public Maticsoft.Model.SNS.CategorySource GetModel(int SourceId, int CategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SourceId,CategoryId,Name,Description,ParentID,Path,Depth,Sequence,HasChildren,IsMenu,Type,MenuIsShow,MenuSequence,CreatedUserID,CreatedDate,Status,SnsCategoryId from SNS_CategorySource ");
            builder.Append(" where SourceId=@SourceId and CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SourceId", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SourceId;
            cmdParms[1].Value = CategoryId;
            new Maticsoft.Model.SNS.CategorySource();
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
            builder.Append("select count(1) FROM SNS_CategorySource ");
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

        public bool IsUpdate(long CategoryId, string name, int SourceId, int ParentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_CategorySource ");
            builder.Append(" where SourceId=@SourceId and CategoryId=@CategoryId and (Name<>@Name or  ParentID<>@ParentID)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SourceId", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = SourceId;
            cmdParms[1].Value = CategoryId;
            cmdParms[2].Value = ParentID;
            cmdParms[3].Value = name;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return false;
            }
            if (Globals.SafeInt(single.ToString(), 0) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Update(Maticsoft.Model.SNS.CategorySource model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_CategorySource set ");
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
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status,");
            builder.Append("SnsCategoryId=@SnsCategoryId");
            builder.Append(" where SourceId=@SourceId and CategoryId=@CategoryId ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 50), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), new SqlParameter("@IsMenu", SqlDbType.Bit, 1), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@MenuIsShow", SqlDbType.Bit, 1), new SqlParameter("@MenuSequence", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@SnsCategoryId", SqlDbType.Int, 4), new SqlParameter("@SourceId", SqlDbType.Int, 4), 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4)
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
            cmdParms[11].Value = model.CreatedUserID;
            cmdParms[12].Value = model.CreatedDate;
            cmdParms[13].Value = model.Status;
            cmdParms[14].Value = model.SnsCategoryId;
            cmdParms[15].Value = model.SourceId;
            cmdParms[0x10].Value = model.CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCategory(Maticsoft.Model.SNS.CategorySource model)
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
                builder.Append("update SNS_CategorySource set ");
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
                builder2.Append("update SNS_CategorySource set ");
                builder2.Append("Depth=(select SNS_CategorySource.depth from SNS_CategorySource where CategoryID=@ParentID)+1,");
                builder2.Append("Path=(select SNS_CategorySource.Path from SNS_CategorySource where CategoryID=@ParentID)+@Path,");
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
            builder3.Append("UPDATE SNS_CategorySource set");
            builder3.Append(" Depth=(select SNS_CategorySource.depth from SNS_CategorySource where CategoryID=@CategoryID)+1,");
            builder3.Append(" Path=(select SNS_CategorySource.Path from SNS_CategorySource where CategoryID=@CategoryID)+@Path ");
            builder3.Append("where ParentID=@CategoryID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            para[0].Value = "|" + id;
            para[1].Value = parentid;
            para[2].Value = id;
            item = new CommandInfo(builder3.ToString(), para);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool UpdateSNSCate(int CategoryId, int SNSCateId, bool IsLoop)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_CategorySource set ");
            builder.Append("SnsCategoryId=@SnsCategoryId");
            if (IsLoop)
            {
                builder.Append(" where  path like (select path from SNS_CategorySource where CategoryId=@CategoryId)+'%';");
            }
            else
            {
                builder.Append(" where  CategoryId=@CategoryId ");
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SnsCategoryId", SqlDbType.Int, 4), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SNSCateId;
            cmdParms[1].Value = CategoryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateSNSCateList(string ids, int SNSCateId, bool IsLoop)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_CategorySource set ");
            builder.Append("SnsCategoryId=@SnsCategoryId");
            if (IsLoop)
            {
                string[] strArray = ids.Split(new char[] { ',' });
                int num = 0;
                foreach (string str in strArray)
                {
                    if (num == 0)
                    {
                        builder.Append(" where  path like (select path from SNS_CategorySource where CategoryId =" + str + ")+'%'");
                    }
                    else
                    {
                        builder.Append(" or  path like (select path from SNS_CategorySource where CategoryId =" + str + ")+'%'");
                    }
                    num++;
                }
            }
            else
            {
                builder.Append(" where  CategoryId in (" + ids + ") ");
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SnsCategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SNSCateId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

