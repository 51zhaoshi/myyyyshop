namespace Maticsoft.Web.Admin.SNS.Products
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class AddColor : PageBaseAdmin
    {
        protected Button btnSave;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HiddenField hidValue;
        protected HtmlGenericControl lblTip;
        protected Literal Literal1;
        protected Literal Literal2;
        protected string strColorValue;
        protected StringBuilder strSelectValue = new StringBuilder();

        private void BindColor()
        {
            int num = Globals.SafeInt(this.Id, 0);
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            Maticsoft.Model.SNS.Products model = new Maticsoft.Model.SNS.Products();
            model = products.GetModel((long) num);
            if (model != null)
            {
                this.hidValue.Value = model.Color;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int num = Globals.SafeInt(this.Id, 0);
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            Maticsoft.Model.SNS.Products model = new Maticsoft.Model.SNS.Products();
            model = products.GetModel((long) num);
            if (model != null)
            {
                model.Color = this.hidValue.Value;
                products.Update(model);
                this.lblTip.Visible = true;
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新商品（ProductId=" + num + "）的颜色成功", this);
            }
            this.BindColor();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindColor();
            }
        }

        public string Id
        {
            get
            {
                return base.Request.QueryString["Id"];
            }
        }
    }
}

