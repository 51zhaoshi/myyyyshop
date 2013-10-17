namespace Maticsoft.Web.Admin.Shop.ProductType
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Step3_1 : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chbDefinePic;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected RadioButton rbPic;
        protected RadioButton rbText;
        protected TextBox txtAttributeName;
        protected HtmlGenericControl txtAttributeNameTip;
        protected TextBox txtAttributeValue;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtAttributeName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "扩展属性名称不能为空，长度限制在1-15个字符之间！");
            }
            else
            {
                string text = this.txtAttributeName.Text;
                this.txtAttributeValue.Text.Trim();
                Maticsoft.Model.Shop.Products.AttributeInfo model = new Maticsoft.Model.Shop.Products.AttributeInfo {
                    UsageMode = 3
                };
                if (this.rbPic.Checked)
                {
                    model.UseAttributeImage = true;
                }
                else
                {
                    model.UseAttributeImage = false;
                }
                List<string> list = new List<string>();
                model.ValueStr = list;
                model.AttributeName = text;
                model.TypeId = this.ProductTypeId;
                model.UserDefinedPic = this.chbDefinePic.Checked;
                if (this.bll.AttributeManage(model, DataProviderAction.Create))
                {
                    if (this.Action == 0)
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='Step3.aspx?tid=" + this.ProductTypeId + "'");
                    }
                    else
                    {
                        MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='Modify3.aspx?tid=", this.ProductTypeId, "&ed=", this.Action, "'" }));
                    }
                }
                else
                {
                    this.btnSave.Enabled = false;
                    MessageBox.ShowFailTip(this, "保存失败！", "Step2.aspx");
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
                else if (this.bll.IsExistDefinedAttribute(this.ProductTypeId, null))
                {
                    this.chbDefinePic.Enabled = false;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1fb;
            }
        }

        private int Action
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["ed"]))
                {
                    num = Globals.SafeInt(base.Request.Params["ed"], 0);
                }
                return num;
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

