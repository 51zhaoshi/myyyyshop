namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class VideoClass : IVideoClass
    {
        public int Add(Maticsoft.Model.CMS.VideoClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_VideoClass(");
            builder.Append("VideoClassName,ParentID,Sequence,Path,Depth)");
            builder.Append(" values (");
            builder.Append("@VideoClassName,@ParentID,@Sequence,@Path,@Depth)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoClassName", SqlDbType.NVarChar, 100), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Depth", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.VideoClassName;
            cmdParms[1].Value = model.ParentID;
            cmdParms[2].Value = model.Sequence;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.Depth;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int AddEx(Maticsoft.Model.CMS.VideoClass model)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@VideoClassName", SqlDbType.NVarChar, 30), new SqlParameter("@Sequence", SqlDbType.Int), new SqlParameter("@ParentID", SqlDbType.Int) };
            parameters[0].Value = model.VideoClassName;
            parameters[1].Value = model.Sequence;
            parameters[2].Value = model.ParentID;
            return DbHelperSQL.RunProcedure("sp_CMS_VideoClass_Create", parameters, out num);
        }

        public bool Delete(int VideoClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_VideoClass ");
            builder.Append(" where VideoClassID=@VideoClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoClassID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int DeleteEx(int VideoClassID)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@VideoClassID", SqlDbType.Int) };
            parameters[0].Value = VideoClassID;
            return DbHelperSQL.RunProcedure("sp_CMS_VideoClass_Delete", parameters, out num);
        }

        public bool DeleteList(string VideoClassIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_VideoClass ");
            builder.Append(" where VideoClassID in (" + VideoClassIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int VideoClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_VideoClass");
            builder.Append(" where VideoClassID=@VideoClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoClassID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth ");
            builder.Append(" FROM CMS_VideoClass ");
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
            builder.Append(" VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth ");
            builder.Append(" FROM CMS_VideoClass ");
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
                builder.Append("order by T.VideoClassID desc");
            }
            builder.Append(")AS Row, T.*  from CMS_VideoClass T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere, string orderBy)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth ");
            builder.Append(" FROM CMS_VideoClass ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(orderBy.Trim()))
            {
                builder.Append("order by " + orderBy);
            }
            else
            {
                builder.Append("order by VideoClassID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("VideoClassID", "CMS_VideoClass");
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_VideoClass");
        }

        public Maticsoft.Model.CMS.VideoClass GetModel(int VideoClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth from CMS_VideoClass ");
            builder.Append(" where VideoClassID=@VideoClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoClassID;
            Maticsoft.Model.CMS.VideoClass class2 = new Maticsoft.Model.CMS.VideoClass();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((ds.Tables[0].Rows[0]["VideoClassID"] != null) && (ds.Tables[0].Rows[0]["VideoClassID"].ToString() != ""))
            {
                class2.VideoClassID = int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["VideoClassName"] != null) && (ds.Tables[0].Rows[0]["VideoClassName"].ToString() != ""))
            {
                class2.VideoClassName = ds.Tables[0].Rows[0]["VideoClassName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["ParentID"] != null) && (ds.Tables[0].Rows[0]["ParentID"].ToString() != ""))
            {
                class2.ParentID = new int?(int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["Sequence"] != null) && (ds.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                class2.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Path"] != null) && (ds.Tables[0].Rows[0]["Path"].ToString() != ""))
            {
                class2.Path = ds.Tables[0].Rows[0]["Path"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Depth"] != null) && (ds.Tables[0].Rows[0]["Depth"].ToString() != ""))
            {
                class2.Depth = int.Parse(ds.Tables[0].Rows[0]["Depth"].ToString());
            }
            return class2;
        }

        public Maticsoft.Model.CMS.VideoClass GetModelByParentID(int ParentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 VideoClassID,VideoClassName,ParentID,Sequence,Path,Depth from CMS_VideoClass ");
            builder.Append(" where VideoClassID=@ParentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ParentID;
            Maticsoft.Model.CMS.VideoClass class2 = new Maticsoft.Model.CMS.VideoClass();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((ds.Tables[0].Rows[0]["VideoClassID"] != null) && (ds.Tables[0].Rows[0]["VideoClassID"].ToString() != ""))
            {
                class2.VideoClassID = int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["VideoClassName"] != null) && (ds.Tables[0].Rows[0]["VideoClassName"].ToString() != ""))
            {
                class2.VideoClassName = ds.Tables[0].Rows[0]["VideoClassName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["ParentID"] != null) && (ds.Tables[0].Rows[0]["ParentID"].ToString() != ""))
            {
                class2.ParentID = new int?(int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["Sequence"] != null) && (ds.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                class2.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Path"] != null) && (ds.Tables[0].Rows[0]["Path"].ToString() != ""))
            {
                class2.Path = ds.Tables[0].Rows[0]["Path"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Depth"] != null) && (ds.Tables[0].Rows[0]["Depth"].ToString() != ""))
            {
                class2.Depth = int.Parse(ds.Tables[0].Rows[0]["Depth"].ToString());
            }
            return class2;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_VideoClass ");
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

        public int SwapCategorySequence(int VideoClassId, SwapSequenceIndex zIndex)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@VideoClassId", SqlDbType.Int), new SqlParameter("@ZIndex", SqlDbType.Int) };
            parameters[0].Value = VideoClassId;
            parameters[1].Value = (int) zIndex;
            return DbHelperSQL.RunProcedure("sp_CMS_SwapVideoClassSequence", parameters, out num);
        }

        public bool Update(Maticsoft.Model.CMS.VideoClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_VideoClass set ");
            builder.Append("VideoClassName=@VideoClassName,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Path=@Path,");
            builder.Append("Depth=@Depth");
            builder.Append(" where VideoClassID=@VideoClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoClassName", SqlDbType.NVarChar, 100), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Path", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@VideoClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.VideoClassName;
            cmdParms[1].Value = model.ParentID;
            cmdParms[2].Value = model.Sequence;
            cmdParms[3].Value = model.Path;
            cmdParms[4].Value = model.Depth;
            cmdParms[5].Value = model.VideoClassID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

