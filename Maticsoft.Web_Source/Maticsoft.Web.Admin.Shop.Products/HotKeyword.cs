namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class HotKeyword : PageBaseAdmin
    {
        protected int Act_AddData = 0x1d8;
        protected int Act_DelData = 0x1da;
        protected int Act_UpdateData = 0x1d9;
        protected Button btnAdd;
        protected Button btnDelete;
        protected Button btnSearch;
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryInfoBll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        protected DropDownList dropCategories;
        protected GridViewEx gridView;
        private Maticsoft.BLL.Shop.Products.HotKeyword hotKeywordBll = new Maticsoft.BLL.Shop.Products.HotKeyword();
        protected HtmlGenericControl liDel;
        protected LinkButton LinkDelete;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox tbKeyWord;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[2].Visible = false;
            }
            DataSet listLeftjoinCategories = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("keywords like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            listLeftjoinCategories = this.hotKeywordBll.GetListLeftjoinCategories(builder.ToString());
            this.gridView.DataSetSource = listLeftjoinCategories;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int num = Globals.SafeInt(this.dropCategories.SelectedValue, -1);
            if (num == -1)
            {
                MessageBox.ShowFailTip(this, "请选择商品分类");
            }
            else
            {
                Maticsoft.Model.Shop.Products.HotKeyword model = new Maticsoft.Model.Shop.Products.HotKeyword {
                    Keywords = this.tbKeyWord.Text.Trim(),
                    CategoryId = new int?(num)
                };
                if (this.hotKeywordBll.Add(model) > 0)
                {
                    MessageBox.ShowSuccessTip(this, "添加成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败！");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.hotKeywordBll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, "删除成功！");
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            this.btnDelete_Click(e, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnAdd.Visible = false;
                }
                DataSet list = new Maticsoft.BLL.Shop.Products.CategoryInfo().GetList(" Depth = 1");
                if (!DataSetTools.DataSetIsNull(list))
                {
                    this.dropCategories.DataSource = list;
                    this.dropCategories.DataTextField = "Name";
                    this.dropCategories.DataValueField = "CategoryId";
                    this.dropCategories.DataBind();
                    this.dropCategories.Items.Insert(0, new ListItem("请选择", ""));
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1d7;
            }
        }
    }
}

