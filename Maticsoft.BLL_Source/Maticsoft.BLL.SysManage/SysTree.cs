namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SysTree
    {
        private readonly ISysTree dal = DASysManage.CreateSysTree();

        public void AddLog(string time, string loginfo, string Particular)
        {
            this.dal.AddLog(time, loginfo, Particular);
        }

        public int AddTreeNode(SysNode node)
        {
            return this.dal.AddTreeNode(node);
        }

        public void DeleteLog(string Idlist)
        {
            string strWhere = "";
            if (Idlist.Trim() != "")
            {
                strWhere = " ID in (" + Idlist + ")";
            }
            this.dal.DeleteLog(strWhere);
        }

        public void DeleteLog(string timestart, string timeend)
        {
            string strWhere = " datetime>'" + timestart + "' and datetime<'" + timeend + "'";
            this.dal.DeleteLog(strWhere);
        }

        public void DelOverdueLog(int days)
        {
            this.dal.DelOverdueLog(days);
        }

        public void DelTreeNode(int nodeid)
        {
            this.dal.DelTreeNode(nodeid);
        }

        public void DelTreeNodes(string nodeidlist)
        {
            this.dal.DelTreeNodes(nodeidlist);
        }

        public DataSet GetAllEnabledTreeByType(int treeType)
        {
            return this.GetAllEnabledTreeByType(treeType, true);
        }

        public DataSet GetAllEnabledTreeByType(int treeType, bool Enabled)
        {
            return this.dal.GetTreeList(string.Concat(new object[] { "TreeType=", treeType, " AND Enabled = ", Enabled ? "1" : "0" }));
        }

        public DataSet GetAllEnabledTreeByType4Cache(int treeType)
        {
            string cacheKey = "GetAllEnabledTreeByType4Cache" + treeType;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetAllEnabledTreeByType(treeType);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (DataSet) cache;
        }

        public DataSet GetAllTree()
        {
            return this.dal.GetTreeList("");
        }

        public DataSet GetAllTreeByType(int treeType)
        {
            return this.dal.GetTreeList("TreeType=" + treeType);
        }

        public DataSet GetEnabledTreeByParentId(int parentID, int treeType, bool Enabled)
        {
            return this.dal.GetTreeList(string.Concat(new object[] { "ParentID=", parentID, " AND TreeType=", treeType, " AND Enabled = ", Enabled ? "1" : "0" }));
        }

        public DataRow GetLog(string ID)
        {
            return this.dal.GetLog(ID);
        }

        public DataSet GetLogs(string strWhere)
        {
            return this.dal.GetLogs(strWhere);
        }

        public SysNode GetModelByCache(int NodeID)
        {
            string cacheKey = "SysManageModel-" + NodeID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetNode(NodeID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (SysNode) cache;
        }

        public SysNode GetNode(int NodeID)
        {
            return this.dal.GetNode(NodeID);
        }

        public int GetPermissionCatalogID(int permissionID)
        {
            return this.dal.GetPermissionCatalogID(permissionID);
        }

        public DataSet GetTreeList(string strWhere)
        {
            return this.dal.GetTreeList(strWhere);
        }

        public DataSet GetTreeSonList(int NodeID, int treeType, List<int> UserPermissions)
        {
            string strWhere = " Enabled=1 and TreeType=" + treeType;
            if (NodeID > -1)
            {
                strWhere = strWhere + " and parentid=" + NodeID;
            }
            if (UserPermissions.Count > 0)
            {
                strWhere = strWhere + " and (PermissionID=-1 or PermissionID in (" + StringPlus.GetArrayStr(UserPermissions) + "))";
            }
            return this.dal.GetTreeList(strWhere);
        }

        public void MoveNodes(string nodeidlist, int ParentID)
        {
            this.dal.MoveNodes(nodeidlist, ParentID);
        }

        public void UpdateEnabled(int nodeid)
        {
            this.dal.UpdateEnabled(nodeid);
        }

        public void UpdateNode(SysNode node)
        {
            this.dal.UpdateNode(node);
        }
    }
}

