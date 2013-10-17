namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ProductService : IProductService
    {
        public bool AddProduct(Maticsoft.Model.Shop.Products.ProductInfo productInfo, out long ProductId)
        {
            ProductId = 0L;
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        productInfo.ProductId = Globals.SafeLong(DbHelperSQL.GetSingle4Trans(this.GenerateProductInfo(productInfo), transaction).ToString(), (long) (-1L));
                        ProductId = productInfo.ProductId;
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.SaveProductCategories(productInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateAttributeInfo(productInfo, transaction), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateSKUs(productInfo, transaction), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateAccessories(productInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateRelatedProduct(productInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateImages(productInfo), transaction);
                        if (productInfo.isRec)
                        {
                            DbHelperSQL.GetSingle4Trans(this.GenerateProductStationModes(productInfo, 0), transaction);
                        }
                        if (productInfo.isNow)
                        {
                            DbHelperSQL.GetSingle4Trans(this.GenerateProductStationModes(productInfo, 3), transaction);
                        }
                        if (productInfo.isHot)
                        {
                            DbHelperSQL.GetSingle4Trans(this.GenerateProductStationModes(productInfo, 1), transaction);
                        }
                        if (productInfo.isLowPrice)
                        {
                            DbHelperSQL.GetSingle4Trans(this.GenerateProductStationModes(productInfo, 2), transaction);
                        }
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GeneratePackage(productInfo), transaction);
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        private CommandInfo CheckSkuItems(Maticsoft.Model.Shop.Products.ProductInfo oldProductInfo, Maticsoft.Model.Shop.Products.ProductInfo newProductInfo)
        {
            DataTable table = new DataTable();
            List<Maticsoft.Model.Shop.Products.SKUItem> list = new List<Maticsoft.Model.Shop.Products.SKUItem>();
            foreach (DataRow row in table.Rows)
            {
                string imgURL = row["ImageUrl"].ToString();
                list.Exists(xx => xx.ImageUrl == imgURL);
            }
            return null;
        }

        private void DeleteOldProductInfo(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            parameters[0].Value = productInfo.ProductId;
            DbHelperSQL.RunProcedure("sp_Shop_DeleteBeforeUpdate", parameters);
        }

        private CommandInfo DelProductStationModes(Maticsoft.Model.Shop.Products.ProductInfo productInfo, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE Shop_ProductStationModes WHERE ProductId = @ProductId AND [Type] = @Type; ");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            para[0].Value = productInfo.ProductId;
            para[1].Value = type;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private List<CommandInfo> GenerateAccessories(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Maticsoft.Model.Shop.Products.ProductAccessorie accessorie in productInfo.ProductAccessories)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO Shop_AccessoriesValues (");
                builder.Append("ProductAccessoriesId ,ProductAccessoriesSKU)");
                builder.Append(" VALUES  ((SELECT ProductId FROM Shop_SKUs WHERE SkuId=@ProductAccessoriesSKU),@ProductAccessoriesSKU)");
                builder.Append(";SELECT @RESULT = @@IDENTITY");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductAccessoriesSKU", SqlDbType.NVarChar), DbHelperSQL.CreateOutParam("@RESULT", SqlDbType.BigInt, 8) };
                para[0].Value = accessorie.SkuId;
                list.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
                builder = new StringBuilder();
                builder.Append("INSERT INTO Shop_ProductAccessories(");
                builder.Append("ProductId ,AccessoriesValueId ,AccessoriesName ,MaxQuantity ,MinQuantity ,DiscountType ,DiscountAmount)");
                builder.Append(" VALUES (");
                builder.Append("@ProductId ,@AccessoriesValueId ,@AccessoriesName ,@MaxQuantity ,@MinQuantity ,@DiscountType ,@DiscountAmount)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), DbHelperSQL.CreateInputOutParam("@AccessoriesValueId", SqlDbType.BigInt, 8, null), new SqlParameter("@AccessoriesName", SqlDbType.NVarChar), new SqlParameter("@MaxQuantity", SqlDbType.Int), new SqlParameter("@MinQuantity", SqlDbType.Int), new SqlParameter("@DiscountType", SqlDbType.Int), new SqlParameter("@DiscountAmount", SqlDbType.Int) };
                parameterArray2[0].Value = productInfo.ProductId;
                parameterArray2[2].Value = accessorie.AccessoriesName;
                parameterArray2[3].Value = accessorie.MaxQuantity;
                parameterArray2[4].Value = accessorie.MinQuantity;
                parameterArray2[5].Value = accessorie.DiscountType;
                parameterArray2[6].Value = accessorie.DiscountAmount;
                list.Add(new CommandInfo(builder.ToString(), parameterArray2, EffentNextType.ExcuteEffectRows));
            }
            return list;
        }

        private CommandInfo GenerateAttribute4Input(Maticsoft.Model.Shop.Products.AttributeValue attributeValue, long productId, SqlTransaction transaction)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_AttributeValues(");
            builder.Append("AttributeId,DisplaySequence,ValueStr,ImageUrl)");
            builder.Append(" VALUES (");
            builder.Append("@AttributeId,@DisplaySequence,@ValueStr,@ImageUrl)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@ValueStr", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff) };
            para[0].Value = attributeValue.AttributeId;
            para[1].Value = -1;
            para[2].Value = attributeValue.ValueStr;
            para[3].Value = attributeValue.ImageUrl;
            attributeValue.ValueId = Globals.SafeInt(DbHelperSQL.GetSingle4Trans(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows), transaction).ToString(), -1);
            return this.GenerateAttribute4One(attributeValue, productId);
        }

        private CommandInfo GenerateAttribute4One(Maticsoft.Model.Shop.Products.AttributeValue attributeValue, long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductAttributes(");
            builder.Append("ProductId,AttributeId,ValueId)");
            builder.Append(" VALUES (");
            builder.Append("@ProductId,@AttributeId,@ValueId)");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.Int, 4) };
            para[0].Value = productId;
            para[1].Value = attributeValue.AttributeId;
            para[2].Value = attributeValue.ValueId;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private List<CommandInfo> GenerateAttributeInfo(Maticsoft.Model.Shop.Products.ProductInfo productInfo, SqlTransaction transaction)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Maticsoft.Model.Shop.Products.AttributeInfo info in productInfo.AttributeInfos)
            {
                switch (Globals.SafeEnum<ProductAttributeModel>(info.UsageMode.ToString(CultureInfo.InvariantCulture), ProductAttributeModel.None))
                {
                    case ProductAttributeModel.One:
                    {
                        list.Add(this.GenerateAttribute4One(info.AttributeValues[0], productInfo.ProductId));
                        continue;
                    }
                    case ProductAttributeModel.Any:
                    {
                        foreach (Maticsoft.Model.Shop.Products.AttributeValue value2 in info.AttributeValues)
                        {
                            list.Add(this.GenerateAttribute4One(value2, productInfo.ProductId));
                        }
                        continue;
                    }
                    case ProductAttributeModel.Input:
                    {
                        list.Add(this.GenerateAttribute4Input(info.AttributeValues[0], productInfo.ProductId, transaction));
                        continue;
                    }
                }
            }
            return list;
        }

        private List<CommandInfo> GenerateImages(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Maticsoft.Model.Shop.Products.ProductImage image in productInfo.ProductImages)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO Shop_ProductImages(");
                builder.Append("ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8)");
                builder.Append(" VALUES (");
                builder.Append("@ProductId,@ImageUrl,@ThumbnailUrl1,@ThumbnailUrl2,@ThumbnailUrl3,@ThumbnailUrl4,@ThumbnailUrl5,@ThumbnailUrl6,@ThumbnailUrl7,@ThumbnailUrl8)");
                builder.Append(";SELECT @@IDENTITY");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 0xff) };
                para[0].Value = productInfo.ProductId;
                para[1].Value = image.ImageUrl;
                para[2].Value = image.ThumbnailUrl1;
                para[3].Value = image.ThumbnailUrl2;
                para[4].Value = image.ThumbnailUrl3;
                para[5].Value = image.ThumbnailUrl4;
                para[6].Value = image.ThumbnailUrl5;
                para[7].Value = image.ThumbnailUrl6;
                para[8].Value = image.ThumbnailUrl7;
                para[9].Value = image.ThumbnailUrl8;
                list.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
            }
            return list;
        }

        private List<CommandInfo> GeneratePackage(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            if (productInfo.PackageId != null)
            {
                foreach (int num in productInfo.PackageId)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("insert into Shop_ProductPackage(");
                    builder.Append("ProductId,PackageId)");
                    builder.Append(" values (");
                    builder.Append("@ProductId,@PackageId)");
                    SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@PackageId", SqlDbType.Int, 4) };
                    para[0].Value = productInfo.ProductId;
                    para[1].Value = num;
                    list.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
                }
            }
            return list;
        }

        private CommandInfo GeneratePaoductCategoriesOne(int categoriesId, long productId, string path)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductCategories ( CategoryId, ProductId,CategoryPath ) ");
            builder.Append(" VALUES (");
            builder.Append("@CategoryId,@ProductId,@CategoryPath)");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@CategoryPath", SqlDbType.NVarChar) };
            para[0].Value = productId;
            para[1].Value = categoriesId;
            para[2].Value = path;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateProductInfo(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_Products(");
            builder.Append("CategoryId,TypeId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,");
            builder.Append("Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,");
            builder.Append("DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,");
            builder.Append("ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,");
            builder.Append("ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle)");
            builder.Append(" VALUES (");
            builder.Append("@CategoryId,@TypeId,@BrandId,@ProductName,@ProductCode,@SupplierId,@RegionId,");
            builder.Append("@ShortDescription,@Unit,@Description,@Title,@Meta_Description,@Meta_Keywords,");
            builder.Append("@SaleStatus,@AddedDate,@VistiCounts,@SaleCounts,@DisplaySequence,@LineId,@MarketPrice,");
            builder.Append("@LowestSalePrice,@PenetrationStatus,@MainCategoryPath,@ExtendCategoryPath,@HasSKU,");
            builder.Append("@Points,@ImageUrl,@ThumbnailUrl1,@ThumbnailUrl2,@ThumbnailUrl3,@ThumbnailUrl4,");
            builder.Append("@ThumbnailUrl5,@ThumbnailUrl6,@ThumbnailUrl7,@ThumbnailUrl8,@MaxQuantity,@MinQuantity,@Tags,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShortDescription", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Unit", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SaleStatus", SqlDbType.Int, 4), new SqlParameter("@AddedDate", SqlDbType.DateTime), new SqlParameter("@VistiCounts", SqlDbType.Int, 4), 
                new SqlParameter("@SaleCounts", SqlDbType.Int, 4), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@LowestSalePrice", SqlDbType.Money, 8), new SqlParameter("@PenetrationStatus", SqlDbType.SmallInt, 2), new SqlParameter("@MainCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@ExtendCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@HasSKU", SqlDbType.Bit, 1), new SqlParameter("@Points", SqlDbType.Decimal, 9), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 0xff), 
                new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 0xff), new SqlParameter("@MaxQuantity", SqlDbType.Int, 4), new SqlParameter("@MinQuantity", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 50), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300)
             };
            para[0].Value = productInfo.CategoryId;
            para[1].Value = productInfo.TypeId;
            para[2].Value = productInfo.BrandId;
            para[3].Value = productInfo.ProductName;
            para[4].Value = productInfo.ProductCode;
            para[5].Value = productInfo.SupplierId;
            para[6].Value = productInfo.RegionId;
            para[7].Value = productInfo.ShortDescription;
            para[8].Value = productInfo.Unit;
            para[9].Value = productInfo.Description;
            para[10].Value = productInfo.Meta_Title;
            para[11].Value = productInfo.Meta_Description;
            para[12].Value = productInfo.Meta_Keywords;
            para[13].Value = productInfo.SaleStatus;
            para[14].Value = productInfo.AddedDate;
            para[15].Value = productInfo.VistiCounts;
            para[0x10].Value = productInfo.SaleCounts;
            para[0x11].Value = productInfo.DisplaySequence;
            para[0x12].Value = productInfo.LineId;
            para[0x13].Value = productInfo.MarketPrice;
            para[20].Value = productInfo.LowestSalePrice;
            para[0x15].Value = productInfo.PenetrationStatus;
            para[0x16].Value = productInfo.MainCategoryPath;
            para[0x17].Value = productInfo.ExtendCategoryPath;
            para[0x18].Value = productInfo.HasSKU;
            para[0x19].Value = productInfo.Points;
            para[0x1a].Value = productInfo.ImageUrl;
            para[0x1b].Value = productInfo.ThumbnailUrl1;
            para[0x1c].Value = productInfo.ThumbnailUrl2;
            para[0x1d].Value = productInfo.ThumbnailUrl3;
            para[30].Value = productInfo.ThumbnailUrl4;
            para[0x1f].Value = productInfo.ThumbnailUrl5;
            para[0x20].Value = productInfo.ThumbnailUrl6;
            para[0x21].Value = productInfo.ThumbnailUrl7;
            para[0x22].Value = productInfo.ThumbnailUrl8;
            para[0x23].Value = productInfo.MaxQuantity;
            para[0x24].Value = productInfo.MinQuantity;
            para[0x25].Value = productInfo.Tags;
            para[0x26].Value = productInfo.SeoUrl;
            para[0x27].Value = productInfo.SeoImageAlt;
            para[40].Value = productInfo.SeoImageTitle;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateProductStationModes(Maticsoft.Model.Shop.Products.ProductInfo productInfo, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE Shop_ProductStationModes WHERE ProductId = @ProductId AND [Type] = @Type; ");
            builder.Append("INSERT INTO Shop_ProductStationModes(");
            builder.Append("ProductId,DisplaySequence,Type)");
            builder.Append(" VALUES (");
            builder.Append("@ProductId,@DisplaySequence,@Type)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.Int, 4), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            para[0].Value = productInfo.ProductId;
            para[1].Value = productInfo.ProductId;
            para[2].Value = type;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private List<CommandInfo> GenerateRelatedProduct(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            if ((productInfo.RelatedProductId != null) && (productInfo.RelatedProductId.Length != 0))
            {
                foreach (string str in productInfo.RelatedProductId)
                {
                    string[] strArray = str.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Globals.SafeInt(strArray[1], 0) == 0)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("INSERT INTO Shop_RelatedProducts(");
                        builder.Append(" RelatedId, ProductId )");
                        builder.Append("VALUES  (");
                        builder.Append("@RelatedId,@ProductId)");
                        SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@RelatedId", SqlDbType.BigInt, 8) };
                        para[0].Value = productInfo.ProductId;
                        para[1].Value = Globals.SafeLong(strArray[0], (long) (-1L));
                        list.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
                    }
                    else
                    {
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("INSERT INTO Shop_RelatedProducts(");
                        builder2.Append(" RelatedId, ProductId )");
                        builder2.Append("VALUES  (");
                        builder2.Append("@RelatedId,@ProductId)");
                        SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@RelatedId", SqlDbType.BigInt, 8) };
                        parameterArray2[0].Value = productInfo.ProductId;
                        parameterArray2[1].Value = Globals.SafeLong(strArray[0], (long) (-1L));
                        list.Add(new CommandInfo(builder2.ToString(), parameterArray2, EffentNextType.ExcuteEffectRows));
                        StringBuilder builder3 = new StringBuilder();
                        builder3.Append("INSERT INTO Shop_RelatedProducts(");
                        builder3.Append(" RelatedId, ProductId )");
                        builder3.Append("VALUES  (");
                        builder3.Append("@RelatedId,@ProductId)");
                        SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@RelatedId", SqlDbType.BigInt, 8) };
                        parameterArray3[0].Value = Globals.SafeLong(strArray[0], (long) (-1L));
                        parameterArray3[1].Value = productInfo.ProductId;
                        list.Add(new CommandInfo(builder3.ToString(), parameterArray3, EffentNextType.ExcuteEffectRows));
                    }
                }
            }
            return list;
        }

        private CommandInfo GenerateSKUItems(Maticsoft.Model.Shop.Products.SKUItem skuItem, Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_SKUItems(");
            builder.Append("AttributeId,ValueId,ImageUrl,ValueStr,ProductId)");
            builder.Append(" VALUES (");
            builder.Append("@AttributeId,@ValueId,@ImageUrl,@ValueStr,@ProductId)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.BigInt, 8), new SqlParameter("@ImageUrl", SqlDbType.NVarChar), new SqlParameter("@ValueStr", SqlDbType.NVarChar), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            para[0].Value = skuItem.AttributeId;
            para[1].Value = skuItem.ValueId;
            para[2].Value = skuItem.ImageUrl;
            para[3].Value = skuItem.ValueStr;
            para[4].Value = productInfo.ProductId;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private List<CommandInfo> GenerateSKUs(Maticsoft.Model.Shop.Products.ProductInfo productInfo, SqlTransaction transaction)
        {
            Dictionary<long, long> dictionary = new Dictionary<long, long>();
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Maticsoft.Model.Shop.Products.SKUInfo info in productInfo.SkuInfos)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO Shop_SKUs(");
                builder.Append("ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling)");
                builder.Append(" VALUES (");
                builder.Append("@ProductId,@SKU,@Weight,@Stock,@AlertStock,@CostPrice,@SalePrice,@Upselling)");
                builder.Append(";SELECT @RESULT = @@IDENTITY");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@SKU", SqlDbType.NVarChar, 50), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@Stock", SqlDbType.Int, 4), new SqlParameter("@AlertStock", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SalePrice", SqlDbType.Money, 8), new SqlParameter("@Upselling", SqlDbType.Bit, 1), DbHelperSQL.CreateOutParam("@RESULT", SqlDbType.BigInt, 8) };
                para[0].Value = productInfo.ProductId;
                para[1].Value = info.SKU;
                para[2].Value = info.Weight;
                para[3].Value = info.Stock;
                para[4].Value = info.AlertStock;
                para[5].Value = info.CostPrice;
                para[6].Value = info.SalePrice;
                para[7].Value = info.Upselling;
                list.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
                foreach (Maticsoft.Model.Shop.Products.SKUItem item in info.SkuItems)
                {
                    if (!dictionary.ContainsKey(item.ValueId))
                    {
                        long num = Globals.SafeLong(DbHelperSQL.GetSingle4Trans(this.GenerateSKUItems(item, productInfo), transaction).ToString(), (long) (-1L));
                        dictionary.Add(item.ValueId, num);
                    }
                    builder = new StringBuilder();
                    builder.Append("INSERT INTO Shop_SKURelation(");
                    builder.Append("SkuId,SpecId,ProductId)");
                    builder.Append(" VALUES (");
                    builder.Append("@SkuId,@SpecId,@ProductId)");
                    para = new SqlParameter[] { DbHelperSQL.CreateInputOutParam("@SkuId", SqlDbType.BigInt, 8, null), new SqlParameter("@SpecId", SqlDbType.BigInt, 8), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
                    para[1].Value = dictionary[item.ValueId];
                    para[2].Value = productInfo.ProductId;
                    list.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
                }
            }
            return list;
        }

        public DataSet GetCompareProudctBasicInfo(string ids)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ProductIDs", SqlDbType.NVarChar) };
            parameters[0].Value = ids;
            return DbHelperSQL.RunProcedure("sp_Shop_CompareProductBasicInfo", parameters, "ds");
        }

        public DataSet GetCompareProudctInfo(string ids)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ProductIDs", SqlDbType.NVarChar) };
            parameters[0].Value = ids;
            return DbHelperSQL.RunProcedure("sp_Shop_CompareProduct", parameters, "ds");
        }

        public bool ModifyProduct(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        this.DeleteOldProductInfo(productInfo);
                        DbHelperSQL.GetSingle4Trans(this.UpdateProductInfo(productInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.SaveProductCategories(productInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateAttributeInfo(productInfo, transaction), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateSKUs(productInfo, transaction), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateAccessories(productInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateRelatedProduct(productInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateImages(productInfo), transaction);
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        private List<CommandInfo> SaveProductCategories(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (string str in productInfo.Product_Categories)
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    string[] strArray = str.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    int categoriesId = Globals.SafeInt(strArray[0], 0);
                    list.Add(this.GeneratePaoductCategoriesOne(categoriesId, productInfo.ProductId, strArray[1]));
                }
            }
            return list;
        }

        private CommandInfo UpdateProductInfo(Maticsoft.Model.Shop.Products.ProductInfo productInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_Products SET ");
            builder.Append("CategoryId=@CategoryId,");
            builder.Append("TypeId=@TypeId,");
            builder.Append("BrandId=@BrandId,");
            builder.Append("ProductName=@ProductName,");
            builder.Append("ProductCode=@ProductCode,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("ShortDescription=@ShortDescription,");
            builder.Append("Unit=@Unit,");
            builder.Append("Description=@Description,");
            builder.Append("Meta_Title=@Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("SaleStatus=@SaleStatus,");
            builder.Append("VistiCounts=@VistiCounts,");
            builder.Append("SaleCounts=@SaleCounts,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("LineId=@LineId,");
            builder.Append("MarketPrice=@MarketPrice,");
            builder.Append("LowestSalePrice=@LowestSalePrice,");
            builder.Append("PenetrationStatus=@PenetrationStatus,");
            builder.Append("MainCategoryPath=@MainCategoryPath,");
            builder.Append("ExtendCategoryPath=@ExtendCategoryPath,");
            builder.Append("HasSKU=@HasSKU,");
            builder.Append("Points=@Points,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("ThumbnailUrl1=@ThumbnailUrl1,");
            builder.Append("ThumbnailUrl2=@ThumbnailUrl2,");
            builder.Append("ThumbnailUrl3=@ThumbnailUrl3,");
            builder.Append("ThumbnailUrl4=@ThumbnailUrl4,");
            builder.Append("ThumbnailUrl5=@ThumbnailUrl5,");
            builder.Append("ThumbnailUrl6=@ThumbnailUrl6,");
            builder.Append("ThumbnailUrl7=@ThumbnailUrl7,");
            builder.Append("ThumbnailUrl8=@ThumbnailUrl8,");
            builder.Append("MaxQuantity=@MaxQuantity,");
            builder.Append("MinQuantity=@MinQuantity,");
            builder.Append("Tags=@Tags,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("SeoImageAlt=@SeoImageAlt,");
            builder.Append("SeoImageTitle=@SeoImageTitle");
            builder.Append(" WHERE ProductId=@ProductId");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShortDescription", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Unit", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SaleStatus", SqlDbType.Int, 4), new SqlParameter("@VistiCounts", SqlDbType.Int, 4), new SqlParameter("@SaleCounts", SqlDbType.Int, 4), 
                new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@LowestSalePrice", SqlDbType.Money, 8), new SqlParameter("@PenetrationStatus", SqlDbType.SmallInt, 2), new SqlParameter("@MainCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@ExtendCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@HasSKU", SqlDbType.Bit, 1), new SqlParameter("@Points", SqlDbType.Decimal, 9), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 0xff), 
                new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 0xff), new SqlParameter("@MaxQuantity", SqlDbType.Int, 4), new SqlParameter("@MinQuantity", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 50), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@ProductId", SqlDbType.BigInt, 8)
             };
            para[0].Value = productInfo.CategoryId;
            para[1].Value = productInfo.TypeId;
            para[2].Value = productInfo.BrandId;
            para[3].Value = productInfo.ProductName;
            para[4].Value = productInfo.ProductCode;
            para[5].Value = productInfo.SupplierId;
            para[6].Value = productInfo.RegionId;
            para[7].Value = productInfo.ShortDescription;
            para[8].Value = productInfo.Unit;
            para[9].Value = productInfo.Description;
            para[10].Value = productInfo.Meta_Title;
            para[11].Value = productInfo.Meta_Description;
            para[12].Value = productInfo.Meta_Keywords;
            para[13].Value = productInfo.SaleStatus;
            para[14].Value = productInfo.VistiCounts;
            para[15].Value = productInfo.SaleCounts;
            para[0x10].Value = productInfo.DisplaySequence;
            para[0x11].Value = productInfo.LineId;
            para[0x12].Value = productInfo.MarketPrice;
            para[0x13].Value = productInfo.LowestSalePrice;
            para[20].Value = productInfo.PenetrationStatus;
            para[0x15].Value = productInfo.MainCategoryPath;
            para[0x16].Value = productInfo.ExtendCategoryPath;
            para[0x17].Value = productInfo.HasSKU;
            para[0x18].Value = productInfo.Points;
            para[0x19].Value = productInfo.ImageUrl;
            para[0x1a].Value = productInfo.ThumbnailUrl1;
            para[0x1b].Value = productInfo.ThumbnailUrl2;
            para[0x1c].Value = productInfo.ThumbnailUrl3;
            para[0x1d].Value = productInfo.ThumbnailUrl4;
            para[30].Value = productInfo.ThumbnailUrl5;
            para[0x1f].Value = productInfo.ThumbnailUrl6;
            para[0x20].Value = productInfo.ThumbnailUrl7;
            para[0x21].Value = productInfo.ThumbnailUrl8;
            para[0x22].Value = productInfo.MaxQuantity;
            para[0x23].Value = productInfo.MinQuantity;
            para[0x24].Value = productInfo.Tags;
            para[0x25].Value = productInfo.SeoUrl;
            para[0x26].Value = productInfo.SeoImageAlt;
            para[0x27].Value = productInfo.SeoImageTitle;
            para[40].Value = productInfo.ProductId;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }
    }
}

