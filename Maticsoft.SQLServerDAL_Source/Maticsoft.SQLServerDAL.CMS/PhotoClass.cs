namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PhotoClass : IPhotoClass
    {
        public void Add(Maticsoft.Model.CMS.PhotoClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_PhotoClass(");
            builder.Append("ClassName,ParentId,Sequence,Path,Depth)");
            builder.Append(" values (");
            builder.Append("@ClassName,@ParentId,@Sequence,@Path,@Depth)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassName", SqlDbType.NVarChar, 200), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@Depth", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ClassName;
            cmdParms[1].Value = model.ParentId;
            cmdParms[2].Value = model.Sequence;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.Depth;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Delete(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_PhotoClass ");
            builder.Append(" where ClassID=@ClassID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_PhotoClass ");
            builder.Append(" where ClassID in (" + ClassIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_PhotoClass");
            builder.Append(" where ClassID=@ClassID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistsByClassName(string ClassName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_PhotoClass");
            builder.Append(" where ClassName=@ClassName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassName", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = ClassName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM CMS_PhotoClass ");
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
            builder.Append(" ClassID,ClassName,ParentId,Sequence,Path,Depth ");
            builder.Append(" FROM CMS_PhotoClass ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ClassID", "CMS_PhotoClass");
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_PhotoClass");
        }

        public Maticsoft.Model.CMS.PhotoClass GetModel(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ClassID,ClassName,ParentId,Sequence,Path,Depth from CMS_PhotoClass ");
            builder.Append(" where ClassID=@ClassID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            Maticsoft.Model.CMS.PhotoClass class2 = new Maticsoft.Model.CMS.PhotoClass();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ClassID"] != null) && (set.Tables[0].Rows[0]["ClassID"].ToString() != ""))
            {
                class2.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ClassName"] != null) && (set.Tables[0].Rows[0]["ClassName"].ToString() != ""))
            {
                class2.ClassName = set.Tables[0].Rows[0]["ClassName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ParentId"] != null) && (set.Tables[0].Rows[0]["ParentId"].ToString() != ""))
            {
                class2.ParentId = new int?(int.Parse(set.Tables[0].Rows[0]["ParentId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Sequence"] != null) && (set.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                class2.Sequence = new int?(int.Parse(set.Tables[0].Rows[0]["Sequence"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Path"] != null) && (set.Tables[0].Rows[0]["Path"].ToString() != ""))
            {
                class2.Path = set.Tables[0].Rows[0]["Path"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Depth"] != null) && (set.Tables[0].Rows[0]["Depth"].ToString() != ""))
            {
                class2.Depth = new int?(int.Parse(set.Tables[0].Rows[0]["Depth"].ToString()));
            }
            return class2;
        }

        public bool Update(Maticsoft.Model.CMS.PhotoClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_PhotoClass set ");
            builder.Append("ClassName=@ClassName,");
            builder.Append("ParentId=@ParentId,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Path=@Path,");
            builder.Append("Depth=@Depth");
            builder.Append(" where ClassID=@ClassID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassName", SqlDbType.NVarChar, 200), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 200), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ClassName;
            cmdParms[1].Value = model.ParentId;
            cmdParms[2].Value = model.Sequence;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.Depth;
            cmdParms[5].Value = model.ClassID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

