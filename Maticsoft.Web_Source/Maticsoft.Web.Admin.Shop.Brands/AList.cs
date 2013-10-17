namespace Maticsoft.Web.Admin.Shop.Brands
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class AList : PageBaseAdmin
    {
        protected int Act_AddData = 0x191;
        protected int Act_DelData = 0x193;
        protected int Act_UpdateData = 0x192;
        private Maticsoft.BLL.Shop.Products.BrandInfo bll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        protected HtmlInputHidden hidDelbtn;
        protected HtmlInputHidden hidModifybtn;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal3;
        public string strHiddenScript = "";
        public string strLiList = "";

        private void CreateTabs()
        {
            Maticsoft.BLL.Shop.Products.ProductType type = new Maticsoft.BLL.Shop.Products.ProductType();
            StringBuilder builder = new StringBuilder();
            int num = 1;
            foreach (Maticsoft.Model.Shop.Products.ProductType type2 in type.GetProductTypes())
            {
                builder.AppendFormat("<li class=\"normal\" onclick=\"nTabs(this,{0},{1});\"><a href=\"#\">{2}</a></li>", num, type2.TypeId, type2.TypeName);
                num++;
            }
            this.strLiList = builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.hidDelbtn.Value = "hidden";
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    this.hidModifybtn.Value = "hidden";
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                this.CreateTabs();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x18d;
            }
        }
    }
}

