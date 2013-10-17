namespace Maticsoft.Web.Admin.Shop.ProductType
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Label lblRemark;
        protected Label lblTypeId;
        protected Label lblTypeName;
        protected Literal Literal2;
        protected Literal Literal3;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int typeId = Convert.ToInt32(this.strid);
                this.ShowInfo(typeId);
            }
        }

        private void ShowInfo(int TypeId)
        {
            Maticsoft.Model.Shop.Products.ProductType model = new Maticsoft.BLL.Shop.Products.ProductType().GetModel(TypeId);
            this.lblTypeId.Text = model.TypeId.ToString();
            this.lblTypeName.Text = model.TypeName;
            this.lblRemark.Text = model.Remark;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x202;
            }
        }
    }
}

