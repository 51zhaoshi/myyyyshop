namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.IDAL.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class TreeFavorite
    {
        private readonly ITreeFavorite dal = DASysManage.CreateTreeFavorite();

        public int Add(int UserID, int NodeID)
        {
            return this.dal.Add(UserID, NodeID);
        }

        public void Delete(int ID)
        {
            this.dal.Delete(ID);
        }

        public void Delete(int UserID, int NodeID)
        {
            this.dal.Delete(UserID, NodeID);
        }

        public void DeleteByUser(int UserID)
        {
            this.dal.DeleteByUser(UserID);
        }

        public bool Exists(int UserID, int NodeID)
        {
            return this.dal.Exists(UserID, NodeID);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetMenuListByUser(int UserID)
        {
            return this.dal.GetMenuListByUser(UserID);
        }

        public List<int> GetNodeIDsByUser(int UserID)
        {
            List<int> list = new List<int>();
            DataSet nodeIDsByUser = this.dal.GetNodeIDsByUser(UserID);
            if (nodeIDsByUser.Tables.Count > 0)
            {
                foreach (DataRow row in nodeIDsByUser.Tables[0].Rows)
                {
                    int item = Convert.ToInt32(row["NodeID"]);
                    list.Add(item);
                }
            }
            return list;
        }

        public void UpDate(int OrderID, int UserID, int NodeID)
        {
            this.dal.UpDate(OrderID, UserID, NodeID);
        }
    }
}

