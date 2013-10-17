namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Text;

    public class AttributeInfo : IAttributeInfo
    {
        public long Add(Maticsoft.Model.Shop.Products.AttributeInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_Attributes(");
            builder.Append("AttributeName,DisplaySequence,TypeId,UsageMode,UseAttributeImage)");
            builder.Append(" VALUES (");
            builder.Append("@AttributeName,@DisplaySequence,@TypeId,@UsageMode,@UseAttributeImage)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeName", SqlDbType.NVarChar, 50), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@UsageMode", SqlDbType.Int, 4), new SqlParameter("@UseAttributeImage", SqlDbType.Bit, 1) };
            cmdParms[0].Value = model.AttributeName;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.TypeId;
            cmdParms[3].Value = model.UsageMode;
            cmdParms[4].Value = model.UseAttributeImage;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public bool AttributeManage(Maticsoft.Model.Shop.Products.AttributeInfo model, DataProviderAction Action)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Action", SqlDbType.Int), new SqlParameter("@AttributeId", SqlDbType.BigInt), new SqlParameter("@AttributeName", SqlDbType.NVarChar), new SqlParameter("@TypeId", SqlDbType.Int), new SqlParameter("@UsageMode", SqlDbType.Int), new SqlParameter("@UseAttributeImage", SqlDbType.Bit), new SqlParameter("@UserDefinedPic", SqlDbType.Bit), new SqlParameter("@AttributeIdOutPut", SqlDbType.BigInt) };
            parameters[0].Value = (int) Action;
            parameters[1].Value = model.AttributeId;
            parameters[2].Value = model.AttributeName;
            parameters[3].Value = model.TypeId;
            parameters[4].Value = model.UsageMode;
            parameters[5].Value = model.UseAttributeImage;
            parameters[6].Value = model.UserDefinedPic;
            parameters[7].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure("sp_Shop_AttributesCreateEditDelete", parameters, out rowsAffected);
            long attributeId = 0L;
            if (Action == DataProviderAction.Create)
            {
                attributeId = Convert.ToInt64(parameters[7].Value);
            }
            else
            {
                attributeId = model.AttributeId;
            }
            if (rowsAffected <= 0)
            {
                return false;
            }
            Maticsoft.SQLServerDAL.Shop.Products.AttributeValue value2 = new Maticsoft.SQLServerDAL.Shop.Products.AttributeValue();
            foreach (string str in model.ValueStr)
            {
                Maticsoft.Model.Shop.Products.AttributeValue value3 = new Maticsoft.Model.Shop.Products.AttributeValue {
                    AttributeId = attributeId,
                    ValueStr = str
                };
                value2.AttributeValueManage(value3, DataProviderAction.Create);
            }
            return true;
        }

        public bool ChangeImageStatue(long AttributeId, ProductAttributeModel status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_Attributes ");
            builder.Append("SET UsageMode=@Status ");
            builder.Append("WHERE AttributeId=@AttributeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt), new SqlParameter("@Status", SqlDbType.Int) };
            cmdParms[0].Value = AttributeId;
            cmdParms[1].Value = (int) status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(long AttributeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Attributes ");
            builder.Append(" WHERE AttributeId=@AttributeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt) };
            cmdParms[0].Value = AttributeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AttributeIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Attributes ");
            builder.Append(" WHERE AttributeId in (" + AttributeIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long AttributeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Attributes");
            builder.Append(" WHERE AttributeId=@AttributeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt) };
            cmdParms[0].Value = AttributeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        private List<Maticsoft.Model.Shop.Products.AttributeInfo> FillAttributeInfos(DataSet ds)
        {
            List<Maticsoft.Model.Shop.Products.AttributeInfo> list = new List<Maticsoft.Model.Shop.Products.AttributeInfo>();
            using (IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator())
            {
                Predicate<Maticsoft.Model.Shop.Products.AttributeInfo> match = null;
                DataRow dataRow;
                while (enumerator.MoveNext())
                {
                    dataRow = (DataRow) enumerator.Current;
                    if (match == null)
                    {
                        match = info => info.AttributeId.ToString(CultureInfo.InvariantCulture) == dataRow["AttributeId"].ToString();
                    }
                    Maticsoft.Model.Shop.Products.AttributeInfo item = list.Find(match);
                    if (item == null)
                    {
                        int num = (int) dataRow["UsageMode"];
                        if ((num != 2) && (dataRow["ValueId"] == DBNull.Value))
                        {
                            continue;
                        }
                        item = new Maticsoft.Model.Shop.Products.AttributeInfo {
                            AttributeId = (long) dataRow["AttributeId"],
                            AttributeName = dataRow["AttributeName"].ToString(),
                            DisplaySequence = (int) dataRow["AttributeDisplaySequence"],
                            TypeId = (int) dataRow["TypeId"],
                            UsageMode = num,
                            UseAttributeImage = (bool) dataRow["UseAttributeImage"],
                            UserDefinedPic = (bool) dataRow["UserDefinedPic"]
                        };
                        list.Add(item);
                    }
                    if (dataRow["ValueId"] != DBNull.Value)
                    {
                        Maticsoft.Model.Shop.Products.AttributeValue value2 = new Maticsoft.Model.Shop.Products.AttributeValue {
                            ValueId = (long) dataRow["ValueId"],
                            AttributeId = (long) dataRow["AttributeId"],
                            DisplaySequence = (int) dataRow["ValueDisplaySequence"],
                            ValueStr = (dataRow["ValueStr"] != DBNull.Value) ? dataRow["ValueStr"].ToString() : string.Empty,
                            ImageUrl = (dataRow["ImageUrl"] != DBNull.Value) ? dataRow["ImageUrl"].ToString() : string.Empty
                        };
                        item.AttributeValues.Add(value2);
                        item.ValueStr.Add(dataRow.Field<string>("ValueStr"));
                    }
                }
            }
            return list;
        }

        public DataSet GetAttribute(int? cateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP(6)*  ");
            builder.Append("FROM Shop_Attributes ");
            builder.Append("WHERE AttributeId IN(SELECT DISTINCT AttributeId FROM Shop_ProductAttributes ");
            builder.Append("WHERE ProductId IN(SELECT ProductId FROM Shop_Products ");
            if (cateID.HasValue)
            {
                builder.AppendFormat("WHERE CategoryId ={0} ", cateID.Value);
            }
            builder.Append(")) ");
            builder.Append("ORDER BY Shop_Attributes.DisplaySequence ASC ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetAttributeInfoList(int? typeId, SearchType searchType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  A.AttributeId ,\r\n                                A.AttributeName ,\r\n                                A.DisplaySequence AS AttributeDisplaySequence ,\r\n                                A.TypeId ,\r\n                                A.UsageMode ,\r\n                                A.UseAttributeImage ,\r\n                                A.UserDefinedPic ,\r\n                                V.ValueId,\r\n                                V.DisplaySequence AS ValueDisplaySequence ,\r\n                                V.ValueStr ,\r\n                                V.ImageUrl\r\n                            FROM Shop_Attributes A\r\n                                LEFT JOIN Shop_AttributeValues V ON A.AttributeId = V.AttributeId\r\n                            WHERE ");
            List<SqlParameter> list = new List<SqlParameter>();
            if (typeId.HasValue)
            {
                builder.Append(" A.TypeId = @TypeId");
                list.Add(new SqlParameter("@TypeId", SqlDbType.Int));
                list[0].Value = typeId;
            }
            switch (searchType)
            {
                case SearchType.ExtAttribute:
                    if (typeId.HasValue)
                    {
                        builder.Append(" AND ");
                    }
                    builder.Append(" A.UsageMode <> 3");
                    break;

                case SearchType.Specification:
                    if (typeId.HasValue)
                    {
                        builder.Append(" AND ");
                    }
                    builder.Append(" A.UsageMode = 3");
                    break;
            }
            builder.Append(" ORDER BY A.DisplaySequence,V.AttributeId,V.DisplaySequence");
            DataSet ds = (list.Count > 0) ? DbHelperSQL.Query(builder.ToString(), list.ToArray()) : DbHelperSQL.Query(builder.ToString());
            if ((ds != null) && (ds.Tables[0].Rows.Count >= 0))
            {
                return this.FillAttributeInfos(ds);
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetAttributeInfoListByProductId(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\nSELECT  A.AttributeId\r\n      , A.AttributeName\r\n      , A.DisplaySequence AS AttributeDisplaySequence\r\n      , A.TypeId\r\n      , A.UsageMode\r\n      , A.UseAttributeImage\r\n      , A.UserDefinedPic\r\n      , V.ValueId\r\n      , V.DisplaySequence AS ValueDisplaySequence\r\n      , V.ValueStr\r\n      , V.ImageUrl\r\nFROM  Shop_ProductAttributes PA LEFT JOIN  Shop_Attributes A ON PA.AttributeId = A.AttributeId\r\n        LEFT JOIN Shop_AttributeValues V ON PA.ValueId = V.ValueId\r\nWHERE PA.ProductId = @ProductId");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@ProductId", 8)
            };
            list[0].Value = productId;
            builder.Append(" ORDER BY A.DisplaySequence,V.AttributeId,V.DisplaySequence");
            DataSet ds = (list.Count > 0) ? DbHelperSQL.Query(builder.ToString(), list.ToArray()) : DbHelperSQL.Query(builder.ToString());
            if ((ds != null) && (ds.Tables[0].Rows.Count >= 0))
            {
                return this.FillAttributeInfos(ds);
            }
            return null;
        }

        public DataSet GetAttributesByCate(int cateID, bool IsChild)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  * FROM    Shop_Attributes ");
            if (cateID > 0)
            {
                builder.Append(" WHERE   EXISTS ( SELECT * FROM   Shop_Products ");
                builder.Append("  WHERE  SaleStatus=1 and  TypeId = Shop_Attributes.TypeId ");
                builder.Append(" AND EXISTS ( SELECT * FROM   Shop_ProductCategories  ");
                builder.Append(" WHERE  ProductId = Shop_Products.ProductId  ");
                if (IsChild)
                {
                    builder.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", cateID);
                    builder.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0})", cateID);
                }
                else
                {
                    builder.AppendFormat("  Shop_ProductCategories.CategoryId = {0}", cateID);
                }
                builder.Append(")) ");
            }
            builder.Append("ORDER BY Shop_Attributes.DisplaySequence ASC ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT AttributeId,AttributeName,DisplaySequence,TypeId,UsageMode,UseAttributeImage ");
            builder.Append(" FROM Shop_Attributes ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(long? Typeid, SearchType searchType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT AttributeId,AttributeName,DisplaySequence,TypeId,UsageMode,UseAttributeImage ");
            builder.Append(" FROM Shop_Attributes ");
            StringBuilder builder2 = new StringBuilder();
            if (searchType == SearchType.ExtAttribute)
            {
                if (!string.IsNullOrWhiteSpace(builder2.ToString()))
                {
                    builder2.Append(" AND ");
                }
                builder2.Append("   UsageMode <>3 ");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(builder2.ToString()))
                {
                    builder2.Append(" AND ");
                }
                builder2.Append("   UsageMode =3 ");
            }
            if (Typeid.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(builder2.ToString()))
                {
                    builder2.Append(" AND ");
                }
                builder2.Append("  TypeId=@TypeId");
                builder.Append(" WHERE  " + builder2.ToString());
                builder.Append(" ORDER BY DisplaySequence ");
                SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.BigInt) };
                cmdParms[0].Value = Typeid.Value;
                return DbHelperSQL.Query(builder.ToString(), cmdParms);
            }
            builder.Append(" WHERE  " + builder2.ToString());
            builder.Append(" ORDER BY DisplaySequence ");
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
            builder.Append(" AttributeId,AttributeName,DisplaySequence,TypeId,UsageMode,UseAttributeImage ");
            builder.Append(" FROM Shop_Attributes ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.AttributeId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_Attributes T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Products.AttributeInfo GetModel(long AttributeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 AttributeId,AttributeName,DisplaySequence,TypeId,UsageMode,UseAttributeImage FROM Shop_Attributes ");
            builder.Append(" WHERE AttributeId=@AttributeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt) };
            cmdParms[0].Value = AttributeId;
            Maticsoft.Model.Shop.Products.AttributeInfo info = new Maticsoft.Model.Shop.Products.AttributeInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["AttributeId"] != null) && (set.Tables[0].Rows[0]["AttributeId"].ToString() != ""))
            {
                info.AttributeId = long.Parse(set.Tables[0].Rows[0]["AttributeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AttributeName"] != null) && (set.Tables[0].Rows[0]["AttributeName"].ToString() != ""))
            {
                info.AttributeName = set.Tables[0].Rows[0]["AttributeName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["DisplaySequence"] != null) && (set.Tables[0].Rows[0]["DisplaySequence"].ToString() != ""))
            {
                info.DisplaySequence = int.Parse(set.Tables[0].Rows[0]["DisplaySequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["TypeId"] != null) && (set.Tables[0].Rows[0]["TypeId"].ToString() != ""))
            {
                info.TypeId = int.Parse(set.Tables[0].Rows[0]["TypeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UsageMode"] != null) && (set.Tables[0].Rows[0]["UsageMode"].ToString() != ""))
            {
                info.UsageMode = int.Parse(set.Tables[0].Rows[0]["UsageMode"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UseAttributeImage"] != null) && (set.Tables[0].Rows[0]["UseAttributeImage"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["UseAttributeImage"].ToString() == "1") || (set.Tables[0].Rows[0]["UseAttributeImage"].ToString().ToLower() == "true"))
                {
                    info.UseAttributeImage = true;
                    return info;
                }
                info.UseAttributeImage = false;
            }
            return info;
        }

        public DataSet GetProductAttributes(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.ValueId,B.* FROM  ");
            builder.Append("Shop_ProductAttributes A ");
            builder.Append("LEFT JOIN Shop_Attributes B ON A.AttributeId = B.AttributeId ");
            builder.Append("WHERE ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = productId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Attributes ");
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

        public bool IsExistDefinedAttribute(int typeId, long? attId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Attributes ");
            builder.AppendFormat("WHERE UsageMode=3 AND TypeId={0} AND UserDefinedPic=1 ", typeId);
            if (attId.HasValue)
            {
                builder.AppendFormat("  AND AttributeId={0} ", attId.Value);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return false;
            }
            return (Convert.ToInt32(single) > 0);
        }

        public bool IsExistName(int typeId, string name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Attributes");
            builder.Append(" WHERE TypeId=@TypeId and AttributeName=@AttributeName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@AttributeName", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = typeId;
            cmdParms[1].Value = name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Update(Maticsoft.Model.Shop.Products.AttributeInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_Attributes SET ");
            builder.Append("AttributeName=@AttributeName,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("TypeId=@TypeId,");
            builder.Append("UsageMode=@UsageMode,");
            builder.Append("UseAttributeImage=@UseAttributeImage");
            builder.Append(" WHERE AttributeId=@AttributeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AttributeName", SqlDbType.NVarChar, 50), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@UsageMode", SqlDbType.Int, 4), new SqlParameter("@UseAttributeImage", SqlDbType.Bit, 1), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.AttributeName;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.TypeId;
            cmdParms[3].Value = model.UsageMode;
            cmdParms[4].Value = model.UseAttributeImage;
            cmdParms[5].Value = model.AttributeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

