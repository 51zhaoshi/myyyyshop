namespace Maticsoft.SQLServerDAL.Settings
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class FriendlyLink : IFriendlyLink
    {
        public int Add(Maticsoft.Model.Settings.FriendlyLink model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_FLinks(");
            builder.Append("Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight)");
            builder.Append(" values (");
            builder.Append("@Name,@ImgUrl,@LinkUrl,@LinkDesc,@State,@OrderID,@ContactPerson,@Email,@TelPhone,@TypeID,@IsDisplay,@ImgWidth,@ImgHeight)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@ImgUrl", SqlDbType.NVarChar, 150), new SqlParameter("@LinkUrl", SqlDbType.NVarChar, 150), new SqlParameter("@LinkDesc", SqlDbType.NVarChar, 300), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@ContactPerson", SqlDbType.NVarChar, 100), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 100), new SqlParameter("@TypeID", SqlDbType.SmallInt, 2), new SqlParameter("@IsDisplay", SqlDbType.Bit, 1), new SqlParameter("@ImgWidth", SqlDbType.Int, 4), new SqlParameter("@ImgHeight", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.ImgUrl;
            cmdParms[2].Value = model.LinkUrl;
            cmdParms[3].Value = model.LinkDesc;
            cmdParms[4].Value = model.State;
            cmdParms[5].Value = model.OrderID;
            cmdParms[6].Value = model.ContactPerson;
            cmdParms[7].Value = model.Email;
            cmdParms[8].Value = model.TelPhone;
            cmdParms[9].Value = model.TypeID;
            cmdParms[10].Value = model.IsDisplay;
            cmdParms[11].Value = model.ImgWidth;
            cmdParms[12].Value = model.ImgHeight;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Settings.FriendlyLink DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Settings.FriendlyLink link = new Maticsoft.Model.Settings.FriendlyLink();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    link.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    link.Name = row["Name"].ToString();
                }
                if (row["ImgUrl"] != null)
                {
                    link.ImgUrl = row["ImgUrl"].ToString();
                }
                if (row["LinkUrl"] != null)
                {
                    link.LinkUrl = row["LinkUrl"].ToString();
                }
                if (row["LinkDesc"] != null)
                {
                    link.LinkDesc = row["LinkDesc"].ToString();
                }
                if ((row["State"] != null) && (row["State"].ToString() != ""))
                {
                    link.State = int.Parse(row["State"].ToString());
                }
                if ((row["OrderID"] != null) && (row["OrderID"].ToString() != ""))
                {
                    link.OrderID = int.Parse(row["OrderID"].ToString());
                }
                if (row["ContactPerson"] != null)
                {
                    link.ContactPerson = row["ContactPerson"].ToString();
                }
                if (row["Email"] != null)
                {
                    link.Email = row["Email"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    link.TelPhone = row["TelPhone"].ToString();
                }
                if ((row["TypeID"] != null) && (row["TypeID"].ToString() != ""))
                {
                    link.TypeID = int.Parse(row["TypeID"].ToString());
                }
                if ((row["IsDisplay"] != null) && (row["IsDisplay"].ToString() != ""))
                {
                    if ((row["IsDisplay"].ToString() == "1") || (row["IsDisplay"].ToString().ToLower() == "true"))
                    {
                        link.IsDisplay = true;
                    }
                    else
                    {
                        link.IsDisplay = false;
                    }
                }
                if ((row["ImgWidth"] != null) && (row["ImgWidth"].ToString() != ""))
                {
                    link.ImgWidth = new int?(int.Parse(row["ImgWidth"].ToString()));
                }
                if ((row["ImgHeight"] != null) && (row["ImgHeight"].ToString() != ""))
                {
                    link.ImgHeight = new int?(int.Parse(row["ImgHeight"].ToString()));
                }
            }
            return link;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_FLinks ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_FLinks ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_FLinks");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight ");
            builder.Append(" FROM CMS_FLinks ");
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
            builder.Append(" ID,Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight ");
            builder.Append(" FROM CMS_FLinks ");
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from CMS_FLinks T ");
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
            return DbHelperSQL.GetMaxID("ID", "CMS_FLinks");
        }

        public Maticsoft.Model.Settings.FriendlyLink GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,Name,ImgUrl,LinkUrl,LinkDesc,State,OrderID,ContactPerson,Email,TelPhone,TypeID,IsDisplay,ImgWidth,ImgHeight from CMS_FLinks ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.Settings.FriendlyLink();
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
            builder.Append("select count(1) FROM CMS_FLinks ");
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

        public bool Update(Maticsoft.Model.Settings.FriendlyLink model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_FLinks set ");
            builder.Append("Name=@Name,");
            builder.Append("ImgUrl=@ImgUrl,");
            builder.Append("LinkUrl=@LinkUrl,");
            builder.Append("LinkDesc=@LinkDesc,");
            builder.Append("State=@State,");
            builder.Append("OrderID=@OrderID,");
            builder.Append("ContactPerson=@ContactPerson,");
            builder.Append("Email=@Email,");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("TypeID=@TypeID,");
            builder.Append("IsDisplay=@IsDisplay,");
            builder.Append("ImgWidth=@ImgWidth,");
            builder.Append("ImgHeight=@ImgHeight");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@ImgUrl", SqlDbType.NVarChar, 150), new SqlParameter("@LinkUrl", SqlDbType.NVarChar, 150), new SqlParameter("@LinkDesc", SqlDbType.NVarChar, 300), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@ContactPerson", SqlDbType.NVarChar, 100), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 100), new SqlParameter("@TypeID", SqlDbType.SmallInt, 2), new SqlParameter("@IsDisplay", SqlDbType.Bit, 1), new SqlParameter("@ImgWidth", SqlDbType.Int, 4), new SqlParameter("@ImgHeight", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.ImgUrl;
            cmdParms[2].Value = model.LinkUrl;
            cmdParms[3].Value = model.LinkDesc;
            cmdParms[4].Value = model.State;
            cmdParms[5].Value = model.OrderID;
            cmdParms[6].Value = model.ContactPerson;
            cmdParms[7].Value = model.Email;
            cmdParms[8].Value = model.TelPhone;
            cmdParms[9].Value = model.TypeID;
            cmdParms[10].Value = model.IsDisplay;
            cmdParms[11].Value = model.ImgWidth;
            cmdParms[12].Value = model.ImgHeight;
            cmdParms[13].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_FLinks set " + strWhere);
            builder.Append(" where ID in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

