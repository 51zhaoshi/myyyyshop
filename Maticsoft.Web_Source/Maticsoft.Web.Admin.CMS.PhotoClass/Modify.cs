namespace Maticsoft.Web.Admin.CMS.PhotoClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected PhotoClassDropList ddlPhotoClass;
        protected HtmlForm Form1;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected TextBox txtClassName;
        protected TextBox txtSequence;

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorClassNameNull);
            }
            else if (!PageValidate.IsNumber(this.txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorOrderFormat);
            }
            else
            {
                int num2;
                Maticsoft.BLL.CMS.PhotoClass class2 = new Maticsoft.BLL.CMS.PhotoClass();
                string text = this.txtClassName.Text;
                int classID = 0;
                if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
                {
                    classID = int.Parse(this.ddlPhotoClass.SelectedValue);
                }
                Maticsoft.Model.CMS.PhotoClass model = class2.GetModel(classID);
                string str2 = string.Empty;
                if (model != null)
                {
                    str2 = model.Path + classID + "|";
                    num2 = model.Depth.Value + 1;
                }
                else
                {
                    str2 = "0|";
                    num2 = 1;
                }
                int num3 = int.Parse(this.txtSequence.Text);
                Maticsoft.Model.CMS.PhotoClass class4 = class2.GetModel(this.ClassID);
                class4.ClassName = text;
                class4.Path = str2;
                class4.Depth = new int?(num2);
                class4.ParentId = new int?(classID);
                class4.Sequence = new int?(num3);
                if (class2.Update(class4))
                {
                    MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.ClassID != 0))
            {
                this.ShowInfo(this.ClassID);
            }
        }

        private void ShowInfo(int ClassID)
        {
            Maticsoft.Model.CMS.PhotoClass model = new Maticsoft.BLL.CMS.PhotoClass().GetModel(ClassID);
            this.txtClassName.Text = model.ClassName;
            this.txtSequence.Text = model.Sequence.ToString();
            this.ddlPhotoClass.SelectedValue = model.ParentId.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xfb;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

