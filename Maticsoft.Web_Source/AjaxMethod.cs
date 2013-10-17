using Ajax;
using Maticsoft.BLL.Ms;
using System;
using System.Data;

public class AjaxMethod
{
    private Regions bll = new Regions();

    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public DataSet GetAreaList(int povinceid)
    {
        return this.bll.GetCitys(povinceid);
    }

    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public DataSet GetCityList(int povinceid)
    {
        return this.bll.GetCitys(povinceid);
    }

    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public DataTable GetParentId(int id)
    {
        return this.bll.GetParentId(id);
    }

    public DataSet GetPovinceList()
    {
        return this.bll.GetProvinces();
    }
}

