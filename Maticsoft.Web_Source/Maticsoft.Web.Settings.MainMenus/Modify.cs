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

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsUsed;
        protected DropDownList ddlTarget;
        protected DropDownList ddlTheme;
        protected DropDownList ddlType;
        protected DropDownList ddNavType;
        protected DropDownList ddValue;
        protected HiddenField HiddenField_Type;
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

        public void btnSave_Click(object sender, EventArgs e)
        {
            if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
            {
                Maticsoft.BLL.Settings.MainMenus menus = new Maticsoft.BLL.Settings.MainMenus();
                int menuID = Convert.ToInt32(base.Request.Params["id"]);
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
                    bool flag = this.chkIsUsed.Checked;
                    Maticsoft.Model.Settings.MainMenus model = menus.GetModel(menuID);
                    model.MenuName = text;
                    model.MenuType = new int?(int.Parse(this.HiddenField_Type.Value));
                    model.Target = new int?(int.Parse(this.ddlTarget.SelectedValue));
                    model.IsUsed = flag;
                    model.MenuTitle = this.txtTile.Text;
                    model.Sequence = int.Parse(this.txtSqueeze.Text);
                    model.NavTheme = this.ddlTheme.SelectedValue;
                    model.NavArea = Globals.SafeInt(this.ddlType.SelectedValue, 0);
                    model.URLType = Globals.SafeInt(this.ddNavType.SelectedValue, 0);
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
                    else if (menus.Update(model))
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
                    this.ddValue.DataSource = new Categories().GetList(" ParentId=0 and type=2 and Status=1");
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
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                int menuID = Convert.ToInt32(base.Request.Params["id"]);
                this.ShowInfo(menuID);
            }
        }

        private void ShowInfo(int MenuID)
        {
            Maticsoft.Model.Settings.MainMenus model = new Maticsoft.BLL.Settings.MainMenus().GetModel(MenuID);
            this.txtMenuName.Text = model.MenuName;
            this.ddlTarget.SelectedValue = model.Target.ToString();
            this.chkIsUsed.Checked = model.IsUsed;
            this.HiddenField_Type.Value = model.MenuType.ToString();
            this.txtTile.Text = model.MenuTitle;
            this.txtSqueeze.Text = model.Sequence.ToString();
            this.ddlType.SelectedValue = model.NavArea.ToString();
            this.ddNavType.SelectedValue = model.URLType.ToString();
            string areaName = "CMS";
            switch (model.NavArea)
            {
                case 0:
                    areaName = "CMS";
                    break;

                case 1:
                    areaName = "SNS";
                    break;

                case 2:
                    areaName = "Shop";
                    break;

                default:
                    areaName = "CMS";
                    break;
            }
            this.ddlTheme.DataSource = FileHelper.GetThemeList(areaName);
            this.ddlTheme.DataTextField = "Name";
            this.ddlTheme.DataValueField = "Name";
            this.ddlTheme.DataBind();
            this.ddlTheme.Items.Insert(0, new ListItem("全部", ""));
            this.ddlTheme.SelectedValue = model.NavTheme;
            switch (model.URLType)
            {
                case 0:
                    this.txtNavURL.Visible = true;
                    this.txtNavURL.Text = model.NavURL;
                    return;

                case 1:
                    this.ddValue.DataSource = new ContentClass().GetList(" ParentId=0");
                    this.ddValue.DataTextField = "ClassName";
                    this.ddValue.DataValueField = "ClassID";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    return;

                case 2:
                    this.ddValue.DataSource = new Categories().GetList(" ParentId=0 and type=0 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    return;

                case 3:
                    this.ddValue.DataSource = new Categories().GetList(" ParentId=0 and type=1 and Status=1");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    return;

                case 4:
                    this.ddValue.DataSource = new CategoryInfo().GetList(" ParentCategoryId=0 ");
                    this.ddValue.DataTextField = "Name";
                    this.ddValue.DataValueField = "CategoryId";
                    this.ddValue.DataBind();
                    this.ddValue.Items.Insert(0, new ListItem("请选择", ""));
                    this.ddValue.SelectedValue = model.NavURL;
                    this.ddValue.Visible = true;
                    return;
            }
            this.txtNavURL.Visible = true;
            this.txtNavURL.Text = model.NavURL;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x184;
            }
        }
    }
}

