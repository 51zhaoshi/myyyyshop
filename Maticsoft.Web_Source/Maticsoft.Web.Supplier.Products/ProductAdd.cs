namespace Maticsoft.Web.Supplier.Products
{
    using LitJson;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Shop.Package;
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ProductAdd : PageBaseSupplier
    {
        protected AjaxRegion ajaxRegion;
        protected Button btnSave;
        protected CheckBox chbHot;
        protected CheckBox chbLowPrice;
        protected CheckBox chbNew;
        protected CheckBox chbRec;
        protected CheckBox chkQQ;
        protected CheckBox chkSina;
        protected DropDownList ddlSelectPackageCategory;
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
        protected HiddenField hfIsOpenSku;
        protected HiddenField hfProductImages;
        protected HiddenField hfProductImagesThumbSize;
        protected HiddenField Hidden_SelectPackage;
        protected HiddenField Hidden_SelectValue;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal32;
        protected Literal Literal4;
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected RadioButtonList rblUpselling;
        private ArrayList salePriceList = new ArrayList();
        protected HiddenField selectPackageText;
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

        public void BindPackageInfo()
        {
            DataSet list = new PackageCategory().GetList("");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.ddlSelectPackageCategory.DataSource = list;
                this.ddlSelectPackageCategory.DataTextField = "Name";
                this.ddlSelectPackageCategory.DataValueField = "CategoryId";
                this.ddlSelectPackageCategory.DataBind();
            }
            this.ddlSelectPackageCategory.Items.Insert(0, new ListItem("请选择类别", "0"));
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
                Maticsoft.Model.Shop.Products.AttributeInfo item = this.GetAttributeInfo4Obj(data);
                if (item != null)
                {
                    list.Add(item);
                }
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
                        string format = data["ImageUrl"].ToString().Replace(this.tempFile, "");
                        if (!this.skuImageList.Contains(string.Format(format, "T32X32_")))
                        {
                            this.skuImageList.Add(string.Format(format, "T32X32_"));
                            this.skuImageList.Add(string.Format(format, "T130X130_"));
                            this.skuImageList.Add(string.Format(format, "T300X390_"));
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
                Size size = StringPlus.SplitToSize(ConfigSystem.GetValueByCache("ProductNormalImageSize"), '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                this.hfProductImagesThumbSize.Value = size.Width + "," + size.Height;
                this.hfIsOpenSku.Value = ConfigSystem.GetValueByCache("Shop_AddProduct_OpenSku");
                this.txtDisplaySequence.Text = (this.productBll.MaxSequence() + 1).ToString();
                this.BindPackageInfo();
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
                int supplierId = base.SupplierId;
                int? nullable = new int?(Globals.SafeInt(this.ajaxRegion.SelectedValue, -1));
                string str2 = this.txtUnit.Text;
                decimal? nullable2 = new decimal?(Globals.SafeDecimal(this.txtMarketPrice.Text, (decimal) -1M));
                int num4 = Globals.SafeInt(this.txtDisplaySequence.Text, -1);
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
                        Maticsoft.Model.Shop.Products.ProductInfo productInfo = new Maticsoft.Model.Shop.Products.ProductInfo();
                        string[] strArray2 = this.Hidden_SelectValue.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        productInfo.Product_Categories = strArray2;
                        productInfo.ProductName = text;
                        productInfo.CategoryId = 0;
                        productInfo.TypeId = new int?(num);
                        productInfo.BrandId = num2;
                        if (supplierId > 0)
                        {
                            productInfo.SupplierId = supplierId;
                        }
                        else
                        {
                            productInfo.SupplierId = -1;
                        }
                        productInfo.RegionId = (nullable == -1) ? null : nullable;
                        productInfo.Unit = str2;
                        productInfo.MarketPrice = (nullable2 == -1M) ? 0 : nullable2;
                        productInfo.LowestSalePrice = nullable3.Value;
                        productInfo.DisplaySequence = num4;
                        productInfo.ProductCode = this.txtProductSKU.Text;
                        productInfo.AddedDate = DateTime.Now;
                        productInfo.SaleStatus = -1;
                        productInfo.ShortDescription = str3;
                        productInfo.Description = str4;
                        productInfo.Tags = "";
                        if (strArray.Length == 0)
                        {
                            productInfo.ImageUrl = productInfo.ThumbnailUrl1 = productInfo.ThumbnailUrl2 = "/Content/themes/base/Shop/images/none.png";
                        }
                        string newValue = string.Format("/Upload/Shop/Images/Product/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                        string str10 = "/Upload/Shop/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                        ArrayList list3 = new ArrayList();
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (i == 0)
                            {
                                string str11 = string.Format(strArray[i], "");
                                string str12 = strArray[i];
                                productInfo.ImageUrl = str11.Replace(this.tempFile, newValue);
                                productInfo.ThumbnailUrl1 = str12.Replace(this.tempFile, str10);
                                list3.Add(str11.Replace(this.tempFile, ""));
                                list3.Add(str12.Replace(this.tempFile, ""));
                            }
                            else
                            {
                                string str13 = string.Format(strArray[i], "");
                                string str14 = strArray[i];
                                Maticsoft.Model.Shop.Products.ProductImage item = new Maticsoft.Model.Shop.Products.ProductImage {
                                    ImageUrl = str13.Replace(this.tempFile, newValue),
                                    ThumbnailUrl1 = str14.Replace(this.tempFile, str10)
                                };
                                productInfo.ProductImages.Add(item);
                                list3.Add(str13.Replace(this.tempFile, ""));
                                list3.Add(str14.Replace(this.tempFile, ""));
                            }
                            Maticsoft.BLL.Shop.Products.ProductImage.MoveImage(strArray[i], newValue, str10);
                        }
                        productInfo.AttributeInfos = list;
                        productInfo.HasSKU = flag;
                        productInfo.SkuInfos = list2;
                        productInfo.isRec = this.chbRec.Checked;
                        productInfo.isNow = this.chbNew.Checked;
                        productInfo.isHot = this.chbHot.Checked;
                        productInfo.isLowPrice = this.chbLowPrice.Checked;
                        if (!string.IsNullOrEmpty(this.Hidden_SelectPackage.Value))
                        {
                            Maticsoft.BLL.Shop.Package.Package package = new Maticsoft.BLL.Shop.Package.Package();
                            List<int> list4 = new List<int>();
                            string[] strArray3 = this.Hidden_SelectPackage.Value.Split(new char[] { ',' });
                            int packageId = 0;
                            foreach (string str15 in strArray3)
                            {
                                packageId = Globals.SafeInt(str15, 0);
                                if (package.Exists(packageId))
                                {
                                    list4.Add(packageId);
                                }
                            }
                            productInfo.PackageId = list4;
                        }
                        long productId = 0L;
                        if (ProductManage.AddProduct(productInfo, out productId))
                        {
                            if (this.skuImageList.Count > 0)
                            {
                                FileManage.MoveFile(base.Server.MapPath(this.tempFile), base.Server.MapPath(this.skuImageFile), this.skuImageList);
                            }
                            string str16 = "";
                            str16 = this.chkSina.Checked ? "3" : "";
                            if (this.chkQQ.Checked)
                            {
                                str16 = str16 + (string.IsNullOrWhiteSpace(str16) ? "13" : ",13");
                            }
                            UserBind bind = new UserBind();
                            string url = string.Concat(new object[] { "http://", Globals.DomainFullName, "/Product/Detail/", productId });
                            bind.SendWeiBo(-1, str16, productInfo.ProductName, url, productInfo.ImageUrl);
                            base.Response.Redirect(string.Format("InStock.aspx?SaleStatus=-1", new object[0]));
                        }
                        else
                        {
                            MessageBox.Show(this, "保存失败! 请重试.");
                        }
                    }
                }
            }
        }
    }
}

