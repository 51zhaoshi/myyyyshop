namespace Maticsoft.SQLServerDAL.Settings
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MainMenus : IMainMenus
    {
        public int Add(Maticsoft.Model.Settings.MainMenus model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_WebMenuConfig(");
            builder.Append("MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme)");
            builder.Append(" values (");
            builder.Append("@MenuName,@NavURL,@MenuTitle,@MenuType,@Target,@IsUsed,@Sequence,@Visible,@NavArea,@URLType,@NavTheme)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MenuName", SqlDbType.NVarChar, 50), new SqlParameter("@NavURL", SqlDbType.NVarChar, -1), new SqlParameter("@MenuTitle", SqlDbType.NVarChar, 50), new SqlParameter("@MenuType", SqlDbType.Int, 4), new SqlParameter("@Target", SqlDbType.Int, 4), new SqlParameter("@IsUsed", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Visible", SqlDbType.Int, 4), new SqlParameter("@NavArea", SqlDbType.Int, 4), new SqlParameter("@URLType", SqlDbType.Int, 4), new SqlParameter("@NavTheme", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.MenuName;
            cmdParms[1].Value = model.NavURL;
            cmdParms[2].Value = model.MenuTitle;
            cmdParms[3].Value = model.MenuType;
            cmdParms[4].Value = model.Target;
            cmdParms[5].Value = model.IsUsed;
            cmdParms[6].Value = model.Sequence;
            cmdParms[7].Value = model.Visible;
            cmdParms[8].Value = model.NavArea;
            cmdParms[9].Value = model.URLType;
            cmdParms[10].Value = model.NavTheme;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Settings.MainMenus DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Settings.MainMenus menus = new Maticsoft.Model.Settings.MainMenus();
            if (row != null)
            {
                if ((row["MenuID"] != null) && (row["MenuID"].ToString() != ""))
                {
                    menus.MenuID = int.Parse(row["MenuID"].ToString());
                }
                if (row["MenuName"] != null)
                {
                    menus.MenuName = row["MenuName"].ToString();
                }
                if (row["NavURL"] != null)
                {
                    menus.NavURL = row["NavURL"].ToString();
                }
                if (row["MenuTitle"] != null)
                {
                    menus.MenuTitle = row["MenuTitle"].ToString();
                }
                if ((row["MenuType"] != null) && (row["MenuType"].ToString() != ""))
                {
                    menus.MenuType = new int?(int.Parse(row["MenuType"].ToString()));
                }
                if ((row["Target"] != null) && (row["Target"].ToString() != ""))
                {
                    menus.Target = new int?(int.Parse(row["Target"].ToString()));
                }
                if ((row["IsUsed"] != null) && (row["IsUsed"].ToString() != ""))
                {
                    if ((row["IsUsed"].ToString() == "1") || (row["IsUsed"].ToString().ToLower() == "true"))
                    {
                        menus.IsUsed = true;
                    }
                    else
                    {
                        menus.IsUsed = false;
                    }
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    menus.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["Visible"] != null) && (row["Visible"].ToString() != ""))
                {
                    menus.Visible = int.Parse(row["Visible"].ToString());
                }
                if ((row["NavArea"] != null) && (row["NavArea"].ToString() != ""))
                {
                    menus.NavArea = int.Parse(row["NavArea"].ToString());
                }
                if ((row["URLType"] != null) && (row["URLType"].ToString() != ""))
                {
                    menus.URLType = int.Parse(row["URLType"].ToString());
                }
                if (row["NavTheme"] != null)
                {
                    menus.NavTheme = row["NavTheme"].ToString();
                }
            }
            return menus;
        }

        public bool Delete(int MenuID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_WebMenuConfig ");
            builder.Append(" where MenuID=@MenuID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MenuID", SqlDbType.Int, 4) };
            cmdParms[0].Value = MenuID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string MenuIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_WebMenuConfig ");
            builder.Append(" where MenuID in (" + MenuIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int MenuID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_WebMenuConfig");
            builder.Append(" where MenuID=@MenuID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MenuID", SqlDbType.Int, 4) };
            cmdParms[0].Value = MenuID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MenuID,MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme ");
            builder.Append(" FROM SA_WebMenuConfig ");
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
            builder.Append(" MenuID,MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme ");
            builder.Append(" FROM SA_WebMenuConfig ");
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
                builder.Append("order by T.MenuID desc");
            }
            builder.Append(")AS Row, T.*  from SA_WebMenuConfig T ");
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
            return DbHelperSQL.GetMaxID("MenuID", "SA_WebMenuConfig");
        }

        public Maticsoft.Model.Settings.MainMenus GetModel(int MenuID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 MenuID,MenuName,NavURL,MenuTitle,MenuType,Target,IsUsed,Sequence,Visible,NavArea,URLType,NavTheme from SA_WebMenuConfig ");
            builder.Append(" where MenuID=@MenuID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MenuID", SqlDbType.Int, 4) };
            cmdParms[0].Value = MenuID;
            new Maticsoft.Model.Settings.MainMenus();
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
            builder.Append("select count(1) FROM SA_WebMenuConfig ");
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

        public bool Update(Maticsoft.Model.Settings.MainMenus model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_WebMenuConfig set ");
            builder.Append("MenuName=@MenuName,");
            builder.Append("NavURL=@NavURL,");
            builder.Append("MenuTitle=@MenuTitle,");
            builder.Append("MenuType=@MenuType,");
            builder.Append("Target=@Target,");
            builder.Append("IsUsed=@IsUsed,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Visible=@Visible,");
            builder.Append("NavArea=@NavArea,");
            builder.Append("URLType=@URLType,");
            builder.Append("NavTheme=@NavTheme");
            builder.Append(" where MenuID=@MenuID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MenuName", SqlDbType.NVarChar, 50), new SqlParameter("@NavURL", SqlDbType.NVarChar, -1), new SqlParameter("@MenuTitle", SqlDbType.NVarChar, 50), new SqlParameter("@MenuType", SqlDbType.Int, 4), new SqlParameter("@Target", SqlDbType.Int, 4), new SqlParameter("@IsUsed", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Visible", SqlDbType.Int, 4), new SqlParameter("@NavArea", SqlDbType.Int, 4), new SqlParameter("@URLType", SqlDbType.Int, 4), new SqlParameter("@NavTheme", SqlDbType.NVarChar, 100), new SqlParameter("@MenuID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.MenuName;
            cmdParms[1].Value = model.NavURL;
            cmdParms[2].Value = model.MenuTitle;
            cmdParms[3].Value = model.MenuType;
            cmdParms[4].Value = model.Target;
            cmdParms[5].Value = model.IsUsed;
            cmdParms[6].Value = model.Sequence;
            cmdParms[7].Value = model.Visible;
            cmdParms[8].Value = model.NavArea;
            cmdParms[9].Value = model.URLType;
            cmdParms[10].Value = model.NavTheme;
            cmdParms[11].Value = model.MenuID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

