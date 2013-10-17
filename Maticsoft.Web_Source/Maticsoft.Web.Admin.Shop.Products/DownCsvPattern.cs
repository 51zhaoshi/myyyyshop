namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public class DownCsvPattern : PageBaseAdmin
    {
        private ProductInfo bll = new ProductInfo();
        protected Button btnDownLoad;
        protected Button Button1;
        protected Literal Literal2;
        protected Literal Literal3;

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (!this.DataToCSV("Product"))
            {
                MessageBox.ShowFailTip(this, "");
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UpLoadCsvData.aspx");
        }

        public bool DataToCSV(string fileName)
        {
            try
            {
                string s = this.ExportCSV();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Expires = 0;
                HttpContext.Current.Response.BufferOutput = true;
                HttpContext.Current.Response.Charset = "GB2312";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                HttpContext.Current.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.csv", HttpUtility.UrlEncode(fileName, Encoding.UTF8)));
                HttpContext.Current.Response.ContentType = "text/h323;charset=gbk";
                HttpContext.Current.Response.Write(s);
                HttpContext.Current.Response.End();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string ExportCSV()
        {
            StringBuilder builder = new StringBuilder();
            DataTable table = this.bll.GetTableSchema().Tables[0];
            if (table == null)
            {
                return "";
            }
            foreach (DataRow row in table.Rows)
            {
                builder.Append(row["column_name"].ToString() + ",");
            }
            builder.Append("\n");
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 470;
            }
        }
    }
}

