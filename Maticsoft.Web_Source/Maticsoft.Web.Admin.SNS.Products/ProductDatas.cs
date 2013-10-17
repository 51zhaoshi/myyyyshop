namespace Maticsoft.Web.Admin.SNS.Products
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.TaoBao.Domain;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class ProductDatas : PageBaseAdmin
    {
        private UserAlbums albumBll = new UserAlbums();
        protected AspNetPager AspNetPager1;
        private Products bll = new Products();
        protected Button btnGetData;
        protected Button btnMove;
        protected Button btnMove2;
        protected Button btnUpload;
        protected CheckBox chkRepeat;
        protected DataList DataListProduct;
        protected DropDownList ddlAlbumList;
        protected DropDownList ddlAlbumList2;
        protected Literal Literal1;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal2;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected RegularExpressionValidator RegularExpressionValidator1;
        protected Maticsoft.Web.Controls.SNSCategoryDropList SNSCategoryDropList;
        protected TaoBaoCategoryDropList TaoBaoCate;
        protected TextBox TopArea;
        protected DropDownList TopEndCredit;
        protected TextBox TopEndNum;
        protected TextBox TopEndRate;
        protected TextBox TopKeyWord;
        protected TextBox TopPageNo;
        protected TextBox TopPageSize;
        protected DropDownList TopSort;
        protected DropDownList TopStartCredit;
        protected TextBox TopStartNum;
        protected TextBox TopStartRate;
        protected Literal txtProduct;
        protected TextBox txtUserId;
        protected TextBox txtUserId2;
        protected FileUpload uploadExcel;
        protected string UploadPath = ("/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/");

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            if (this.Session["ProductList"] != null)
            {
                int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
                int pageSize = this.AspNetPager1.PageSize;
                List<TaobaokeItem> source = this.Session["ProductList"] as List<TaobaokeItem>;
                this.DataListProduct.DataSource = source.Skip<TaobaokeItem>(((currentPageIndex - 1) * pageSize)).Take<TaobaokeItem>(pageSize);
                this.DataListProduct.DataBind();
            }
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int num = Globals.SafeInt(this.TopPageSize.Text, 20);
            int num2 = Globals.SafeInt(this.TopPageNo.Text, 1);
            this.AspNetPager1.PageSize = num;
            int cid = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (cid == 0)
            {
                MessageBox.ShowFailTip(this, "请选择淘宝分类");
            }
            else
            {
                string text = this.TopKeyWord.Text;
                string area = this.TopArea.Text;
                string selectedValue = this.TopSort.SelectedValue;
                int num4 = ((int) Globals.SafeDecimal(this.TopStartRate.Text, (decimal) 0M)) * 100;
                int num5 = ((int) Globals.SafeDecimal(this.TopEndRate.Text, (decimal) 0M)) * 100;
                string str4 = this.TopStartCredit.Text;
                string str5 = this.TopEndCredit.Text;
                int num6 = Globals.SafeInt(this.TopStartNum.Text, 0);
                int num7 = Globals.SafeInt(this.TopEndNum.Text, 0);
                List<TaobaokeItem> source = this.bll.GetProductDates(cid, text, area, num2, num, selectedValue, num4, num5, str4, str5, num6, num7, 0, 0);
                if ((source != null) && (source.Count > 0))
                {
                    this.AspNetPager1.RecordCount = source.Count;
                    this.DataListProduct.DataSource = source.Take<TaobaokeItem>(num);
                    this.DataListProduct.DataBind();
                    this.Session["ProductList"] = source;
                }
                else
                {
                    MessageBox.ShowFailTip(this, "获取数据失败，请检查淘宝客设置是否正确，并确保申请的淘宝Key具有获取淘宝客数据权限。");
                }
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            Func<TaobaokeItem, bool> func = null;
            Predicate<TaobaokeItem> match = null;
            string[] arryId;
            int cid = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (string.IsNullOrWhiteSpace(this.txtUserId.Text) || !PageValidate.IsNumber(this.txtUserId.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
            }
            else
            {
                int userID = Globals.SafeInt(this.txtUserId.Text, 0);
                Maticsoft.Model.Members.Users model = new Maticsoft.BLL.Members.Users().GetModel(userID);
                if (model == null)
                {
                    MessageBox.ShowFailTip(this, "该用户ID不存在");
                }
                else
                {
                    string selIDlist = this.GetSelIDlist();
                    if (selIDlist.Trim().Length == 0)
                    {
                        MessageBox.ShowFailTip(this, "选择您要导入的商品");
                    }
                    else
                    {
                        int albumId = Globals.SafeInt(this.ddlAlbumList.SelectedValue, 0);
                        if (albumId == 0)
                        {
                            MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                        }
                        else
                        {
                            arryId = selIDlist.Split(new char[] { ',' });
                            bool reRepeat = this.chkRepeat.Checked;
                            if (this.Session["ProductList"] != null)
                            {
                                int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
                                int pageSize = this.AspNetPager1.PageSize;
                                List<TaobaokeItem> source = this.Session["ProductList"] as List<TaobaokeItem>;
                                if (func == null)
                                {
                                    func = c => arryId.Contains<string>(c.NumIid.ToString());
                                }
                                int num6 = this.bll.ImportData(model.UserID, albumId, cid, source.Where<TaobaokeItem>(func).ToList<TaobaokeItem>(), reRepeat);
                                if (match == null)
                                {
                                    match = c => arryId.Contains<string>(c.NumIid.ToString());
                                }
                                source.RemoveAll(match);
                                this.AspNetPager1.RecordCount = source.Count;
                                if (((currentPageIndex - 1) * pageSize) >= source.Count)
                                {
                                    this.AspNetPager1.CurrentPageIndex = currentPageIndex - 1;
                                    this.DataListProduct.DataSource = source.Skip<TaobaokeItem>(((currentPageIndex - 2) * pageSize)).Take<TaobaokeItem>(pageSize);
                                }
                                else
                                {
                                    this.DataListProduct.DataSource = source.Skip<TaobaokeItem>(((currentPageIndex - 1) * pageSize)).Take<TaobaokeItem>(pageSize);
                                }
                                this.DataListProduct.DataBind();
                                this.Session["ProductList"] = source;
                                MessageBox.ShowSuccessTip(this, "成功导入【" + num6 + "】条数据");
                            }
                        }
                    }
                }
            }
        }

        protected void btnImportAll_Click(object sender, EventArgs e)
        {
            int cid = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (string.IsNullOrWhiteSpace(this.txtUserId.Text) || !PageValidate.IsNumber(this.txtUserId.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
            }
            else
            {
                int albumId = Globals.SafeInt(this.ddlAlbumList.SelectedValue, 0);
                if (albumId == 0)
                {
                    MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                }
                else
                {
                    int userid = Globals.SafeInt(this.txtUserId.Text, 0);
                    bool reRepeat = this.chkRepeat.Checked;
                    if (this.Session["ProductList"] != null)
                    {
                        List<TaobaokeItem> list = this.Session["ProductList"] as List<TaobaokeItem>;
                        int num4 = this.bll.ImportData(userid, albumId, cid, list, reRepeat);
                        this.AspNetPager1.RecordCount = 0;
                        this.DataListProduct.DataSource = null;
                        this.DataListProduct.DataBind();
                        this.Session["ProductList"] = null;
                        MessageBox.ShowSuccessTip(this, "成功导入【" + num4 + "】条数据");
                    }
                }
            }
        }

        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtUserId2.Text) || !PageValidate.IsNumber(this.txtUserId2.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
            }
            else
            {
                int userID = Globals.SafeInt(this.txtUserId2.Text, 0);
                Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
                if (users.GetModel(userID) == null)
                {
                    MessageBox.ShowFailTip(this, "该用户ID不存在");
                }
                else
                {
                    int albumid = Globals.SafeInt(this.ddlAlbumList2.SelectedValue, 0);
                    if (albumid == 0)
                    {
                        MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                    }
                    else
                    {
                        int categoryId = Globals.SafeInt(this.SNSCategoryDropList.SelectedValue, 0);
                        string fileName = this.uploadExcel.PostedFile.FileName;
                        string errorMsg = "出现异常，请检查您的数据格式";
                        int count = 0;
                        if (!this.uploadExcel.HasFile)
                        {
                            MessageBox.ShowSuccessTip(this, "请上传文件");
                        }
                        else
                        {
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(this.UploadPath)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(this.UploadPath));
                            }
                            this.uploadExcel.PostedFile.SaveAs(base.Server.MapPath(this.UploadPath) + fileName);
                            if (this.ExcelImport(this.UploadPath, fileName, userID, albumid, categoryId, out errorMsg, ref count))
                            {
                                MessageBox.ShowSuccessTip(this, "成功插入" + count + "条数据");
                            }
                            else
                            {
                                MessageBox.ShowSuccessTip(this, "插入失败,信息:" + errorMsg + "提示：检查您填写数据的数据格式");
                            }
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }

        public bool ExcelImport(string Path, string FileName, int userid, int albumid, int categoryId, out string ErrorMsg, ref int Count)
        {
            bool flag;
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_TaoBaoExcel_SheetName");
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = "Page1";
            }
            OleDbConnection connection = new OleDbConnection();
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataSet dataSet = new DataSet();
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Persist Security Info=False;Data Source=" + base.Server.MapPath(Path + FileName) + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2'";
            connection.Open();
            command.Connection = connection;
            command.CommandText = "  select  *  from  [" + valueByCache + "$]  ";
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(dataSet, "Excel");
                Count = this.bll.ImportExcelData(userid, albumid, categoryId, dataSet.Tables[0]);
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

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.DataListProduct.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.DataListProduct.Items[i].FindControl("ckProduct");
                HiddenField field = (HiddenField) this.DataListProduct.Items[i].FindControl("hfProduct");
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (field.Value != null)
                    {
                        str = str + field.Value + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int defaultUserId = new Maticsoft.BLL.Members.Users().GetDefaultUserId();
                this.txtUserId.Text = defaultUserId.ToString();
                this.txtUserId2.Text = defaultUserId.ToString();
                List<AlbumIndex> listByUserId = this.albumBll.GetListByUserId(defaultUserId, -1);
                this.ddlAlbumList.DataSource = listByUserId;
                this.ddlAlbumList.DataTextField = "AlbumName";
                this.ddlAlbumList.DataValueField = "AlbumID";
                this.ddlAlbumList.DataBind();
                this.ddlAlbumList.Items.Insert(0, new ListItem("--请选择--", ""));
                this.ddlAlbumList2.DataSource = listByUserId;
                this.ddlAlbumList2.DataTextField = "AlbumName";
                this.ddlAlbumList2.DataValueField = "AlbumID";
                this.ddlAlbumList2.DataBind();
                this.ddlAlbumList2.Items.Insert(0, new ListItem("--请选择--", ""));
            }
        }

        protected void Text_Change(object sender, EventArgs e)
        {
            int userId = Globals.SafeInt(this.txtUserId.Text, 0);
            List<AlbumIndex> listByUserId = this.albumBll.GetListByUserId(userId, -1);
            this.ddlAlbumList.DataSource = listByUserId;
            this.ddlAlbumList.DataTextField = "AlbumName";
            this.ddlAlbumList.DataValueField = "AlbumID";
            this.ddlAlbumList.DataBind();
            this.ddlAlbumList.Items.Insert(0, new ListItem("--请选择--", ""));
        }

        protected void Text2_Change(object sender, EventArgs e)
        {
            int userId = Globals.SafeInt(this.txtUserId2.Text, 0);
            List<AlbumIndex> listByUserId = this.albumBll.GetListByUserId(userId, -1);
            this.ddlAlbumList2.DataSource = listByUserId;
            this.ddlAlbumList2.DataTextField = "AlbumName";
            this.ddlAlbumList2.DataValueField = "AlbumID";
            this.ddlAlbumList2.DataBind();
            this.ddlAlbumList2.Items.Insert(0, new ListItem("--请选择--", ""));
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x254;
            }
        }
    }
}

