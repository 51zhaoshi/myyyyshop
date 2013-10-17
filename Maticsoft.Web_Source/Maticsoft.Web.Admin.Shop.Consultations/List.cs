namespace Maticsoft.Web.Admin.Shop.Consultations
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x198;
        protected int Act_UpdateData = 0x197;
        private ProductConsults bll = new ProductConsults();
        protected Button btnDelete;
        protected Button btnSearch;
        protected Button btnStatus;
        protected DropDownList ddlStatus;
        protected GridViewEx gridView;
        private ProductInfo infoBll = new ProductInfo();
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;

        public void BindData()
        {
            DataSet set = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.ddlStatus.SelectedValue))
            {
                builder.AppendFormat(" IsReply ={0} ", this.ddlStatus.SelectedValue);
            }
            set = this.bll.GetList(-1, builder.ToString(), " CreatedDate desc");
            this.gridView.DataSetSource = set;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.UpdateStatusList(selIDlist, 1);
                MessageBox.ShowSuccessTip(this, "批量审核成功");
                this.gridView.OnBind();
            }
        }

        public string GetProductName(object target)
        {
            string productName = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                long productId = Globals.SafeLong(target, (long) 0L);
                productName = this.infoBll.GetProductName(productId);
            }
            return productName;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal literal = (Literal) e.Row.FindControl("Literal3");
                HiddenField field = (HiddenField) e.Row.FindControl("hfCid");
                if ((bool) DataBinder.Eval(e.Row.DataItem, "IsReply"))
                {
                    literal.Text = "";
                }
                else if (base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) || (base.GetPermidByActID(this.Act_UpdateData) == -1))
                {
                    literal.Text = "<a class=\"iframe\" href=\"Modify.aspx?id=" + field.Value + "\">回复</a>";
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.btnDelete.Visible = false;
                    this.liDel.Visible = false;
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
                return 0x194;
            }
        }
    }
}

