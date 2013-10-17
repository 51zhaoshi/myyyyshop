namespace Maticsoft.Web.Settings.MainMenus
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsUsed;
        protected DropDownList ddlTarget;
        protected DropDownList ddlTheme;
        protected DropDownList ddlType;
        protected DropDownList ddNavType;
        protected DropDownList ddValue;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected TextBox txtMenuName;
        protected TextBox txtNavURL;
        protected TextBox txtSqueeze;
        protected TextBox txtTile;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtMenuName.Text))
            {
                MessageBox.ShowFailTip(this, CMS.WMErrorMenuName);
            }
            else if (string.IsNullOrWhiteSpace(this.txtSqueeze.Text) || !PageValidate.IsNumber(this.txtSqueeze.Text))
            {
                MessageBox.ShowFailTip(this, CMS.WMErrorSqueeze);
            }
            else
            {
                string text = this.txtMenuName.Text;
                string text1 = this.txtNavURL.Text;
                int num = int.Parse(this.ddlTarget.SelectedValue);
                bool flag = this.chkIsUsed.Checked;
                Maticsoft.Model.Settings.MainMenus model = new Maticsoft.Model.Settings.MainMenus {
                    NavArea = Globals.SafeInt(this.ddlType.SelectedValue, 0),
                    URLType = Globals.SafeInt(this.ddNavType.SelectedValue, 0)
                };
                switch (model.URLType)
                {
                    case 0:
                        model.NavURL = this.txtNavURL.Text.Trim();
                        break;

                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        model.NavURL = this.ddValue.SelectedValue;
                        break;

                    default:
                        model.NavURL = this.txtNavURL.Text.Trim();
                        break;
                }
                if (string.IsNullOrWhiteSpace(model.NavURL))
                {
                    MessageBox.ShowFailTip(this, CMS.WMErrorPageUrl);
                }
                else
                {
                    model.MenuName = text;
                    model.MenuType = 1;
                    model.Target = new int?(num);
                    model.IsUsed = flag;
                    model.MenuTitle = this.txtTile.Text;
                    model.Sequence = int.Parse(this.txtSqueeze.Text);
                    model.NavTheme = this.ddlTheme.SelectedValue;
                    Maticsoft.BLL.Settings.MainMenus menus2 = new Maticsoft.BLL.Settings.MainMenus();
                    if (menus2.Add(model) > 0)
                    {
                        this.btnSave.Enabled = false;
                        this.btnCancle.Enabled = false;
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "list.aspx");
                    }
                    else
                    {
                        this.btnSave.Enabled = false;
                        this.btnCancle.Enabled = false;
                        MessageBox.ShowFailTip(this, Site.TooltipTryAgainLater, "list.aspx");
                    }
                }
            }
        }

        protected void ddlType_Change(object sender, EventArgs e)
        {
            this.ddlTheme.DataSource = FileHelper.GetThemeList(this.ddlType.SelectedItem.Text);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            this.ddlTheme.DataBind();
            this.ddlTheme.Items.Insert(0, new ListItem("全部", ""));
            this.ddValue.Visible = false;
            this.ddNavType.SelectedValue = "0";
            this.txtNavURL.Visible = true;
        }

        protected void ddNavType_Change(object sender, EventArgs e)
        {
            switch (Globals.SafeInt(this.ddNavType.SelectedValue, 0))
            {
                case 0:
                    this.txtNavURL.Visible = true;
                    this.ddValue.Visible = false;
                    return;

                case 1:
                    this.ddValue.DataSource = new ContentClass().GetList(" ParentId=0");
                    this.ddValue.DataTextField = "ClassName";
                    this.ddValue.DataValueField = "ClassID";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    return;

                case 2:
                    this.ddValue.DataSource = new Categories().GetList(" ParentId=0 and type=0 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    return;

                case 3:
                    this.ddValue.DataSource = new Categories().GetList(" ParentId=0 and type=1 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    return;

                case 4:
                    this.ddValue.DataSource = new CategoryInfo().GetList(" ParentCategoryId=0 ");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.Visible = true;
                    return;
            }
            this.ddValue.Visible = false;
            this.txtNavURL.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                }
                else
                {
                    this.ddlTheme.DataSource = FileHelper.GetThemeList("CMS");
                    this.ddlTheme.DataTextField = "Name";
                    this.ddlTheme.DataValueField = "Name";
                    this.ddlTheme.DataBind();
                    this.ddlTheme.Items.Insert(0, new ListItem("全部", ""));
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x183;
            }
        }
    }
}

