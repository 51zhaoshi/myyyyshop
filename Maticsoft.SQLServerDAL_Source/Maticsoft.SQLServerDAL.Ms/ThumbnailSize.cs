namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ThumbnailSize : IThumbnailSize
    {
        public bool Add(Maticsoft.Model.Ms.ThumbnailSize model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_ThumbnailSize(");
            builder.Append("ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme)");
            builder.Append(" values (");
            builder.Append("@ThumName,@ThumWidth,@ThumHeight,@Type,@Remark,@CloudSizeName,@CloudType,@ThumMode,@IsWatermark,@Theme)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThumName", SqlDbType.NVarChar, 50), new SqlParameter("@ThumWidth", SqlDbType.Int, 4), new SqlParameter("@ThumHeight", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@CloudSizeName", SqlDbType.NVarChar, 50), new SqlParameter("@CloudType", SqlDbType.Int, 4), new SqlParameter("@ThumMode", SqlDbType.Int, 4), new SqlParameter("@IsWatermark", SqlDbType.Bit, 1), new SqlParameter("@Theme", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.ThumName;
            cmdParms[1].Value = model.ThumWidth;
            cmdParms[2].Value = model.ThumHeight;
            cmdParms[3].Value = model.Type;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CloudSizeName;
            cmdParms[6].Value = model.CloudType;
            cmdParms[7].Value = model.ThumMode;
            cmdParms[8].Value = model.IsWatermark;
            cmdParms[9].Value = model.Theme;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Ms.ThumbnailSize DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.ThumbnailSize size = new Maticsoft.Model.Ms.ThumbnailSize();
            if (row != null)
            {
                if (row["ThumName"] != null)
                {
                    size.ThumName = row["ThumName"].ToString();
                }
                if ((row["ThumWidth"] != null) && (row["ThumWidth"].ToString() != ""))
                {
                    size.ThumWidth = int.Parse(row["ThumWidth"].ToString());
                }
                if ((row["ThumHeight"] != null) && (row["ThumHeight"].ToString() != ""))
                {
                    size.ThumHeight = int.Parse(row["ThumHeight"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    size.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Remark"] != null)
                {
                    size.Remark = row["Remark"].ToString();
                }
                if (row["CloudSizeName"] != null)
                {
                    size.CloudSizeName = row["CloudSizeName"].ToString();
                }
                if ((row["CloudType"] != null) && (row["CloudType"].ToString() != ""))
                {
                    size.CloudType = int.Parse(row["CloudType"].ToString());
                }
                if ((row["ThumMode"] != null) && (row["ThumMode"].ToString() != ""))
                {
                    size.ThumMode = int.Parse(row["ThumMode"].ToString());
                }
                if ((row["IsWatermark"] != null) && (row["IsWatermark"].ToString() != ""))
                {
                    if ((row["IsWatermark"].ToString() == "1") || (row["IsWatermark"].ToString().ToLower() == "true"))
                    {
                        size.IsWatermark = true;
                    }
                    else
                    {
                        size.IsWatermark = false;
                    }
                }
                if (row["Theme"] != null)
                {
                    size.Theme = row["Theme"].ToString();
                }
            }
            return size;
        }

        public bool Delete(string ThumName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_ThumbnailSize ");
            builder.Append(" where ThumName=@ThumName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThumName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = ThumName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ThumNamelist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_ThumbnailSize ");
            builder.Append(" where ThumName in (" + ThumNamelist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ThumName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_ThumbnailSize");
            builder.Append(" where ThumName=@ThumName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThumName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = ThumName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme ");
            builder.Append(" FROM Ms_ThumbnailSize ");
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
            builder.Append(" ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme ");
            builder.Append(" FROM Ms_ThumbnailSize ");
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
                builder.Append("order by T.ThumName desc");
            }
            builder.Append(")AS Row, T.*  from Ms_ThumbnailSize T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Ms.ThumbnailSize GetModel(string ThumName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ThumName,ThumWidth,ThumHeight,Type,Remark,CloudSizeName,CloudType,ThumMode,IsWatermark,Theme from Ms_ThumbnailSize ");
            builder.Append(" where ThumName=@ThumName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThumName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = ThumName;
            new Maticsoft.Model.Ms.ThumbnailSize();
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
            builder.Append("select count(1) FROM Ms_ThumbnailSize ");
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

        public bool Update(Maticsoft.Model.Ms.ThumbnailSize model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_ThumbnailSize set ");
            builder.Append("ThumWidth=@ThumWidth,");
            builder.Append("ThumHeight=@ThumHeight,");
            builder.Append("Type=@Type,");
            builder.Append("Remark=@Remark,");
            builder.Append("CloudSizeName=@CloudSizeName,");
            builder.Append("CloudType=@CloudType,");
            builder.Append("ThumMode=@ThumMode,");
            builder.Append("IsWatermark=@IsWatermark,");
            builder.Append("Theme=@Theme");
            builder.Append(" where ThumName=@ThumName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThumWidth", SqlDbType.Int, 4), new SqlParameter("@ThumHeight", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@CloudSizeName", SqlDbType.NVarChar, 50), new SqlParameter("@CloudType", SqlDbType.Int, 4), new SqlParameter("@ThumMode", SqlDbType.Int, 4), new SqlParameter("@IsWatermark", SqlDbType.Bit, 1), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@ThumName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.ThumWidth;
            cmdParms[1].Value = model.ThumHeight;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CloudSizeName;
            cmdParms[5].Value = model.CloudType;
            cmdParms[6].Value = model.ThumMode;
            cmdParms[7].Value = model.IsWatermark;
            cmdParms[8].Value = model.Theme;
            cmdParms[9].Value = model.ThumName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

