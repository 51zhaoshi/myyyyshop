namespace Maticsoft.Web.Supplier.Statistics
{
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;

    public class SalesLine : PageBaseSupplier
    {
        protected Button btnEdit;
        protected DropDownList drpYear;
        protected Literal litChart;
        protected Literal litDec;
        protected Literal litTitle;

        private string CreateXmlStr(DataSet ds, int type)
        {
            string text = this.litTitle.Text;
            string str2 = string.Empty;
            if (type == 2)
            {
                str2 = " yAxisName='金额（元）' numberPrefix ='￥' ";
            }
            if (DataSetTools.DataSetIsNull(ds))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                builder.AppendFormat("<?xml version='1.0' encoding='utf-8' ?><chart caption='{0}' xAxisName='月份' " + str2 + " showValues='1' showhovercap='0'  formatNumberScale='0' showBorder='0' palette='2' animation='1'  showPercentInToolTip='0'> ", text);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    builder.AppendFormat("<set label='{0}' ", ds.Tables[0].Rows[i].Field<int>("Mon"));
                    if (type == 2)
                    {
                        builder.AppendFormat(" value='{0}' />", ds.Tables[0].Rows[i].Field<decimal>("ToalPrice").ToString("F"));
                    }
                    else
                    {
                        builder.AppendFormat(" value='{0}' />", ds.Tables[0].Rows[i].Field<int>("ToalQuantity"));
                    }
                }
                builder.Append("</chart>");
            }
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str;
                if (this.SalesType == 2)
                {
                    str = "业绩";
                }
                else
                {
                    str = "销量";
                }
                this.litTitle.Text = string.Format(this.litTitle.Text, str);
                this.litDec.Text = string.Format(this.litDec.Text, str);
                DateTime time = Globals.SafeDateTime("2013-01-01", DateTime.Now);
                int num = DateTime.Now.Year - time.Year;
                for (int i = 0; i <= num; i++)
                {
                    ListItem item = new ListItem {
                        Text = time.AddYears(i).Year.ToString(),
                        Value = time.AddYears(i).Year.ToString()
                    };
                    this.drpYear.Items.Add(item);
                }
                this.drpYear.SelectedValue = DateTime.Now.Year.ToString();
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            DataSet statisticsSales = new SupplierInfo().GetStatisticsSales(base.SupplierId, Globals.SafeInt(this.drpYear.SelectedValue, DateTime.Now.Year));
            string strXML = this.CreateXmlStr(statisticsSales, this.SalesType);
            this.litChart.Text = FusionCharts.RenderChart("/FusionCharts/Line.swf", "", strXML, "FusionChartsLine", "900", "500", false, true);
        }

        public int SalesType
        {
            get
            {
                return Globals.SafeInt(base.Request.QueryString["SalesType"], 0);
            }
        }
    }
}

