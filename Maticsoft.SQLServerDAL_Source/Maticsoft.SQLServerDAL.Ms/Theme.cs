namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.Accounts;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Theme : ITheme
    {
        public int Add(Maticsoft.Model.Ms.Theme model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_Theme(");
            builder.Append("Name,Description,PreviewPhotoSrc,ZipPackageSrc,ThemeSize,Author,IsCurrent,Language,CreatedDate,Remark)");
            builder.Append(" values (");
            builder.Append("@Name,@Description,@PreviewPhotoSrc,@ZipPackageSrc,@ThemeSize,@Author,@IsCurrent,@Language,@CreatedDate,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@PreviewPhotoSrc", SqlDbType.NVarChar, 100), new SqlParameter("@ZipPackageSrc", SqlDbType.NVarChar, 50), new SqlParameter("@ThemeSize", SqlDbType.Int, 4), new SqlParameter("@Author", SqlDbType.NVarChar, 100), new SqlParameter("@IsCurrent", SqlDbType.Bit, 1), new SqlParameter("@Language", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.PreviewPhotoSrc;
            cmdParms[3].Value = model.ZipPackageSrc;
            cmdParms[4].Value = model.ThemeSize;
            cmdParms[5].Value = model.Author;
            cmdParms[6].Value = model.IsCurrent;
            cmdParms[7].Value = model.Language;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.Remark;
            object single = Maticsoft.DBUtility.DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Ms.Theme DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.Theme theme = new Maticsoft.Model.Ms.Theme();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    theme.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    theme.Name = row["Name"].ToString();
                }
                if (row["Description"] != null)
                {
                    theme.Description = row["Description"].ToString();
                }
                if (row["PreviewPhotoSrc"] != null)
                {
                    theme.PreviewPhotoSrc = row["PreviewPhotoSrc"].ToString();
                }
                if (row["ZipPackageSrc"] != null)
                {
                    theme.ZipPackageSrc = row["ZipPackageSrc"].ToString();
                }
                if ((row["ThemeSize"] != null) && (row["ThemeSize"].ToString() != ""))
                {
                    theme.ThemeSize = new int?(int.Parse(row["ThemeSize"].ToString()));
                }
                if (row["Author"] != null)
                {
                    theme.Author = row["Author"].ToString();
                }
                if ((row["IsCurrent"] != null) && (row["IsCurrent"].ToString() != ""))
                {
                    if ((row["IsCurrent"].ToString() == "1") || (row["IsCurrent"].ToString().ToLower() == "true"))
                    {
                        theme.IsCurrent = true;
                    }
                    else
                    {
                        theme.IsCurrent = false;
                    }
                }
                if (row["Language"] != null)
                {
                    theme.Language = row["Language"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    theme.CreatedDate = new DateTime?(DateTime.Parse(row["CreatedDate"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    theme.Remark = row["Remark"].ToString();
                }
            }
            return theme;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Theme ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Theme ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_Theme");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return Maticsoft.DBUtility.DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Name,Description,PreviewPhotoSrc,ZipPackageSrc,ThemeSize,Author,IsCurrent,Language,CreatedDate,Remark ");
            builder.Append(" FROM Ms_Theme ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return Maticsoft.DBUtility.DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ID,Name,Description,PreviewPhotoSrc,ZipPackageSrc,ThemeSize,Author,IsCurrent,Language,CreatedDate,Remark ");
            builder.Append(" FROM Ms_Theme ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return Maticsoft.DBUtility.DbHelperSQL.Query(builder.ToString());
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
            builder.Append(")AS Row, T.*  from Ms_Theme T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return Maticsoft.DBUtility.DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return Maticsoft.DBUtility.DbHelperSQL.GetMaxID("ID", "Ms_Theme");
        }

        public Maticsoft.Model.Ms.Theme GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,Name,Description,PreviewPhotoSrc,ZipPackageSrc,ThemeSize,Author,IsCurrent,Language,CreatedDate,Remark from Ms_Theme ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.Ms.Theme();
            DataSet set = Maticsoft.DBUtility.DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Ms_Theme ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = Maticsoft.DBUtility.DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Ms.Theme model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_Theme set ");
            builder.Append("Name=@Name,");
            builder.Append("Description=@Description,");
            builder.Append("PreviewPhotoSrc=@PreviewPhotoSrc,");
            builder.Append("ZipPackageSrc=@ZipPackageSrc,");
            builder.Append("ThemeSize=@ThemeSize,");
            builder.Append("Author=@Author,");
            builder.Append("IsCurrent=@IsCurrent,");
            builder.Append("Language=@Language,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@PreviewPhotoSrc", SqlDbType.NVarChar, 100), new SqlParameter("@ZipPackageSrc", SqlDbType.NVarChar, 50), new SqlParameter("@ThemeSize", SqlDbType.Int, 4), new SqlParameter("@Author", SqlDbType.NVarChar, 100), new SqlParameter("@IsCurrent", SqlDbType.Bit, 1), new SqlParameter("@Language", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.PreviewPhotoSrc;
            cmdParms[3].Value = model.ZipPackageSrc;
            cmdParms[4].Value = model.ThemeSize;
            cmdParms[5].Value = model.Author;
            cmdParms[6].Value = model.IsCurrent;
            cmdParms[7].Value = model.Language;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.Remark;
            cmdParms[10].Value = model.ID;
            return (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateEx(int Id)
        {
            List<string> sQLStringList = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_Theme set IsCurrent=0 ");
            sQLStringList.Add(builder.ToString());
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Ms_Theme set IsCurrent=1 where  ID=" + Id + " ");
            sQLStringList.Add(builder2.ToString());
            return (Maticsoft.Accounts.DbHelperSQL.ExecuteSqlTran(sQLStringList) > 0);
        }
    }
}

