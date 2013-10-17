namespace Maticsoft.SQLServerDAL.Shop.Gift
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Gift;
    using Maticsoft.Model.Shop.Gift;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Gifts : IGifts
    {
        public int Add(Maticsoft.Model.Shop.Gift.Gifts model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Gifts(");
            builder.Append("CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled)");
            builder.Append(" values (");
            builder.Append("@CategoryID,@Name,@ShortDescription,@Unit,@Weight,@LongDescription,@Title,@Meta_Description,@Meta_Keywords,@ThumbnailsUrl,@InFocusImageUrl,@CostPrice,@MarketPrice,@SalePrice,@Stock,@NeedPoint,@NeedGrade,@SaleCounts,@CreateDate,@Enabled)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@ShortDescription", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Unit", SqlDbType.NVarChar, 50), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@LongDescription", SqlDbType.NText), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@InFocusImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@SalePrice", SqlDbType.Money, 8), new SqlParameter("@Stock", SqlDbType.Int, 4), new SqlParameter("@NeedPoint", SqlDbType.Int, 4), 
                new SqlParameter("@NeedGrade", SqlDbType.Int, 4), new SqlParameter("@SaleCounts", SqlDbType.Int, 4), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@Enabled", SqlDbType.Bit, 1)
             };
            cmdParms[0].Value = model.CategoryID;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.ShortDescription;
            cmdParms[3].Value = model.Unit;
            cmdParms[4].Value = model.Weight;
            cmdParms[5].Value = model.LongDescription;
            cmdParms[6].Value = model.Title;
            cmdParms[7].Value = model.Meta_Description;
            cmdParms[8].Value = model.Meta_Keywords;
            cmdParms[9].Value = model.ThumbnailsUrl;
            cmdParms[10].Value = model.InFocusImageUrl;
            cmdParms[11].Value = model.CostPrice;
            cmdParms[12].Value = model.MarketPrice;
            cmdParms[13].Value = model.SalePrice;
            cmdParms[14].Value = model.Stock;
            cmdParms[15].Value = model.NeedPoint;
            cmdParms[0x10].Value = model.NeedGrade;
            cmdParms[0x11].Value = model.SaleCounts;
            cmdParms[0x12].Value = model.CreateDate;
            cmdParms[0x13].Value = model.Enabled;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int GiftId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Gifts ");
            builder.Append(" where GiftId=@GiftId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GiftId", SqlDbType.Int, 4) };
            cmdParms[0].Value = GiftId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string GiftIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Gifts ");
            builder.Append(" where GiftId in (" + GiftIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int GiftId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Gifts");
            builder.Append(" where GiftId=@GiftId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GiftId", SqlDbType.Int, 4) };
            cmdParms[0].Value = GiftId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select GiftId,CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled ");
            builder.Append(" FROM Shop_Gifts ");
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
            builder.Append(" GiftId,CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled ");
            builder.Append(" FROM Shop_Gifts ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.GiftId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Gifts T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("GiftId", "Shop_Gifts");
        }

        public Maticsoft.Model.Shop.Gift.Gifts GetModel(int GiftId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 GiftId,CategoryID,Name,ShortDescription,Unit,Weight,LongDescription,Title,Meta_Description,Meta_Keywords,ThumbnailsUrl,InFocusImageUrl,CostPrice,MarketPrice,SalePrice,Stock,NeedPoint,NeedGrade,SaleCounts,CreateDate,Enabled from Shop_Gifts ");
            builder.Append(" where GiftId=@GiftId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GiftId", SqlDbType.Int, 4) };
            cmdParms[0].Value = GiftId;
            Maticsoft.Model.Shop.Gift.Gifts gifts = new Maticsoft.Model.Shop.Gift.Gifts();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["GiftId"] != null) && (set.Tables[0].Rows[0]["GiftId"].ToString() != ""))
            {
                gifts.GiftId = int.Parse(set.Tables[0].Rows[0]["GiftId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CategoryID"] != null) && (set.Tables[0].Rows[0]["CategoryID"].ToString() != ""))
            {
                gifts.CategoryID = int.Parse(set.Tables[0].Rows[0]["CategoryID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Name"] != null) && (set.Tables[0].Rows[0]["Name"].ToString() != ""))
            {
                gifts.Name = set.Tables[0].Rows[0]["Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ShortDescription"] != null) && (set.Tables[0].Rows[0]["ShortDescription"].ToString() != ""))
            {
                gifts.ShortDescription = set.Tables[0].Rows[0]["ShortDescription"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Unit"] != null) && (set.Tables[0].Rows[0]["Unit"].ToString() != ""))
            {
                gifts.Unit = set.Tables[0].Rows[0]["Unit"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Weight"] != null) && (set.Tables[0].Rows[0]["Weight"].ToString() != ""))
            {
                gifts.Weight = int.Parse(set.Tables[0].Rows[0]["Weight"].ToString());
            }
            if ((set.Tables[0].Rows[0]["LongDescription"] != null) && (set.Tables[0].Rows[0]["LongDescription"].ToString() != ""))
            {
                gifts.LongDescription = set.Tables[0].Rows[0]["LongDescription"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Title"] != null) && (set.Tables[0].Rows[0]["Title"].ToString() != ""))
            {
                gifts.Title = set.Tables[0].Rows[0]["Title"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Description"] != null) && (set.Tables[0].Rows[0]["Meta_Description"].ToString() != ""))
            {
                gifts.Meta_Description = set.Tables[0].Rows[0]["Meta_Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Keywords"] != null) && (set.Tables[0].Rows[0]["Meta_Keywords"].ToString() != ""))
            {
                gifts.Meta_Keywords = set.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailsUrl"] != null) && (set.Tables[0].Rows[0]["ThumbnailsUrl"].ToString() != ""))
            {
                gifts.ThumbnailsUrl = set.Tables[0].Rows[0]["ThumbnailsUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["InFocusImageUrl"] != null) && (set.Tables[0].Rows[0]["InFocusImageUrl"].ToString() != ""))
            {
                gifts.InFocusImageUrl = set.Tables[0].Rows[0]["InFocusImageUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CostPrice"] != null) && (set.Tables[0].Rows[0]["CostPrice"].ToString() != ""))
            {
                gifts.CostPrice = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["CostPrice"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["MarketPrice"] != null) && (set.Tables[0].Rows[0]["MarketPrice"].ToString() != ""))
            {
                gifts.MarketPrice = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["MarketPrice"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["SalePrice"] != null) && (set.Tables[0].Rows[0]["SalePrice"].ToString() != ""))
            {
                gifts.SalePrice = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["SalePrice"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Stock"] != null) && (set.Tables[0].Rows[0]["Stock"].ToString() != ""))
            {
                gifts.Stock = new int?(int.Parse(set.Tables[0].Rows[0]["Stock"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["NeedPoint"] != null) && (set.Tables[0].Rows[0]["NeedPoint"].ToString() != ""))
            {
                gifts.NeedPoint = int.Parse(set.Tables[0].Rows[0]["NeedPoint"].ToString());
            }
            if ((set.Tables[0].Rows[0]["NeedGrade"] != null) && (set.Tables[0].Rows[0]["NeedGrade"].ToString() != ""))
            {
                gifts.NeedGrade = int.Parse(set.Tables[0].Rows[0]["NeedGrade"].ToString());
            }
            if ((set.Tables[0].Rows[0]["SaleCounts"] != null) && (set.Tables[0].Rows[0]["SaleCounts"].ToString() != ""))
            {
                gifts.SaleCounts = int.Parse(set.Tables[0].Rows[0]["SaleCounts"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CreateDate"] != null) && (set.Tables[0].Rows[0]["CreateDate"].ToString() != ""))
            {
                gifts.CreateDate = DateTime.Parse(set.Tables[0].Rows[0]["CreateDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Enabled"] != null) && (set.Tables[0].Rows[0]["Enabled"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["Enabled"].ToString() == "1") || (set.Tables[0].Rows[0]["Enabled"].ToString().ToLower() == "true"))
                {
                    gifts.Enabled = true;
                    return gifts;
                }
                gifts.Enabled = false;
            }
            return gifts;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_Gifts ");
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

        public bool Update(Maticsoft.Model.Shop.Gift.Gifts model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Gifts set ");
            builder.Append("CategoryID=@CategoryID,");
            builder.Append("Name=@Name,");
            builder.Append("ShortDescription=@ShortDescription,");
            builder.Append("Unit=@Unit,");
            builder.Append("Weight=@Weight,");
            builder.Append("LongDescription=@LongDescription,");
            builder.Append("Title=@Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("ThumbnailsUrl=@ThumbnailsUrl,");
            builder.Append("InFocusImageUrl=@InFocusImageUrl,");
            builder.Append("CostPrice=@CostPrice,");
            builder.Append("MarketPrice=@MarketPrice,");
            builder.Append("SalePrice=@SalePrice,");
            builder.Append("Stock=@Stock,");
            builder.Append("NeedPoint=@NeedPoint,");
            builder.Append("NeedGrade=@NeedGrade,");
            builder.Append("SaleCounts=@SaleCounts,");
            builder.Append("CreateDate=@CreateDate,");
            builder.Append("Enabled=@Enabled");
            builder.Append(" where GiftId=@GiftId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@ShortDescription", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Unit", SqlDbType.NVarChar, 50), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@LongDescription", SqlDbType.NText), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@InFocusImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@SalePrice", SqlDbType.Money, 8), new SqlParameter("@Stock", SqlDbType.Int, 4), new SqlParameter("@NeedPoint", SqlDbType.Int, 4), 
                new SqlParameter("@NeedGrade", SqlDbType.Int, 4), new SqlParameter("@SaleCounts", SqlDbType.Int, 4), new SqlParameter("@CreateDate", SqlDbType.DateTime), new SqlParameter("@Enabled", SqlDbType.Bit, 1), new SqlParameter("@GiftId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.CategoryID;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.ShortDescription;
            cmdParms[3].Value = model.Unit;
            cmdParms[4].Value = model.Weight;
            cmdParms[5].Value = model.LongDescription;
            cmdParms[6].Value = model.Title;
            cmdParms[7].Value = model.Meta_Description;
            cmdParms[8].Value = model.Meta_Keywords;
            cmdParms[9].Value = model.ThumbnailsUrl;
            cmdParms[10].Value = model.InFocusImageUrl;
            cmdParms[11].Value = model.CostPrice;
            cmdParms[12].Value = model.MarketPrice;
            cmdParms[13].Value = model.SalePrice;
            cmdParms[14].Value = model.Stock;
            cmdParms[15].Value = model.NeedPoint;
            cmdParms[0x10].Value = model.NeedGrade;
            cmdParms[0x11].Value = model.SaleCounts;
            cmdParms[0x12].Value = model.CreateDate;
            cmdParms[0x13].Value = model.Enabled;
            cmdParms[20].Value = model.GiftId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStock(int giftid, int stock)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Gifts set ");
            builder.Append("Stock=@Stock");
            builder.Append(" where GiftId=@GiftId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Stock", SqlDbType.Int, 4), new SqlParameter("@GiftId", SqlDbType.Int, 4) };
            cmdParms[0].Value = stock;
            cmdParms[1].Value = giftid;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

