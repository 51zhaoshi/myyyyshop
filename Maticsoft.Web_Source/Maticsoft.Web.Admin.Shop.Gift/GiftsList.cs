namespace Maticsoft.Web.Admin.Shop.Gift
{
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class GiftsList : PageBaseAdmin
    {
        protected int Act_AddData = 0x1a7;
        protected int Act_DelData = 0x1a9;
        protected int Act_UpdateData = 0x1a8;
        protected Button btnSearch;
        private Gifts GiftBll = new Gifts();
        protected GridViewEx gridView;
        private const string ImageUrl = "/images/pics/none.gif";
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal4;
        protected Literal Literal5;
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet allList = new DataSet();
            allList = this.GiftBll.GetAllList();
            if (allList != null)
            {
                this.gridView.DataSetSource = allList;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DoCallback()
        {
            string str3;
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            if (((str3 = str) != null) && (str3 == "SetStock"))
            {
                s = this.SetStock();
            }
            base.Response.Write(s);
            base.Response.End();
        }

        public string GetImageUrl(string ThumbnailsUrl)
        {
            if (!string.IsNullOrWhiteSpace(ThumbnailsUrl))
            {
                return ThumbnailsUrl;
            }
            return "/images/pics/none.gif";
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    control.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
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
            int giftId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.GiftBll.Delete(giftId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
            {
                this.liAdd.Visible = false;
            }
            if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)))
                {
                    this.liAdd.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)))
                {
                    this.liAdd.Visible = false;
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
            }
        }

        private string SetStock()
        {
            if (string.IsNullOrWhiteSpace(base.Request.Form["GiftId"]) || string.IsNullOrWhiteSpace(base.Request.Form["Stock"]))
            {
                return "Fail";
            }
            int giftid = Globals.SafeInt(base.Request.Form["GiftId"], 0);
            int stock = Globals.SafeInt(base.Request.Form["Stock"], 0);
            JsonObject obj2 = new JsonObject();
            if (this.GiftBll.UpdateStock(giftid, stock))
            {
                obj2.Put("STATUS", "OK");
            }
            else
            {
                obj2.Put("STATUS", "NODATA");
            }
            return obj2.ToString();
        }

        protected void SetStock(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SetStock")
            {
                Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Convert.ToInt32(e.CommandArgument);
                GridViewRow namingContainer = (GridViewRow) ((LinkButton) e.CommandSource).NamingContainer;
                Convert.ToInt32(this.gridView.Rows[namingContainer.RowIndex].Cells[2].Text);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1a6;
            }
        }
    }
}

