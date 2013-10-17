namespace Maticsoft.Web.Admin.CMS.Photo
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected DropDownList DropAlbum;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected string strUserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.DropAlbum.DataSource = new PhotoAlbum().GetList("");
                this.DropAlbum.DataTextField = "AlbumName";
                this.DropAlbum.DataValueField = "AlbumID";
                this.DropAlbum.DataBind();
                this.DropAlbum.SelectedValue = this.AlbumId.ToString();
                this.strUserId = base.CurrentUser.UserID.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xec;
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

