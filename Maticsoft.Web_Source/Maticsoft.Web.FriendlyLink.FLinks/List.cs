namespace Maticsoft.Web.FriendlyLink.FLinks
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x17f;
        protected int Act_DelData = 0x181;
        protected int Act_UpdateData = 0x180;
        private FriendlyLink bll = new FriendlyLink();
        protected Button btnApproveList;
        protected Button btnDelete;
        protected Button btnInverseApprove;
        protected Button btnSearch;
        protected DropDownList DropState;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[12].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[13].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            string selectedValue = this.DropState.SelectedValue;
            if ("" != this.DropState.SelectedValue)
            {
                builder.AppendFormat("Name like '%{0}%' and state=" + selectedValue, this.txtKeyword.Text.Trim());
            }
            else if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("Name like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            if (builder.Length > 0)
            {
                builder.Append(" and ");
            }
            builder.Append(" 1=1 order by OrderID ");
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnApproveList_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string strWhere = "State=" + 1;
                if (this.bll.UpdateList(selIDlist, strWhere))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveError);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveError);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string strWhere = "State=" + 0;
                if (this.bll.UpdateList(selIDlist, strWhere))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveError);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string fsState(object data)
        {
            string str = "";
            if (data == null)
            {
                return str;
            }
            switch (Convert.ToInt32(data))
            {
                case 0:
                    return Site.Unaudited;

                case 1:
                    return Site.btnApproveText;
            }
            return Site.Unknown;
        }

        public string fsType(object data)
        {
            string str = "";
            if (data == null)
            {
                return str;
            }
            switch (Convert.ToInt32(data))
            {
                case 0:
                    return SiteSetting.lblImgLink;

                case 1:
                    return SiteSetting.lblTextLink;
            }
            return Site.Unknown;
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
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
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(iD);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
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
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x17b;
            }
        }
    }
}

