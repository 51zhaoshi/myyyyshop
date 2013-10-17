namespace Maticsoft.SQLServerDAL.Shop.Coupon
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Coupon;
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CouponHistory : ICouponHistory
    {
        public bool Add(Maticsoft.Model.Shop.Coupon.CouponHistory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_CouponHistory(");
            builder.Append("CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate)");
            builder.Append(" values (");
            builder.Append("@CouponCode,@CategoryId,@ClassId,@SupplierId,@RuleId,@CouponName,@CouponPwd,@UserId,@UserEmail,@Status,@CouponPrice,@LimitPrice,@NeedPoint,@IsPwd,@IsReuse,@StartDate,@EndDate,@GenerateTime,@UsedDate)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@CouponName", SqlDbType.NVarChar, 200), new SqlParameter("@CouponPwd", SqlDbType.NVarChar, 200), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CouponPrice", SqlDbType.Money, 8), new SqlParameter("@LimitPrice", SqlDbType.Money, 8), new SqlParameter("@NeedPoint", SqlDbType.Int, 4), new SqlParameter("@IsPwd", SqlDbType.Int, 4), new SqlParameter("@IsReuse", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), 
                new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@GenerateTime", SqlDbType.DateTime), new SqlParameter("@UsedDate", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.CouponCode;
            cmdParms[1].Value = model.CategoryId;
            cmdParms[2].Value = model.ClassId;
            cmdParms[3].Value = model.SupplierId;
            cmdParms[4].Value = model.RuleId;
            cmdParms[5].Value = model.CouponName;
            cmdParms[6].Value = model.CouponPwd;
            cmdParms[7].Value = model.UserId;
            cmdParms[8].Value = model.UserEmail;
            cmdParms[9].Value = model.Status;
            cmdParms[10].Value = model.CouponPrice;
            cmdParms[11].Value = model.LimitPrice;
            cmdParms[12].Value = model.NeedPoint;
            cmdParms[13].Value = model.IsPwd;
            cmdParms[14].Value = model.IsReuse;
            cmdParms[15].Value = model.StartDate;
            cmdParms[0x10].Value = model.EndDate;
            cmdParms[0x11].Value = model.GenerateTime;
            cmdParms[0x12].Value = model.UsedDate;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Coupon.CouponHistory DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponHistory history = new Maticsoft.Model.Shop.Coupon.CouponHistory();
            if (row != null)
            {
                if (row["CouponCode"] != null)
                {
                    history.CouponCode = row["CouponCode"].ToString();
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    history.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["ClassId"] != null) && (row["ClassId"].ToString() != ""))
                {
                    history.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    history.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if ((row["RuleId"] != null) && (row["RuleId"].ToString() != ""))
                {
                    history.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if (row["CouponName"] != null)
                {
                    history.CouponName = row["CouponName"].ToString();
                }
                if (row["CouponPwd"] != null)
                {
                    history.CouponPwd = row["CouponPwd"].ToString();
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    history.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserEmail"] != null)
                {
                    history.UserEmail = row["UserEmail"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    history.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CouponPrice"] != null) && (row["CouponPrice"].ToString() != ""))
                {
                    history.CouponPrice = decimal.Parse(row["CouponPrice"].ToString());
                }
                if ((row["LimitPrice"] != null) && (row["LimitPrice"].ToString() != ""))
                {
                    history.LimitPrice = decimal.Parse(row["LimitPrice"].ToString());
                }
                if ((row["NeedPoint"] != null) && (row["NeedPoint"].ToString() != ""))
                {
                    history.NeedPoint = int.Parse(row["NeedPoint"].ToString());
                }
                if ((row["IsPwd"] != null) && (row["IsPwd"].ToString() != ""))
                {
                    history.IsPwd = int.Parse(row["IsPwd"].ToString());
                }
                if ((row["IsReuse"] != null) && (row["IsReuse"].ToString() != ""))
                {
                    history.IsReuse = int.Parse(row["IsReuse"].ToString());
                }
                if ((row["StartDate"] != null) && (row["StartDate"].ToString() != ""))
                {
                    history.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if ((row["EndDate"] != null) && (row["EndDate"].ToString() != ""))
                {
                    history.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if ((row["GenerateTime"] != null) && (row["GenerateTime"].ToString() != ""))
                {
                    history.GenerateTime = DateTime.Parse(row["GenerateTime"].ToString());
                }
                if ((row["UsedDate"] != null) && (row["UsedDate"].ToString() != ""))
                {
                    history.UsedDate = new DateTime?(DateTime.Parse(row["UsedDate"].ToString()));
                }
            }
            return history;
        }

        public bool Delete(string CouponCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponHistory ");
            builder.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CouponCodelist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponHistory ");
            builder.Append(" where CouponCode in (" + CouponCodelist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string CouponCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_CouponHistory");
            builder.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate ");
            builder.Append(" FROM Shop_CouponHistory ");
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
            builder.Append(" CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate ");
            builder.Append(" FROM Shop_CouponHistory ");
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
                builder.Append("order by T.CouponCode desc");
            }
            builder.Append(")AS Row, T.*  from Shop_CouponHistory T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Coupon.CouponHistory GetModel(string CouponCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate from Shop_CouponHistory ");
            builder.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            new Maticsoft.Model.Shop.Coupon.CouponHistory();
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
            builder.Append("select count(1) FROM Shop_CouponHistory ");
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

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponHistory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponHistory set ");
            builder.Append("CategoryId=@CategoryId,");
            builder.Append("ClassId=@ClassId,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("RuleId=@RuleId,");
            builder.Append("CouponName=@CouponName,");
            builder.Append("CouponPwd=@CouponPwd,");
            builder.Append("UserId=@UserId,");
            builder.Append("UserEmail=@UserEmail,");
            builder.Append("Status=@Status,");
            builder.Append("CouponPrice=@CouponPrice,");
            builder.Append("LimitPrice=@LimitPrice,");
            builder.Append("NeedPoint=@NeedPoint,");
            builder.Append("IsPwd=@IsPwd,");
            builder.Append("IsReuse=@IsReuse,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("GenerateTime=@GenerateTime,");
            builder.Append("UsedDate=@UsedDate");
            builder.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@CouponName", SqlDbType.NVarChar, 200), new SqlParameter("@CouponPwd", SqlDbType.NVarChar, 200), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CouponPrice", SqlDbType.Money, 8), new SqlParameter("@LimitPrice", SqlDbType.Money, 8), new SqlParameter("@NeedPoint", SqlDbType.Int, 4), new SqlParameter("@IsPwd", SqlDbType.Int, 4), new SqlParameter("@IsReuse", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), 
                new SqlParameter("@GenerateTime", SqlDbType.DateTime), new SqlParameter("@UsedDate", SqlDbType.DateTime), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200)
             };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.ClassId;
            cmdParms[2].Value = model.SupplierId;
            cmdParms[3].Value = model.RuleId;
            cmdParms[4].Value = model.CouponName;
            cmdParms[5].Value = model.CouponPwd;
            cmdParms[6].Value = model.UserId;
            cmdParms[7].Value = model.UserEmail;
            cmdParms[8].Value = model.Status;
            cmdParms[9].Value = model.CouponPrice;
            cmdParms[10].Value = model.LimitPrice;
            cmdParms[11].Value = model.NeedPoint;
            cmdParms[12].Value = model.IsPwd;
            cmdParms[13].Value = model.IsReuse;
            cmdParms[14].Value = model.StartDate;
            cmdParms[15].Value = model.EndDate;
            cmdParms[0x10].Value = model.GenerateTime;
            cmdParms[0x11].Value = model.UsedDate;
            cmdParms[0x12].Value = model.CouponCode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

