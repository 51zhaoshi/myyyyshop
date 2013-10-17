namespace Maticsoft.Web.Admin.Ms.EmailTemplet
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class TempletList : PageBaseAdmin
    {
        protected int Act_AddData = 0x134;
        protected int Act_DelData = 310;
        protected int Act_UpdateData = 0x135;
        private EmailTemplet bll = new EmailTemplet();
        protected GridViewEx gridView;
        protected HtmlGenericControl liadd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal5;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[4].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            DataSet allList = new DataSet();
            allList = this.bll.GetAllList();
            if (allList != null)
            {
                this.gridView.DataSetSource = allList;
            }
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
            int templetId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(templetId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData))) && (base.GetPermidByActID(this.Act_AddData) != -1))
            {
                this.liadd.Visible = false;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x133;
            }
        }
    }
}

