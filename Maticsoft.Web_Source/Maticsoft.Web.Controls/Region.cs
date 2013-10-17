namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Region : UserControl
    {
        private bool _autobinddata = true;
        private bool _visibleall;
        private string _visiblealltext = "全部";
        private Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
        protected DropDownList ddlArea;
        protected DropDownList ddlCity;
        protected DropDownList ddlProvince;
        private userEventArea DeptSelectedIndexChanged;

        public event userEventArea DeptSelectedIndexChanged
        {
            add
            {
                userEventArea area2;
                userEventArea deptSelectedIndexChanged = this.DeptSelectedIndexChanged;
                do
                {
                    area2 = deptSelectedIndexChanged;
                    userEventArea area3 = (userEventArea) Delegate.Combine(area2, value);
                    deptSelectedIndexChanged = Interlocked.CompareExchange<userEventArea>(ref this.DeptSelectedIndexChanged, area3, area2);
                }
                while (deptSelectedIndexChanged != area2);
            }
            remove
            {
                userEventArea area2;
                userEventArea deptSelectedIndexChanged = this.DeptSelectedIndexChanged;
                do
                {
                    area2 = deptSelectedIndexChanged;
                    userEventArea area3 = (userEventArea) Delegate.Remove(area2, value);
                    deptSelectedIndexChanged = Interlocked.CompareExchange<userEventArea>(ref this.DeptSelectedIndexChanged, area3, area2);
                }
                while (deptSelectedIndexChanged != area2);
            }
        }

        private void BindArea(int City_iID)
        {
            try
            {
                this.ddlArea.DataSource = this.bll.GetDistrictByParentId(City_iID);
                this.ddlArea.DataTextField = "RegionName";
                this.ddlArea.DataValueField = "RegionId";
                this.ddlArea.DataBind();
                if (this._visibleall)
                {
                    this.ddlArea.Items.Insert(0, new ListItem(this._visiblealltext, "0"));
                    this.ddlArea.SelectedValue = "0";
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
                if (this._visibleall)
                {
                    this.ddlCity.Items.Insert(0, new ListItem(this._visiblealltext, "0"));
                    this.ddlCity.SelectedValue = "0";
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
                DataSet privoces = this.bll.GetPrivoces();
                this.ddlProvince.DataSource = privoces;
                this.ddlProvince.DataTextField = "RegionName";
                this.ddlProvince.DataValueField = "RegionId";
                this.ddlProvince.DataBind();
                if (this._visibleall)
                {
                    this.ddlProvince.Items.Insert(0, new ListItem(this._visiblealltext, "0"));
                    this.ddlProvince.SelectedValue = "0";
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

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DeptSelectedIndexChanged != null)
            {
                this.DeptSelectedIndexChanged(this, e);
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlCity.SelectedItem != null)
            {
                this.ddlArea.Items.Clear();
                int num = Convert.ToInt32(this.ddlCity.SelectedValue);
                this.BindArea(num);
                this.ddlArea_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlProvince.SelectedItem != null)
            {
                this.ddlCity.Items.Clear();
                this.ddlArea.Items.Clear();
                int parentId = Convert.ToInt32(this.ddlProvince.SelectedValue);
                this.BindCity(parentId);
                this.ddlCity_SelectedIndexChanged(null, null);
            }
        }

        private void GetCityByArea(int id)
        {
            Maticsoft.Model.Ms.Regions model = this.bll.GetModel(id);
            if (model != null)
            {
                this.City_iID = Convert.ToInt32(model.ParentId);
            }
        }

        private void GetProvinceByCity(int id)
        {
            Maticsoft.Model.Ms.Regions model = this.bll.GetModel(id);
            if (model != null)
            {
                this.Province_iID = Convert.ToInt32(model.ParentId);
            }
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && this._autobinddata)
            {
                if (this.Area_iID != -1)
                {
                    this.GetCityByArea(this.Area_iID);
                    this.GetProvinceByCity(this.City_iID);
                }
                else
                {
                    this.BindPrivoces();
                    if (this.ddlProvince.Items.Count > 0)
                    {
                        int parentId = Convert.ToInt32(this.ddlProvince.Items[0].Value);
                        this.BindCity(parentId);
                    }
                    if (this.ddlCity.Items.Count > 0)
                    {
                        int num2 = Convert.ToInt32(this.ddlCity.Items[0].Value);
                        this.BindArea(num2);
                    }
                }
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
                if ((modelByCache != null) && modelByCache.ParentId.HasValue)
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

        public bool AreaEnabled
        {
            set
            {
                this.ddlArea.Enabled = value;
            }
        }

        public bool AreaVisible
        {
            set
            {
                this.ddlArea.Visible = value;
            }
        }

        public bool AutoBindData
        {
            set
            {
                this._autobinddata = value;
            }
        }

        public bool AutoPostBackArea
        {
            set
            {
                this.ddlArea.AutoPostBack = value;
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
                    }
                }
            }
        }

        public bool CityEnabled
        {
            set
            {
                this.ddlCity.Enabled = value;
            }
        }

        public bool CityVisible
        {
            set
            {
                this.ddlCity.Visible = value;
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

        public bool ProvinceEnabled
        {
            set
            {
                this.ddlProvince.Enabled = value;
            }
        }

        public bool ProvinceVisible
        {
            set
            {
                this.ddlProvince.Visible = value;
            }
        }

        public int Region_iID
        {
            get
            {
                if ((this.ddlArea.SelectedItem != null) && (this.ddlArea.SelectedValue.Length > 0))
                {
                    int num = Globals.SafeInt(this.ddlArea.SelectedValue, 0);
                    if (num > 0)
                    {
                        return num;
                    }
                }
                if ((this.ddlCity.SelectedItem != null) && (this.ddlCity.SelectedValue.Length > 0))
                {
                    int num2 = Globals.SafeInt(this.ddlCity.SelectedValue, 0);
                    if (num2 > 0)
                    {
                        return num2;
                    }
                }
                return this.Province_iID;
            }
            set
            {
                Maticsoft.Model.Ms.Regions modelByCache = this.bll.GetModelByCache(value);
                if (modelByCache != null)
                {
                    switch (modelByCache.Depth)
                    {
                        case 1:
                            this.BindPrivoces();
                            if (this.ddlProvince.Items.Count > 0)
                            {
                                int parentId = Convert.ToInt32(modelByCache.RegionId);
                                this.BindCity(parentId);
                            }
                            if (this.ddlCity.Items.Count > 0)
                            {
                                int num2 = Convert.ToInt32(this.ddlCity.Items[0].Value);
                                this.BindArea(num2);
                            }
                            this.ddlProvince.SelectedValue = modelByCache.RegionId.ToString();
                            return;

                        case 2:
                            this.BindPrivoces();
                            if (this.ddlProvince.Items.Count > 0)
                            {
                                this.ddlProvince.SelectedValue = modelByCache.ParentId.Value.ToString();
                                this.BindCity(modelByCache.ParentId.Value);
                            }
                            if (this.ddlCity.Items.Count > 0)
                            {
                                this.BindArea(modelByCache.RegionId);
                            }
                            this.ddlCity.SelectedValue = modelByCache.RegionId.ToString();
                            return;

                        case 3:
                        {
                            Maticsoft.Model.Ms.Regions regions2 = this.bll.GetModelByCache(modelByCache.ParentId.Value);
                            if ((regions2 != null) && regions2.ParentId.HasValue)
                            {
                                this.BindPrivoces();
                                if (this.ddlProvince.Items.Count > 0)
                                {
                                    this.ddlProvince.SelectedValue = regions2.ParentId.Value.ToString();
                                    this.BindCity(regions2.ParentId.Value);
                                }
                                if (this.ddlCity.Items.Count > 0)
                                {
                                    this.ddlCity.SelectedValue = regions2.RegionId.ToString();
                                    this.BindArea(regions2.RegionId);
                                }
                                this.ddlArea.SelectedValue = modelByCache.RegionId.ToString();
                                return;
                            }
                            return;
                        }
                    }
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

        public delegate void userEventArea(object sender, EventArgs arg);
    }
}

