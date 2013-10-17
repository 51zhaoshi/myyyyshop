namespace Maticsoft.SQLServerDAL.Poll
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserPoll : IUserPoll
    {
        public void Add(Maticsoft.Model.Poll.UserPoll model)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            builder2.Append("UserID,");
            builder3.Append(model.UserID + ",");
            if (model.TopicID.HasValue)
            {
                builder2.Append("TopicID,");
                builder3.Append(model.TopicID + ",");
            }
            if (model.OptionID.HasValue)
            {
                builder2.Append("OptionID,");
                builder3.Append(model.OptionID + ",");
            }
            builder2.Append("CreatTime,");
            builder3.Append("'" + DateTime.Now.ToString() + "',");
            builder2.Append("UserIP");
            builder3.Append("'" + model.UserIP + "'");
            builder.Append("insert into Poll_UserPoll(");
            builder.Append(builder2.ToString());
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(builder3.ToString());
            builder.Append(")");
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("update Poll_Options set ");
            builder4.Append("SubmitNum=SubmitNum+1");
            builder4.Append(" where ID=" + model.OptionID);
            DbHelperSQL.ExecuteSqlTran(new List<string> { builder.ToString(), builder4.ToString() });
        }

        public bool Add2(Maticsoft.Model.Poll.UserPoll model)
        {
            List<string> sQLStringList = new List<string>();
            if ((model == null) || string.IsNullOrWhiteSpace(model.OptionIDList))
            {
                return false;
            }
            string[] strArray = model.OptionIDList.Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Poll_UserPoll(UserID,TopicID,OptionID,CreatTime,UserIP) values ");
            foreach (string str in strArray)
            {
                builder.AppendFormat(" ( {0},{1},{2},'{3}','{4}' ),", new object[] { model.UserID, model.TopicID, str, DateTime.Now, model.UserIP });
            }
            string item = builder.ToString().TrimEnd(new char[] { ',' });
            sQLStringList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Poll_Options set ");
            builder2.Append("SubmitNum=SubmitNum+1");
            builder2.Append(" where ID  in (" + model.OptionIDList + ")");
            sQLStringList.Add(builder2.ToString());
            return (DbHelperSQL.ExecuteSqlTran(sQLStringList) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Poll_UserPoll ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListInnerJoin(int userid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Poll_UserPoll AS userpoll  INNER JOIN dbo.Poll_Options  AS opt ON userpoll.TopicID = opt.TopicID AND userpoll.OptionID=opt.ID ");
            if (userid > 0)
            {
                builder.AppendFormat("  AND  UserID ={0} ", userid);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetUserByForm(int FormID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(distinct userid) from Poll_UserPoll ");
            if (FormID > 0)
            {
                builder.Append(" where TopicID in ( ");
                builder.Append(" select ID from Poll_Topics where FormID=" + FormID);
                builder.Append(" )");
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (!object.Equals(single, null) && !object.Equals(single, DBNull.Value))
            {
                return int.Parse(single.ToString());
            }
            return 0;
        }

        public void Update(Maticsoft.Model.Poll.UserPoll model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Poll_UserPoll set ");
            builder.Append("UserID=@UserID,");
            builder.Append("TopicID=@TopicID,");
            builder.Append("OptionID=@OptionID,");
            builder.Append("CreatTime=@CreatTime");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@OptionID", SqlDbType.Int, 4), new SqlParameter("@CreatTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.TopicID;
            cmdParms[2].Value = model.OptionID;
            cmdParms[3].Value = model.CreatTime;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

