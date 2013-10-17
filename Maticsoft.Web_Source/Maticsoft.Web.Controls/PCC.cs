namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class PCC : UserControl
    {
        private Regions bll = new Regions();
        protected DropDownList ddlCity;
        protected DropDownList ddlCountry;
        protected DropDownList ddlProvince;

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageValidate.IsNumber(this.ddlCity.SelectedValue))
            {
                this.GetCountry(int.Parse(this.ddlCity.SelectedValue));
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageValidate.IsNumber(this.ddlProvince.SelectedValue))
            {
                this.GetCitys(int.Parse(this.ddlProvince.SelectedValue));
            }
            if (PageValidate.IsNumber(this.ddlCity.SelectedValue))
            {
                this.GetCountry(int.Parse(this.ddlCity.SelectedValue));
            }
        }

        public void GetCitys(int parentID)
        {
            DataSet citys = this.bll.GetCitys(parentID);
            this.ddlCity.DataSource = citys.Tables[0];
            this.ddlCity.DataTextField = "RegionName";
            this.ddlCity.DataValueField = "RegionID";
            this.ddlCity.DataBind();
        }

        public void GetCountry(int parentID)
        {
            DataSet citys = this.bll.GetCitys(parentID);
            this.ddlCountry.DataSource = citys.Tables[0];
            this.ddlCountry.DataTextField = "RegionName";
            this.ddlCountry.DataValueField = "RegionID";
            this.ddlCountry.DataBind();
        }

        public void GetProvinces()
        {
            DataSet provinces = this.bll.GetProvinces();
            this.ddlProvince.DataSource = provinces.Tables[0];
            this.ddlProvince.DataTextField = "RegionName";
            this.ddlProvince.DataValueField = "RegionID";
            this.ddlProvince.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.GetProvinces();
                if (PageValidate.IsNumber(this.ddlProvince.SelectedValue))
                {
                    this.GetCitys(int.Parse(this.ddlProvince.SelectedValue));
                }
                if (PageValidate.IsNumber(this.ddlCity.SelectedValue))
                {
                    this.GetCountry(int.Parse(this.ddlCity.SelectedValue));
                }
            }
        }
    }
}

