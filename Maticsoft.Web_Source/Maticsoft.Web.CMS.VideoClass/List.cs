namespace Maticsoft.Web.CMS.VideoClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x10a;
        protected int Act_DelData = 0x10c;
        protected int Act_UpdateData = 0x10b;
        private Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
        protected Button btnSearch;
        protected GridView gridView;
        protected HtmlGenericControl liAdd;
        protected Literal Literal11;
        protected Literal Literal6;
        protected Literal ltlAdd;
        protected Literal ltlCategory;
        protected Literal ltlList;
        protected Literal ltlTip;
        protected VideoClassDropList VideoClassDropList1;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[3].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[4].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            string selectedValue = this.VideoClassDropList1.SelectedValue;
            if (((selectedValue != "") && (selectedValue != "0")) && PageValidate.IsNumber(selectedValue))
            {
                string path = "";
                Maticsoft.Model.CMS.VideoClass model = this.bll.GetModel(int.Parse(selectedValue));
                if (model != null)
                {
                    path = model.Path;
                }
                builder.Append(" VideoClassID=" + this.VideoClassDropList1.SelectedValue + " OR Path LIKE '" + path + "|%'");
            }
            DataSet listEx = this.bll.GetListEx(builder.ToString(), " Sequence ASC ");
            this.gridView.DataSource = listEx;
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        public string GetVideoClassNameByParentID(object target)
        {
            Maticsoft.BLL.CMS.VideoClass class2 = new Maticsoft.BLL.CMS.VideoClass();
            string videoClassName = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string inputData = target.ToString();
                if (PageValidate.IsNumber(inputData))
                {
                    Maticsoft.Model.CMS.VideoClass modelByParentID = class2.GetModelByParentID(int.Parse(inputData));
                    if (modelByParentID != null)
                    {
                        videoClassName = modelByParentID.VideoClassName;
                    }
                }
            }
            return videoClassName;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            int videoClassId = (int) this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                this.bll.SwapCategorySequence(videoClassId, SwapSequenceIndex.Down);
            }
            if (e.CommandName == "Rise")
            {
                this.bll.SwapCategorySequence(videoClassId, SwapSequenceIndex.Up);
            }
            this.BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int num = (int) DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "VideoClassName").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    HtmlGenericControl control = e.Row.FindControl("spShowImage") as HtmlGenericControl;
                    control.Visible = false;
                }
                Label label = e.Row.FindControl("lblVideoClassName") as Label;
                label.Text = str;
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int videoClassID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.DeleteEx(videoClassID);
            this.LoadVideoClassData();
            this.BindData();
        }

        protected void LoadVideoClassData()
        {
            this.bll.GetListEx(" ParentID=0 ", "");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                this.LoadVideoClassData();
                this.BindData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x106;
            }
        }
    }
}

