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
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class ListEx : PageBaseAdmin
    {
        protected int Act_DelData = 0x15;
        protected int Act_UpdateData = 0x24f;
        protected AspNetPager AspNetPager1;
        private Maticsoft.BLL.SNS.Photos bll = new Maticsoft.BLL.SNS.Photos();
        protected LinkButton btnDelete;
        protected Button btnMove;
        protected Button btnRecomend;
        protected Button btnRecomend2;
        protected Button btnSearch;
        protected Button Button1;
        protected Button Button2;
        protected CheckBox chkRecomend;
        protected CheckBox chkRecomend2;
        protected DataList DataListPhoto;
        protected DropDownList dropState;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected SNSPhotoCateDropList PhotoCate;
        protected SNSPhotoCateDropList PhotoCategory;
        protected RadioButtonList radlState;
        private SiteMessage siteBll = new SiteMessage();
        protected Literal txtCateParent;
        protected HiddenField txtFrom;
        protected TextBox txtKeyword;
        protected Literal txtPhoto;
        protected HiddenField txtTo;

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("  (Type <>3 or Type is Null)", new object[0]);
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
            this.AspNetPager1.RecordCount = this.bll.GetRecordCountEx(builder.ToString(), cateId);
            this.DataListPhoto.DataSource = this.bll.GetListByPageEx(builder.ToString(), cateId, "", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            this.DataListPhoto.DataBind();
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
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除图片(PhotoIDs=" + selIDlist + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    this.BindData();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int cateId = Globals.SafeInt(this.PhotoCategory.SelectedValue, 0);
                if (cateId == 0)
                {
                    MessageBox.ShowSuccessTip(this, "请选择类别");
                }
                if (!this.bll.UpdateCateList(selIDlist, cateId))
                {
                    MessageBox.ShowSuccessTip(this, "批量归类失败");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "批量归类成功");
                }
                this.BindData();
            }
        }

        protected void btnNoRecomend_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (!this.bll.UpdateRecomendList(selIDlist, 0))
                {
                    MessageBox.ShowFailTip(this, "批量取消推荐失败");
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量取消推荐图片(PhotoIDs=" + selIDlist + ")到首页成功", this);
                this.BindData();
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
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + selIDlist + ")到首页失败", this);
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + selIDlist + ")到首页成功", this);
                this.BindData();
            }
        }

        protected void btnRecomend2_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (!this.bll.UpdateRecomendList(selIDlist, 2))
                {
                    MessageBox.ShowFailTip(this, "批量推荐到频道首页失败");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + selIDlist + ")到首页失败", this);
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + selIDlist + ")到首页成功", this);
                this.BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void DataListPhoto_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                new Maticsoft.BLL.SNS.Photos();
                if (e.CommandArgument != null)
                {
                    int num = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    int result = 0;
                    DataSet set = this.bll.DeleteListEx(num.ToString(), out result, true, base.CurrentUser.UserID);
                    if (result <= 0)
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipDelError);
                        return;
                    }
                    if (set != null)
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除图片(PhotoID=" + num + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                this.BindData();
            }
        }

        protected void DataListPhoto_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) && (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1)))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("spanbtnDel");
                control.Visible = false;
            }
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

        protected string GetIsRecomend(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (Globals.SafeInt(target.ToString(), 0))
            {
                case 0:
                    return "推荐频道首页";

                case 2:
                    return "取消频道首页";
            }
            return "推荐频道首页";
        }

        protected string GetRecomendIndex(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (Globals.SafeInt(target.ToString(), 0))
            {
                case 0:
                    return "推荐首页";

                case 1:
                    return "取消首页";
            }
            return "推荐首页";
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.DataListPhoto.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.DataListPhoto.Items[i].FindControl("ckPhoto");
                HiddenField field = (HiddenField) this.DataListPhoto.Items[i].FindControl("hfPhotoId");
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (field.Value != null)
                    {
                        str = str + field.Value + ",";
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
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (Globals.SafeInt(target.ToString(), 0))
            {
                case 0:
                    return "通过审核";

                case 1:
                    return "撤消审核";
            }
            return "通过审核";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                    this.Button1.Visible = false;
                }
                if (this.CateType == 0)
                {
                    this.txtCateParent.Visible = false;
                    this.PhotoCate.Visible = false;
                }
                this.BindData();
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
            base.Response.Redirect("list.aspx?type=" + this.CateType);
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

