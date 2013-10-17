namespace Maticsoft.Web.Forms
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Common;
    using Maticsoft.Model.Poll;
    using Maticsoft.Web;
    using Maticsoft.Web.Admin;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnAdd;
        protected CheckBox chkIsActive;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox txtDescription;
        protected TextBox txtName;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.ShowFailTip(this, Poll.ErrorFormsNameNull);
            }
            else if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                MessageBox.ShowFailTip(this, Poll.ErrorFormsExplainNull);
            }
            else
            {
                string text = this.txtName.Text;
                string str2 = this.txtDescription.Text;
                Maticsoft.Model.Poll.Forms model = new Maticsoft.Model.Poll.Forms {
                    Name = text,
                    Description = str2
                };
                int num = new Maticsoft.BLL.Poll.Forms().Add(model);
                if (num > 0)
                {
                    base.Response.Redirect("../Topics/Index.aspx?fid=" + num.ToString());
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x162;
            }
        }

        public Basic Master
        {
            get
            {
                return (Basic) base.Master;
            }
        }
    }
}

