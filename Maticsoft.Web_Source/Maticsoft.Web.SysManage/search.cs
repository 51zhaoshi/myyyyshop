namespace Maticsoft.Web.SysManage
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class search : PageBaseAdmin
    {
        protected Button btnCancel;
        protected Button btnSearch;
        protected copyright CopyRight1;
        protected HtmlForm Form1;
        protected DropDownList listPermission;
        protected DropDownList listTarget;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected TextBox txtDescription;
        protected TextBox txtID;
        protected TextBox txtName;

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = row["NodeID"].ToString();
                string text = row["TreeText"].ToString();
                row["PermissionID"].ToString();
                text = blank + "『" + text + "』";
                this.listTarget.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BiudPermTree()
        {
            DataTable table = AccountsTool.GetAllCategories().Tables[0];
            int count = table.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string str = table.Rows[i]["CategoryID"].ToString();
                string text = table.Rows[i]["Description"].ToString();
                text = "╋" + text;
                this.listPermission.Items.Add(new ListItem(text, str));
                DataTable table2 = AccountsTool.GetPermissionsByCategory(int.Parse(str)).Tables[0];
                int num3 = table2.Rows.Count;
                for (int j = 0; j < num3; j++)
                {
                    string str3 = table2.Rows[j]["PermissionID"].ToString();
                    string str4 = table2.Rows[j]["Description"].ToString();
                    str4 = "  ├『" + str4 + "』";
                    this.listPermission.Items.Add(new ListItem(str4, str3));
                }
            }
            this.listPermission.DataBind();
            this.listPermission.Items.Insert(0, Site.PleaseSelect);
        }

        private void BiudTree()
        {
            SysTree tree = new SysTree();
            DataTable dt = tree.GetTreeList("").Tables[0];
            this.listTarget.Items.Clear();
            this.listTarget.Items.Add(new ListItem(Site.lblRootDirectory, "0"));
            foreach (DataRow row in dt.Select("ParentID= " + 0))
            {
                string str = row["NodeID"].ToString();
                string text = row["TreeText"].ToString();
                row["ParentID"].ToString();
                row["PermissionID"].ToString();
                text = "╋" + text;
                this.listTarget.Items.Add(new ListItem(text, str));
                int parentid = int.Parse(str);
                string blank = "├";
                this.BindNode(parentid, dt, blank);
            }
            this.listTarget.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtID.Text = "";
            this.txtName.Text = "";
            this.txtDescription.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string s = PageValidate.InputText(this.txtID.Text, 10);
            string str2 = PageValidate.InputText(this.txtName.Text, 10);
            string str3 = PageValidate.InputText(this.txtDescription.Text, 100);
            string selectedValue = this.listTarget.SelectedValue;
            string str5 = " (1=1) ";
            if (this.listPermission.SelectedItem.Text.StartsWith("╋"))
            {
                base.Response.Write("<script defer> window.alert('" + SysManage.ErrorPermission + "');</script>");
            }
            else
            {
                string str6 = "-1";
                if (this.listPermission.SelectedIndex > 0)
                {
                    str6 = this.listPermission.SelectedValue;
                }
                if (s != "")
                {
                    try
                    {
                        int.Parse(s);
                    }
                    catch
                    {
                        base.Response.Write("<script defer> window.alert('" + SysManage.ErrorIDFormalError + "');</script>");
                        return;
                    }
                    str5 = str5 + " and (NodeID=" + s + ")";
                }
                if (str2 != "")
                {
                    str5 = str5 + " and (TreeText like'%" + str2 + "%')";
                }
                if (selectedValue != "-1")
                {
                    str5 = str5 + " and (ParentID=" + selectedValue + ")";
                }
                if (str6 != "-1")
                {
                    str5 = str5 + " and (PermissionID=" + str6 + ")";
                }
                if (str3 != "")
                {
                    str5 = str5 + " and (comment like'%" + str3 + "%')";
                }
                if (str5 != "")
                {
                    this.Session["strWheresys"] = str5;
                }
                else
                {
                    this.Session["strWheresys"] = "";
                }
                base.Response.Redirect("treelist.aspx?page=1");
            }
        }

        private void InitializeComponent()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BiudTree();
                this.BiudPermTree();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd6;
            }
        }
    }
}

