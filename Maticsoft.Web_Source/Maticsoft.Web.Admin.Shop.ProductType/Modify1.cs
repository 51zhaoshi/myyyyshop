namespace Maticsoft.Web.Admin.Shop.ProductType
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Maticsoft.Web.Validator;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify1 : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductType bll = new Maticsoft.BLL.Shop.Products.ProductType();
        protected Button btnCancle;
        protected Button btnSave;
        protected BrandsCheckBoxList chkBrandsCheckBox;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;
        protected HtmlGenericControl txtTypeNameTip;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtTypeName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "商品类型名称不能为空，长度限制在1-50个字符之间！");
            }
            else
            {
                string text = this.txtTypeName.Text;
                string str2 = this.txtRemark.Text;
                IList<int> list = new List<int>();
                foreach (ListItem item in this.chkBrandsCheckBox.Items)
                {
                    if (item.Selected)
                    {
                        list.Add(int.Parse(item.Value));
                    }
                }
                Maticsoft.Model.Shop.Products.ProductType model = new Maticsoft.Model.Shop.Products.ProductType {
                    TypeName = text,
                    Remark = str2,
                    BrandsTypes = list,
                    TypeId = this.ProductTypeId
                };
                int typeid = 0;
                if (this.bll.ProductTypeManage(model, DataProviderAction.Update, out typeid))
                {
                    MessageBox.ShowSuccessTip(this, "基本设置信息保存成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败，正在返回商品类别列表页！", "list.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (this.ProductTypeId == 0)
                {
                    MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                }
                else
                {
                    this.chkBrandsCheckBox.DataBind();
                    this.ShowInfo();
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Products.ProductType model = this.bll.GetModel(this.ProductTypeId);
            if (model != null)
            {
                this.txtTypeName.Text = model.TypeName;
                this.txtRemark.Text = model.Remark;
                Maticsoft.Model.Shop.Products.BrandInfo relatedProduct = new Maticsoft.BLL.Shop.Products.BrandInfo().GetRelatedProduct(null, new int?(this.ProductTypeId));
                foreach (ListItem item in this.chkBrandsCheckBox.Items)
                {
                    if (relatedProduct.ProductTypeIdOrBrandsId.Contains(int.Parse(item.Value)))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1ee;
            }
        }

        private int ProductTypeId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["tid"]))
                {
                    num = Globals.SafeInt(base.Request.Params["tid"], 0);
                }
                return num;
            }
        }
    }
}

