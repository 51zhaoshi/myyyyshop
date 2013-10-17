namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Sales;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class SalesProductList : PageBaseAdmin
    {
        protected int Act_DelData = 0x228;
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList ddlRule;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        private Maticsoft.BLL.Shop.Sales.SalesRule ruleBll = new Maticsoft.BLL.Shop.Sales.SalesRule();
        private Maticsoft.BLL.Shop.Sales.SalesRuleProduct salesRuleProductBll = new Maticsoft.BLL.Shop.Sales.SalesRuleProduct();
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[3].Visible = false;
                this.btnDelete.Visible = false;
            }
            DataSet set = new DataSet();
            StringBuilder builder = new StringBuilder();
            int num = Globals.SafeInt(this.ddlRule.SelectedValue, 0);
            if (num > 0)
            {
                builder.AppendFormat(" RuleId={0}", num);
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("ProductName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            set = this.salesRuleProductBll.GetList(-1, builder.ToString(), "ProductId ");
            this.gridView.DataSetSource = set;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.salesRuleProductBll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetRuleName(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            int ruleId = Globals.SafeInt(obj.ToString(), 0);
            Maticsoft.Model.Shop.Sales.SalesRule model = this.ruleBll.GetModel(ruleId);
            if (model != null)
            {
                return model.RuleName;
            }
            return "未知规则名称";
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
                        int num2 = (int) this.gridView.DataKeys[i][0];
                        int num3 = Globals.SafeInt(this.gridView.DataKeys[i][1].ToString(), 0);
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, num2, "|", num3, "," });
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
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ruleId = (int) this.gridView.DataKeys[e.RowIndex][0];
            int num2 = Globals.SafeInt(this.gridView.DataKeys[e.RowIndex][1].ToString(), 0);
            this.salesRuleProductBll.Delete(ruleId, (long) num2);
            this.gridView.OnBind();
            MessageBox.ShowSuccessTip(this, "删除成功");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.btnDelete.Attributes.Add("onclick", "return confirm(\"" + Site.TooltipDelConfirm + "\")");
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.btnDelete.Visible = false;
                }
                this.ddlRule.DataSource = this.ruleBll.GetAllList();
                this.ddlRule.DataTextField = "RuleName";
                this.ddlRule.DataValueField = "RuleId";
                this.ddlRule.DataBind();
                this.ddlRule.Items.Insert(0, new ListItem("请选择", "0"));
                this.gridView.OnBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x227;
            }
        }
    }
}

