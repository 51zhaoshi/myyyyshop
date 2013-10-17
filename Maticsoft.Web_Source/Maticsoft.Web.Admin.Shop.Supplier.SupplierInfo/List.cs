namespace Maticsoft.Web.Admin.Shop.Supplier.SupplierInfo
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
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
        protected int Act_AddData = 0x215;
        protected int Act_DelData = 0x217;
        protected int Act_UpdateData = 0x216;
        private SupplierInfo bll = new SupplierInfo();
        private Maticsoft.BLL.Members.Users bllUsers = new Maticsoft.BLL.Members.Users();
        protected Button btnApproveList;
        protected Button btnDelete;
        protected Button btnInverseApprove;
        protected Button btnSearch;
        protected Button btnUpdateState;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected Literal Literal2;
        protected Literal Literal5;
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("Name like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSource = list;
            this.gridView.DataBind();
        }

        protected void btnApproveList_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string strWhere = "Status=" + 1;
                this.bll.UpdateList(selIDlist, strWhere);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DeleteList(selIDlist);
                this.bllUsers.DeleteListByDepartmentID(selIDlist);
                this.BindData();
            }
        }

        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string strWhere = "Status=" + 0;
                this.bll.UpdateList(selIDlist, strWhere);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void btnUpdateState_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string strWhere = "Status=" + 2;
                this.bll.UpdateList(selIDlist, strWhere);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                this.gridView.OnBind();
            }
        }

        public string DateTimeFormat(object target, string format, bool isFormat)
        {
            return TimeParser.DateTimeFormat(target, format, isFormat);
        }

        public string GetCompanyType(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return "国营供应商";
                }
            }
            else
            {
                return "个体工商";
            }
            return "私营独资供应商";
        }

        public string GetEnteClassName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        return "合资";

                    case "2":
                        return "独资";

                    case "3":
                        return "国有";

                    case "4":
                        return "私营";

                    case "5":
                        return "全民所有制";

                    case "6":
                        return "集体所有制";

                    case "7":
                        return "股份制";

                    case "8":
                        return "有限责任制";
                }
            }
            return str;
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

        public static string GetStatus(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 == "2")
                    {
                        return "冻结";
                    }
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return "删除";
                }
            }
            else
            {
                return "未审核";
            }
            return "正常";
        }

        protected void GridViewEx1_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            DataControlRowType rowType = e.Row.RowType;
        }

        protected void GridViewEx1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void GridViewEx1_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GridViewEx1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int supplierId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(supplierId);
            Maticsoft.Model.Members.Users userIdByDepartmentID = new Maticsoft.BLL.Members.Users().GetUserIdByDepartmentID(supplierId.ToString());
            if (userIdByDepartmentID != null)
            {
                this.bllUsers.DeleteByDepartmentID(supplierId);
                new UsersExp().DeleteUsersExp(userIdByDepartmentID.UserID);
            }
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
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

        protected override int Act_PageLoad
        {
            get
            {
                return 0x214;
            }
        }
    }
}

