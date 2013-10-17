namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UsersExp : IUsersExp
    {
        public bool Add(UsersExpModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UsersExp(");
            builder.Append("UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount)");
            builder.Append(" values (");
            builder.Append("@UserID,@Gravatar,@Singature,@TelPhone,@QQ,@MSN,@HomePage,@Birthday,@BirthdayVisible,@BirthdayIndexVisible,@Constellation,@ConstellationVisible,@ConstellationIndexVisible,@NativePlace,@NativePlaceVisible,@NativePlaceIndexVisible,@RegionId,@Address,@AddressVisible,@AddressIndexVisible,@BodilyForm,@BodilyFormVisible,@BodilyFormIndexVisible,@BloodType,@BloodTypeVisible,@BloodTypeIndexVisible,@Marriaged,@MarriagedVisible,@MarriagedIndexVisible,@PersonalStatus,@PersonalStatusVisible,@PersonalStatusIndexVisible,@Grade,@Balance,@Points,@TopicCount,@ReplyTopicCount,@FavTopicCount,@PvCount,@FansCount,@FellowCount,@AblumsCount,@FavouritesCount,@FavoritedCount,@ShareCount,@ProductsCount,@PersonalDomain,@LastAccessTime,@LastAccessIP,@LastPostTime,@LastLoginTime,@Remark,@IsUserDPI,@PayAccount)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Gravatar", SqlDbType.NVarChar, 200), new SqlParameter("@Singature", SqlDbType.NVarChar, 200), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 20), new SqlParameter("@QQ", SqlDbType.NVarChar, 30), new SqlParameter("@MSN", SqlDbType.NVarChar, 30), new SqlParameter("@HomePage", SqlDbType.NVarChar, 50), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@BirthdayVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BirthdayIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Constellation", SqlDbType.NVarChar, 10), new SqlParameter("@ConstellationVisible", SqlDbType.SmallInt, 2), new SqlParameter("@ConstellationIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@NativePlace", SqlDbType.NVarChar, 300), new SqlParameter("@NativePlaceVisible", SqlDbType.SmallInt, 2), new SqlParameter("@NativePlaceIndexVisible", SqlDbType.Bit, 1), 
                new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Address", SqlDbType.NVarChar, 300), new SqlParameter("@AddressVisible", SqlDbType.SmallInt, 2), new SqlParameter("@AddressIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BodilyForm", SqlDbType.NVarChar, 10), new SqlParameter("@BodilyFormVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BodilyFormIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BloodType", SqlDbType.NVarChar, 10), new SqlParameter("@BloodTypeVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BloodTypeIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Marriaged", SqlDbType.NVarChar, 10), new SqlParameter("@MarriagedVisible", SqlDbType.SmallInt, 2), new SqlParameter("@MarriagedIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@PersonalStatus", SqlDbType.NVarChar, 10), new SqlParameter("@PersonalStatusVisible", SqlDbType.SmallInt, 2), new SqlParameter("@PersonalStatusIndexVisible", SqlDbType.Bit, 1), 
                new SqlParameter("@Grade", SqlDbType.Int, 4), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@TopicCount", SqlDbType.Int, 4), new SqlParameter("@ReplyTopicCount", SqlDbType.Int, 4), new SqlParameter("@FavTopicCount", SqlDbType.Int, 4), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@FansCount", SqlDbType.Int, 4), new SqlParameter("@FellowCount", SqlDbType.Int, 4), new SqlParameter("@AblumsCount", SqlDbType.Int, 4), new SqlParameter("@FavouritesCount", SqlDbType.Int, 4), new SqlParameter("@FavoritedCount", SqlDbType.Int, 4), new SqlParameter("@ShareCount", SqlDbType.Int, 4), new SqlParameter("@ProductsCount", SqlDbType.Int, 4), new SqlParameter("@PersonalDomain", SqlDbType.NVarChar, 50), new SqlParameter("@LastAccessTime", SqlDbType.DateTime), 
                new SqlParameter("@LastAccessIP", SqlDbType.NVarChar, 50), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@LastLoginTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@IsUserDPI", SqlDbType.Bit, 1), new SqlParameter("@PayAccount", SqlDbType.NVarChar, 200)
             };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.Gravatar;
            cmdParms[2].Value = model.Singature;
            cmdParms[3].Value = model.TelPhone;
            cmdParms[4].Value = model.QQ;
            cmdParms[5].Value = model.MSN;
            cmdParms[6].Value = model.HomePage;
            cmdParms[7].Value = model.Birthday;
            cmdParms[8].Value = model.BirthdayVisible;
            cmdParms[9].Value = model.BirthdayIndexVisible;
            cmdParms[10].Value = model.Constellation;
            cmdParms[11].Value = model.ConstellationVisible;
            cmdParms[12].Value = model.ConstellationIndexVisible;
            cmdParms[13].Value = model.NativePlace;
            cmdParms[14].Value = model.NativePlaceVisible;
            cmdParms[15].Value = model.NativePlaceIndexVisible;
            cmdParms[0x10].Value = model.RegionId;
            cmdParms[0x11].Value = model.Address;
            cmdParms[0x12].Value = model.AddressVisible;
            cmdParms[0x13].Value = model.AddressIndexVisible;
            cmdParms[20].Value = model.BodilyForm;
            cmdParms[0x15].Value = model.BodilyFormVisible;
            cmdParms[0x16].Value = model.BodilyFormIndexVisible;
            cmdParms[0x17].Value = model.BloodType;
            cmdParms[0x18].Value = model.BloodTypeVisible;
            cmdParms[0x19].Value = model.BloodTypeIndexVisible;
            cmdParms[0x1a].Value = model.Marriaged;
            cmdParms[0x1b].Value = model.MarriagedVisible;
            cmdParms[0x1c].Value = model.MarriagedIndexVisible;
            cmdParms[0x1d].Value = model.PersonalStatus;
            cmdParms[30].Value = model.PersonalStatusVisible;
            cmdParms[0x1f].Value = model.PersonalStatusIndexVisible;
            cmdParms[0x20].Value = model.Grade;
            cmdParms[0x21].Value = model.Balance;
            cmdParms[0x22].Value = model.Points;
            cmdParms[0x23].Value = model.TopicCount;
            cmdParms[0x24].Value = model.ReplyTopicCount;
            cmdParms[0x25].Value = model.FavTopicCount;
            cmdParms[0x26].Value = model.PvCount;
            cmdParms[0x27].Value = model.FansCount;
            cmdParms[40].Value = model.FellowCount;
            cmdParms[0x29].Value = model.AblumsCount;
            cmdParms[0x2a].Value = model.FavouritesCount;
            cmdParms[0x2b].Value = model.FavoritedCount;
            cmdParms[0x2c].Value = model.ShareCount;
            cmdParms[0x2d].Value = model.ProductsCount;
            cmdParms[0x2e].Value = model.PersonalDomain;
            cmdParms[0x2f].Value = model.LastAccessTime;
            cmdParms[0x30].Value = model.LastAccessIP;
            cmdParms[0x31].Value = model.LastPostTime;
            cmdParms[50].Value = model.LastLoginTime;
            cmdParms[0x33].Value = model.Remark;
            cmdParms[0x34].Value = model.IsUserDPI;
            cmdParms[0x35].Value = model.PayAccount;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Add(int userID)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) };
            parameters[0].Value = userID;
            DbHelperSQL.RunProcedure("sp_Accounts_CreateUserExp", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool AddEx(UsersExpModel model, int inviteID, string inviteNick, int pointScore)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UsersExp(");
            builder.Append("UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount)");
            builder.Append(" values (");
            builder.Append("@UserID,@Gravatar,@Singature,@TelPhone,@QQ,@MSN,@HomePage,@Birthday,@BirthdayVisible,@BirthdayIndexVisible,@Constellation,@ConstellationVisible,@ConstellationIndexVisible,@NativePlace,@NativePlaceVisible,@NativePlaceIndexVisible,@RegionId,@Address,@AddressVisible,@AddressIndexVisible,@BodilyForm,@BodilyFormVisible,@BodilyFormIndexVisible,@BloodType,@BloodTypeVisible,@BloodTypeIndexVisible,@Marriaged,@MarriagedVisible,@MarriagedIndexVisible,@PersonalStatus,@PersonalStatusVisible,@PersonalStatusIndexVisible,@Grade,@Balance,@Points,@TopicCount,@ReplyTopicCount,@FavTopicCount,@PvCount,@FansCount,@FellowCount,@AblumsCount,@FavouritesCount,@FavoritedCount,@ShareCount,@ProductsCount,@PersonalDomain,@LastAccessTime,@LastAccessIP,@LastPostTime,@LastLoginTime,@Remark,@IsUserDPI,@PayAccount)");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Gravatar", SqlDbType.NVarChar, 200), new SqlParameter("@Singature", SqlDbType.NVarChar, 200), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 20), new SqlParameter("@QQ", SqlDbType.NVarChar, 30), new SqlParameter("@MSN", SqlDbType.NVarChar, 30), new SqlParameter("@HomePage", SqlDbType.NVarChar, 50), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@BirthdayVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BirthdayIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Constellation", SqlDbType.NVarChar, 10), new SqlParameter("@ConstellationVisible", SqlDbType.SmallInt, 2), new SqlParameter("@ConstellationIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@NativePlace", SqlDbType.NVarChar, 300), new SqlParameter("@NativePlaceVisible", SqlDbType.SmallInt, 2), new SqlParameter("@NativePlaceIndexVisible", SqlDbType.Bit, 1), 
                new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Address", SqlDbType.NVarChar, 300), new SqlParameter("@AddressVisible", SqlDbType.SmallInt, 2), new SqlParameter("@AddressIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BodilyForm", SqlDbType.NVarChar, 10), new SqlParameter("@BodilyFormVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BodilyFormIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BloodType", SqlDbType.NVarChar, 10), new SqlParameter("@BloodTypeVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BloodTypeIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Marriaged", SqlDbType.NVarChar, 10), new SqlParameter("@MarriagedVisible", SqlDbType.SmallInt, 2), new SqlParameter("@MarriagedIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@PersonalStatus", SqlDbType.NVarChar, 10), new SqlParameter("@PersonalStatusVisible", SqlDbType.SmallInt, 2), new SqlParameter("@PersonalStatusIndexVisible", SqlDbType.Bit, 1), 
                new SqlParameter("@Grade", SqlDbType.Int, 4), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@TopicCount", SqlDbType.Int, 4), new SqlParameter("@ReplyTopicCount", SqlDbType.Int, 4), new SqlParameter("@FavTopicCount", SqlDbType.Int, 4), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@FansCount", SqlDbType.Int, 4), new SqlParameter("@FellowCount", SqlDbType.Int, 4), new SqlParameter("@AblumsCount", SqlDbType.Int, 4), new SqlParameter("@FavouritesCount", SqlDbType.Int, 4), new SqlParameter("@FavoritedCount", SqlDbType.Int, 4), new SqlParameter("@ShareCount", SqlDbType.Int, 4), new SqlParameter("@ProductsCount", SqlDbType.Int, 4), new SqlParameter("@PersonalDomain", SqlDbType.NVarChar, 50), new SqlParameter("@LastAccessTime", SqlDbType.DateTime), 
                new SqlParameter("@LastAccessIP", SqlDbType.NVarChar, 50), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@LastLoginTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@IsUserDPI", SqlDbType.Bit, 1), new SqlParameter("@PayAccount", SqlDbType.NVarChar, 200)
             };
            para[0].Value = model.UserID;
            para[1].Value = model.Gravatar;
            para[2].Value = model.Singature;
            para[3].Value = model.TelPhone;
            para[4].Value = model.QQ;
            para[5].Value = model.MSN;
            para[6].Value = model.HomePage;
            para[7].Value = model.Birthday;
            para[8].Value = model.BirthdayVisible;
            para[9].Value = model.BirthdayIndexVisible;
            para[10].Value = model.Constellation;
            para[11].Value = model.ConstellationVisible;
            para[12].Value = model.ConstellationIndexVisible;
            para[13].Value = model.NativePlace;
            para[14].Value = model.NativePlaceVisible;
            para[15].Value = model.NativePlaceIndexVisible;
            para[0x10].Value = model.RegionId;
            para[0x11].Value = model.Address;
            para[0x12].Value = model.AddressVisible;
            para[0x13].Value = model.AddressIndexVisible;
            para[20].Value = model.BodilyForm;
            para[0x15].Value = model.BodilyFormVisible;
            para[0x16].Value = model.BodilyFormIndexVisible;
            para[0x17].Value = model.BloodType;
            para[0x18].Value = model.BloodTypeVisible;
            para[0x19].Value = model.BloodTypeIndexVisible;
            para[0x1a].Value = model.Marriaged;
            para[0x1b].Value = model.MarriagedVisible;
            para[0x1c].Value = model.MarriagedIndexVisible;
            para[0x1d].Value = model.PersonalStatus;
            para[30].Value = model.PersonalStatusVisible;
            para[0x1f].Value = model.PersonalStatusIndexVisible;
            para[0x20].Value = model.Grade;
            para[0x21].Value = model.Balance;
            para[0x22].Value = model.Points;
            para[0x23].Value = model.TopicCount;
            para[0x24].Value = model.ReplyTopicCount;
            para[0x25].Value = model.FavTopicCount;
            para[0x26].Value = model.PvCount;
            para[0x27].Value = model.FansCount;
            para[40].Value = model.FellowCount;
            para[0x29].Value = model.AblumsCount;
            para[0x2a].Value = model.FavouritesCount;
            para[0x2b].Value = model.FavoritedCount;
            para[0x2c].Value = model.ShareCount;
            para[0x2d].Value = model.ProductsCount;
            para[0x2e].Value = model.PersonalDomain;
            para[0x2f].Value = model.LastAccessTime;
            para[0x30].Value = model.LastAccessIP;
            para[0x31].Value = model.LastPostTime;
            para[50].Value = model.LastLoginTime;
            para[0x33].Value = model.Remark;
            para[0x34].Value = model.IsUserDPI;
            para[0x35].Value = model.PayAccount;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into Accounts_UserInvite(");
            builder2.Append("UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,RebateDesc)");
            builder2.Append(" values (");
            builder2.Append("@UserId,@UserNick,@InviteUserId,@InviteNick,@IsRebate,@IsNew,@CreatedDate,@RebateDesc)");
            builder2.Append(";select @@IDENTITY");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserNick", SqlDbType.NVarChar, 200), new SqlParameter("@InviteUserId", SqlDbType.Int, 4), new SqlParameter("@InviteNick", SqlDbType.NVarChar, 200), new SqlParameter("@IsRebate", SqlDbType.Bit, 1), new SqlParameter("@IsNew", SqlDbType.Bit, 1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@RebateDesc", SqlDbType.NVarChar, 200) };
            parameterArray2[0].Value = model.UserID;
            parameterArray2[1].Value = model.NickName;
            parameterArray2[2].Value = inviteID;
            parameterArray2[3].Value = inviteNick;
            parameterArray2[4].Value = true;
            parameterArray2[5].Value = true;
            parameterArray2[6].Value = DateTime.Now;
            parameterArray2[7].Value = "邀请用户+" + pointScore + "积分";
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool Delete(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Accounts_UsersExp ");
            builder.Append(" WHERE UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string UserIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Accounts_UsersExp ");
            builder.Append(" WHERE UserID in (" + UserIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Accounts_UsersExp");
            builder.Append(" WHERE UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAllEmpByUserId(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\nWITH    CTEGetChild\r\n          AS ( SELECT   *\r\n               FROM     Accounts_Users\r\n               WHERE    EmployeeID = {0}\r\n               UNION ALL\r\n               ( SELECT a.*\r\n                 FROM   Accounts_Users AS a\r\n                        INNER JOIN CTEGetChild AS b ON a.EmployeeID = b.UserID\r\n               )\r\n             )\r\n    SELECT  *\r\n    FROM    CTEGetChild ORDER BY EmployeeID, UserID\r\n", userId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount ");
            builder.Append(" FROM Accounts_UsersExp ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
            builder.Append(" UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount ");
            builder.Append(" FROM Accounts_UsersExp ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.UserID desc");
            }
            builder.Append(")AS Row, T.*  FROM Accounts_UsersExp T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public UsersExpModel GetModel(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount from Accounts_UsersExp ");
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            UsersExpModel model = new UsersExpModel();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
            {
                model.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Gravatar"] != null) && (set.Tables[0].Rows[0]["Gravatar"].ToString() != ""))
            {
                model.Gravatar = set.Tables[0].Rows[0]["Gravatar"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Singature"] != null) && (set.Tables[0].Rows[0]["Singature"].ToString() != ""))
            {
                model.Singature = set.Tables[0].Rows[0]["Singature"].ToString();
            }
            if ((set.Tables[0].Rows[0]["TelPhone"] != null) && (set.Tables[0].Rows[0]["TelPhone"].ToString() != ""))
            {
                model.TelPhone = set.Tables[0].Rows[0]["TelPhone"].ToString();
            }
            if ((set.Tables[0].Rows[0]["QQ"] != null) && (set.Tables[0].Rows[0]["QQ"].ToString() != ""))
            {
                model.QQ = set.Tables[0].Rows[0]["QQ"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MSN"] != null) && (set.Tables[0].Rows[0]["MSN"].ToString() != ""))
            {
                model.MSN = set.Tables[0].Rows[0]["MSN"].ToString();
            }
            if ((set.Tables[0].Rows[0]["HomePage"] != null) && (set.Tables[0].Rows[0]["HomePage"].ToString() != ""))
            {
                model.HomePage = set.Tables[0].Rows[0]["HomePage"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Birthday"] != null) && (set.Tables[0].Rows[0]["Birthday"].ToString() != ""))
            {
                model.Birthday = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["Birthday"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["BirthdayVisible"] != null) && (set.Tables[0].Rows[0]["BirthdayVisible"].ToString() != ""))
            {
                model.BirthdayVisible = int.Parse(set.Tables[0].Rows[0]["BirthdayVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["BirthdayIndexVisible"] != null) && (set.Tables[0].Rows[0]["BirthdayIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["BirthdayIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["BirthdayIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.BirthdayIndexVisible = true;
                }
                else
                {
                    model.BirthdayIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["Constellation"] != null) && (set.Tables[0].Rows[0]["Constellation"].ToString() != ""))
            {
                model.Constellation = set.Tables[0].Rows[0]["Constellation"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ConstellationVisible"] != null) && (set.Tables[0].Rows[0]["ConstellationVisible"].ToString() != ""))
            {
                model.ConstellationVisible = int.Parse(set.Tables[0].Rows[0]["ConstellationVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ConstellationIndexVisible"] != null) && (set.Tables[0].Rows[0]["ConstellationIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["ConstellationIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["ConstellationIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.ConstellationIndexVisible = true;
                }
                else
                {
                    model.ConstellationIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["NativePlace"] != null) && (set.Tables[0].Rows[0]["NativePlace"].ToString() != ""))
            {
                model.NativePlace = set.Tables[0].Rows[0]["NativePlace"].ToString();
            }
            if ((set.Tables[0].Rows[0]["NativePlaceVisible"] != null) && (set.Tables[0].Rows[0]["NativePlaceVisible"].ToString() != ""))
            {
                model.NativePlaceVisible = int.Parse(set.Tables[0].Rows[0]["NativePlaceVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["NativePlaceIndexVisible"] != null) && (set.Tables[0].Rows[0]["NativePlaceIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["NativePlaceIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["NativePlaceIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.NativePlaceIndexVisible = true;
                }
                else
                {
                    model.NativePlaceIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["RegionId"] != null) && (set.Tables[0].Rows[0]["RegionId"].ToString() != ""))
            {
                model.RegionId = new int?(int.Parse(set.Tables[0].Rows[0]["RegionId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Address"] != null) && (set.Tables[0].Rows[0]["Address"].ToString() != ""))
            {
                model.Address = set.Tables[0].Rows[0]["Address"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AddressVisible"] != null) && (set.Tables[0].Rows[0]["AddressVisible"].ToString() != ""))
            {
                model.AddressVisible = int.Parse(set.Tables[0].Rows[0]["AddressVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AddressIndexVisible"] != null) && (set.Tables[0].Rows[0]["AddressIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["AddressIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["AddressIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.AddressIndexVisible = true;
                }
                else
                {
                    model.AddressIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["BodilyForm"] != null) && (set.Tables[0].Rows[0]["BodilyForm"].ToString() != ""))
            {
                model.BodilyForm = set.Tables[0].Rows[0]["BodilyForm"].ToString();
            }
            if ((set.Tables[0].Rows[0]["BodilyFormVisible"] != null) && (set.Tables[0].Rows[0]["BodilyFormVisible"].ToString() != ""))
            {
                model.BodilyFormVisible = int.Parse(set.Tables[0].Rows[0]["BodilyFormVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["BodilyFormIndexVisible"] != null) && (set.Tables[0].Rows[0]["BodilyFormIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["BodilyFormIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["BodilyFormIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.BodilyFormIndexVisible = true;
                }
                else
                {
                    model.BodilyFormIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["BloodType"] != null) && (set.Tables[0].Rows[0]["BloodType"].ToString() != ""))
            {
                model.BloodType = set.Tables[0].Rows[0]["BloodType"].ToString();
            }
            if ((set.Tables[0].Rows[0]["BloodTypeVisible"] != null) && (set.Tables[0].Rows[0]["BloodTypeVisible"].ToString() != ""))
            {
                model.BloodTypeVisible = int.Parse(set.Tables[0].Rows[0]["BloodTypeVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["BloodTypeIndexVisible"] != null) && (set.Tables[0].Rows[0]["BloodTypeIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["BloodTypeIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["BloodTypeIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.BloodTypeIndexVisible = true;
                }
                else
                {
                    model.BloodTypeIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["Marriaged"] != null) && (set.Tables[0].Rows[0]["Marriaged"].ToString() != ""))
            {
                model.Marriaged = set.Tables[0].Rows[0]["Marriaged"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MarriagedVisible"] != null) && (set.Tables[0].Rows[0]["MarriagedVisible"].ToString() != ""))
            {
                model.MarriagedVisible = int.Parse(set.Tables[0].Rows[0]["MarriagedVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["MarriagedIndexVisible"] != null) && (set.Tables[0].Rows[0]["MarriagedIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["MarriagedIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["MarriagedIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.MarriagedIndexVisible = true;
                }
                else
                {
                    model.MarriagedIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["PersonalStatus"] != null) && (set.Tables[0].Rows[0]["PersonalStatus"].ToString() != ""))
            {
                model.PersonalStatus = set.Tables[0].Rows[0]["PersonalStatus"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PersonalStatusVisible"] != null) && (set.Tables[0].Rows[0]["PersonalStatusVisible"].ToString() != ""))
            {
                model.PersonalStatusVisible = int.Parse(set.Tables[0].Rows[0]["PersonalStatusVisible"].ToString());
            }
            if ((set.Tables[0].Rows[0]["PersonalStatusIndexVisible"] != null) && (set.Tables[0].Rows[0]["PersonalStatusIndexVisible"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["PersonalStatusIndexVisible"].ToString() == "1") || (set.Tables[0].Rows[0]["PersonalStatusIndexVisible"].ToString().ToLower() == "true"))
                {
                    model.PersonalStatusIndexVisible = true;
                }
                else
                {
                    model.PersonalStatusIndexVisible = false;
                }
            }
            if ((set.Tables[0].Rows[0]["Grade"] != null) && (set.Tables[0].Rows[0]["Grade"].ToString() != ""))
            {
                model.Grade = new int?(int.Parse(set.Tables[0].Rows[0]["Grade"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Balance"] != null) && (set.Tables[0].Rows[0]["Balance"].ToString() != ""))
            {
                model.Balance = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["Balance"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Points"] != null) && (set.Tables[0].Rows[0]["Points"].ToString() != ""))
            {
                model.Points = new int?(int.Parse(set.Tables[0].Rows[0]["Points"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["PvCount"] != null) && (set.Tables[0].Rows[0]["PvCount"].ToString() != ""))
            {
                model.PvCount = new int?(int.Parse(set.Tables[0].Rows[0]["PvCount"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["FansCount"] != null) && (set.Tables[0].Rows[0]["FansCount"].ToString() != ""))
            {
                model.FansCount = new int?(int.Parse(set.Tables[0].Rows[0]["FansCount"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["FellowCount"] != null) && (set.Tables[0].Rows[0]["FellowCount"].ToString() != ""))
            {
                model.FellowCount = new int?(int.Parse(set.Tables[0].Rows[0]["FellowCount"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["AblumsCount"] != null) && (set.Tables[0].Rows[0]["AblumsCount"].ToString() != ""))
            {
                model.AblumsCount = new int?(int.Parse(set.Tables[0].Rows[0]["AblumsCount"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["FavouritesCount"] != null) && (set.Tables[0].Rows[0]["FavouritesCount"].ToString() != ""))
            {
                model.FavouritesCount = new int?(int.Parse(set.Tables[0].Rows[0]["FavouritesCount"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ShareCount"] != null) && (set.Tables[0].Rows[0]["ShareCount"].ToString() != ""))
            {
                model.ShareCount = new int?(int.Parse(set.Tables[0].Rows[0]["ShareCount"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["PersonalDomain"] != null) && (set.Tables[0].Rows[0]["PersonalDomain"].ToString() != ""))
            {
                model.PersonalDomain = set.Tables[0].Rows[0]["PersonalDomain"].ToString();
            }
            if ((set.Tables[0].Rows[0]["LastAccessTime"] != null) && (set.Tables[0].Rows[0]["LastAccessTime"].ToString() != ""))
            {
                model.LastAccessTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["LastAccessTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["LastAccessIP"] != null) && (set.Tables[0].Rows[0]["LastAccessIP"].ToString() != ""))
            {
                model.LastAccessIP = set.Tables[0].Rows[0]["LastAccessIP"].ToString();
            }
            if ((set.Tables[0].Rows[0]["LastPostTime"] != null) && (set.Tables[0].Rows[0]["LastPostTime"].ToString() != ""))
            {
                model.LastPostTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["LastPostTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["LastLoginTime"] != null) && (set.Tables[0].Rows[0]["LastLoginTime"].ToString() != ""))
            {
                model.LastLoginTime = DateTime.Parse(set.Tables[0].Rows[0]["LastLoginTime"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Remark"] != null) && (set.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            }
            if (set.Tables[0].Rows[0]["PayAccount"] != null)
            {
                model.PayAccount = set.Tables[0].Rows[0]["PayAccount"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IsUserDPI"] != null) && (set.Tables[0].Rows[0]["IsUserDPI"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsUserDPI"].ToString() == "1") || (set.Tables[0].Rows[0]["IsUserDPI"].ToString().ToLower() == "true"))
                {
                    model.IsUserDPI = true;
                    return model;
                }
                model.IsUserDPI = false;
                return model;
            }
            model.IsUserDPI = false;
            return model;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Accounts_UsersExp ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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

        public decimal GetUserBalance(int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  Balance FROM Accounts_UsersExp WHERE UserId=@UserId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0M;
            }
            return Convert.ToDecimal(single);
        }

        public int GetUserCountByKeyWord(string NickName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM  Accounts_Users au inner JOIN Accounts_UsersExp uea ON au.UserID=uea.UserID  ");
            if (!string.IsNullOrEmpty(NickName))
            {
                builder.Append("AND NickName LIKE '%" + NickName + "%'");
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetUserList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" * ");
            builder.Append(" FROM Accounts_Users ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetUserListByKeyWord(string NickName, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.UserID desc");
            }
            builder.Append(")AS Row, T.*  FROM (SELECT uea.*,au.NickName FROM Accounts_Users au inner JOIN Accounts_UsersExp uea ON au.UserID=uea.UserID  ");
            if (!string.IsNullOrEmpty(NickName))
            {
                builder.Append(" AND NickName LIKE @NickName");
            }
            builder.Append(" ) T) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar) };
            cmdParms[0].Value = "%" + NickName + "%";
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public int GetUserRankId(int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  Grade FROM Accounts_UsersExp WHERE UserId=@UserId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(UsersExpModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UsersExp set ");
            builder.Append("Gravatar=@Gravatar,");
            builder.Append("Singature=@Singature,");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("QQ=@QQ,");
            builder.Append("MSN=@MSN,");
            builder.Append("HomePage=@HomePage,");
            builder.Append("Birthday=@Birthday,");
            builder.Append("BirthdayVisible=@BirthdayVisible,");
            builder.Append("BirthdayIndexVisible=@BirthdayIndexVisible,");
            builder.Append("Constellation=@Constellation,");
            builder.Append("ConstellationVisible=@ConstellationVisible,");
            builder.Append("ConstellationIndexVisible=@ConstellationIndexVisible,");
            builder.Append("NativePlace=@NativePlace,");
            builder.Append("NativePlaceVisible=@NativePlaceVisible,");
            builder.Append("NativePlaceIndexVisible=@NativePlaceIndexVisible,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("Address=@Address,");
            builder.Append("AddressVisible=@AddressVisible,");
            builder.Append("AddressIndexVisible=@AddressIndexVisible,");
            builder.Append("BodilyForm=@BodilyForm,");
            builder.Append("BodilyFormVisible=@BodilyFormVisible,");
            builder.Append("BodilyFormIndexVisible=@BodilyFormIndexVisible,");
            builder.Append("BloodType=@BloodType,");
            builder.Append("BloodTypeVisible=@BloodTypeVisible,");
            builder.Append("BloodTypeIndexVisible=@BloodTypeIndexVisible,");
            builder.Append("Marriaged=@Marriaged,");
            builder.Append("MarriagedVisible=@MarriagedVisible,");
            builder.Append("MarriagedIndexVisible=@MarriagedIndexVisible,");
            builder.Append("PersonalStatus=@PersonalStatus,");
            builder.Append("PersonalStatusVisible=@PersonalStatusVisible,");
            builder.Append("PersonalStatusIndexVisible=@PersonalStatusIndexVisible,");
            builder.Append("Grade=@Grade,");
            builder.Append("Balance=@Balance,");
            builder.Append("Points=@Points,");
            builder.Append("TopicCount=@TopicCount,");
            builder.Append("ReplyTopicCount=@ReplyTopicCount,");
            builder.Append("FavTopicCount=@FavTopicCount,");
            builder.Append("PvCount=@PvCount,");
            builder.Append("FansCount=@FansCount,");
            builder.Append("FellowCount=@FellowCount,");
            builder.Append("AblumsCount=@AblumsCount,");
            builder.Append("FavouritesCount=@FavouritesCount,");
            builder.Append("FavoritedCount=@FavoritedCount,");
            builder.Append("ShareCount=@ShareCount,");
            builder.Append("ProductsCount=@ProductsCount,");
            builder.Append("PersonalDomain=@PersonalDomain,");
            builder.Append("LastAccessTime=@LastAccessTime,");
            builder.Append("LastAccessIP=@LastAccessIP,");
            builder.Append("LastPostTime=@LastPostTime,");
            builder.Append("LastLoginTime=@LastLoginTime,");
            builder.Append("Remark=@Remark,");
            builder.Append("IsUserDPI=@IsUserDPI,");
            builder.Append("PayAccount=@PayAccount");
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Gravatar", SqlDbType.NVarChar, 200), new SqlParameter("@Singature", SqlDbType.NVarChar, 200), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 20), new SqlParameter("@QQ", SqlDbType.NVarChar, 30), new SqlParameter("@MSN", SqlDbType.NVarChar, 30), new SqlParameter("@HomePage", SqlDbType.NVarChar, 50), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@BirthdayVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BirthdayIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Constellation", SqlDbType.NVarChar, 10), new SqlParameter("@ConstellationVisible", SqlDbType.SmallInt, 2), new SqlParameter("@ConstellationIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@NativePlace", SqlDbType.NVarChar, 300), new SqlParameter("@NativePlaceVisible", SqlDbType.SmallInt, 2), new SqlParameter("@NativePlaceIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@RegionId", SqlDbType.Int, 4), 
                new SqlParameter("@Address", SqlDbType.NVarChar, 300), new SqlParameter("@AddressVisible", SqlDbType.SmallInt, 2), new SqlParameter("@AddressIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BodilyForm", SqlDbType.NVarChar, 10), new SqlParameter("@BodilyFormVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BodilyFormIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BloodType", SqlDbType.NVarChar, 10), new SqlParameter("@BloodTypeVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BloodTypeIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Marriaged", SqlDbType.NVarChar, 10), new SqlParameter("@MarriagedVisible", SqlDbType.SmallInt, 2), new SqlParameter("@MarriagedIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@PersonalStatus", SqlDbType.NVarChar, 10), new SqlParameter("@PersonalStatusVisible", SqlDbType.SmallInt, 2), new SqlParameter("@PersonalStatusIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Grade", SqlDbType.Int, 4), 
                new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@TopicCount", SqlDbType.Int, 4), new SqlParameter("@ReplyTopicCount", SqlDbType.Int, 4), new SqlParameter("@FavTopicCount", SqlDbType.Int, 4), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@FansCount", SqlDbType.Int, 4), new SqlParameter("@FellowCount", SqlDbType.Int, 4), new SqlParameter("@AblumsCount", SqlDbType.Int, 4), new SqlParameter("@FavouritesCount", SqlDbType.Int, 4), new SqlParameter("@FavoritedCount", SqlDbType.Int, 4), new SqlParameter("@ShareCount", SqlDbType.Int, 4), new SqlParameter("@ProductsCount", SqlDbType.Int, 4), new SqlParameter("@PersonalDomain", SqlDbType.NVarChar, 50), new SqlParameter("@LastAccessTime", SqlDbType.DateTime), new SqlParameter("@LastAccessIP", SqlDbType.NVarChar, 50), 
                new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@LastLoginTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@IsUserDPI", SqlDbType.Bit, 1), new SqlParameter("@PayAccount", SqlDbType.NVarChar, 200), new SqlParameter("@UserID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Gravatar;
            cmdParms[1].Value = model.Singature;
            cmdParms[2].Value = model.TelPhone;
            cmdParms[3].Value = model.QQ;
            cmdParms[4].Value = model.MSN;
            cmdParms[5].Value = model.HomePage;
            cmdParms[6].Value = model.Birthday;
            cmdParms[7].Value = model.BirthdayVisible;
            cmdParms[8].Value = model.BirthdayIndexVisible;
            cmdParms[9].Value = model.Constellation;
            cmdParms[10].Value = model.ConstellationVisible;
            cmdParms[11].Value = model.ConstellationIndexVisible;
            cmdParms[12].Value = model.NativePlace;
            cmdParms[13].Value = model.NativePlaceVisible;
            cmdParms[14].Value = model.NativePlaceIndexVisible;
            cmdParms[15].Value = model.RegionId;
            cmdParms[0x10].Value = model.Address;
            cmdParms[0x11].Value = model.AddressVisible;
            cmdParms[0x12].Value = model.AddressIndexVisible;
            cmdParms[0x13].Value = model.BodilyForm;
            cmdParms[20].Value = model.BodilyFormVisible;
            cmdParms[0x15].Value = model.BodilyFormIndexVisible;
            cmdParms[0x16].Value = model.BloodType;
            cmdParms[0x17].Value = model.BloodTypeVisible;
            cmdParms[0x18].Value = model.BloodTypeIndexVisible;
            cmdParms[0x19].Value = model.Marriaged;
            cmdParms[0x1a].Value = model.MarriagedVisible;
            cmdParms[0x1b].Value = model.MarriagedIndexVisible;
            cmdParms[0x1c].Value = model.PersonalStatus;
            cmdParms[0x1d].Value = model.PersonalStatusVisible;
            cmdParms[30].Value = model.PersonalStatusIndexVisible;
            cmdParms[0x1f].Value = model.Grade;
            cmdParms[0x20].Value = model.Balance;
            cmdParms[0x21].Value = model.Points;
            cmdParms[0x22].Value = model.TopicCount;
            cmdParms[0x23].Value = model.ReplyTopicCount;
            cmdParms[0x24].Value = model.FavTopicCount;
            cmdParms[0x25].Value = model.PvCount;
            cmdParms[0x26].Value = model.FansCount;
            cmdParms[0x27].Value = model.FellowCount;
            cmdParms[40].Value = model.AblumsCount;
            cmdParms[0x29].Value = model.FavouritesCount;
            cmdParms[0x2a].Value = model.FavoritedCount;
            cmdParms[0x2b].Value = model.ShareCount;
            cmdParms[0x2c].Value = model.ProductsCount;
            cmdParms[0x2d].Value = model.PersonalDomain;
            cmdParms[0x2e].Value = model.LastAccessTime;
            cmdParms[0x2f].Value = model.LastAccessIP;
            cmdParms[0x30].Value = model.LastPostTime;
            cmdParms[0x31].Value = model.LastLoginTime;
            cmdParms[50].Value = model.Remark;
            cmdParms[0x33].Value = model.IsUserDPI;
            cmdParms[0x34].Value = model.PayAccount;
            cmdParms[0x35].Value = model.UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateAblumsCount()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_UsersExp SET ");
            builder.Append("AblumsCount=(select COUNT(1) from SNS_UserAlbums where CreatedUserID=Accounts_UsersExp.UserID)");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateFavouritesCount()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_UsersExp SET ");
            builder.Append("FavouritesCount=( select COUNT(1) from SNS_UserFavourite where CreatedUserID=Accounts_UsersExp.UserID)");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateIsDPI(string userIds, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE UE SET IsUserDPI={0} FROM Accounts_UsersExp UE, ", status);
            builder.AppendFormat("(SELECT UserID FROM Accounts_UsersApprove WHERE ApproveID IN ({0}))AP ", userIds);
            builder.Append("WHERE UE.UserID=AP.UserID ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdatePhoneAndPay(int userId, string account, string phone)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UsersExp set ");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("PayAccount=@PayAccount");
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TelPhone", SqlDbType.NVarChar, 20), new SqlParameter("@PayAccount", SqlDbType.NVarChar, 200), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = phone;
            cmdParms[1].Value = account;
            cmdParms[2].Value = userId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateProductCount()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_UsersExp SET ");
            builder.Append("ProductsCount=(select COUNT(1) from SNS_Products where CreateUserID=Accounts_UsersExp.UserID)");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateShareCount()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_UsersExp SET ");
            builder.Append("ShareCount=ProductsCount+(select COUNT(1) from SNS_Photos where CreatedUserID=Accounts_UsersExp.UserID)");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

