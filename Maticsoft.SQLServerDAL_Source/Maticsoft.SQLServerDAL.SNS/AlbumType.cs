namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AlbumType : IAlbumType
    {
        public int Add(Maticsoft.Model.SNS.AlbumType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_AlbumType(");
            builder.Append("TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark)");
            builder.Append(" values (");
            builder.Append("@TypeName,@IsMenu,@MenuIsShow,@MenuSequence,@AlbumsCount,@Status,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@IsMenu", SqlDbType.Bit, 1), new SqlParameter("@MenuIsShow", SqlDbType.Bit, 1), new SqlParameter("@MenuSequence", SqlDbType.Int, 4), new SqlParameter("@AlbumsCount", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.IsMenu;
            cmdParms[2].Value = model.MenuIsShow;
            cmdParms[3].Value = model.MenuSequence;
            cmdParms[4].Value = model.AlbumsCount;
            cmdParms[5].Value = model.Status;
            cmdParms[6].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.AlbumType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.AlbumType type = new Maticsoft.Model.SNS.AlbumType();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    type.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["TypeName"] != null) && (row["TypeName"].ToString() != ""))
                {
                    type.TypeName = row["TypeName"].ToString();
                }
                if ((row["IsMenu"] != null) && (row["IsMenu"].ToString() != ""))
                {
                    if ((row["IsMenu"].ToString() == "1") || (row["IsMenu"].ToString().ToLower() == "true"))
                    {
                        type.IsMenu = true;
                    }
                    else
                    {
                        type.IsMenu = false;
                    }
                }
                if ((row["MenuIsShow"] != null) && (row["MenuIsShow"].ToString() != ""))
                {
                    if ((row["MenuIsShow"].ToString() == "1") || (row["MenuIsShow"].ToString().ToLower() == "true"))
                    {
                        type.MenuIsShow = true;
                    }
                    else
                    {
                        type.MenuIsShow = false;
                    }
                }
                if ((row["MenuSequence"] != null) && (row["MenuSequence"].ToString() != ""))
                {
                    type.MenuSequence = int.Parse(row["MenuSequence"].ToString());
                }
                if ((row["AlbumsCount"] != null) && (row["AlbumsCount"].ToString() != ""))
                {
                    type.AlbumsCount = new int?(int.Parse(row["AlbumsCount"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    type.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["Remark"] != null) && (row["Remark"].ToString() != ""))
                {
                    type.Remark = row["Remark"].ToString();
                }
            }
            return type;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_AlbumType ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_AlbumType ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TypeName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_AlbumType");
            builder.Append(" where TypeName=@TypeName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TypeName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark ");
            builder.Append(" FROM SNS_AlbumType ");
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
            builder.Append(" ID,TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark ");
            builder.Append(" FROM SNS_AlbumType ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            else
            {
                builder.Append(" order by ID desc");
            }
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_AlbumType T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.AlbumType GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,TypeName,IsMenu,MenuIsShow,MenuSequence,AlbumsCount,Status,Remark from SNS_AlbumType ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.AlbumType();
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
            builder.Append("select count(1) FROM SNS_AlbumType ");
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

        public DataSet GetTypeList(int albumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SNS_AlbumType where ID in (select TypeID from  SNS_UserAlbumsType where AlbumsID=" + albumId + ") ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Maticsoft.Model.SNS.AlbumType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_AlbumType set ");
            builder.Append("TypeName=@TypeName,");
            builder.Append("IsMenu=@IsMenu,");
            builder.Append("MenuIsShow=@MenuIsShow,");
            builder.Append("MenuSequence=@MenuSequence,");
            builder.Append("AlbumsCount=@AlbumsCount,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@IsMenu", SqlDbType.Bit, 1), new SqlParameter("@MenuIsShow", SqlDbType.Bit, 1), new SqlParameter("@MenuSequence", SqlDbType.Int, 4), new SqlParameter("@AlbumsCount", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.IsMenu;
            cmdParms[2].Value = model.MenuIsShow;
            cmdParms[3].Value = model.MenuSequence;
            cmdParms[4].Value = model.AlbumsCount;
            cmdParms[5].Value = model.Status;
            cmdParms[6].Value = model.Remark;
            cmdParms[7].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateIsMenu(int IsMenu, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_AlbumType set ");
            builder.AppendFormat(" IsMenu={0} ", IsMenu);
            builder.AppendFormat(" where ID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateMenuIsShow(int MenuIsShow, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_AlbumType set ");
            builder.AppendFormat(" MenuIsShow={0} ", MenuIsShow);
            builder.AppendFormat(" where ID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_AlbumType set ");
            builder.AppendFormat(" Status={0} ", Status);
            builder.AppendFormat(" where ID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

