namespace Maticsoft.Web.Admin.Ms.Enterprise
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Map.BLL;
    using Maticsoft.Map.Model;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class MapManage : PageBaseAdmin
    {
        protected HiddenField hfEnID;
        protected HiddenField hfMapId;
        protected HiddenField hfMapImgUrl;
        protected HiddenField hfUserID;
        protected Literal Literal1;
        protected Literal Literal7;
        private MapInfoManage mapInfoManage = new MapInfoManage();
        protected HtmlInputText txtCity;
        protected TextBox txtMarkersDimension;
        protected TextBox txtMarkersLongitude;
        protected TextBox txtPointerContent;
        protected TextBox txtPointerTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && !string.IsNullOrWhiteSpace(base.Request.QueryString["DepartmentId"]))
            {
                int departmentId = Globals.SafeInt(base.Request.QueryString["DepartmentId"], -1);
                this.hfEnID.Value = departmentId.ToString();
                MapInfo modelByDepartmentId = this.mapInfoManage.GetModelByDepartmentId(departmentId);
                if (modelByDepartmentId == null)
                {
                    Maticsoft.Model.Ms.Enterprise model = new Maticsoft.BLL.Ms.Enterprise().GetModel(departmentId);
                    if ((model != null) && model.RegionID.HasValue)
                    {
                        this.txtCity.Value = new Maticsoft.BLL.Ms.Regions().GetRegionNameByRID(model.RegionID.Value);
                    }
                }
                else
                {
                    this.hfMapId.Value = modelByDepartmentId.MapId.ToString();
                    this.txtPointerTitle.Text = HttpUtility.HtmlDecode(modelByDepartmentId.PointerTitle);
                    this.txtPointerContent.Text = Globals.HtmlDecodeForSpaceWrap(modelByDepartmentId.PointerContent);
                    this.txtMarkersLongitude.Text = modelByDepartmentId.MarkersLongitude;
                    this.txtMarkersDimension.Text = modelByDepartmentId.MarkersDimension;
                    this.hfMapImgUrl.Value = modelByDepartmentId.PointImg;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x13f;
            }
        }
    }
}

