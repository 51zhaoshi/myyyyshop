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

    public class Step1 : PageBaseAdmin
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
                    BrandsTypes = list
                };
                int typeid = 0;
                if (this.bll.ProductTypeManage(model, DataProviderAction.Create, out typeid))
                {
                    this.btnCancle.Enabled = false;
                    this.btnSave.Enabled = false;
                    base.Response.Redirect("Step2.aspx?tid=" + typeid);
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败！", "list.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.chkBrandsCheckBox.DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1ed;
            }
        }
    }
}

