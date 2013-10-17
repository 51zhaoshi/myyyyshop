namespace Maticsoft.Web.Admin.CMS.ClassType
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class EditInfo : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
        protected Button btnCancle;
        protected Button btnSave;
        protected HtmlForm form1;
        protected HiddenField HiddenField_ID;
        protected Label lblClassTypeID;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected HtmlTableRow modify;
        protected TextBox txtClassTypeName;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtClassTypeName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "请输入类型名称！");
            }
            else
            {
                Maticsoft.BLL.CMS.ClassType type = new Maticsoft.BLL.CMS.ClassType();
                Maticsoft.Model.CMS.ClassType model = null;
                if (!string.IsNullOrWhiteSpace(this.txtClassTypeName.Text.Trim()) && !string.IsNullOrWhiteSpace(this.HiddenField_ID.Value))
                {
                    string s = this.HiddenField_ID.Value;
                    model = new Maticsoft.Model.CMS.ClassType {
                        ClassTypeID = int.Parse(s),
                        ClassTypeName = this.txtClassTypeName.Text.Trim()
                    };
                    if (type.Update(model))
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                        MessageBox.ShowSuccessTip(this, "保存成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "保存失败！");
                    }
                }
                else if (!string.IsNullOrWhiteSpace(this.txtClassTypeName.Text.Trim()))
                {
                    model = new Maticsoft.Model.CMS.ClassType {
                        ClassTypeName = this.txtClassTypeName.Text.Trim()
                    };
                    if (type.Add(model))
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                        MessageBox.ShowSuccessTip(this, "保存成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "保存失败！");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (this.ClassTypeID >= 0)
                {
                    this.modify.Visible = true;
                    this.ShowInfo();
                }
                else
                {
                    this.modify.Visible = false;
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.ClassType modelByCache = this.bll.GetModelByCache(this.ClassTypeID);
            if (modelByCache != null)
            {
                this.lblClassTypeID.Text = modelByCache.ClassTypeID.ToString();
                this.HiddenField_ID.Value = modelByCache.ClassTypeID.ToString();
                this.txtClassTypeName.Text = modelByCache.ClassTypeName;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd8;
            }
        }

        public int ClassTypeID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

