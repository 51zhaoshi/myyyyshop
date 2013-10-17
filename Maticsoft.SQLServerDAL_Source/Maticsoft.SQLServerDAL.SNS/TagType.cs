namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TagType : ITagType
    {
        public int Add(Maticsoft.Model.SNS.TagType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_TagType(");
            builder.Append("TypeName,Remark,Cid,Status)");
            builder.Append(" values (");
            builder.Append("@TypeName,@Remark,@Cid,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@Cid", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.Remark;
            cmdParms[2].Value = model.Cid;
            cmdParms[3].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.TagType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.TagType type = new Maticsoft.Model.SNS.TagType();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    type.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["TypeName"] != null) && (row["TypeName"].ToString() != ""))
                {
                    type.TypeName = row["TypeName"].ToString();
                }
                if ((row["Remark"] != null) && (row["Remark"].ToString() != ""))
                {
                    type.Remark = row["Remark"].ToString();
                }
                if ((row["Cid"] != null) && (row["Cid"].ToString() != ""))
                {
                    type.Cid = new int?(int.Parse(row["Cid"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    type.Status = new int?(int.Parse(row["Status"].ToString()));
                }
            }
            return type;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_TagType ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_TagType ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TypeName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_TagType");
            builder.Append(" where TypeName=@TypeName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TypeName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAllListEX()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TypeName+'_'+Remark as Name,TypeName,Cid,Status ");
            builder.Append(" from SNS_TagType ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TypeName,Remark,Cid,Status ");
            builder.Append(" from SNS_TagType ");
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
            builder.Append(" ID,TypeName,Remark,Cid,Status ");
            builder.Append(" from SNS_TagType ");
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
                builder.Append(" order by ID DESC");
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
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_TagType T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.TagType GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,TypeName,Remark,Cid,Status from SNS_TagType ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.TagType();
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
            builder.Append("select count(1) from SNS_TagType ");
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

        public bool RelationSNSCate(int tagTypeId, int SNSCategoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_TagType set ");
            builder.Append("Cid=@Cid");
            builder.Append(" where  ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Cid", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = SNSCategoryId;
            cmdParms[1].Value = tagTypeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(Maticsoft.Model.SNS.TagType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_TagType set ");
            builder.Append("TypeName=@TypeName,");
            builder.Append("Remark=@Remark,");
            builder.Append("Cid=@Cid,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@Cid", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.Remark;
            cmdParms[2].Value = model.Cid;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

