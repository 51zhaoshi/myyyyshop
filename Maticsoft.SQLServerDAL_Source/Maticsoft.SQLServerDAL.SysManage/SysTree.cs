namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SysTree : ISysTree
    {
        public void AddLog(string time, string loginfo, string Particular)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_Log(");
            builder.Append("datetime,loginfo,Particular)");
            builder.Append(" values (");
            builder.Append("'" + time + "',");
            builder.Append("'" + loginfo + "',");
            builder.Append("'" + Particular + "'");
            builder.Append(")");
            DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public int AddTreeNode(SysNode model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_Tree(");
            builder.Append("TreeText,ParentID,Location,OrderID,comment,Url,PermissionID,ImageUrl,TreeType,Enabled)");
            builder.Append(" values (");
            builder.Append("@TreeText,@ParentID,@Location,@OrderID,@comment,@Url,@PermissionID,@ImageUrl,@TreeType,@Enabled)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TreeText", SqlDbType.NVarChar, 100), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Location", SqlDbType.NVarChar, 50), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@comment", SqlDbType.NVarChar, 50), new SqlParameter("@Url", SqlDbType.NVarChar, 100), new SqlParameter("@PermissionID", SqlDbType.Int, 4), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 100), new SqlParameter("@TreeType", SqlDbType.SmallInt), new SqlParameter("@Enabled", SqlDbType.Bit) };
            cmdParms[0].Value = model.TreeText;
            cmdParms[1].Value = model.ParentID;
            cmdParms[2].Value = model.Location;
            cmdParms[3].Value = model.OrderID;
            cmdParms[4].Value = model.Comment;
            cmdParms[5].Value = model.Url;
            cmdParms[6].Value = model.PermissionID;
            cmdParms[7].Value = model.ImageUrl;
            cmdParms[8].Value = model.TreeType;
            cmdParms[9].Value = model.Enabled;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public void DeleteLog(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_Log ");
            builder.Append(" where ID= " + ID);
            DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public void DeleteLog(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_Log ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public void DelOverdueLog(int days)
        {
            string strWhere = " DATEDIFF(day,[datetime],getdate())>" + days;
            this.DeleteLog(strWhere);
        }

        public void DelTreeNode(int NodeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_Tree ");
            builder.Append(" where NodeID=@NodeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = NodeID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void DelTreeNodes(string nodeidlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_Tree ");
            builder.Append(" where NodeID in(" + nodeidlist + ")");
            DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public DataRow GetLog(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from SA_Log ");
            builder.Append(" where ID= " + ID);
            return DbHelperSQL.Query(builder.ToString()).Tables[0].Rows[0];
        }

        public DataSet GetLogs(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from SA_Log ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by ID DESC");
            return DbHelperSQL.Query(builder.ToString());
        }

        public SysNode GetNode(int NodeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from SA_Tree ");
            builder.Append(" where NodeID=@NodeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = NodeID;
            SysNode node = new SysNode();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            node.NodeID = int.Parse(set.Tables[0].Rows[0]["NodeID"].ToString());
            node.TreeText = set.Tables[0].Rows[0]["TreeText"].ToString();
            if (set.Tables[0].Rows[0]["ParentID"].ToString() != "")
            {
                node.ParentID = int.Parse(set.Tables[0].Rows[0]["ParentID"].ToString());
            }
            node.Location = set.Tables[0].Rows[0]["Location"].ToString();
            if (set.Tables[0].Rows[0]["OrderID"].ToString() != "")
            {
                node.OrderID = new int?(int.Parse(set.Tables[0].Rows[0]["OrderID"].ToString()));
            }
            node.Comment = set.Tables[0].Rows[0]["comment"].ToString();
            node.Url = set.Tables[0].Rows[0]["url"].ToString();
            if (set.Tables[0].Rows[0]["PermissionID"].ToString() != "")
            {
                node.PermissionID = int.Parse(set.Tables[0].Rows[0]["PermissionID"].ToString());
            }
            node.ImageUrl = set.Tables[0].Rows[0]["ImageUrl"].ToString();
            node.TreeType = int.Parse(set.Tables[0].Rows[0]["TreeType"].ToString());
            node.Enabled = bool.Parse(set.Tables[0].Rows[0]["Enabled"].ToString());
            return node;
        }

        public int GetPermissionCatalogID(int permissionID)
        {
            object single = DbHelperSQL.GetSingle("select CategoryID from Accounts_Permissions where PermissionID=" + permissionID);
            if (single == null)
            {
                return 0;
            }
            return (int) single;
        }

        public DataSet GetTreeList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from SA_Tree ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by parentid,orderid ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public void MoveNodes(string nodeidlist, int ParentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Tree set ");
            builder.Append("ParentID=" + ParentID);
            builder.Append(" where NodeID in(" + nodeidlist + ")");
            DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public void UpdateEnabled(int nodeid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Tree set ");
            builder.Append("Enabled=(Enabled+1)%2");
            builder.Append(" where NodeID=@NodeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = nodeid;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void UpdateNode(SysNode model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Tree set ");
            builder.Append("TreeText=@TreeText,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Location=@Location,");
            builder.Append("OrderID=@OrderID,");
            builder.Append("comment=@comment,");
            builder.Append("Url=@Url,");
            builder.Append("PermissionID=@PermissionID,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("TreeType=@TreeType,");
            builder.Append("Enabled=@Enabled");
            builder.Append(" where NodeID=@NodeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NodeID", SqlDbType.Int, 4), new SqlParameter("@TreeText", SqlDbType.NVarChar, 100), new SqlParameter("@ParentID", SqlDbType.Int, 4), new SqlParameter("@Location", SqlDbType.NVarChar, 50), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@comment", SqlDbType.NVarChar, 50), new SqlParameter("@Url", SqlDbType.NVarChar, 100), new SqlParameter("@PermissionID", SqlDbType.Int, 4), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 100), new SqlParameter("@TreeType", SqlDbType.SmallInt), new SqlParameter("@Enabled", SqlDbType.Bit) };
            cmdParms[0].Value = model.NodeID;
            cmdParms[1].Value = model.TreeText;
            cmdParms[2].Value = model.ParentID;
            cmdParms[3].Value = model.Location;
            cmdParms[4].Value = model.OrderID;
            cmdParms[5].Value = model.Comment;
            cmdParms[6].Value = model.Url;
            cmdParms[7].Value = model.PermissionID;
            cmdParms[8].Value = model.ImageUrl;
            cmdParms[9].Value = model.TreeType;
            cmdParms[10].Value = model.Enabled;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

