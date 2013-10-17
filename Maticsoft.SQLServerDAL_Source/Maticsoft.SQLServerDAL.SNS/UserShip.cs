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

    public class UserShip : IUserShip
    {
        public bool Add(Maticsoft.Model.SNS.UserShip model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserShip(");
            builder.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
            builder.Append(" values (");
            builder.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRead", SqlDbType.Bit, 1) };
            cmdParms[0].Value = model.ActiveUserID;
            cmdParms[1].Value = model.PassiveUserID;
            cmdParms[2].Value = model.State;
            cmdParms[3].Value = model.Type;
            cmdParms[4].Value = model.CategoryID;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.IsRead;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddAttention(int ActiveUserID, int PassiveUserID)
        {
            if (!this.Exists(ActiveUserID, PassiveUserID))
            {
                List<CommandInfo> cmdList = new List<CommandInfo>();
                int num = 0;
                if (this.Exists(PassiveUserID, ActiveUserID))
                {
                    num = 1;
                    StringBuilder builder = new StringBuilder();
                    builder.Append("update SNS_UserShip set ");
                    builder.Append("Type=@Type");
                    builder.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                    SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
                    parameterArray[0].Value = num;
                    parameterArray[1].Value = PassiveUserID;
                    parameterArray[2].Value = ActiveUserID;
                    CommandInfo info = new CommandInfo(builder.ToString(), parameterArray);
                    cmdList.Add(info);
                }
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("insert into SNS_UserShip(");
                builder2.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
                builder2.Append(" values (");
                builder2.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRead", SqlDbType.Bit, 1) };
                para[0].Value = ActiveUserID;
                para[1].Value = PassiveUserID;
                para[2].Value = 1;
                para[3].Value = num;
                para[4].Value = 0;
                para[5].Value = DateTime.Now;
                para[6].Value = false;
                CommandInfo item = new CommandInfo(builder2.ToString(), para);
                cmdList.Add(item);
                StringBuilder builder3 = new StringBuilder();
                builder3.Append(" update Accounts_UsersExp set ");
                builder3.Append(" FellowCount=FellowCount+1 ");
                builder3.Append(" where UserID=@UserID");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameterArray3[0].Value = ActiveUserID;
                CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
                cmdList.Add(info3);
                StringBuilder builder4 = new StringBuilder();
                builder4.Append("update Accounts_UsersExp set ");
                builder4.Append("FansCount=FansCount+1");
                builder4.Append(" where UserID=@UserID");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameterArray4[0].Value = PassiveUserID;
                CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
                cmdList.Add(info4);
                if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CancelAttention(int ActiveUserID, int PassiveUserID)
        {
            if (this.Exists(ActiveUserID, PassiveUserID))
            {
                List<CommandInfo> cmdList = new List<CommandInfo>();
                if (this.Exists(PassiveUserID, ActiveUserID))
                {
                    int num = 0;
                    StringBuilder builder = new StringBuilder();
                    builder.Append("update SNS_UserShip set ");
                    builder.Append("Type=@Type");
                    builder.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                    SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
                    parameterArray[0].Value = num;
                    parameterArray[1].Value = PassiveUserID;
                    parameterArray[2].Value = ActiveUserID;
                    CommandInfo info = new CommandInfo(builder.ToString(), parameterArray);
                    cmdList.Add(info);
                }
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("DELETE FROM  SNS_UserShip ");
                builder2.Append(" where ActiveUserID=@ActiveUserID AND PassiveUserID=@PassiveUserID");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
                para[0].Value = ActiveUserID;
                para[1].Value = PassiveUserID;
                CommandInfo item = new CommandInfo(builder2.ToString(), para);
                cmdList.Add(item);
                StringBuilder builder3 = new StringBuilder();
                builder3.Append(" update Accounts_UsersExp set ");
                builder3.Append(" FellowCount=FellowCount-1 ");
                builder3.Append(" where UserID=@UserID");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameterArray3[0].Value = ActiveUserID;
                CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
                cmdList.Add(info3);
                StringBuilder builder4 = new StringBuilder();
                builder4.Append("update Accounts_UsersExp set ");
                builder4.Append("FansCount=FansCount-1");
                builder4.Append(" where UserID=@UserID");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameterArray4[0].Value = PassiveUserID;
                CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
                cmdList.Add(info4);
                if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public Maticsoft.Model.SNS.UserShip DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserShip ship = new Maticsoft.Model.SNS.UserShip();
            if (row != null)
            {
                if ((row["ActiveUserID"] != null) && (row["ActiveUserID"].ToString() != ""))
                {
                    ship.ActiveUserID = int.Parse(row["ActiveUserID"].ToString());
                }
                if ((row["PassiveUserID"] != null) && (row["PassiveUserID"].ToString() != ""))
                {
                    ship.PassiveUserID = int.Parse(row["PassiveUserID"].ToString());
                }
                if ((row["State"] != null) && (row["State"].ToString() != ""))
                {
                    ship.State = int.Parse(row["State"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    ship.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["CategoryID"] != null) && (row["CategoryID"].ToString() != ""))
                {
                    ship.CategoryID = int.Parse(row["CategoryID"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    ship.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["IsRead"] == null) || !(row["IsRead"].ToString() != ""))
                {
                    return ship;
                }
                if ((row["IsRead"].ToString() == "1") || (row["IsRead"].ToString().ToLower() == "true"))
                {
                    ship.IsRead = true;
                    return ship;
                }
                ship.IsRead = false;
            }
            return ship;
        }

        public Maticsoft.Model.SNS.UserShip DataRowToModelEx(DataRow row)
        {
            Maticsoft.Model.SNS.UserShip ship = new Maticsoft.Model.SNS.UserShip();
            if (row != null)
            {
                if ((row["ActiveUserID"] != null) && (row["ActiveUserID"].ToString() != ""))
                {
                    ship.ActiveUserID = int.Parse(row["ActiveUserID"].ToString());
                }
                if ((row["PassiveUserID"] != null) && (row["PassiveUserID"].ToString() != ""))
                {
                    ship.PassiveUserID = int.Parse(row["PassiveUserID"].ToString());
                }
                if ((row["State"] != null) && (row["State"].ToString() != ""))
                {
                    ship.State = int.Parse(row["State"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    ship.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["CategoryID"] != null) && (row["CategoryID"].ToString() != ""))
                {
                    ship.CategoryID = int.Parse(row["CategoryID"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    ship.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["IsRead"] != null) && (row["IsRead"].ToString() != ""))
                {
                    if ((row["IsRead"].ToString() == "1") || (row["IsRead"].ToString().ToLower() == "true"))
                    {
                        ship.IsRead = true;
                    }
                    else
                    {
                        ship.IsRead = false;
                    }
                }
                if ((row["NickName"] != null) && (row["NickName"].ToString() != ""))
                {
                    ship.NickName = row["NickName"].ToString();
                }
                if ((row["FansCount"] != null) && (row["FansCount"].ToString() != ""))
                {
                    ship.FansCount = int.Parse(row["FansCount"].ToString());
                }
                if ((row["Singature"] != null) && (row["Singature"].ToString() != ""))
                {
                    ship.Singature = row["Singature"].ToString();
                }
            }
            return ship;
        }

        public List<Maticsoft.Model.SNS.UserShip> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserShip> list = new List<Maticsoft.Model.SNS.UserShip>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserShip item = this.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<Maticsoft.Model.SNS.UserShip> DataTableToListEx(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserShip> list = new List<Maticsoft.Model.SNS.UserShip>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserShip item = this.DataRowToModelEx(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ActiveUserID, int PassiveUserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserShip ");
            builder.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActiveUserID;
            cmdParms[1].Value = PassiveUserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int ActiveUserID, int PassiveUserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserShip");
            builder.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActiveUserID;
            cmdParms[1].Value = PassiveUserID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool FellowUser(Maticsoft.Model.SNS.UserShip model)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UsersExp set ");
            builder.Append("FansCount=FansCount+1,");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = model.PassiveUserID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Accounts_UsersExp set ");
            builder2.Append("FellowCount=FellowCount+1,");
            builder2.Append(" where UserID=@UserID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.ActiveUserID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), para);
            cmdList.Add(info2);
            if (this.Exists(model.PassiveUserID, model.ActiveUserID))
            {
                model.Type = 1;
                StringBuilder builder3 = new StringBuilder();
                builder3.Append("insert into SNS_UserShip(");
                builder3.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
                builder3.Append(" values (");
                builder3.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRead", SqlDbType.Bit, 1) };
                parameterArray3[0].Value = model.ActiveUserID;
                parameterArray3[1].Value = model.PassiveUserID;
                parameterArray3[2].Value = 1;
                parameterArray3[3].Value = model.Type;
                parameterArray3[4].Value = model.CategoryID;
                parameterArray3[5].Value = model.CreatedDate;
                parameterArray3[6].Value = model.IsRead;
                CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
                cmdList.Add(info3);
                StringBuilder builder4 = new StringBuilder();
                builder4.Append("update SNS_UserShip set ");
                builder4.Append("Type=@Type,");
                builder4.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
                parameterArray4[0].Value = model.Type;
                parameterArray4[1].Value = model.PassiveUserID;
                parameterArray4[2].Value = model.ActiveUserID;
                CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
                cmdList.Add(info4);
                if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
                {
                    return false;
                }
                return true;
            }
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("insert into SNS_UserShip(");
            builder5.Append("ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead)");
            builder5.Append(" values (");
            builder5.Append("@ActiveUserID,@PassiveUserID,@State,@Type,@CategoryID,@CreatedDate,@IsRead)");
            SqlParameter[] parameterArray5 = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRead", SqlDbType.Bit, 1) };
            parameterArray5[0].Value = model.ActiveUserID;
            parameterArray5[1].Value = model.PassiveUserID;
            parameterArray5[2].Value = model.State;
            parameterArray5[3].Value = model.Type;
            parameterArray5[4].Value = model.CategoryID;
            parameterArray5[5].Value = model.CreatedDate;
            parameterArray5[6].Value = model.IsRead;
            CommandInfo info5 = new CommandInfo(builder5.ToString(), parameterArray5);
            cmdList.Add(info5);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead ");
            builder.Append(" FROM SNS_UserShip ");
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
            builder.Append(" ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead ");
            builder.Append(" FROM SNS_UserShip ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByFansPage(int userid, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT *,NickName,FansCount,Singature FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.PassiveUserID desc");
            }
            builder.Append(")AS Row, T.*  FROM  SNS_UserShip T");
            builder.AppendFormat(" WHERE T.PassiveUserID={0}", userid);
            builder.Append(" ) TT");
            builder.Append(" inner JOIN Accounts_Users AU ON AU.UserID=ActiveUserID ");
            builder.Append(" inner JOIN Accounts_UsersExp AUE ON AUE.UserID=ActiveUserID ");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByFellowsPage(int userid, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT *,NickName,FansCount,Singature FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.PassiveUserID desc");
            }
            builder.Append(" )AS Row, T.* FROM  SNS_UserShip T ");
            builder.AppendFormat(" WHERE T.ActiveUserID={0}", userid);
            builder.Append(" ) TT");
            builder.Append(" LEFT JOIN Accounts_Users AU ON AU.UserID=PassiveUserID ");
            builder.Append(" LEFT JOIN Accounts_UsersExp AUE ON AUE.UserID=PassiveUserID ");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
                builder.Append("order by T.PassiveUserID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserShip T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.UserShip GetModel(int ActiveUserID, int PassiveUserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ActiveUserID,PassiveUserID,State,Type,CategoryID,CreatedDate,IsRead from SNS_UserShip ");
            builder.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActiveUserID;
            cmdParms[1].Value = PassiveUserID;
            new Maticsoft.Model.SNS.UserShip();
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
            builder.Append("select count(1) FROM SNS_UserShip ");
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

        public bool UnFellowUser(int UserID, int UnfellowUserID)
        {
            if (!this.Exists(UnfellowUserID, UserID))
            {
                return this.Delete(UserID, UnfellowUserID);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserShip ");
            builder.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
            para[0].Value = UserID;
            para[1].Value = UnfellowUserID;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update SNS_UserShip set ");
            builder2.Append("Type=@Type,");
            builder2.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = -1;
            parameterArray2[1].Value = UnfellowUserID;
            parameterArray2[2].Value = UserID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Update(Maticsoft.Model.SNS.UserShip model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserShip set ");
            builder.Append("State=@State,");
            builder.Append("Type=@Type,");
            builder.Append("CategoryID=@CategoryID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("IsRead=@IsRead");
            builder.Append(" where ActiveUserID=@ActiveUserID and PassiveUserID=@PassiveUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRead", SqlDbType.Bit, 1), new SqlParameter("@ActiveUserID", SqlDbType.Int, 4), new SqlParameter("@PassiveUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.State;
            cmdParms[1].Value = model.Type;
            cmdParms[2].Value = model.CategoryID;
            cmdParms[3].Value = model.CreatedDate;
            cmdParms[4].Value = model.IsRead;
            cmdParms[5].Value = model.ActiveUserID;
            cmdParms[6].Value = model.PassiveUserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

