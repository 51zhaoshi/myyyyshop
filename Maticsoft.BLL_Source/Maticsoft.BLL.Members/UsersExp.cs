namespace Maticsoft.BLL.Members
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UsersExp : UsersExpModel
    {
        private readonly IUsersExp dal = DAMembers.CreateUsersExp();

        public bool Add(int userId)
        {
            return this.dal.Add(userId);
        }

        public bool AddEx(UsersExpModel model, int inviteID, string inviteNick, int pointScore)
        {
            return this.dal.AddEx(model, inviteID, inviteNick, pointScore);
        }

        public bool AddExp(UsersExpModel model, int inviteuid)
        {
            if (inviteuid > 0)
            {
                string nickName = new Maticsoft.BLL.Members.Users().GetNickName(inviteuid);
                if (!string.IsNullOrWhiteSpace(nickName))
                {
                    int pointScore = new Maticsoft.BLL.Members.PointsDetail().AddPoints("Invite", inviteuid, "邀请用户", "");
                    return this.dal.AddEx(model, inviteuid, nickName, pointScore);
                }
            }
            return this.dal.Add(model);
        }

        public bool AddUsersExp(UsersExpModel model)
        {
            return this.dal.Add(model);
        }

        public List<UsersExpModel> DataTableToList(DataTable dt)
        {
            List<UsersExpModel> list = new List<UsersExpModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    UsersExpModel item = new UsersExpModel();
                    if ((dt.Rows[i]["UserID"] != null) && (dt.Rows[i]["UserID"].ToString() != ""))
                    {
                        item.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                    }
                    if ((dt.Rows[i]["Gravatar"] != null) && (dt.Rows[i]["Gravatar"].ToString() != ""))
                    {
                        item.Gravatar = dt.Rows[i]["Gravatar"].ToString();
                    }
                    if ((dt.Rows[i]["Singature"] != null) && (dt.Rows[i]["Singature"].ToString() != ""))
                    {
                        item.Singature = dt.Rows[i]["Singature"].ToString();
                    }
                    if ((dt.Rows[i]["TelPhone"] != null) && (dt.Rows[i]["TelPhone"].ToString() != ""))
                    {
                        item.TelPhone = dt.Rows[i]["TelPhone"].ToString();
                    }
                    if ((dt.Rows[i]["QQ"] != null) && (dt.Rows[i]["QQ"].ToString() != ""))
                    {
                        item.QQ = dt.Rows[i]["QQ"].ToString();
                    }
                    if ((dt.Rows[i]["MSN"] != null) && (dt.Rows[i]["MSN"].ToString() != ""))
                    {
                        item.MSN = dt.Rows[i]["MSN"].ToString();
                    }
                    if ((dt.Rows[i]["HomePage"] != null) && (dt.Rows[i]["HomePage"].ToString() != ""))
                    {
                        item.HomePage = dt.Rows[i]["HomePage"].ToString();
                    }
                    if ((dt.Rows[i]["Birthday"] != null) && (dt.Rows[i]["Birthday"].ToString() != ""))
                    {
                        item.Birthday = new DateTime?(DateTime.Parse(dt.Rows[i]["Birthday"].ToString()));
                    }
                    if ((dt.Rows[i]["BirthdayVisible"] != null) && (dt.Rows[i]["BirthdayVisible"].ToString() != ""))
                    {
                        item.BirthdayVisible = int.Parse(dt.Rows[i]["BirthdayVisible"].ToString());
                    }
                    if ((dt.Rows[i]["BirthdayIndexVisible"] != null) && (dt.Rows[i]["BirthdayIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["BirthdayIndexVisible"].ToString() == "1") || (dt.Rows[i]["BirthdayIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.BirthdayIndexVisible = true;
                        }
                        else
                        {
                            item.BirthdayIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["Constellation"] != null) && (dt.Rows[i]["Constellation"].ToString() != ""))
                    {
                        item.Constellation = dt.Rows[i]["Constellation"].ToString();
                    }
                    if ((dt.Rows[i]["ConstellationVisible"] != null) && (dt.Rows[i]["ConstellationVisible"].ToString() != ""))
                    {
                        item.ConstellationVisible = int.Parse(dt.Rows[i]["ConstellationVisible"].ToString());
                    }
                    if ((dt.Rows[i]["ConstellationIndexVisible"] != null) && (dt.Rows[i]["ConstellationIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["ConstellationIndexVisible"].ToString() == "1") || (dt.Rows[i]["ConstellationIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.ConstellationIndexVisible = true;
                        }
                        else
                        {
                            item.ConstellationIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["NativePlace"] != null) && (dt.Rows[i]["NativePlace"].ToString() != ""))
                    {
                        item.NativePlace = dt.Rows[i]["NativePlace"].ToString();
                    }
                    if ((dt.Rows[i]["NativePlaceVisible"] != null) && (dt.Rows[i]["NativePlaceVisible"].ToString() != ""))
                    {
                        item.NativePlaceVisible = int.Parse(dt.Rows[i]["NativePlaceVisible"].ToString());
                    }
                    if ((dt.Rows[i]["NativePlaceIndexVisible"] != null) && (dt.Rows[i]["NativePlaceIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["NativePlaceIndexVisible"].ToString() == "1") || (dt.Rows[i]["NativePlaceIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.NativePlaceIndexVisible = true;
                        }
                        else
                        {
                            item.NativePlaceIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["RegionId"] != null) && (dt.Rows[i]["RegionId"].ToString() != ""))
                    {
                        item.RegionId = new int?(int.Parse(dt.Rows[i]["RegionId"].ToString()));
                    }
                    if ((dt.Rows[i]["Address"] != null) && (dt.Rows[i]["Address"].ToString() != ""))
                    {
                        item.Address = dt.Rows[i]["Address"].ToString();
                    }
                    if ((dt.Rows[i]["AddressVisible"] != null) && (dt.Rows[i]["AddressVisible"].ToString() != ""))
                    {
                        item.AddressVisible = int.Parse(dt.Rows[i]["AddressVisible"].ToString());
                    }
                    if ((dt.Rows[i]["AddressIndexVisible"] != null) && (dt.Rows[i]["AddressIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["AddressIndexVisible"].ToString() == "1") || (dt.Rows[i]["AddressIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.AddressIndexVisible = true;
                        }
                        else
                        {
                            item.AddressIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["BodilyForm"] != null) && (dt.Rows[i]["BodilyForm"].ToString() != ""))
                    {
                        item.BodilyForm = dt.Rows[i]["BodilyForm"].ToString();
                    }
                    if ((dt.Rows[i]["BodilyFormVisible"] != null) && (dt.Rows[i]["BodilyFormVisible"].ToString() != ""))
                    {
                        item.BodilyFormVisible = int.Parse(dt.Rows[i]["BodilyFormVisible"].ToString());
                    }
                    if ((dt.Rows[i]["BodilyFormIndexVisible"] != null) && (dt.Rows[i]["BodilyFormIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["BodilyFormIndexVisible"].ToString() == "1") || (dt.Rows[i]["BodilyFormIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.BodilyFormIndexVisible = true;
                        }
                        else
                        {
                            item.BodilyFormIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["BloodType"] != null) && (dt.Rows[i]["BloodType"].ToString() != ""))
                    {
                        item.BloodType = dt.Rows[i]["BloodType"].ToString();
                    }
                    if ((dt.Rows[i]["BloodTypeVisible"] != null) && (dt.Rows[i]["BloodTypeVisible"].ToString() != ""))
                    {
                        item.BloodTypeVisible = int.Parse(dt.Rows[i]["BloodTypeVisible"].ToString());
                    }
                    if ((dt.Rows[i]["BloodTypeIndexVisible"] != null) && (dt.Rows[i]["BloodTypeIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["BloodTypeIndexVisible"].ToString() == "1") || (dt.Rows[i]["BloodTypeIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.BloodTypeIndexVisible = true;
                        }
                        else
                        {
                            item.BloodTypeIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["Marriaged"] != null) && (dt.Rows[i]["Marriaged"].ToString() != ""))
                    {
                        item.Marriaged = dt.Rows[i]["Marriaged"].ToString();
                    }
                    if ((dt.Rows[i]["MarriagedVisible"] != null) && (dt.Rows[i]["MarriagedVisible"].ToString() != ""))
                    {
                        item.MarriagedVisible = int.Parse(dt.Rows[i]["MarriagedVisible"].ToString());
                    }
                    if ((dt.Rows[i]["MarriagedIndexVisible"] != null) && (dt.Rows[i]["MarriagedIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["MarriagedIndexVisible"].ToString() == "1") || (dt.Rows[i]["MarriagedIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.MarriagedIndexVisible = true;
                        }
                        else
                        {
                            item.MarriagedIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["PersonalStatus"] != null) && (dt.Rows[i]["PersonalStatus"].ToString() != ""))
                    {
                        item.PersonalStatus = dt.Rows[i]["PersonalStatus"].ToString();
                    }
                    if ((dt.Rows[i]["PersonalStatusVisible"] != null) && (dt.Rows[i]["PersonalStatusVisible"].ToString() != ""))
                    {
                        item.PersonalStatusVisible = int.Parse(dt.Rows[i]["PersonalStatusVisible"].ToString());
                    }
                    if ((dt.Rows[i]["PersonalStatusIndexVisible"] != null) && (dt.Rows[i]["PersonalStatusIndexVisible"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["PersonalStatusIndexVisible"].ToString() == "1") || (dt.Rows[i]["PersonalStatusIndexVisible"].ToString().ToLower() == "true"))
                        {
                            item.PersonalStatusIndexVisible = true;
                        }
                        else
                        {
                            item.PersonalStatusIndexVisible = false;
                        }
                    }
                    if ((dt.Rows[i]["Grade"] != null) && (dt.Rows[i]["Grade"].ToString() != ""))
                    {
                        item.Grade = new int?(int.Parse(dt.Rows[i]["Grade"].ToString()));
                    }
                    if ((dt.Rows[i]["Balance"] != null) && (dt.Rows[i]["Balance"].ToString() != ""))
                    {
                        item.Balance = new decimal?(decimal.Parse(dt.Rows[i]["Balance"].ToString()));
                    }
                    if ((dt.Rows[i]["Points"] != null) && (dt.Rows[i]["Points"].ToString() != ""))
                    {
                        item.Points = new int?(int.Parse(dt.Rows[i]["Points"].ToString()));
                    }
                    if ((dt.Rows[i]["PvCount"] != null) && (dt.Rows[i]["PvCount"].ToString() != ""))
                    {
                        item.PvCount = new int?(int.Parse(dt.Rows[i]["PvCount"].ToString()));
                    }
                    if ((dt.Rows[i]["LastAccessTime"] != null) && (dt.Rows[i]["LastAccessTime"].ToString() != ""))
                    {
                        item.LastAccessTime = new DateTime?(DateTime.Parse(dt.Rows[i]["LastAccessTime"].ToString()));
                    }
                    if ((dt.Rows[i]["LastAccessIP"] != null) && (dt.Rows[i]["LastAccessIP"].ToString() != ""))
                    {
                        item.LastAccessIP = dt.Rows[i]["LastAccessIP"].ToString();
                    }
                    if ((dt.Rows[i]["LastPostTime"] != null) && (dt.Rows[i]["LastPostTime"].ToString() != ""))
                    {
                        item.LastPostTime = new DateTime?(DateTime.Parse(dt.Rows[i]["LastPostTime"].ToString()));
                    }
                    if ((dt.Rows[i]["LastLoginTime"] != null) && (dt.Rows[i]["LastLoginTime"].ToString() != ""))
                    {
                        item.LastLoginTime = DateTime.Parse(dt.Rows[i]["LastLoginTime"].ToString());
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    if ((dt.Rows[i]["NickName"] != null) && (dt.Rows[i]["NickName"].ToString() != ""))
                    {
                        item.NickName = dt.Rows[i]["NickName"].ToString();
                    }
                    if ((dt.Rows[i]["FellowCount"] != null) && (dt.Rows[i]["FellowCount"].ToString() != ""))
                    {
                        item.FellowCount = new int?(int.Parse(dt.Rows[i]["FellowCount"].ToString()));
                    }
                    if ((dt.Rows[i]["FansCount"] != null) && (dt.Rows[i]["FansCount"].ToString() != ""))
                    {
                        item.FansCount = new int?(int.Parse(dt.Rows[i]["FansCount"].ToString()));
                    }
                    if ((dt.Rows[i]["AblumsCount"] != null) && (dt.Rows[i]["AblumsCount"].ToString() != ""))
                    {
                        item.AblumsCount = new int?(int.Parse(dt.Rows[i]["AblumsCount"].ToString()));
                    }
                    if ((dt.Rows[i]["ShareCount"] != null) && (dt.Rows[i]["ShareCount"].ToString() != ""))
                    {
                        item.ShareCount = new int?(int.Parse(dt.Rows[i]["ShareCount"].ToString()));
                    }
                    if ((dt.Rows[i]["IsUserDPI"] != null) && (dt.Rows[i]["IsUserDPI"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsUserDPI"].ToString() == "1") || (dt.Rows[i]["IsUserDPI"].ToString().ToLower() == "true"))
                        {
                            item.IsUserDPI = true;
                        }
                        else
                        {
                            item.IsUserDPI = false;
                        }
                    }
                    if (dt.Rows[i]["PayAccount"] != null)
                    {
                        item.PayAccount = dt.Rows[i]["PayAccount"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool DeleteListUsersExp(string UserIDlist)
        {
            return this.dal.DeleteList(UserIDlist);
        }

        public bool DeleteUsersExp(int UserID)
        {
            return this.dal.Delete(UserID);
        }

        public DataSet GetAllEmpByUserId(int userId)
        {
            return this.dal.GetAllEmpByUserId(userId);
        }

        public List<UsersExpModel> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public decimal GetUserBalance(int UserId)
        {
            return this.dal.GetUserBalance(UserId);
        }

        public int GetUserCountByKeyWord(string NickName)
        {
            return this.dal.GetUserCountByKeyWord(NickName);
        }

        public List<UsersExpModel> GetUserListByKeyWord(string NickName, string orderby, int startIndex, int endIndex)
        {
            return this.DataTableToList(this.dal.GetUserListByKeyWord(NickName, orderby, startIndex, endIndex).Tables[0]);
        }

        public DataSet GetUserName(string strUName, int iCount)
        {
            string strWhere = "UserName like '" + strUName + "%' AND Activity=1";
            return this.dal.GetUserList(iCount, strWhere, "UserName");
        }

        public int GetUserRankId(int UserId)
        {
            return this.dal.GetUserRankId(UserId);
        }

        public DataSet GetUsersExpList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public UsersExpModel GetUsersExpModel(int UserID)
        {
            return this.dal.GetModel(UserID);
        }

        public UsersExpModel GetUsersExpModelByCache(int UserID)
        {
            string cacheKey = "UsersExpModel-" + UserID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetUsersModel(UserID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (UsersExpModel) cache;
        }

        public UsersExpModel GetUsersModel(int UserID)
        {
            UsersExpModel model = this.dal.GetModel(UserID);
            if (model == null)
            {
                model = new UsersExpModel();
            }
            User user = new User(UserID);
            model.Activity = user.Activity;
            model.DepartmentID = user.DepartmentID;
            model.Email = user.Email;
            model.EmployeeID = user.EmployeeID;
            model.Phone = user.Phone;
            if (user.Sex != null)
            {
                model.Sex = user.Sex.Trim();
            }
            model.Style = user.Style;
            model.TrueName = user.TrueName;
            model.NickName = user.NickName;
            model.User_cLang = user.User_cLang;
            model.User_dateApprove = user.User_dateApprove;
            model.User_dateCreate = user.User_dateCreate;
            model.User_dateExpire = user.User_dateExpire;
            model.User_dateValid = user.User_dateValid;
            model.User_iApprover = user.User_iApprover;
            model.User_iApproveState = user.User_iApproveState;
            model.User_iCreator = user.User_iCreator;
            model.UserID = user.UserID;
            model.UserName = user.UserName;
            model.UserType = user.UserType;
            return model;
        }

        public bool UpdateAblumsCount()
        {
            return this.dal.UpdateAblumsCount();
        }

        public bool UpdateFavouritesCount()
        {
            return this.dal.UpdateFavouritesCount();
        }

        public bool UpdateIsDPI(string userIds, int status)
        {
            return this.dal.UpdateIsDPI(userIds, status);
        }

        public bool UpdatePhoneAndPay(int userid, string account, string phone)
        {
            return this.dal.UpdatePhoneAndPay(userid, account, phone);
        }

        public bool UpdateProductCount()
        {
            return this.dal.UpdateProductCount();
        }

        public bool UpdateShareCount()
        {
            return this.dal.UpdateShareCount();
        }

        public bool UpdateUsersExp(UsersExpModel model)
        {
            return this.dal.Update(model);
        }
    }
}

