namespace Maticsoft.Web.Admin.Ms.TaoData
{
    using LitJson;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.TaoBao.Domain;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Controls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class GetTaoList : PageBaseAdmin
    {
        protected AspNetPager AspNetPager1;
        protected Button btnAuthorize;
        protected Button btnCancel;
        protected Button btnGetData;
        protected Button btnMove;
        protected Button btnMove2;
        protected CheckBox chkRepeat;
        protected DataList DataListProduct;
        protected DropDownList ddlDiscount;
        protected DropDownList ddlShowcase;
        protected HiddenField hfTaoBaoAppkey;
        private Maticsoft.BLL.Shop.Products.ProductInfo infoBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected Literal Literal1;
        protected Literal Literal12;
        protected Literal Literal14;
        protected Literal Literal2;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected string savePath = string.Format("/Upload/Shop/Images/Product/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        private string skuImageFile = string.Format("/Upload/Shop/Images/ProductsSkuImages/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        protected TaoBaoCategoryDropList TaoBaoCate;
        protected TextBox TextBox1;
        protected TextBox TextBox2;
        protected TextBox TopKeyWord;
        protected TextBox TopPageNo;
        protected TextBox TopPageSize;
        protected Literal txtProduct;

        private bool AddProduct(Item item)
        {
            string path = "";
            string imgname = this.CreateIDCode() + ".jpg";
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                path = this.savePath + imgname;
                client.DownloadFile(item.PicUrl, base.Server.MapPath(path));
            }
            string json = string.Concat(new object[] { "{ 'SKU':'", item.NumIid, "', 'SalePrice': '", item.Price, "','CostPrice':'", item.Price, "','Stock':'", item.Num, "','AlertStock':'0','Weight':'0','Upselling':'1','SKUItems':[]}" });
            string thumbImagePath = "";
            this.MakeThumbnail(imgname, out thumbImagePath);
            Maticsoft.Model.Shop.Products.ProductInfo productInfo = new Maticsoft.Model.Shop.Products.ProductInfo {
                HasSKU = false,
                ImageUrl = path,
                ThumbnailUrl1 = thumbImagePath,
                LowestSalePrice = Globals.SafeDecimal(item.Price, (decimal) 0M),
                MarketPrice = new decimal?(Globals.SafeDecimal(item.Price, (decimal) 0M)),
                SaleStatus = 1,
                AddedDate = DateTime.Now,
                ProductName = item.Title,
                ProductCode = item.NumIid.ToString(),
                CategoryId = -1
            };
            List<Maticsoft.Model.Shop.Products.SKUInfo> list = this.GetSKUInfo4Json(JsonMapper.ToObject(json));
            productInfo.SkuInfos = list;
            productInfo.SupplierId = -1;
            productInfo.Product_Categories = new string[0];
            long productId = 0L;
            return ProductManage.AddProduct(productInfo, out productId);
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            if (this.Session["ProductList"] != null)
            {
                int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
                int pageSize = this.AspNetPager1.PageSize;
                List<Product> source = this.Session["ProductList"] as List<Product>;
                this.DataListProduct.DataSource = source.Skip<Product>(((currentPageIndex - 1) * pageSize)).Take<Product>(pageSize);
                this.DataListProduct.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Session["TaoBao_Session_Key"] = null;
            this.btnCancel.Visible = false;
            this.btnAuthorize.Visible = true;
            this.btnGetData.Visible = false;
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int num = Globals.SafeInt(this.TopPageSize.Text, 20);
            int num2 = Globals.SafeInt(this.TopPageNo.Text, 1);
            this.AspNetPager1.PageSize = num;
            string sessionKey = "";
            if ((this.Session["TaoBao_Session_Key"] == null) || string.IsNullOrWhiteSpace(this.Session["TaoBao_Session_Key"].ToString()))
            {
                MessageBox.ShowFailTip(this, "请先进行用户授权");
            }
            else
            {
                sessionKey = this.Session["TaoBao_Session_Key"].ToString();
                int cid = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
                string text = this.TopKeyWord.Text;
                string selectedValue = this.ddlDiscount.SelectedValue;
                string hasShowcase = this.ddlShowcase.SelectedValue;
                List<Item> source = this.infoBll.GetTaoListByUser(sessionKey, cid, text, num2, num, selectedValue, hasShowcase);
                if ((source != null) && (source.Count > 0))
                {
                    this.AspNetPager1.RecordCount = source.Count;
                    this.DataListProduct.DataSource = source.Take<Item>(num);
                    this.DataListProduct.DataBind();
                    this.Session["ProductList"] = source;
                }
                else
                {
                    MessageBox.ShowFailTip(this, "获取数据失败，请确保该授权用户有出售中的淘宝商品。");
                }
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            Func<Item, bool> func = null;
            Predicate<Item> match = null;
            string[] arryId;
            if (!Directory.Exists(base.Server.MapPath(this.savePath)))
            {
                Directory.CreateDirectory(base.Server.MapPath(this.savePath));
            }
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "选择您要导入的商品");
            }
            else
            {
                arryId = selIDlist.Split(new char[] { ',' });
                if (this.Session["ProductList"] != null)
                {
                    int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
                    int pageSize = this.AspNetPager1.PageSize;
                    List<Item> source = this.Session["ProductList"] as List<Item>;
                    int num3 = 0;
                    if (func == null)
                    {
                        func = c => arryId.Contains<string>(c.NumIid.ToString());
                    }
                    foreach (Item item in source.Where<Item>(func).ToList<Item>())
                    {
                        if (this.chkRepeat.Checked)
                        {
                            if (!this.infoBll.Exists(item.NumIid.ToString()) && this.AddProduct(item))
                            {
                                num3++;
                            }
                        }
                        else if (this.AddProduct(item))
                        {
                            num3++;
                        }
                    }
                    if (match == null)
                    {
                        match = c => arryId.Contains<string>(c.ProductId.ToString());
                    }
                    source.RemoveAll(match);
                    this.AspNetPager1.RecordCount = source.Count;
                    if (((currentPageIndex - 1) * pageSize) >= source.Count)
                    {
                        this.AspNetPager1.CurrentPageIndex = currentPageIndex - 1;
                        this.DataListProduct.DataSource = source.Skip<Item>(((currentPageIndex - 2) * pageSize)).Take<Item>(pageSize);
                    }
                    else
                    {
                        this.DataListProduct.DataSource = source.Skip<Item>(((currentPageIndex - 1) * pageSize)).Take<Item>(pageSize);
                    }
                    this.DataListProduct.DataBind();
                    this.Session["ProductList"] = source;
                    MessageBox.ShowSuccessTip(this, "成功导入【" + num3 + "】条数据");
                }
            }
        }

        protected void btnImportAll_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(base.Server.MapPath(this.savePath)))
            {
                Directory.CreateDirectory(base.Server.MapPath(this.savePath));
            }
            if (this.Session["ProductList"] != null)
            {
                List<Item> list = this.Session["ProductList"] as List<Item>;
                int num = 0;
                foreach (Item item in list)
                {
                    if (this.chkRepeat.Checked)
                    {
                        if (!this.infoBll.Exists(item.NumIid.ToString()) && this.AddProduct(item))
                        {
                            num++;
                        }
                    }
                    else if (this.AddProduct(item))
                    {
                        num++;
                    }
                }
                this.AspNetPager1.RecordCount = 0;
                this.DataListProduct.DataSource = null;
                this.DataListProduct.DataBind();
                this.Session["ProductList"] = null;
                MessageBox.ShowSuccessTip(this, "成功导入【" + num + "】条数据");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }

