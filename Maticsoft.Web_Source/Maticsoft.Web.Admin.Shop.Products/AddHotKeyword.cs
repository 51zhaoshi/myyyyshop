namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class AddHotKeyword : PageBaseAdmin
    {
        protected Button btnAdd;
        protected DropDownList dropCategories;
        private Maticsoft.BLL.Shop.Products.HotKeyword hotKeywordBll = new Maticsoft.BLL.Shop.Products.HotKeyword();
        protected Literal Literal2;
        protected Literal Literal3;
        private Maticsoft.Model.Shop.Products.HotKeyword model = new Maticsoft.Model.Shop.Products.HotKeyword();
        protected TextBox tbKeyWord;

        public void BindData()
        {
            if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
            {
                this.btnAdd.Text = "修改";
                this.model = this.hotKeywordBll.GetModel(Convert.ToInt32(base.Request.Params["id"].ToString()));
                this.tbKeyWord.Text = this.model.Keywords;
                this.BindDropList(true);
                if (this.model.CategoryId != -1)
                {
                    this.dropCategories.SelectedValue = this.model.CategoryId.ToString();
                }
                this.dropCategories.Items.Insert(0, string.Empty);
            }
            else
            {
                this.BindDropList(false);
            }
        }

        private void BindDropList(bool isnoSelected)
        {
            DataSet list = new Maticsoft.BLL.Shop.Products.CategoryInfo().GetList(" Depth = 1");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.dropCategories.DataSource = list;
                this.dropCategories.DataTextField = "Name";
                this.dropCategories.DataValueField = "CategoryId";
                this.dropCategories.DataBind();
            }
            if (!isnoSelected)
            {
                this.dropCategories.Items.Insert(0, string.Empty);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32((this.dropCategories.SelectedValue == string.Empty) ? "-1" : this.dropCategories.SelectedValue);
            this.model.Keywords = this.tbKeyWord.Text.Trim();
            this.model.CategoryId = new int?(num);
            if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
            {
                this.model.Id = Convert.ToInt32(base.Request.Params["id"].ToString());
                if (this.hotKeywordBll.Update(this.model))
                {
                    MessageBox.ResponseScript(this, "parent.location.href='HotKeyword.aspx'");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "修改失败！");
                }
            }
            else if (this.hotKeywordBll.Add(this.model) > 0)
            {
                MessageBox.ResponseScript(this, "parent.location.href='HotKeyword.aspx'");
            }
            else
            {
                MessageBox.ShowFailTip(this, "保存失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1db;
            }
        }
    }
}

