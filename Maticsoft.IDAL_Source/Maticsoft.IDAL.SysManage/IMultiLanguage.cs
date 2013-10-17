namespace Maticsoft.IDAL.SysManage
{
    using System;
    using System.Data;

    public interface IMultiLanguage
    {
        int Add(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang, string MultiLang_cValue);
        void Delete(int ID);
        bool Exists(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang);
        string GetDefaultLangCode();
        DataSet GetLangListByValue(string MultiLang_cField, int MultiLang_iPKValue);
        DataSet GetLanguageList();
        string GetLanguageName(string Language_cCode);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        string GetModel(int ID);
        string GetModel(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang);
        DataSet GetValueListByLang(string MultiLang_cField, string MultiLang_cLang);
        void Update(int ID, string MultiLang_cValue);
    }
}

