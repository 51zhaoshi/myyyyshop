namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SysManage;
    using Maticsoft.OAuth;
    using Maticsoft.OAuth.Sina;
    using Maticsoft.OAuth.Tencent.QQ;
    using Maticsoft.OAuth.Tencent.Weibo;
    using Maticsoft.OAuth.v2;
    using Maticsoft.ViewModel.UserCenter;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Web;

    public class UserBind
    {
        private readonly IUserBind dal = DAMembers.CreateUserBind();

        public int Add(Maticsoft.Model.Members.UserBind model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(Maticsoft.Model.Members.UserBind model)
        {
            if (this.Exists(model.UserId, model.MediaUserID))
            {
                return this.dal.UpdateEx(model);
            }
            return (this.dal.Add(model) > 0);
        }

        public string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        public List<Maticsoft.Model.Members.UserBind> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UserBind> list = new List<Maticsoft.Model.Members.UserBind>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.UserBind item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int BindId)
        {
            return this.dal.Delete(BindId);
        }

        public bool DeleteList(string BindIdlist)
        {
            return this.dal.DeleteList(BindIdlist);
        }

        public bool Exists(int BindId)
        {
            return this.dal.Exists(BindId);
        }

        public bool Exists(int userId, string MediaUserID)
        {
            return this.dal.Exists(userId, MediaUserID);
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

        public UserBindList GetListEx(int userId)
        {
            UserBindList list = new UserBindList();
            List<Maticsoft.Model.Members.UserBind> modelList = this.GetModelList(" userid=" + userId);
            if ((modelList != null) && (modelList.Count > 0))
            {
                list.Count = modelList.Count;
                foreach (Maticsoft.Model.Members.UserBind bind in modelList)
                {
                    switch (bind.MediaID)
                    {
                        case 3:
                        {
                            list.Sina = bind;
                            continue;
                        }
                        case 4:
                        {
                            list.TenCent = bind;
                            continue;
                        }
                        case 13:
                            break;

                        default:
                        {
                            continue;
                        }
                    }
                    list.QZone = bind;
                }
            }
            return list;
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Members.UserBind GetModel(int BindId)
        {
            return this.dal.GetModel(BindId);
        }

        public Maticsoft.Model.Members.UserBind GetModel(int userId, int MediaID)
        {
            return this.dal.GetModel(userId, MediaID);
        }

        public Maticsoft.Model.Members.UserBind GetModelByCache(int BindId)
        {
            string cacheKey = "UserBindModel-" + BindId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BindId);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Members.UserBind) cache;
        }

        public List<Maticsoft.Model.Members.UserBind> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.Members.UserBind> GetWeiBoList(int userId)
        {
            List<Maticsoft.Model.Members.UserBind> modelList = this.GetModelList(" userid=" + userId);
            if ((modelList != null) && (modelList.Count > 0))
            {
                foreach (Maticsoft.Model.Members.UserBind bind in modelList)
                {
                    switch (bind.MediaID)
                    {
                        case 3:
                        {
                            bind.WeiboName = "新浪微博";
                            bind.WeiboLogo = "<img alt='QZone' src='/Admin/images/sina_16.png' />" + bind.MediaNickName;
                            continue;
                        }
                        case 4:
                        {
                            bind.WeiboName = "腾讯微博";
                            continue;
                        }
                        case 13:
                        {
                            bind.WeiboName = "QQ空间";
                            bind.WeiboLogo = "<img alt='QZone' src='/Admin/images/qq_16.png' />" + bind.MediaNickName;
                            continue;
                        }
                    }
                }
            }
            return modelList;
        }

        public void SendWeiBo(string bindIds, string content, string url, string imageUrl = new string())
        {
            string strWhere = "";
            if (!string.IsNullOrWhiteSpace(bindIds))
            {
                strWhere = strWhere + "  BindId in  (" + bindIds + ")";
            }
            List<Maticsoft.Model.Members.UserBind> modelList = this.GetModelList(strWhere);
            if ((modelList != null) && (modelList.Count != 0))
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    url = "http://" + Globals.DomainFullName;
                }
                foreach (Maticsoft.Model.Members.UserBind bind in modelList)
                {
                    string str6;
                    switch (bind.MediaID)
                    {
                        case 3:
                        {
                            string clientId = ConfigSystem.GetValueByCache("Social_SinaAppId");
                            string clientSecret = ConfigSystem.GetValueByCache("Social_SinaSercet");
                            IOAuth2ServiceProvider<Maticsoft.OAuth.Sina.IWeibo> provider = new Maticsoft.OAuth.Sina.WeiboServiceProvider(clientId, clientSecret);
                            Maticsoft.OAuth.Sina.IWeibo weibo = provider.GetApi(new AccessGrant(bind.TokenAccess, new string[] { bind.MediaUserID }));
                            try
                            {
                                if (string.IsNullOrWhiteSpace(imageUrl))
                                {
                                    weibo.UpdateStatusAsync(content + " " + url).Wait();
                                }
                                else
                                {
                                    string path = imageUrl;
                                    if (imageUrl.Contains("http://"))
                                    {
                                        System.Net.WebClient client = new System.Net.WebClient();
                                        string str5 = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(str5)))
                                        {
                                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(str5));
                                        }
                                        path = str5 + this.CreateIDCode() + ".jpg";
                                        client.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(path));
                                    }
                                    weibo.UploadStatusAsync(content + url, new FileInfo(HttpContext.Current.Server.MapPath(path))).Wait();
                                }
                            }
                            catch (Exception exception)
                            {
                                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                                    Loginfo = exception.Message,
                                    OPTime = DateTime.Now,
                                    StackTrace = exception.StackTrace,
                                    Url = ""
                                };
                                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                            }
                            break;
                        }
                        case 13:
                            goto Label_0201;
                    }
                    continue;
                Label_0201:
                    str6 = ConfigSystem.GetValueByCache("Social_QQAppId");
                    string valueByCache = ConfigSystem.GetValueByCache("Social_QQSercet");
                    IOAuth2ServiceProvider<IQConnect> provider2 = new QConnectServiceProvider(str6, valueByCache);
                    IQConnect api = provider2.GetApi(new AccessGrant(bind.TokenAccess, new string[] { bind.MediaUserID }));
                    try
                    {
                        if (string.IsNullOrWhiteSpace(imageUrl))
                        {
                            api.UpdateStatusAsync(content + url).Wait();
                        }
                        else
                        {
                            string str8 = imageUrl;
                            if (imageUrl.Contains("http://"))
                            {
                                System.Net.WebClient client2 = new System.Net.WebClient();
                                string str9 = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                if (!Directory.Exists(HttpContext.Current.Server.MapPath(str9)))
                                {
                                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(str9));
                                }
                                str8 = str9 + this.CreateIDCode() + ".jpg";
                                client2.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(str8));
                            }
                            api.UploadStatusAsync(content + url, new FileInfo(HttpContext.Current.Server.MapPath(str8))).Wait();
                        }
                        continue;
                    }
                    catch (Exception exception2)
                    {
                        Maticsoft.Model.SysManage.ErrorLog log2 = new Maticsoft.Model.SysManage.ErrorLog {
                            Loginfo = exception2.Message,
                            OPTime = DateTime.Now,
                            StackTrace = exception2.StackTrace,
                            Url = ""
                        };
                        Maticsoft.BLL.SysManage.ErrorLog.Add(log2);
                        continue;
                    }
                }
            }
        }

        public void SendWeiBo(int userId, string mediaIDs, string content, string url, string imageUrl = new string())
        {
            string strWhere = " userid=" + userId;
            if (!string.IsNullOrWhiteSpace(mediaIDs))
            {
                strWhere = strWhere + " and MediaID in  (" + mediaIDs + ")";
            }
            List<Maticsoft.Model.Members.UserBind> modelList = this.GetModelList(strWhere);
            if ((modelList != null) && (modelList.Count != 0))
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    url = "http://" + Globals.DomainFullName;
                }
                foreach (Maticsoft.Model.Members.UserBind bind in modelList)
                {
                    switch (bind.MediaID)
                    {
                        case 3:
                        {
                            string clientId = ConfigSystem.GetValueByCache("Social_SinaAppId");
                            string str3 = ConfigSystem.GetValueByCache("Social_SinaSercet");
                            IOAuth2ServiceProvider<Maticsoft.OAuth.Sina.IWeibo> provider = new Maticsoft.OAuth.Sina.WeiboServiceProvider(clientId, str3);
                            Maticsoft.OAuth.Sina.IWeibo weibo = provider.GetApi(new AccessGrant(bind.TokenAccess, new string[] { bind.MediaUserID }));
                            try
                            {
                                if (string.IsNullOrWhiteSpace(imageUrl))
                                {
                                    weibo.UpdateStatusAsync(content + " " + url).Wait();
                                }
                                else
                                {
                                    string str4 = imageUrl;
                                    if (imageUrl.Contains("http://"))
                                    {
                                        System.Net.WebClient client = new System.Net.WebClient();
                                        string str5 = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(str5)))
                                        {
                                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(str5));
                                        }
                                        str4 = str5 + this.CreateIDCode() + ".jpg";
                                        client.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(str4));
                                    }
                                    weibo.UploadStatusAsync(content + url, new FileInfo(HttpContext.Current.Server.MapPath(str4))).Wait();
                                }
                            }
                            catch (Exception exception)
                            {
                                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                                    Loginfo = exception.Message,
                                    OPTime = DateTime.Now,
                                    StackTrace = exception.StackTrace,
                                    Url = ""
                                };
                                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                            }
                            continue;
                        }
                        case 4:
                            break;

                        case 13:
                        {
                            string str6 = ConfigSystem.GetValueByCache("Social_QQAppId");
                            string str7 = ConfigSystem.GetValueByCache("Social_QQSercet");
                            IOAuth2ServiceProvider<IQConnect> provider2 = new QConnectServiceProvider(str6, str7);
                            IQConnect connect = provider2.GetApi(new AccessGrant(bind.TokenAccess, new string[] { bind.MediaUserID }));
                            try
                            {
                                if (string.IsNullOrWhiteSpace(imageUrl))
                                {
                                    connect.UpdateStatusAsync(content + url).Wait();
                                }
                                else
                                {
                                    string str8 = imageUrl;
                                    if (imageUrl.Contains("http://"))
                                    {
                                        System.Net.WebClient client2 = new System.Net.WebClient();
                                        string str9 = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(str9)))
                                        {
                                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(str9));
                                        }
                                        str8 = str9 + this.CreateIDCode() + ".jpg";
                                        client2.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(str8));
                                    }
                                    connect.UploadStatusAsync(content + url, new FileInfo(HttpContext.Current.Server.MapPath(str8))).Wait();
                                }
                            }
                            catch (Exception exception2)
                            {
                                Maticsoft.Model.SysManage.ErrorLog log2 = new Maticsoft.Model.SysManage.ErrorLog {
                                    Loginfo = exception2.Message,
                                    OPTime = DateTime.Now,
                                    StackTrace = exception2.StackTrace,
                                    Url = ""
                                };
                                Maticsoft.BLL.SysManage.ErrorLog.Add(log2);
                            }
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                    string valueByCache = ConfigSystem.GetValueByCache("Social_TencentAppId");
                    string clientSecret = ConfigSystem.GetValueByCache("Social_TencentSercet");
                    IOAuth2ServiceProvider<Maticsoft.OAuth.Tencent.Weibo.IWeibo> provider3 = new Maticsoft.OAuth.Tencent.Weibo.WeiboServiceProvider(valueByCache, clientSecret);
                    string[] strArray = bind.MediaUserID.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray.Length < 2)
                    {
                        throw new ArgumentNullException(" OpenIdKeys is NULL !");
                    }
                    Maticsoft.OAuth.Tencent.Weibo.IWeibo api = provider3.GetApi(new AccessGrant(bind.TokenAccess, new string[] { strArray[0], strArray[1], Globals.ClientIP }));
                    if (string.IsNullOrWhiteSpace(imageUrl))
                    {
                        api.UpdateStatusAsync(content + url).Wait();
                        continue;
                    }
                    string path = imageUrl;
                    if (imageUrl.Contains("http://"))
                    {
                        System.Net.WebClient client3 = new System.Net.WebClient();
                        string str13 = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(str13)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(str13));
                        }
                        path = str13 + this.CreateIDCode() + ".jpg";
                        client3.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(path));
                    }
                    api.UploadStatusAsync(content + url, new FileInfo(HttpContext.Current.Server.MapPath(path))).Wait();
                }
            }
        }

        public void SendWeiBo(int userId, string mediaUserIDs, string content, string url = new string(), string imageUrl = new string(), string videourl = new string())
        {
            if (mediaUserIDs == "-1")
            {
                List<Maticsoft.Model.Members.UserBind> modelList = this.GetModelList(" userid=" + userId);
                if ((modelList != null) && (modelList.Count > 0))
                {
                    mediaUserIDs = string.Join(",", (IEnumerable<string>) (from c in modelList select c.MediaUserID));
                }
            }
            if (!string.IsNullOrWhiteSpace(imageUrl) && !imageUrl.StartsWith("http://"))
            {
                imageUrl = "http://" + Globals.DomainFullName + imageUrl;
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "http://" + Globals.DomainFullName;
            }
            string valueByCache = ConfigSystem.GetValueByCache("DengluAPPID");
            string apiKey = ConfigSystem.GetValueByCache("DengluAPIKEY");
            new Denglu(valueByCache, apiKey, "MD5").share(mediaUserIDs, content, url, userId.ToString(), imageUrl, videourl);
        }

        public bool Update(Maticsoft.Model.Members.UserBind model)
        {
            return this.dal.Update(model);
        }
    }
}

