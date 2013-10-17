namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ClassType : IClassType
    {
        public bool Add(Maticsoft.Model.CMS.ClassType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_ClassType(");
            builder.Append("ClassTypeName)");
            builder.Append(" values (");
            builder.Append("@ClassTypeName)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassTypeName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.ClassTypeName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public List<Maticsoft.Model.CMS.ClassType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.ClassType> list = new List<Maticsoft.Model.CMS.ClassType>();
            if (DataTableTools.DataTableIsNull(dt))
            {
                return null;
            }
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.ClassType item = new Maticsoft.Model.CMS.ClassType();
                    if (dt.Rows[i]["ClassTypeID"].ToString() != "")
                    {
                        item.ClassTypeID = int.Parse(dt.Rows[i]["ClassTypeID"].ToString());
                    }
                    item.ClassTypeName = dt.Rows[i]["ClassTypeName"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ClassTypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_ClassType ");
            builder.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassTypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassTypeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ClassTypeIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_ClassType ");
            builder.Append(" where ClassTypeID in (" + ClassTypeIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ClassTypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_ClassType");
            builder.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassTypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassTypeID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClassTypeID,ClassTypeName ");
            builder.Append(" FROM CMS_ClassType ");
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
            builder.Append(" ClassTypeID,ClassTypeName ");
            builder.Append(" FROM CMS_ClassType ");
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
                builder.Append(" order by DESC ");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ClassTypeID", "CMS_ClassType");
        }

        public Maticsoft.Model.CMS.ClassType GetModel(int ClassTypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ClassTypeID,ClassTypeName from CMS_ClassType ");
            builder.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassTypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassTypeID;
            Maticsoft.Model.CMS.ClassType type = new Maticsoft.Model.CMS.ClassType();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (ds.Tables[0].Rows[0]["ClassTypeID"].ToString() != "")
            {
                type.ClassTypeID = int.Parse(ds.Tables[0].Rows[0]["ClassTypeID"].ToString());
            }
            if (ds.Tables[0].Rows[0]["ClassTypeName"] != null)
            {
                type.ClassTypeName = ds.Tables[0].Rows[0]["ClassTypeName"].ToString();
            }
            return type;
        }

        public bool Update(Maticsoft.Model.CMS.ClassType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_ClassType set ");
            builder.Append("ClassTypeName=@ClassTypeName");
            builder.Append(" where ClassTypeID=@ClassTypeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassTypeName", SqlDbType.NVarChar, 50), new SqlParameter("@ClassTypeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ClassTypeName;
            cmdParms[1].Value = model.ClassTypeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

