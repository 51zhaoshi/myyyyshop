namespace Maticsoft.Web.CMS.ContentClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.ContentClass bll = new Maticsoft.BLL.CMS.ContentClass();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkAllowAddContent;
        protected CheckBox chkAllowSubclass;
        protected DropDownList dropClassTypeID;
        protected DropDownList dropParentID;
        protected HiddenField HiddenField_ICOPath;
        protected Literal lblHelpSecret;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RadioButtonList radlClassModel;
        protected RadioButtonList radlState;
        protected RegularExpressionValidator RegularExpressionValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;
        protected TextBox txtClassIndex;
        protected TextBox txtClassName;
        protected HtmlGenericControl txtClassNameTip;
        protected TextBox txtDescription;
        protected TextBox txtIndexChar;
        protected TextBox txtKeywords;
        protected TextBox txtOrders;
        protected TextBox txtPageModelName;
        protected TextBox txtRemark;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        public void BindClassTypeID()
        {
            DataSet allList = new Maticsoft.BLL.CMS.ClassType().GetAllList();
            if (!DataSetTools.DataSetIsNull(allList))
            {
                this.dropClassTypeID.DataSource = allList;
                this.dropClassTypeID.DataTextField = "ClassTypeName";
                this.dropClassTypeID.DataValueField = "ClassTypeID";
                this.dropClassTypeID.DataBind();
            }
        }

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = row["ClassID"].ToString();
                string text = row["ClassName"].ToString();
                text = blank + "『" + text + "』";
                this.dropParentID.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem("无父级", "0"));
            DataSet treeList = this.bll.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(treeList))
            {
                DataTable dt = treeList.Tables[0];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Select("ParentID= " + 0))
                    {
                        string str = row["ClassID"].ToString();
                        string text = row["ClassName"].ToString();
                        row["ParentID"].ToString();
                        text = "╋" + text;
                        this.dropParentID.Items.Add(new ListItem(text, str));
                        int parentid = int.Parse(str);
                        string blank = "├";
                        this.BindNode(parentid, dt, blank);
                    }
                }
            }
            this.dropParentID.DataBind();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("List.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int classID = 0;
            if (this.dropParentID.SelectedIndex > 0)
            {
                classID = Globals.SafeInt(this.dropParentID.SelectedValue, 0);
                Maticsoft.Model.CMS.ContentClass class2 = this.bll.GetModel(classID);
                if ((class2 != null) && !class2.AllowSubclass)
                {
                    MessageBox.ShowFailTip(this, CMS.CCErrorAddClass);
                    return;
                }
            }
            Maticsoft.Model.CMS.ContentClass model = new Maticsoft.Model.CMS.ContentClass {
                ClassName = Globals.HtmlEncode(this.txtClassName.Text),
                ClassIndex = this.txtClassIndex.Text,
                ParentId = new int?(classID),
                State = Globals.SafeInt(this.radlState.SelectedValue, 0),
                AllowSubclass = this.chkAllowSubclass.Checked,
                AllowAddContent = this.chkAllowAddContent.Checked,
                ImageUrl = this.HiddenField_ICOPath.Value,
                Description = Globals.HtmlEncode(this.txtDescription.Text),
                Keywords = Globals.HtmlEncode(this.txtKeywords.Text),
                ClassTypeID = Globals.SafeInt(this.dropClassTypeID.SelectedValue, 0),
                ClassModel = Globals.SafeInt(this.radlClassModel.SelectedValue, 0),
                PageModelName = Globals.HtmlEncode(this.txtPageModelName.Text),
                IndexChar = this.txtIndexChar.Text,
                CreatedUserID = base.CurrentUser.UserID,
                Remark = Globals.HtmlEncode(this.txtRemark.Text)
            };
            if (this.bll.AddExt(model))
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltipSaveError);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindClassTypeID();
                this.BindTree();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xdd;
            }
        }
    }
}

