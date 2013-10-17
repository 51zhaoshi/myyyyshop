namespace Maticsoft.SQLServerDAL.Settings
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SEORelation : ISEORelation
    {
        public int Add(Maticsoft.Model.Settings.SEORelation model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Ms_SEORelation(");
            builder.Append("KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive)");
            builder.Append(" VALUES (");
            builder.Append("@KeyName,@LinkURL,@IsCMS,@IsShop,@IsSNS,@IsComment,@CreatedDate,@IsActive)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyName", SqlDbType.NVarChar, 200), new SqlParameter("@LinkURL", SqlDbType.NVarChar, 500), new SqlParameter("@IsCMS", SqlDbType.Bit, 1), new SqlParameter("@IsShop", SqlDbType.Bit, 1), new SqlParameter("@IsSNS", SqlDbType.Bit, 1), new SqlParameter("@IsComment", SqlDbType.Bit, 1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsActive", SqlDbType.Bit, 1) };
            cmdParms[0].Value = model.KeyName;
            cmdParms[1].Value = model.LinkURL;
            cmdParms[2].Value = model.IsCMS;
            cmdParms[3].Value = model.IsShop;
            cmdParms[4].Value = model.IsSNS;
            cmdParms[5].Value = model.IsComment;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.IsActive;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int RelationID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Ms_SEORelation ");
            builder.Append(" WHERE RelationID=@RelationID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationID", SqlDbType.Int, 4) };
            cmdParms[0].Value = RelationID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RelationIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Ms_SEORelation ");
            builder.Append(" WHERE RelationID in (" + RelationIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int RelationID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Ms_SEORelation");
            builder.Append(" WHERE RelationID=@RelationID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationID", SqlDbType.Int, 4) };
            cmdParms[0].Value = RelationID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Ms_SEORelation");
            builder.Append(" WHERE KeyName=@KeyName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyName", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT RelationID,KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive ");
            builder.Append(" FROM Ms_SEORelation ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" RelationID,KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive ");
            builder.Append(" FROM Ms_SEORelation ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.RelationID desc");
            }
            builder.Append(")AS Row, T.*  FROM Ms_SEORelation T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("RelationID", "Ms_SEORelation");
        }

        public Maticsoft.Model.Settings.SEORelation GetModel(int RelationID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 RelationID,KeyName,LinkURL,IsCMS,IsShop,IsSNS,IsComment,CreatedDate,IsActive FROM Ms_SEORelation ");
            builder.Append(" WHERE RelationID=@RelationID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationID", SqlDbType.Int, 4) };
            cmdParms[0].Value = RelationID;
            Maticsoft.Model.Settings.SEORelation relation = new Maticsoft.Model.Settings.SEORelation();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["RelationID"] != null) && (set.Tables[0].Rows[0]["RelationID"].ToString() != ""))
            {
                relation.RelationID = int.Parse(set.Tables[0].Rows[0]["RelationID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["KeyName"] != null) && (set.Tables[0].Rows[0]["KeyName"].ToString() != ""))
            {
                relation.KeyName = set.Tables[0].Rows[0]["KeyName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["LinkURL"] != null) && (set.Tables[0].Rows[0]["LinkURL"].ToString() != ""))
            {
                relation.LinkURL = set.Tables[0].Rows[0]["LinkURL"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IsCMS"] != null) && (set.Tables[0].Rows[0]["IsCMS"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsCMS"].ToString() == "1") || (set.Tables[0].Rows[0]["IsCMS"].ToString().ToLower() == "true"))
                {
                    relation.IsCMS = true;
                }
                else
                {
                    relation.IsCMS = false;
                }
            }
            if ((set.Tables[0].Rows[0]["IsShop"] != null) && (set.Tables[0].Rows[0]["IsShop"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsShop"].ToString() == "1") || (set.Tables[0].Rows[0]["IsShop"].ToString().ToLower() == "true"))
                {
                    relation.IsShop = true;
                }
                else
                {
                    relation.IsShop = false;
                }
            }
            if ((set.Tables[0].Rows[0]["IsSNS"] != null) && (set.Tables[0].Rows[0]["IsSNS"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsSNS"].ToString() == "1") || (set.Tables[0].Rows[0]["IsSNS"].ToString().ToLower() == "true"))
                {
                    relation.IsSNS = true;
                }
                else
                {
                    relation.IsSNS = false;
                }
            }
            if ((set.Tables[0].Rows[0]["IsComment"] != null) && (set.Tables[0].Rows[0]["IsComment"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsComment"].ToString() == "1") || (set.Tables[0].Rows[0]["IsComment"].ToString().ToLower() == "true"))
                {
                    relation.IsComment = true;
                }
                else
                {
                    relation.IsComment = false;
                }
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                relation.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["IsActive"] != null) && (set.Tables[0].Rows[0]["IsActive"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsActive"].ToString() == "1") || (set.Tables[0].Rows[0]["IsActive"].ToString().ToLower() == "true"))
                {
                    relation.IsActive = true;
                    return relation;
                }
                relation.IsActive = false;
            }
            return relation;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Ms_SEORelation ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Settings.SEORelation model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Ms_SEORelation SET ");
            builder.Append("KeyName=@KeyName,");
            builder.Append("LinkURL=@LinkURL,");
            builder.Append("IsCMS=@IsCMS,");
            builder.Append("IsShop=@IsShop,");
            builder.Append("IsSNS=@IsSNS,");
            builder.Append("IsComment=@IsComment,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("IsActive=@IsActive");
            builder.Append(" WHERE RelationID=@RelationID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyName", SqlDbType.NVarChar, 200), new SqlParameter("@LinkURL", SqlDbType.NVarChar, 500), new SqlParameter("@IsCMS", SqlDbType.Bit, 1), new SqlParameter("@IsShop", SqlDbType.Bit, 1), new SqlParameter("@IsSNS", SqlDbType.Bit, 1), new SqlParameter("@IsComment", SqlDbType.Bit, 1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsActive", SqlDbType.Bit, 1), new SqlParameter("@RelationID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.KeyName;
            cmdParms[1].Value = model.LinkURL;
            cmdParms[2].Value = model.IsCMS;
            cmdParms[3].Value = model.IsShop;
            cmdParms[4].Value = model.IsSNS;
            cmdParms[5].Value = model.IsComment;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.IsActive;
            cmdParms[8].Value = model.RelationID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

