namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Products : IProducts
    {
        public long Add(Maticsoft.Model.SNS.Products model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Products(");
            builder.Append("ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl)");
            builder.Append(" values (");
            builder.Append("@ProductName,@ProductDescription,@Price,@ProductSourceID,@CategoryID,@ProductUrl,@FavouriteCount,@GroupBuyCount,@CreateUserID,@CreatedNickName,@ThumbImageUrl,@NormalImageUrl,@PVCount,@IsRecomend,@Status,@Sequence,@TopCommentsId,@CommentCount,@ForwardedCount,@ShareDescription,@SkipCount,@OwnerProductId,@Tags,@CreatedDate,@Color,@OriginalID,@SourceType,@StaticUrl)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@ProductDescription", SqlDbType.NVarChar, 500), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductSourceID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@GroupBuyCount", SqlDbType.Int, 4), new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), 
                new SqlParameter("@TopCommentsId", SqlDbType.NVarChar, 100), new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@ForwardedCount", SqlDbType.Int, 4), new SqlParameter("@ShareDescription", SqlDbType.NVarChar, 200), new SqlParameter("@SkipCount", SqlDbType.Int, 4), new SqlParameter("@OwnerProductId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 400), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Color", SqlDbType.NVarChar, 100), new SqlParameter("@OriginalID", SqlDbType.BigInt, 8), new SqlParameter("@SourceType", SqlDbType.Int, 4), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 300)
             };
            cmdParms[0].Value = model.ProductName;
            cmdParms[1].Value = model.ProductDescription;
            cmdParms[2].Value = model.Price;
            cmdParms[3].Value = model.ProductSourceID;
            cmdParms[4].Value = model.CategoryID;
            cmdParms[5].Value = model.ProductUrl;
            cmdParms[6].Value = model.FavouriteCount;
            cmdParms[7].Value = model.GroupBuyCount;
            cmdParms[8].Value = model.CreateUserID;
            cmdParms[9].Value = model.CreatedNickName;
            cmdParms[10].Value = model.ThumbImageUrl;
            cmdParms[11].Value = model.NormalImageUrl;
            cmdParms[12].Value = model.PVCount;
            cmdParms[13].Value = model.IsRecomend;
            cmdParms[14].Value = model.Status;
            cmdParms[15].Value = model.Sequence;
            cmdParms[0x10].Value = model.TopCommentsId;
            cmdParms[0x11].Value = model.CommentCount;
            cmdParms[0x12].Value = model.ForwardedCount;
            cmdParms[0x13].Value = model.ShareDescription;
            cmdParms[20].Value = model.SkipCount;
            cmdParms[0x15].Value = model.OwnerProductId;
            cmdParms[0x16].Value = model.Tags;
            cmdParms[0x17].Value = model.CreatedDate;
            cmdParms[0x18].Value = model.Color;
            cmdParms[0x19].Value = model.OriginalID;
            cmdParms[0x1a].Value = model.SourceType;
            cmdParms[0x1b].Value = model.StaticUrl;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.SNS.Products DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Products products = new Maticsoft.Model.SNS.Products();
            if (row != null)
            {
                if ((row["ProductID"] != null) && (row["ProductID"].ToString() != ""))
                {
                    products.ProductID = long.Parse(row["ProductID"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    products.ProductName = row["ProductName"].ToString();
                }
                if (row["ProductDescription"] != null)
                {
                    products.ProductDescription = row["ProductDescription"].ToString();
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    products.Price = new decimal?(decimal.Parse(row["Price"].ToString()));
                }
                if ((row["ProductSourceID"] != null) && (row["ProductSourceID"].ToString() != ""))
                {
                    products.ProductSourceID = new int?(int.Parse(row["ProductSourceID"].ToString()));
                }
                if ((row["CategoryID"] != null) && (row["CategoryID"].ToString() != ""))
                {
                    products.CategoryID = new int?(int.Parse(row["CategoryID"].ToString()));
                }
                if (row["ProductUrl"] != null)
                {
                    products.ProductUrl = row["ProductUrl"].ToString();
                }
                if ((row["FavouriteCount"] != null) && (row["FavouriteCount"].ToString() != ""))
                {
                    products.FavouriteCount = int.Parse(row["FavouriteCount"].ToString());
                }
                if ((row["GroupBuyCount"] != null) && (row["GroupBuyCount"].ToString() != ""))
                {
                    products.GroupBuyCount = new int?(int.Parse(row["GroupBuyCount"].ToString()));
                }
                if ((row["CreateUserID"] != null) && (row["CreateUserID"].ToString() != ""))
                {
                    products.CreateUserID = int.Parse(row["CreateUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    products.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["ThumbImageUrl"] != null)
                {
                    products.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if (row["NormalImageUrl"] != null)
                {
                    products.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if ((row["PVCount"] != null) && (row["PVCount"].ToString() != ""))
                {
                    products.PVCount = int.Parse(row["PVCount"].ToString());
                }
                if ((row["IsRecomend"] != null) && (row["IsRecomend"].ToString() != ""))
                {
                    products.IsRecomend = int.Parse(row["IsRecomend"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    products.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    products.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["TopCommentsId"] != null)
                {
                    products.TopCommentsId = row["TopCommentsId"].ToString();
                }
                if ((row["CommentCount"] != null) && (row["CommentCount"].ToString() != ""))
                {
                    products.CommentCount = int.Parse(row["CommentCount"].ToString());
                }
                if ((row["ForwardedCount"] != null) && (row["ForwardedCount"].ToString() != ""))
                {
                    products.ForwardedCount = new int?(int.Parse(row["ForwardedCount"].ToString()));
                }
                if (row["ShareDescription"] != null)
                {
                    products.ShareDescription = row["ShareDescription"].ToString();
                }
                if ((row["SkipCount"] != null) && (row["SkipCount"].ToString() != ""))
                {
                    products.SkipCount = int.Parse(row["SkipCount"].ToString());
                }
                if ((row["OwnerProductId"] != null) && (row["OwnerProductId"].ToString() != ""))
                {
                    products.OwnerProductId = new int?(int.Parse(row["OwnerProductId"].ToString()));
                }
                if (row["Tags"] != null)
                {
                    products.Tags = row["Tags"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    products.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Color"] != null)
                {
                    products.Color = row["Color"].ToString();
                }
                if ((row["OriginalID"] != null) && (row["OriginalID"].ToString() != ""))
                {
                    products.OriginalID = new long?(long.Parse(row["OriginalID"].ToString()));
                }
                if ((row["SourceType"] != null) && (row["SourceType"].ToString() != ""))
                {
                    products.SourceType = new int?(int.Parse(row["SourceType"].ToString()));
                }
                if (row["StaticUrl"] != null)
                {
                    products.StaticUrl = row["StaticUrl"].ToString();
                }
            }
            return products;
        }

        public bool Delete(long ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Products ");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.BigInt) };
            cmdParms[0].Value = ProductID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEX(int ProductID)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SNS_UserFavourite ");
            builder.Append(" where type=1 and TargetID=@TargetId ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            para[0].Value = ProductID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update Accounts_UsersExp set  ShareCount=ShareCount-1,ProductsCount=ProductsCount-1");
            builder2.Append(" where UserID=( Select CreateUserID  from SNS_Products where ProductID=@ProductID) ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = ProductID;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            builder3.Append("  where AlbumID=( Select AlbumID  from SNS_UserAlbumDetail where type=1 and TargetID=@TargetId)");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray3[0].Value = ProductID;
            item = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(item);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete SNS_Posts ");
            builder4.Append(" where Type=2 and TargetId=@TargetId ");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray4[0].Value = ProductID;
            item = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(item);
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete SNS_Comments ");
            builder5.Append(" where type=2 and TargetID=@TargetId ");
            SqlParameter[] parameterArray5 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray5[0].Value = ProductID;
            item = new CommandInfo(builder5.ToString(), parameterArray5);
            cmdList.Add(item);
            StringBuilder builder6 = new StringBuilder();
            builder6.Append("delete SNS_UserAlbumDetail ");
            builder6.Append(" where type=1 and TargetID=@TargetId ");
            SqlParameter[] parameterArray6 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray6[0].Value = ProductID;
            item = new CommandInfo(builder6.ToString(), parameterArray6);
            cmdList.Add(item);
            StringBuilder builder7 = new StringBuilder();
            builder7.Append("delete SNS_Products ");
            builder7.Append(" where ProductID=@ProductID");
            SqlParameter[] parameterArray7 = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            parameterArray7[0].Value = ProductID;
            item = new CommandInfo(builder7.ToString(), parameterArray7);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool DeleteList(string ProductIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Products ");
            builder.Append(" where ProductID in (" + ProductIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet DeleteListEx(string Ids, out int Result)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetIds ", SqlDbType.NVarChar), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = 2;
            parameters[1].Value = Ids;
            DataSet set = DbHelperSQL.RunProcedure("sp_SNS_ImageDeleteAction", parameters, "tb", out Result);
            if (Result == 1)
            {
                return set;
            }
            return null;
        }

        public bool DeleteListEX(string ProductIds)
        {
            int length = ProductIds.Split(new char[] { ',' }).Length;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "Update Accounts_UsersExp set  ShareCount=ShareCount-", length, " ,ProductsCount=ProductsCount-", length }));
            builder.Append(" where UserID in ( Select CreateUserID  from SNS_Products where ProductID in (" + ProductIds + ")) ");
            SqlParameter[] para = new SqlParameter[0];
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            builder2.Append("  where AlbumID in ( Select AlbumID  from SNS_UserAlbumDetail where type=1 and TargetID in (" + ProductIds + "))");
            item = new CommandInfo(builder2.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete SNS_Products ");
            builder3.Append(" where ProductID in (" + ProductIds + ")");
            item = new CommandInfo(builder3.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete SNS_Posts ");
            builder4.Append(" where Type=2 and TargetId in (" + ProductIds + ") ");
            item = new CommandInfo(builder4.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete SNS_Comments ");
            builder5.Append(" where type=2 and TargetID in (" + ProductIds + ") ");
            item = new CommandInfo(builder5.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder6 = new StringBuilder();
            builder6.Append("delete SNS_UserFavourite ");
            builder6.Append(" where type=1 and TargetID in (" + ProductIds + ") ");
            item = new CommandInfo(builder6.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder7 = new StringBuilder();
            builder7.Append("delete SNS_UserAlbumDetail ");
            builder7.Append(" where type=1 and TargetID in (" + ProductIds + ") ");
            item = new CommandInfo(builder7.ToString(), para);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool Exists(long ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Products");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.BigInt) };
            cmdParms[0].Value = ProductID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exsit(long originalID, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Products");
            builder.Append(" where originalID=@OriginalID and ProductSourceID=@ProductSourceID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OriginalID", SqlDbType.BigInt), new SqlParameter("@ProductSourceID", SqlDbType.Int, 4) };
            cmdParms[0].Value = originalID;
            cmdParms[1].Value = type;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exsit(string ProductName, int Uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Products");
            builder.Append(" where ProductName=@ProductName and CreateUserID=@CreateUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductName", SqlDbType.NVarChar), new SqlParameter("@CreateUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductName;
            cmdParms[1].Value = Uid;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExsitUrl(string ProductUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Products");
            builder.Append(" where ProductUrl=@ProductUrl  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductUrl", SqlDbType.NVarChar) };
            cmdParms[0].Value = ProductUrl;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl ");
            builder.Append(" FROM SNS_Products ");
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
            builder.Append(" ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl ");
            builder.Append(" FROM SNS_Products ");
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
                builder.Append("order by T.ProductID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Products T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.AppendFormat("order by T.{0} DESC", orderby);
            }
            else
            {
                builder.Append("order by T.ProductID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Products T  ");
            if ((CateId > 0) || (strWhere.Length > 1))
            {
                builder.Append(" where ");
            }
            if (CateId > 0)
            {
                builder.Append(string.Concat(new object[] { "  CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=", CateId, " or Path like  (select path from SNS_Categories where CategoryID=", CateId, ")+'|%'))" }));
            }
            if (strWhere.Length > 1)
            {
                if (CateId > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere, int CateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate ");
            builder.Append(" FROM SNS_Products where");
            if (CateId > 0)
            {
                builder.Append(string.Concat(new object[] { "   CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=", CateId, " or Path like '", CateId, "|%'))" }));
            }
            if (strWhere.Trim() != "")
            {
                if (CateId > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(strWhere);
            }
            builder.Append(" ORDER BY CreatedDate DESC");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListToStatic(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select ProductId from SNS_Products  ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append("WHERE  " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Products GetModel(long ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ProductID,ProductName,ProductDescription,Price,ProductSourceID,CategoryID,ProductUrl,FavouriteCount,GroupBuyCount,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,PVCount,IsRecomend,Status,Sequence,TopCommentsId,CommentCount,ForwardedCount,ShareDescription,SkipCount,OwnerProductId,Tags,CreatedDate,Color,OriginalID,SourceType,StaticUrl from SNS_Products ");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.BigInt) };
            cmdParms[0].Value = ProductID;
            new Maticsoft.Model.SNS.Products();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetProductByPage(string strWhere, string Order, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(Order.Trim()))
            {
                builder.Append(Order);
            }
            else
            {
                builder.Append("order by T.ProductID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Products T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetProductUrl(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ProductUrl from SNS_Products ");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.BigInt) };
            cmdParms[0].Value = productId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return "";
            }
            return single.ToString();
        }

        public DataSet GetProductUserIds(string ids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select CreateUserID from SNS_Products  where ProductID IN (" + ids + ") ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_Products ");
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

        public int GetRecordCountEx(string strWhere, int CateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_Products ");
            if ((CateId > 0) || (strWhere.Length > 1))
            {
                builder.Append(" where ");
            }
            if (CateId > 0)
            {
                builder.Append(string.Concat(new object[] { "  CategoryID in ( select CategoryID from SNS_Categories where Type=0 and (CategoryID=", CateId, " or Path like  (select path from SNS_Categories where CategoryID=", CateId, ")+'|%'))" }));
            }
            if (strWhere.Trim() != "")
            {
                if (CateId > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.SNS.Products model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("ProductName=@ProductName,");
            builder.Append("ProductDescription=@ProductDescription,");
            builder.Append("Price=@Price,");
            builder.Append("ProductSourceID=@ProductSourceID,");
            builder.Append("CategoryID=@CategoryID,");
            builder.Append("ProductUrl=@ProductUrl,");
            builder.Append("FavouriteCount=@FavouriteCount,");
            builder.Append("GroupBuyCount=@GroupBuyCount,");
            builder.Append("CreateUserID=@CreateUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("ThumbImageUrl=@ThumbImageUrl,");
            builder.Append("NormalImageUrl=@NormalImageUrl,");
            builder.Append("PVCount=@PVCount,");
            builder.Append("IsRecomend=@IsRecomend,");
            builder.Append("Status=@Status,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("TopCommentsId=@TopCommentsId,");
            builder.Append("CommentCount=@CommentCount,");
            builder.Append("ForwardedCount=@ForwardedCount,");
            builder.Append("ShareDescription=@ShareDescription,");
            builder.Append("SkipCount=@SkipCount,");
            builder.Append("OwnerProductId=@OwnerProductId,");
            builder.Append("Tags=@Tags,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Color=@Color,");
            builder.Append("OriginalID=@OriginalID,");
            builder.Append("SourceType=@SourceType,");
            builder.Append("StaticUrl=@StaticUrl");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@ProductDescription", SqlDbType.NVarChar, 500), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductSourceID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@GroupBuyCount", SqlDbType.Int, 4), new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), 
                new SqlParameter("@TopCommentsId", SqlDbType.NVarChar, 100), new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@ForwardedCount", SqlDbType.Int, 4), new SqlParameter("@ShareDescription", SqlDbType.NVarChar, 200), new SqlParameter("@SkipCount", SqlDbType.Int, 4), new SqlParameter("@OwnerProductId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 400), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Color", SqlDbType.NVarChar, 100), new SqlParameter("@OriginalID", SqlDbType.BigInt, 8), new SqlParameter("@SourceType", SqlDbType.Int, 4), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 300), new SqlParameter("@ProductID", SqlDbType.BigInt, 8)
             };
            cmdParms[0].Value = model.ProductName;
            cmdParms[1].Value = model.ProductDescription;
            cmdParms[2].Value = model.Price;
            cmdParms[3].Value = model.ProductSourceID;
            cmdParms[4].Value = model.CategoryID;
            cmdParms[5].Value = model.ProductUrl;
            cmdParms[6].Value = model.FavouriteCount;
            cmdParms[7].Value = model.GroupBuyCount;
            cmdParms[8].Value = model.CreateUserID;
            cmdParms[9].Value = model.CreatedNickName;
            cmdParms[10].Value = model.ThumbImageUrl;
            cmdParms[11].Value = model.NormalImageUrl;
            cmdParms[12].Value = model.PVCount;
            cmdParms[13].Value = model.IsRecomend;
            cmdParms[14].Value = model.Status;
            cmdParms[15].Value = model.Sequence;
            cmdParms[0x10].Value = model.TopCommentsId;
            cmdParms[0x11].Value = model.CommentCount;
            cmdParms[0x12].Value = model.ForwardedCount;
            cmdParms[0x13].Value = model.ShareDescription;
            cmdParms[20].Value = model.SkipCount;
            cmdParms[0x15].Value = model.OwnerProductId;
            cmdParms[0x16].Value = model.Tags;
            cmdParms[0x17].Value = model.CreatedDate;
            cmdParms[0x18].Value = model.Color;
            cmdParms[0x19].Value = model.OriginalID;
            cmdParms[0x1a].Value = model.SourceType;
            cmdParms[0x1b].Value = model.StaticUrl;
            cmdParms[0x1c].Value = model.ProductID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCateList(string ProductIds, int CateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("CategoryID=@CategoryID");
            builder.Append(" where ProductID in (" + ProductIds + ")");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CateId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateClickCount(int ProuductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("SkipCount=SkipCount+1 ");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProuductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateEX(int ProductId, int CateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("CategoryID=@CategoryID");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CateId;
            cmdParms[1].Value = ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdatePvCount(int ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("PVCount=PVCount+1");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecomend(int ProductId, int Recomend)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("IsRecomend=@Recomend");
            builder.Append(" where ProductID =@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Recomend", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.Int, 4) };
            cmdParms[0].Value = Recomend;
            cmdParms[1].Value = ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecomendList(string ProductIds, int Recomend)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("IsRecomend=@Recomend");
            builder.Append(" where ProductID in (" + ProductIds + ")");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Recomend", SqlDbType.Int, 4) };
            cmdParms[0].Value = Recomend;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecommandState(int id, int State)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("IsRecomend=@IsRecomend,");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = State;
            cmdParms[1].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStaticUrl(int productId, string staticUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("StaticUrl=@StaticUrl");
            builder.Append(" where ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 300), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = staticUrl;
            cmdParms[1].Value = productId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(int ProductId, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("Status=@Status");
            builder.Append(" where ProductID =@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.Int, 4) };
            cmdParms[0].Value = Status;
            cmdParms[1].Value = ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet UserUploadProductsImage(int ablumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT P.* FROM SNS_Products P , ");
            builder.Append("( SELECT TargetID ");
            builder.Append("FROM SNS_UserAlbumDetail ");
            builder.Append("WHERE AlbumID = @AlbumID ");
            builder.Append("AND Type = 1 ) UAD , ");
            builder.Append("( SELECT CreatedUserID ");
            builder.Append("FROM SNS_UserAlbums ");
            builder.Append("WHERE AlbumID = @AlbumID) U ");
            builder.Append("WHERE P.ProductID = UAD.TargetID ");
            builder.Append("AND P.CreateUserID = U.CreatedUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ablumId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }
    }
}

