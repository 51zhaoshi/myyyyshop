namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Feedback : IFeedback
    {
        public int Add(Maticsoft.Model.Members.Feedback model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_Feedback(");
            builder.Append("TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData)");
            builder.Append(" values (");
            builder.Append("@TypeId,@Description,@UserName,@UserSex,@UserEmail,@Phone,@TelPhone,@UserCompany,@UserIP,@IsSolved,@CreatedDate,@Result,@Status,@Remark,@ExtData)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@UserSex", SqlDbType.Char, 10), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 100), new SqlParameter("@Phone", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 100), new SqlParameter("@UserCompany", SqlDbType.NVarChar, 200), new SqlParameter("@UserIP", SqlDbType.NVarChar, 20), new SqlParameter("@IsSolved", SqlDbType.Bit, 1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Result", SqlDbType.Text), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@ExtData", SqlDbType.Text) };
            cmdParms[0].Value = model.TypeId;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.UserName;
            cmdParms[3].Value = model.UserSex;
            cmdParms[4].Value = model.UserEmail;
            cmdParms[5].Value = model.Phone;
            cmdParms[6].Value = model.TelPhone;
            cmdParms[7].Value = model.UserCompany;
            cmdParms[8].Value = model.UserIP;
            cmdParms[9].Value = model.IsSolved;
            cmdParms[10].Value = model.CreatedDate;
            cmdParms[11].Value = model.Result;
            cmdParms[12].Value = model.Status;
            cmdParms[13].Value = model.Remark;
            cmdParms[14].Value = model.ExtData;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Members.Feedback DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.Feedback feedback = new Maticsoft.Model.Members.Feedback();
            if (row != null)
            {
                if ((row["FeedbackId"] != null) && (row["FeedbackId"].ToString() != ""))
                {
                    feedback.FeedbackId = int.Parse(row["FeedbackId"].ToString());
                }
                if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                {
                    feedback.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if (row["Description"] != null)
                {
                    feedback.Description = row["Description"].ToString();
                }
                if (row["UserName"] != null)
                {
                    feedback.UserName = row["UserName"].ToString();
                }
                if (row["UserSex"] != null)
                {
                    feedback.UserSex = row["UserSex"].ToString();
                }
                if (row["UserEmail"] != null)
                {
                    feedback.UserEmail = row["UserEmail"].ToString();
                }
                if (row["Phone"] != null)
                {
                    feedback.Phone = row["Phone"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    feedback.TelPhone = row["TelPhone"].ToString();
                }
                if (row["UserCompany"] != null)
                {
                    feedback.UserCompany = row["UserCompany"].ToString();
                }
                if (row["UserIP"] != null)
                {
                    feedback.UserIP = row["UserIP"].ToString();
                }
                if ((row["IsSolved"] != null) && (row["IsSolved"].ToString() != ""))
                {
                    if ((row["IsSolved"].ToString() == "1") || (row["IsSolved"].ToString().ToLower() == "true"))
                    {
                        feedback.IsSolved = true;
                    }
                    else
                    {
                        feedback.IsSolved = false;
                    }
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    feedback.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Result"] != null)
                {
                    feedback.Result = row["Result"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    feedback.Status = new int?(int.Parse(row["Status"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    feedback.Remark = row["Remark"].ToString();
                }
                if (row["ExtData"] != null)
                {
                    feedback.ExtData = row["ExtData"].ToString();
                }
            }
            return feedback;
        }

        public bool Delete(int FeedbackId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_Feedback ");
            builder.Append(" where FeedbackId=@FeedbackId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FeedbackId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FeedbackId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string FeedbackIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_Feedback ");
            builder.Append(" where FeedbackId in (" + FeedbackIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int FeedbackId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_Feedback");
            builder.Append(" where FeedbackId=@FeedbackId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FeedbackId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FeedbackId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FeedbackId,TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData ");
            builder.Append(" FROM SA_Feedback ");
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
            builder.Append(" FeedbackId,TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData ");
            builder.Append(" FROM SA_Feedback ");
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
                builder.Append("order by T.FeedbackId desc");
            }
            builder.Append(")AS Row, T.*  from SA_Feedback T ");
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
            return DbHelperSQL.GetMaxID("FeedbackId", "SA_Feedback");
        }

        public Maticsoft.Model.Members.Feedback GetModel(int FeedbackId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FeedbackId,TypeId,Description,UserName,UserSex,UserEmail,Phone,TelPhone,UserCompany,UserIP,IsSolved,CreatedDate,Result,Status,Remark,ExtData from SA_Feedback ");
            builder.Append(" where FeedbackId=@FeedbackId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FeedbackId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FeedbackId;
            new Maticsoft.Model.Members.Feedback();
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
            builder.Append("select count(1) FROM SA_Feedback ");
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

        public bool Update(Maticsoft.Model.Members.Feedback model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Feedback set ");
            builder.Append("TypeId=@TypeId,");
            builder.Append("Description=@Description,");
            builder.Append("UserName=@UserName,");
            builder.Append("UserSex=@UserSex,");
            builder.Append("UserEmail=@UserEmail,");
            builder.Append("Phone=@Phone,");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("UserCompany=@UserCompany,");
            builder.Append("UserIP=@UserIP,");
            builder.Append("IsSolved=@IsSolved,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Result=@Result,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark,");
            builder.Append("ExtData=@ExtData");
            builder.Append(" where FeedbackId=@FeedbackId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@UserSex", SqlDbType.Char, 10), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 100), new SqlParameter("@Phone", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 100), new SqlParameter("@UserCompany", SqlDbType.NVarChar, 200), new SqlParameter("@UserIP", SqlDbType.NVarChar, 20), new SqlParameter("@IsSolved", SqlDbType.Bit, 1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Result", SqlDbType.Text), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@ExtData", SqlDbType.Text), new SqlParameter("@FeedbackId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeId;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.UserName;
            cmdParms[3].Value = model.UserSex;
            cmdParms[4].Value = model.UserEmail;
            cmdParms[5].Value = model.Phone;
            cmdParms[6].Value = model.TelPhone;
            cmdParms[7].Value = model.UserCompany;
            cmdParms[8].Value = model.UserIP;
            cmdParms[9].Value = model.IsSolved;
            cmdParms[10].Value = model.CreatedDate;
            cmdParms[11].Value = model.Result;
            cmdParms[12].Value = model.Status;
            cmdParms[13].Value = model.Remark;
            cmdParms[14].Value = model.ExtData;
            cmdParms[15].Value = model.FeedbackId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

