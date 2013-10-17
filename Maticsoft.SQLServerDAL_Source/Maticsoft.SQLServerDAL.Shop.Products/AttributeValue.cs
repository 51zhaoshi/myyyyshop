namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AttributeValue : IAttributeValue
    {
        public long Add(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_AttributeValues(");
            builder.Append("AttributeId,DisplaySequence,ValueStr,ImageUrl)");
            builder.Append(" VALUES (");
            builder.Append("@AttributeId,@DisplaySequence,@ValueStr,@ImageUrl)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@ValueStr", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff) };
            cmdParms[0].Value = model.AttributeId;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.ValueStr;
            cmdParms[3].Value = model.ImageUrl;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public bool AttributeValueManage(Maticsoft.Model.Shop.Products.AttributeValue model, DataProviderAction Action)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Action", SqlDbType.Int), new SqlParameter("@ValueId", SqlDbType.BigInt), new SqlParameter("@AttributeId", SqlDbType.BigInt), new SqlParameter("@ValueStr", SqlDbType.NVarChar), new SqlParameter("@ImageUrl", SqlDbType.NVarChar) };
            parameters[0].Value = (int) Action;
            parameters[1].Value = model.ValueId;
            parameters[2].Value = model.AttributeId;
            parameters[3].Value = model.ValueStr;
            parameters[4].Value = model.ImageUrl;
            DbHelperSQL.RunProcedure("sp_Shop_AttributesValuesCreateEditDelete", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Delete(long ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_AttributeValues ");
            builder.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ValueId", SqlDbType.BigInt) };
            cmdParms[0].Value = ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteImage(long valueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_AttributeValues SET ImageUrl=N''");
            builder.Append("WHERE ValueId=@ValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ValueId", SqlDbType.BigInt) };
            cmdParms[0].Value = valueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ValueIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_AttributeValues ");
            builder.Append(" WHERE ValueId in (" + ValueIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_AttributeValues");
            builder.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ValueId", SqlDbType.BigInt) };
            cmdParms[0].Value = ValueId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAttributeValue(int? cateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT *  ");
            builder.Append("FROM Shop_AttributeValues ");
            builder.Append("WHERE AttributeId IN ( ");
            builder.Append("SELECT DISTINCT AttributeId FROM Shop_ProductAttributes ");
            builder.Append("WHERE ProductId IN(SELECT ProductId FROM Shop_Products ");
            if (cateID.HasValue)
            {
                builder.AppendFormat("WHERE CategoryId ={0} ", cateID.Value);
            }
            builder.Append(")) ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(long? AttributeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl ");
            builder.Append(" FROM Shop_AttributeValues ");
            if (AttributeId.HasValue)
            {
                builder.Append("WHERE AttributeId=@AttributeId");
                SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt) };
                cmdParms[0].Value = AttributeId.Value;
                return DbHelperSQL.Query(builder.ToString(), cmdParms);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl ");
            builder.Append(" FROM Shop_AttributeValues ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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
            builder.Append(" ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl ");
            builder.Append(" FROM Shop_AttributeValues ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByAttribute(long AttributeId)
        {
            return this.GetList(" AttributeId=" + AttributeId.ToString() + " ORDER BY DisplaySequence ");
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.ValueId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_AttributeValues T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Products.AttributeValue GetModel(long ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ValueId,AttributeId,DisplaySequence,ValueStr,ImageUrl FROM Shop_AttributeValues ");
            builder.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ValueId", SqlDbType.BigInt) };
            cmdParms[0].Value = ValueId;
            Maticsoft.Model.Shop.Products.AttributeValue value2 = new Maticsoft.Model.Shop.Products.AttributeValue();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ValueId"] != null) && (set.Tables[0].Rows[0]["ValueId"].ToString() != ""))
            {
                value2.ValueId = long.Parse(set.Tables[0].Rows[0]["ValueId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AttributeId"] != null) && (set.Tables[0].Rows[0]["AttributeId"].ToString() != ""))
            {
                value2.AttributeId = long.Parse(set.Tables[0].Rows[0]["AttributeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["DisplaySequence"] != null) && (set.Tables[0].Rows[0]["DisplaySequence"].ToString() != ""))
            {
                value2.DisplaySequence = int.Parse(set.Tables[0].Rows[0]["DisplaySequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ValueStr"] != null) && (set.Tables[0].Rows[0]["ValueStr"].ToString() != ""))
            {
                value2.ValueStr = set.Tables[0].Rows[0]["ValueStr"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ImageUrl"] != null) && (set.Tables[0].Rows[0]["ImageUrl"].ToString() != ""))
            {
                value2.ImageUrl = set.Tables[0].Rows[0]["ImageUrl"].ToString();
            }
            return value2;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_AttributeValues ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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

        public bool Update(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_AttributeValues SET ");
            builder.Append("AttributeId=@AttributeId,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("ValueStr=@ValueStr,");
            builder.Append("ImageUrl=@ImageUrl");
            builder.Append(" WHERE ValueId=@ValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@ValueStr", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ValueId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.AttributeId;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.ValueStr;
            cmdParms[3].Value = model.ImageUrl;
            cmdParms[4].Value = model.ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

