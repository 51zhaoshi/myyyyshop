namespace Maticsoft.BLL.Ms
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Enterprise
    {
        private readonly IEnterprise dal = DAMs.CreateEnterprise();

        public int Add(Maticsoft.Model.Ms.Enterprise model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Ms.Enterprise> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.Enterprise> list = new List<Maticsoft.Model.Ms.Enterprise>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.Enterprise item = new Maticsoft.Model.Ms.Enterprise();
                    if ((dt.Rows[i]["EnterpriseID"] != null) && (dt.Rows[i]["EnterpriseID"].ToString() != ""))
                    {
                        item.EnterpriseID = int.Parse(dt.Rows[i]["EnterpriseID"].ToString());
                    }
                    if ((dt.Rows[i]["Name"] != null) && (dt.Rows[i]["Name"].ToString() != ""))
                    {
                        item.Name = dt.Rows[i]["Name"].ToString();
                    }
                    if ((dt.Rows[i]["Introduction"] != null) && (dt.Rows[i]["Introduction"].ToString() != ""))
                    {
                        item.Introduction = dt.Rows[i]["Introduction"].ToString();
                    }
                    if ((dt.Rows[i]["RegisteredCapital"] != null) && (dt.Rows[i]["RegisteredCapital"].ToString() != ""))
                    {
                        item.RegisteredCapital = new int?(int.Parse(dt.Rows[i]["RegisteredCapital"].ToString()));
                    }
                    if ((dt.Rows[i]["TelPhone"] != null) && (dt.Rows[i]["TelPhone"].ToString() != ""))
                    {
                        item.TelPhone = dt.Rows[i]["TelPhone"].ToString();
                    }
                    if ((dt.Rows[i]["CellPhone"] != null) && (dt.Rows[i]["CellPhone"].ToString() != ""))
                    {
                        item.CellPhone = dt.Rows[i]["CellPhone"].ToString();
                    }
                    if ((dt.Rows[i]["ContactMail"] != null) && (dt.Rows[i]["ContactMail"].ToString() != ""))
                    {
                        item.ContactMail = dt.Rows[i]["ContactMail"].ToString();
                    }
                    if ((dt.Rows[i]["RegionID"] != null) && (dt.Rows[i]["RegionID"].ToString() != ""))
                    {
                        item.RegionID = new int?(int.Parse(dt.Rows[i]["RegionID"].ToString()));
                    }
                    if ((dt.Rows[i]["Address"] != null) && (dt.Rows[i]["Address"].ToString() != ""))
                    {
                        item.Address = dt.Rows[i]["Address"].ToString();
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    if ((dt.Rows[i]["Contact"] != null) && (dt.Rows[i]["Contact"].ToString() != ""))
                    {
                        item.Contact = dt.Rows[i]["Contact"].ToString();
                    }
                    if ((dt.Rows[i]["UserName"] != null) && (dt.Rows[i]["UserName"].ToString() != ""))
                    {
                        item.UserName = dt.Rows[i]["UserName"].ToString();
                    }
                    if ((dt.Rows[i]["EstablishedDate"] != null) && (dt.Rows[i]["EstablishedDate"].ToString() != ""))
                    {
                        item.EstablishedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["EstablishedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["EstablishedCity"] != null) && (dt.Rows[i]["EstablishedCity"].ToString() != ""))
                    {
                        item.EstablishedCity = new int?(int.Parse(dt.Rows[i]["EstablishedCity"].ToString()));
                    }
                    if ((dt.Rows[i]["LOGO"] != null) && (dt.Rows[i]["LOGO"].ToString() != ""))
                    {
                        item.LOGO = dt.Rows[i]["LOGO"].ToString();
                    }
                    if ((dt.Rows[i]["Fax"] != null) && (dt.Rows[i]["Fax"].ToString() != ""))
                    {
                        item.Fax = dt.Rows[i]["Fax"].ToString();
                    }
                    if ((dt.Rows[i]["PostCode"] != null) && (dt.Rows[i]["PostCode"].ToString() != ""))
                    {
                        item.PostCode = dt.Rows[i]["PostCode"].ToString();
                    }
                    if ((dt.Rows[i]["HomePage"] != null) && (dt.Rows[i]["HomePage"].ToString() != ""))
                    {
                        item.HomePage = dt.Rows[i]["HomePage"].ToString();
                    }
                    if ((dt.Rows[i]["ArtiPerson"] != null) && (dt.Rows[i]["ArtiPerson"].ToString() != ""))
                    {
                        item.ArtiPerson = dt.Rows[i]["ArtiPerson"].ToString();
                    }
                    if ((dt.Rows[i]["EnteRank"] != null) && (dt.Rows[i]["EnteRank"].ToString() != ""))
                    {
                        item.EnteRank = new int?(int.Parse(dt.Rows[i]["EnteRank"].ToString()));
                    }
                    if ((dt.Rows[i]["EnteClassID"] != null) && (dt.Rows[i]["EnteClassID"].ToString() != ""))
                    {
                        item.EnteClassID = new int?(int.Parse(dt.Rows[i]["EnteClassID"].ToString()));
                    }
                    if ((dt.Rows[i]["CompanyType"] != null) && (dt.Rows[i]["CompanyType"].ToString() != ""))
                    {
                        item.CompanyType = new int?(int.Parse(dt.Rows[i]["CompanyType"].ToString()));
                    }
                    if ((dt.Rows[i]["BusinessLicense"] != null) && (dt.Rows[i]["BusinessLicense"].ToString() != ""))
                    {
                        item.BusinessLicense = dt.Rows[i]["BusinessLicense"].ToString();
                    }
                    if ((dt.Rows[i]["TaxNumber"] != null) && (dt.Rows[i]["TaxNumber"].ToString() != ""))
                    {
                        item.TaxNumber = dt.Rows[i]["TaxNumber"].ToString();
                    }
                    if ((dt.Rows[i]["AccountBank"] != null) && (dt.Rows[i]["AccountBank"].ToString() != ""))
                    {
                        item.AccountBank = dt.Rows[i]["AccountBank"].ToString();
                    }
                    if ((dt.Rows[i]["AccountInfo"] != null) && (dt.Rows[i]["AccountInfo"].ToString() != ""))
                    {
                        item.AccountInfo = dt.Rows[i]["AccountInfo"].ToString();
                    }
                    if ((dt.Rows[i]["ServicePhone"] != null) && (dt.Rows[i]["ServicePhone"].ToString() != ""))
                    {
                        item.ServicePhone = dt.Rows[i]["ServicePhone"].ToString();
                    }
                    if ((dt.Rows[i]["QQ"] != null) && (dt.Rows[i]["QQ"].ToString() != ""))
                    {
                        item.QQ = dt.Rows[i]["QQ"].ToString();
                    }
                    if ((dt.Rows[i]["MSN"] != null) && (dt.Rows[i]["MSN"].ToString() != ""))
                    {
                        item.MSN = dt.Rows[i]["MSN"].ToString();
                    }
                    if ((dt.Rows[i]["Status"] != null) && (dt.Rows[i]["Status"].ToString() != ""))
                    {
                        item.Status = new int?(int.Parse(dt.Rows[i]["Status"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedUserID"] != null) && (dt.Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        item.CreatedUserID = new int?(int.Parse(dt.Rows[i]["CreatedUserID"].ToString()));
                    }
                    if ((dt.Rows[i]["UpdatedDate"] != null) && (dt.Rows[i]["UpdatedDate"].ToString() != ""))
                    {
                        item.UpdatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["UpdatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["UpdatedUserID"] != null) && (dt.Rows[i]["UpdatedUserID"].ToString() != ""))
                    {
                        item.UpdatedUserID = new int?(int.Parse(dt.Rows[i]["UpdatedUserID"].ToString()));
                    }
                    if ((dt.Rows[i]["AgentID"] != null) && (dt.Rows[i]["AgentID"].ToString() != ""))
                    {
                        item.AgentID = int.Parse(dt.Rows[i]["AgentID"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int EnterpriseID)
        {
            return this.dal.Delete(EnterpriseID);
        }

        public bool DeleteList(string EnterpriseIDlist)
        {
            return this.dal.DeleteList(EnterpriseIDlist);
        }

        public bool Exists(int EnterpriseID)
        {
            return this.dal.Exists(EnterpriseID);
        }

        public bool Exists(string Name)
        {
            return this.dal.Exists(Name);
        }

        public bool Exists(string Name, int EnterpriseID)
        {
            return this.dal.Exists(Name, EnterpriseID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetEnteName(string strEnteName, int iCount)
        {
            string strWhere = "Name like '" + strEnteName + "%' AND Status=1 ";
            return this.dal.GetList(iCount, strWhere, "Name");
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Ms.Enterprise GetModel(int EnterpriseID)
        {
            return this.dal.GetModel(EnterpriseID);
        }

        public Maticsoft.Model.Ms.Enterprise GetModelByCache(int EnterpriseID)
        {
            string cacheKey = "EnterpriseModel-" + EnterpriseID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(EnterpriseID);
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
            return (Maticsoft.Model.Ms.Enterprise) cache;
        }

        public List<Maticsoft.Model.Ms.Enterprise> GetModelByEnterpriseName(string strEnterpriseName)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrWhiteSpace(strEnterpriseName))
            {
                strWhere = "Name = '" + strEnterpriseName + "'";
            }
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Ms.Enterprise> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Ms.Enterprise model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }
    }
}

