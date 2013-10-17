namespace Maticsoft.IDAL.SysManage
{
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;

    public interface ISysTree
    {
        void AddLog(string time, string loginfo, string Particular);
        int AddTreeNode(SysNode model);
        void DeleteLog(int ID);
        void DeleteLog(string strWhere);
        void DelOverdueLog(int days);
        void DelTreeNode(int NodeID);
        void DelTreeNodes(string nodeidlist);
        DataRow GetLog(string ID);
        DataSet GetLogs(string strWhere);
        SysNode GetNode(int NodeID);
        int GetPermissionCatalogID(int permissionID);
        DataSet GetTreeList(string strWhere);
        void MoveNodes(string nodeidlist, int ParentID);
        void UpdateEnabled(int nodeid);
        void UpdateNode(SysNode model);
    }
}

