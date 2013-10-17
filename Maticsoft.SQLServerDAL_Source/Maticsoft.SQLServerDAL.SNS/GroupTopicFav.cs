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

    public class GroupTopicFav : IGroupTopicFav
    {
        public int Add(Maticsoft.Model.SNS.GroupTopicFav model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupTopicFav(");
            builder.Append("CreatedUserID,CreatedDate,TopicID,Tags,Remark)");
            builder.Append(" values (");
            builder.Append("@CreatedUserID,@CreatedDate,@TopicID,@Tags,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.TopicID;
            cmdParms[3].Value = model.Tags;
            cmdParms[4].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddEx(Maticsoft.Model.SNS.GroupTopicFav model)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupTopicFav(");
            builder.Append("CreatedUserID,CreatedDate,TopicID,Tags,Remark)");
            builder.Append(" values (");
            builder.Append("@CreatedUserID,@CreatedDate,@TopicID,@Tags,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            para[0].Value = model.CreatedUserID;
            para[1].Value = model.CreatedDate;
            para[2].Value = model.TopicID;
            para[3].Value = model.Tags;
            para[4].Value = model.Remark;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Accounts_UsersExp set FavTopicCount=FavTopicCount-1");
            builder2.Append(" where UserID=@UserID  ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.CreatedUserID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public Maticsoft.Model.SNS.GroupTopicFav DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.GroupTopicFav fav = new Maticsoft.Model.SNS.GroupTopicFav();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    fav.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    fav.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    fav.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["TopicID"] != null) && (row["TopicID"].ToString() != ""))
                {
                    fav.TopicID = int.Parse(row["TopicID"].ToString());
                }
                if (row["Tags"] != null)
                {
                    fav.Tags = row["Tags"].ToString();
                }
                if (row["Remark"] != null)
                {
                    fav.Remark = row["Remark"].ToString();
                }
            }
            return fav;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupTopicFav ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupTopicFav ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TopicID, int CreatedUserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_GroupTopicFav");
            builder.Append(" where CreatedUserID=@CreatedUserID and TopicID=@TopicID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CreatedUserID;
            cmdParms[1].Value = TopicID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,CreatedUserID,CreatedDate,TopicID,Tags,Remark ");
            builder.Append(" FROM SNS_GroupTopicFav ");
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
            builder.Append(" ID,CreatedUserID,CreatedDate,TopicID,Tags,Remark ");
            builder.Append(" FROM SNS_GroupTopicFav ");
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
            builder.Append(")AS Row, T.*  from SNS_GroupTopicFav T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.GroupTopicFav GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,CreatedUserID,CreatedDate,TopicID,Tags,Remark from SNS_GroupTopicFav ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.GroupTopicFav();
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
            builder.Append("select count(1) FROM SNS_GroupTopicFav ");
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

        public bool Update(Maticsoft.Model.SNS.GroupTopicFav model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTopicFav set ");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("TopicID=@TopicID,");
            builder.Append("Tags=@Tags,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.TopicID;
            cmdParms[3].Value = model.Tags;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

