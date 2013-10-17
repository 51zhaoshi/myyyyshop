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

    public class Add : PageBaseAdmin
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.CMS.PhotoClass class2 = new Maticsoft.BLL.CMS.PhotoClass();
            if (string.IsNullOrWhiteSpace(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorClassNameNull);
            }
            else if (string.IsNullOrWhiteSpace(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorClassNameNull);
            }
            else if (class2.ExistsByClassName(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorClassRepeat);
            }
            else if (!PageValidate.IsNumber(this.txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorOrderFormat);
            }
            else
            {
                int num3;
                string text = this.txtClassName.Text;
                int classID = 0;
                if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
                {
                    classID = int.Parse(this.ddlPhotoClass.SelectedValue);
                }
                int num2 = int.Parse(this.txtSequence.Text);
                Maticsoft.Model.CMS.PhotoClass model = class2.GetModel(classID);
                string str2 = string.Empty;
                if (model != null)
                {
                    str2 = model.Path + classID + "|";
                    num3 = model.Depth.Value + 1;
                }
                else
                {
                    str2 = "0|";
                    num3 = 1;
                }
                Maticsoft.Model.CMS.PhotoClass class6 = new Maticsoft.Model.CMS.PhotoClass {
                    ClassName = text,
                    ParentId = new int?(classID),
                    Sequence = new int?(num2),
                    Path = str2,
                    Depth = new int?(num3)
                };
                Maticsoft.Model.CMS.PhotoClass class4 = class6;
                new Maticsoft.BLL.CMS.PhotoClass().Add(class4);
                MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Maticsoft.BLL.CMS.PhotoClass class2 = new Maticsoft.BLL.CMS.PhotoClass();
                this.txtSequence.Text = class2.GetMaxSequence().ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 250;
            }
        }
    }
}

