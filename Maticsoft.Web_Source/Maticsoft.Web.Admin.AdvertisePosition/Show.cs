namespace Maticsoft.Web.Admin.AdvertisePosition
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected CheckBox chkIsOne;
        protected HtmlTableRow codeClass;
        protected HtmlTableRow horizontalClass;
        protected Label lblAdvHtml;
        protected Label lblAdvPositionId;
        protected Label lblAdvPositionName;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblHeight;
        protected Label lblIsOne;
        protected Label lblRepeatColumns;
        protected Label lblShowType;
        protected Label lblTimeInterval;
        protected Label lblWidth;
        protected Literal Literal2;
        protected Literal Literal3;
        public string strid = "";
        protected HtmlGenericControl timeintervalClass;
        protected HtmlTableRow verticalClass;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected string ConvertShowType(object obj)
        {
            if (obj != null)
            {
                switch (obj.ToString())
                {
                    case "0":
                        return "纵向平铺";

                    case "1":
                        return "横向平铺";

                    case "2":
                        return "层叠显示";

                    case "3":
                        return "交替显示";

                    case "4":
                        return "自定义广告位";
                }
            }
            return "未定义的显示类型";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int advPositionId = Convert.ToInt32(this.strid);
                this.ShowInfo(advPositionId);
            }
        }

        private void ShowInfo(int AdvPositionId)
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.BLL.Settings.AdvertisePosition().GetModel(AdvPositionId);
            new Users();
            this.lblAdvPositionId.Text = model.AdvPositionId.ToString();
            this.lblAdvPositionName.Text = model.AdvPositionName;
            this.lblShowType.Text = this.ConvertShowType(model.ShowType);
            int valueOrDefault = model.ShowType.GetValueOrDefault();
            if (model.ShowType.HasValue)
            {
                switch (valueOrDefault)
                {
                    case 0:
                    case 2:
                    case 3:
                        this.horizontalClass.Visible = false;
                        this.codeClass.Visible = false;
                        break;

                    case 1:
                        this.codeClass.Visible = false;
                        break;

                    case 4:
                        this.verticalClass.Visible = false;
                        this.horizontalClass.Visible = false;
                        break;
                }
            }
            this.lblRepeatColumns.Text = model.RepeatColumns.ToString();
            this.lblWidth.Text = model.Width.ToString();
            this.lblHeight.Text = model.Height.ToString();
            this.lblAdvHtml.Text = Globals.HtmlEncode(model.AdvHtml);
            this.chkIsOne.Checked = model.IsOne;
            if (!model.IsOne)
            {
                this.timeintervalClass.Visible = false;
            }
            this.lblTimeInterval.Text = model.TimeInterval.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblCreatedUserID.Text = new User().GetUserNameByCache(model.CreatedUserID.Value);
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x170;
            }
        }
    }
}

