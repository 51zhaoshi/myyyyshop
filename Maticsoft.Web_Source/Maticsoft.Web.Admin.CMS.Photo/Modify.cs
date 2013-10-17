namespace Maticsoft.Web.Admin.CMS.Photo
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsRecomend;
        protected DropDownList ddlAlbum;
        protected PhotoClassDropList ddlPhotoClass;
        protected DropDownList ddlState;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblPVCount;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected HtmlAnchor ShowImage;
        protected Image ThumbImage;
        protected TextBox txtDescription;
        protected TextBox txtPhotoName;
        protected TextBox txtSequence;
        protected TextBox txtTags;

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtPhotoName.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorImageNameNull);
            }
            else if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorIntroductionNull);
            }
            else if (!PageValidate.IsNumber(this.txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorOrderFormat);
            }
            else if (string.IsNullOrWhiteSpace(this.txtTags.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorLabel);
            }
            else
            {
                string text = this.txtPhotoName.Text;
                string str2 = this.txtDescription.Text;
                int num = int.Parse(this.ddlAlbum.SelectedValue);
                int num2 = int.Parse(this.ddlState.SelectedValue);
                int num3 = int.Parse(this.lblPVCount.Text);
                int num4 = 0;
                if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
                {
                    num4 = int.Parse(this.ddlPhotoClass.SelectedValue);
                }
                int num5 = int.Parse(this.txtSequence.Text);
                bool flag = this.chkIsRecomend.Checked;
                string str3 = this.txtTags.Text;
                Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
                Maticsoft.Model.CMS.Photo model = photo.GetModel(this.PhotoID);
                if (model != null)
                {
                    model.PhotoName = text;
                    model.Description = str2;
                    model.AlbumID = num;
                    model.State = num2;
                    model.PVCount = num3;
                    model.ClassID = num4;
                    model.Sequence = new int?(num5);
                    model.IsRecomend = new bool?(flag);
                    model.Tags = str3;
                    if (photo.Update(model))
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='list.aspx?AlbumID=" + num + "'");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ddlAlbum.DataSource = new Maticsoft.BLL.CMS.PhotoAlbum().GetList("");
                this.ddlAlbum.DataTextField = "AlbumName";
                this.ddlAlbum.DataValueField = "AlbumID";
                this.ddlAlbum.DataBind();
                this.ddlAlbum.Items.Add(new ListItem("", "0"));
                if (this.PhotoID > 0)
                {
                    this.ShowInfo();
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Photo model = new Maticsoft.BLL.CMS.Photo().GetModel(this.PhotoID);
            if (model != null)
            {
                this.txtPhotoName.Text = model.PhotoName;
                this.ShowImage.HRef = model.ImageUrl;
                this.txtDescription.Text = model.Description;
                this.ddlState.SelectedValue = model.State.ToString();
                this.lblCreatedUserID.Text = model.CreatedUserName;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblPVCount.Text = model.PVCount.ToString();
                if (model.ClassID != 1)
                {
                    this.ddlPhotoClass.SelectedValue = model.ClassID.ToString();
                }
                this.ThumbImage.ImageUrl = FileHelper.GeThumbImage(model.ThumbImageUrl, "T235x1280_");
                if (model.Sequence.HasValue)
                {
                    this.txtSequence.Text = model.Sequence.ToString();
                }
                if (model.IsRecomend.HasValue)
                {
                    this.chkIsRecomend.Checked = model.IsRecomend.Value;
                }
                this.txtTags.Text = model.Tags;
                this.ddlAlbum.SelectedValue = model.AlbumID.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xed;
            }
        }

        public int PhotoID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }
    }
}

