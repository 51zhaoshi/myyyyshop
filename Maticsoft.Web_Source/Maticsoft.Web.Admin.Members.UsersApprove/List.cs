namespace Maticsoft.Web.Admin.Members.UsersApprove
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_ApproveList = 0xb1;
        protected int Act_DeleteList = 0xb0;
        private UsersApprove bll = new UsersApprove();
        protected Button btnBatchAccess;
        protected Button btnBatchUnAcc;
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList ddlApproveStatus;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal2;
        protected Literal Literal7;
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet approveList = new DataSet();
            approveList = this.bll.GetApproveList(this.ddlApproveStatus.SelectedValue, this.txtKeyword.Text.Trim());
            this.gridView.DataSetSource = approveList;
        }

        protected void btnBatchAccess_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.BatchUpdate(selIDlist.TrimEnd(new char[] { ',' }), "1"))
                {
                    new UsersExp().UpdateIsDPI(selIDlist.TrimEnd(new char[] { ',' }), 1);
                }
                MessageBox.ShowSuccessTip(this, "批量审核成功！");
                this.gridView.OnBind();
            }
        }

        protected void btnBatchUnAcc_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.BatchUpdate(selIDlist.TrimEnd(new char[] { ',' }), "0"))
                {
                    new UsersExp().UpdateIsDPI(selIDlist.TrimEnd(new char[] { ',' }), 0);
                }
                MessageBox.ShowSuccessTip(this, "批量拒绝成功！");
                this.gridView.OnBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected string GetApproveStatus(object obj)
        {
            if (obj != null)
            {
                if (string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return "审核失败";
                }
                switch (obj.ToString())
                {
                    case "0":
                        return "未 审 核";

                    case "1":
                        return "审核通过";

                    case "2":
                        return "审核失败";
                }
            }
            return "审核失败";
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
            DataControlRowType rowType = e.Row.RowType;
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ApproveList)) && (base.GetPermidByActID(this.Act_ApproveList) != -1))
                {
                    this.btnBatchAccess.Visible = false;
                    this.btnBatchUnAcc.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xaf;
            }
        }
    }
}

