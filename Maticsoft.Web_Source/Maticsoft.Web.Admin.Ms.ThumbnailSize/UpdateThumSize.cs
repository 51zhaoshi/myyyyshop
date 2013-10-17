namespace Maticsoft.Web.Admin.Ms.ThumbnailSize
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using System;
    using System.Web.UI.WebControls;

    public class UpdateThumSize : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkWatermark;
        protected DropDownList ddlTheme;
        protected DropDownList ddlThumMode;
        protected DropDownList ddlType;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal51;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RangeValidator RangeValidator1;
        protected RangeValidator RangeValidator2;
        protected TextBox tCloudSizeName;
        protected TextBox tDesc;
        protected TextBox tHeight;
        private Maticsoft.BLL.Ms.ThumbnailSize thumBll = new Maticsoft.BLL.Ms.ThumbnailSize();
        protected Literal tName;
        protected TextBox tWidth;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ThumSizeList.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Ms.ThumbnailSize model = this.thumBll.GetModel(this.tName.Text);
            model.Type = Globals.SafeInt(this.ddlType.SelectedValue, 0);
            model.ThumName = this.tName.Text.Trim();
            model.ThumWidth = Globals.SafeInt(this.tWidth.Text, 1);
            model.ThumHeight = Globals.SafeInt(this.tHeight.Text, 1);
            model.Remark = this.tDesc.Text.Trim();
            model.Theme = this.ddlTheme.SelectedValue;
            model.ThumMode = Globals.SafeInt(this.ddlThumMode.SelectedValue, 0);
            model.IsWatermark = this.chkWatermark.Checked;
            model.CloudSizeName = this.tCloudSizeName.Text;
            if (this.thumBll.Update(model))
            {
                base.Response.Redirect("ThumSizeList.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, "更新失败！请重试。");
            }
        }

        protected void ddlType_Change(object sender, EventArgs e)
        {
            this.ddlTheme.DataSource = FileHelper.GetThemeList(this.ddlType.SelectedItem.Text);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            this.ddlTheme.DataBind();
            this.ddlTheme.Items.Insert(0, new ListItem("全部", ""));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (base.Request.Params["Name"] != null)) && (base.Request.Params["Name"].ToString() != ""))
            {
                string thumName = base.Request.Params["Name"];
                Maticsoft.Model.Ms.ThumbnailSize model = this.thumBll.GetModel(thumName);
                if (model == null)
                {
                    base.Response.Redirect("ThumSizeList.aspx");
                }
                this.tWidth.Text = model.ThumWidth.ToString();
                this.tName.Text = model.ThumName;
                this.tHeight.Text = model.ThumHeight.ToString();
                this.tDesc.Text = model.Remark;
                this.ddlThumMode.SelectedValue = model.ThumMode.ToString();
                this.ddlType.SelectedValue = model.Type.ToString();
                this.ddlTheme.DataSource = FileHelper.GetThemeList(this.ddlType.SelectedItem.Text);
                this.ddlTheme.DataTextField = "Name";
                this.ddlTheme.DataValueField = "Name";
                this.ddlTheme.DataBind();
                this.ddlTheme.Items.Insert(0, new ListItem("全部", ""));
                this.ddlTheme.SelectedValue = model.Theme;
                this.chkWatermark.Checked = model.IsWatermark;
                this.tCloudSizeName.Text = model.CloudSizeName;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 340;
            }
        }
    }
}

