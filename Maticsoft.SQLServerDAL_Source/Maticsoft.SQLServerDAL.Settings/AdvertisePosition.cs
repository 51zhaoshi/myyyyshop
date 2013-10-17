namespace Maticsoft.SQLServerDAL.Settings
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AdvertisePosition : IAdvertisePosition
    {
        public int Add(Maticsoft.Model.Settings.AdvertisePosition model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO AD_AdvertisePosition(");
            builder.Append("AdvPositionName,ShowType,RepeatColumns,Width,Height,AdvHtml,IsOne,TimeInterval,CreatedDate,CreatedUserID)");
            builder.Append(" VALUES (");
            builder.Append("@AdvPositionName,@ShowType,@RepeatColumns,@Width,@Height,@AdvHtml,@IsOne,@TimeInterval,@CreatedDate,@CreatedUserID)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvPositionName", SqlDbType.NVarChar, 50), new SqlParameter("@ShowType", SqlDbType.Int, 4), new SqlParameter("@RepeatColumns", SqlDbType.Int, 4), new SqlParameter("@Width", SqlDbType.Int, 4), new SqlParameter("@Height", SqlDbType.Int, 4), new SqlParameter("@AdvHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@IsOne", SqlDbType.Bit, 1), new SqlParameter("@TimeInterval", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AdvPositionName;
            cmdParms[1].Value = model.ShowType;
            cmdParms[2].Value = model.RepeatColumns;
            cmdParms[3].Value = model.Width;
            cmdParms[4].Value = model.Height;
            cmdParms[5].Value = model.AdvHtml;
            cmdParms[6].Value = model.IsOne;
            cmdParms[7].Value = model.TimeInterval;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.CreatedUserID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int AdvPositionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM AD_AdvertisePosition ");
            builder.Append(" WHERE AdvPositionId=@AdvPositionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvPositionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdvPositionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AdvPositionIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM AD_AdvertisePosition ");
            builder.Append(" WHERE AdvPositionId in (" + AdvPositionIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int AdvPositionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM AD_AdvertisePosition");
            builder.Append(" WHERE AdvPositionId=@AdvPositionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvPositionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdvPositionId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT AdvPositionId,AdvPositionName,ShowType,RepeatColumns,Width,Height,AdvHtml,IsOne,TimeInterval,CreatedDate,CreatedUserID ");
            builder.Append(" FROM AD_AdvertisePosition ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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
            builder.Append(" AdvPositionId,AdvPositionName,ShowType,RepeatColumns,Width,Height,AdvHtml,IsOne,TimeInterval,CreatedDate,CreatedUserID ");
            builder.Append(" FROM AD_AdvertisePosition ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.AdvPositionId desc");
            }
            builder.Append(")AS Row, T.*  FROM AD_AdvertisePosition T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Settings.AdvertisePosition GetModel(int AdvPositionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 * FROM AD_AdvertisePosition ");
            builder.Append(" WHERE AdvPositionId=@AdvPositionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvPositionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdvPositionId;
            Maticsoft.Model.Settings.AdvertisePosition position = new Maticsoft.Model.Settings.AdvertisePosition();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["AdvPositionId"] != null) && (set.Tables[0].Rows[0]["AdvPositionId"].ToString() != ""))
            {
                position.AdvPositionId = int.Parse(set.Tables[0].Rows[0]["AdvPositionId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AdvPositionName"] != null) && (set.Tables[0].Rows[0]["AdvPositionName"].ToString() != ""))
            {
                position.AdvPositionName = set.Tables[0].Rows[0]["AdvPositionName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ShowType"] != null) && (set.Tables[0].Rows[0]["ShowType"].ToString() != ""))
            {
                position.ShowType = new int?(int.Parse(set.Tables[0].Rows[0]["ShowType"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["RepeatColumns"] != null) && (set.Tables[0].Rows[0]["RepeatColumns"].ToString() != ""))
            {
                position.RepeatColumns = new int?(int.Parse(set.Tables[0].Rows[0]["RepeatColumns"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Width"] != null) && (set.Tables[0].Rows[0]["Width"].ToString() != ""))
            {
                position.Width = new int?(int.Parse(set.Tables[0].Rows[0]["Width"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Height"] != null) && (set.Tables[0].Rows[0]["Height"].ToString() != ""))
            {
                position.Height = new int?(int.Parse(set.Tables[0].Rows[0]["Height"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["AdvHtml"] != null) && (set.Tables[0].Rows[0]["AdvHtml"].ToString() != ""))
            {
                position.AdvHtml = set.Tables[0].Rows[0]["AdvHtml"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IsOne"] != null) && (set.Tables[0].Rows[0]["IsOne"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsOne"].ToString() == "1") || (set.Tables[0].Rows[0]["IsOne"].ToString().ToLower() == "true"))
                {
                    position.IsOne = true;
                }
                else
                {
                    position.IsOne = false;
                }
            }
            if ((set.Tables[0].Rows[0]["TimeInterval"] != null) && (set.Tables[0].Rows[0]["TimeInterval"].ToString() != ""))
            {
                position.TimeInterval = new int?(int.Parse(set.Tables[0].Rows[0]["TimeInterval"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                position.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedUserID"] != null) && (set.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                position.CreatedUserID = new int?(int.Parse(set.Tables[0].Rows[0]["CreatedUserID"].ToString()));
            }
            return position;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM AD_AdvertisePosition ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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

        public bool Update(Maticsoft.Model.Settings.AdvertisePosition model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE AD_AdvertisePosition SET ");
            builder.Append("AdvPositionName=@AdvPositionName,");
            builder.Append("ShowType=@ShowType,");
            builder.Append("RepeatColumns=@RepeatColumns,");
            builder.Append("Width=@Width,");
            builder.Append("Height=@Height,");
            builder.Append("AdvHtml=@AdvHtml,");
            builder.Append("IsOne=@IsOne,");
            builder.Append("TimeInterval=@TimeInterval,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserID=@CreatedUserID");
            builder.Append(" WHERE AdvPositionId=@AdvPositionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvPositionName", SqlDbType.NVarChar, 50), new SqlParameter("@ShowType", SqlDbType.Int, 4), new SqlParameter("@RepeatColumns", SqlDbType.Int, 4), new SqlParameter("@Width", SqlDbType.Int, 4), new SqlParameter("@Height", SqlDbType.Int, 4), new SqlParameter("@AdvHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@IsOne", SqlDbType.Bit, 1), new SqlParameter("@TimeInterval", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@AdvPositionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AdvPositionName;
            cmdParms[1].Value = model.ShowType;
            cmdParms[2].Value = model.RepeatColumns;
            cmdParms[3].Value = model.Width;
            cmdParms[4].Value = model.Height;
            cmdParms[5].Value = model.AdvHtml;
            cmdParms[6].Value = model.IsOne;
            cmdParms[7].Value = model.TimeInterval;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.CreatedUserID;
            cmdParms[10].Value = model.AdvPositionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

