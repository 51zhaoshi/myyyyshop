namespace Maticsoft.Web.Admin.Shop.ProductReview
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
        protected int Act_DelData = 0x1d3;
        private ProductReviews bll = new ProductReviews();
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList ddlAction;
        protected DropDownList ddlStatus;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal6;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[9].Visible = false;
            }
            DataSet listLeftOrderItems = new DataSet();
            new StringBuilder();
            int? status = null;
            if (!string.IsNullOrWhiteSpace(this.ddlStatus.SelectedValue))
            {
                int num = Globals.SafeInt(this.ddlStatus.SelectedValue, -1);
                if (num >= 0)
                {
                    status = new int?(num);
                }
            }
            listLeftOrderItems = this.bll.GetListLeftOrderItems(status);
            this.gridView.DataSetSource = listLeftOrderItems;
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

        protected void ddlAction_Changed(object sender, EventArgs e)
        {
            int status = Globals.SafeInt(this.ddlAction.SelectedValue, -1);
            if (status > -1)
            {
                string selIDlist = this.GetSelIDlist();
                if (selIDlist.Trim().Length != 0)
                {
                    if (this.bll.AuditComment(selIDlist, status))
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

        public string GetCommentStatus(object obj)
        {
            if ((obj != null) && !string.IsNullOrWhiteSpace(obj.ToString()))
            {
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
            int reviewId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(reviewId);
            this.gridView.OnBind();
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
                return 0x1d2;
            }
        }
    }
}

