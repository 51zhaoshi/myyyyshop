namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class ReferUsers
    {
        private readonly IReferUsers dal = DASNS.CreateReferUsers();

        public int Add(Maticsoft.Model.SNS.ReferUsers model)
        {
            return this.dal.Add(model);
        }

        public void AddEx(string Content, EnumHelper.ReferType Type, int TargetId, string CreateNickName = "")
        {
            Maticsoft.Model.SNS.ReferUsers model = new Maticsoft.Model.SNS.ReferUsers();
            Users users2 = new Users();
            if (!string.IsNullOrWhiteSpace(Content))
            {
                MatchCollection matchs = Regex.Matches(Content, @"@[\u4e00-\u9fa5\w\-]+");
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(CreateNickName))
                {
                    list.Add(CreateNickName);
                }
                foreach (Match match in matchs)
                {
                    int num;
                    string item = match.Value.Trim(new char[] { '@' });
                    if ((!list.Contains(item) && !string.IsNullOrEmpty(item)) && ((num = users2.GetUserIdByNickName(item)) > 0))
                    {
                        list.Add(item);
                        model.CreatedDate = DateTime.Now;
                        model.IsRead = false;
                        model.ReferUserID = num;
                        model.ReferNickName = item;
                        model.Type = (int) Type;
                        model.TagetID = TargetId;
                        this.Add(model);
                    }
                }
            }
        }

        public List<Maticsoft.Model.SNS.ReferUsers> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.ReferUsers> list = new List<Maticsoft.Model.SNS.ReferUsers>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.ReferUsers item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
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

        public Maticsoft.Model.SNS.ReferUsers GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.ReferUsers GetModelByCache(int ID)
        {
            string cacheKey = "ReferUsersModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.ReferUsers) cache;
        }

        public List<Maticsoft.Model.SNS.ReferUsers> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetReferNotReadCountByType(int UserId, int Type)
        {
            return this.dal.GetReferNotReadCountByType(UserId, Type);
        }

        public bool Update(Maticsoft.Model.SNS.ReferUsers model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateReferStateToRead(int UserID, int Type)
        {
            return this.dal.UpdateReferStateToRead(UserID, Type);
        }
    }
}

