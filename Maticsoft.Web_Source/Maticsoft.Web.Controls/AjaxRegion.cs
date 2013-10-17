namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class AjaxRegion : UserControl
    {
        private string _classStyle;
        private string _hfValue;
        private bool _visibleall;
        private string _visiblealltext = "请选择";
        private Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
        protected DropDownList ddlArea;
        protected DropDownList ddlCity;
        protected DropDownList ddlProvince;
        protected HiddenField HiddenField_OldValue;
        protected HiddenField HiddenField_SelectValue;

        private void BindArea(int City_iID)
        {
            try
            {
                this.ddlArea.DataSource = this.bll.GetDistrictByParentId(City_iID);
                this.ddlArea.DataTextField = "RegionName";
                this.ddlArea.DataValueField = "RegionId";
                this.ddlArea.DataBind();
                this.ddlArea.Items.Insert(0, new ListItem("请选择", "0"));
                if (this._visibleall)
                {
                    this.ddlArea.Items.Insert(0, new ListItem(this._visiblealltext, "0"));
                }
            }
            catch (Exception exception)
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                    Loginfo = exception.Message,
                    StackTrace = exception.StackTrace,
                    Url = base.Request.Url.AbsoluteUri
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            }
        }

        private void BindCity(int ParentId)
        {
            try
            {
                this.ddlCity.DataSource = this.bll.GetDistrictByParentId(ParentId);
                this.ddlCity.DataTextField = "RegionName";
                this.ddlCity.DataValueField = "RegionId";
                this.ddlCity.DataBind();
                this.ddlCity.Items.Insert(0, new ListItem("请选择", "0"));
                if (this._visibleall)
                {
                    this.ddlCity.Items.Insert(0, new ListItem(this._visiblealltext, "0"));
                }
            }
            catch (Exception exception)
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                    Loginfo = exception.Message,
                    StackTrace = exception.StackTrace,
                    Url = base.Request.Url.AbsoluteUri
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            }
        }

        protected void BindPrivoces()
        {
            try
            {
                this.ddlProvince.DataSource = this.bll.GetPrivoces();
                this.ddlProvince.DataTextField = "RegionName";
                this.ddlProvince.DataValueField = "RegionId";
                this.ddlProvince.DataBind();
                this.ddlProvince.Items.Insert(0, new ListItem("请选择", "0"));
                if (this._visibleall)
                {
                    this.ddlProvince.Items.Insert(0, new ListItem(this._visiblealltext, "0"));
                }
            }
            catch (Exception exception)
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                    Loginfo = exception.Message,
                    StackTrace = exception.StackTrace,
                    Url = base.Request.Url.AbsoluteUri
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            }
        }

        private void InitPrivoces()
        {
            this.BindPrivoces();
            this.ddlProvince.Attributes.Add("onchange", "getCitys(this);");
            this.ddlCity.Items.Clear();
            this.ddlCity.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddlArea.Items.Clear();
            this.ddlArea.Items.Insert(0, new ListItem("请选择", "0"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (this.Area_iID == -1)
                {
                    this.InitPrivoces();
                }
            }
            else if (this.Area_iID == -1)
            {
                this.InitPrivoces();
            }
        }

        public int Area_iID
        {
            get
            {
                if ((this.ddlArea.SelectedItem != null) && (this.ddlArea.SelectedValue.Length > 0))
                {
                    return Convert.ToInt32(this.ddlArea.SelectedValue);
                }
                return this.City_iID;
            }
            set
            {
                Maticsoft.Model.Ms.Regions modelByCache = this.bll.GetModelByCache(value);
                if (modelByCache != null)
                {
                    Maticsoft.Model.Ms.Regions regions2 = this.bll.GetModelByCache(modelByCache.ParentId.Value);
                    if (regions2 != null)
                    {
                        this.BindPrivoces();
                        this.ddlProvince.SelectedValue = regions2.ParentId.ToString();
                        if (regions2.ParentId > 0)
                        {
                            this.BindCity(regions2.ParentId.Value);
                            this.ddlCity.SelectedValue = regions2.RegionId.ToString();
                        }
                        if (regions2.RegionId > 0)
                        {
                            this.BindArea(regions2.RegionId);
                            this.ddlArea.SelectedValue = value.ToString();
                        }
                    }
                }
            }
        }

        public bool AreaVisible
        {
            set
            {
                this.ddlArea.Visible = value;
            }
        }

        public int City_iID
        {
            get
            {
                if ((this.ddlCity.SelectedItem != null) && (this.ddlCity.SelectedValue.Length > 0))
                {
                    return Convert.ToInt32(this.ddlCity.SelectedValue);
                }
                return -1;
            }
            set
            {
                Maticsoft.Model.Ms.Regions model = this.bll.GetModel(value);
                if (model != null)
                {
                    this.BindPrivoces();
                    this.ddlProvince.SelectedValue = model.ParentId.ToString();
                    if (model.ParentId > 0)
                    {
                        this.BindCity(model.ParentId.Value);
                        this.ddlCity.SelectedValue = value.ToString();
                        if (value > 0)
                        {
                            this.BindArea(value);
                        }
                    }
                }
            }
        }

        public bool CityVisible
        {
            set
            {
                this.ddlCity.Visible = value;
            }
        }

        public string ClassStyle
        {
            get
            {
                return this._classStyle;
            }
            set
            {
                this._classStyle = value;
                if (!string.IsNullOrWhiteSpace(this._classStyle))
                {
                    this.ddlProvince.Attributes.Add("class", this._classStyle);
                    this.ddlCity.Attributes.Add("class", this._classStyle);
                    this.ddlArea.Attributes.Add("class", this._classStyle);
                }
            }
        }

        [Obsolete]
        public string HFValue
        {
            get
            {
                return this._hfValue;
            }
            set
            {
                this._hfValue = value;
                this.HiddenField_SelectValue.Value = this._hfValue;
                int count = 0;
                this.bll.GetParentIDs(Globals.SafeInt(this._hfValue, 0), out count);
                switch (count)
                {
                    case 2:
                        this.Area_iID = Globals.SafeInt(this._hfValue, 0);
                        break;

                    case 1:
                        this.City_iID = Globals.SafeInt(this._hfValue, 0);
                        break;
                }
            }
        }

        public int Province_iID
        {
            get
            {
                if ((this.ddlProvince.SelectedItem != null) && (this.ddlProvince.SelectedValue.Length > 0))
                {
                    return Convert.ToInt32(this.ddlProvince.SelectedValue);
                }
                return -1;
            }
            set
            {
                if (this.ddlProvince.Items.Count > 0)
                {
                    this.ddlProvince.SelectedValue = value.ToString();
                }
            }
        }

        public bool ProvinceVisible
        {
            set
            {
                this.ddlProvince.Visible = value;
            }
        }

        public string SelectedValue
        {
            get
            {
                return this.HiddenField_SelectValue.Value;
            }
            set
            {
                this.HiddenField_SelectValue.Value = value;
                int count = 0;
                this.bll.GetParentIDs(Globals.SafeInt(value, 0), out count);
                switch (count)
                {
                    case 2:
                        this.Area_iID = Globals.SafeInt(value, 0);
                        break;

                    case 1:
                        this.City_iID = Globals.SafeInt(value, 0);
                        break;
                }
            }
        }

        public bool VisibleAll
        {
            set
            {
                this._visibleall = value;
            }
        }

        public string VisibleAllText
        {
            set
            {
                this._visiblealltext = value;
            }
        }
    }
}

