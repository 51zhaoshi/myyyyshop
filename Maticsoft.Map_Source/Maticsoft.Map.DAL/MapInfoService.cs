namespace Maticsoft.Map.DAL
{
    using Maticsoft.DBUtility;
    using Maticsoft.Map.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MapInfoService
    {
        public int Add(MapInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_Maps(");
            builder.Append("UserId,DepartmentId,PointerLongitude,PointDimension,PointClass,PointerType,PointerTitle,PointImg,PointerContent,SearchCity,searchArea,Level,enableKeyboard,enableScrollWheelZoom,NavigationControl,ScaleControl,MapTypeControl,MarkersLongitude,MarkersDimension,setAnimation,LoadEvent,MenuItemzoomIn,MenuItemzoomOut,MenuItemsetZoomTop,MenuItemsetPoint,MapType,Other1,Other2,Other3)");
            builder.Append(" values (");
            builder.Append("@UserId,@DepartmentId,@PointerLongitude,@PointDimension,@PointClass,@PointerType,@PointerTitle,@PointImg,@PointerContent,@SearchCity,@searchArea,@Level,@enableKeyboard,@enableScrollWheelZoom,@NavigationControl,@ScaleControl,@MapTypeControl,@MarkersLongitude,@MarkersDimension,@setAnimation,@LoadEvent,@MenuItemzoomIn,@MenuItemzoomOut,@MenuItemsetZoomTop,@MenuItemsetPoint,@MapType,@Other1,@Other2,@Other3)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@DepartmentId", SqlDbType.Int, 4), new SqlParameter("@PointerLongitude", SqlDbType.NVarChar, 100), new SqlParameter("@PointDimension", SqlDbType.NVarChar, 100), new SqlParameter("@PointClass", SqlDbType.NVarChar, 100), new SqlParameter("@PointerType", SqlDbType.NVarChar, 100), new SqlParameter("@PointerTitle", SqlDbType.NVarChar, 100), new SqlParameter("@PointImg", SqlDbType.NVarChar, 100), new SqlParameter("@PointerContent", SqlDbType.NVarChar, 300), new SqlParameter("@SearchCity", SqlDbType.NVarChar, 50), new SqlParameter("@searchArea", SqlDbType.NVarChar, 50), new SqlParameter("@Level", SqlDbType.Int, 4), new SqlParameter("@enableKeyboard", SqlDbType.Bit, 1), new SqlParameter("@enableScrollWheelZoom", SqlDbType.Bit, 1), new SqlParameter("@NavigationControl", SqlDbType.Bit, 1), new SqlParameter("@ScaleControl", SqlDbType.Bit, 1), 
                new SqlParameter("@MapTypeControl", SqlDbType.Bit, 1), new SqlParameter("@MarkersLongitude", SqlDbType.NVarChar, 100), new SqlParameter("@MarkersDimension", SqlDbType.NVarChar, 100), new SqlParameter("@setAnimation", SqlDbType.NVarChar, 100), new SqlParameter("@LoadEvent", SqlDbType.NVarChar, 50), new SqlParameter("@MenuItemzoomIn", SqlDbType.Bit, 1), new SqlParameter("@MenuItemzoomOut", SqlDbType.Bit, 1), new SqlParameter("@MenuItemsetZoomTop", SqlDbType.Bit, 1), new SqlParameter("@MenuItemsetPoint", SqlDbType.Bit, 1), new SqlParameter("@MapType", SqlDbType.SmallInt, 2), new SqlParameter("@Other1", SqlDbType.NVarChar, 100), new SqlParameter("@Other2", SqlDbType.NVarChar, 100), new SqlParameter("@Other3", SqlDbType.NVarChar, 50)
             };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.DepartmentId;
            cmdParms[2].Value = model.PointerLongitude;
            cmdParms[3].Value = model.PointDimension;
            cmdParms[4].Value = model.PointClass;
            cmdParms[5].Value = model.PointerType;
            cmdParms[6].Value = model.PointerTitle;
            cmdParms[7].Value = model.PointImg;
            cmdParms[8].Value = model.PointerContent;
            cmdParms[9].Value = model.SearchCity;
            cmdParms[10].Value = model.searchArea;
            cmdParms[11].Value = model.Level;
            cmdParms[12].Value = model.enableKeyboard;
            cmdParms[13].Value = model.enableScrollWheelZoom;
            cmdParms[14].Value = model.NavigationControl;
            cmdParms[15].Value = model.ScaleControl;
            cmdParms[0x10].Value = model.MapTypeControl;
            cmdParms[0x11].Value = model.MarkersLongitude;
            cmdParms[0x12].Value = model.MarkersDimension;
            cmdParms[0x13].Value = model.setAnimation;
            cmdParms[20].Value = model.LoadEvent;
            cmdParms[0x15].Value = model.MenuItemzoomIn;
            cmdParms[0x16].Value = model.MenuItemzoomOut;
            cmdParms[0x17].Value = model.MenuItemsetZoomTop;
            cmdParms[0x18].Value = model.MenuItemsetPoint;
            cmdParms[0x19].Value = model.MapType;
            cmdParms[0x1a].Value = model.Other1;
            cmdParms[0x1b].Value = model.Other2;
            cmdParms[0x1c].Value = model.Other3;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int MapId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Maps ");
            builder.Append(" where MapId=@MapId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MapId", SqlDbType.Int, 4) };
            cmdParms[0].Value = MapId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string MapIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Maps ");
            builder.Append(" where MapId in (" + MapIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int MapId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_Maps");
            builder.Append(" where MapId=@MapId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MapId", SqlDbType.Int, 4) };
            cmdParms[0].Value = MapId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistsByDepartmentId(int department)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_Maps");
            builder.Append(" where DepartmentId=@DepartmentId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DepartmentId", SqlDbType.Int, 4) };
            cmdParms[0].Value = department;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MapId,UserId,DepartmentId,PointerLongitude,PointDimension,PointClass,PointerType,PointerTitle,PointImg,PointerContent,SearchCity,searchArea,Level,enableKeyboard,enableScrollWheelZoom,NavigationControl,ScaleControl,MapTypeControl,MarkersLongitude,MarkersDimension,setAnimation,LoadEvent,MenuItemzoomIn,MenuItemzoomOut,MenuItemsetZoomTop,MenuItemsetPoint,MapType,Other1,Other2,Other3 ");
            builder.Append(" FROM Ms_Maps ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" MapId,UserId,DepartmentId,PointerLongitude,PointDimension,PointClass,PointerType,PointerTitle,PointImg,PointerContent,SearchCity,searchArea,Level,enableKeyboard,enableScrollWheelZoom,NavigationControl,ScaleControl,MapTypeControl,MarkersLongitude,MarkersDimension,setAnimation,LoadEvent,MenuItemzoomIn,MenuItemzoomOut,MenuItemsetZoomTop,MenuItemsetPoint,MapType,Other1,Other2,Other3 ");
            builder.Append(" FROM Ms_Maps ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.MapId desc");
            }
            builder.Append(")AS Row, T.*  from Ms_Maps T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        private static MapInfo GetMapInfo(StringBuilder strSql, SqlParameter[] parameters)
        {
            MapInfo info = new MapInfo();
            DataSet set = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["MapId"] != null) && (set.Tables[0].Rows[0]["MapId"].ToString() != ""))
            {
                info.MapId = int.Parse(set.Tables[0].Rows[0]["MapId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserId"] != null) && (set.Tables[0].Rows[0]["UserId"].ToString() != ""))
            {
                info.UserId = int.Parse(set.Tables[0].Rows[0]["UserId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["DepartmentId"] != null) && (set.Tables[0].Rows[0]["DepartmentId"].ToString() != ""))
            {
                info.DepartmentId = int.Parse(set.Tables[0].Rows[0]["DepartmentId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["PointerLongitude"] != null) && (set.Tables[0].Rows[0]["PointerLongitude"].ToString() != ""))
            {
                info.PointerLongitude = set.Tables[0].Rows[0]["PointerLongitude"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PointDimension"] != null) && (set.Tables[0].Rows[0]["PointDimension"].ToString() != ""))
            {
                info.PointDimension = set.Tables[0].Rows[0]["PointDimension"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PointClass"] != null) && (set.Tables[0].Rows[0]["PointClass"].ToString() != ""))
            {
                info.PointClass = set.Tables[0].Rows[0]["PointClass"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PointerType"] != null) && (set.Tables[0].Rows[0]["PointerType"].ToString() != ""))
            {
                info.PointerType = set.Tables[0].Rows[0]["PointerType"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PointerTitle"] != null) && (set.Tables[0].Rows[0]["PointerTitle"].ToString() != ""))
            {
                info.PointerTitle = set.Tables[0].Rows[0]["PointerTitle"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PointImg"] != null) && (set.Tables[0].Rows[0]["PointImg"].ToString() != ""))
            {
                info.PointImg = set.Tables[0].Rows[0]["PointImg"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PointerContent"] != null) && (set.Tables[0].Rows[0]["PointerContent"].ToString() != ""))
            {
                info.PointerContent = set.Tables[0].Rows[0]["PointerContent"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SearchCity"] != null) && (set.Tables[0].Rows[0]["SearchCity"].ToString() != ""))
            {
                info.SearchCity = set.Tables[0].Rows[0]["SearchCity"].ToString();
            }
            if ((set.Tables[0].Rows[0]["searchArea"] != null) && (set.Tables[0].Rows[0]["searchArea"].ToString() != ""))
            {
                info.searchArea = set.Tables[0].Rows[0]["searchArea"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Level"] != null) && (set.Tables[0].Rows[0]["Level"].ToString() != ""))
            {
                info.Level = new int?(int.Parse(set.Tables[0].Rows[0]["Level"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["enableKeyboard"] != null) && (set.Tables[0].Rows[0]["enableKeyboard"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["enableKeyboard"].ToString() == "1") || (set.Tables[0].Rows[0]["enableKeyboard"].ToString().ToLower() == "true"))
                {
                    info.enableKeyboard = true;
                }
                else
                {
                    info.enableKeyboard = false;
                }
            }
            if ((set.Tables[0].Rows[0]["enableScrollWheelZoom"] != null) && (set.Tables[0].Rows[0]["enableScrollWheelZoom"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["enableScrollWheelZoom"].ToString() == "1") || (set.Tables[0].Rows[0]["enableScrollWheelZoom"].ToString().ToLower() == "true"))
                {
                    info.enableScrollWheelZoom = true;
                }
                else
                {
                    info.enableScrollWheelZoom = false;
                }
            }
            if ((set.Tables[0].Rows[0]["NavigationControl"] != null) && (set.Tables[0].Rows[0]["NavigationControl"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["NavigationControl"].ToString() == "1") || (set.Tables[0].Rows[0]["NavigationControl"].ToString().ToLower() == "true"))
                {
                    info.NavigationControl = true;
                }
                else
                {
                    info.NavigationControl = false;
                }
            }
            if ((set.Tables[0].Rows[0]["ScaleControl"] != null) && (set.Tables[0].Rows[0]["ScaleControl"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["ScaleControl"].ToString() == "1") || (set.Tables[0].Rows[0]["ScaleControl"].ToString().ToLower() == "true"))
                {
                    info.ScaleControl = true;
                }
                else
                {
                    info.ScaleControl = false;
                }
            }
            if ((set.Tables[0].Rows[0]["MapTypeControl"] != null) && (set.Tables[0].Rows[0]["MapTypeControl"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["MapTypeControl"].ToString() == "1") || (set.Tables[0].Rows[0]["MapTypeControl"].ToString().ToLower() == "true"))
                {
                    info.MapTypeControl = true;
                }
                else
                {
                    info.MapTypeControl = false;
                }
            }
            if ((set.Tables[0].Rows[0]["MarkersLongitude"] != null) && (set.Tables[0].Rows[0]["MarkersLongitude"].ToString() != ""))
            {
                info.MarkersLongitude = set.Tables[0].Rows[0]["MarkersLongitude"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MarkersDimension"] != null) && (set.Tables[0].Rows[0]["MarkersDimension"].ToString() != ""))
            {
                info.MarkersDimension = set.Tables[0].Rows[0]["MarkersDimension"].ToString();
            }
            if ((set.Tables[0].Rows[0]["setAnimation"] != null) && (set.Tables[0].Rows[0]["setAnimation"].ToString() != ""))
            {
                info.setAnimation = set.Tables[0].Rows[0]["setAnimation"].ToString();
            }
            if ((set.Tables[0].Rows[0]["LoadEvent"] != null) && (set.Tables[0].Rows[0]["LoadEvent"].ToString() != ""))
            {
                info.LoadEvent = set.Tables[0].Rows[0]["LoadEvent"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MenuItemzoomIn"] != null) && (set.Tables[0].Rows[0]["MenuItemzoomIn"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["MenuItemzoomIn"].ToString() == "1") || (set.Tables[0].Rows[0]["MenuItemzoomIn"].ToString().ToLower() == "true"))
                {
                    info.MenuItemzoomIn = true;
                }
                else
                {
                    info.MenuItemzoomIn = false;
                }
            }
            if ((set.Tables[0].Rows[0]["MenuItemzoomOut"] != null) && (set.Tables[0].Rows[0]["MenuItemzoomOut"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["MenuItemzoomOut"].ToString() == "1") || (set.Tables[0].Rows[0]["MenuItemzoomOut"].ToString().ToLower() == "true"))
                {
                    info.MenuItemzoomOut = true;
                }
                else
                {
                    info.MenuItemzoomOut = false;
                }
            }
            if ((set.Tables[0].Rows[0]["MenuItemsetZoomTop"] != null) && (set.Tables[0].Rows[0]["MenuItemsetZoomTop"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["MenuItemsetZoomTop"].ToString() == "1") || (set.Tables[0].Rows[0]["MenuItemsetZoomTop"].ToString().ToLower() == "true"))
                {
                    info.MenuItemsetZoomTop = true;
                }
                else
                {
                    info.MenuItemsetZoomTop = false;
                }
            }
            if ((set.Tables[0].Rows[0]["MenuItemsetPoint"] != null) && (set.Tables[0].Rows[0]["MenuItemsetPoint"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["MenuItemsetPoint"].ToString() == "1") || (set.Tables[0].Rows[0]["MenuItemsetPoint"].ToString().ToLower() == "true"))
                {
                    info.MenuItemsetPoint = true;
                }
                else
                {
                    info.MenuItemsetPoint = false;
                }
            }
            if ((set.Tables[0].Rows[0]["MapType"] != null) && (set.Tables[0].Rows[0]["MapType"].ToString() != ""))
            {
                info.MapType = int.Parse(set.Tables[0].Rows[0]["MapType"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Other1"] != null) && (set.Tables[0].Rows[0]["Other1"].ToString() != ""))
            {
                info.Other1 = set.Tables[0].Rows[0]["Other1"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Other2"] != null) && (set.Tables[0].Rows[0]["Other2"].ToString() != ""))
            {
                info.Other2 = set.Tables[0].Rows[0]["Other2"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Other3"] != null) && (set.Tables[0].Rows[0]["Other3"].ToString() != ""))
            {
                info.Other3 = set.Tables[0].Rows[0]["Other3"].ToString();
            }
            return info;
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("MapId", "Ms_Maps");
        }

        public MapInfo GetModel(int MapId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MapId,UserId,DepartmentId,PointerLongitude,PointDimension,PointClass,PointerType,PointerTitle,PointImg,PointerContent,SearchCity,searchArea,Level,enableKeyboard,enableScrollWheelZoom,NavigationControl,ScaleControl,MapTypeControl,MarkersLongitude,MarkersDimension,setAnimation,LoadEvent,MenuItemzoomIn,MenuItemzoomOut,MenuItemsetZoomTop,MenuItemsetPoint,MapType,Other1,Other2,Other3 from Ms_Maps ");
            strSql.Append(" where MapId=@MapId");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MapId", SqlDbType.Int, 4) };
            parameters[0].Value = MapId;
            return GetMapInfo(strSql, parameters);
        }

        public MapInfo GetModelByDepartmentId(int departmentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MapId,UserId,DepartmentId,PointerLongitude,PointDimension,PointClass,PointerType,PointerTitle,PointImg,PointerContent,SearchCity,searchArea,Level,enableKeyboard,enableScrollWheelZoom,NavigationControl,ScaleControl,MapTypeControl,MarkersLongitude,MarkersDimension,setAnimation,LoadEvent,MenuItemzoomIn,MenuItemzoomOut,MenuItemsetZoomTop,MenuItemsetPoint,MapType,Other1,Other2,Other3 from Ms_Maps ");
            strSql.Append(" where DepartmentId=@DepartmentId");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@DepartmentId", SqlDbType.Int, 4) };
            parameters[0].Value = departmentId;
            return GetMapInfo(strSql, parameters);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Ms_Maps ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(MapInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_Maps set ");
            builder.Append("UserId=@UserId,");
            builder.Append("DepartmentId=@DepartmentId,");
            builder.Append("PointerLongitude=@PointerLongitude,");
            builder.Append("PointDimension=@PointDimension,");
            builder.Append("PointClass=@PointClass,");
            builder.Append("PointerType=@PointerType,");
            builder.Append("PointerTitle=@PointerTitle,");
            builder.Append("PointImg=@PointImg,");
            builder.Append("PointerContent=@PointerContent,");
            builder.Append("SearchCity=@SearchCity,");
            builder.Append("searchArea=@searchArea,");
            builder.Append("Level=@Level,");
            builder.Append("enableKeyboard=@enableKeyboard,");
            builder.Append("enableScrollWheelZoom=@enableScrollWheelZoom,");
            builder.Append("NavigationControl=@NavigationControl,");
            builder.Append("ScaleControl=@ScaleControl,");
            builder.Append("MapTypeControl=@MapTypeControl,");
            builder.Append("MarkersLongitude=@MarkersLongitude,");
            builder.Append("MarkersDimension=@MarkersDimension,");
            builder.Append("setAnimation=@setAnimation,");
            builder.Append("LoadEvent=@LoadEvent,");
            builder.Append("MenuItemzoomIn=@MenuItemzoomIn,");
            builder.Append("MenuItemzoomOut=@MenuItemzoomOut,");
            builder.Append("MenuItemsetZoomTop=@MenuItemsetZoomTop,");
            builder.Append("MenuItemsetPoint=@MenuItemsetPoint,");
            builder.Append("MapType=@MapType,");
            builder.Append("Other1=@Other1,");
            builder.Append("Other2=@Other2,");
            builder.Append("Other3=@Other3");
            builder.Append(" where MapId=@MapId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@DepartmentId", SqlDbType.Int, 4), new SqlParameter("@PointerLongitude", SqlDbType.NVarChar, 100), new SqlParameter("@PointDimension", SqlDbType.NVarChar, 100), new SqlParameter("@PointClass", SqlDbType.NVarChar, 100), new SqlParameter("@PointerType", SqlDbType.NVarChar, 100), new SqlParameter("@PointerTitle", SqlDbType.NVarChar, 100), new SqlParameter("@PointImg", SqlDbType.NVarChar, 100), new SqlParameter("@PointerContent", SqlDbType.NVarChar, 300), new SqlParameter("@SearchCity", SqlDbType.NVarChar, 50), new SqlParameter("@searchArea", SqlDbType.NVarChar, 50), new SqlParameter("@Level", SqlDbType.Int, 4), new SqlParameter("@enableKeyboard", SqlDbType.Bit, 1), new SqlParameter("@enableScrollWheelZoom", SqlDbType.Bit, 1), new SqlParameter("@NavigationControl", SqlDbType.Bit, 1), new SqlParameter("@ScaleControl", SqlDbType.Bit, 1), 
                new SqlParameter("@MapTypeControl", SqlDbType.Bit, 1), new SqlParameter("@MarkersLongitude", SqlDbType.NVarChar, 100), new SqlParameter("@MarkersDimension", SqlDbType.NVarChar, 100), new SqlParameter("@setAnimation", SqlDbType.NVarChar, 100), new SqlParameter("@LoadEvent", SqlDbType.NVarChar, 50), new SqlParameter("@MenuItemzoomIn", SqlDbType.Bit, 1), new SqlParameter("@MenuItemzoomOut", SqlDbType.Bit, 1), new SqlParameter("@MenuItemsetZoomTop", SqlDbType.Bit, 1), new SqlParameter("@MenuItemsetPoint", SqlDbType.Bit, 1), new SqlParameter("@MapType", SqlDbType.SmallInt, 2), new SqlParameter("@Other1", SqlDbType.NVarChar, 100), new SqlParameter("@Other2", SqlDbType.NVarChar, 100), new SqlParameter("@Other3", SqlDbType.NVarChar, 50), new SqlParameter("@MapId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.DepartmentId;
            cmdParms[2].Value = model.PointerLongitude;
            cmdParms[3].Value = model.PointDimension;
            cmdParms[4].Value = model.PointClass;
            cmdParms[5].Value = model.PointerType;
            cmdParms[6].Value = model.PointerTitle;
            cmdParms[7].Value = model.PointImg;
            cmdParms[8].Value = model.PointerContent;
            cmdParms[9].Value = model.SearchCity;
            cmdParms[10].Value = model.searchArea;
            cmdParms[11].Value = model.Level;
            cmdParms[12].Value = model.enableKeyboard;
            cmdParms[13].Value = model.enableScrollWheelZoom;
            cmdParms[14].Value = model.NavigationControl;
            cmdParms[15].Value = model.ScaleControl;
            cmdParms[0x10].Value = model.MapTypeControl;
            cmdParms[0x11].Value = model.MarkersLongitude;
            cmdParms[0x12].Value = model.MarkersDimension;
            cmdParms[0x13].Value = model.setAnimation;
            cmdParms[20].Value = model.LoadEvent;
            cmdParms[0x15].Value = model.MenuItemzoomIn;
            cmdParms[0x16].Value = model.MenuItemzoomOut;
            cmdParms[0x17].Value = model.MenuItemsetZoomTop;
            cmdParms[0x18].Value = model.MenuItemsetPoint;
            cmdParms[0x19].Value = model.MapType;
            cmdParms[0x1a].Value = model.Other1;
            cmdParms[0x1b].Value = model.Other2;
            cmdParms[0x1c].Value = model.Other3;
            cmdParms[0x1d].Value = model.MapId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

