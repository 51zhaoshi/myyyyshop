namespace Maticsoft.BLL.Poll
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Topics
    {
        private readonly ITopics dal = DAPoll.CreateTopics();

        public int Add(Maticsoft.Model.Poll.Topics model)
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

        public bool Exists(int FormID, string Title)
        {
            return this.dal.Exists(FormID, Title);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetListByForm(int FormID)
        {
            return this.GetList(" FormID=" + FormID);
        }

        public Maticsoft.Model.Poll.Topics GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Poll.Topics GetModelByCache(int ID)
        {
            string cacheKey = "TopicsModel-" + ID;
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
            return (Maticsoft.Model.Poll.Topics) cache;
        }

        public List<Maticsoft.Model.Poll.Topics> GetModelList(string strWhere)
        {
            DataSet set = this.dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.Topics> list = new List<Maticsoft.Model.Poll.Topics>();
            int count = set.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.Topics item = new Maticsoft.Model.Poll.Topics();
                    if (set.Tables[0].Rows[i]["ID"].ToString() != "")
                    {
                        item.ID = int.Parse(set.Tables[0].Rows[i]["ID"].ToString());
                    }
                    item.Title = set.Tables[0].Rows[i]["Title"].ToString();
                    if (set.Tables[0].Rows[i]["Type"].ToString() != "")
                    {
                        item.Type = new int?(int.Parse(set.Tables[0].Rows[i]["Type"].ToString()));
                    }
                    if (set.Tables[0].Rows[i]["FormID"].ToString() != "")
                    {
                        item.FormID = new int?(int.Parse(set.Tables[0].Rows[i]["FormID"].ToString()));
                    }
                    if (set.Tables[0].Rows[i]["RowNum"].ToString() != "")
                    {
                        item.RowNum = int.Parse(set.Tables[0].Rows[i]["RowNum"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.Poll.Topics> GetModelList(int Top, int formid)
        {
            Maticsoft.Model.Poll.Forms modelByCache = new Maticsoft.BLL.Poll.Forms().GetModelByCache(formid);
            if ((modelByCache == null) || !modelByCache.IsActive)
            {
                return null;
            }
            DataSet set = this.dal.GetList(Top, string.Format(" Type in (0,1) and  FormID={0} ", formid), "  ORDER BY ID ASC ");
            List<Maticsoft.Model.Poll.Topics> list = new List<Maticsoft.Model.Poll.Topics>();
            int count = set.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.Topics item = new Maticsoft.Model.Poll.Topics();
                    if (set.Tables[0].Rows[i]["ID"].ToString() != "")
                    {
                        item.ID = int.Parse(set.Tables[0].Rows[i]["ID"].ToString());
                    }
                    item.Title = set.Tables[0].Rows[i]["Title"].ToString();
                    if (set.Tables[0].Rows[i]["Type"].ToString() != "")
                    {
                        item.Type = new int?(int.Parse(set.Tables[0].Rows[i]["Type"].ToString()));
                    }
                    if (set.Tables[0].Rows[i]["FormID"].ToString() != "")
                    {
                        item.FormID = new int?(int.Parse(set.Tables[0].Rows[i]["FormID"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public void Update(Maticsoft.Model.Poll.Topics model)
        {
            this.dal.Update(model);
        }
    }
}

