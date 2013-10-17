namespace Maticsoft.Web.Ms.EntryForm
{
    using Maticsoft.BLL.Ms;
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
        protected int Act_DeleteList = 0x57;
        protected int Act_UpdateData = 0x58;
        private EntryForm bll = new EntryForm();
        protected Button btnBatchManage;
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList dropState;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal6;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[13].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat(" UserName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            if (this.dropState.SelectedValue != "")
            {
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND State={0} ", this.dropState.SelectedValue);
                }
                else
                {
                    builder.AppendFormat(" State={0} ", this.dropState.SelectedValue);
                }
            }
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnBatchManage_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.bll.UpdateList(selIDlist, " State= 1"))
            {
                MessageBox.ShowSuccessTip(this, "批量处理成功！");
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetRegionNameByRID(object target)
        {
            Regions regions = new Regions();
            string regionNameByRID = "";
            if ((target != null) && ("" != target.ToString()))
            {
                string inputData = target.ToString();
                if (PageValidate.IsNumber(inputData))
                {
                    regionNameByRID = regions.GetRegionNameByRID(int.Parse(inputData));
                }
            }
            return regionNameByRID;
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

        public string GetSex(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString().Trim()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    return str;
                }
            }
            else
            {
                return Site.SexMale;
            }
            return Site.SexWoman;
        }

        public string GetState(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    return str;
                }
            }
            else
            {
                return Site.Untreated;
            }
            return Site.Processed;
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
            int id = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(id);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
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
                return 0x56;
            }
        }
    }
}

