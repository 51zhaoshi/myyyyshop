namespace Maticsoft.Web.Controls
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class UCPermission : UserControlBase
    {
        protected DropDownList droplistPermCategories;
        protected HiddenField HiddenFieldPermID;
        protected Label lblPermName;
        private Permissions perm = new Permissions();
        protected RadioButtonList radbtnlistPermission;
        protected UpdatePanel UpdatePanel1;
        private int userid;

        private void BindCategorylist()
        {
            DataSet allCategories = AccountsTool.GetAllCategories();
            this.droplistPermCategories.DataSource = allCategories.Tables[0];
            this.droplistPermCategories.DataTextField = "Description";
            this.droplistPermCategories.DataValueField = "CategoryID";
            this.droplistPermCategories.DataBind();
        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            if ((this.droplistPermCategories.SelectedItem != null) && (this.droplistPermCategories.SelectedValue.Length > 0))
            {
                string selectedValue = this.droplistPermCategories.SelectedValue;
                this.FillCategoryList(int.Parse(selectedValue));
            }
        }

        public void droplistPermCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.droplistPermCategories.SelectedItem != null) && (this.droplistPermCategories.SelectedValue.Length > 0))
            {
                string selectedValue = this.droplistPermCategories.SelectedValue;
                this.FillCategoryList(int.Parse(selectedValue));
            }
        }

        public void FillCategoryList(int categoryId)
        {
            DataView defaultView = base.UserPrincipal.PermissionLists.Tables[0].DefaultView;
            defaultView.RowFilter = "CategoryID=" + categoryId;
            this.radbtnlistPermission.DataSource = defaultView;
            this.radbtnlistPermission.DataValueField = "PermissionID";
            this.radbtnlistPermission.DataTextField = "Description";
            this.radbtnlistPermission.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindCategorylist();
            }
            else
            {
                if ((this.droplistPermCategories.SelectedItem != null) && (this.droplistPermCategories.SelectedValue.Length > 0))
                {
                    string selectedValue = this.droplistPermCategories.SelectedValue;
                    this.FillCategoryList(int.Parse(selectedValue));
                }
                if (this.HiddenFieldPermID.Value.Length > 0)
                {
                    if (this.HiddenFieldPermID.Value != "undefined")
                    {
                        this.perm.GetPermissionDetails(int.Parse(this.HiddenFieldPermID.Value));
                        this.lblPermName.Text = this.perm.Description;
                    }
                    if (this.droplistPermCategories.SelectedValue == this.perm.CategoryID.ToString())
                    {
                        this.radbtnlistPermission.SelectedValue = this.HiddenFieldPermID.Value;
                    }
                }
            }
        }

        public int PermissionID
        {
            get
            {
                if (this.HiddenFieldPermID.Value.Length > 0)
                {
                    return Convert.ToInt32(this.HiddenFieldPermID.Value);
                }
                return 0;
            }
            set
            {
                if (value > 0)
                {
                    this.HiddenFieldPermID.Value = value.ToString();
                    this.perm.GetPermissionDetails(value);
                    this.lblPermName.Text = this.perm.Description;
                    if (this.droplistPermCategories.Items.FindByValue(this.perm.CategoryID.ToString()) != null)
                    {
                        this.droplistPermCategories.SelectedValue = this.perm.CategoryID.ToString();
                        this.FillCategoryList(this.perm.CategoryID);
                        if (this.radbtnlistPermission.Items.FindByValue(value.ToString()) != null)
                        {
                            this.radbtnlistPermission.SelectedValue = value.ToString();
                        }
                    }
                }
            }
        }

        public int UserID
        {
            set
            {
                this.userid = value;
            }
        }
    }
}

