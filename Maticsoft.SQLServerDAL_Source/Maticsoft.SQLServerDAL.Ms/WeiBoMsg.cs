namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class WeiBoMsg : IWeiBoMsg
    {
        public int Add(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_WeiBoMsg(");
            builder.Append("WeiboMsg,ImageUrl,CreateDate,PublishDate)");
            builder.Append(" values (");
            builder.Append("@WeiboMsg,@ImageUrl,@CreateDate,@PublishDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiboMsg", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@PublishDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.WeiboMsg;
            cmdParms[1].Value = model.ImageUrl;
            cmdParms[2].Value = model.CreateDate;
            cmdParms[3].Value = model.PublishDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Ms.WeiBoMsg DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.WeiBoMsg msg = new Maticsoft.Model.Ms.WeiBoMsg();
            if (row != null)
            {
                if ((row["WeiBoId"] != null) && (row["WeiBoId"].ToString() != ""))
                {
                    msg.WeiBoId = int.Parse(row["WeiBoId"].ToString());
                }
                if (row["WeiboMsg"] != null)
                {
                    msg.WeiboMsg = row["WeiboMsg"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    msg.ImageUrl = row["ImageUrl"].ToString();
                }
                if ((row["CreateDate"] != null) && (row["CreateDate"].ToString() != ""))
                {
                    msg.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if ((row["PublishDate"] != null) && (row["PublishDate"].ToString() != ""))
                {
                    msg.PublishDate = new DateTime?(DateTime.Parse(row["PublishDate"].ToString()));
                }
            }
            return msg;
        }

        public bool Delete(int WeiBoId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_WeiBoMsg ");
            builder.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiBoId", SqlDbType.Int, 4) };
            cmdParms[0].Value = WeiBoId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string WeiBoIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_WeiBoMsg ");
            builder.Append(" where WeiBoId in (" + WeiBoIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int WeiBoId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_WeiBoMsg");
            builder.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiBoId", SqlDbType.Int, 4) };
            cmdParms[0].Value = WeiBoId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WeiBoId,WeiboMsg,ImageUrl,CreateDate,PublishDate ");
            builder.Append(" FROM Ms_WeiBoMsg ");
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
            builder.Append(" WeiBoId,WeiboMsg,ImageUrl,CreateDate,PublishDate ");
            builder.Append(" FROM Ms_WeiBoMsg ");
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
                builder.Append("order by T.WeiBoId desc");
            }
            builder.Append(")AS Row, T.*  from Ms_WeiBoMsg T ");
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
            return DbHelperSQL.GetMaxID("WeiBoId", "Ms_WeiBoMsg");
        }

        public Maticsoft.Model.Ms.WeiBoMsg GetModel(int WeiBoId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 WeiBoId,WeiboMsg,ImageUrl,CreateDate,PublishDate from Ms_WeiBoMsg ");
            builder.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiBoId", SqlDbType.Int, 4) };
            cmdParms[0].Value = WeiBoId;
            new Maticsoft.Model.Ms.WeiBoMsg();
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
            builder.Append("select count(1) FROM Ms_WeiBoMsg ");
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

        public bool Update(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_WeiBoMsg set ");
            builder.Append("WeiboMsg=@WeiboMsg,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("CreateDate=@CreateDate,");
            builder.Append("PublishDate=@PublishDate");
            builder.Append(" where WeiBoId=@WeiBoId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiboMsg", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@PublishDate", SqlDbType.DateTime), new SqlParameter("@WeiBoId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.WeiboMsg;
            cmdParms[1].Value = model.ImageUrl;
            cmdParms[2].Value = model.CreateDate;
            cmdParms[3].Value = model.PublishDate;
            cmdParms[4].Value = model.WeiBoId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

