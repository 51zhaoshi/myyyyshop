namespace Maticsoft.Email.BLL
{
    using Maticsoft.Common;
    using Maticsoft.Email.IDAL;
    using Maticsoft.Email.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class EmailQueue
    {
        private readonly IEmailQueue dal = (PubConstant.IsSQLServer ? ((IEmailQueue) new Maticsoft.Email.SQLServerDAL.EmailQueue()) : ((IEmailQueue) new Maticsoft.Email.MySqlDAL.EmailQueue()));

        public bool Add(Maticsoft.Email.Model.EmailQueue model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Email.Model.EmailQueue> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Email.Model.EmailQueue> list = new List<Maticsoft.Email.Model.EmailQueue>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Email.Model.EmailQueue item = new Maticsoft.Email.Model.EmailQueue();
                    if ((dt.Rows[i]["EmailId"] != null) && (dt.Rows[i]["EmailId"].ToString() != ""))
                    {
                        item.EmailId = int.Parse(dt.Rows[i]["EmailId"].ToString());
                    }
                    if ((dt.Rows[i]["EmailPriority"] != null) && (dt.Rows[i]["EmailPriority"].ToString() != ""))
                    {
                        item.EmailPriority = int.Parse(dt.Rows[i]["EmailPriority"].ToString());
                    }
                    if ((dt.Rows[i]["IsBodyHtml"] != null) && (dt.Rows[i]["IsBodyHtml"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsBodyHtml"].ToString() == "1") || (dt.Rows[i]["IsBodyHtml"].ToString().ToLower() == "true"))
                        {
                            item.IsBodyHtml = true;
                        }
                        else
                        {
                            item.IsBodyHtml = false;
                        }
                    }
                    if ((dt.Rows[i]["EmailTo"] != null) && (dt.Rows[i]["EmailTo"].ToString() != ""))
                    {
                        item.EmailTo = dt.Rows[i]["EmailTo"].ToString();
                    }
                    if ((dt.Rows[i]["EmailCc"] != null) && (dt.Rows[i]["EmailCc"].ToString() != ""))
                    {
                        item.EmailCc = dt.Rows[i]["EmailCc"].ToString();
                    }
                    if ((dt.Rows[i]["EmailBcc"] != null) && (dt.Rows[i]["EmailBcc"].ToString() != ""))
                    {
                        item.EmailBcc = dt.Rows[i]["EmailBcc"].ToString();
                    }
                    if ((dt.Rows[i]["EmailFrom"] != null) && (dt.Rows[i]["EmailFrom"].ToString() != ""))
                    {
                        item.EmailFrom = dt.Rows[i]["EmailFrom"].ToString();
                    }
                    if ((dt.Rows[i]["EmailSubject"] != null) && (dt.Rows[i]["EmailSubject"].ToString() != ""))
                    {
                        item.EmailSubject = dt.Rows[i]["EmailSubject"].ToString();
                    }
                    if ((dt.Rows[i]["EmailBody"] != null) && (dt.Rows[i]["EmailBody"].ToString() != ""))
                    {
                        item.EmailBody = dt.Rows[i]["EmailBody"].ToString();
                    }
                    if ((dt.Rows[i]["NextTryTime"] != null) && (dt.Rows[i]["NextTryTime"].ToString() != ""))
                    {
                        item.NextTryTime = DateTime.Parse(dt.Rows[i]["NextTryTime"].ToString());
                    }
                    if ((dt.Rows[i]["NumberOfTries"] != null) && (dt.Rows[i]["NumberOfTries"].ToString() != ""))
                    {
                        item.NumberOfTries = int.Parse(dt.Rows[i]["NumberOfTries"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete()
        {
            return this.dal.Delete();
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

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Email.Model.EmailQueue GetModel()
        {
            return this.dal.GetModel();
        }

        public Maticsoft.Email.Model.EmailQueue GetModelByCache()
        {
            string cacheKey = "EmailQueueModel-";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel();
                    if (cache != null)
                    {
                        int num = 30;
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Email.Model.EmailQueue) cache;
        }

        public List<Maticsoft.Email.Model.EmailQueue> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool PushEmailQueur(string uType, string uName, string EmailSubject, string EmailBody, string EmailFrom)
        {
            return this.dal.PushEmailQueur(uType, uName, EmailSubject, EmailBody, EmailFrom);
        }

        public bool Update(Maticsoft.Email.Model.EmailQueue model)
        {
            return this.dal.Update(model);
        }
    }
}

