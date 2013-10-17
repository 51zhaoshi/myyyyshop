namespace Maticsoft.Web.Admin.SNS.Categories
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x7a;
        protected int Act_DelData = 0x7c;
        protected int Act_UpdateData = 0x7b;
        private Categories bll = new Categories();
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal4;
        protected HiddenField txtType;
        public int Type;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.Type = this.type;
                this.Session["CategoryType"] = this.type;
                if (this.type == 1)
                {
                    this.Literal1.Text = "图片分享分类管理";
                    this.Act_DelData = 0x85;
                    this.Act_UpdateData = 0x84;
                    this.Act_AddData = 0x83;
                }
                else
                {
                    this.Literal1.Text = "商品分享分类管理";
                    this.Act_DelData = 0x7c;
                    this.Act_UpdateData = 0x7b;
                    this.Act_AddData = 0x7a;
                }
                this.txtType.Value = this.type.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x79;
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

