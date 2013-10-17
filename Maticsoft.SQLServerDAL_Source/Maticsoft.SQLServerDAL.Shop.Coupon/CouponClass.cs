namespace Maticsoft.SQLServerDAL.Shop.Coupon
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Coupon;
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CouponClass : ICouponClass
    {
        public int Add(Maticsoft.Model.Shop.Coupon.CouponClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_CouponClass(");
            builder.Append("Name,Sequence,Status)");
            builder.Append(" values (");
            builder.Append("@Name,@Sequence,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Sequence;
            cmdParms[2].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Coupon.CouponClass DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Coupon.CouponClass class2 = new Maticsoft.Model.Shop.Coupon.CouponClass();
            if (row != null)
            {
                if ((row["ClassId"] != null) && (row["ClassId"].ToString() != ""))
                {
                    class2.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if (row["Name"] != null)
                {
                    class2.Name = row["Name"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    class2.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    class2.Status = int.Parse(row["Status"].ToString());
                }
            }
            return class2;
        }

        public bool Delete(int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponClass ");
            builder.Append(" where ClassId=@ClassId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ClassIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CouponClass ");
            builder.Append(" where ClassId in (" + ClassIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_CouponClass");
            builder.Append(" where ClassId=@ClassId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClassId,Name,Sequence,Status ");
            builder.Append(" FROM Shop_CouponClass ");
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
            builder.Append(" ClassId,Name,Sequence,Status ");
            builder.Append(" FROM Shop_CouponClass ");
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
                builder.Append("order by T.ClassId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_CouponClass T ");
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
            return DbHelperSQL.GetMaxID("ClassId", "Shop_CouponClass");
        }

        public Maticsoft.Model.Shop.Coupon.CouponClass GetModel(int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ClassId,Name,Sequence,Status from Shop_CouponClass ");
            builder.Append(" where ClassId=@ClassId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassId;
            new Maticsoft.Model.Shop.Coupon.CouponClass();
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
            builder.Append("select count(1) FROM Shop_CouponClass ");
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

        public int GetSequence()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT MAX(Sequence) FROM Shop_CouponClass ");
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponClass set ");
            builder.Append("Name=@Name,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Status=@Status");
            builder.Append(" where ClassId=@ClassId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Sequence;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.ClassId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateSeqByCid(int Cid, int seq)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CouponClass set ");
            builder.Append("Sequence=@Sequence");
            builder.Append(" where ClassId=@ClassId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4) };
            cmdParms[0].Value = seq;
            cmdParms[1].Value = Cid;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

