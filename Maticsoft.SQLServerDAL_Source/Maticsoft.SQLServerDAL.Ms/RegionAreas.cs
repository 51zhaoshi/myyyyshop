namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class RegionAreas : IRegionAreas
    {
        public int Add(Maticsoft.Model.Ms.RegionAreas model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_RegionAreas(");
            builder.Append("Name)");
            builder.Append(" values (");
            builder.Append("@Name)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.Name;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Ms.RegionAreas DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.RegionAreas areas = new Maticsoft.Model.Ms.RegionAreas();
            if (row != null)
            {
                if ((row["AreaId"] != null) && (row["AreaId"].ToString() != ""))
                {
                    areas.AreaId = int.Parse(row["AreaId"].ToString());
                }
                if (row["Name"] != null)
                {
                    areas.Name = row["Name"].ToString();
                }
            }
            return areas;
        }

        public bool Delete(int AreaId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_RegionAreas ");
            builder.Append(" where AreaId=@AreaId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AreaId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AreaId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AreaIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_RegionAreas ");
            builder.Append(" where AreaId in (" + AreaIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int AreaId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_RegionAreas");
            builder.Append(" where AreaId=@AreaId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AreaId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AreaId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AreaId,Name ");
            builder.Append(" FROM Ms_RegionAreas ");
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
            builder.Append(" AreaId,Name ");
            builder.Append(" FROM Ms_RegionAreas ");
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
                builder.Append("order by T.AreaId desc");
            }
            builder.Append(")AS Row, T.*  from Ms_RegionAreas T ");
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
            return DbHelperSQL.GetMaxID("AreaId", "Ms_RegionAreas");
        }

        public Maticsoft.Model.Ms.RegionAreas GetModel(int AreaId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AreaId,Name from Ms_RegionAreas ");
            builder.Append(" where AreaId=@AreaId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AreaId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AreaId;
            new Maticsoft.Model.Ms.RegionAreas();
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
            builder.Append("select count(1) FROM Ms_RegionAreas ");
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

        public bool Update(Maticsoft.Model.Ms.RegionAreas model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_RegionAreas set ");
            builder.Append("Name=@Name");
            builder.Append(" where AreaId=@AreaId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@AreaId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.AreaId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

