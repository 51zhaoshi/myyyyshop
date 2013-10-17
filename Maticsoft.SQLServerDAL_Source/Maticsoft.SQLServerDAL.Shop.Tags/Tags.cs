namespace Maticsoft.SQLServerDAL.Shop.Tags
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Tags;
    using Maticsoft.Model.Shop.Tags;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Tags : ITags
    {
        public int Add(Maticsoft.Model.Shop.Tags.Tags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_Tags(");
            builder.Append("TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords)");
            builder.Append(" VALUES (");
            builder.Append("@TagCategoryId,@TagName,@IsRecommand,@Status,@Meta_Title,@Meta_Description,@Meta_Keywords)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagCategoryId", SqlDbType.Int, 4), new SqlParameter("@TagName", SqlDbType.NVarChar, 50), new SqlParameter("@IsRecommand", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8) };
            cmdParms[0].Value = model.TagCategoryId;
            cmdParms[1].Value = model.TagName;
            cmdParms[2].Value = model.IsRecommand;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.Meta_Title;
            cmdParms[5].Value = model.Meta_Description;
            cmdParms[6].Value = model.Meta_Keywords;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Tags ");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TagIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Tags ");
            builder.Append(" WHERE TagID in (" + TagIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Tags");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TagID,TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords ");
            builder.Append(" FROM Shop_Tags ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
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
            builder.Append(" TagID,TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords ");
            builder.Append(" FROM Shop_Tags ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
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
                builder.Append("ORDER BY T.TagID desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_Tags T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" TagT.*,TagTT.CategoryName ");
            builder.Append(" from Shop_Tags TagT Left join Shop_TagCategories TagTT on TagTT.ID=TagT.TagCategoryId ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            else
            {
                builder.Append(" order by TagID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Tags.Tags GetModel(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 TagID,TagCategoryId,TagName,IsRecommand,Status,Meta_Title,Meta_Description,Meta_Keywords FROM Shop_Tags ");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            Maticsoft.Model.Shop.Tags.Tags tags = new Maticsoft.Model.Shop.Tags.Tags();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["TagID"] != null) && (set.Tables[0].Rows[0]["TagID"].ToString() != ""))
            {
                tags.TagID = int.Parse(set.Tables[0].Rows[0]["TagID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["TagCategoryId"] != null) && (set.Tables[0].Rows[0]["TagCategoryId"].ToString() != ""))
            {
                tags.TagCategoryId = int.Parse(set.Tables[0].Rows[0]["TagCategoryId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["TagName"] != null) && (set.Tables[0].Rows[0]["TagName"].ToString() != ""))
            {
                tags.TagName = set.Tables[0].Rows[0]["TagName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IsRecommand"] != null) && (set.Tables[0].Rows[0]["IsRecommand"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsRecommand"].ToString() == "1") || (set.Tables[0].Rows[0]["IsRecommand"].ToString().ToLower() == "true"))
                {
                    tags.IsRecommand = true;
                }
                else
                {
                    tags.IsRecommand = false;
                }
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                tags.Status = int.Parse(set.Tables[0].Rows[0]["Status"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Meta_Title"] != null) && (set.Tables[0].Rows[0]["Meta_Title"].ToString() != ""))
            {
                tags.Meta_Title = set.Tables[0].Rows[0]["Meta_Title"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Description"] != null) && (set.Tables[0].Rows[0]["Meta_Description"].ToString() != ""))
            {
                tags.Meta_Description = set.Tables[0].Rows[0]["Meta_Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Keywords"] != null) && (set.Tables[0].Rows[0]["Meta_Keywords"].ToString() != ""))
            {
                tags.Meta_Keywords = set.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            }
            return tags;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Tags ");
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

        public bool Update(Maticsoft.Model.Shop.Tags.Tags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_Tags SET ");
            builder.Append("TagCategoryId=@TagCategoryId,");
            builder.Append("TagName=@TagName,");
            builder.Append("IsRecommand=@IsRecommand,");
            builder.Append("Status=@Status,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagCategoryId", SqlDbType.Int, 4), new SqlParameter("@TagName", SqlDbType.NVarChar, 50), new SqlParameter("@IsRecommand", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TagCategoryId;
            cmdParms[1].Value = model.TagName;
            cmdParms[2].Value = model.IsRecommand;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.Meta_Title;
            cmdParms[5].Value = model.Meta_Description;
            cmdParms[6].Value = model.Meta_Keywords;
            cmdParms[7].Value = model.TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateIsRecommand(string IsRecommand, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Tags set ");
            builder.AppendFormat(" IsRecommand={0} ", "'" + IsRecommand + "'");
            builder.AppendFormat(" where TagID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Tags set ");
            builder.AppendFormat(" Status={0} ", Status);
            builder.AppendFormat(" where TagID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

