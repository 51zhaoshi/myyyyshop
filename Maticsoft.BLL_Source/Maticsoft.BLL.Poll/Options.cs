namespace Maticsoft.BLL.Poll
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Options
    {
        private readonly IOptions dal = DAPoll.CreateOptions();

        public int Add(Maticsoft.Model.Poll.Options model)
        {
            return this.dal.Add(model);
        }

        public void Delete(int ID)
        {
            this.dal.Delete(ID);
        }

        public bool DeleteList(string ClassIDlist)
        {
            return this.dal.DeleteList(ClassIDlist);
        }

        public bool Exists(int TopicID, string Name)
        {
            return this.dal.Exists(TopicID, Name);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetCountList(int FormID)
        {
            return this.dal.GetCountList(FormID);
        }

        public List<Maticsoft.Model.Poll.Options> GetCountList(string strwhere)
        {
            DataSet countList = this.dal.GetCountList(strwhere);
            List<Maticsoft.Model.Poll.Options> list = new List<Maticsoft.Model.Poll.Options>();
            int count = countList.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.Options item = new Maticsoft.Model.Poll.Options {
                        Name = countList.Tables[0].Rows[i]["Name"].ToString()
                    };
                    if (countList.Tables[0].Rows[i]["TopicID"].ToString() != "")
                    {
                        item.TopicID = new int?(int.Parse(countList.Tables[0].Rows[i]["TopicID"].ToString()));
                    }
                    if (countList.Tables[0].Rows[i]["SubmitNum"].ToString() != "")
                    {
                        item.SubmitNum = new int?(int.Parse(countList.Tables[0].Rows[i]["SubmitNum"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetListByTopic(int TopicID)
        {
            return this.GetList(" TopicID=" + TopicID);
        }

        public Maticsoft.Model.Poll.Options GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Poll.Options GetModelByCache(int ID)
        {
            string cacheKey = "OptionsModel-" + ID;
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
            return (Maticsoft.Model.Poll.Options) cache;
        }

        public List<Maticsoft.Model.Poll.Options> GetModelList(string strWhere)
        {
            DataSet set = this.dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.Options> list = new List<Maticsoft.Model.Poll.Options>();
            int count = set.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.Options item = new Maticsoft.Model.Poll.Options();
                    if (set.Tables[0].Rows[i]["ID"].ToString() != "")
                    {
                        item.ID = int.Parse(set.Tables[0].Rows[i]["ID"].ToString());
                    }
                    item.Name = set.Tables[0].Rows[i]["Name"].ToString();
                    if (set.Tables[0].Rows[i]["TopicID"].ToString() != "")
                    {
                        item.TopicID = new int?(int.Parse(set.Tables[0].Rows[i]["TopicID"].ToString()));
                    }
                    if (set.Tables[0].Rows[i]["isChecked"].ToString() != "")
                    {
                        item.isChecked = new int?(int.Parse(set.Tables[0].Rows[i]["isChecked"].ToString()));
                    }
                    if (set.Tables[0].Rows[i]["SubmitNum"].ToString() != "")
                    {
                        item.SubmitNum = new int?(int.Parse(set.Tables[0].Rows[i]["SubmitNum"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public void Update(Maticsoft.Model.Poll.Options model)
        {
            this.dal.Update(model);
        }
    }
}

