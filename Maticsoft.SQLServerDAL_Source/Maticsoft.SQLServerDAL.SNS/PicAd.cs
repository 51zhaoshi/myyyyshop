namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PicAd
    {
        public int Add(Maticsoft.Model.SNS.PicAd model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into X_PicAd(");
            builder.Append("Name,Src,Href,Title,Alt,IsShow,Orders)");
            builder.Append(" values (");
            builder.Append("@Name,@Src,@Href,@Title,@Alt,@IsShow,@Orders)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Src", SqlDbType.NVarChar, 200), new SqlParameter("@Href", SqlDbType.NVarChar, 200), new SqlParameter("@Title", SqlDbType.NVarChar, 50), new SqlParameter("@Alt", SqlDbType.NVarChar, 50), new SqlParameter("@IsShow", SqlDbType.Bit, 1), new SqlParameter("@Orders", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Src;
            cmdParms[2].Value = model.Href;
            cmdParms[3].Value = model.Title;
            cmdParms[4].Value = model.Alt;
            cmdParms[5].Value = model.IsShow;
            cmdParms[6].Value = model.Orders;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from X_PicAd ");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from X_PicAd ");
            builder.Append(" where Id in (" + Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from X_PicAd");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList()
        {
            return DbHelperSQL.Query(" SELECT top 4 ROW_NUMBER() OVER( ORDER BY Id ASC) AS ROW,* FROM X_PicAd where IsShow=1 ");
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,Name,Src,Href,Title,Alt,IsShow,Orders ");
            builder.Append(" FROM X_PicAd ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" Order by Orders desc ");
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
            builder.Append(" Id,Name,Src,Href,Title,Alt,IsShow,Orders ");
            builder.Append(" FROM X_PicAd ");
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
                builder.Append("order by T.Id desc");
            }
            builder.Append(")AS Row, T.*  from X_PicAd T ");
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
            return DbHelperSQL.GetMaxID("Id", "X_PicAd");
        }

        public Maticsoft.Model.SNS.PicAd GetModel(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Id,Name,Src,Href,Title,Alt,IsShow,Orders from X_PicAd ");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            Maticsoft.Model.SNS.PicAd ad = new Maticsoft.Model.SNS.PicAd();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["Id"] != null) && (set.Tables[0].Rows[0]["Id"].ToString() != ""))
            {
                ad.Id = int.Parse(set.Tables[0].Rows[0]["Id"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Name"] != null) && (set.Tables[0].Rows[0]["Name"].ToString() != ""))
            {
                ad.Name = set.Tables[0].Rows[0]["Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Src"] != null) && (set.Tables[0].Rows[0]["Src"].ToString() != ""))
            {
                ad.Src = set.Tables[0].Rows[0]["Src"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Href"] != null) && (set.Tables[0].Rows[0]["Href"].ToString() != ""))
            {
                ad.Href = set.Tables[0].Rows[0]["Href"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Title"] != null) && (set.Tables[0].Rows[0]["Title"].ToString() != ""))
            {
                ad.Title = set.Tables[0].Rows[0]["Title"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Alt"] != null) && (set.Tables[0].Rows[0]["Alt"].ToString() != ""))
            {
                ad.Alt = set.Tables[0].Rows[0]["Alt"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IsShow"] != null) && (set.Tables[0].Rows[0]["IsShow"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsShow"].ToString() == "1") || (set.Tables[0].Rows[0]["IsShow"].ToString().ToLower() == "true"))
                {
                    ad.IsShow = true;
                }
                else
                {
                    ad.IsShow = false;
                }
            }
            if ((set.Tables[0].Rows[0]["Orders"] != null) && (set.Tables[0].Rows[0]["Orders"].ToString() != ""))
            {
                ad.Orders = int.Parse(set.Tables[0].Rows[0]["Orders"].ToString());
            }
            return ad;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM X_PicAd ");
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

        public bool Update(Maticsoft.Model.SNS.PicAd model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update X_PicAd set ");
            builder.Append("Name=@Name,");
            builder.Append("Src=@Src,");
            builder.Append("Href=@Href,");
            builder.Append("Title=@Title,");
            builder.Append("Alt=@Alt,");
            builder.Append("IsShow=@IsShow,");
            builder.Append("Orders=@Orders");
            builder.Append(" where Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Src", SqlDbType.NVarChar, 200), new SqlParameter("@Href", SqlDbType.NVarChar, 200), new SqlParameter("@Title", SqlDbType.NVarChar, 50), new SqlParameter("@Alt", SqlDbType.NVarChar, 50), new SqlParameter("@IsShow", SqlDbType.Bit, 1), new SqlParameter("@Orders", SqlDbType.Int, 4), new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Src;
            cmdParms[2].Value = model.Href;
            cmdParms[3].Value = model.Title;
            cmdParms[4].Value = model.Alt;
            cmdParms[5].Value = model.IsShow;
            cmdParms[6].Value = model.Orders;
            cmdParms[7].Value = model.Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

