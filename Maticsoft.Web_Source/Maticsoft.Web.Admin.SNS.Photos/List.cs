namespace Maticsoft.Web.Admin.SNS.Photos
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
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
        protected int Act_DelData = 0x15;
        protected int Act_UpdateData = 0x24f;
        private Maticsoft.BLL.SNS.Photos bll = new Maticsoft.BLL.SNS.Photos();
        protected Button btnDelete;
        protected Button btnMove;
        protected Button btnRecomend;
        protected Button btnRecomend2;
        protected Button btnSearch;
        protected Button Button2;
        protected CheckBox chkRecomend;
        protected CheckBox chkRecomend2;
        protected DropDownList dropState;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected SNSPhotoCateDropList PhotoCate;
        protected SNSPhotoCateDropList PhotoCategory;
        protected RadioButtonList radlState;
        private SiteMessage siteBll = new SiteMessage();
        protected Literal txtCateParent;
        protected HiddenField txtFrom;
        protected TextBox txtKeyword;
        protected Literal txtPhoto;
        protected HiddenField txtTo;
        public int Type = 1;

        public void BindData()
        {
            DataSet listEx = new DataSet();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" (Type <>3 or Type is Null) ", new object[0]);
            int cateId = Globals.SafeInt(this.PhotoCate.SelectedValue, 0);
            if (this.CateType == 0)
            {
                cateId = -1;
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.AppendFormat(" and", new object[0]);
                }
                builder.AppendFormat("  (PhotoName like '%{0}%' or CreatedNickName like '%{0}%')", this.txtKeyword.Text.Trim());
            }
            if (this.chkRecomend.Checked)
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.AppendFormat(" and", new object[0]);
                }
                if (this.chkRecomend2.Checked)
                {
                    builder.AppendFormat("  IsRecomend<>0 ", new object[0]);
                }
                else
                {
                    builder.AppendFormat("  IsRecomend=1 ", new object[0]);
                }
            }
            else if (this.chkRecomend2.Checked)
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.AppendFormat(" and", new object[0]);
                }
                builder.AppendFormat("  IsRecomend=2 ", new object[0]);
            }
            if (this.dropState.SelectedIndex > 0)
            {
                int num2 = Globals.SafeInt(this.dropState.SelectedValue, 0);
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.AppendFormat(" and", new object[0]);
                }
                builder.AppendFormat("  Status =" + num2, new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(this.txtFrom.Value) && PageValidate.IsDateTime(this.txtFrom.Value))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.AppendFormat(" and", new object[0]);
                }
                builder.AppendFormat("   CreatedDate >'" + this.txtFrom.Value + "' ", new object[0]);
            }
            if (!string.IsNullOrWhiteSpace(this.txtTo.Value) && PageValidate.IsDateTime(this.txtTo.Value))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.AppendFormat(" and", new object[0]);
                }
                builder.AppendFormat("  CreatedDate <'" + this.txtTo.Value + "' ", new object[0]);
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
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "批量删除失败");
                }
            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int cateId = Globals.SafeInt(this.PhotoCategory.SelectedValue, 0);
                if (!this.bll.UpdateCateList(selIDlist, cateId))
                {
                    MessageBox.ShowSuccessTip(this, "批量转移分类失败");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnNoRecomend_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (!this.bll.UpdateRecomendList(selIDlist, 0))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量取消推荐图片(PhotoIDs=" + selIDlist + ")到首页成功", this);
                    MessageBox.ShowSuccessTip(this, "批量取消推荐失败");
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
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + selIDlist + ")到首页成功", this);
                this.gridView.OnBind();
            }
        }

        protected void btnRecomend2_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (!this.bll.UpdateRecomendList(selIDlist, 2))
                {
                    MessageBox.ShowSuccessTip(this, "批量推荐到频道首页失败");
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + selIDlist + ")到首页成功", this);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DeletePhysicalFile(string path)
        {
            FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, path);
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
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除图片(PhotoID=" + num + ")成功", this);
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.btnDelete.Visible = false;
                }
                this.Type = this.CateType;
                if (this.CateType == 0)
                {
                    this.txtCateParent.Visible = false;
                    this.PhotoCate.Visible = false;
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
            base.Response.Redirect("listEx.aspx?type=" + this.CateType);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 590;
            }
        }

        public int CateType
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    num = Globals.SafeInt(base.Request.Params["type"], 1);
                }
                return num;
            }
        }
    }
}

