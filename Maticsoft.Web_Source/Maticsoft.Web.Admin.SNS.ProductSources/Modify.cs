namespace Maticsoft.Web.Admin.SNS.ProductSources
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancel;
        protected Button btnSave;
        protected Label lblID;
        protected TextBox txtCategoryTags;
        protected TextBox txtImagesTag;
        protected TextBox txtPriceTags;
        protected TextBox txtStatus;
        protected TextBox txtWebSiteLogo;
        protected TextBox txtWebSiteName;
        protected TextBox txtWebSiteUrl;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtWebSiteName.Text.Trim().Length == 0)
            {
                msg = msg + @"商品来源网站的名称不能为空！\n";
            }
            if (this.txtWebSiteUrl.Text.Trim().Length == 0)
            {
                msg = msg + @"商品来源网站的url不能为空！\n";
            }
            if (this.txtWebSiteLogo.Text.Trim().Length == 0)
            {
                msg = msg + @"网站的log,在单品也链接到此不能为空！\n";
            }
            if (this.txtCategoryTags.Text.Trim().Length == 0)
            {
                msg = msg + @"采集时商品类别匹配的正则表达式不能为空！\n";
            }
            if (this.txtPriceTags.Text.Trim().Length == 0)
            {
                msg = msg + @"采集时商品价格匹配的正则表达式不能为空！\n";
            }
            if (this.txtImagesTag.Text.Trim().Length == 0)
            {
                msg = msg + @"采集时图片匹配的正则表达式不能为空！\n";
            }
            if (!PageValidate.IsNumber(this.txtStatus.Text))
            {
                msg = msg + @"状态格式错误！\n";
            }
            if (msg != "")
            {
                MessageBox.Show(this, msg);
            }
            else
            {
                int num = int.Parse(this.lblID.Text);
                string text = this.txtWebSiteName.Text;
                string str3 = this.txtWebSiteUrl.Text;
                string str4 = this.txtWebSiteLogo.Text;
                string str5 = this.txtCategoryTags.Text;
                string str6 = this.txtPriceTags.Text;
                string str7 = this.txtImagesTag.Text;
                int num2 = int.Parse(this.txtStatus.Text);
                Maticsoft.Model.SNS.ProductSources model = new Maticsoft.Model.SNS.ProductSources {
                    ID = num,
                    WebSiteName = text,
                    WebSiteUrl = str3,
                    WebSiteLogo = str4,
                    CategoryTags = str5,
                    PriceTags = str6,
                    ImagesTag = str7,
                    Status = num2
                };
                new Maticsoft.BLL.SNS.ProductSources().Update(model);
                MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                int iD = Convert.ToInt32(base.Request.Params["id"]);
                this.ShowInfo(iD);
            }
        }

        private void ShowInfo(int ID)
        {
            Maticsoft.Model.SNS.ProductSources model = new Maticsoft.BLL.SNS.ProductSources().GetModel(ID);
            this.lblID.Text = model.ID.ToString();
            this.txtWebSiteName.Text = model.WebSiteName;
            this.txtWebSiteUrl.Text = model.WebSiteUrl;
            this.txtWebSiteLogo.Text = model.WebSiteLogo;
            this.txtCategoryTags.Text = model.CategoryTags;
            this.txtPriceTags.Text = model.PriceTags;
            this.txtImagesTag.Text = model.ImagesTag;
            this.txtStatus.Text = model.Status.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x25e;
            }
        }
    }
}

