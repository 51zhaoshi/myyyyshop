namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public class ProductsBatchUpload : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo bll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected Button btnDownLoad;
        protected Button btnUpload;
        protected string ExField = "";
        protected Literal Literal2;
        protected Literal Literal3;
        protected RegularExpressionValidator RegularExpressionValidator1;
        protected FileUpload uploadCsv;
        protected string UploadPath = "/Upload/Shop/Files/";

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (!this.DataToCSV("Product"))
            {
                MessageBox.ShowFailTip(this, "下载失败，请重试");
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string fileName = this.uploadCsv.PostedFile.FileName;
            string errorMsg = "出现异常，请检查您的数据格式";
            int count = 0;
            if (!this.uploadCsv.HasFile)
            {
                MessageBox.ShowSuccessTip(this, "请上传文件");
            }
            else
            {
                this.uploadCsv.PostedFile.SaveAs(base.Server.MapPath(this.UploadPath) + fileName);
                if (this.Csv(this.UploadPath, fileName, out errorMsg, ref count))
                {
                    MessageBox.ShowSuccessTip(this, "成功插入" + count + "条数据");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "插入失败,信息:" + errorMsg + "提示：检查您填写数据的数据格式");
                }
            }
        }

        public bool Csv(string Path, string FileName, out string ErrorMsg, ref int Count)
        {
            bool flag;
            OleDbConnection connection = new OleDbConnection();
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataSet dataSet = new DataSet();
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + base.Server.MapPath(Path) + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            connection.Open();
            command.Connection = connection;
            command.CommandText = "  select  *  from  [" + FileName + "]  ";
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(dataSet);
                foreach (Maticsoft.Model.Shop.Products.ProductInfo info in this.bll.DataTableToList(dataSet.Tables[0]))
                {
                    this.bll.Add(info);
                    Count++;
                }
                ErrorMsg = "";
                flag = true;
            }
            catch (Exception exception)
            {
                ErrorMsg = exception.Message;
                flag = false;
            }
            finally
            {
                connection.Close();
                command.Dispose();
                adapter.Dispose();
                connection.Dispose();
            }
            return flag;
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
                if ((this.ExField != null) && !this.ExField.Contains(row["column_name"].ToString()))
                {
                    builder.Append(row["column_name"].ToString() + ",");
                }
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

