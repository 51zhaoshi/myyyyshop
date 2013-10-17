namespace Maticsoft.Web.Controls
{
    using Ajax;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class RegionDropList : UserControl
    {
        private int? _regionId;
        private AjaxMethod ajax = new AjaxMethod();
        protected DropDownList ddlArea;
        protected DropDownList ddlCity;
        protected DropDownList ddlProvince;

        private void BindReg(int id)
        {
            int num = int.Parse(this.ajax.GetParentId(id).Rows[0]["ParentId"].ToString());
            int povinceid = int.Parse(this.ajax.GetParentId(num).Rows[0]["ParentId"].ToString());
            this.ddlProvince.DataSource = this.ajax.GetPovinceList();
            this.ddlProvince.DataTextField = "RegionName";
            this.ddlProvince.DataValueField = "RegionId";
            this.ddlProvince.DataBind();
            this.ddlProvince.SelectedValue = id.ToString();
            this.ddlProvince.Attributes.Add("onChange", "cityResult();");
            this.ddlCity.Attributes.Add("onChange", "areaResult();");
            if (this.ddlProvince.SelectedValue != "")
            {
                this.ddlCity.DataSource = this.ajax.GetCityList(povinceid);
                this.ddlCity.DataTextField = "RegionName";
                this.ddlCity.DataValueField = "RegionId";
                this.ddlCity.DataBind();
                this.ddlCity.SelectedValue = num.ToString();
            }
            if (this.ddlCity.SelectedValue != "")
            {
                this.ddlArea.DataSource = this.ajax.GetAreaList(num);
                this.ddlArea.DataTextField = "RegionName";
                this.ddlArea.DataValueField = "RegionId";
                this.ddlArea.DataBind();
                this.ddlArea.SelectedValue = id.ToString();
            }
        }

        private void InitRegion()
        {
            this.ddlProvince.DataSource = this.ajax.GetPovinceList();
            this.ddlProvince.DataTextField = "RegionName";
            this.ddlProvince.DataValueField = "RegionId";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0, new ListItem("请选择", ""));
            this.ddlProvince.Attributes.Add("onChange", "cityResult();");
            this.ddlCity.Attributes.Add("onChange", "areaResult();");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(AjaxMethod));
            if (this.RegionId.HasValue)
            {
                this.BindReg(this.RegionId.Value);
            }
            else
            {
                this.InitRegion();
            }
        }

        public int? RegionId
        {
            get
            {
                return this._regionId;
            }
            set
            {
                this._regionId = value;
            }
        }
    }
}

