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
    using System.Web.UI.WebControls;

    public class UpLoadCsvData : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo bll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected Button btnUpload;
        protected Literal Literal2;
        protected Literal Literal3;
        protected FileUpload uploadCsv;

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string path = "/Upload/Shop/Files/";
            string fileName = this.uploadCsv.PostedFile.FileName;
            string errorMsg = "出现异常，请检查您的数据格式";
            this.uploadCsv.PostedFile.SaveAs(base.Server.MapPath(path) + fileName);
            if (this.Csv(path, fileName, out errorMsg))
            {
                MessageBox.ShowSuccessTip(this, "批量插入成功");
            }
            else
            {
                MessageBox.ShowSuccessTip(this, "插入失败,信息:" + errorMsg + "提示：检查您填写数据的数据格式");
            }
        }

        public bool Csv(string Path, string FileName, out string ErrorMsg)
        {
            bool flag;
            OleDbConnection connection = new OleDbConnection();
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataSet dataSet = new DataSet();
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + base.Server.MapPath(Path) + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * From " + FileName;
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(dataSet, "Csv");
                foreach (Maticsoft.Model.Shop.Products.ProductInfo info in this.bll.DataTableToList(dataSet.Tables[0]))
                {
                    this.bll.Add(info);
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

