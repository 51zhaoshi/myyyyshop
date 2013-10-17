namespace Maticsoft.Web.Admin.Shop.ProductQA
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

    public class ProductQAList : PageBaseAdmin
    {
        protected int Act_DelData = 0x1cd;
        protected int Act_UpdateData = 460;
        protected Button btnAction;
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList ddlAction;
        protected DropDownList ddlStatus;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal6;
        private ProductQA QAbll = new ProductQA();

        public void BindData()
        {
            DataSet listEX = new DataSet();
            new StringBuilder();
            int status = Globals.SafeInt(this.ddlStatus.SelectedValue, -1);
            listEX = this.QAbll.GetListEX(status);
            int count = listEX.Tables[0].Rows.Count;
            if (listEX != null)
            {
                this.gridView.DataSetSource = listEX;
            }
        }

        protected void btnAction_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ddlAction.SelectedValue))
            {
                string selIDlist = this.GetSelIDlist();
                if (selIDlist.Trim().Length != 0)
                {
                    if (this.QAbll.SetStatus(selIDlist, Globals.SafeInt(this.ddlAction.SelectedValue, 0)))
                    {
                        MessageBox.ShowSuccessTip(this, "批量操作成功！");
                        this.gridView.OnBind();
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "批量操作失败！");
                        this.gridView.OnBind();
                    }
                }
            }
            else
            {
                MessageBox.ShowFailTip(this, "请选择一个操作！");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.QAbll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetProudctName(object obj)
        {
            if ((obj != null) && !string.IsNullOrWhiteSpace(obj.ToString()))
            {
                return new ProductInfo().GetProductName(Globals.SafeLong(obj.ToString(), (long) 0L));
            }
            return "";
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
            if (obj != null)
            {
                if (string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return "";
                }
                switch (obj.ToString())
                {
                    case "0":
                        return "未审核";

                    case "1":
                        return "已审核";

                    case "2":
                        return "审核失败";
                }
            }
            return "";
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (((e.Row.RowType == DataControlRowType.DataRow) && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData))) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("btnModify");
                control.Visible = false;
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_DeleteList)) && (base.GetPermidByActID(base.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
            }
        }

        public string SubString(object target, string sign, int subLength)
        {
            return StringPlus.SubString(target, subLength, sign);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1cb;
            }
        }
    }
}

