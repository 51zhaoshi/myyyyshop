namespace Maticsoft.Web.Admin.SNS.Products
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData;
        private Maticsoft.BLL.SNS.Products bll = new Maticsoft.BLL.SNS.Products();
        protected Button btnDelete;
        protected Button btnMove;
        protected Button btnRecomend;
        protected Button btnSearch;
        protected CheckBox chkRecomend;
        protected DropDownList dropState;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected RadioButtonList radlState;
        private SiteMessage siteBll = new SiteMessage();
        protected SNSCategoryDropList SNSCate;
        protected SNSCategoryDropList SNSCate2;
        protected HiddenField tType;
        protected Literal txtCateParent;
        protected HiddenField txtFrom;
        protected TextBox txtKeyword;
        protected Literal txtProduct;
        protected HiddenField txtTo;
        public string TypeStr = "已分类商品管理";

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[4].Visible = false;
            }
            DataSet listEx = new DataSet();
            StringBuilder builder = new StringBuilder();
            int cateId = Globals.SafeInt(this.SNSCate.SelectedValue, 0);
            if (this.type > 0)
            {
                builder.AppendFormat("  (CategoryID is null or CategoryID=0)", new object[0]);
            }
            else if (cateId == 0)
            {
                builder.AppendFormat("  CategoryID is not null and CategoryID>0", new object[0]);
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and  ");
                }
                builder.AppendFormat(" (ProductName like '%{0}%' or CreatedNickName like '%{0}%')", this.txtKeyword.Text.Trim());
            }
            if (this.chkRecomend.Checked)
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and  ");
                }
                builder.AppendFormat(" IsRecomend=1", new object[0]);
            }
            if (this.dropState.SelectedIndex > 0)
            {
                int num2 = Globals.SafeInt(this.dropState.SelectedValue, 0);
                if (builder.Length > 1)
                {
                    builder.Append(" and  ");
                }
                builder.AppendFormat(" Status =" + num2, new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(this.txtFrom.Value) && PageValidate.IsDateTime(this.txtFrom.Value))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and  ");
                }
                builder.AppendFormat(" CreatedDate >'" + this.txtFrom.Value + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(this.txtTo.Value) && PageValidate.IsDateTime(this.txtTo.Value))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and  ");
                }
                builder.AppendFormat(" CreatedDate <'" + this.txtTo.Value + "' ", new object[0]);
            }
            listEx = this.bll.GetListEx(builder.ToString(), cateId);
            this.gridView.DataSetSource = listEx;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int result = 0;
                DataSet set = this.bll.DeleteListEx(selIDlist, out result, true, base.CurrentUser.UserID);
                if (result > 0)
                {
                    if (set != null)
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                    MessageBox.ShowSuccessTip(this, "批量删除成功");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除商品(id=" + selIDlist + ")成功", this);
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "批量删除失败");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除商品(id=" + selIDlist + ")失败", this);
                }
            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int cateId = Globals.SafeInt(this.SNSCate2.SelectedValue, 0);
                if (!this.bll.UpdateCateList(selIDlist, cateId))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量转移商品(id=" + selIDlist + ")分类成功", this);
                    MessageBox.ShowFailTip(this, "批量转移分类失败");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnRecomend_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (!this.bll.UpdateRecomendList(selIDlist, 1))
                {
                    MessageBox.ShowSuccessTip(this, "批量推荐到首页失败");
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐商品(id=" + selIDlist + ")到首页成功", this);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DeletePhysicalFile(string path)
        {
            if (!path.StartsWith("http://"))
            {
                FileManage.DeleteFile(base.Server.MapPath(path));
            }
            else
            {
                UpYunManager.DeleteImage(path, ApplicationKeyType.SNS);
            }
        }

        protected string GetCategoryName(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            Maticsoft.BLL.SNS.Categories categories = new Maticsoft.BLL.SNS.Categories();
            int categoryId = Globals.SafeInt(target.ToString(), 0);
            Maticsoft.Model.SNS.Categories model = categories.GetModel(categoryId);
            if (model != null)
            {
                return model.Name;
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

        protected string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), 0))
                {
                    case 0:
                        return "未审核";

                    case 1:
                        return "已审核";

                    case 2:
                        return "审核未通过";

                    case 3:
                        return "分类未明确";

                    case 4:
                        return "分类已明确";
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int num = (int) this.gridView.DataKeys[e.RowIndex].Value;
            int result = 0;
            DataSet set = this.bll.DeleteListEx(num.ToString(), out result, true, base.CurrentUser.UserID);
            if (result > 0)
            {
                if (set != null)
                {
                    this.PhysicalFileInfo(set.Tables[0]);
                }
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltipDelError);
                return;
            }
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.tType.Value = this.type.ToString();
                if (this.type > 0)
                {
                    this.txtProduct.Text = "未分类商品管理";
                    this.txtCateParent.Visible = false;
                    this.SNSCate.Visible = false;
                    this.Act_DelData = 20;
                }
                else
                {
                    this.Act_DelData = 0x13;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_DeleteList)) && (base.GetPermidByActID(base.Act_DeleteList) != -1))
                {
                    this.btnDelete.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if ((dt.Rows[i]["TargetImageURL"] != null) && (dt.Rows[i]["TargetImageURL"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["TargetImageURL"].ToString());
                    }
                    if ((dt.Rows[i]["ThumbImageUrl"] != null) && (dt.Rows[i]["ThumbImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ThumbImageUrl"].ToString());
                    }
                    if ((dt.Rows[i]["NormalImageUrl"] != null) && (dt.Rows[i]["NormalImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["NormalImageUrl"].ToString());
                    }
                }
            }
        }

        protected void radlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Response.Redirect("listEx.aspx?type=" + this.type);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                switch (this.type)
                {
                    case 1:
                        return 0x252;

                    case 2:
                        return 0x253;
                }
                return 0x253;
            }
        }

        public int type
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    num = Globals.SafeInt(base.Request.Params["type"], 0);
                }
                return num;
            }
        }
    }
}

