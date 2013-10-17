namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ImageTransitionEffects : UserControl
    {
        private string _adPositionID;
        private string _contentType;
        private string _direction;
        private Advertisement bll = new Advertisement();
        protected Repeater rp_FlashShow;
        protected Repeater rp_HtmlCode;
        protected Repeater rp_TextShow;
        protected Repeater rpTransitionEffects;
        public string strHeight = "0";
        public string strWidth = "0";

        private void BoundData(int PositionID)
        {
            int contentType = Globals.SafeInt(this.ContentType, -1);
            DataSet set = this.bll.GetTransitionImg(PositionID, contentType, null);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                this.strWidth = set.Tables[0].Rows[0]["Width"].ToString();
                this.strHeight = set.Tables[0].Rows[0]["Height"].ToString();
                this._direction = set.Tables[0].Rows[0]["ShowType"].ToString();
                switch (int.Parse(this.ContentType))
                {
                    case 0:
                        this.rp_TextShow.DataSource = set;
                        this.rp_TextShow.DataBind();
                        return;

                    case 1:
                        this.rpTransitionEffects.DataSource = set;
                        this.rpTransitionEffects.DataBind();
                        return;

                    case 2:
                        this.rp_FlashShow.DataSource = set;
                        this.rp_FlashShow.DataBind();
                        return;

                    case 3:
                        this.rp_HtmlCode.DataSource = set;
                        this.rp_HtmlCode.DataBind();
                        return;

                    default:
                        return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BoundData(int.Parse(this.AdPositionID));
            }
        }

        public string AdPositionID
        {
            get
            {
                return this._adPositionID;
            }
            set
            {
                this._adPositionID = value;
            }
        }

        public string ContentType
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this._contentType))
                {
                    return this._contentType;
                }
                return "-1";
            }
            set
            {
                this._contentType = value;
            }
        }

        public string Direction
        {
            get
            {
                switch (this._direction)
                {
                    case "0":
                        return "0";

                    case "1":
                        return "1";

                    case "2":
                        return "4";

                    case "3":
                        return "-1";

                    case "4":
                        return "2";
                }
                return this._direction;
            }
            set
            {
                this._direction = value;
            }
        }
    }
}

