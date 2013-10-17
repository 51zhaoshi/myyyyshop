namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Web.UI.WebControls;

    public class BrandsCheckBoxList : CheckBoxList
    {
        private int repeatColumns = 7;
        private System.Web.UI.WebControls.RepeatDirection repeatDirection;

        public override void DataBind()
        {
            this.Items.Clear();
            Maticsoft.BLL.Shop.Products.BrandInfo info = new Maticsoft.BLL.Shop.Products.BrandInfo();
            foreach (Maticsoft.Model.Shop.Products.BrandInfo info2 in info.GetBrands())
            {
                base.Items.Add(new ListItem(info2.BrandName, info2.BrandId.ToString()));
            }
        }

        public override int RepeatColumns
        {
            get
            {
                return this.repeatColumns;
            }
            set
            {
                this.repeatColumns = value;
            }
        }

        public override System.Web.UI.WebControls.RepeatDirection RepeatDirection
        {
            get
            {
                return this.repeatDirection;
            }
            set
            {
                this.repeatDirection = value;
            }
        }
    }
}

