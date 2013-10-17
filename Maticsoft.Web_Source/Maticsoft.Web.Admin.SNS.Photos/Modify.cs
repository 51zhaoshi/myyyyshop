namespace Maticsoft.Web.Admin.SNS.Photos
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.Photos bll = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.SNS.PhotoTags bllTags = new Maticsoft.BLL.SNS.PhotoTags();
        protected Button Button1;
        protected Button Button2;
        protected DropDownList ddlTags;
        protected HiddenField HidTags;
        protected Literal Literal2;
        protected Literal Literal3;
        protected SNSPhotoCateDropList PhotoCategory;
        protected RadioButtonList rabRecomend;
        protected RadioButtonList radlState;
        protected string TagsValue;
        protected Label txtCommentCount;
        protected Label txtCreatedNickName;
        protected TextBox txtDescription;
        protected Label txtFavouriteCount;
        protected Image txtImage;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx?type=" + this.type);
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string text = this.txtDescription.Text;
            int num = Globals.SafeInt(this.radlState.SelectedValue, 0);
            string str2 = this.txtCreatedNickName.Text;
            int num2 = Globals.SafeInt(this.PhotoCategory.SelectedValue, 0);
            int num3 = Globals.SafeInt(this.rabRecomend.SelectedValue, 0);
            Maticsoft.Model.SNS.Photos model = this.bll.GetModel(this.PhotoId);
            model.Description = text;
            model.Status = num;
            model.CreatedNickName = str2;
            model.CategoryId = num2;
            model.IsRecomend = num3;
            string[] strArray = this.HidTags.Value.Split(new char[] { ',' });
            if (strArray.Length > 0)
            {
                model.Tags = "";
                foreach (string str3 in strArray)
                {
                    model.Tags = model.Tags + "'" + str3 + "',";
                }
                model.Tags = model.Tags.Substring(0, model.Tags.LastIndexOf(","));
            }
            this.bll.Update(model);
            MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx?type=" + this.type);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo(this.PhotoId);
            }
        }

        private void ShowInfo(int PhotoID)
        {
            Maticsoft.Model.SNS.Photos model = new Maticsoft.BLL.SNS.Photos().GetModel(PhotoID);
            this.txtImage.ImageUrl = model.ThumbImageUrl;
            this.radlState.SelectedValue = model.Status.ToString();
            this.txtDescription.Text = model.Description;
            this.txtCreatedNickName.Text = model.CreatedNickName;
            this.PhotoCategory.SelectedValue = model.CategoryId.ToString();
            this.rabRecomend.SelectedValue = model.IsRecomend.ToString();
            this.txtCommentCount.Text = model.CommentCount.ToString();
            this.txtFavouriteCount.Text = model.FavouriteCount.ToString();
            this.ddlTags.DataSource = this.bllTags.GetList("");
            this.ddlTags.DataTextField = "TagName";
            this.ddlTags.DataValueField = "TagName";
            this.ddlTags.DataBind();
            this.TagsValue = model.Tags;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x250;
            }
        }

        public int PhotoId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }

        public int type
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

