namespace Maticsoft.Web.Admin.SysManage
{
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

    public class modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SysManage.MultiLanguage bllML = new Maticsoft.BLL.SysManage.MultiLanguage();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkEnable;
        protected DropDownList drpTreeType;
        protected HtmlInputHidden hideimgurl;
        public string id = "";
        protected HtmlSelect imgsel;
        protected HtmlImage imgview;
        protected Label lblID;
        protected Label lblMsg;
        protected DropDownList listTarget;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected TextBox txtDescription;
        protected TextBox txtOrderid;
        protected TextBox txtTreeText;
        protected TextBox txtUrl;
        protected UCDroplistPermission UCDroplistPermission1;

        private void BindImages()
        {
            DirectoryInfo info = new DirectoryInfo(base.Server.MapPath("../Images/MenuImg"));
            FileInfo[] files = info.GetFiles("*.gif");
            this.imgsel.Items.Clear();
            foreach (FileInfo info2 in files)
            {
                ListItem item = new ListItem(info2.Name, "Images/MenuImg/" + info2.Name);
                this.imgsel.Items.Add(item);
            }
            foreach (FileInfo info3 in info.GetFiles("*.jpg"))
            {
                ListItem item2 = new ListItem(info3.Name, "Images/MenuImg/" + info3.Name);
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
            this.lblID.Text = "";
            this.txtTreeText.Text = "";
            this.txtUrl.Text = "";
            this.txtDescription.Text = "";
            base.Response.Redirect(string.Concat(new object[] { "show.aspx?id=", this.id, "&TreeType=", this.TreeType }));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string text = PageValidate.InputText(this.lblID.Text, 10);
            string s = PageValidate.InputText(this.txtOrderid.Text, 5);
            string str3 = this.txtTreeText.Text;
            string str4 = PageValidate.InputText(this.txtUrl.Text, 100);
            string str5 = this.hideimgurl.Value;
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
            if (str3.Trim() == "")
            {
                msg = msg + SysManage.ErrorNameNotNull + @"\n";
            }
            if (msg != "")
            {
                MessageBox.ShowFailTip(this, msg);
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
                string str8 = "false";
                string str9 = PageValidate.InputText(this.txtDescription.Text, 100);
                SysTree tree = new SysTree();
                SysNode node = tree.GetNode(Globals.SafeInt(text, 0));
                node.OrderID = new int?(int.Parse(s));
                node.TreeText = str3;
                node.ParentID = num;
                node.Location = num + "." + s;
                node.Comment = str9;
                node.Url = str4.Replace(@"\", "/");
                node.PermissionID = permissionID;
                node.ImageUrl = str5;
                node.ModuleID = new int?(num3);
                node.KeShiDM = new int?(num4);
                node.KeshiPublic = str8;
                node.TreeType = Globals.SafeInt(this.drpTreeType.SelectedValue, 0);
                node.Enabled = this.chkEnable.Checked;
                tree.UpdateNode(node);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑菜单：【{0}】", this.txtTreeText.Text), this);
                base.Response.Redirect(string.Concat(new object[] { "show.aspx?id=", text, "&TreeType=", this.TreeType }));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BiudTree(this.TreeType);
                this.drpTreeType.SelectedValue = this.TreeType.ToString();
                this.drpTreeType.Enabled = false;
                this.BindImages();
                if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].ToString() != ""))
                {
                    this.id = base.Request.Params["id"];
                    if ((this.id == null) || (this.id.Trim() == ""))
                    {
                        base.Response.Redirect("treelist.aspx?TreeType=" + this.TreeType);
                        base.Response.End();
                    }
                    else
                    {
                        this.ShowInfo(this.id);
                    }
                }
            }
        }

        private void ShowInfo(string id)
        {
            SysNode node = new SysTree().GetNode(int.Parse(id));
            this.lblID.Text = id;
            this.txtOrderid.Text = node.OrderID.ToString();
            this.txtTreeText.Text = node.TreeText;
            if (node.ParentID == 0)
            {
                this.listTarget.SelectedIndex = 0;
            }
            else
            {
                for (int j = 0; j < this.listTarget.Items.Count; j++)
                {
                    if (this.listTarget.Items[j].Value == node.ParentID.ToString())
                    {
                        this.listTarget.Items[j].Selected = true;
                    }
                }
            }
            this.txtUrl.Text = node.Url;
            this.txtDescription.Text = node.Comment;
            this.UCDroplistPermission1.PermissionID = node.PermissionID;
            for (int i = 0; i < this.imgsel.Items.Count; i++)
            {
                if (this.imgsel.Items[i].Value == node.ImageUrl)
                {
                    this.imgsel.Items[i].Selected = true;
                    this.hideimgurl.Value = node.ImageUrl;
                }
            }
            this.drpTreeType.SelectedValue = node.TreeType.ToString();
            this.chkEnable.Checked = node.Enabled;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 210;
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

