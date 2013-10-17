namespace Maticsoft.Web.Admin.AdvertisePosition
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsOne;
        protected DropDownList ddlShowType;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.Model.Settings.AdvertisePosition();
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
                model.ShowType = new int?(Globals.SafeInt(selectedValue, -1));
                model.AdvPositionName = this.txtAdvPositionName.Text.Trim();
                model.CreatedDate = new DateTime?(DateTime.Now);
                model.CreatedUserID = new int?(base.CurrentUser.UserID);
                Maticsoft.BLL.Settings.AdvertisePosition position2 = new Maticsoft.BLL.Settings.AdvertisePosition();
                int num = 0;
                if (selectedValue == "4")
                {
                    num = position2.Add(model);
                    Maticsoft.BLL.Settings.Advertisement advertisement = new Maticsoft.BLL.Settings.Advertisement();
                    Maticsoft.Model.Settings.Advertisement advertisement2 = new Maticsoft.Model.Settings.Advertisement {
                        AdvertisementName = "自定义广告代码",
                        ContentType = 3,
                        AdvPositionId = new int?(num),
                        CreatedDate = new DateTime?(DateTime.Now)
                    };
                    advertisement.Add(advertisement2);
                }
                else
                {
                    num = position2.Add(model);
                }
                if (num > 0)
                {
                    MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "网络异常，请稍后再试");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x16e;
            }
        }
    }
}

