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

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Poll.Forms bll = new Maticsoft.BLL.Poll.Forms();
        protected Button btnAdd;
        protected Button btnCancle;
        protected CheckBox chkIsActive;
        protected Literal Literal3;
        protected Literal Literal4;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox txtDescription;
        protected TextBox txtName;

        private void BindData(int fid)
        {
            Maticsoft.Model.Poll.Forms model = this.bll.GetModel(fid);
            if (model != null)
            {
                this.txtName.Text = model.Name;
                this.txtDescription.Text = model.Description;
                this.chkIsActive.Checked = model.IsActive;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                this.btnAdd.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, Poll.ErrorFormsNameNull);
            }
            else if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                this.btnAdd.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, Poll.ErrorFormsExplainNull);
            }
            else
            {
                string text = this.txtName.Text;
                string str2 = this.txtDescription.Text;
                Maticsoft.Model.Poll.Forms model = new Maticsoft.Model.Poll.Forms {
                    Name = text,
                    Description = str2,
                    FormID = this.Fid,
                    IsActive = this.chkIsActive.Checked
                };
                if (this.bll.Update(model) > 0)
                {
                    MessageBox.ShowSuccessTip(this, "保存成功！", "index.aspx");
                }
                else
                {
                    this.btnAdd.Enabled = true;
                    this.btnCancle.Enabled = true;
                    MessageBox.ShowFailTip(this, "保存失败！");
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("index.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && (this.Fid > 0))
            {
                this.BindData(this.Fid);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x163;
            }
        }

        public int Fid
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["fid"]))
                {
                    num = Globals.SafeInt(base.Request.Params["fid"], 0);
                }
                return num;
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

