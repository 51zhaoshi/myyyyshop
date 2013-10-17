namespace Maticsoft.Web.Admin.Shop.ProductType
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ListV : PageBaseAdmin
    {
        protected int Act_AddData = 500;
        protected int Act_DelData = 0x1f6;
        protected int Act_UpdateData = 0x1f5;
        private Maticsoft.BLL.Shop.Products.AttributeValue bll = new Maticsoft.BLL.Shop.Products.AttributeValue();
        protected Button btnBack;
        protected Button btnDelete;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal3;
        private Maticsoft.BLL.Shop.Products.ProductType ptypeBll = new Maticsoft.BLL.Shop.Products.ProductType();

        public void BindData()
        {
            DataSet listByAttribute = new DataSet();
            listByAttribute = this.bll.GetListByAttribute(this.AttId);
            this.gridView.DataSetSource = listByAttribute;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (this.AddOrModify > 0)
            {
                base.Response.Redirect("Step2.aspx?tid=" + this.ProductTypeId);
            }
            else
            {
                base.Response.Redirect(string.Concat(new object[] { "Modify2.aspx?tid=", this.ProductTypeId, "&ed=", this.AttId }));
            }
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            long num2 = (long) this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                this.ptypeBll.SwapSeqManage(null, new long?(this.AttId), new long?(num2), SwapSequenceIndex.Down, false);
            }
            if (e.CommandName == "Rise")
            {
                this.ptypeBll.SwapSeqManage(null, new long?(this.AttId), new long?(num2), SwapSequenceIndex.Up, false);
            }
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    control.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long num = (long) this.gridView.DataKeys[e.RowIndex].Value;
            this.ptypeBll.DeleteManage(null, null, new long?(num));
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if ((this.ProductTypeId == 0) || (this.AttId == 0L))
                {
                    MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                }
                else
                {
                    this.ShowInfo();
                    if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                    {
                        this.btnDelete.Visible = false;
                    }
                    if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                    {
                        this.liAdd.Visible = false;
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
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Products.AttributeInfo model = new Maticsoft.BLL.Shop.Products.AttributeInfo().GetModel(this.AttId);
            if (model != null)
            {
                this.Literal1.Text = model.AttributeName;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1f3;
            }
        }

        private int AddOrModify
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["a"]))
                {
                    num = Globals.SafeInt(base.Request.Params["a"], 0);
                }
                return num;
            }
        }

        private long AttId
        {
            get
            {
                long num = 0L;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["ed"]))
                {
                    num = Globals.SafeInt(base.Request.Params["ed"], 0);
                }
                return num;
            }
        }

        private int ProductTypeId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["tid"]))
                {
                    num = Globals.SafeInt(base.Request.Params["tid"], 0);
                }
                return num;
            }
        }
    }
}

