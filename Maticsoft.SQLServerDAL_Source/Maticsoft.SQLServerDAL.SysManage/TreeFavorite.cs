namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TreeFavorite : ITreeFavorite
    {
        public int Add(int UserID, int NodeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_TreeFavorite(");
            builder.Append("UserID,NodeID,CreatDate)");
            builder.Append(" values (");
            builder.Append("@UserID,@NodeID,@CreatDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4), new SqlParameter("@CreatDate", SqlDbType.DateTime) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = NodeID;
            cmdParms[2].Value = DateTime.Now;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public void Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_TreeFavorite ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void Delete(int UserID, int NodeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_TreeFavorite ");
            builder.Append(" where UserID=@UserID and  NodeID=@NodeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = NodeID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void DeleteByUser(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_TreeFavorite ");
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(int UserID, int NodeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_TreeFavorite");
            builder.Append(" where UserID=@UserID and  NodeID=@NodeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = NodeID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,UserID,NodeID,CreatDate ");
            builder.Append(" FROM SA_TreeFavorite ");
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
            builder.Append(" ID,UserID,NodeID,CreatDate ");
            builder.Append(" FROM SA_TreeFavorite ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetMenuListByUser(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SA_TreeFavorite.*,SA_Tree.TreeText,SA_Tree.Url from SA_TreeFavorite  ");
            builder.Append(" left join SA_Tree on SA_TreeFavorite.NodeID=SA_Tree.NodeID ");
            if (UserID > 0)
            {
                builder.Append(" where UserID=" + UserID);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetNodeIDsByUser(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select NodeID ");
            builder.Append(" FROM SA_TreeFavorite ");
            if (UserID > 0)
            {
                builder.Append(" where UserID=" + UserID);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public void UpDate(int OrderID, int UserID, int NodeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SA_TreeFavorite SET OrderID = @OrderID where UserID =@UserID and NodeID=@NodeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = OrderID;
            cmdParms[1].Value = UserID;
            cmdParms[2].Value = NodeID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

