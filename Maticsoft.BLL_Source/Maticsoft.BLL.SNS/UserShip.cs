namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class UserShip
    {
        private readonly IUserShip dal = DASNS.CreateUserShip();

        public bool Add(Maticsoft.Model.SNS.UserShip model)
        {
            return this.dal.Add(model);
        }

        public bool AddAttention(int ActiveUserID, int PassiveUserID)
        {
            return this.dal.AddAttention(ActiveUserID, PassiveUserID);
        }

        public bool CancelAttention(int ActiveUserID, int PassiveUserID)
        {
            return this.dal.CancelAttention(ActiveUserID, PassiveUserID);
        }

        public bool Delete(int ActiveUserID, int PassiveUserID)
        {
            return this.dal.Delete(ActiveUserID, PassiveUserID);
        }

        public bool Exists(int ActiveUserID, int PassiveUserID)
        {
            return this.dal.Exists(ActiveUserID, PassiveUserID);
        }

        public bool FellowUser(int Userid, int FellowUserId)
        {
            Maticsoft.Model.SNS.UserShip model = new Maticsoft.Model.SNS.UserShip {
                ActiveUserID = Userid,
                PassiveUserID = FellowUserId,
                CreatedDate = DateTime.Now,
                IsRead = false,
                State = 1,
                Type = 0
            };
            return this.dal.FellowUser(model);
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

        public DataSet GetListByFansPage(int userid, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByFansPage(userid, orderby, startIndex, endIndex);
        }

        public DataSet GetListByFellowsPage(int userid, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByFellowsPage(userid, orderby, startIndex, endIndex);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.SNS.UserShip GetModel(int ActiveUserID, int PassiveUserID)
        {
            return this.dal.GetModel(ActiveUserID, PassiveUserID);
        }

        public Maticsoft.Model.SNS.UserShip GetModelByCache(int ActiveUserID, int PassiveUserID)
        {
            string cacheKey = "UserShipModel-" + ActiveUserID + PassiveUserID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ActiveUserID, PassiveUserID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.UserShip) cache;
        }

        public List<Maticsoft.Model.SNS.UserShip> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            if (DataSetTools.DataSetIsNull(list))
            {
                return null;
            }
            return this.dal.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.SNS.UserShip> GetToListByFansPage(int userid, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = this.dal.GetListByFansPage(userid, orderby, startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.dal.DataTableToListEx(ds.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.UserShip> GetToListByFellowsPage(int userid, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = this.dal.GetListByFellowsPage(userid, orderby, startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.dal.DataTableToListEx(ds.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.UserShip> GetUserFellowList(int UserId)
        {
            return this.GetModelList("ActiveUserID = " + UserId);
        }

        public List<Maticsoft.Model.SNS.UserShip> GetUsersAllFansByPage(int userid, int startIndex, int endIndex, int CurrentUserId)
        {
            List<Maticsoft.Model.SNS.UserShip> list = this.GetToListByFansPage(userid, "", startIndex, endIndex);
            if (((CurrentUserId != 0) && (list != null)) && (list.Count > 0))
            {
                Action<Maticsoft.Model.SNS.UserShip> action = null;
                List<Maticsoft.Model.SNS.UserShip> shipList = this.GetUserFellowList(CurrentUserId);
                if ((shipList == null) || (shipList.Count <= 0))
                {
                    return list;
                }
                if (action == null)
                {
                    action = delegate (Maticsoft.Model.SNS.UserShip item) {
                        item.IsFellow = this.UserIsFellow(item.ActiveUserID, shipList);
                    };
                }
                list.ForEach(action);
            }
            return list;
        }

        public int GetUsersAllFansRecordCount(int userid)
        {
            return this.dal.GetRecordCount(string.Format(" PassiveUserID={0}", userid));
        }

        public List<Maticsoft.Model.SNS.UserShip> GetUsersAllFellowsByPage(int userid, int startIndex, int endIndex, int CurrentUserId)
        {
            List<Maticsoft.Model.SNS.UserShip> list = this.GetToListByFellowsPage(userid, "", startIndex, endIndex);
            if (((CurrentUserId != 0) && (list != null)) && (list.Count > 0))
            {
                Action<Maticsoft.Model.SNS.UserShip> action = null;
                List<Maticsoft.Model.SNS.UserShip> shipList = this.GetUserFellowList(CurrentUserId);
                if ((shipList == null) || (shipList.Count <= 0))
                {
                    return list;
                }
                if (action == null)
                {
                    action = delegate (Maticsoft.Model.SNS.UserShip item) {
                        item.IsFellow = this.UserIsFellow(item.PassiveUserID, shipList);
                    };
                }
                list.ForEach(action);
            }
            return list;
        }

        public int GetUsersAllFellowsRecordCount(int userid)
        {
            return this.dal.GetRecordCount(string.Format(" ActiveUserID={0}", userid));
        }

        public void GiveUserFellow(int UserId)
        {
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FansList");
            Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
            if (!string.IsNullOrEmpty(valueByCache))
            {
                foreach (string str2 in valueByCache.Split(new char[] { ',' }))
                {
                    int userID = Globals.SafeInt(str2, 0);
                    if (users.Exists(userID))
                    {
                        this.AddAttention(userID, UserId);
                    }
                }
            }
        }

        public void GiveUserFellow(int UserId, int Top)
        {
            Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
            List<Maticsoft.Model.Members.Users> list = new List<Maticsoft.Model.Members.Users>();
            foreach (Maticsoft.Model.Members.Users users2 in users.DataTableToList(users.GetList(Top, "UserType='UU' and UserID<>" + UserId + " ", "newid()").Tables[0]))
            {
                this.AddAttention(users2.UserID, UserId);
            }
        }

        public bool UnFellowUser(int Userid, int FellowUserId)
        {
            return this.dal.UnFellowUser(Userid, FellowUserId);
        }

        public bool Update(Maticsoft.Model.SNS.UserShip model)
        {
            return this.dal.Update(model);
        }

        public bool UserIsFellow(int UserId, List<Maticsoft.Model.SNS.UserShip> shipList)
        {
            return ((shipList != null) && (shipList.Count<Maticsoft.Model.SNS.UserShip>(item => (item.PassiveUserID == UserId)) > 0));
        }
    }
}

