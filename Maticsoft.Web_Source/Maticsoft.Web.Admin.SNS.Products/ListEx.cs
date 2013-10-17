namespace Maticsoft.Web.Admin.SNS.Products
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
        protected int Act_DelData;
        protected AspNetPager AspNetPager1;
        private Maticsoft.BLL.SNS.Products bll = new Maticsoft.BLL.SNS.Products();
        protected LinkButton btnDelete;
        protected Button btnMove;
        protected Button btnRecomend;
        protected Button btnSearch;
        protected Button Button1;
        protected Button Button2;
        protected CheckBox chkRecomend;
        protected DataList DataListProduct;
        protected DropDownList dropState;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RadioButtonList RadioButtonList1;
        private SiteMessage siteBll = new SiteMessage();
        protected SNSCategoryDropList SNSCate;
        protected SNSCategoryDropList SNSCate2;
        protected Literal txtCateParent;
        protected HiddenField txtFrom;
        protected TextBox txtKeyword;
        protected Literal txtProduct;
        protected HiddenField txtTo;

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            int cateId = Globals.SafeInt(this.SNSCate.SelectedValue, 0);
            if (this.ProType == 0)
            {
                if (cateId == 0)
                {
                    builder.AppendFormat("  CategoryID is not null and CategoryID>0", new object[0]);
                }
            }
            else
            {
                builder.AppendFormat("  (CategoryID is null or CategoryID=0)", new object[0]);
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
            this.AspNetPager1.RecordCount = this.bll.GetRecordCountEx(builder.ToString(), cateId);
            this.DataListProduct.DataSource = this.bll.GetListByPageEx(builder.ToString(), cateId, "CreatedDate", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            this.DataListProduct.DataBind();
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
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除商品(ProductIds=" + selIDlist + ")成功", this);
                    this.BindData();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除商品(ProductIds=" + selIDlist + ")失败", this);
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
                    MessageBox.ShowFailTip(this, "批量转移分类失败");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量转移商品(ProductIds=" + selIDlist + ")失败", this);
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
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "取消推荐商品(ProductIds=" + selIDlist + ")到首页失败", this);
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "取消推荐商品(ProductIds=" + selIDlist + ")到首页成功", this);
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
                    MessageBox.ShowFailTip(this, "批量推荐到首页失败");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐商品(ProductIds=" + selIDlist + ")到首页失败", this);
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐商品(ProductIds=" + selIDlist + ")到首页成功", this);
                this.BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void DataListProduct_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (e.CommandArgument != null)
                {
                    int num = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    int result = 0;
                    DataSet set = this.bll.DeleteListEx(num.ToString(), out result, true, base.CurrentUser.UserID);
                    if (result <= 0)
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipDelError);
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除商品(ProductId=" + num + ")失败", this);
                        return;
                    }
                    if (set != null)
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除商品(ProductId=" + num + ")成功", this);
                }
                this.BindData();
            }
        }

        protected void DataListProduct_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton button = (LinkButton) e.Item.FindControl("lbtnDel");
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    button.Visible = false;
                }
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
                    return "推荐到首页";

                case 1:
                    return "取消推荐";
            }
            return "推荐到首页";
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.DataListProduct.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.DataListProduct.Items[i].FindControl("ckProduct");
                HiddenField field = (HiddenField) this.DataListProduct.Items[i].FindControl("hfProduct");
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
                    return "审核通过";

                case 1:
                    return "取消审核";
            }
            return "审核通过";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.ProType == 1)
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                    this.Button1.Visible = false;
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
            base.Response.Redirect("list.aspx?type=" + this.ProType);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                switch (this.ProType)
                {
                    case 1:
                        return 0x252;

                    case 2:
                        return 0x253;
                }
                return 0x253;
            }
        }

        public int ProType
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

