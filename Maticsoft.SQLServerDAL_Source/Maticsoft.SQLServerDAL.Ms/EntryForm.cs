namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class EntryForm : IEntryForm
    {
        public int Add(Maticsoft.Model.Ms.EntryForm model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_EntryForm(");
            builder.Append("UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State)");
            builder.Append(" values (");
            builder.Append("@UserName,@Age,@Email,@TelPhone,@Phone,@QQ,@MSN,@HouseAddress,@CompanyAddress,@RegionId,@Sex,@Description,@remark,@State)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Age", SqlDbType.Int, 4), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@QQ", SqlDbType.NVarChar, 50), new SqlParameter("@MSN", SqlDbType.NVarChar, 100), new SqlParameter("@HouseAddress", SqlDbType.NVarChar, 200), new SqlParameter("@CompanyAddress", SqlDbType.NVarChar, 200), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Sex", SqlDbType.Char, 10), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@remark", SqlDbType.NVarChar, 300), new SqlParameter("@State", SqlDbType.SmallInt, 2) };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.Age;
            cmdParms[2].Value = model.Email;
            cmdParms[3].Value = model.TelPhone;
            cmdParms[4].Value = model.Phone;
            cmdParms[5].Value = model.QQ;
            cmdParms[6].Value = model.MSN;
            cmdParms[7].Value = model.HouseAddress;
            cmdParms[8].Value = model.CompanyAddress;
            cmdParms[9].Value = model.RegionId;
            cmdParms[10].Value = model.Sex;
            cmdParms[11].Value = model.Description;
            cmdParms[12].Value = model.Remark;
            cmdParms[13].Value = model.State;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_EntryForm ");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_EntryForm ");
            builder.Append(" where Id in (" + Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_EntryForm");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State ");
            builder.Append(" FROM Ms_EntryForm ");
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
            builder.Append(" Id,UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State ");
            builder.Append(" FROM Ms_EntryForm ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.Id desc");
            }
            builder.Append(")AS Row, T.*  from Ms_EntryForm T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Ms_EntryForm");
        }

        public Maticsoft.Model.Ms.EntryForm GetModel(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Id,UserName,Age,Email,TelPhone,Phone,QQ,MSN,HouseAddress,CompanyAddress,RegionId,Sex,Description,remark,State from Ms_EntryForm ");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            Maticsoft.Model.Ms.EntryForm form = new Maticsoft.Model.Ms.EntryForm();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["Id"] != null) && (set.Tables[0].Rows[0]["Id"].ToString() != ""))
            {
                form.Id = int.Parse(set.Tables[0].Rows[0]["Id"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserName"] != null) && (set.Tables[0].Rows[0]["UserName"].ToString() != ""))
            {
                form.UserName = set.Tables[0].Rows[0]["UserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Age"] != null) && (set.Tables[0].Rows[0]["Age"].ToString() != ""))
            {
                form.Age = new int?(int.Parse(set.Tables[0].Rows[0]["Age"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Email"] != null) && (set.Tables[0].Rows[0]["Email"].ToString() != ""))
            {
                form.Email = set.Tables[0].Rows[0]["Email"].ToString();
            }
            if ((set.Tables[0].Rows[0]["TelPhone"] != null) && (set.Tables[0].Rows[0]["TelPhone"].ToString() != ""))
            {
                form.TelPhone = set.Tables[0].Rows[0]["TelPhone"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Phone"] != null) && (set.Tables[0].Rows[0]["Phone"].ToString() != ""))
            {
                form.Phone = set.Tables[0].Rows[0]["Phone"].ToString();
            }
            if ((set.Tables[0].Rows[0]["QQ"] != null) && (set.Tables[0].Rows[0]["QQ"].ToString() != ""))
            {
                form.QQ = set.Tables[0].Rows[0]["QQ"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MSN"] != null) && (set.Tables[0].Rows[0]["MSN"].ToString() != ""))
            {
                form.MSN = set.Tables[0].Rows[0]["MSN"].ToString();
            }
            if ((set.Tables[0].Rows[0]["HouseAddress"] != null) && (set.Tables[0].Rows[0]["HouseAddress"].ToString() != ""))
            {
                form.HouseAddress = set.Tables[0].Rows[0]["HouseAddress"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CompanyAddress"] != null) && (set.Tables[0].Rows[0]["CompanyAddress"].ToString() != ""))
            {
                form.CompanyAddress = set.Tables[0].Rows[0]["CompanyAddress"].ToString();
            }
            if ((set.Tables[0].Rows[0]["RegionId"] != null) && (set.Tables[0].Rows[0]["RegionId"].ToString() != ""))
            {
                form.RegionId = new int?(int.Parse(set.Tables[0].Rows[0]["RegionId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Sex"] != null) && (set.Tables[0].Rows[0]["Sex"].ToString() != ""))
            {
                form.Sex = set.Tables[0].Rows[0]["Sex"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                form.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["remark"] != null) && (set.Tables[0].Rows[0]["remark"].ToString() != ""))
            {
                form.Remark = set.Tables[0].Rows[0]["remark"].ToString();
            }
            if ((set.Tables[0].Rows[0]["State"] != null) && (set.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                form.State = new int?(int.Parse(set.Tables[0].Rows[0]["State"].ToString()));
            }
            return form;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Ms_EntryForm ");
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

        public bool Update(Maticsoft.Model.Ms.EntryForm model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_EntryForm set ");
            builder.Append("UserName=@UserName,");
            builder.Append("Age=@Age,");
            builder.Append("Email=@Email,");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("Phone=@Phone,");
            builder.Append("QQ=@QQ,");
            builder.Append("MSN=@MSN,");
            builder.Append("HouseAddress=@HouseAddress,");
            builder.Append("CompanyAddress=@CompanyAddress,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("Sex=@Sex,");
            builder.Append("Description=@Description,");
            builder.Append("remark=@remark,");
            builder.Append("State=@State");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Age", SqlDbType.Int, 4), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@QQ", SqlDbType.NVarChar, 50), new SqlParameter("@MSN", SqlDbType.NVarChar, 100), new SqlParameter("@HouseAddress", SqlDbType.NVarChar, 200), new SqlParameter("@CompanyAddress", SqlDbType.NVarChar, 200), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Sex", SqlDbType.Char, 10), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@remark", SqlDbType.NVarChar, 300), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.Age;
            cmdParms[2].Value = model.Email;
            cmdParms[3].Value = model.TelPhone;
            cmdParms[4].Value = model.Phone;
            cmdParms[5].Value = model.QQ;
            cmdParms[6].Value = model.MSN;
            cmdParms[7].Value = model.HouseAddress;
            cmdParms[8].Value = model.CompanyAddress;
            cmdParms[9].Value = model.RegionId;
            cmdParms[10].Value = model.Sex;
            cmdParms[11].Value = model.Description;
            cmdParms[12].Value = model.Remark;
            cmdParms[13].Value = model.State;
            cmdParms[14].Value = model.Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_EntryForm set " + strWhere);
            builder.Append(" where Id in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

