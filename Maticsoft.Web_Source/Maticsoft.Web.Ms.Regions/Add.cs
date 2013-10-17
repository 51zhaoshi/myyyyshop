namespace Maticsoft.Web.Ms.Regions
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
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
                Maticsoft.Model.Ms.Regions model = new Maticsoft.Model.Ms.Regions {
                    AreaId = null
                };
                if (((this.Regions1.Province_iID == 0) && (this.Regions1.City_iID == 0)) && (this.Regions1.Area_iID == 0))
                {
                    model.ParentId = null;
                    model.Path = "0,";
                    model.Depth = 1;
                }
                if (((this.Regions1.Province_iID != 0) && (this.Regions1.City_iID == 0)) && (this.Regions1.Area_iID == 0))
                {
                    model.ParentId = new int?(this.Regions1.Province_iID);
                    model.Path = "0," + this.Regions1.Province_iID;
                    model.Depth = 2;
                }
                if (((this.Regions1.Province_iID != 0) && (this.Regions1.City_iID != 0)) && (this.Regions1.Area_iID == 0))
                {
                    model.ParentId = new int?(this.Regions1.City_iID);
                    model.Path = string.Concat(new object[] { "0,", this.Regions1.Province_iID, ",", this.Regions1.City_iID });
                    model.Depth = 3;
                }
                if (((this.Regions1.Province_iID != 0) && (this.Regions1.City_iID != 0)) && (this.Regions1.Area_iID != 0))
                {
                    MessageBox.ShowSuccessTip(this, "暂时支持添加到三级区域");
                }
                else
                {
                    Maticsoft.BLL.Ms.Regions regions2 = new Maticsoft.BLL.Ms.Regions();
                    model.RegionId = regions2.GetMaxId();
                    model.RegionName = this.txtRegionName.Text;
                    model.Spell = null;
                    model.SpellShort = this.txtSpellShort.Text;
                    model.DisplaySequence = Globals.SafeInt(this.txtDisplaySequence.Text, 1);
                    if (0 < regions2.Add(model))
                    {
                        this.btnSave.Enabled = false;
                        this.btnCancle.Enabled = false;
                        MessageBox.ShowSuccessTip(this, "保存成功！", "add.aspx");
                    }
                    else
                    {
                        this.btnSave.Enabled = true;
                        this.btnCancle.Enabled = true;
                        MessageBox.ShowSuccessTip(this, "添加失败！");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Regions1.ProvinceVisible = true;
            this.Regions1.CityVisible = true;
            this.Regions1.AreaVisible = false;
            this.Regions1.VisibleAll = true;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x146;
            }
        }
    }
}

