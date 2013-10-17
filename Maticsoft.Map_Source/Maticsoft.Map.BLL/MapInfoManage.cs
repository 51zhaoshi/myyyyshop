namespace Maticsoft.Map.BLL
{
    using Maticsoft.Common;
    using Maticsoft.Map.DAL;
    using Maticsoft.Map.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class MapInfoManage
    {
        private readonly MapInfoService dal = new MapInfoService();
        private const int ModelCache = 30;

        public int Add(MapInfo model)
        {
            return this.dal.Add(model);
        }

        public List<MapInfo> DataTableToList(DataTable dt)
        {
            List<MapInfo> list = new List<MapInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    MapInfo item = new MapInfo();
                    if ((dt.Rows[i]["MapId"] != null) && (dt.Rows[i]["MapId"].ToString() != ""))
                    {
                        item.MapId = int.Parse(dt.Rows[i]["MapId"].ToString());
                    }
                    if ((dt.Rows[i]["UserId"] != null) && (dt.Rows[i]["UserId"].ToString() != ""))
                    {
                        item.UserId = int.Parse(dt.Rows[i]["UserId"].ToString());
                    }
                    if ((dt.Rows[i]["DepartmentId"] != null) && (dt.Rows[i]["DepartmentId"].ToString() != ""))
                    {
                        item.DepartmentId = int.Parse(dt.Rows[i]["DepartmentId"].ToString());
                    }
                    if ((dt.Rows[i]["PointerLongitude"] != null) && (dt.Rows[i]["PointerLongitude"].ToString() != ""))
                    {
                        item.PointerLongitude = dt.Rows[i]["PointerLongitude"].ToString();
                    }
                    if ((dt.Rows[i]["PointDimension"] != null) && (dt.Rows[i]["PointDimension"].ToString() != ""))
                    {
                        item.PointDimension = dt.Rows[i]["PointDimension"].ToString();
                    }
                    if ((dt.Rows[i]["PointClass"] != null) && (dt.Rows[i]["PointClass"].ToString() != ""))
                    {
                        item.PointClass = dt.Rows[i]["PointClass"].ToString();
                    }
                    if ((dt.Rows[i]["PointerType"] != null) && (dt.Rows[i]["PointerType"].ToString() != ""))
                    {
                        item.PointerType = dt.Rows[i]["PointerType"].ToString();
                    }
                    if ((dt.Rows[i]["PointerTitle"] != null) && (dt.Rows[i]["PointerTitle"].ToString() != ""))
                    {
                        item.PointerTitle = dt.Rows[i]["PointerTitle"].ToString();
                    }
                    if ((dt.Rows[i]["PointImg"] != null) && (dt.Rows[i]["PointImg"].ToString() != ""))
                    {
                        item.PointImg = dt.Rows[i]["PointImg"].ToString();
                    }
                    if ((dt.Rows[i]["PointerContent"] != null) && (dt.Rows[i]["PointerContent"].ToString() != ""))
                    {
                        item.PointerContent = dt.Rows[i]["PointerContent"].ToString();
                    }
                    if ((dt.Rows[i]["SearchCity"] != null) && (dt.Rows[i]["SearchCity"].ToString() != ""))
                    {
                        item.SearchCity = dt.Rows[i]["SearchCity"].ToString();
                    }
                    if ((dt.Rows[i]["searchArea"] != null) && (dt.Rows[i]["searchArea"].ToString() != ""))
                    {
                        item.searchArea = dt.Rows[i]["searchArea"].ToString();
                    }
                    if ((dt.Rows[i]["Level"] != null) && (dt.Rows[i]["Level"].ToString() != ""))
                    {
                        item.Level = new int?(int.Parse(dt.Rows[i]["Level"].ToString()));
                    }
                    if ((dt.Rows[i]["enableKeyboard"] != null) && (dt.Rows[i]["enableKeyboard"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["enableKeyboard"].ToString() == "1") || (dt.Rows[i]["enableKeyboard"].ToString().ToLower() == "true"))
                        {
                            item.enableKeyboard = true;
                        }
                        else
                        {
                            item.enableKeyboard = false;
                        }
                    }
                    if ((dt.Rows[i]["enableScrollWheelZoom"] != null) && (dt.Rows[i]["enableScrollWheelZoom"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["enableScrollWheelZoom"].ToString() == "1") || (dt.Rows[i]["enableScrollWheelZoom"].ToString().ToLower() == "true"))
                        {
                            item.enableScrollWheelZoom = true;
                        }
                        else
                        {
                            item.enableScrollWheelZoom = false;
                        }
                    }
                    if ((dt.Rows[i]["NavigationControl"] != null) && (dt.Rows[i]["NavigationControl"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["NavigationControl"].ToString() == "1") || (dt.Rows[i]["NavigationControl"].ToString().ToLower() == "true"))
                        {
                            item.NavigationControl = true;
                        }
                        else
                        {
                            item.NavigationControl = false;
                        }
                    }
                    if ((dt.Rows[i]["ScaleControl"] != null) && (dt.Rows[i]["ScaleControl"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["ScaleControl"].ToString() == "1") || (dt.Rows[i]["ScaleControl"].ToString().ToLower() == "true"))
                        {
                            item.ScaleControl = true;
                        }
                        else
                        {
                            item.ScaleControl = false;
                        }
                    }
                    if ((dt.Rows[i]["MapTypeControl"] != null) && (dt.Rows[i]["MapTypeControl"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["MapTypeControl"].ToString() == "1") || (dt.Rows[i]["MapTypeControl"].ToString().ToLower() == "true"))
                        {
                            item.MapTypeControl = true;
                        }
                        else
                        {
                            item.MapTypeControl = false;
                        }
                    }
                    if ((dt.Rows[i]["MarkersLongitude"] != null) && (dt.Rows[i]["MarkersLongitude"].ToString() != ""))
                    {
                        item.MarkersLongitude = dt.Rows[i]["MarkersLongitude"].ToString();
                    }
                    if ((dt.Rows[i]["Markersdimension"] != null) && (dt.Rows[i]["Markersdimension"].ToString() != ""))
                    {
                        item.MarkersDimension = dt.Rows[i]["Markersdimension"].ToString();
                    }
                    if ((dt.Rows[i]["setAnimation"] != null) && (dt.Rows[i]["setAnimation"].ToString() != ""))
                    {
                        item.setAnimation = dt.Rows[i]["setAnimation"].ToString();
                    }
                    if ((dt.Rows[i]["LoadEvent"] != null) && (dt.Rows[i]["LoadEvent"].ToString() != ""))
                    {
                        item.LoadEvent = dt.Rows[i]["LoadEvent"].ToString();
                    }
                    if ((dt.Rows[i]["MenuItemzoomIn"] != null) && (dt.Rows[i]["MenuItemzoomIn"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["MenuItemzoomIn"].ToString() == "1") || (dt.Rows[i]["MenuItemzoomIn"].ToString().ToLower() == "true"))
                        {
                            item.MenuItemzoomIn = true;
                        }
                        else
                        {
                            item.MenuItemzoomIn = false;
                        }
                    }
                    if ((dt.Rows[i]["MenuItemzoomOut"] != null) && (dt.Rows[i]["MenuItemzoomOut"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["MenuItemzoomOut"].ToString() == "1") || (dt.Rows[i]["MenuItemzoomOut"].ToString().ToLower() == "true"))
                        {
                            item.MenuItemzoomOut = true;
                        }
                        else
                        {
                            item.MenuItemzoomOut = false;
                        }
                    }
                    if ((dt.Rows[i]["MenuItemsetZoomTop"] != null) && (dt.Rows[i]["MenuItemsetZoomTop"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["MenuItemsetZoomTop"].ToString() == "1") || (dt.Rows[i]["MenuItemsetZoomTop"].ToString().ToLower() == "true"))
                        {
                            item.MenuItemsetZoomTop = true;
                        }
                        else
                        {
                            item.MenuItemsetZoomTop = false;
                        }
                    }
                    if ((dt.Rows[i]["MenuItemsetPoint"] != null) && (dt.Rows[i]["MenuItemsetPoint"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["MenuItemsetPoint"].ToString() == "1") || (dt.Rows[i]["MenuItemsetPoint"].ToString().ToLower() == "true"))
                        {
                            item.MenuItemsetPoint = true;
                        }
                        else
                        {
                            item.MenuItemsetPoint = false;
                        }
                    }
                    if ((dt.Rows[i]["MapType"] != null) && (dt.Rows[i]["MapType"].ToString() != ""))
                    {
                        item.MapType = int.Parse(dt.Rows[i]["MapType"].ToString());
                    }
                    if ((dt.Rows[i]["Other1"] != null) && (dt.Rows[i]["Other1"].ToString() != ""))
                    {
                        item.Other1 = dt.Rows[i]["Other1"].ToString();
                    }
                    if ((dt.Rows[i]["Other2"] != null) && (dt.Rows[i]["Other2"].ToString() != ""))
                    {
                        item.Other2 = dt.Rows[i]["Other2"].ToString();
                    }
                    if ((dt.Rows[i]["Other3"] != null) && (dt.Rows[i]["Other3"].ToString() != ""))
                    {
                        item.Other3 = dt.Rows[i]["Other3"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int MapId)
        {
            return this.dal.Delete(MapId);
        }

        public bool DeleteList(string MapIdlist)
        {
            return this.dal.DeleteList(MapIdlist);
        }

        public bool Exists(int MapId)
        {
            return this.dal.Exists(MapId);
        }

        public bool ExistsByDepartmentId(int departmentId)
        {
            return this.dal.ExistsByDepartmentId(departmentId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public MapInfo GetModel(int MapId)
        {
            return this.dal.GetModel(MapId);
        }

        public MapInfo GetModelByCache(int MapId)
        {
            string cacheKey = "MapsModel-" + MapId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(MapId);
                    if (cache != null)
                    {
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes(30.0), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (MapInfo) cache;
        }

        public MapInfo GetModelByDepartmentId(int departmentId)
        {
            return this.dal.GetModelByDepartmentId(departmentId);
        }

        public List<MapInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(MapInfo model)
        {
            return this.dal.Update(model);
        }
    }
}

