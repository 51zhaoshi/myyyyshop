namespace Maticsoft.Web.Admin.CMS.Photo
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0xef;
        protected int Act_DelData = 0xf1;
        protected int Act_UpdateData = 240;
        protected AspNetPager AspNetPager1;
        private Photo bll = new Photo();
        protected LinkButton btnDelete;
        protected Button btnSearch;
        protected DataList DataListPhoto;
        protected PhotoClassDropList ddlPhotoClass;
        protected DropDownList dorpPhotoAlbum;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal8;
        protected Literal Literal9;
        protected string strThumbImageHeight = ConfigSystem.GetValueByCache("ThumbImageHeight");
        protected string strThumbImageWidth = ConfigSystem.GetValueByCache("ThumbImageWidth");
        protected TextBox txtKeyWord;

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(this.dorpPhotoAlbum.SelectedValue);
        }

        public void BindData(string strAlbumId)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(strAlbumId) && (strAlbumId != "0"))
            {
                builder.Append("T.AlbumId=" + strAlbumId);
            }
            if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" AND ");
                }
                builder.Append("T.ClassID = " + this.ddlPhotoClass.SelectedValue);
            }
            if (!string.IsNullOrWhiteSpace(this.txtKeyWord.Text.Trim()))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" AND ");
                }
                builder.Append("T.PhotoName like '%" + this.txtKeyWord.Text.Trim() + "%' OR T.Description like '%" + this.txtKeyWord.Text.Trim() + "%' OR T.Tags like '%" + this.txtKeyWord.Text.Trim() + "%'");
            }
            this.AspNetPager1.RecordCount = this.bll.GetRecordCount(builder.ToString());
            this.DataListPhoto.DataSource = this.bll.GetListByPage(builder.ToString(), "", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            this.DataListPhoto.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                DataSet set;
                if (this.bll.DeleteList(selIDlist, out set))
                {
                    if ((set != null) && (set.Tables[0].Rows.Count > 0))
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelError);
                }
                this.BindData(this.dorpPhotoAlbum.SelectedValue);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData(this.dorpPhotoAlbum.SelectedValue);
        }

        protected void DataListPhoto_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (e.CommandArgument != null)
                {
                    DataSet set;
                    int num = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    if (!this.bll.DeleteList(num.ToString(), out set))
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipDelError);
                        return;
                    }
                    if ((set != null) && (set.Tables[0].Rows.Count > 0))
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                this.BindData(this.dorpPhotoAlbum.SelectedValue);
            }
        }

        protected void DataListPhoto_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                LinkButton button = (LinkButton) e.Item.FindControl("lbtnDel");
                button.Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("lbtnModify");
                control.Visible = false;
            }
        }

        private void DeletePhysicalFile(string path)
        {
            try
            {
                FileHelper.DeleteFile(EnumHelper.AreaType.CMS, path);
            }
            catch (Exception)
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("删除文件:{0}失败！", path), this);
            }
        }

        public string GetPhotoCover(object objCoverPhoto, object objPhoto)
        {
            string lblSetToCover = CMSPhoto.lblSetToCover;
            if ((objPhoto != null) && (objCoverPhoto != null))
            {
                string str2 = objPhoto.ToString();
                string str3 = objCoverPhoto.ToString();
                if (str2 == str3)
                {
                    lblSetToCover = CMSPhoto.lblFrontCover;
                }
            }
            return lblSetToCover;
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
                this.dorpPhotoAlbum.DataSource = new PhotoAlbum().GetList("");
                this.dorpPhotoAlbum.DataTextField = "AlbumName";
                this.dorpPhotoAlbum.DataValueField = "AlbumID";
                this.dorpPhotoAlbum.DataBind();
                this.dorpPhotoAlbum.Items.Add(new ListItem("", "0"));
                this.dorpPhotoAlbum.SelectedValue = this.AlbumId.ToString();
                this.BindData(this.AlbumId.ToString());
            }
        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ImageUrl"].ToString());
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

        protected override int Act_PageLoad
        {
            get
            {
                return 0xeb;
            }
        }

        public int AlbumId
        {
            get
            {
                int num = 0;
                if ((base.Request.Params["AlbumID"] != null) && PageValidate.IsNumber(base.Request.Params["AlbumID"]))
                {
                    num = int.Parse(base.Request.Params["AlbumID"]);
                }
                return num;
            }
        }
    }
}

