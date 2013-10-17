namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class WeiBoTaskMsg : IWeiBoTaskMsg
    {
        public int Add(Maticsoft.Model.Ms.WeiBoTaskMsg model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_WeiBoTaskMsg(");
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

        public int AddEx(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_WeiBoTaskMsg(");
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

        public Maticsoft.Model.Ms.WeiBoTaskMsg DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.WeiBoTaskMsg msg = new Maticsoft.Model.Ms.WeiBoTaskMsg();
            if (row != null)
            {
                if ((row["WeiBoTaskId"] != null) && (row["WeiBoTaskId"].ToString() != ""))
                {
                    msg.WeiBoTaskId = int.Parse(row["WeiBoTaskId"].ToString());
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

        public bool Delete(int WeiBoTaskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_WeiBoTaskMsg ");
            builder.Append(" where WeiBoTaskId=@WeiBoTaskId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiBoTaskId", SqlDbType.Int, 4) };
            cmdParms[0].Value = WeiBoTaskId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string WeiBoTaskIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_WeiBoTaskMsg ");
            builder.Append(" where WeiBoTaskId in (" + WeiBoTaskIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int WeiBoTaskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_WeiBoTaskMsg");
            builder.Append(" where WeiBoTaskId=@WeiBoTaskId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiBoTaskId", SqlDbType.Int, 4) };
            cmdParms[0].Value = WeiBoTaskId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WeiBoTaskId,WeiboMsg,ImageUrl,CreateDate,PublishDate ");
            builder.Append(" FROM Ms_WeiBoTaskMsg ");
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
            builder.Append(" WeiBoTaskId,WeiboMsg,ImageUrl,CreateDate,PublishDate ");
            builder.Append(" FROM Ms_WeiBoTaskMsg ");
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
                builder.Append("order by T.WeiBoTaskId desc");
            }
            builder.Append(")AS Row, T.*  from Ms_WeiBoTaskMsg T ");
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
            return DbHelperSQL.GetMaxID("WeiBoTaskId", "Ms_WeiBoTaskMsg");
        }

        public Maticsoft.Model.Ms.WeiBoTaskMsg GetModel(int WeiBoTaskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 WeiBoTaskId,WeiboMsg,ImageUrl,CreateDate,PublishDate from Ms_WeiBoTaskMsg ");
            builder.Append(" where WeiBoTaskId=@WeiBoTaskId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiBoTaskId", SqlDbType.Int, 4) };
            cmdParms[0].Value = WeiBoTaskId;
            new Maticsoft.Model.Ms.WeiBoTaskMsg();
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
            builder.Append("select count(1) FROM Ms_WeiBoTaskMsg ");
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

        public bool RunTask(Maticsoft.Model.Ms.WeiBoTaskMsg model)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_WeiBoMsg(");
            builder.Append("WeiboMsg,ImageUrl,CreateDate,PublishDate)");
            builder.Append(" values (");
            builder.Append("@WeiboMsg,@ImageUrl,@CreateDate,@PublishDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@WeiboMsg", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@PublishDate", SqlDbType.DateTime) };
            para[0].Value = model.WeiboMsg;
            para[1].Value = model.ImageUrl;
            para[2].Value = model.CreateDate;
            para[3].Value = model.PublishDate;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Ms_WeiBoTaskMsg ");
            builder2.Append(" where WeiBoTaskId=@WeiBoTaskId  ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@WeiBoTaskId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.WeiBoTaskId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Update(Maticsoft.Model.Ms.WeiBoTaskMsg model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_WeiBoTaskMsg set ");
            builder.Append("WeiboMsg=@WeiboMsg,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("CreateDate=@CreateDate,");
            builder.Append("PublishDate=@PublishDate");
            builder.Append(" where WeiBoTaskId=@WeiBoTaskId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WeiboMsg", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@PublishDate", SqlDbType.DateTime), new SqlParameter("@WeiBoTaskId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.WeiboMsg;
            cmdParms[1].Value = model.ImageUrl;
            cmdParms[2].Value = model.CreateDate;
            cmdParms[3].Value = model.PublishDate;
            cmdParms[4].Value = model.WeiBoTaskId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

