namespace Maticsoft.Web.Admin.Shop.ProductType
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using System;
    using System.Web.UI.WebControls;

    public class AddValue : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected TextBox txtAttributeValue;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;
        private Maticsoft.BLL.Shop.Products.AttributeValue ValueBll = new Maticsoft.BLL.Shop.Products.AttributeValue();

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "扩展属性的值，多个属性值可用“，”号隔开，每个值最多15个字符！");
            }
            else if (((this.ValueId > 0L) && string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim())) && this.txtAttributeValue.Text.Trim().Contains(","))
            {
                MessageBox.ShowFailTip(this, "属性值必须小于15个字符，不能为空，并且不能包含逗号！");
            }
            else
            {
                string str = this.txtAttributeValue.Text.Trim();
                Maticsoft.Model.Shop.Products.AttributeValue model = new Maticsoft.Model.Shop.Products.AttributeValue();
                int num = 0;
                if ((this.MidValue == 0) && (this.ValueId == 0L))
                {
                    string[] strArray = str.Replace("\r\n", "\n").Replace("\n", ",").Replace("，", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].Length <= 100)
                        {
                            string str2 = strArray[i];
                            if (!string.IsNullOrWhiteSpace(str2))
                            {
                                model.AttributeId = this.AttributeId;
                                model.ValueStr = str2;
                                num = this.ValueBll.AttributeValueManage(model, DataProviderAction.Create) ? (num + 1) : 0;
                            }
                        }
                    }
                }
                else if ((this.MidValue > 0) && (this.ValueId == 0L))
                {
                    model.AttributeId = this.AttributeId;
                    model.ValueStr = str;
                    num = this.ValueBll.AttributeValueManage(model, DataProviderAction.Create) ? (num + 1) : 0;
                }
                else
                {
                    model = this.ValueBll.GetModel(this.ValueId);
                    model.ValueStr = str;
                    if (this.ValueBll.Update(model))
                    {
                        num++;
                    }
                }
                if (((num > 0) && (this.AddOrModify > 0)) && (this.MidValue > 0))
                {
                    MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='listV.aspx?tid=", this.ProductTypeId, "&ed=", this.AttributeId, "&a=", this.AddOrModify, "'" }));
                }
                else if ((num > 0) && (this.AddOrModify > 0))
                {
                    MessageBox.ResponseScript(this, "parent.location.href='Step2.aspx?tid=" + this.ProductTypeId + "'");
                }
                else if ((num > 0) && (this.MidValue == 0))
                {
                    MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='Modify2.aspx?tid=", this.ProductTypeId, "&ed=", this.AttributeId, "'" }));
                }
                else if ((num > 0) && (this.MidValue > 0))
                {
                    MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='listV.aspx?tid=", this.ProductTypeId, "&ed=", this.AttributeId, "'" }));
                }
                else if ((num > 0) && (this.ValueId > 0L))
                {
                    MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='listV.aspx?tid=", this.ProductTypeId, "&ed=", this.AttributeId, "'" }));
                }
                else
                {
                    this.btnSave.Enabled = false;
                    MessageBox.ShowFailTip(this, "保存失败！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                }
                else if ((this.ProductTypeId == 0) || (this.AttributeId == 0L))
                {
                    MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                }
                else
                {
                    this.ShowTitleInfo();
                    if (this.ValueId > 0L)
                    {
                        this.ShowValueInfo();
                    }
                }
            }
        }

        private void ShowTitleInfo()
        {
            Maticsoft.Model.Shop.Products.AttributeInfo model = this.bll.GetModel(this.AttributeId);
            if (model != null)
            {
                this.Literal2.Text = model.AttributeName;
            }
        }

        private void ShowValueInfo()
        {
            Maticsoft.Model.Shop.Products.AttributeValue model = this.ValueBll.GetModel(this.ValueId);
            if (model != null)
            {
                this.txtAttributeValue.Text = model.ValueStr;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1f7;
            }
        }

        private int AddOrModify
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["a"]))
                {
                    num = Globals.SafeInt(base.Request.Params["a"], 0);
                }
                return num;
            }
        }

        private long AttributeId
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

        private int MidValue
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["m"]))
                {
                    num = Globals.SafeInt(base.Request.Params["m"], 0);
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

        private long ValueId
        {
            get
            {
                long num = 0L;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["v"]))
                {
                    num = Globals.SafeInt(base.Request.Params["v"], 0);
                }
                return num;
            }
        }
    }
}

