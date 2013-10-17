namespace Maticsoft.BLL.Poll
{
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserPoll
    {
        private readonly IUserPoll dal = DAPoll.CreateUserPoll();

        public void Add(Maticsoft.Model.Poll.UserPoll model)
        {
            this.dal.Add(model);
        }

        public bool Add2(Maticsoft.Model.Poll.UserPoll model)
        {
            return this.dal.Add2(model);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetListByUser(int UserID)
        {
            return this.GetList(" UserID=" + UserID);
        }

        public DataSet GetListInnerJoin(int userid)
        {
            return this.dal.GetListInnerJoin(userid);
        }

        public List<Maticsoft.Model.Poll.UserPoll> GetModelList(string strWhere)
        {
            DataSet set = this.dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.UserPoll> list = new List<Maticsoft.Model.Poll.UserPoll>();
            int count = set.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.UserPoll item = new Maticsoft.Model.Poll.UserPoll();
                    if (set.Tables[0].Rows[i]["UserID"].ToString() != "")
                    {
                        item.UserID = int.Parse(set.Tables[0].Rows[i]["UserID"].ToString());
                    }
                    if (set.Tables[0].Rows[i]["TopicID"].ToString() != "")
                    {
                        item.TopicID = new int?(int.Parse(set.Tables[0].Rows[i]["TopicID"].ToString()));
                    }
                    if (set.Tables[0].Rows[i]["OptionID"].ToString() != "")
                    {
                        item.OptionID = new int?(int.Parse(set.Tables[0].Rows[i]["OptionID"].ToString()));
                    }
                    if (set.Tables[0].Rows[i]["CreatTime"].ToString() != "")
                    {
                        item.CreatTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[i]["CreatTime"].ToString()));
                    }
                    item.UserIP = set.Tables[0].Rows[0]["UserIP"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public int GetUserByForm(int FormID)
        {
            return this.dal.GetUserByForm(FormID);
        }

        public void Update(Maticsoft.Model.Poll.UserPoll model)
        {
            this.dal.Update(model);
        }
    }
}

