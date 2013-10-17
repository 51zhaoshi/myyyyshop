namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Regions : IRegions
    {
        public int Add(Maticsoft.Model.Ms.Regions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_Regions(");
            builder.Append("AreaId,ParentId,RegionName,Spell,SpellShort,DisplaySequence,Path,Depth)");
            builder.Append(" values (");
            builder.Append("@AreaId,@ParentId,@RegionName,@Spell,@SpellShort,@DisplaySequence,@Path,@Depth)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AreaId", SqlDbType.Int, 4), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@RegionName", SqlDbType.NVarChar, 100), new SqlParameter("@Spell", SqlDbType.NVarChar, 50), new SqlParameter("@SpellShort", SqlDbType.NVarChar, 50), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Depth", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AreaId;
            cmdParms[1].Value = model.ParentId;
            cmdParms[2].Value = model.RegionName;
            cmdParms[3].Value = model.Spell;
            cmdParms[4].Value = model.SpellShort;
            cmdParms[5].Value = model.DisplaySequence;
            cmdParms[6].Value = model.Path;
            cmdParms[7].Value = model.Depth;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Ms.Regions DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.Regions regions = new Maticsoft.Model.Ms.Regions();
            if (row != null)
            {
                if ((row["AreaId"] != null) && (row["AreaId"].ToString() != ""))
                {
                    regions.AreaId = new int?(int.Parse(row["AreaId"].ToString()));
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    regions.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if ((row["ParentId"] != null) && (row["ParentId"].ToString() != ""))
                {
                    regions.ParentId = new int?(int.Parse(row["ParentId"].ToString()));
                }
                if (row["RegionName"] != null)
                {
                    regions.RegionName = row["RegionName"].ToString();
                }
                if (row["Spell"] != null)
                {
                    regions.Spell = row["Spell"].ToString();
                }
                if (row["SpellShort"] != null)
                {
                    regions.SpellShort = row["SpellShort"].ToString();
                }
                if ((row["DisplaySequence"] != null) && (row["DisplaySequence"].ToString() != ""))
                {
                    regions.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["Path"] != null)
                {
                    regions.Path = row["Path"].ToString();
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    regions.Depth = int.Parse(row["Depth"].ToString());
                }
            }
            return regions;
        }

        public bool Delete(int RegionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Regions ");
            builder.Append(" where RegionId=@RegionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RegionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RegionIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Regions ");
            builder.Append(" where RegionId in (" + RegionIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int RegionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_Regions");
            builder.Append(" where RegionId=@RegionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RegionId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAllCityList()
        {
            string sQLString = "SELECT * FROM MS_Regions where Depth=2";
            return DbHelperSQL.Query(sQLString);
        }

        public DataSet GetCitys(int parentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT RegionId,RegionName  ");
            builder.Append("FROM Ms_Regions  ");
            builder.Append("WHERE ParentId= " + parentID);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetDistrictByParentId(int iParentId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT *  ");
            builder.Append("FROM Ms_Regions  ");
            builder.Append("WHERE ParentId= " + iParentId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Ms_Regions ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
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
                builder.Append("order by T.RegionId desc");
            }
            builder.Append(")AS Row, T.*  from Ms_Regions T ");
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
            return DbHelperSQL.GetMaxID("RegionId", "Ms_Regions");
        }

        public Maticsoft.Model.Ms.Regions GetModel(int RegionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from MS_Regions ");
            builder.Append(" where RegionId=@RegionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RegionId;
            Maticsoft.Model.Ms.Regions regions = new Maticsoft.Model.Ms.Regions();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["AreaId"] != null) && (set.Tables[0].Rows[0]["AreaId"].ToString() != ""))
            {
                regions.AreaId = new int?(int.Parse(set.Tables[0].Rows[0]["AreaId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["RegionId"] != null) && (set.Tables[0].Rows[0]["RegionId"].ToString() != ""))
            {
                regions.RegionId = int.Parse(set.Tables[0].Rows[0]["RegionId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ParentId"] != null) && (set.Tables[0].Rows[0]["ParentId"].ToString() != ""))
            {
                regions.ParentId = new int?(int.Parse(set.Tables[0].Rows[0]["ParentId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["RegionName"] != null) && (set.Tables[0].Rows[0]["RegionName"].ToString() != ""))
            {
                regions.RegionName = set.Tables[0].Rows[0]["RegionName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Spell"] != null) && (set.Tables[0].Rows[0]["Spell"].ToString() != ""))
            {
                regions.Spell = set.Tables[0].Rows[0]["Spell"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SpellShort"] != null) && (set.Tables[0].Rows[0]["SpellShort"].ToString() != ""))
            {
                regions.SpellShort = set.Tables[0].Rows[0]["SpellShort"].ToString();
            }
            if ((set.Tables[0].Rows[0]["DisplaySequence"] != null) && (set.Tables[0].Rows[0]["DisplaySequence"].ToString() != ""))
            {
                regions.DisplaySequence = int.Parse(set.Tables[0].Rows[0]["DisplaySequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Path"] != null) && (set.Tables[0].Rows[0]["Path"].ToString() != ""))
            {
                regions.Path = set.Tables[0].Rows[0]["Path"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Depth"] != null) && (set.Tables[0].Rows[0]["Depth"].ToString() != ""))
            {
                regions.Depth = int.Parse(set.Tables[0].Rows[0]["Depth"].ToString());
            }
            return regions;
        }

        public DataTable GetParentID(int regionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ParentId  ");
            builder.Append("FROM Ms_Regions  ");
            builder.Append("WHERE RegionId= " + regionID);
            return DbHelperSQL.Query(builder.ToString()).Tables[0];
        }

        public DataSet GetParentIDs(int regID, out int Count)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Region", SqlDbType.Int), new SqlParameter("@Count", SqlDbType.Int) };
            parameters[0].Value = regID;
            parameters[1].Direction = ParameterDirection.Output;
            DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetRegionID", parameters, "ds");
            Count = Convert.ToInt32(parameters[1].Value);
            return set;
        }

        public string GetPath(int regid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Path FROM MS_Regions ");
            builder.Append("WHERE RegionId= " + regid);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return single.ToString();
            }
            return "0.";
        }

        public DataSet GetPrivoceName()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TR.RegionId,RegionName FROM Ms_Regions TR ");
            builder.Append("WHERE AreaId BETWEEN 1 AND 10 ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetPrivoces()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Ms_Regions  ");
            builder.Append("WHERE Depth=1 ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetProvinces()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TR.RegionId,RegionName FROM Ms_Regions TR ");
            builder.Append("WHERE AreaId BETWEEN 1 AND 10 ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Ms_Regions ");
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

        public string GetRegionIDsByAreaId(int areaid)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" SELECT  RegionId ");
            builder2.Append(" FROM Ms_Regions ");
            builder2.Append(" where  AreaId=" + areaid);
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(builder2.ToString()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        builder.Append("'" + reader["RegionId"] + "',");
                    }
                }
            }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }

        public DataSet GetRegionName(string parentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT RegionId,RegionName ");
            builder.Append("FROM Ms_Regions  ");
            builder.Append("WHERE ParentId= " + parentID);
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetRegionNameByRID(int RID)
        {
            string str = this.GetPath(RID) + RID.ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM MS_Regions ");
            builder.Append("WHERE RegionId in (" + str + ")");
            DataSet set = DbHelperSQL.Query(builder.ToString());
            StringBuilder builder2 = new StringBuilder();
            if ((set != null) && (set.Tables.Count > 0))
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    DataRow row = set.Tables[0].Rows[i];
                    string str2 = row["RegionName"].ToString();
                    builder2.Append(str2);
                }
            }
            return builder2.ToString();
        }

        public int GetRegPath(int? regid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Depth FROM MS_Regions ");
            builder.Append("WHERE RegionId= " + regid.Value);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public bool Update(Maticsoft.Model.Ms.Regions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_Regions set ");
            builder.Append("AreaId=@AreaId,");
            builder.Append("ParentId=@ParentId,");
            builder.Append("RegionName=@RegionName,");
            builder.Append("Spell=@Spell,");
            builder.Append("SpellShort=@SpellShort,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Path=@Path,");
            builder.Append("Depth=@Depth");
            builder.Append(" where RegionId=@RegionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AreaId", SqlDbType.Int, 4), new SqlParameter("@ParentId", SqlDbType.Int, 4), new SqlParameter("@RegionName", SqlDbType.NVarChar, 100), new SqlParameter("@Spell", SqlDbType.NVarChar, 50), new SqlParameter("@SpellShort", SqlDbType.NVarChar, 50), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AreaId;
            cmdParms[1].Value = model.ParentId;
            cmdParms[2].Value = model.RegionName;
            cmdParms[3].Value = model.Spell;
            cmdParms[4].Value = model.SpellShort;
            cmdParms[5].Value = model.DisplaySequence;
            cmdParms[6].Value = model.Path;
            cmdParms[7].Value = model.Depth;
            cmdParms[8].Value = model.RegionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateAreaID(string regionlist, int AreaId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE dbo.Ms_Regions SET  AreaId= @AreaId ");
            builder.Append(" where RegionId in (" + regionlist + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AreaId", AreaId) };
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