        public string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.DataListProduct.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.DataListProduct.Items[i].FindControl("ckProduct");
                HiddenField field = (HiddenField) this.DataListProduct.Items[i].FindControl("hfProduct");
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (field.Value != null)
                    {
                        str = str + field.Value + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
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
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
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
            list2.Add(info.SalePrice);
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
                        str2 = data["ImageUrl"].ToString().Replace(this.savePath, this.skuImageFile);
                        string format = data["ImageUrl"].ToString().Replace(this.savePath, "");
                        if (!list.Contains(string.Format(format, "T32X32_")))
                        {
                            list.Add(string.Format(format, "T32X32_"));
                            list.Add(string.Format(format, "T130X130_"));
                            list.Add(string.Format(format, "T300X390_"));
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

        protected MakeThumbnailMode GetThumMode(int ThumMode)
        {
            switch (ThumMode)
            {
                case 0:
                    return MakeThumbnailMode.Auto;

                case 1:
                    return MakeThumbnailMode.Cut;

                case 2:
                    return MakeThumbnailMode.H;

                case 3:
                    return MakeThumbnailMode.HW;

                case 4:
                    return MakeThumbnailMode.W;
            }
            return MakeThumbnailMode.Auto;
        }

        public void MakeThumbnail(string imgname, out string thumbImagePath)
        {
            string path = "/Upload/Shop/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            if (!Directory.Exists(base.Server.MapPath(path)))
            {
                Directory.CreateDirectory(base.Server.MapPath(path));
            }
            List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(EnumHelper.AreaType.Shop, "");
            if (ConfigSystem.GetBoolValueByCache("System_ThumbImage_AddWater"))
            {
                string str2 = this.savePath + "W_";
                FileHelper.MakeWater(base.Server.MapPath(this.savePath + imgname), base.Server.MapPath(str2 + imgname));
            }
            if ((thumSizeList != null) && (thumSizeList.Count > 0))
            {
                foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                {
                    ImageTools.MakeThumbnail(base.Server.MapPath(this.savePath + imgname), base.Server.MapPath(path + size.ThumName + imgname), size.ThumWidth, size.ThumHeight, this.GetThumMode(size.ThumMode), InterpolationMode.High, SmoothingMode.HighQuality);
                }
            }
            thumbImagePath = path + "{0}" + imgname;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.hfTaoBaoAppkey.Value = ConfigSystem.GetValueByCache("OpenAPI_Shop_TaoBaoAppkey");
                if ((this.Session["TaoBao_Session_Key"] == null) || string.IsNullOrWhiteSpace(this.Session["TaoBao_Session_Key"].ToString()))
                {
                    this.btnGetData.Visible = false;
                    this.btnAuthorize.Visible = true;
                    this.btnCancel.Visible = false;
                }
                else
                {
                    this.btnGetData.Visible = true;
                    this.btnAuthorize.Visible = false;
                    this.btnCancel.Visible = true;
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x254;
            }
        }
    }
}

