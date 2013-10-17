namespace Maticsoft.SQLServerDAL.Settings
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Advertisement : IAdvertisement
    {
        public bool Add(Maticsoft.Model.Settings.Advertisement model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO AD_Advertisement(");
            builder.Append("AdvertisementName,AdvPositionId,ContentType,FileUrl,AlternateText,NavigateUrl,AdvHtml,Impressions,CreatedDate,CreatedUserID,State,StartDate,EndDate,DayMaxPV,DayMaxIP,CPMPrice,AutoStop,Sequence,EnterpriseID)");
            builder.Append(" VALUES (");
            builder.Append("@AdvertisementName,@AdvPositionId,@ContentType,@FileUrl,@AlternateText,@NavigateUrl,@AdvHtml,@Impressions,@CreatedDate,@CreatedUserID,@State,@StartDate,@EndDate,@DayMaxPV,@DayMaxIP,@CPMPrice,@AutoStop,@Sequence,@EnterpriseID)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@AdvertisementName", SqlDbType.NVarChar, 50), new SqlParameter("@AdvPositionId", SqlDbType.Int, 4), new SqlParameter("@ContentType", SqlDbType.Int, 4), new SqlParameter("@FileUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AlternateText", SqlDbType.NVarChar, 200), new SqlParameter("@NavigateUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AdvHtml", SqlDbType.NVarChar), new SqlParameter("@Impressions", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@DayMaxPV", SqlDbType.Int, 4), new SqlParameter("@DayMaxIP", SqlDbType.Int, 4), new SqlParameter("@CPMPrice", SqlDbType.Money, 8), 
                new SqlParameter("@AutoStop", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@EnterpriseID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.AdvertisementName;
            cmdParms[1].Value = model.AdvPositionId;
            cmdParms[2].Value = model.ContentType;
            cmdParms[3].Value = model.FileUrl;
            cmdParms[4].Value = model.AlternateText;
            cmdParms[5].Value = model.NavigateUrl;
            cmdParms[6].Value = model.AdvHtml;
            cmdParms[7].Value = model.Impressions;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.CreatedUserID;
            cmdParms[10].Value = model.State;
            cmdParms[11].Value = model.StartDate;
            cmdParms[12].Value = model.EndDate;
            cmdParms[13].Value = model.DayMaxPV;
            cmdParms[14].Value = model.DayMaxIP;
            cmdParms[15].Value = model.CPMPrice;
            cmdParms[0x10].Value = model.AutoStop;
            cmdParms[0x11].Value = model.Sequence;
            cmdParms[0x12].Value = model.EnterpriseID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public List<Maticsoft.Model.Settings.Advertisement> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.Advertisement> list = new List<Maticsoft.Model.Settings.Advertisement>();
            if (DataTableTools.DataTableIsNull(dt))
            {
                return null;
            }
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Settings.Advertisement item = new Maticsoft.Model.Settings.Advertisement {
                        Row = i + 1
                    };
                    if ((dt.Rows[i]["AdvertisementId"] != null) && (dt.Rows[i]["AdvertisementId"].ToString() != ""))
                    {
                        item.AdvertisementId = int.Parse(dt.Rows[i]["AdvertisementId"].ToString());
                    }
                    if ((dt.Rows[i]["AdvertisementName"] != null) && (dt.Rows[i]["AdvertisementName"].ToString() != ""))
                    {
                        item.AdvertisementName = dt.Rows[i]["AdvertisementName"].ToString();
                    }
                    if ((dt.Rows[i]["AdvPositionId"] != null) && (dt.Rows[i]["AdvPositionId"].ToString() != ""))
                    {
                        item.AdvPositionId = new int?(int.Parse(dt.Rows[i]["AdvPositionId"].ToString()));
                    }
                    if ((dt.Rows[i]["ContentType"] != null) && (dt.Rows[i]["ContentType"].ToString() != ""))
                    {
                        item.ContentType = new int?(int.Parse(dt.Rows[i]["ContentType"].ToString()));
                    }
                    if ((dt.Rows[i]["FileUrl"] != null) && (dt.Rows[i]["FileUrl"].ToString() != ""))
                    {
                        item.FileUrl = dt.Rows[i]["FileUrl"].ToString();
                    }
                    if ((dt.Rows[i]["AlternateText"] != null) && (dt.Rows[i]["AlternateText"].ToString() != ""))
                    {
                        item.AlternateText = dt.Rows[i]["AlternateText"].ToString();
                    }
                    if ((dt.Rows[i]["NavigateUrl"] != null) && (dt.Rows[i]["NavigateUrl"].ToString() != ""))
                    {
                        item.NavigateUrl = dt.Rows[i]["NavigateUrl"].ToString();
                    }
                    if ((dt.Rows[i]["AdvHtml"] != null) && (dt.Rows[i]["AdvHtml"].ToString() != ""))
                    {
                        item.AdvHtml = dt.Rows[i]["AdvHtml"].ToString();
                    }
                    if ((dt.Rows[i]["Impressions"] != null) && (dt.Rows[i]["Impressions"].ToString() != ""))
                    {
                        item.Impressions = new int?(int.Parse(dt.Rows[i]["Impressions"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedUserID"] != null) && (dt.Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        item.CreatedUserID = new int?(int.Parse(dt.Rows[i]["CreatedUserID"].ToString()));
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = new int?(int.Parse(dt.Rows[i]["State"].ToString()));
                    }
                    if ((dt.Rows[i]["StartDate"] != null) && (dt.Rows[i]["StartDate"].ToString() != ""))
                    {
                        item.StartDate = new DateTime?(DateTime.Parse(dt.Rows[i]["StartDate"].ToString()));
                    }
                    if ((dt.Rows[i]["EndDate"] != null) && (dt.Rows[i]["EndDate"].ToString() != ""))
                    {
                        item.EndDate = new DateTime?(DateTime.Parse(dt.Rows[i]["EndDate"].ToString()));
                    }
                    if ((dt.Rows[i]["DayMaxPV"] != null) && (dt.Rows[i]["DayMaxPV"].ToString() != ""))
                    {
                        item.DayMaxPV = new int?(int.Parse(dt.Rows[i]["DayMaxPV"].ToString()));
                    }
                    if ((dt.Rows[i]["DayMaxIP"] != null) && (dt.Rows[i]["DayMaxIP"].ToString() != ""))
                    {
                        item.DayMaxIP = new int?(int.Parse(dt.Rows[i]["DayMaxIP"].ToString()));
                    }
                    if ((dt.Rows[i]["CPMPrice"] != null) && (dt.Rows[i]["CPMPrice"].ToString() != ""))
                    {
                        item.CPMPrice = new decimal?(decimal.Parse(dt.Rows[i]["CPMPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["AutoStop"] != null) && (dt.Rows[i]["AutoStop"].ToString() != ""))
                    {
                        item.AutoStop = new int?(int.Parse(dt.Rows[i]["AutoStop"].ToString()));
                    }
                    if ((dt.Rows[i]["Sequence"] != null) && (dt.Rows[i]["Sequence"].ToString() != ""))
                    {
                        item.Sequence = new int?(int.Parse(dt.Rows[i]["Sequence"].ToString()));
                    }
                    if ((dt.Rows[i]["EnterpriseID"] != null) && (dt.Rows[i]["EnterpriseID"].ToString() != ""))
                    {
                        item.EnterpriseID = new int?(int.Parse(dt.Rows[i]["EnterpriseID"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int AdvertisementId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM AD_Advertisement ");
            builder.Append(" WHERE AdvertisementId=@AdvertisementId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvertisementId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdvertisementId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AdvertisementIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM AD_Advertisement ");
            builder.Append(" WHERE AdvertisementId in (" + AdvertisementIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetContentType(int AdvPositionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT DISTINCT ContentType ");
            builder.Append("FROM AD_Advertisement ");
            builder.AppendFormat("WHERE  AdvPositionId= {0}", AdvPositionId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetDefindCode(int AdvPositionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM AD_AdvertisePosition");
            builder.AppendFormat(" WHERE  AdvPositionId = {0}", AdvPositionId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * ");
            builder.Append(" FROM AD_Advertisement ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int? Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top.HasValue && (Top.Value > 0))
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" AdvertisementId,AdvertisementName,AdvPositionId,ContentType,FileUrl,AlternateText,NavigateUrl,AdvHtml,Impressions,CreatedDate,CreatedUserID,State,StartDate,EndDate,DayMaxPV,DayMaxIP,CPMPrice,AutoStop,Sequence,EnterpriseID ");
            builder.Append(" FROM AD_Advertisement ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(filedOrder.Trim()))
            {
                builder.Append(" ORDER BY " + filedOrder);
            }
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
                builder.Append("ORDER BY T.AdvertisementId desc");
            }
            builder.Append(")AS Row, T.*  FROM AD_Advertisement T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "AD_Advertisement");
        }

        public Maticsoft.Model.Settings.Advertisement GetModel(int AdvertisementId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 * FROM AD_Advertisement ");
            builder.Append(" WHERE AdvertisementId=@AdvertisementId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvertisementId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdvertisementId;
            Maticsoft.Model.Settings.Advertisement advertisement = new Maticsoft.Model.Settings.Advertisement();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["AdvertisementId"] != null) && (set.Tables[0].Rows[0]["AdvertisementId"].ToString() != ""))
            {
                advertisement.AdvertisementId = int.Parse(set.Tables[0].Rows[0]["AdvertisementId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AdvertisementName"] != null) && (set.Tables[0].Rows[0]["AdvertisementName"].ToString() != ""))
            {
                advertisement.AdvertisementName = set.Tables[0].Rows[0]["AdvertisementName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AdvPositionId"] != null) && (set.Tables[0].Rows[0]["AdvPositionId"].ToString() != ""))
            {
                advertisement.AdvPositionId = new int?(int.Parse(set.Tables[0].Rows[0]["AdvPositionId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ContentType"] != null) && (set.Tables[0].Rows[0]["ContentType"].ToString() != ""))
            {
                advertisement.ContentType = new int?(int.Parse(set.Tables[0].Rows[0]["ContentType"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["FileUrl"] != null) && (set.Tables[0].Rows[0]["FileUrl"].ToString() != ""))
            {
                advertisement.FileUrl = set.Tables[0].Rows[0]["FileUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AlternateText"] != null) && (set.Tables[0].Rows[0]["AlternateText"].ToString() != ""))
            {
                advertisement.AlternateText = set.Tables[0].Rows[0]["AlternateText"].ToString();
            }
            if ((set.Tables[0].Rows[0]["NavigateUrl"] != null) && (set.Tables[0].Rows[0]["NavigateUrl"].ToString() != ""))
            {
                advertisement.NavigateUrl = set.Tables[0].Rows[0]["NavigateUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AdvHtml"] != null) && (set.Tables[0].Rows[0]["AdvHtml"].ToString() != ""))
            {
                advertisement.AdvHtml = set.Tables[0].Rows[0]["AdvHtml"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Impressions"] != null) && (set.Tables[0].Rows[0]["Impressions"].ToString() != ""))
            {
                advertisement.Impressions = new int?(int.Parse(set.Tables[0].Rows[0]["Impressions"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                advertisement.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedUserID"] != null) && (set.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                advertisement.CreatedUserID = new int?(int.Parse(set.Tables[0].Rows[0]["CreatedUserID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["State"] != null) && (set.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                advertisement.State = new int?(int.Parse(set.Tables[0].Rows[0]["State"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["StartDate"] != null) && (set.Tables[0].Rows[0]["StartDate"].ToString() != ""))
            {
                advertisement.StartDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["StartDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["EndDate"] != null) && (set.Tables[0].Rows[0]["EndDate"].ToString() != ""))
            {
                advertisement.EndDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["EndDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DayMaxPV"] != null) && (set.Tables[0].Rows[0]["DayMaxPV"].ToString() != ""))
            {
                advertisement.DayMaxPV = new int?(int.Parse(set.Tables[0].Rows[0]["DayMaxPV"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DayMaxIP"] != null) && (set.Tables[0].Rows[0]["DayMaxIP"].ToString() != ""))
            {
                advertisement.DayMaxIP = new int?(int.Parse(set.Tables[0].Rows[0]["DayMaxIP"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CPMPrice"] != null) && (set.Tables[0].Rows[0]["CPMPrice"].ToString() != ""))
            {
                advertisement.CPMPrice = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["CPMPrice"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["AutoStop"] != null) && (set.Tables[0].Rows[0]["AutoStop"].ToString() != ""))
            {
                advertisement.AutoStop = new int?(int.Parse(set.Tables[0].Rows[0]["AutoStop"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Sequence"] != null) && (set.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                advertisement.Sequence = new int?(int.Parse(set.Tables[0].Rows[0]["Sequence"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["EnterpriseID"] != null) && (set.Tables[0].Rows[0]["EnterpriseID"].ToString() != ""))
            {
                advertisement.EnterpriseID = new int?(int.Parse(set.Tables[0].Rows[0]["EnterpriseID"].ToString()));
            }
            return advertisement;
        }

        public Maticsoft.Model.Settings.Advertisement GetModelByAdvPositionId(int AdvPositionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 * FROM AD_Advertisement ");
            builder.Append(" WHERE AdvPositionId=@AdvPositionId AND State=1");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AdvPositionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AdvPositionId;
            Maticsoft.Model.Settings.Advertisement advertisement = new Maticsoft.Model.Settings.Advertisement();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["AdvertisementId"] != null) && (set.Tables[0].Rows[0]["AdvertisementId"].ToString() != ""))
            {
                advertisement.AdvertisementId = int.Parse(set.Tables[0].Rows[0]["AdvertisementId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AdvertisementName"] != null) && (set.Tables[0].Rows[0]["AdvertisementName"].ToString() != ""))
            {
                advertisement.AdvertisementName = set.Tables[0].Rows[0]["AdvertisementName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AdvPositionId"] != null) && (set.Tables[0].Rows[0]["AdvPositionId"].ToString() != ""))
            {
                advertisement.AdvPositionId = new int?(int.Parse(set.Tables[0].Rows[0]["AdvPositionId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ContentType"] != null) && (set.Tables[0].Rows[0]["ContentType"].ToString() != ""))
            {
                advertisement.ContentType = new int?(int.Parse(set.Tables[0].Rows[0]["ContentType"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["FileUrl"] != null) && (set.Tables[0].Rows[0]["FileUrl"].ToString() != ""))
            {
                advertisement.FileUrl = set.Tables[0].Rows[0]["FileUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AlternateText"] != null) && (set.Tables[0].Rows[0]["AlternateText"].ToString() != ""))
            {
                advertisement.AlternateText = set.Tables[0].Rows[0]["AlternateText"].ToString();
            }
            if ((set.Tables[0].Rows[0]["NavigateUrl"] != null) && (set.Tables[0].Rows[0]["NavigateUrl"].ToString() != ""))
            {
                advertisement.NavigateUrl = set.Tables[0].Rows[0]["NavigateUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AdvHtml"] != null) && (set.Tables[0].Rows[0]["AdvHtml"].ToString() != ""))
            {
                advertisement.AdvHtml = set.Tables[0].Rows[0]["AdvHtml"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Impressions"] != null) && (set.Tables[0].Rows[0]["Impressions"].ToString() != ""))
            {
                advertisement.Impressions = new int?(int.Parse(set.Tables[0].Rows[0]["Impressions"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                advertisement.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedUserID"] != null) && (set.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                advertisement.CreatedUserID = new int?(int.Parse(set.Tables[0].Rows[0]["CreatedUserID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["State"] != null) && (set.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                advertisement.State = new int?(int.Parse(set.Tables[0].Rows[0]["State"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["StartDate"] != null) && (set.Tables[0].Rows[0]["StartDate"].ToString() != ""))
            {
                advertisement.StartDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["StartDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["EndDate"] != null) && (set.Tables[0].Rows[0]["EndDate"].ToString() != ""))
            {
                advertisement.EndDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["EndDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DayMaxPV"] != null) && (set.Tables[0].Rows[0]["DayMaxPV"].ToString() != ""))
            {
                advertisement.DayMaxPV = new int?(int.Parse(set.Tables[0].Rows[0]["DayMaxPV"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DayMaxIP"] != null) && (set.Tables[0].Rows[0]["DayMaxIP"].ToString() != ""))
            {
                advertisement.DayMaxIP = new int?(int.Parse(set.Tables[0].Rows[0]["DayMaxIP"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CPMPrice"] != null) && (set.Tables[0].Rows[0]["CPMPrice"].ToString() != ""))
            {
                advertisement.CPMPrice = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["CPMPrice"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["AutoStop"] != null) && (set.Tables[0].Rows[0]["AutoStop"].ToString() != ""))
            {
                advertisement.AutoStop = new int?(int.Parse(set.Tables[0].Rows[0]["AutoStop"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Sequence"] != null) && (set.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                advertisement.Sequence = new int?(int.Parse(set.Tables[0].Rows[0]["Sequence"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["EnterpriseID"] != null) && (set.Tables[0].Rows[0]["EnterpriseID"].ToString() != ""))
            {
                advertisement.EnterpriseID = new int?(int.Parse(set.Tables[0].Rows[0]["EnterpriseID"].ToString()));
            }
            return advertisement;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM AD_Advertisement ");
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

        public DataSet GetTransitionImg(int Aid, int ContentType, int? Num)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  ");
            if (Num.HasValue)
            {
                builder.Append(" TOP " + Num.Value);
            }
            builder.Append(" * FROM AD_Advertisement ADV ");
            builder.Append("LEFT JOIN AD_AdvertisePosition ADP ON ADV.AdvPositionId = ADP.AdvPositionId ");
            builder.AppendFormat("WHERE   ADP.AdvPositionId ={0} AND ContentType={1} AND State=1 ", Aid, ContentType);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int IsExist(int AdvPositionId, int contentType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) ");
            builder.Append("FROM AD_Advertisement  ");
            builder.AppendFormat("WHERE AdvPositionId={0} AND State=1  AND ContentType={1}", AdvPositionId, contentType);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public DataSet SelectInfoByContentType(int ContentType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 *");
            builder.Append("FROM AD_Advertisement  ");
            builder.AppendFormat("WHERE ContentType={0} AND State=1 ", ContentType);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Maticsoft.Model.Settings.Advertisement model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE AD_Advertisement SET ");
            builder.Append("AdvertisementName=@AdvertisementName,");
            builder.Append("AdvPositionId=@AdvPositionId,");
            builder.Append("ContentType=@ContentType,");
            builder.Append("FileUrl=@FileUrl,");
            builder.Append("AlternateText=@AlternateText,");
            builder.Append("NavigateUrl=@NavigateUrl,");
            builder.Append("AdvHtml=@AdvHtml,");
            builder.Append("Impressions=@Impressions,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("State=@State,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("DayMaxPV=@DayMaxPV,");
            builder.Append("DayMaxIP=@DayMaxIP,");
            builder.Append("CPMPrice=@CPMPrice,");
            builder.Append("AutoStop=@AutoStop,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("EnterpriseID=@EnterpriseID");
            builder.Append(" WHERE AdvertisementId=@AdvertisementId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@AdvertisementName", SqlDbType.NVarChar, 50), new SqlParameter("@AdvPositionId", SqlDbType.Int, 4), new SqlParameter("@ContentType", SqlDbType.Int, 4), new SqlParameter("@FileUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AlternateText", SqlDbType.NVarChar, 200), new SqlParameter("@NavigateUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AdvHtml", SqlDbType.NVarChar), new SqlParameter("@Impressions", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@DayMaxPV", SqlDbType.Int, 4), new SqlParameter("@DayMaxIP", SqlDbType.Int, 4), new SqlParameter("@CPMPrice", SqlDbType.Money, 8), 
                new SqlParameter("@AutoStop", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@EnterpriseID", SqlDbType.Int, 4), new SqlParameter("@AdvertisementId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.AdvertisementName;
            cmdParms[1].Value = model.AdvPositionId;
            cmdParms[2].Value = model.ContentType;
            cmdParms[3].Value = model.FileUrl;
            cmdParms[4].Value = model.AlternateText;
            cmdParms[5].Value = model.NavigateUrl;
            cmdParms[6].Value = model.AdvHtml;
            cmdParms[7].Value = model.Impressions;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.CreatedUserID;
            cmdParms[10].Value = model.State;
            cmdParms[11].Value = model.StartDate;
            cmdParms[12].Value = model.EndDate;
            cmdParms[13].Value = model.DayMaxPV;
            cmdParms[14].Value = model.DayMaxIP;
            cmdParms[15].Value = model.CPMPrice;
            cmdParms[0x10].Value = model.AutoStop;
            cmdParms[0x11].Value = model.Sequence;
            cmdParms[0x12].Value = model.EnterpriseID;
            cmdParms[0x13].Value = model.AdvertisementId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

