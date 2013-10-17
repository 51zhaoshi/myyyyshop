namespace Maticsoft.BLL
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL;
    using Maticsoft.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class MailConfig
    {
        private readonly IMailConfig dal = DAMembers.CreateMailConfig();

        public int Add(Maticsoft.Model.MailConfig model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.MailConfig> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.MailConfig> list = new List<Maticsoft.Model.MailConfig>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.MailConfig item = new Maticsoft.Model.MailConfig();
                    if (dt.Rows[i]["ID"].ToString() != "")
                    {
                        item.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    }
                    if (dt.Rows[i]["UserID"].ToString() != "")
                    {
                        item.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                    }
                    item.Mailaddress = dt.Rows[i]["Mailaddress"].ToString();
                    item.Username = dt.Rows[i]["Username"].ToString();
                    item.Password = dt.Rows[i]["Password"].ToString();
                    item.SMTPServer = dt.Rows[i]["SMTPServer"].ToString();
                    if (dt.Rows[i]["SMTPPort"].ToString() != "")
                    {
                        item.SMTPPort = int.Parse(dt.Rows[i]["SMTPPort"].ToString());
                    }
                    if (dt.Rows[i]["SMTPSSL"].ToString() != "")
                    {
                        item.SMTPSSL = dt.Rows[i]["SMTPSSL"].ToString() == "1";
                    }
                    item.POPServer = dt.Rows[i]["POPServer"].ToString();
                    if (dt.Rows[i]["POPPort"].ToString() != "")
                    {
                        item.POPPort = int.Parse(dt.Rows[i]["POPPort"].ToString());
                    }
                    if (dt.Rows[i]["POPSSL"].ToString() != "")
                    {
                        item.POPSSL = dt.Rows[i]["POPSSL"].ToString() == "1";
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public void Delete(int ID)
        {
            this.dal.Delete(ID);
        }

        public bool Exists(int UserID, string Mailaddress)
        {
            return this.dal.Exists(UserID, Mailaddress);
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

        public DataSet GetListByUser(int UserID)
        {
            return this.GetList(" UserID=" + UserID);
        }

        public Maticsoft.Model.MailConfig GetModel()
        {
            return this.dal.GetModel();
        }

        public Maticsoft.Model.MailConfig GetModel(int? userId)
        {
            return this.dal.GetModel(userId);
        }

        public Maticsoft.Model.MailConfig GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.MailConfig GetModelByCache(int ID)
        {
            string cacheKey = "MailConfigModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.MailConfig) cache;
        }

        public List<Maticsoft.Model.MailConfig> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public void Update(Maticsoft.Model.MailConfig model)
        {
            this.dal.Update(model);
        }
    }
}

