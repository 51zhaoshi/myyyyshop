namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface ISiteMessage
    {
        int Add(SiteMessage model);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetAdminSendList(int AdminID);
        DataSet GetAdminSendList(int AdminID, string KeyWord);
        DataSet GetAdminSendListByPage(int AdminID, int StartIndex, int EndIndex);
        int GetAdminSendMsgCount(int AdminID);
        int GetAllReceiveMsgCount(int RecevieID);
        int GetAllReceiveMsgCount(int RecevieID, int AdminID);
        DataSet GetAllReceiveMsgList(int RecevieID, int AdminID);
        DataSet GetAllReceiveMsgListByPage(int RecevierID, int StartIndex, int EndIndex);
        DataSet GetAllReceiveMsgListByPage(int RecevieID, int AdminID, int StartIndex, int EndIndex);
        DataSet GetAllSendMsgList(int SenderID);
        DataSet GetAllSendMsgListByPage(int SenderID, int StartIndex, int EndIndex);
        int GetAllSystemMsgCount(int ReceiverID, int AdminId, string UserType);
        DataSet GetAllSystemMsgList(int ReceiverID, int AdminId, string UserType);
        DataSet GetAllSystemMsgListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SiteMessage GetModel(int ID);
        DataSet GetReceiveMsgAlreadyReadList(int ReceiverID, int AdminId);
        DataSet GetReceiveMsgAlreadyReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex);
        int GetReceiveMsgAreadyReadCount(int ReceiverID, int AdminId);
        int GetReceiveMsgNotReadCount(int ReceiverID, int AdminId);
        DataSet GetReceiveMsgNotReadList(int ReceiverID, int AdminId);
        DataSet GetReceiveMsgNotReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex);
        int GetRecordCount(string strWhere);
        int GetSendMsgCount(int SenderID);
        int GetSystemMsgAlreadyReadCount(int ReceiverID, int AdminId, string UserType);
        DataSet GetSystemMsgAlreadyReadList(int ReceiverID, int AdminId, string UserType);
        DataSet GetSystemMsgAlreadyReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex);
        int GetSystemMsgNotReadCount(int ReceiverID, int AdminId, string UserType);
        DataSet GetSystemMsgNotReadList(int ReceiverID, int AdminId, string UserType);
        DataSet GetSystemMsgNotReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex);
        int SetAdminMsgToDelById(int ID, int AdminID);
        int SetReceiveMsgAlreadyRead(int ID);
        int SetReceiveMsgToDelById(int ID);
        int SetSendMsgToDelById(int ID);
        int SetSystemMsgStateToAlreadyRead(int ID, int ReceiverID, string UserType);
        int SetSystemMsgStateToDel(int ID, int ReceiverID, string UserType);
        bool Update(SiteMessage model);
    }
}

