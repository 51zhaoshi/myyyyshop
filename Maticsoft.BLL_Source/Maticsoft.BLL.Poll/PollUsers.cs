namespace Maticsoft.BLL.Poll
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PollUsers
    {
        private readonly IPollUsers dal = DAPoll.CreatePollUsers();

        public int Add(Maticsoft.Model.Poll.PollUsers model)
        {
            return this.dal.Add(model);
        }

        public void Delete(int UserID)
        {
            this.dal.Delete(UserID);
        }

        public bool DeleteList(string ClassIDlist)
        {
            return this.dal.DeleteList(ClassIDlist);
        }

        public bool Exists(int UserID)
        {
            return this.dal.Exists(UserID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Poll.PollUsers GetModel(int UserID)
        {
            return this.dal.GetModel(UserID);
        }

        public Maticsoft.Model.Poll.PollUsers GetModelByCache(int UserID)
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
            return (Maticsoft.Model.Poll.PollUsers) cache;
        }

        public List<Maticsoft.Model.Poll.PollUsers> GetModelList(string strWhere)
        {
            DataSet set = this.dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.PollUsers> list = new List<Maticsoft.Model.Poll.PollUsers>();
            int count = set.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.PollUsers item = new Maticsoft.Model.Poll.PollUsers();
                    if (set.Tables[0].Rows[i]["UserID"].ToString() != "")
                    {
                        item.UserID = int.Parse(set.Tables[0].Rows[i]["UserID"].ToString());
                    }
                    item.UserName = set.Tables[0].Rows[i]["UserName"].ToString();
                    if (set.Tables[0].Rows[i]["Password"].ToString() != "")
                    {
                        item.Password = (byte[]) set.Tables[0].Rows[i]["Password"];
                    }
                    item.TrueName = set.Tables[0].Rows[i]["TrueName"].ToString();
                    if (set.Tables[0].Rows[i]["Age"].ToString() != "")
                    {
                        item.Age = new int?(int.Parse(set.Tables[0].Rows[i]["Age"].ToString()));
                    }
                    item.Sex = set.Tables[0].Rows[i]["Sex"].ToString();
                    item.Phone = set.Tables[0].Rows[i]["Phone"].ToString();
                    item.Email = set.Tables[0].Rows[i]["Email"].ToString();
                    item.UserType = set.Tables[0].Rows[i]["UserType"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public int GetUserCount()
        {
            return this.dal.GetUserCount();
        }

        public void Update(Maticsoft.Model.Poll.PollUsers model)
        {
            this.dal.Update(model);
        }
    }
}

