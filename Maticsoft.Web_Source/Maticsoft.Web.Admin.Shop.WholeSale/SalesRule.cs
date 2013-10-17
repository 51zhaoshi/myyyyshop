namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class SalesRule : PageBaseAdmin
    {
        protected int Act_AddData = 0x22a;
        protected int Act_DelData = 0x22c;
        protected int Act_UpdateData = 0x22b;
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        private Maticsoft.BLL.Shop.Sales.SalesRule ruleBll = new Maticsoft.BLL.Shop.Sales.SalesRule();
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet set = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("RuleName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            set = this.ruleBll.GetList(-1, builder.ToString(), " CreatedDate desc");
            this.gridView.DataSetSource = set;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.ruleBll.DeleteListEx(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetCreatedName(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            int existingUserID = Globals.SafeInt(obj.ToString(), -1);
            if (existingUserID < 0)
            {
                return string.Empty;
            }
            User user = new User(existingUserID);
            return user.UserName;
        }

        public string GetRuleMode(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            int num = Globals.SafeInt(obj.ToString(), -1);
            if (num < 0)
            {
                return string.Empty;
            }
            switch (num)
            {
                case 0:
                    return "单个商品";

                case 1:
                    return "商品总计";
            }
            return "单个商品";
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

        public string GetStatus(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            int num = Globals.SafeInt(obj.ToString(), -1);
            if (num < 0)
            {
                return string.Empty;
            }
            switch (num)
            {
                case 0:
                    return "不启用";

                case 1:
                    return "启用";
            }
            return "不启用";
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    control.Visible = false;
                }
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
            int ruleId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.ruleBll.DeleteEx(ruleId);
            this.gridView.OnBind();
            MessageBox.ShowSuccessTip(this, "删除成功", "SalesRule.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.btnDelete.Attributes.Add("onclick", "return confirm(\"" + Site.TooltipDelConfirm + "\")");
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
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
                return 0x229;
            }
        }
    }
}

