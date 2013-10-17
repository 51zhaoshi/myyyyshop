namespace Maticsoft.BLL.Poll
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Reply
    {
        private readonly IReply dal = DAPoll.CreateReply();

        public int Add(Maticsoft.Model.Poll.Reply model)
        {
            return this.dal.Add(model);
        }

        public void Delete(int ID)
        {
            this.dal.Delete(ID);
        }

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
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

        public Maticsoft.Model.Poll.Reply GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Poll.Reply GetModelByCache(int ID)
        {
            string cacheKey = "ReplyModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
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
            return (Maticsoft.Model.Poll.Reply) cache;
        }

        public List<Maticsoft.Model.Poll.Reply> GetModelList(string strWhere)
        {
            DataSet set = this.dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.Reply> list = new List<Maticsoft.Model.Poll.Reply>();
            int count = set.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.Reply item = new Maticsoft.Model.Poll.Reply();
                    if (set.Tables[0].Rows[i]["ID"].ToString() != "")
                    {
                        item.ID = int.Parse(set.Tables[0].Rows[i]["ID"].ToString());
                    }
                    if (set.Tables[0].Rows[i]["TopicID"].ToString() != "")
                    {
                        item.TopicID = new int?(int.Parse(set.Tables[0].Rows[i]["TopicID"].ToString()));
                    }
                    item.ReContent = set.Tables[0].Rows[i]["ReContent"].ToString();
                    if (set.Tables[0].Rows[i]["ReTime"].ToString() != "")
                    {
                        item.ReTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[i]["ReTime"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public void Update(Maticsoft.Model.Poll.Reply model)
        {
            this.dal.Update(model);
        }
    }
}

