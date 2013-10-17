namespace Maticsoft.SQLServerDAL.Shop.Coupon
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Coupon;
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CouponInfo : ICouponInfo
    {
        public bool Add(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_CouponInfo(");
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

        public bool AddHistory(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_CouponHistory(");
            builder.Append("CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate)");
            builder.Append(" values (");
            builder.Append("@CouponCode,@CategoryId,@ClassId,@SupplierId,@RuleId,@CouponName,@CouponPwd,@UserId,@UserEmail,@Status,@CouponPrice,@LimitPrice,@NeedPoint,@IsPwd,@IsReuse,@StartDate,@EndDate,@GenerateTime,@UsedDate)");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@CouponName", SqlDbType.NVarChar, 200), new SqlParameter("@CouponPwd", SqlDbType.NVarChar, 200), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CouponPrice", SqlDbType.Money, 8), new SqlParameter("@LimitPrice", SqlDbType.Money, 8), new SqlParameter("@NeedPoint", SqlDbType.Int, 4), new SqlParameter("@IsPwd", SqlDbType.Int, 4), new SqlParameter("@IsReuse", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), 
                new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@GenerateTime", SqlDbType.DateTime), new SqlParameter("@UsedDate", SqlDbType.DateTime)
             };
            para[0].Value = model.CouponCode;
            para[1].Value = model.CategoryId;
            para[2].Value = model.ClassId;
            para[3].Value = model.SupplierId;
            para[4].Value = model.RuleId;
            para[5].Value = model.CouponName;
            para[6].Value = model.CouponPwd;
            para[7].Value = model.UserId;
            para[8].Value = model.UserEmail;
            para[9].Value = model.Status;
            para[10].Value = model.CouponPrice;
            para[11].Value = model.LimitPrice;
            para[12].Value = model.NeedPoint;
            para[13].Value = model.IsPwd;
            para[14].Value = model.IsReuse;
            para[15].Value = model.StartDate;
            para[0x10].Value = model.EndDate;
            para[0x11].Value = model.GenerateTime;
            para[0x12].Value = model.UsedDate;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Shop_CouponInfo ");
            builder2.Append(" where CouponCode=@CouponCode  ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            parameterArray2[0].Value = model.CouponCode;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponInfo info = new Maticsoft.Model.Shop.Coupon.CouponInfo();
            if (row != null)
            {
                if (row["CouponCode"] != null)
                {
                    info.CouponCode = row["CouponCode"].ToString();
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    info.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["ClassId"] != null) && (row["ClassId"].ToString() != ""))
                {
                    info.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    info.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if ((row["RuleId"] != null) && (row["RuleId"].ToString() != ""))
                {
                    info.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if (row["CouponName"] != null)
                {
                    info.CouponName = row["CouponName"].ToString();
                }
                if (row["CouponPwd"] != null)
                {
                    info.CouponPwd = row["CouponPwd"].ToString();
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    info.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserEmail"] != null)
                {
                    info.UserEmail = row["UserEmail"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    info.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CouponPrice"] != null) && (row["CouponPrice"].ToString() != ""))
                {
                    info.CouponPrice = decimal.Parse(row["CouponPrice"].ToString());
                }
                if ((row["LimitPrice"] != null) && (row["LimitPrice"].ToString() != ""))
                {
                    info.LimitPrice = decimal.Parse(row["LimitPrice"].ToString());
                }
                if ((row["NeedPoint"] != null) && (row["NeedPoint"].ToString() != ""))
                {
                    info.NeedPoint = int.Parse(row["NeedPoint"].ToString());
                }
                if ((row["IsPwd"] != null) && (row["IsPwd"].ToString() != ""))
                {
                    info.IsPwd = int.Parse(row["IsPwd"].ToString());
                }
                if ((row["IsReuse"] != null) && (row["IsReuse"].ToString() != ""))
                {
                    info.IsReuse = int.Parse(row["IsReuse"].ToString());
                }
                if ((row["StartDate"] != null) && (row["StartDate"].ToString() != ""))
                {
                    info.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if ((row["EndDate"] != null) && (row["EndDate"].ToString() != ""))
                {
                    info.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if ((row["GenerateTime"] != null) && (row["GenerateTime"].ToString() != ""))
                {
                    info.GenerateTime = DateTime.Parse(row["GenerateTime"].ToString());
                }
                if ((row["UsedDate"] != null) && (row["UsedDate"].ToString() != ""))
                {
                    info.UsedDate = new DateTime?(DateTime.Parse(row["UsedDate"].ToString()));
                }
            }
            return info;
        }

        public bool Delete(string CouponCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponInfo ");
            builder.Append(" where CouponCode='@CouponCode' ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int RuleId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponInfo ");
            builder.Append(" where RuleId=@RuleId  ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            para[0].Value = RuleId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Shop_CouponHistory ");
            builder2.Append(" where RuleId=@RuleId  ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = RuleId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string CouponCodelist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponInfo ");
            builder.Append(" where CouponCode in (" + CouponCodelist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string CouponCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_CouponInfo");
            builder.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, bool IsExpired)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 *  from Shop_CouponInfo");
            builder.Append(" where CouponCode=@CouponCode ");
            if (!IsExpired)
            {
                builder.AppendFormat(" and  EndDate>='{0}'", DateTime.Now);
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            new Maticsoft.Model.Shop.Coupon.CouponInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, string pwd, bool IsExpired)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 *  from Shop_CouponInfo");
            builder.Append(" where CouponCode=@CouponCode  and CouponPwd=@CouponPwd");
            if (!IsExpired)
            {
                builder.AppendFormat(" and  EndDate>='{0}'", DateTime.Now);
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200), new SqlParameter("@CouponPwd", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            cmdParms[1].Value = pwd;
            new Maticsoft.Model.Shop.Coupon.CouponInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate ");
            builder.Append(" FROM Shop_CouponInfo ");
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
            builder.Append(" FROM Shop_CouponInfo ");
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
            builder.Append(")AS Row, T.*  from Shop_CouponInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetModel(string CouponCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CouponCode,CategoryId,ClassId,SupplierId,RuleId,CouponName,CouponPwd,UserId,UserEmail,Status,CouponPrice,LimitPrice,NeedPoint,IsPwd,IsReuse,StartDate,EndDate,GenerateTime,UsedDate from Shop_CouponInfo ");
            builder.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = CouponCode;
            new Maticsoft.Model.Shop.Coupon.CouponInfo();
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
            builder.Append("select count(1) FROM Shop_CouponInfo ");
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

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponInfo set ");
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

        public bool UpdateUser(int ruleId, int userId, string userEmail)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponInfo set ");
            builder.Append("UserId=@UserId,");
            builder.Append("UserEmail=@UserEmail,");
            builder.Append("Status=@Status");
            builder.Append(" where CouponCode=(SELECT TOP 1 CouponCode FROM Shop_CouponInfo WHERE ruleId=@ruleId AND Status=0 )  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ruleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = userEmail;
            cmdParms[2].Value = 1;
            cmdParms[3].Value = ruleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateUser(string couponCode, int userId, string userEmail)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponInfo set ");
            builder.Append("UserId=@UserId,");
            builder.Append("UserEmail=@UserEmail,");
            builder.Append("Status=@Status");
            builder.Append(" where CouponCode=@CouponCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = userEmail;
            cmdParms[2].Value = 1;
            cmdParms[3].Value = couponCode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UseCoupon(string couponCode, int userId, string userEmail)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponInfo set ");
            builder.Append("UserId=@UserId,");
            builder.Append("UserEmail=@UserEmail,");
            builder.Append("Status=@Status,");
            builder.Append("UsedDate=@UsedDate");
            builder.Append(" where CouponCode=@CouponCode  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserEmail", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 200), new SqlParameter("@UsedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = userEmail;
            cmdParms[2].Value = 2;
            cmdParms[3].Value = couponCode;
            cmdParms[4].Value = DateTime.Now;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

