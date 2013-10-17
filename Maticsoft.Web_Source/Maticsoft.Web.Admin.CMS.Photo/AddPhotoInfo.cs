namespace Maticsoft.Web.Admin.CMS.Photo
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class AddPhotoInfo : PageBaseAdmin
    {
        private readonly Maticsoft.BLL.CMS.PhotoAlbum albumBll = new Maticsoft.BLL.CMS.PhotoAlbum();
        protected Button btnCancle;
        protected Button btnSave;
        private static int iAlbumId;
        protected Literal Literal1;
        protected Literal Literal2;
        private readonly Maticsoft.BLL.CMS.Photo photoBll = new Maticsoft.BLL.CMS.Photo();
        protected Repeater RepeaterPhoto;
        protected string strNormalImageHeight = ConfigSystem.GetValueByCache("NormalImageHeight");
        protected string strNormalImageWidth = ConfigSystem.GetValueByCache("NormalImageWidth");
        protected string strThumbImageHeight = ConfigSystem.GetValueByCache("ThumbImageHeight");
        protected string strThumbImageWidth = ConfigSystem.GetValueByCache("ThumbImageWidth");

        public void btnCancle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.IdList))
            {
                DataSet set;
                this.photoBll.DeleteList(this.IdList.TrimEnd(new char[] { ',' }), out set);
                if ((set != null) && (set.Tables[0].Rows.Count > 0))
                {
                    this.PhysicalFileInfo(set.Tables[0]);
                }
            }
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.RepeaterPhoto.Items.Count; i++)
            {
                TextBox box = this.RepeaterPhoto.Items[i].FindControl("txtPhotoName") as TextBox;
                PhotoClassDropList list = this.RepeaterPhoto.Items[i].FindControl("ddlPhotoClass") as PhotoClassDropList;
                CheckBox box2 = this.RepeaterPhoto.Items[i].FindControl("chkIsRecomend") as CheckBox;
                TextBox box3 = this.RepeaterPhoto.Items[i].FindControl("txtTags") as TextBox;
                TextBox box4 = this.RepeaterPhoto.Items[i].FindControl("txtDescription") as TextBox;
                int photoID = 0;
                if (!string.IsNullOrWhiteSpace(this.IdList))
                {
                    photoID = int.Parse(this.IdList.Split(new char[] { ',' })[i]);
                }
                Maticsoft.Model.CMS.Photo model = this.photoBll.GetModel(photoID);
                if (model != null)
                {
                    if ((box != null) && !string.IsNullOrWhiteSpace(box.Text))
                    {
                        model.PhotoName = box.Text;
                    }
                    if (!string.IsNullOrWhiteSpace(list.SelectedValue))
                    {
                        model.ClassID = int.Parse(list.SelectedValue);
                    }
                    if (box2 != null)
                    {
                        model.IsRecomend = new bool?(box2.Checked);
                    }
                    if (box3 != null)
                    {
                        model.Tags = box3.Text;
                    }
                    if (box4 != null)
                    {
                        model.Description = box4.Text;
                    }
                    this.photoBll.Update(model);
                    iAlbumId = model.AlbumID;
                }
            }
            MessageBox.ShowSuccessTip(this, CMSPhoto.TooltipEditedSuccess);
            base.Response.Redirect("list.aspx?AlbumID=" + iAlbumId);
        }

        private void DeletePhysicalFile(string path)
        {
            try
            {
                FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, path);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(this.IdList))
                {
                    DataSet list = this.photoBll.GetList("photoid in(" + this.IdList + ")");
                    this.RepeaterPhoto.DataSource = list;
                    this.RepeaterPhoto.DataBind();
                    if ((list != null) && (list.Tables[0].Rows.Count > 0))
                    {
                        iAlbumId = int.Parse(list.Tables[0].Rows[0]["AlbumId"].ToString());
                    }
                }
                else
                {
                    MessageBox.ShowFailTip(this, CMSPhoto.TooltipAddFail, "List.aspx");
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

        protected void RepeaterPhoto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                ((PhotoClassDropList) e.Item.FindControl("ddlPhotoClass")).DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xed;
            }
        }

        public string IdList
        {
            get
            {
                string str = string.Empty;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["idlist"]))
                {
                    str = base.Request.Params["idlist"].TrimEnd(new char[] { ',' });
                }
                return str;
            }
        }
    }
}

