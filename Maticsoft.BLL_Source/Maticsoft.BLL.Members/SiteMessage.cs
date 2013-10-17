namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Webdiyer.WebControls.Mvc;

    public class SiteMessage
    {
        private readonly ISiteMessage dal = DAMembers.CreateSiteMessage();

        public int Add(Maticsoft.Model.Members.SiteMessage model)
        {
            return this.dal.Add(model);
        }

        public int AddMessageByUser(int SendID, int ReceiverID, string Title, string Content)
        {
            Maticsoft.Model.Members.SiteMessage model = new Maticsoft.Model.Members.SiteMessage {
                Content = Content,
                Title = Title,
                ReceiverID = new int?(ReceiverID),
                SenderID = new int?(SendID),
                SendTime = new DateTime?(DateTime.Now),
                ReceiverIsRead = false,
                SenderIsDel = false,
                ReaderIsDel = false
            };
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.SiteMessage> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.SiteMessage> list = new List<Maticsoft.Model.Members.SiteMessage>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.SiteMessage item = new Maticsoft.Model.Members.SiteMessage();
                    if ((dt.Rows[i]["ID"] != null) && (dt.Rows[i]["ID"].ToString() != ""))
                    {
                        item.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    }
                    if ((dt.Rows[i]["SenderID"] != null) && (dt.Rows[i]["SenderID"].ToString() != ""))
                    {
                        item.SenderID = new int?(int.Parse(dt.Rows[i]["SenderID"].ToString()));
                    }
                    if ((dt.Rows[i]["ReceiverID"] != null) && (dt.Rows[i]["ReceiverID"].ToString() != ""))
                    {
                        item.ReceiverID = new int?(int.Parse(dt.Rows[i]["ReceiverID"].ToString()));
                    }
                    if ((dt.Rows[i]["Title"] != null) && (dt.Rows[i]["Title"].ToString() != ""))
                    {
                        item.Title = dt.Rows[i]["Title"].ToString();
                    }
                    if ((dt.Rows[i]["Content"] != null) && (dt.Rows[i]["Content"].ToString() != ""))
                    {
                        item.Content = dt.Rows[i]["Content"].ToString();
                    }
                    if ((dt.Rows[i]["MsgType"] != null) && (dt.Rows[i]["MsgType"].ToString() != ""))
                    {
                        item.MsgType = dt.Rows[i]["MsgType"].ToString();
                    }
                    if ((dt.Rows[i]["SendTime"] != null) && (dt.Rows[i]["SendTime"].ToString() != ""))
                    {
                        item.SendTime = new DateTime?(DateTime.Parse(dt.Rows[i]["SendTime"].ToString()));
                    }
                    if ((dt.Rows[i]["ReadTime"] != null) && (dt.Rows[i]["ReadTime"].ToString() != ""))
                    {
                        item.ReadTime = new DateTime?(DateTime.Parse(dt.Rows[i]["ReadTime"].ToString()));
                    }
                    if ((dt.Rows[i]["ReceiverIsRead"] != null) && (dt.Rows[i]["ReceiverIsRead"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["ReceiverIsRead"].ToString() == "1") || (dt.Rows[i]["ReceiverIsRead"].ToString().ToLower() == "true"))
                        {
                            item.ReceiverIsRead = true;
                        }
                        else
                        {
                            item.ReceiverIsRead = false;
                        }
                    }
                    if ((dt.Rows[i]["SenderIsDel"] != null) && (dt.Rows[i]["SenderIsDel"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["SenderIsDel"].ToString() == "1") || (dt.Rows[i]["SenderIsDel"].ToString().ToLower() == "true"))
                        {
                            item.SenderIsDel = true;
                        }
                        else
                        {
                            item.SenderIsDel = false;
                        }
                    }
                    if ((dt.Rows[i]["ReaderIsDel"] != null) && (dt.Rows[i]["ReaderIsDel"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["ReaderIsDel"].ToString() == "1") || (dt.Rows[i]["ReaderIsDel"].ToString().ToLower() == "true"))
                        {
                            item.ReaderIsDel = true;
                        }
                        else
                        {
                            item.ReaderIsDel = false;
                        }
                    }
                    if ((dt.Rows[i]["Ext1"] != null) && (dt.Rows[i]["Ext1"].ToString() != ""))
                    {
                        item.Ext1 = dt.Rows[i]["Ext1"].ToString();
                    }
                    if ((dt.Rows[i]["Ext2"] != null) && (dt.Rows[i]["Ext2"].ToString() != ""))
                    {
                        item.Ext2 = dt.Rows[i]["Ext2"].ToString();
                    }
                    if ((dt.Columns.Contains("SenderUserName") && (dt.Rows[i]["SenderUserName"] != null)) && (dt.Rows[i]["SenderUserName"].ToString() != ""))
                    {
                        item.SenderUserName = dt.Rows[i]["SenderUserName"].ToString();
                    }
                    if ((dt.Columns.Contains("ReceiverUserName") && (dt.Rows[i]["ReceiverUserName"] != null)) && (dt.Rows[i]["ReceiverUserName"].ToString() != ""))
                    {
                        item.ReceiverUserName = dt.Rows[i]["ReceiverUserName"].ToString();
                    }
                    list.Add(item);
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

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAdminSendList(int AdminID)
        {
            return this.DataTableToList(this.dal.GetAdminSendList(AdminID).Tables[0]);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAdminSendList(int AdminID, string KeyWord)
        {
            return this.DataTableToList(this.dal.GetAdminSendList(AdminID, KeyWord).Tables[0]);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAdminSendListByPage(int AdminID, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetAdminSendListByPage(AdminID, StartIndex, EndIndex).Tables[0]);
        }

        public int GetAdminSendMsgCount(int AdminID)
        {
            return this.dal.GetAdminSendMsgCount(AdminID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgAlReadReadyListByMvcPage(int ReceiverID, int AdminID, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetReceiveMsgAlreadyReadListByPage(ReceiverID, AdminID, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, this.GetReceiveMsgAreadyReadCount(ReceiverID, AdminID));
        }

        public int GetAllReceiveMsgCount(int RecevieID)
        {
            return this.dal.GetAllReceiveMsgCount(RecevieID);
        }

        public int GetAllReceiveMsgCount(int RecevieID, int AdminID)
        {
            return this.dal.GetAllReceiveMsgCount(RecevieID, AdminID);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgList(int RecevierID, int AdminID)
        {
            return this.DataTableToList(this.dal.GetAllReceiveMsgList(RecevierID, AdminID).Tables[0]);
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgListByMvcPage(int ReceiverID, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetAllReceiveMsgListByPage(ReceiverID, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, PageSize, this.GetAllReceiveMsgCount(ReceiverID));
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgListByMvcPage(int ReceiverID, int AdminID, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetAllReceiveMsgListByPage(ReceiverID, AdminID, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, PageSize, this.GetAllReceiveMsgCount(ReceiverID, AdminID));
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgListByPage(int RecevierID, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetAllReceiveMsgListByPage(RecevierID, StartIndex, EndIndex).Tables[0]);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgListByPage(int RecevierID, int AdminID, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetAllReceiveMsgListByPage(RecevierID, AdminID, StartIndex, EndIndex).Tables[0]);
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgNotReadListByMvcPage(int ReceiverID, int AdminID, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetReceiveMsgNotReadListByPage(ReceiverID, AdminID, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, this.GetReceiveMsgNotReadCount(ReceiverID, AdminID));
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAllSendMsgList(int SenderID)
        {
            return this.DataTableToList(this.dal.GetAllSendMsgList(SenderID).Tables[0]);
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetAllSendMsgListByMvcPage(int SenderID, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetAllSendMsgListByPage(SenderID, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, PageSize, this.GetSendMsgCount(SenderID));
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAllSendMsgListByPage(int SenderID, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetAllSendMsgListByPage(SenderID, StartIndex, EndIndex).Tables[0]);
        }

        public int GetAllSystemMsgCount(int ReceiverID, int AdminId, string UserType)
        {
            return this.dal.GetAllSystemMsgCount(ReceiverID, AdminId, UserType);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAllSystemMsgList(int ReceiverID, int AdminId, string UserType)
        {
            return this.DataTableToList(this.dal.GetAllSystemMsgList(ReceiverID, AdminId, UserType).Tables[0]);
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetAllSystemMsgListByMvcPage(int ReceiverID, int AdminID, string UserType, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetAllSystemMsgListByPage(ReceiverID, AdminID, UserType, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, PageSize, this.GetAllSystemMsgCount(ReceiverID, AdminID, UserType));
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetAllSystemMsgListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetAllSystemMsgListByPage(ReceiverID, AdminId, UserType, StartIndex, EndIndex).Tables[0]);
        }

        public int GetEndPageIndex(int PageSize, int PageIndex)
        {
            return (PageSize * PageIndex);
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

        public Maticsoft.Model.Members.SiteMessage GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Members.SiteMessage GetModelByCache(int ID)
        {
            string cacheKey = "SiteMessageModel-" + ID;
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
            return (Maticsoft.Model.Members.SiteMessage) cache;
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgAlreadyReadList(int ReceiverID, int AdminId)
        {
            return this.DataTableToList(this.dal.GetReceiveMsgAlreadyReadList(ReceiverID, AdminId).Tables[0]);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgAlreadyReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetReceiveMsgAlreadyReadListByPage(ReceiverID, AdminId, StartIndex, EndIndex).Tables[0]);
        }

        public int GetReceiveMsgAreadyReadCount(int ReceiverID, int AdminId)
        {
            return this.dal.GetReceiveMsgAreadyReadCount(ReceiverID, AdminId);
        }

        public int GetReceiveMsgNotReadCount(int ReceiverID, int AdminId)
        {
            return this.dal.GetReceiveMsgNotReadCount(ReceiverID, AdminId);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgNotReadList(int ReceiverID, int AdminId)
        {
            return this.DataTableToList(this.dal.GetReceiveMsgNotReadList(ReceiverID, AdminId).Tables[0]);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgNotReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetReceiveMsgNotReadListByPage(ReceiverID, AdminId, StartIndex, EndIndex).Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetSendMsgCount(int SenderID)
        {
            return this.dal.GetSendMsgCount(SenderID);
        }

        public int GetStartPageIndex(int PageSize, int PageIndex)
        {
            return ((PageSize * (PageIndex - 1)) + 1);
        }

        public int GetSystemMsgAlreadyReadCount(int ReceiverID, int AdminId, string UserType)
        {
            return this.dal.GetSystemMsgAlreadyReadCount(ReceiverID, AdminId, UserType);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgAlreadyReadList(int ReceiverID, int AdminId, string UserType)
        {
            return this.DataTableToList(this.dal.GetSystemMsgAlreadyReadList(ReceiverID, AdminId, UserType).Tables[0]);
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetSystemMsgAlreadyReadListByMvcPage(int ReceiverID, int AdminID, string UserType, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetSystemMsgAlreadyReadListByPage(ReceiverID, AdminID, UserType, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, this.GetSystemMsgAlreadyReadCount(ReceiverID, AdminID, UserType));
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgAlreadyReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetSystemMsgAlreadyReadListByPage(ReceiverID, AdminId, UserType, StartIndex, EndIndex).Tables[0]);
        }

        public int GetSystemMsgNotReadCount(int ReceiverID, int AdminId, string UserType)
        {
            return this.dal.GetSystemMsgNotReadCount(ReceiverID, AdminId, UserType);
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgNotReadList(int ReceiverID, int AdminId, string UserType)
        {
            return this.DataTableToList(this.dal.GetSystemMsgNotReadList(ReceiverID, AdminId, UserType).Tables[0]);
        }

        public PagedList<Maticsoft.Model.Members.SiteMessage> GetSystemMsgNotReadListByMvcPage(int ReceiverID, int AdminID, string UserType, int PageSize, int PageIndex)
        {
            return new PagedList<Maticsoft.Model.Members.SiteMessage>(this.GetSystemMsgNotReadListByPage(ReceiverID, AdminID, UserType, this.GetStartPageIndex(PageSize, PageIndex), this.GetEndPageIndex(PageSize, PageIndex)), PageIndex, this.GetSystemMsgNotReadCount(ReceiverID, AdminID, UserType));
        }

        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgNotReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.dal.GetSystemMsgNotReadListByPage(ReceiverID, AdminId, UserType, StartIndex, EndIndex).Tables[0]);
        }

        public int SetAdminMsgToDelById(int ID, int AdminID)
        {
            return this.dal.SetAdminMsgToDelById(ID, AdminID);
        }

        public int SetReceiveMsgAlreadyRead(int ID)
        {
            return this.dal.SetReceiveMsgAlreadyRead(ID);
        }

        public int SetReceiveMsgToDelById(int ID)
        {
            return this.dal.SetReceiveMsgToDelById(ID);
        }

        public int SetSendMsgToDelById(int ID)
        {
            return this.dal.SetSendMsgToDelById(ID);
        }

        public int SetSystemMsgStateToAlreadyRead(int ID, int ReceiverID, string UserType)
        {
            return this.dal.SetSystemMsgStateToAlreadyRead(ID, ReceiverID, UserType);
        }

        public int SetSystemMsgStateToDel(int ID, int ReceiverID, string UserType)
        {
            return this.dal.SetSystemMsgStateToDel(ID, ReceiverID, UserType);
        }

        public bool Update(Maticsoft.Model.Members.SiteMessage model)
        {
            return this.dal.Update(model);
        }
    }
}

