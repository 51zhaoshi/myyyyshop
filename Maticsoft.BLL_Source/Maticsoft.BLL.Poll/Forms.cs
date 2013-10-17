namespace Maticsoft.BLL.Poll
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Forms
    {
        private readonly IForms dal = DAPoll.CreateForms();

        public int Add(Maticsoft.Model.Poll.Forms model)
        {
            return this.dal.Add(model);
        }

        public void Delete(int FormID)
        {
            this.dal.Delete(FormID);
        }

        public bool DeleteList(string ClassIDlist)
        {
            return this.dal.DeleteList(ClassIDlist);
        }

        public bool Exists(int FormID)
        {
            return this.dal.Exists(FormID);
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

        public Maticsoft.Model.Poll.Forms GetModel(int FormID)
        {
            return this.dal.GetModel(FormID);
        }

        public Maticsoft.Model.Poll.Forms GetModelByCache(int FormID)
        {
            string cacheKey = "FormsModel-" + FormID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FormID);
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
            return (Maticsoft.Model.Poll.Forms) cache;
        }

        public List<Maticsoft.Model.Poll.Forms> GetModelList(string strWhere)
        {
            DataSet set = this.dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.Forms> list = new List<Maticsoft.Model.Poll.Forms>();
            int count = set.Tables[0].Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Poll.Forms item = new Maticsoft.Model.Poll.Forms();
                    if (set.Tables[0].Rows[i]["FormID"].ToString() != "")
                    {
                        item.FormID = int.Parse(set.Tables[0].Rows[i]["FormID"].ToString());
                    }
                    item.Name = set.Tables[0].Rows[i]["Name"].ToString();
                    item.Description = set.Tables[0].Rows[i]["Description"].ToString();
                    if ((set.Tables[0].Rows[0]["IsActive"] != null) && (set.Tables[0].Rows[0]["IsActive"].ToString() != ""))
                    {
                        if ((set.Tables[0].Rows[0]["IsActive"].ToString() == "1") || (set.Tables[0].Rows[0]["IsActive"].ToString().ToLower() == "true"))
                        {
                            item.IsActive = true;
                        }
                        else
                        {
                            item.IsActive = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public int Update(Maticsoft.Model.Poll.Forms model)
        {
            return this.dal.Update(model);
        }
    }
}

