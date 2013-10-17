namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class Users
    {
        private readonly IUsers dal = DAMembers.CreateUsers();

        public int Add(Maticsoft.Model.Members.Users model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.Users> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.Users> list = new List<Maticsoft.Model.Members.Users>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.Users item = new Maticsoft.Model.Members.Users();
                    if ((dt.Rows[i]["UserID"] != null) && (dt.Rows[i]["UserID"].ToString() != ""))
                    {
                        item.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                    }
                    if ((dt.Rows[i]["UserName"] != null) && (dt.Rows[i]["UserName"].ToString() != ""))
                    {
                        item.UserName = dt.Rows[i]["UserName"].ToString();
                    }
                    if ((dt.Rows[i]["Password"] != null) && (dt.Rows[i]["Password"].ToString() != ""))
                    {
                        item.Password = (byte[]) dt.Rows[i]["Password"];
                    }
                    if ((dt.Rows[i]["TrueName"] != null) && (dt.Rows[i]["TrueName"].ToString() != ""))
                    {
                        item.TrueName = dt.Rows[i]["TrueName"].ToString();
                    }
                    if ((dt.Rows[i]["Sex"] != null) && (dt.Rows[i]["Sex"].ToString() != ""))
                    {
                        item.Sex = dt.Rows[i]["Sex"].ToString();
                    }
                    if ((dt.Rows[i]["Phone"] != null) && (dt.Rows[i]["Phone"].ToString() != ""))
                    {
                        item.Phone = dt.Rows[i]["Phone"].ToString();
                    }
                    if ((dt.Rows[i]["Email"] != null) && (dt.Rows[i]["Email"].ToString() != ""))
                    {
                        item.Email = dt.Rows[i]["Email"].ToString();
                    }
                    if ((dt.Rows[i]["EmployeeID"] != null) && (dt.Rows[i]["EmployeeID"].ToString() != ""))
                    {
                        item.EmployeeID = new int?(int.Parse(dt.Rows[i]["EmployeeID"].ToString()));
                    }
                    if ((dt.Rows[i]["DepartmentID"] != null) && (dt.Rows[i]["DepartmentID"].ToString() != ""))
                    {
                        item.DepartmentID = dt.Rows[i]["DepartmentID"].ToString();
                    }
                    if ((dt.Rows[i]["Activity"] != null) && (dt.Rows[i]["Activity"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["Activity"].ToString() == "1") || (dt.Rows[i]["Activity"].ToString().ToLower() == "true"))
                        {
                            item.Activity = true;
                        }
                        else
                        {
                            item.Activity = false;
                        }
                    }
                    if ((dt.Rows[i]["UserType"] != null) && (dt.Rows[i]["UserType"].ToString() != ""))
                    {
                        item.UserType = dt.Rows[i]["UserType"].ToString();
                    }
                    if ((dt.Rows[i]["Style"] != null) && (dt.Rows[i]["Style"].ToString() != ""))
                    {
                        item.Style = new int?(int.Parse(dt.Rows[i]["Style"].ToString()));
                    }
                    if ((dt.Rows[i]["User_iCreator"] != null) && (dt.Rows[i]["User_iCreator"].ToString() != ""))
                    {
                        item.User_iCreator = new int?(int.Parse(dt.Rows[i]["User_iCreator"].ToString()));
                    }
                    if ((dt.Rows[i]["User_dateCreate"] != null) && (dt.Rows[i]["User_dateCreate"].ToString() != ""))
                    {
                        item.User_dateCreate = new DateTime?(DateTime.Parse(dt.Rows[i]["User_dateCreate"].ToString()));
                    }
                    if ((dt.Rows[i]["User_dateValid"] != null) && (dt.Rows[i]["User_dateValid"].ToString() != ""))
                    {
                        item.User_dateValid = new DateTime?(DateTime.Parse(dt.Rows[i]["User_dateValid"].ToString()));
                    }
                    if ((dt.Rows[i]["User_dateExpire"] != null) && (dt.Rows[i]["User_dateExpire"].ToString() != ""))
                    {
                        item.User_dateExpire = new DateTime?(DateTime.Parse(dt.Rows[i]["User_dateExpire"].ToString()));
                    }
                    if ((dt.Rows[i]["User_iApprover"] != null) && (dt.Rows[i]["User_iApprover"].ToString() != ""))
                    {
                        item.User_iApprover = new int?(int.Parse(dt.Rows[i]["User_iApprover"].ToString()));
                    }
                    if ((dt.Rows[i]["User_dateApprove"] != null) && (dt.Rows[i]["User_dateApprove"].ToString() != ""))
                    {
                        item.User_dateApprove = new DateTime?(DateTime.Parse(dt.Rows[i]["User_dateApprove"].ToString()));
                    }
                    if ((dt.Rows[i]["User_iApproveState"] != null) && (dt.Rows[i]["User_iApproveState"].ToString() != ""))
                    {
                        item.User_iApproveState = new int?(int.Parse(dt.Rows[i]["User_iApproveState"].ToString()));
                    }
                    if ((dt.Rows[i]["User_cLang"] != null) && (dt.Rows[i]["User_cLang"].ToString() != ""))
                    {
                        item.User_cLang = dt.Rows[i]["User_cLang"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int UserID)
        {
            return this.dal.Delete(UserID);
        }

        public bool DeleteByDepartmentID(int DepartmentID)
        {
            return this.dal.DeleteByDepartmentID(DepartmentID);
        }

        public bool DeleteEx(int userId)
        {
            return this.dal.DeleteEx(userId);
        }

        public bool DeleteList(string UserIDlist)
        {
            return this.dal.DeleteList(UserIDlist);
        }

        public bool DeleteListByDepartmentID(string DepartmentIDlist)
        {
            return this.dal.DeleteListByDepartmentID(DepartmentIDlist);
        }

        public bool ExistByPhone(string Phone)
        {
            return this.dal.ExistByPhone(Phone);
        }

        public bool Exists(int UserID)
        {
            return this.dal.Exists(UserID);
        }

        public bool ExistsByEmail(string UserEmail)
        {
            return this.dal.ExistsByEmail(UserEmail);
        }

        public bool ExistsNickName(string nickname)
        {
            return this.dal.ExistsNickName(nickname);
        }

        public bool ExistsNickName(int userid, string nickname)
        {
            return this.dal.ExistsNickName(userid, nickname);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetDefaultUserId()
        {
            return this.dal.GetDefaultUserId();
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(string type, string keyWord)
        {
            return this.dal.GetList(type, keyWord);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListEX(string keyWord)
        {
            return this.dal.GetListEX(keyWord);
        }

        public DataSet GetListEXByType(string type, string keyWord = "")
        {
            return this.dal.GetListEXByType(type, keyWord);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Members.Users GetModel(int UserID)
        {
            return this.dal.GetModel(UserID);
        }

        public Maticsoft.Model.Members.Users GetModelByCache(int UserID)
        {
            string cacheKey = "UsersModel-" + UserID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(UserID);
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
            return (Maticsoft.Model.Members.Users) cache;
        }

        public List<Maticsoft.Model.Members.Users> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public string GetNickName(int userId)
        {
            return this.dal.GetNickName(userId);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string type, string StrWhere = "")
        {
            return this.dal.GetSearchList(type, StrWhere);
        }

        public Maticsoft.Model.Members.Users GetUserIdByDepartmentID(string DepartmentID)
        {
            return this.dal.GetUserIdByDepartmentID(DepartmentID);
        }

        public int GetUserIdByNickName(string NickName)
        {
            return this.dal.GetUserIdByNickName(NickName);
        }

        public string GetUserName(int userId)
        {
            return this.dal.GetUserName(userId);
        }

        public bool Update(Maticsoft.Model.Members.Users model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateActiveStatus(string Ids, int ActiveType)
        {
            return this.dal.UpdateActiveStatus(Ids, ActiveType);
        }

        public bool UpdateFansAndFellowCount()
        {
            return this.dal.UpdateFansAndFellowCount();
        }
    }
}

