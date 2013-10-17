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

    public class CouponRule : ICouponRule
    {
        public int Add(Maticsoft.Model.Shop.Coupon.CouponRule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_CouponRule(");
            builder.Append("CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId)");
            builder.Append(" values (");
            builder.Append("@CategoryId,@ClassId,@SupplierId,@Name,@PreName,@ImageUrl,@CouponPrice,@LimitPrice,@CouponDesc,@SendCount,@NeedPoint,@IsPwd,@IsReuse,@Status,@Recommend,@StartDate,@EndDate,@CreateDate,@CreateUserId)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@PreName", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CouponPrice", SqlDbType.Money, 8), new SqlParameter("@LimitPrice", SqlDbType.Money, 8), new SqlParameter("@CouponDesc", SqlDbType.NVarChar, -1), new SqlParameter("@SendCount", SqlDbType.Int, 4), new SqlParameter("@NeedPoint", SqlDbType.Int, 4), new SqlParameter("@IsPwd", SqlDbType.Int, 4), new SqlParameter("@IsReuse", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Recommend", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), 
                new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@CreateUserId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.ClassId;
            cmdParms[2].Value = model.SupplierId;
            cmdParms[3].Value = model.Name;
            cmdParms[4].Value = model.PreName;
            cmdParms[5].Value = model.ImageUrl;
            cmdParms[6].Value = model.CouponPrice;
            cmdParms[7].Value = model.LimitPrice;
            cmdParms[8].Value = model.CouponDesc;
            cmdParms[9].Value = model.SendCount;
            cmdParms[10].Value = model.NeedPoint;
            cmdParms[11].Value = model.IsPwd;
            cmdParms[12].Value = model.IsReuse;
            cmdParms[13].Value = model.Status;
            cmdParms[14].Value = model.Recommend;
            cmdParms[15].Value = model.StartDate;
            cmdParms[0x10].Value = model.EndDate;
            cmdParms[0x11].Value = model.CreateDate;
            cmdParms[0x12].Value = model.CreateUserId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Coupon.CouponRule DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponRule rule = new Maticsoft.Model.Shop.Coupon.CouponRule();
            if (row != null)
            {
                if ((row["RuleId"] != null) && (row["RuleId"].ToString() != ""))
                {
                    rule.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    rule.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["ClassId"] != null) && (row["ClassId"].ToString() != ""))
                {
                    rule.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    rule.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["Name"] != null)
                {
                    rule.Name = row["Name"].ToString();
                }
                if (row["PreName"] != null)
                {
                    rule.PreName = row["PreName"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    rule.ImageUrl = row["ImageUrl"].ToString();
                }
                if ((row["CouponPrice"] != null) && (row["CouponPrice"].ToString() != ""))
                {
                    rule.CouponPrice = decimal.Parse(row["CouponPrice"].ToString());
                }
                if ((row["LimitPrice"] != null) && (row["LimitPrice"].ToString() != ""))
                {
                    rule.LimitPrice = decimal.Parse(row["LimitPrice"].ToString());
                }
                if (row["CouponDesc"] != null)
                {
                    rule.CouponDesc = row["CouponDesc"].ToString();
                }
                if ((row["SendCount"] != null) && (row["SendCount"].ToString() != ""))
                {
                    rule.SendCount = int.Parse(row["SendCount"].ToString());
                }
                if ((row["NeedPoint"] != null) && (row["NeedPoint"].ToString() != ""))
                {
                    rule.NeedPoint = int.Parse(row["NeedPoint"].ToString());
                }
                if ((row["IsPwd"] != null) && (row["IsPwd"].ToString() != ""))
                {
                    rule.IsPwd = int.Parse(row["IsPwd"].ToString());
                }
                if ((row["IsReuse"] != null) && (row["IsReuse"].ToString() != ""))
                {
                    rule.IsReuse = int.Parse(row["IsReuse"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    rule.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["Recommend"] != null) && (row["Recommend"].ToString() != ""))
                {
                    rule.Recommend = int.Parse(row["Recommend"].ToString());
                }
                if ((row["StartDate"] != null) && (row["StartDate"].ToString() != ""))
                {
                    rule.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if ((row["EndDate"] != null) && (row["EndDate"].ToString() != ""))
                {
                    rule.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if ((row["CreateDate"] != null) && (row["CreateDate"].ToString() != ""))
                {
                    rule.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if ((row["CreateUserId"] != null) && (row["CreateUserId"].ToString() != ""))
                {
                    rule.CreateUserId = int.Parse(row["CreateUserId"].ToString());
                }
            }
            return rule;
        }

        public bool Delete(int RuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponRule ");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int RuleId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponRule ");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            para[0].Value = RuleId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Shop_CouponInfo ");
            builder2.Append(" where RuleId=@RuleId  ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = RuleId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from Shop_CouponHistory ");
            builder3.Append(" where RuleId=@RuleId  ");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            parameterArray3[0].Value = RuleId;
            CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(info3);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string RuleIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponRule ");
            builder.Append(" where RuleId in (" + RuleIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int RuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_CouponRule");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RuleId,CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId ");
            builder.Append(" FROM Shop_CouponRule ");
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
            builder.Append(" RuleId,CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId ");
            builder.Append(" FROM Shop_CouponRule ");
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
                builder.Append("order by T.RuleId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_CouponRule T ");
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
            return DbHelperSQL.GetMaxID("RuleId", "Shop_CouponRule");
        }

        public Maticsoft.Model.Shop.Coupon.CouponRule GetModel(int RuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RuleId,CategoryId,ClassId,SupplierId,Name,PreName,ImageUrl,CouponPrice,LimitPrice,CouponDesc,SendCount,NeedPoint,IsPwd,IsReuse,Status,Recommend,StartDate,EndDate,CreateDate,CreateUserId from Shop_CouponRule ");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            new Maticsoft.Model.Shop.Coupon.CouponRule();
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
            builder.Append("select count(1) FROM Shop_CouponRule ");
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

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponRule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponRule set ");
            builder.Append("CategoryId=@CategoryId,");
            builder.Append("ClassId=@ClassId,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("Name=@Name,");
            builder.Append("PreName=@PreName,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("CouponPrice=@CouponPrice,");
            builder.Append("LimitPrice=@LimitPrice,");
            builder.Append("CouponDesc=@CouponDesc,");
            builder.Append("SendCount=@SendCount,");
            builder.Append("NeedPoint=@NeedPoint,");
            builder.Append("IsPwd=@IsPwd,");
            builder.Append("IsReuse=@IsReuse,");
            builder.Append("Status=@Status,");
            builder.Append("Recommend=@Recommend,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("CreateDate=@CreateDate,");
            builder.Append("CreateUserId=@CreateUserId");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@PreName", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CouponPrice", SqlDbType.Money, 8), new SqlParameter("@LimitPrice", SqlDbType.Money, 8), new SqlParameter("@CouponDesc", SqlDbType.NVarChar, -1), new SqlParameter("@SendCount", SqlDbType.Int, 4), new SqlParameter("@NeedPoint", SqlDbType.Int, 4), new SqlParameter("@IsPwd", SqlDbType.Int, 4), new SqlParameter("@IsReuse", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Recommend", SqlDbType.Int, 4), new SqlParameter("@StartDate", SqlDbType.DateTime), 
                new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@CreateUserId", SqlDbType.Int, 4), new SqlParameter("@RuleId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.ClassId;
            cmdParms[2].Value = model.SupplierId;
            cmdParms[3].Value = model.Name;
            cmdParms[4].Value = model.PreName;
            cmdParms[5].Value = model.ImageUrl;
            cmdParms[6].Value = model.CouponPrice;
            cmdParms[7].Value = model.LimitPrice;
            cmdParms[8].Value = model.CouponDesc;
            cmdParms[9].Value = model.SendCount;
            cmdParms[10].Value = model.NeedPoint;
            cmdParms[11].Value = model.IsPwd;
            cmdParms[12].Value = model.IsReuse;
            cmdParms[13].Value = model.Status;
            cmdParms[14].Value = model.Recommend;
            cmdParms[15].Value = model.StartDate;
            cmdParms[0x10].Value = model.EndDate;
            cmdParms[0x11].Value = model.CreateDate;
            cmdParms[0x12].Value = model.CreateUserId;
            cmdParms[0x13].Value = model.RuleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

