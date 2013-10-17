namespace Maticsoft.Web.Admin.SNS.Posts
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x19;
        private Maticsoft.BLL.SNS.Posts bll = new Maticsoft.BLL.SNS.Posts();
        protected Button btnChecked;
        protected Button btnCheckedUnpass;
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList dropType;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;
        protected TextBox txtPoster;
        protected int type = -1;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[7].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.txtPoster.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("CreatedNickName like '%{0}%' ", this.txtPoster.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtBeginTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,CreatedDate)>='" + this.txtBeginTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,CreatedDate)<='" + this.txtEndTime.Text.Trim() + "' ");
            }
            if (this.Type > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" Type=" + this.Type + " ");
            }
            if (this.txtKeyword.Text.Length > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" Description like '%" + this.txtKeyword.Text + "%' ");
            }
            if (builder.Length > 0)
            {
                builder.Append(" and ");
            }
            builder.Append(" 1=1 order by CreatedDate desc ");
            DataSet list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStatusList(selIDlist, 1))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新动态(PostIDs=" + selIDlist + ")为审核状态成功", this);
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新动态(PostIDs=" + selIDlist + ")为审核状态失败", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnCheckedUnPass_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStatusList(selIDlist, 2))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新动态(PostIDs=" + selIDlist + ")为未审核状态成功", this);
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新动态(PostIDs=" + selIDlist + ")为未审核状态失败", this);
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            if (this.GetSelIDlist().Trim().Length != 0)
            {
                int result = 0;
                int num2 = 0;
                string selIDTypelist = this.GetSelIDTypelist("1");
                if (!string.IsNullOrEmpty(selIDTypelist) && (selIDTypelist.Length > 0))
                {
                    DataSet set = photos.DeleteListEx(selIDTypelist, out result, true, base.CurrentUser.UserID);
                    if (set != null)
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除图片动态(ProductID=" + selIDTypelist + ")成功", this);
                }
                string str3 = this.GetSelIDTypelist("2");
                if (!string.IsNullOrEmpty(str3) && (str3.Length > 0))
                {
                    DataSet set2 = products.DeleteListEx(str3, out num2, true, base.CurrentUser.UserID);
                    if (set2 != null)
                    {
                        this.PhysicalFileInfo(set2.Tables[0]);
                    }
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除商品动态(ProductID=" + str3 + ")成功", this);
                }
                string postIDs = this.GetSelIDTypelist("0");
                bool flag = false;
                if (postIDs.Length > 0)
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除文字动态(PostID=" + postIDs + ")成功", this);
                    flag = this.bll.DeleteListByNormalPost(postIDs, true, base.CurrentUser.UserID);
                }
                if (((result == 1) || (num2 == 1)) || flag)
                {
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "删除失败！");
                }
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

        public string GetContent(object Type, object TargetId, object Description, object ImageUrl)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(Type))
            {
                return str;
            }
            string currentRoutePath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(AreaRoute.SNS);
            switch (Globals.SafeInt(Type.ToString(), -1))
            {
                case 0:
                    return ((Description != null) ? Description.ToString() : "");

                case 1:
                    if (StringPlus.IsNullOrEmpty(ImageUrl))
                    {
                        return ((Description != null) ? Description.ToString() : "");
                    }
                    return (" <a  target='_blank' href='" + currentRoutePath + "Photo/Detail/" + TargetId.ToString() + "'><img src=\"" + FileHelper.GeThumbImage(ImageUrl.ToString(), "T80x80_") + "\" style=\"height:64px;width:64px;border-width:0px;\"><a>");

                case 2:
                    if (StringPlus.IsNullOrEmpty(ImageUrl))
                    {
                        return ((Description != null) ? Description.ToString() : "");
                    }
                    return string.Concat(new object[] { " <a  target='_blank' href='", currentRoutePath, "Product/Detail/", TargetId.ToString(), "'><img src=\"", ImageUrl, "\" style=\"height:64px;width:64px;border-width:0px;\"><a>" });

                case 3:
                    return ((Description != null) ? Description.ToString() : "");

                case 4:
                {
                    string newValue = currentRoutePath + "Blog/BlogDetail?id=" + TargetId;
                    string str4 = Description.ToString();
                    return (!string.IsNullOrWhiteSpace(str4) ? str4.Replace("{BlogUrl}", newValue) : "");
                }
            }
            return ((Description != null) ? Description.ToString() : "");
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

        private string GetSelIDTypelist(string type)
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Values.Count > 1)
                    {
                        if ((this.gridView.DataKeys[i].Values[1].ToString() == type) && (type == "0"))
                        {
                            str = str + this.gridView.DataKeys[i].Values[0] + ",";
                        }
                        else if (this.gridView.DataKeys[i].Values[1].ToString() == type)
                        {
                            str = str + this.gridView.DataKeys[i].Values[2] + ",";
                        }
                    }
                }
            }
            if (flag && (str.Length > 0))
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
                        return "未通过";
                }
            }
            return str;
        }

        public string GetTypeName(object Type, object TargetId)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(Type))
            {
                return str;
            }
            switch (Globals.SafeInt(Type.ToString(), -1))
            {
                case 0:
                    return "文字 ";

                case 1:
                    return (" <a  target='_blank' href='" + Maticsoft.Components.MvcApplication.GetCurrentRoutePath(AreaRoute.SNS) + "Photo/Detail/" + TargetId.ToString() + "'>图片<a>");

                case 2:
                    return (" <a  target='_blank' href='" + Maticsoft.Components.MvcApplication.GetCurrentRoutePath(AreaRoute.SNS) + "Product/Detail/" + TargetId.ToString() + "'>商品 <a>");

                case 3:
                    return "视频 ";
            }
            return "文字 ";
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandArgument.Equals("Delete"))
            {
                return;
            }
            if ((e.CommandName == null) || (e.CommandName.ToString() == ""))
            {
                return;
            }
            int postID = Globals.SafeInt(e.CommandName, -1);
            Maticsoft.Model.SNS.Posts modelByCache = this.bll.GetModelByCache(postID);
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            int result = 0;
            DataSet set = new DataSet();
            int valueOrDefault = modelByCache.Type.GetValueOrDefault();
            if (modelByCache.Type.HasValue)
            {
                switch (valueOrDefault)
                {
                    case 0:
                        if ((this.bll.DeleteEx(postID, true, base.CurrentUser.UserID) && !string.IsNullOrWhiteSpace(modelByCache.ImageUrl)) && !modelByCache.ImageUrl.StartsWith("http://"))
                        {
                            this.DeletePhysicalFile(modelByCache.ImageUrl);
                        }
                        goto Label_01B3;

                    case 1:
                        set = photos.DeleteListEx(modelByCache.TargetId.ToString(), out result, true, base.CurrentUser.UserID);
                        if (set != null)
                        {
                            this.PhysicalFileInfo(set.Tables[0]);
                        }
                        goto Label_01B3;

                    case 2:
                        set = products.DeleteListEx(modelByCache.TargetId.ToString(), out result, true, base.CurrentUser.UserID);
                        if (set != null)
                        {
                            this.PhysicalFileInfo(set.Tables[0]);
                        }
                        goto Label_01B3;
                }
            }
            if ((this.bll.DeleteEx(postID, true, base.CurrentUser.UserID) && !string.IsNullOrWhiteSpace(modelByCache.ImageUrl)) && !modelByCache.ImageUrl.StartsWith("http://"))
            {
                this.DeletePhysicalFile(modelByCache.ImageUrl);
            }
        Label_01B3:
            this.gridView.OnBind();
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除动态(PostID=" + postID + ")成功", this);
            MessageBox.ShowSuccessTip(this, "删除成功！");
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("linkbtnDel");
            }
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_ApproveList)) && (base.GetPermidByActID(base.Act_ApproveList) != -1))
                {
                    this.btnChecked.Visible = false;
                    this.btnCheckedUnpass.Visible = false;
                }
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

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x5b;
            }
        }

        public int Type
        {
            get
            {
                string selectedValue = this.dropType.SelectedValue;
                this.type = Globals.SafeInt(selectedValue, 0);
                return this.type;
            }
        }
    }
}

