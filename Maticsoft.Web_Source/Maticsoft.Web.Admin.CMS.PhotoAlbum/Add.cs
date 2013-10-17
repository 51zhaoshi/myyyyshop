namespace Maticsoft.Web.Admin.CMS.PhotoAlbum
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected HtmlForm Form1;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected RadioButtonList radlPrivacy;
        protected RadioButtonList radlState;
        protected TextBox txtAlbumName;
        protected TextBox txtDescription;
        protected TextBox txtSequence;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.CMS.PhotoAlbum album = new Maticsoft.BLL.CMS.PhotoAlbum();
            if (string.IsNullOrWhiteSpace(this.txtAlbumName.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorAlbumNull);
            }
            else if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorIntroductionNull);
            }
            else if (!PageValidate.IsNumber(this.txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorOrderFormat);
            }
            else
            {
                string text = this.txtAlbumName.Text;
                string str2 = this.txtDescription.Text;
                int num = int.Parse(this.radlState.SelectedValue);
                int num2 = 0;
                int num3 = int.Parse(this.radlPrivacy.SelectedValue);
                Maticsoft.Model.CMS.PhotoAlbum album3 = new Maticsoft.Model.CMS.PhotoAlbum {
                    AlbumName = text,
                    Description = str2,
                    CoverPhoto = 0,
                    State = num,
                    CreatedUserID = base.CurrentUser.UserID,
                    CreatedDate = DateTime.Now,
                    PVCount = num2,
                    Sequence = album.GetMaxSequence(),
                    Privacy = num3,
                    LastUpdatedDate = DateTime.Now
                };
                Maticsoft.Model.CMS.PhotoAlbum model = album3;
                MessageBox.ResponseScript(this, "parent.location.href='/Admin/CMS/Photo/add.aspx?AlbumID=" + album.Add(model) + "'");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
            {
                MessageBox.ShowAndBack(this, "您没有权限");
            }
            else
            {
                Maticsoft.BLL.CMS.PhotoAlbum album = new Maticsoft.BLL.CMS.PhotoAlbum();
                this.txtSequence.Text = album.GetMaxSequence().ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xf4;
            }
        }
    }
}

