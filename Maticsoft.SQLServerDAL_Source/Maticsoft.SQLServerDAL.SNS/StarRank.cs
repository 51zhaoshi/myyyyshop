namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class StarRank : IStarRank
    {
        public int Add(Maticsoft.Model.SNS.StarRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_StarRank(");
            builder.Append("UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType)");
            builder.Append(" values (");
            builder.Append("@UserId,@UserGravatar,@TypeId,@NickName,@IsRecommend,@Sequence,@TimeUnit,@StartDate,@EndDate,@CreatedDate,@RankDate,@Status,@RankType)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserGravatar", SqlDbType.NVarChar, 200), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@TimeUnit", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@RankDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@RankType", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.UserGravatar;
            cmdParms[2].Value = model.TypeId;
            cmdParms[3].Value = model.NickName;
            cmdParms[4].Value = model.IsRecommend;
            cmdParms[5].Value = model.Sequence;
            cmdParms[6].Value = model.TimeUnit;
            cmdParms[7].Value = model.StartDate;
            cmdParms[8].Value = model.EndDate;
            cmdParms[9].Value = model.CreatedDate;
            cmdParms[10].Value = model.RankDate;
            cmdParms[11].Value = model.Status;
            cmdParms[12].Value = model.RankType;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddCollocationRank()
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SNS_StarRank ");
            builder.Append(" where typeId=3  ");
            SqlParameter[] para = new SqlParameter[0];
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into SNS_StarRank(UserId,UserGravatar,TypeId,NickName,Status,RankType,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate) ");
            builder2.Append(" select top 10 UserID,UserGravatar,TypeID,NickName,Status,'0','false',ROW_NUMBER() OVER( ORDER BY ID),'-1',GETDATE(),GETDATE(),GETDATE(),GETDATE() from   ");
            builder2.Append(" (select top 10 *,PhotoCount=(select COUNT(1) from SNS_Photos where CreatedUserID=star.UserID and Type=1) from  SNS_Star star where Status=1 and TypeID=3 order by PhotoCount desc) temp  ");
            SqlParameter[] parameterArray2 = new SqlParameter[0];
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool AddHotStarRank()
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SNS_StarRank ");
            builder.Append(" where typeId=1  ");
            SqlParameter[] para = new SqlParameter[0];
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into SNS_StarRank(UserId,UserGravatar,TypeId,NickName,Status,RankType,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate) ");
            builder2.Append(" select top 10 UserID,UserGravatar,TypeID,NickName,Status,'0','false',ROW_NUMBER() OVER( ORDER BY ID),'-1',GETDATE(),GETDATE(),GETDATE(),GETDATE() from  ");
            builder2.Append(" (select top 10 *,FansCount=(select FansCount from Accounts_UsersExp where UserID=star.UserID) from  SNS_Star star where Status=1 and TypeID=1 order by FansCount desc) temp  ");
            SqlParameter[] parameterArray2 = new SqlParameter[0];
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool AddShareProductRank()
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SNS_StarRank ");
            builder.Append(" where typeId=2  ");
            SqlParameter[] para = new SqlParameter[0];
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into SNS_StarRank(UserId,UserGravatar,TypeId,NickName,Status,RankType,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate) ");
            builder2.Append(" select top 10 UserID,UserGravatar,TypeID,NickName,Status,'0','false',ROW_NUMBER() OVER( ORDER BY ID),'-1',GETDATE(),GETDATE(),GETDATE(),GETDATE() from   ");
            builder2.Append(" (select top 10 *,PhotoCount=(select COUNT(1) from SNS_Photos where CreatedUserID=star.UserID and Type=0) from  SNS_Star star where Status=1 and TypeID=2 order by PhotoCount desc) temp  ");
            SqlParameter[] parameterArray2 = new SqlParameter[0];
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public Maticsoft.Model.SNS.StarRank DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.StarRank rank = new Maticsoft.Model.SNS.StarRank();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    rank.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    rank.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserGravatar"] != null)
                {
                    rank.UserGravatar = row["UserGravatar"].ToString();
                }
                if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                {
                    rank.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if (row["NickName"] != null)
                {
                    rank.NickName = row["NickName"].ToString();
                }
                if ((row["IsRecommend"] != null) && (row["IsRecommend"].ToString() != ""))
                {
                    if ((row["IsRecommend"].ToString() == "1") || (row["IsRecommend"].ToString().ToLower() == "true"))
                    {
                        rank.IsRecommend = true;
                    }
                    else
                    {
                        rank.IsRecommend = false;
                    }
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    rank.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["TimeUnit"] != null) && (row["TimeUnit"].ToString() != ""))
                {
                    rank.TimeUnit = int.Parse(row["TimeUnit"].ToString());
                }
                if ((row["StartDate"] != null) && (row["StartDate"].ToString() != ""))
                {
                    rank.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if ((row["EndDate"] != null) && (row["EndDate"].ToString() != ""))
                {
                    rank.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    rank.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["RankDate"] != null) && (row["RankDate"].ToString() != ""))
                {
                    rank.RankDate = DateTime.Parse(row["RankDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    rank.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["RankType"] != null) && (row["RankType"].ToString() != ""))
                {
                    rank.RankType = int.Parse(row["RankType"].ToString());
                }
            }
            return rank;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_StarRank ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_StarRank ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_StarRank");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType ");
            builder.Append(" FROM SNS_StarRank ");
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
            builder.Append(" ID,UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType ");
            builder.Append(" FROM SNS_StarRank ");
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_StarRank T ");
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
            return DbHelperSQL.GetMaxID("ID", "SNS_StarRank");
        }

        public Maticsoft.Model.SNS.StarRank GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,UserId,UserGravatar,TypeId,NickName,IsRecommend,Sequence,TimeUnit,StartDate,EndDate,CreatedDate,RankDate,Status,RankType from SNS_StarRank ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.StarRank();
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
            builder.Append("select count(1) FROM SNS_StarRank ");
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

        public bool Update(Maticsoft.Model.SNS.StarRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_StarRank set ");
            builder.Append("UserId=@UserId,");
            builder.Append("UserGravatar=@UserGravatar,");
            builder.Append("TypeId=@TypeId,");
            builder.Append("NickName=@NickName,");
            builder.Append("IsRecommend=@IsRecommend,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("TimeUnit=@TimeUnit,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("RankDate=@RankDate,");
            builder.Append("Status=@Status,");
            builder.Append("RankType=@RankType");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserGravatar", SqlDbType.NVarChar, 200), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@TimeUnit", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@RankDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@RankType", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.UserGravatar;
            cmdParms[2].Value = model.TypeId;
            cmdParms[3].Value = model.NickName;
            cmdParms[4].Value = model.IsRecommend;
            cmdParms[5].Value = model.Sequence;
            cmdParms[6].Value = model.TimeUnit;
            cmdParms[7].Value = model.StartDate;
            cmdParms[8].Value = model.EndDate;
            cmdParms[9].Value = model.CreatedDate;
            cmdParms[10].Value = model.RankDate;
            cmdParms[11].Value = model.Status;
            cmdParms[12].Value = model.RankType;
            cmdParms[13].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStateList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_StarRank set ");
            builder.Append("Status=@Status,");
            builder.Append(" where ID in (" + IDlist + ")");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

