namespace Maticsoft.Web.Admin.Shop.ProductType
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ModifySpec : PageBaseAdmin
    {
        protected HtmlTableRow attributeText;
        private Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected HiddenField hfFileUrl;
        protected Image imgInfo;
        protected HtmlTableRow imgMsg;
        protected HtmlTableRow imgShow;
        protected HtmlTableRow imgTitle;
        protected LinkButton linDelete;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected HtmlTableRow TextMsg;
        protected TextBox txtAttributeValue;
        protected TextBox txtImgTitle;
        protected HtmlTableRow UseAttributeImage;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;
        private Maticsoft.BLL.Shop.Products.AttributeValue ValueBll = new Maticsoft.BLL.Shop.Products.AttributeValue();

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsImg == 0)
            {
                if (string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim()))
                {
                    MessageBox.ShowFailTip(this, "请将规格值控制在1到15个字符之间！");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value.Trim()))
                {
                    MessageBox.ShowFailTip(this, "请选择要上传的图片，仅接受jpg、gif、png、格式的图片!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.txtImgTitle.Text.Trim()))
                {
                    MessageBox.ShowFailTip(this, "请填写图片描述信息！");
                    return;
                }
            }
            Maticsoft.Model.Shop.Products.AttributeValue model = this.ValueBll.GetModel(this.ValueId);
            if (this.IsImg == 1)
            {
                model.ValueStr = this.txtImgTitle.Text;
                model.ImageUrl = this.hfFileUrl.Value;
            }
            else
            {
                model.ValueStr = this.txtAttributeValue.Text;
            }
            if (this.ValueBll.Update(model))
            {
                MessageBox.ResponseScript(this, string.Concat(new object[] { "parent.location.href='ListSpec.aspx?tid=", this.ProductTypeId, "&ed=", this.AttributeId, "&img=", this.IsImg, "&a=", this.AddOrModify, "'" }));
            }
            else
            {
                this.btnSave.Enabled = false;
                MessageBox.ShowFailTip(this, "保存失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (((this.ProductTypeId == 0) || (this.AttributeId == 0L)) || (this.ValueId == 0L))
                {
                    MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                }
                else if (this.IsImg < 0)
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
            if (this.IsImg > 0)
            {
                this.attributeText.Visible = false;
                this.TextMsg.Visible = false;
            }
            else
            {
                this.imgInfo.Visible = false;
                this.linDelete.Visible = false;
                this.UseAttributeImage.Visible = false;
                this.imgTitle.Visible = false;
                this.imgMsg.Visible = false;
            }
        }

        private void ShowValueInfo()
        {
            Maticsoft.Model.Shop.Products.AttributeValue model = this.ValueBll.GetModel(this.ValueId);
            if (model != null)
            {
                if (this.IsImg > 0)
                {
                    this.hfFileUrl.Value = model.ImageUrl;
                    this.imgInfo.ImageUrl = model.ImageUrl;
                    this.txtImgTitle.Text = model.ValueStr;
                }
                else
                {
                    this.txtAttributeValue.Text = model.ValueStr;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x201;
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

        private int IsImg
        {
            get
            {
                int num = -1;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["img"]))
                {
                    num = Globals.SafeInt(base.Request.Params["img"], -1);
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

