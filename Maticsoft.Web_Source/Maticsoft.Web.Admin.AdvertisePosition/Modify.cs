namespace Maticsoft.Web.Admin.AdvertisePosition
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsOne;
        protected DropDownList ddlShowType;
        protected Label lblAdvPositionId;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtAdvHtml;
        protected TextBox txtAdvPositionName;
        protected TextBox txtHeight;
        protected TextBox txtRepeatColumns;
        protected TextBox txtTimeInterval;
        protected TextBox txtWidth;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.Model.Settings.AdvertisePosition();
            int num = int.Parse(this.lblAdvPositionId.Text);
            model.AdvHtml = string.Empty;
            if (this.txtAdvPositionName.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "广告位名称不能为空！");
            }
            else
            {
                string selectedValue = this.ddlShowType.SelectedValue;
                switch (selectedValue)
                {
                    case "1":
                        if (!PageValidate.IsNumber(this.txtRepeatColumns.Text))
                        {
                            MessageBox.ShowFailTip(this, "请数如正确的横向平铺时行显示个数！");
                            return;
                        }
                        model.RepeatColumns = new int?(Globals.SafeInt(this.txtRepeatColumns.Text, 0));
                        break;

                    case "4":
                        if (string.IsNullOrWhiteSpace(this.txtAdvHtml.Text))
                        {
                            MessageBox.ShowFailTip(this, "请数广告位内容！");
                            return;
                        }
                        model.AdvHtml = this.txtAdvHtml.Text.Trim();
                        break;
                }
                if (!PageValidate.IsNumber(this.txtWidth.Text) || !PageValidate.IsNumber(this.txtHeight.Text))
                {
                    MessageBox.ShowFailTip(this, "请设置此广告位里面广告内容的宽、高，单位为像素（px）！");
                }
                else
                {
                    model.Width = new int?(Globals.SafeInt(this.txtWidth.Text, 0));
                    model.Height = new int?(Globals.SafeInt(this.txtHeight.Text, 0));
                    if (this.chkIsOne.Checked)
                    {
                        if (!PageValidate.IsNumber(this.txtTimeInterval.Text))
                        {
                            MessageBox.ShowFailTip(this, "请输入正确的循环广告时间间隔！");
                            return;
                        }
                        model.IsOne = true;
                        model.TimeInterval = new int?(Globals.SafeInt(this.txtTimeInterval.Text, 0));
                    }
                    else
                    {
                        model.IsOne = false;
                    }
                    model.AdvPositionId = num;
                    model.ShowType = new int?(Globals.SafeInt(selectedValue, -1));
                    model.AdvPositionName = this.txtAdvPositionName.Text.Trim();
                    model.CreatedDate = new DateTime?(DateTime.Now);
                    model.CreatedUserID = new int?(base.CurrentUser.UserID);
                    Maticsoft.BLL.Settings.AdvertisePosition position2 = new Maticsoft.BLL.Settings.AdvertisePosition();
                    if (position2.Update(model))
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "网络异常，请稍后再试！");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                int advPositionId = Convert.ToInt32(base.Request.Params["id"]);
                this.ShowInfo(advPositionId);
            }
        }

        private void ShowInfo(int AdvPositionId)
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.BLL.Settings.AdvertisePosition().GetModel(AdvPositionId);
            this.lblAdvPositionId.Text = model.AdvPositionId.ToString();
            this.txtAdvPositionName.Text = model.AdvPositionName;
            this.ddlShowType.SelectedValue = model.ShowType.Value.ToString();
            this.txtRepeatColumns.Text = model.RepeatColumns.ToString();
            this.txtWidth.Text = model.Width.ToString();
            this.txtHeight.Text = model.Height.ToString();
            this.txtAdvHtml.Text = model.AdvHtml;
            this.chkIsOne.Checked = model.IsOne;
            this.txtTimeInterval.Text = model.TimeInterval.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x16f;
            }
        }
    }
}

