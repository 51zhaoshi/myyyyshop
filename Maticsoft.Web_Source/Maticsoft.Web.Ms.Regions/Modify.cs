namespace Maticsoft.Web.Ms.Regions
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
        protected Button btnCancle;
        protected Button btnSave;
        protected Region Regions1;
        protected TextBox txtDisplaySequence;
        protected TextBox txtRegionName;
        protected TextBox txtSpellShort;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrEmpty(this.txtRegionName.Text.Trim()))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "地区名称不能为空！");
            }
            else
            {
                Maticsoft.Model.Ms.Regions model = this.bll.GetModel(this.RegionId);
                model.RegionName = this.txtRegionName.Text;
                model.SpellShort = this.txtSpellShort.Text;
                model.DisplaySequence = Globals.SafeInt(this.txtDisplaySequence.Text, 1);
                if (this.bll.Update(model))
                {
                    this.btnSave.Enabled = false;
                    this.btnCancle.Enabled = false;
                    MessageBox.ShowSuccessTip(this, "保存成功！", "Modify.aspx");
                }
                else
                {
                    this.btnSave.Enabled = true;
                    this.btnCancle.Enabled = true;
                    MessageBox.ShowSuccessTip(this, "编辑失败！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Regions1.ProvinceVisible = true;
            this.Regions1.CityVisible = true;
            this.Regions1.AreaVisible = true;
            this.Regions1.VisibleAll = true;
            if (!this.Page.IsPostBack && (this.RegionId > 0))
            {
                this.ShowInfo(this.RegionId);
            }
        }

        private void ShowInfo(int RegionId)
        {
            Maticsoft.Model.Ms.Regions model = this.bll.GetModel(RegionId);
            if (model.Depth == 3)
            {
                this.Regions1.Area_iID = model.RegionId;
                this.Regions1.AreaVisible = false;
            }
            else if (model.Depth == 2)
            {
                this.Regions1.City_iID = model.RegionId;
                this.Regions1.AreaVisible = false;
                this.Regions1.CityVisible = false;
            }
            this.txtRegionName.Text = model.RegionName;
            this.txtSpellShort.Text = model.SpellShort;
            this.txtDisplaySequence.Text = model.DisplaySequence.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x147;
            }
        }

        public int RegionId
        {
            get
            {
                int num = -1;
                if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
                {
                    num = Convert.ToInt32(base.Request.Params["id"]);
                }
                return num;
            }
        }
    }
}

