namespace Maticsoft.Web.Admin.Shop.Tags
{
    using Maticsoft.BLL.Shop.Tags;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x224;
        protected int Act_DelData = 0x222;
        protected int Act_UpdateData = 0x223;
        private Maticsoft.BLL.Shop.Tags.Tags bll = new Maticsoft.BLL.Shop.Tags.Tags();
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList dropIsRecommand;
        protected DropDownList dropStatus;
        protected GridViewEx gridView;
        protected LinkButton lbtnDelete;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[6].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[7].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and  ");
                }
                builder.AppendFormat(" TagName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            this.gridView.DataSetSource = this.bll.GetListEx(builder.ToString());
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void dropIsRecommand_Changed(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && ((this.dropIsRecommand.SelectedValue != "-1") && this.bll.UpdateIsRecommand(this.dropIsRecommand.SelectedValue, selIDlist)))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipBatchUpdateOK);
                this.gridView.OnBind();
            }
        }

        protected void dropStatus_Changed(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && ((this.dropStatus.SelectedValue != "-1") && this.bll.UpdateStatus(Globals.SafeInt(this.dropStatus.SelectedValue, 0), selIDlist)))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipBatchUpdateOK);
                this.gridView.OnBind();
            }
        }

        public string GetCateName(object target)
        {
            int categoryId = Globals.SafeInt(target.ToString(), 0);
            if (categoryId <= 0)
            {
                return "暂无分类信息";
            }
            TagCategories categories = new TagCategories();
            return categories.GetFullNameByCache(categoryId);
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

        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "不可用";

                    case 1:
                        return "可用";
                }
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
            int tagID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(tagID);
            MessageBox.ShowSuccessTip(this, "删除成功！");
            this.gridView.OnBind();
        }

        public string IsRecommand(object target)
        {
            string str = string.Empty;
            bool flag = bool.Parse(target.ToString());
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            if (flag)
            {
                return "推荐";
            }
            return "不推荐";
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.bll.DeleteList(selIDlist))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除商品标签(id=" + selIDlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData))) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.liDel.Visible = false;
                this.lbtnDelete.Visible = false;
                this.btnDelete.Visible = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x221;
            }
        }
    }
}

