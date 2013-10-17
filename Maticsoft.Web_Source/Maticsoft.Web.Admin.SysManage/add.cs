namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox CheckBox1;
        protected CheckBox chkAddContinue;
        protected CheckBox chkEnable;
        protected DropDownList drpTreeType;
        protected HtmlInputHidden hideimgurl;
        protected HtmlSelect imgsel;
        protected HtmlImage imgview;
        protected Label lblMsg;
        protected DropDownList listTarget;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected TextBox txtDescription;
        protected TextBox txtImgUrl;
        protected TextBox txtName;
        protected TextBox txtOrderid;
        protected TextBox txtUrl;
        protected UCDroplistPermission UCDroplistPermission1;

        private void BindImages()
        {
            DirectoryInfo info = new DirectoryInfo(base.Server.MapPath("/admin/Images/MenuImg"));
            FileInfo[] files = info.GetFiles("*.gif");
            this.imgsel.Items.Clear();
            foreach (FileInfo info2 in files)
            {
                ListItem item = new ListItem(info2.Name, "/admin/Images/MenuImg/" + info2.Name);
                this.imgsel.Items.Add(item);
            }
            foreach (FileInfo info3 in info.GetFiles("*.jpg"))
            {
                ListItem item2 = new ListItem(info3.Name, "/admin/Images/MenuImg/" + info3.Name);
                this.imgsel.Items.Add(item2);
            }
            this.imgsel.Items.Insert(0, Site.lblDefaultIcon);
            this.imgsel.DataBind();
        }

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = row["NodeID"].ToString();
                string text = row["TreeText"].ToString();
                text = blank + "『" + text + "』";
                this.listTarget.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BiudPermTree()
        {
        }

        private void BiudTree(int treeType)
        {
            DataTable table;
            SysTree tree = new SysTree();
            if (treeType > -1)
            {
                table = tree.GetTreeList("TreeType = " + treeType).Tables[0];
            }
            else
            {
                table = tree.GetTreeList("").Tables[0];
            }
            this.listTarget.Items.Clear();
            this.listTarget.Items.Add(new ListItem(Site.lblRootDirectory, "0"));
            foreach (DataRow row in table.Select("ParentID= " + 0))
            {
                string str = row["NodeID"].ToString();
                string text = row["TreeText"].ToString();
                text = "╋" + text;
                this.listTarget.Items.Add(new ListItem(text, str));
                int parentid = int.Parse(str);
                string blank = "├";
                this.BindNode(parentid, table, blank);
            }
            this.listTarget.DataBind();
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            this.txtOrderid.Text = "";
            this.txtName.Text = "";
            this.txtUrl.Text = "";
            this.txtImgUrl.Text = "";
            this.txtDescription.Text = "";
            this.chkAddContinue.Checked = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string s = PageValidate.InputText(this.txtOrderid.Text, 10);
            string text = this.txtName.Text;
            string str3 = PageValidate.InputText(this.txtUrl.Text, 100);
            string str4 = this.hideimgurl.Value;
            int num = int.Parse(this.listTarget.SelectedValue);
            string msg = "";
            if (s.Trim() == "")
            {
                msg = msg + SysManage.ErrorIDNotNull + @"\n";
            }
            try
            {
                int.Parse(s);
            }
            catch
            {
                msg = msg + SysManage.ErrorIDFormalError + @"\n";
            }
            if (text.Trim() == "")
            {
                msg = msg + SysManage.ErrorNameNotNull + @"\n";
            }
            if (msg != "")
            {
                MessageBox.ShowSuccessTip(this, msg);
            }
            else
            {
                int permissionID = -1;
                if (this.UCDroplistPermission1.PermissionID > 0)
                {
                    permissionID = this.UCDroplistPermission1.PermissionID;
                }
                int num3 = -1;
                int num4 = -1;
                string str7 = "false";
                string str8 = PageValidate.InputText(this.txtDescription.Text, 100);
                SysNode node = new SysNode {
                    TreeText = text,
                    ParentID = num,
                    Location = num + "." + s,
                    OrderID = new int?(int.Parse(s)),
                    Comment = str8,
                    Url = str3.Replace(@"\", "/"),
                    PermissionID = permissionID,
                    ImageUrl = str4,
                    ModuleID = new int?(num3),
                    KeShiDM = new int?(num4),
                    KeshiPublic = str7,
                    TreeType = Globals.SafeInt(this.drpTreeType.SelectedValue, 0),
                    Enabled = this.chkEnable.Checked
                };
                SysTree tree = new SysTree();
                if (this.CheckBox1.Checked)
                {
                    Permissions permissions = new Permissions();
                    string treeText = node.TreeText;
                    int parentID = node.ParentID;
                    if (parentID == 0)
                    {
                        MessageBox.ShowSuccessTip(this.Page, SysManage.ErrorCheckedCheckBox1);
                        return;
                    }
                    SysNode node2 = new SysNode();
                    node2 = tree.GetNode(parentID);
                    int permissionCatalogID = tree.GetPermissionCatalogID(node2.PermissionID);
                    int num7 = permissions.Create(permissionCatalogID, treeText);
                    node.PermissionID = num7;
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加菜单：【{0}】", this.txtName.Text), this);
                tree.AddTreeNode(node);
                this.lblMsg.Text = Site.TooltipSaveOK;
                if (this.chkAddContinue.Checked)
                {
                    this.txtOrderid.Text = "";
                    this.txtName.Text = "";
                    this.txtUrl.Text = "";
                    this.txtImgUrl.Text = "";
                    this.txtDescription.Text = "";
                }
                else
                {
                    base.Response.Redirect("treelist.aspx?TreeType=" + this.TreeType);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.TreeType >= 0))
            {
                this.BiudTree(this.TreeType);
                this.BindImages();
                this.drpTreeType.SelectedValue = this.TreeType.ToString();
                this.drpTreeType.Enabled = false;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd1;
            }
        }

        public int TreeType
        {
            get
            {
                return Globals.SafeInt(base.Request.Params["TreeType"], -1);
            }
        }
    }
}

