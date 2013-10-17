namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Web.UI.WebControls;

    public class ProductTypesCheckBoxList : CheckBoxList
    {
        private int repeatColumns = 7;
        private System.Web.UI.WebControls.RepeatDirection repeatDirection;

        public override void DataBind()
        {
            this.Items.Clear();
            Maticsoft.BLL.Shop.Products.ProductType type = new Maticsoft.BLL.Shop.Products.ProductType();
            foreach (Maticsoft.Model.Shop.Products.ProductType type2 in type.GetProductTypes())
            {
                base.Items.Add(new ListItem(type2.TypeName, type2.TypeId.ToString()));
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

