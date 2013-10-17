namespace Maticsoft.Web.Supplier.Products
{
    using LitJson;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Maticsoft.Web.Validator;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ProductModify : PageBaseSupplier
    {
        private int _categoryId;
        protected AjaxRegion ajaxRegion;
        protected Button btnSave;
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryManage = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        protected HiddenField hfCategoryId;
        protected HiddenField hfCurrentAttributes;
        protected HiddenField hfCurrentBaseProductSKUs;
        protected HiddenField hfCurrentProductBrand;
        protected HiddenField hfCurrentProductSKUs;
        protected HiddenField hfCurrentProductType;
        protected HiddenField hfImage0;
        protected HiddenField hfImage1;
        protected HiddenField hfImage2;
        protected HiddenField hfImage3;
        protected HiddenField hfImage4;
        protected HiddenField hfIsOpenFit;
        protected HiddenField hfIsOpenRelated;
        protected HiddenField hfIsOpenSku;
        protected HiddenField hfProductImages;
        protected HiddenField hfProductImagesThumbSize;
        protected HiddenField Hidden_SelectName;
        protected HiddenField Hidden_SelectValue;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal32;
        protected Literal Literal4;
        protected Literal LitPName;
        protected RadioButtonList rblUpselling;
        protected Repeater rptSelectCategory;
        private ArrayList salePriceList = new ArrayList();
        private string skuImageFile = string.Format("/Upload/Shop/Images/ProductsSkuImages/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        private ArrayList skuImageList = new ArrayList();
        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        protected TextBox txtAlertStock;
        protected TextBox txtCostPrice;
        protected TextBox txtDescription;
        protected TextBox txtDisplaySequence;
        protected HtmlGenericControl txtDisplaySequenceTip;
        protected TextBox txtMarketPrice;
        protected TextBox txtProductName;
        protected HtmlGenericControl txtProductNameTip;
        protected TextBox txtProductSKU;
        protected TextBox txtSalePrice;
        protected TextBox txtShortDescription;
        protected TextBox txtStock;
        protected TextBox txtUnit;
        protected TextBox txtWeight;
        protected ValidateTarget ValidateTarget1;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void BindProductBaseInfo()
        {
            Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.BLL.Shop.Products.ProductInfo().GetModel(this.ProductId);
            if (model != null)
            {
                Maticsoft.BLL.Shop.Products.ProductCategories categories = new Maticsoft.BLL.Shop.Products.ProductCategories();
                this.hfCategoryId.Value = model.CategoryId.ToString();
                DataSet listByProductId = categories.GetListByProductId(model.ProductId);
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                for (int i = 0; i < listByProductId.Tables[0].Rows.Count; i++)
                {
                    builder.Append(listByProductId.Tables[0].Rows[i]["CategoryId"] + "_" + listByProductId.Tables[0].Rows[i]["CategoryPath"]);
                    builder.Append(",");
                    Maticsoft.Model.Shop.Products.CategoryInfo info3 = this.categoryManage.GetModel(Globals.SafeInt(listByProductId.Tables[0].Rows[i]["CategoryId"].ToString(), 0));
                    if (info3 != null)
                    {
                        builder2.Append(this.categoryManage.GetFullNameByCache(info3.CategoryId));
                        builder2.Append("  ");
                    }
                }
                this.Hidden_SelectValue.Value = builder.ToString().TrimEnd(new char[] { ',' });
                this.Hidden_SelectName.Value = this.LitPName.Text = builder2.ToString();
                this.rptSelectCategory.DataSource = categories.GetListByProductId(model.ProductId);
                this.rptSelectCategory.DataBind();
                if (model.RegionId.HasValue)
                {
                    this.ajaxRegion.Area_iID = model.RegionId.Value;
                    this.ajaxRegion.SelectedValue = model.RegionId.Value.ToString();
                }
                this.txtSalePrice.Text = model.LowestSalePrice.ToString("F");
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            this.SaveProductInfo();
        }

        private List<Maticsoft.Model.Shop.Products.AttributeInfo> GetAttributeInfo4Json(JsonData jsonData)
        {
            List<Maticsoft.Model.Shop.Products.AttributeInfo> list = new List<Maticsoft.Model.Shop.Products.AttributeInfo>();
            if (!jsonData.IsArray || (jsonData.Count < 1))
            {
                return null;
            }
            foreach (JsonData data in (IEnumerable) jsonData)
            {
                list.Add(this.GetAttributeInfo4Obj(data));
            }
            return list;
        }

        private Maticsoft.Model.Shop.Products.AttributeInfo GetAttributeInfo4Obj(JsonData jsonData)
        {
            Maticsoft.Model.Shop.Products.AttributeInfo info = null;
            if (!jsonData.IsObject)
            {
                return null;
            }
            info = new Maticsoft.Model.Shop.Products.AttributeInfo {
                AttributeId = Globals.SafeInt(jsonData["AttributeId"].ToString(), -1)
            };
            string str = jsonData["AttributeMode"].ToString();
            ProductAttributeModel model = (ProductAttributeModel) Enum.Parse(typeof(ProductAttributeModel), str);
            info.UsageMode = (int) model;
            switch (model)
            {
                case ProductAttributeModel.One:
                {
                    Maticsoft.Model.Shop.Products.AttributeValue item = new Maticsoft.Model.Shop.Products.AttributeValue {
                        AttributeId = info.AttributeId,
                        ValueId = Globals.SafeInt(jsonData["ValueItem"].ToString(), -1)
                    };
                    info.AttributeValues.Add(item);
                    return info;
                }
                case ProductAttributeModel.Any:
                    foreach (JsonData data in (IEnumerable) jsonData["ValueItem"])
                    {
                        Maticsoft.Model.Shop.Products.AttributeValue value2 = new Maticsoft.Model.Shop.Products.AttributeValue {
                            AttributeId = info.AttributeId,
                            ValueId = Globals.SafeInt(data.ToString(), -1)
                        };
                        info.AttributeValues.Add(value2);
                    }
                    return info;

                case ProductAttributeModel.Input:
                {
                    Maticsoft.Model.Shop.Products.AttributeValue value4 = new Maticsoft.Model.Shop.Products.AttributeValue {
                        AttributeId = info.AttributeId,
                        ValueStr = jsonData["ValueItem"].ToString()
                    };
                    info.AttributeValues.Add(value4);
                    return info;
                }
            }
            return info;
        }

        private List<Maticsoft.Model.Shop.Products.SKUInfo> GetSKUInfo4Json(JsonData jsonData)
        {
            List<Maticsoft.Model.Shop.Products.SKUInfo> list = new List<Maticsoft.Model.Shop.Products.SKUInfo>();
            if (jsonData.IsArray && (jsonData.Count > 0))
            {
                foreach (JsonData data in (IEnumerable) jsonData)
                {
                    list.Add(this.GetSKUInfo4Obj(data));
                }
                return list;
            }
            if (jsonData.IsObject)
            {
                list.Add(this.GetSKUInfo4Obj(jsonData));
            }
            return list;
        }

        private Maticsoft.Model.Shop.Products.SKUInfo GetSKUInfo4Obj(JsonData jsonData)
        {
            Maticsoft.Model.Shop.Products.SKUInfo info = null;
            if (!jsonData.IsObject)
            {
                return null;
            }
            info = new Maticsoft.Model.Shop.Products.SKUInfo {
                SKU = jsonData["SKU"].ToString()
            };
            string str = jsonData["CostPrice"].ToString();
            if (!string.IsNullOrWhiteSpace(str))
            {
                info.CostPrice = new decimal?(Globals.SafeDecimal(str, (decimal) -1M));
            }
            info.SalePrice = Globals.SafeDecimal(jsonData["SalePrice"].ToString(), (decimal) -1M);
            this.salePriceList.Add(info.SalePrice);
            info.Stock = Globals.SafeInt(jsonData["Stock"].ToString(), -1);
            info.AlertStock = Globals.SafeInt(jsonData["AlertStock"].ToString(), -1);
            info.Weight = new int?(Globals.SafeInt(jsonData["Weight"].ToString(), -1));
            info.Upselling = Globals.SafeBool(jsonData["Upselling"].ToString(), false);
            if (jsonData["SKUItems"].IsArray && (jsonData["SKUItems"].Count > 0))
            {
                foreach (JsonData data in (IEnumerable) jsonData["SKUItems"])
                {
                    string str2 = string.Empty;
                    if (!string.IsNullOrWhiteSpace(data["ImageUrl"].ToString()))
                    {
                        str2 = data["ImageUrl"].ToString().Replace(this.tempFile, this.skuImageFile);
                        if (data["ImageUrl"].ToString().Contains(this.tempFile))
                        {
                            string format = data["ImageUrl"].ToString().Replace(this.tempFile, "");
                            if (!this.skuImageList.Contains(string.Format(format, "T32X32_")))
                            {
                                this.skuImageList.Add(string.Format(format, "T32X32_"));
                                this.skuImageList.Add(string.Format(format, "T130X130_"));
                                this.skuImageList.Add(string.Format(format, "T300X390_"));
                            }
                        }
                    }
                    Maticsoft.Model.Shop.Products.SKUItem item = new Maticsoft.Model.Shop.Products.SKUItem {
                        AttributeId = Globals.SafeLong(data["AttributeId"].ToString(), (long) (-1L)),
                        ValueId = Globals.SafeLong(data["ValueId"].ToString(), (long) (-1L)),
                        ImageUrl = str2,
                        ValueStr = data["ValueStr"].ToString()
                    };
                    info.SkuItems.Add(item);
                }
            }
            return info;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindProductBaseInfo();
                Size size = StringPlus.SplitToSize(ConfigSystem.GetValueByCache("ProductNormalImageSize"), '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                this.hfProductImagesThumbSize.Value = size.Width + "," + size.Height;
                this.hfIsOpenSku.Value = ConfigSystem.GetValueByCache("Shop_AddProduct_OpenSku");
                this.hfIsOpenFit.Value = ConfigSystem.GetValueByCache("Shop_AddProduct_OpenFit");
                this.hfIsOpenRelated.Value = ConfigSystem.GetValueByCache("Shop_AddProduct_OpenRelated");
            }
        }

        protected void rptSelectCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal literal = (Literal) e.Item.FindControl("litCateName_1");
                Literal literal2 = (Literal) e.Item.FindControl("litCateName_2");
                object obj2 = DataBinder.Eval(e.Item.DataItem, "CategoryId");
                if (obj2 != null)
                {
                    if (literal != null)
                    {
                        literal.Text = this.categoryManage.GetFullNameByCache(Globals.SafeInt(obj2.ToString(), 0));
                    }
                    if (literal2 != null)
                    {
                        literal2.Text = this.categoryManage.GetFullNameByCache(Globals.SafeInt(obj2.ToString(), 0));
                    }
                }
            }
        }

        private void SaveProductInfo()
        {
            if (string.IsNullOrWhiteSpace(this.Hidden_SelectValue.Value))
            {
                MessageBox.ShowFailTip(this, "请选择产品分类！");
            }
            else
            {
                string text = this.txtProductName.Text;
                int num = Globals.SafeInt(this.hfCurrentProductType.Value, -1);
                int num2 = Globals.SafeInt(this.hfCurrentProductBrand.Value, -1);
                int? nullable = new int?(Globals.SafeInt(this.ajaxRegion.SelectedValue, -1));
                string str2 = this.txtUnit.Text;
                decimal? nullable2 = new decimal?(Globals.SafeDecimal(this.txtMarketPrice.Text, (decimal) -1M));
                int num3 = Globals.SafeInt(this.txtDisplaySequence.Text, -1);
                string str3 = this.txtShortDescription.Text;
                string str4 = this.txtDescription.Text;
                string str5 = this.hfProductImages.Value;
                string str6 = this.hfCurrentAttributes.Value;
                string str7 = this.hfCurrentBaseProductSKUs.Value;
                string str8 = this.hfCurrentProductSKUs.Value;
                bool flag = false;
                string[] strArray = new string[0];
                if (!string.IsNullOrWhiteSpace(str3))
                {
                    str3 = Globals.HtmlEncodeForSpaceWrap(str3);
                }
                if (!string.IsNullOrWhiteSpace(str5))
                {
                    strArray = str5.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (string.IsNullOrWhiteSpace(str6))
                {
                    MessageBox.Show(this, "属性信息不存在,请检查已填数据是否正确!");
                }
                else
                {
                    List<Maticsoft.Model.Shop.Products.AttributeInfo> list = this.GetAttributeInfo4Json(JsonMapper.ToObject(str6));
                    if (string.IsNullOrWhiteSpace(str7))
                    {
                        MessageBox.Show(this, "基础SKU信息不存在,请检查已填数据是否正确!");
                    }
                    else
                    {
                        List<Maticsoft.Model.Shop.Products.SKUInfo> list2 = null;
                        decimal? nullable3 = 0M;
                        flag = !string.IsNullOrWhiteSpace(str8);
                        if (flag)
                        {
                            list2 = this.GetSKUInfo4Json(JsonMapper.ToObject(str8));
                            if (this.salePriceList.Count > 0)
                            {
                                this.salePriceList.Sort();
                                nullable3 = new decimal?(Convert.ToDecimal(this.salePriceList[0]));
                            }
                        }
                        else
                        {
                            list2 = this.GetSKUInfo4Json(JsonMapper.ToObject(str7));
                            nullable3 = new decimal?(Globals.SafeDecimal(this.txtSalePrice.Text, (decimal) -1M));
                        }
                        Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.BLL.Shop.Products.ProductInfo().GetModel(this.ProductId);
                        string[] strArray2 = this.Hidden_SelectValue.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        model.Product_Categories = strArray2;
                        model.ProductName = text;
                        model.CategoryId = 0;
                        model.TypeId = new int?(num);
                        model.BrandId = num2;
                        model.RegionId = (nullable == -1) ? null : nullable;
                        model.Unit = str2;
                        model.MarketPrice = (nullable2 == -1M) ? null : nullable2;
                        model.LowestSalePrice = nullable3.Value;
                        model.DisplaySequence = num3;
                        model.ProductCode = this.txtProductSKU.Text;
                        model.SaleStatus = -1;
                        model.ShortDescription = str3;
                        model.Description = str4;
                        model.Tags = "";
                        if (strArray.Length == 0)
                        {
                            model.ImageUrl = model.ThumbnailUrl1 = model.ThumbnailUrl2 = "/Content/themes/base/Shop/images/none.png";
                        }
                        string newValue = string.Format("/Upload/Shop/Images/Product/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                        string str10 = "/Upload/Shop/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                        ArrayList list3 = new ArrayList();
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (i == 0)
                            {
                                if (strArray[i].Contains(this.tempFile))
                                {
                                    string str11 = string.Format(strArray[i], "");
                                    string str12 = strArray[i];
                                    model.ImageUrl = str11.Replace(this.tempFile, newValue);
                                    model.ThumbnailUrl1 = str12.Replace(this.tempFile, str10);
                                    list3.Add(str11.Replace(this.tempFile, ""));
                                    list3.Add(str12.Replace(this.tempFile, ""));
                                    Maticsoft.BLL.Shop.Products.ProductImage.MoveImage(strArray[i], newValue, str10);
                                }
                            }
                            else
                            {
                                string str13 = string.Format(strArray[i], "");
                                string str14 = strArray[i];
                                Maticsoft.Model.Shop.Products.ProductImage item = new Maticsoft.Model.Shop.Products.ProductImage {
                                    ImageUrl = str13.Replace(this.tempFile, newValue),
                                    ThumbnailUrl1 = str14.Replace(this.tempFile, str10)
                                };
                                model.ProductImages.Add(item);
                                list3.Add(str13.Replace(this.tempFile, ""));
                                list3.Add(str14.Replace(this.tempFile, ""));
                                Maticsoft.BLL.Shop.Products.ProductImage.MoveImage(strArray[i], newValue, str10);
                            }
                        }
                        model.AttributeInfos = list;
                        model.HasSKU = flag;
                        model.SkuInfos = list2;
                        if (ProductManage.ModifyProduct(model))
                        {
                            try
                            {
                                int count = list3.Count;
                                if (this.skuImageList.Count > 0)
                                {
                                    FileManage.MoveFile(base.Server.MapPath(this.tempFile), base.Server.MapPath(this.skuImageFile), this.skuImageList);
                                }
                            }
                            catch (Exception)
                            {
                            }
                            base.Response.Redirect(string.Format("InStock.aspx?SaleStatus={0}", model.SaleStatus));
                        }
                        else
                        {
                            MessageBox.Show(this, "保存失败! 请重试.");
                        }
                    }
                }
            }
        }

        public int CategoryId
        {
            get
            {
                return this._categoryId;
            }
            set
            {
                this._categoryId = value;
            }
        }

        public long ProductId
        {
            get
            {
                long num = 0L;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["pid"]))
                {
                    num = Globals.SafeLong(base.Request.Params["pid"], (long) 0L);
                }
                return num;
            }
        }
    }
}

