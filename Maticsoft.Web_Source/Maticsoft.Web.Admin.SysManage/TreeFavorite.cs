namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class TreeFavorite : PageBaseAdmin
    {
        protected int Act_TreeFavorite = 0xbf;
        protected Button btnEdit;
        protected Button btnSave;
        protected Label lblTooltip;
        protected DataList listMenus;
        protected Literal Literal1;
        protected Literal Literal2;
        private List<int> nodeidlist = new List<int>();
        private SysTree smbll = new SysTree();
        private Maticsoft.BLL.SysManage.TreeFavorite tfbll = new Maticsoft.BLL.SysManage.TreeFavorite();

        public void BindTreeView(TreeView treeview, DataTable dt, int rootNodeid)
        {
            DataRow[] rowArray = dt.Select("ParentID= " + rootNodeid);
            bool flag = true;
            treeview.Nodes.Clear();
            foreach (DataRow row in rowArray)
            {
                string str = row["NodeID"].ToString();
                string str2 = row["TreeText"].ToString();
                row["ParentID"].ToString();
                row["Location"].ToString();
                string str3 = row["Url"].ToString();
                row["ImageUrl"].ToString();
                int permissionid = int.Parse(row["PermissionID"].ToString().Trim());
                bool flag2 = Convert.ToBoolean(row["Enabled"]);
                if (((permissionid == -1) || base.UserPrincipal.HasPermissionID(permissionid)) && flag2)
                {
                    TreeNode child = new TreeNode {
                        Text = str2,
                        Value = str,
                        NavigateUrl = (str3.ToLower().Contains("/admin/") ? "" : "/admin/") + str3,
                        Expanded = new bool?(flag)
                    };
                    if (this.nodeidlist.Contains(Convert.ToInt32(str)))
                    {
                        child.Checked = true;
                    }
                    else
                    {
                        child.Checked = false;
                    }
                    treeview.Nodes.Add(child);
                    int parentid = int.Parse(str);
                    this.CreateNode(parentid, child, dt);
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("addpi.aspx?TreeType=" + this.TreeType);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (base.CurrentUser != null)
            {
                this.tfbll.DeleteByUser(base.CurrentUser.UserID);
                for (int i = 0; i < this.listMenus.Items.Count; i++)
                {
                    TreeView view = (TreeView) this.listMenus.Items[i].FindControl("treeMenu");
                    foreach (TreeNode node in view.CheckedNodes)
                    {
                        int nodeID = Convert.ToInt32(node.Value);
                        this.tfbll.Add(base.CurrentUser.UserID, nodeID);
                    }
                }
                this.lblTooltip.Text = Site.TooltipSaveOK;
            }
        }

        public void CreateNode(int parentid, TreeNode parentnode, DataTable dt)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string s = row["NodeID"].ToString();
                string str2 = row["TreeText"].ToString();
                row["Location"].ToString();
                string str3 = row["Url"].ToString();
                row["ImageUrl"].ToString();
                int permissionid = int.Parse(row["PermissionID"].ToString().Trim());
                bool flag = Convert.ToBoolean(row["Enabled"]);
                if (((permissionid == -1) || base.UserPrincipal.HasPermissionID(permissionid)) && flag)
                {
                    TreeNode child = new TreeNode {
                        Text = str2,
                        Value = s,
                        NavigateUrl = (str3.ToLower().Contains("/admin/") ? "" : "/admin/") + str3,
                        Expanded = false
                    };
                    int num2 = int.Parse(s);
                    if (this.nodeidlist.Contains(Convert.ToInt32(s)))
                    {
                        child.Checked = true;
                    }
                    else
                    {
                        child.Checked = false;
                    }
                    parentnode.ChildNodes.Add(child);
                    this.CreateNode(num2, child, dt);
                }
            }
        }

        public void listMenus_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                object obj2 = DataBinder.Eval(e.Item.DataItem, "NodeID");
                object obj3 = DataBinder.Eval(e.Item.DataItem, "PermissionID");
                bool flag = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Enabled"));
                int permissionid = Convert.ToInt32(obj3);
                if (((permissionid == -1) || base.UserPrincipal.HasPermissionID(permissionid)) && flag)
                {
                    TreeView treeview = (TreeView) e.Item.FindControl("treeMenu");
                    DataSet allEnabledTreeByType = this.smbll.GetAllEnabledTreeByType(this.TreeType);
                    this.BindTreeView(treeview, allEnabledTreeByType.Tables[0], (int) obj2);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (((!this.Page.IsPostBack && this.Context.User.Identity.IsAuthenticated) && (this.TreeType >= 0)) && (base.CurrentUser != null))
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_TreeFavorite)))
                {
                    this.btnSave.Visible = false;
                }
                this.nodeidlist = this.tfbll.GetNodeIDsByUser(base.CurrentUser.UserID);
                DataSet set = this.smbll.GetTreeSonList(0, this.TreeType, base.UserPrincipal.PermissionsID);
                this.listMenus.DataSource = set;
                this.listMenus.DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd4;
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

