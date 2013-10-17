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

    public class Step2_1 : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected RadioButton rbAny;
        protected RadioButton rbInput;
        protected RadioButton rbOne;
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
            else if (this.bll.IsExistName(this.ProductTypeId, this.txtAttributeName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "该类型下的扩展属性名称已存在，请换一个属性名称！");
            }
            else if (!this.rbInput.Checked && string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "扩展属性的值，多个属性值可用“，”号隔开，每个值最多15个字符！");
            }
            else
            {
                string text = this.txtAttributeName.Text;
                string str2 = this.txtAttributeValue.Text.Trim();
                Maticsoft.Model.Shop.Products.AttributeInfo model = new Maticsoft.Model.Shop.Products.AttributeInfo();
                if (this.rbOne.Checked)
                {
                    model.UsageMode = 0;
                }
                else if (this.rbAny.Checked)
                {
                    model.UsageMode = 1;
                }
                else
                {
                    model.UsageMode = 2;
                }
                List<string> list = new List<string>();
                if (!this.rbInput.Checked)
                {
                    string[] strArray = str2.Replace("\r\n", "\n").Replace("\n", ",").Replace("，", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].Length <= 100)
                        {
                            string str3 = strArray[i].Replace("+", "");
                            if (!string.IsNullOrWhiteSpace(str3))
                            {
                                list.Add(str3);
                            }
                        }
                    }
                }
                model.ValueStr = list;
                model.AttributeName = text;
                model.TypeId = this.ProductTypeId;
                if (this.bll.AttributeManage(model, DataProviderAction.Create))
                {
                    if (this.Action == 0L)
                    {
                        MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='Step2.aspx?tid=", this.ProductTypeId, "&ed=", this.Action, "'" }));
                    }
                    else
                    {
                        MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='Modify2.aspx?tid=", this.ProductTypeId, "&ed=", this.Action, "'" }));
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
            if (!base.IsPostBack && (this.ProductTypeId == 0))
            {
                MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1f2;
            }
        }

        private long Action
        {
            get
            {
                long num = 0L;
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

