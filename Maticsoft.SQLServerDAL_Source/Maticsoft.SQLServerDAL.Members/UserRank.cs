namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserRank : IUserRank
    {
        public int Add(Maticsoft.Model.Members.UserRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UserRank(");
            builder.Append("Name,Description,PointMax,PointMin,IsDefault,RankType,NumberOfMemberRanks,IsMemberCreated,CreatorUserId,PriceType,PriceOperations,PriceValue)");
            builder.Append(" values (");
            builder.Append("@Name,@Description,@PointMax,@PointMin,@IsDefault,@RankType,@NumberOfMemberRanks,@IsMemberCreated,@CreatorUserId,@PriceType,@PriceOperations,@PriceValue)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@PointMax", SqlDbType.Int, 4), new SqlParameter("@PointMin", SqlDbType.Int, 4), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@RankType", SqlDbType.Int, 4), new SqlParameter("@NumberOfMemberRanks", SqlDbType.Int, 4), new SqlParameter("@IsMemberCreated", SqlDbType.Bit, 1), new SqlParameter("@CreatorUserId", SqlDbType.Int, 4), new SqlParameter("@PriceType", SqlDbType.NVarChar, 20), new SqlParameter("@PriceOperations", SqlDbType.NVarChar, 10), new SqlParameter("@PriceValue", SqlDbType.Money, 8) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.PointMax;
            cmdParms[3].Value = model.PointMin;
            cmdParms[4].Value = model.IsDefault;
            cmdParms[5].Value = model.RankType;
            cmdParms[6].Value = model.NumberOfMemberRanks;
            cmdParms[7].Value = model.IsMemberCreated;
            cmdParms[8].Value = model.CreatorUserId;
            cmdParms[9].Value = model.PriceType;
            cmdParms[10].Value = model.PriceOperations;
            cmdParms[11].Value = model.PriceValue;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Members.UserRank DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.UserRank rank = new Maticsoft.Model.Members.UserRank();
            if (row != null)
            {
                if ((row["RankId"] != null) && (row["RankId"].ToString() != ""))
                {
                    rank.RankId = int.Parse(row["RankId"].ToString());
                }
                if (row["Name"] != null)
                {
                    rank.Name = row["Name"].ToString();
                }
                if (row["Description"] != null)
                {
                    rank.Description = row["Description"].ToString();
                }
                if ((row["PointMax"] != null) && (row["PointMax"].ToString() != ""))
                {
                    rank.PointMax = int.Parse(row["PointMax"].ToString());
                }
                if ((row["PointMin"] != null) && (row["PointMin"].ToString() != ""))
                {
                    rank.PointMin = int.Parse(row["PointMin"].ToString());
                }
                if ((row["IsDefault"] != null) && (row["IsDefault"].ToString() != ""))
                {
                    if ((row["IsDefault"].ToString() == "1") || (row["IsDefault"].ToString().ToLower() == "true"))
                    {
                        rank.IsDefault = true;
                    }
                    else
                    {
                        rank.IsDefault = false;
                    }
                }
                if ((row["RankType"] != null) && (row["RankType"].ToString() != ""))
                {
                    rank.RankType = int.Parse(row["RankType"].ToString());
                }
                if ((row["NumberOfMemberRanks"] != null) && (row["NumberOfMemberRanks"].ToString() != ""))
                {
                    rank.NumberOfMemberRanks = new int?(int.Parse(row["NumberOfMemberRanks"].ToString()));
                }
                if ((row["IsMemberCreated"] != null) && (row["IsMemberCreated"].ToString() != ""))
                {
                    if ((row["IsMemberCreated"].ToString() == "1") || (row["IsMemberCreated"].ToString().ToLower() == "true"))
                    {
                        rank.IsMemberCreated = true;
                    }
                    else
                    {
                        rank.IsMemberCreated = false;
                    }
                }
                if ((row["CreatorUserId"] != null) && (row["CreatorUserId"].ToString() != ""))
                {
                    rank.CreatorUserId = new int?(int.Parse(row["CreatorUserId"].ToString()));
                }
                if (row["PriceType"] != null)
                {
                    rank.PriceType = row["PriceType"].ToString();
                }
                if (row["PriceOperations"] != null)
                {
                    rank.PriceOperations = row["PriceOperations"].ToString();
                }
                if ((row["PriceValue"] != null) && (row["PriceValue"].ToString() != ""))
                {
                    rank.PriceValue = decimal.Parse(row["PriceValue"].ToString());
                }
            }
            return rank;
        }

        public bool Delete(int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_UserRank ");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RankIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_UserRank ");
            builder.Append(" where RankId in (" + RankIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserRank");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RankId,Name,Description,PointMax,PointMin,IsDefault,RankType,NumberOfMemberRanks,IsMemberCreated,CreatorUserId,PriceType,PriceOperations,PriceValue ");
            builder.Append(" FROM Accounts_UserRank ");
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
            builder.Append(" RankId,Name,Description,PointMax,PointMin,IsDefault,RankType,NumberOfMemberRanks,IsMemberCreated,CreatorUserId,PriceType,PriceOperations,PriceValue ");
            builder.Append(" FROM Accounts_UserRank ");
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
                builder.Append("order by T.RankId desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_UserRank T ");
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
            return DbHelperSQL.GetMaxID("RankId", "Accounts_UserRank");
        }

        public Maticsoft.Model.Members.UserRank GetModel(int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RankId,Name,Description,PointMax,PointMin,IsDefault,RankType,NumberOfMemberRanks,IsMemberCreated,CreatorUserId,PriceType,PriceOperations,PriceValue from Accounts_UserRank ");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            new Maticsoft.Model.Members.UserRank();
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
            builder.Append("select count(1) FROM Accounts_UserRank ");
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

        public string GetUserLevel(int grades)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   top 1 name from Accounts_UserRank ");
            builder.Append(" WHERE @Score BETWEEN PointMin AND PointMax");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Score", SqlDbType.Int, 4) };
            parameterArray[0].Value = grades;
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return "";
            }
            return single.ToString();
        }

        public bool Update(Maticsoft.Model.Members.UserRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UserRank set ");
            builder.Append("Name=@Name,");
            builder.Append("Description=@Description,");
            builder.Append("PointMax=@PointMax,");
            builder.Append("PointMin=@PointMin,");
            builder.Append("IsDefault=@IsDefault,");
            builder.Append("RankType=@RankType,");
            builder.Append("NumberOfMemberRanks=@NumberOfMemberRanks,");
            builder.Append("IsMemberCreated=@IsMemberCreated,");
            builder.Append("CreatorUserId=@CreatorUserId,");
            builder.Append("PriceType=@PriceType,");
            builder.Append("PriceOperations=@PriceOperations,");
            builder.Append("PriceValue=@PriceValue");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@PointMax", SqlDbType.Int, 4), new SqlParameter("@PointMin", SqlDbType.Int, 4), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@RankType", SqlDbType.Int, 4), new SqlParameter("@NumberOfMemberRanks", SqlDbType.Int, 4), new SqlParameter("@IsMemberCreated", SqlDbType.Bit, 1), new SqlParameter("@CreatorUserId", SqlDbType.Int, 4), new SqlParameter("@PriceType", SqlDbType.NVarChar, 20), new SqlParameter("@PriceOperations", SqlDbType.NVarChar, 10), new SqlParameter("@PriceValue", SqlDbType.Money, 8), new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.PointMax;
            cmdParms[3].Value = model.PointMin;
            cmdParms[4].Value = model.IsDefault;
            cmdParms[5].Value = model.RankType;
            cmdParms[6].Value = model.NumberOfMemberRanks;
            cmdParms[7].Value = model.IsMemberCreated;
            cmdParms[8].Value = model.CreatorUserId;
            cmdParms[9].Value = model.PriceType;
            cmdParms[10].Value = model.PriceOperations;
            cmdParms[11].Value = model.PriceValue;
            cmdParms[12].Value = model.RankId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

