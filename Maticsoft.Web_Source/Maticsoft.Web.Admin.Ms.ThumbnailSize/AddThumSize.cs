namespace Maticsoft.Web.Admin.Ms.ThumbnailSize
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class AddThumSize : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList ddlType;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected RangeValidator RangeValidator1;
        protected RangeValidator RangeValidator2;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox tDesc;
        protected TextBox tHeight;
        private Maticsoft.BLL.Ms.ThumbnailSize thumBll = new Maticsoft.BLL.Ms.ThumbnailSize();
        protected TextBox tName;
        protected TextBox tWidth;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ThumSizeList.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (this.thumBll.Exists(this.tName.Text.Trim()))
            {
                MessageBox.ShowSuccessTip(this, "已存在该缩略图尺寸名称，请重新填写");
            }
            else
            {
                Maticsoft.Model.Ms.ThumbnailSize model = new Maticsoft.Model.Ms.ThumbnailSize {
                    Type = Globals.SafeInt(this.ddlType.SelectedValue, 0),
                    ThumName = this.tName.Text.Trim(),
                    ThumWidth = Globals.SafeInt(this.tWidth.Text, 1),
                    ThumHeight = Globals.SafeInt(this.tHeight.Text, 1),
                    Remark = this.tDesc.Text.Trim()
                };
                if (this.thumBll.Add(model))
                {
                    base.Response.Redirect("ThumSizeList.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "添加失败！请重试。");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = base.IsPostBack;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x153;
            }
        }
    }
}

