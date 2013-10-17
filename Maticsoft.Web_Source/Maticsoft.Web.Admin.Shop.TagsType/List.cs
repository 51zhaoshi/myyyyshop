namespace Maticsoft.Web.Admin.Shop.TagsType
{
    using Maticsoft.BLL.Shop.Tags;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Shop.Tags;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 540;
        protected int Act_DelData = 0x21e;
        protected int Act_UpdateData = 0x21d;
        private Maticsoft.BLL.Shop.Tags.TagCategories bll = new Maticsoft.BLL.Shop.Tags.TagCategories();
        protected Button btnSearch;
        protected GridView gridView;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected ScriptManager ScriptManager1;
        protected TextBox txtKeyword;
        protected UpdatePanel UpdatePanel1;

        public void BindData()
        {
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("Name like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSource = list;
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.DataBind();
        }

        private void DeletePhysicalFile(string path)
        {
            FileHelper.DeleteFile(EnumHelper.AreaType.Shop, path);
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.DataBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            int iD = (int) this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                this.bll.TagCategoriesSequence(iD, SequenceIndex.Down);
            }
            if (e.CommandName == "Rise")
            {
                this.bll.TagCategoriesSequence(iD, SequenceIndex.Up);
            }
            this.BindData();
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
                int num = (int) DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "CategoryName").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    HtmlGenericControl control2 = e.Row.FindControl("spShowImage") as HtmlGenericControl;
                    control2.Visible = false;
                }
                Label label = e.Row.FindControl("lblName") as Label;
                label.Text = str;
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int num;
            DataSet set = this.bll.DeleteTagCategories((int) this.gridView.DataKeys[e.RowIndex].Value, out num);
            if (set != null)
            {
                this.PhysicalFileInfo(set.Tables[0]);
            }
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)))
                {
                    this.liAdd.Visible = false;
                }
                this.BindData();
            }
        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ImageUrl"].ToString());
                    }
                }
            }
        }

        private bool RegURL(string path)
        {
            Regex regex = new Regex("^[a-zA-z]+://(//w+(-//w+)*)(//.(//w+(-//w+)*))*(//?//S*)?$");
            return regex.Match(path).Success;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x21b;
            }
        }
    }
}

