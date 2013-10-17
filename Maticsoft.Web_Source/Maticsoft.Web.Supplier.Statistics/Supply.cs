namespace Maticsoft.Web.Supplier.Statistics
{
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class Supply : PageBaseSupplier
    {
        protected Button btnEdit;
        protected Label lblRemainPrice;
        protected Label lblRemainQuantity;
        protected Label lblSoldPrice;
        protected Label lblSoldQuantity;
        protected Label lblToalPrice;
        protected Label lblToalQuantity;
        protected Literal Literal2;
        protected Literal Literal3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            DataSet statisticsSupply = new SupplierInfo().GetStatisticsSupply(base.SupplierId);
            int? nullable = statisticsSupply.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? nullable2 = statisticsSupply.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!nullable.HasValue)
            {
                nullable = 0;
            }
            if (!nullable2.HasValue)
            {
                nullable2 = 0M;
            }
            this.lblRemainQuantity.Text = nullable.Value.ToString();
            this.lblRemainPrice.Text = nullable2.Value.ToString("C2");
            int? nullable3 = statisticsSupply.Tables[0].Rows[1].Field<int?>("ToalQuantity");
            decimal? nullable4 = statisticsSupply.Tables[0].Rows[1].Field<decimal?>("ToalPrice");
            if (!nullable3.HasValue)
            {
                nullable3 = 0;
            }
            if (!nullable4.HasValue)
            {
                nullable4 = 0M;
            }
            this.lblSoldQuantity.Text = nullable3.Value.ToString();
            this.lblSoldPrice.Text = nullable4.Value.ToString("C2");
            int? nullable8 = nullable + nullable3;
            this.lblToalQuantity.Text = nullable8.Value.ToString();
            decimal? nullable12 = nullable2 + nullable4;
            this.lblToalPrice.Text = nullable12.Value.ToString("C2");
        }
    }
}

