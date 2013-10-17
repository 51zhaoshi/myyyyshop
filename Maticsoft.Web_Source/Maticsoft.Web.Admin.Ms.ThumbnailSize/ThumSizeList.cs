namespace Maticsoft.Web.Admin.Ms.ThumbnailSize
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ThumSizeList : PageBaseAdmin
    {
        protected int Act_AddData = 0x150;
        protected int Act_DelData = 0x152;
        protected int Act_UpdateData = 0x151;
        protected Button btnSave;
        protected Button btnSearch;
        protected CheckBox chkWatermark;
        protected DropDownList ddAreaType;
        protected DropDownList ddlTheme;
        protected DropDownList ddlThumMode;
        protected DropDownList ddlType;
        protected GridViewEx gridView;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal51;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal91;
        protected RangeValidator RangeValidator1;
        protected RangeValidator RangeValidator2;
        protected TextBox tCloudSizeName;
        protected TextBox tDesc;
        protected TextBox tHeight;
        private Maticsoft.BLL.Ms.ThumbnailSize thumBll = new Maticsoft.BLL.Ms.ThumbnailSize();
        protected TextBox tName;
        protected HtmlTableRow tradd;
        protected TextBox tWidth;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[8].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[9].Visible = false;
            }
            DataSet list = new DataSet();
            int num = Globals.SafeInt(this.ddAreaType.SelectedValue, 0);
            string strWhere = "";
            if (num > -1)
            {
                strWhere = " Type=" + num;
            }
            list = this.thumBll.GetList(strWhere);
            if (list != null)
            {
                this.gridView.DataSetSource = list;
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tName.Text))
            {
                MessageBox.ShowSuccessTip(this, "请填写缩略图尺寸名称");
            }
            else if (this.thumBll.Exists(this.tName.Text.Trim()))
            {
                MessageBox.ShowSuccessTip(this, "已存在该缩略图尺寸名称，请重新填写");
            }
            else
            {
                Maticsoft.Model.Ms.ThumbnailSize model = new Maticsoft.Model.Ms.ThumbnailSize {
                    Type = Globals.SafeInt(this.ddlType.SelectedValue, 0),
                    ThumName = this.tName.Text.Trim(),
                    ThumWidth = Globals.SafeInt(this.tWidth.Text, 1),
                    ThumHeight = Globals.SafeInt(this.tHeight.Text, 1),
                    Remark = this.tDesc.Text.Trim(),
                    CloudSizeName = this.tCloudSizeName.Text,
                    CloudType = 0,
                    Theme = this.ddlTheme.SelectedValue.Trim(),
                    ThumMode = Globals.SafeInt(this.ddlThumMode.SelectedValue, 0),
                    IsWatermark = false
                };
                if (this.thumBll.Add(model))
                {
                    base.Response.Redirect("ThumSizeList.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "添加失败！请重试。");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void ddlType_Change(object sender, EventArgs e)
        {
            this.ddlTheme.DataSource = FileHelper.GetThemeList(this.ddlType.SelectedItem.Text);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            this.ddlTheme.DataBind();
            this.ddlTheme.Items.Insert(0, new ListItem("全部", ""));
        }

        protected string GetModeName(object target)
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
                    if (str2 == "2")
                    {
                        return "指定高，宽按比例";
                    }
                    if (str2 == "3")
                    {
                        return "指定高宽缩放（可能变形）";
                    }
                    if (str2 != "4")
                    {
                        return str;
                    }
                    return "指定宽，高按比例";
                }
            }
            else
            {
                return "Auto自动缩放";
            }
            return "指定高宽裁减（不变形）";
        }

        protected string GetTypeName(object target)
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
                    if (str2 == "2")
                    {
                        return "Shop";
                    }
                    if (str2 == "3")
                    {
                        return "Tao";
                    }
                    if (str2 != "4")
                    {
                        return str;
                    }
                    return "COM";
                }
            }
            else
            {
                return "CMS";
            }
            return "SNS";
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
            string thumName = this.gridView.DataKeys[e.RowIndex].Value.ToString();
            this.thumBll.Delete(thumName);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnSave.Visible = false;
                    this.tradd.Visible = false;
                }
                this.ddlTheme.DataSource = FileHelper.GetThemeList("CMS");
                this.ddlTheme.DataTextField = "Name";
                this.ddlTheme.DataValueField = "Name";
                this.ddlTheme.DataBind();
                this.ddlTheme.Items.Insert(0, new ListItem("全部", ""));
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x14f;
            }
        }
    }
}

