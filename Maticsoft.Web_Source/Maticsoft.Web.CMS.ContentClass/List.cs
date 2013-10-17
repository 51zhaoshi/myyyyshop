namespace Maticsoft.Web.CMS.ContentClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Web;
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
        protected int Act_AddData = 0xe0;
        protected int Act_DelData = 0xe2;
        protected int Act_UpdateData = 0xe1;
        private ContentClass bll = new ContentClass();
        protected Button btnApprove;
        protected Button btnDelete;
        protected Button btnInverseApprove;
        protected Button btnSearch;
        protected Button btnUpdateState;
        protected DropDownList ddlState;
        protected DropDownList ddlType;
        protected GridView gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        public string tag = "_self";
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[15].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[0x10].Visible = false;
            }
            DataSet set = new DataSet();
            StringBuilder builder = new StringBuilder();
            string selectedValue = this.ddlState.SelectedValue;
            if (!string.IsNullOrWhiteSpace(this.ddlState.SelectedValue))
            {
                builder.AppendFormat(" State={0} ", this.ddlState.SelectedValue);
            }
            if (!string.IsNullOrWhiteSpace(this.ddlType.SelectedValue))
            {
                string str2;
                string str = InjectionFilter.QuoteFilter(this.txtKeyword.Text.Trim());
                if (!string.IsNullOrWhiteSpace(str) && ((str2 = this.ddlType.SelectedValue) != null))
                {
                    if (!(str2 == "1"))
                    {
                        if (str2 == "2")
                        {
                            if (builder.ToString().Trim().Length > 0)
                            {
                                builder.AppendFormat(" and Description like '%{0}%' ", str);
                            }
                            else
                            {
                                builder.AppendFormat(" Description like '%{0}%' ", str);
                            }
                        }
                    }
                    else if (builder.ToString().Trim().Length > 0)
                    {
                        builder.AppendFormat(" and ClassName like '%{0}%' ", str);
                    }
                    else
                    {
                        builder.AppendFormat(" ClassName like '%{0}%' ", str);
                    }
                }
            }
            if (this.ClassID > 0)
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" CMSCC.ClassID={0} ", this.ClassID);
            }
            set = this.bll.GetListByView(0, builder.ToString(), "Sequence ASC");
            this.gridView.DataSource = set;
            this.gridView.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string strWhere = "State=" + 0;
                if (this.bll.UpdateList(selIDlist, strWhere))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                }
                this.BindData();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.BindData();
            }
        }

        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string strWhere = "State=" + 2;
                if (this.bll.UpdateList(selIDlist, strWhere))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                }
                this.BindData();
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
                string strWhere = "State=" + 1;
                if (this.bll.UpdateList(selIDlist, strWhere))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                }
                this.BindData();
            }
        }

        public string GetContentModel(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "2"))
            {
                if (str2 != "3")
                {
                    return str;
                }
            }
            else
            {
                return CMS.CCSingleArticle;
            }
            return CMS.CCArticleList;
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl("ckSelect");
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

        public string GetState(object target)
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
                    if (str2 != "2")
                    {
                        return str;
                    }
                    return Site.PendingReview;
                }
            }
            else
            {
                return Site.Approved;
            }
            return Site.Draft;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            int contentClassId = (int) this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                this.bll.SwapCategorySequence(contentClassId, SwapSequenceIndex.Down);
            }
            if (e.CommandName == "Rise")
            {
                this.bll.SwapCategorySequence(contentClassId, SwapSequenceIndex.Up);
            }
            this.BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int num = (int) DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "ClassName").ToString();
                e.Row.Cells[1].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    HtmlGenericControl control = e.Row.FindControl("spShowImage") as HtmlGenericControl;
                    control.Visible = false;
                }
                Label label = e.Row.FindControl("lblContentClassName") as Label;
                if (label != null)
                {
                    label.Text = str;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int categoryId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.DeleteCategory(categoryId);
            this.BindData();
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
                this.BindData();
                string str2 = base.Request.Params["t"];
                if (!string.IsNullOrWhiteSpace(str2) && (str2.ToLower() == "list"))
                {
                    this.tag = "_parent";
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
                return 220;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["ClassID"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

