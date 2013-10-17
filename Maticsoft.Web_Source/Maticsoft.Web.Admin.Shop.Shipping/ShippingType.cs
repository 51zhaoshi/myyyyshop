namespace Maticsoft.Web.Admin.Shop.Shipping
{
    using Maticsoft.BLL.Shop.Shipping;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ShippingType : PageBaseAdmin
    {
        protected int Act_AddData = 0x206;
        protected int Act_DelData = 520;
        protected int Act_UpdateData = 0x207;
        private Maticsoft.BLL.Shop.Shipping.ShippingType bll = new Maticsoft.BLL.Shop.Shipping.ShippingType();
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;

        public void BindData()
        {
            DataSet allList = new DataSet();
            allList = this.bll.GetAllList();
            if (allList != null)
            {
                this.gridView.DataSetSource = allList;
            }
        }

        protected string GetModeName(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (target.ToString())
            {
                case "1":
                    return "下拉列表";

                case "2":
                    return "单选按钮";
            }
            return "下拉列表";
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    control.Visible = false;
                }
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int modeId = Globals.SafeInt(this.gridView.DataKeys[e.RowIndex].Value.ToString(), 0);
            this.bll.Delete(modeId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData))) && (base.GetPermidByActID(this.Act_AddData) != -1))
            {
                this.liAdd.Visible = false;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x205;
            }
        }
    }
}

