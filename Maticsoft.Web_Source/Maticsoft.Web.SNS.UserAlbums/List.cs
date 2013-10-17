namespace Maticsoft.Web.SNS.UserAlbums
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x5e;
        private Maticsoft.BLL.SNS.UserAlbums bll = new Maticsoft.BLL.SNS.UserAlbums();
        protected Button btnRecommand;
        protected Button Button1;
        protected DropDownList ddlTypeList;
        protected DropDownList dropIsRecommand;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected LinkButton ltbnDelete;
        protected DropDownList rdoisable;
        protected DropDownList rdorecommand;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;
        protected TextBox txtName;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[0x11].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text))
            {
                builder.AppendFormat(" AlbumName like '%{0}%'", this.txtKeyword.Text);
            }
            if (Globals.SafeInt(this.rdoisable.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("  Status={0}", this.rdoisable.SelectedValue);
            }
            if (Globals.SafeInt(this.rdorecommand.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("  IsRecommend={0}", this.rdorecommand.SelectedValue);
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
            if (Globals.SafeInt(this.ddlTypeList.SelectedValue, -1) > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" AlbumID in (select AlbumsID from  SNS_UserAlbumsType where TypeID=" + this.ddlTypeList.SelectedValue + ")");
            }
            if (!string.IsNullOrWhiteSpace(builder.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    builder.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            else if (!string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                builder.AppendFormat(" CreatedNickName  like '%{0}%'", this.txtName.Text);
            }
            this.gridView.DataSetSource = this.bll.GetUserAblumSearchList(builder.ToString());
        }

        private void BindToType()
        {
            this.ddlTypeList.DataSource = new Maticsoft.BLL.SNS.AlbumType().GetList("");
            this.ddlTypeList.DataTextField = "TypeName";
            this.ddlTypeList.DataValueField = "ID";
            this.ddlTypeList.DataBind();
            this.ddlTypeList.Items.Insert(0, new ListItem("请选择分类", "-1"));
        }

        protected void btnRecommand_Click(object sender, EventArgs e)
        {
            if (this.dropIsRecommand.SelectedValue != "-1")
            {
                string selIDlist = this.GetSelIDlist();
                if (selIDlist.Trim().Length != 0)
                {
                    if (this.bll.UpdateIsRecommand(Globals.SafeInt(this.dropIsRecommand.SelectedValue, 0), selIDlist))
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量推荐专辑(id=" + selIDlist + ")成功", this);
                        MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    }
                    this.gridView.OnBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DeletePhysicalFile(List<string> list)
        {
            foreach (string str in list)
            {
                FileManage.DeleteFile(str);
            }
        }

        public string GetCategoryName(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            int albumId = Globals.SafeInt(target.ToString(), 0);
            if (albumId <= 0)
            {
                return str;
            }
            List<Maticsoft.Model.SNS.AlbumType> typeList = new Maticsoft.BLL.SNS.AlbumType().GetTypeList(albumId);
            if ((typeList == null) || (typeList.Count <= 0))
            {
                return str;
            }
            foreach (Maticsoft.Model.SNS.AlbumType type2 in typeList)
            {
                str = str + type2.TypeName + ",";
            }
            return str.Substring(0, str.LastIndexOf(","));
        }

        public string GetCoverTargetType(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "照片";

                    case 1:
                        return "可用";
                }
            }
            return str;
        }

        public string GetPrivacy(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "公开";

                    case 1:
                        return "仅好友可见";

                    case 2:
                        return "仅自己可见";
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName == "RecommandIndex") && (e.CommandArgument != null))
            {
                int ablumId = 0;
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                ablumId = Globals.SafeInt(strArray[0], 0);
                switch (Globals.SafeInt(strArray[1], 0))
                {
                    case 0:
                    case 2:
                        this.bll.UpdateRecommand(ablumId, EnumHelper.RecommendType.Home);
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "推荐专辑(id=" + ablumId + ")到首页成功", this);
                        this.gridView.OnBind();
                        goto Label_00C3;
                }
                this.bll.UpdateRecommand(ablumId, EnumHelper.RecommendType.None);
                this.gridView.OnBind();
            }
        Label_00C3:
            if ((e.CommandName == "RecommandAblum") && (e.CommandArgument != null))
            {
                int num3 = 0;
                string[] strArray2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                num3 = Globals.SafeInt(strArray2[0], 0);
                switch (Globals.SafeInt(strArray2[1], 0))
                {
                    case 0:
                    case 1:
                        this.bll.UpdateRecommand(num3, EnumHelper.RecommendType.Channel);
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "推荐专辑(id=" + num3 + ")到频道页成功", this);
                        this.gridView.OnBind();
                        return;
                }
                this.bll.UpdateRecommand(num3, EnumHelper.RecommendType.None);
                this.gridView.OnBind();
            }
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            List<string> list = this.PhotoPhysicalInfo(iD);
            List<string> list2 = this.ProductsPhysicalInfo(iD);
            if (albums.DeleteAblumAction(iD))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除专辑(id=" + iD + ")成功", this);
                if ((list != null) && (list.Count > 0))
                {
                    this.DeletePhysicalFile(list);
                }
                if ((list2 != null) && (list2.Count > 0))
                {
                    this.DeletePhysicalFile(list2);
                }
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除专辑(id=" + iD + ")失败", this);
                MessageBox.ShowFailTip(this, Site.TooltipDelError);
            }
        }

        protected void ltbnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除专辑(id=" + selIDlist + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除专辑(id=" + selIDlist + ")失败", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_DeleteList)) && (base.GetPermidByActID(base.Act_DeleteList) != -1))
                {
                    this.ltbnDelete.Visible = false;
                }
                this.BindToType();
            }
        }

        private List<string> PhotoPhysicalInfo(int ID)
        {
            List<Maticsoft.Model.SNS.Photos> list = new Maticsoft.BLL.SNS.Photos().UserUploadPhotoList(ID);
            if ((list != null) && (list.Count > 0))
            {
                List<string> list2 = new List<string>();
                foreach (Maticsoft.Model.SNS.Photos photos2 in list)
                {
                    if (!string.IsNullOrWhiteSpace(photos2.PhotoUrl) && !photos2.PhotoUrl.StartsWith("http://"))
                    {
                        list2.Add(base.Server.MapPath(photos2.PhotoUrl));
                        list2.Add(base.Server.MapPath(photos2.ThumbImageUrl));
                        list2.Add(base.Server.MapPath(photos2.NormalImageUrl));
                    }
                }
                if (list2.Count > 0)
                {
                    return list2;
                }
            }
            return null;
        }

        private List<string> ProductsPhysicalInfo(int ID)
        {
            List<Maticsoft.Model.SNS.Products> list = new Maticsoft.BLL.SNS.Products().UserUploadPhotoList(ID);
            if ((list != null) && (list.Count > 0))
            {
                List<string> list2 = new List<string>();
                foreach (Maticsoft.Model.SNS.Products products2 in list)
                {
                    if (this.RegURL(products2.ProductUrl))
                    {
                        list2.Add(base.Server.MapPath(products2.ProductUrl));
                    }
                    if (this.RegURL(products2.ThumbImageUrl))
                    {
                        list2.Add(base.Server.MapPath(products2.ThumbImageUrl));
                    }
                    if (this.RegURL(products2.NormalImageUrl))
                    {
                        list2.Add(base.Server.MapPath(products2.NormalImageUrl));
                    }
                }
                if (list2.Count > 0)
                {
                    return list2;
                }
            }
            return null;
        }

        private bool RegURL(string path)
        {
            Regex regex = new Regex("^[a-zA-z]+://(//w+(-//w+)*)(//.(//w+(-//w+)*))*(//?//S*)?$");
            return regex.Match(path).Success;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x5d;
            }
        }
    }
}

