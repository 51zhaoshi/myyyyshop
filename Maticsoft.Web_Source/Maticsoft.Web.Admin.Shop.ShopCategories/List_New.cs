namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.HtmlControls;

    public class List_New : PageBaseAdmin
    {
        protected int Act_AddData = 0x20d;
        protected copyright Copyright1;
        protected HtmlGenericControl liAdd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
            {
                this.liAdd.Visible = false;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x20c;
            }
        }
    }
}

