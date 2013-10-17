namespace Maticsoft.OAuth
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    public class Denglu
    {
        private string apiKey;
        protected static readonly Dictionary<string, string> apiPath;
        private string appID;
        public static string domain = "http://open.denglu.cc";
        public static readonly Dictionary<string, Provider> providers;
        private string signatureMethod;
        public static readonly float version;

        static Denglu()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("user_info", domain + "/api/v4/user_info");
            dictionary.Add("get_media", domain + "/api/v4/get_media");
            dictionary.Add("bind", domain + "/api/v4/bind");
            dictionary.Add("unbind", domain + "/api/v4/unbind");
            dictionary.Add("all_unbind", domain + "/api/v4/all_unbind");
            dictionary.Add("send_login_feed", domain + "/api/v4/send_login_feed");
            dictionary.Add("share", domain + "/api/v4/share");
            apiPath = dictionary;
            Dictionary<string, Provider> dictionary2 = new Dictionary<string, Provider>();
            dictionary2.Add("Google", new Provider(domain + "/transfer/google", domain + "/transfer/google", 1, "Google"));
            dictionary2.Add("WindowsLive", new Provider(domain + "/transfer/windowslive", domain + "/transfer/windowslive", 2, "Windows Live"));
            dictionary2.Add("Sina", new Provider(domain + "/transfer/sina", domain + "/transfer/sina", 3, "新浪微博"));
            dictionary2.Add("Tencent", new Provider(domain + "/transfer/tencent", domain + "/transfer/tencent", 4, "腾讯微博"));
            dictionary2.Add("Sohu", new Provider(domain + "/transfer/sohu", domain + "/transfer/sohu", 5, "搜狐微博"));
            dictionary2.Add("Netease", new Provider(domain + "/transfer/netease", domain + "/transfer/netease", 6, "网易微博"));
            dictionary2.Add("Renren", new Provider(domain + "/transfer/renren", domain + "/transfer/renren", 7, "人人网"));
            dictionary2.Add("Kaixin001", new Provider(domain + "/transfer/kaixin001", domain + "/transfer/kaixin001", 8, "开心网"));
            dictionary2.Add("Douban", new Provider(domain + "/transfer/douban", domain + "/transfer/douban", 9, "豆瓣网"));
            dictionary2.Add("Yahoo", new Provider(domain + "/transfer/yahoo", domain + "/transfer/yahoo", 12, "雅虎"));
            dictionary2.Add("QZone", new Provider(domain + "/transfer/qzone", domain + "/transfer/qzone", 13, "QQ空间"));
            dictionary2.Add("Taobao", new Provider(domain + "/transfer/taobao", domain + "/transfer/taobao", 0x10, "淘宝"));
            dictionary2.Add("Tianya", new Provider(domain + "/transfer/tianya", domain + "/transfer/tianya", 0x11, "天涯"));
            dictionary2.Add("AlipayQuick", new Provider(domain + "/transfer/alipayquick", domain + "/transfer/alipayquick", 0x12, "支付宝"));
            dictionary2.Add("Baidu", new Provider(domain + "/transfer/baidu", domain + "/transfer/baidu", 0x13, "百度"));
            providers = dictionary2;
            version = 1f;
        }

        public Denglu(string appID, string apiKey, string signatureMethod)
        {
            this.AppID = appID;
            this.ApiKey = apiKey;
            this.SignatureMethod = signatureMethod;
        }

        public JsonObject bind(string mediaUID, string uid, string uname, string uemail)
        {
            string url = apiPath["bind"];
            List<Parameter> parameters = new List<Parameter> {
                new Parameter("appid", this.appID),
                new Parameter("muid", mediaUID),
                new Parameter("uid", uid),
                new Parameter("uname", uname),
                new Parameter("uemail", uemail)
            };
            parameters = this.signRequest(parameters);
            HttpWebRequest request = this.combineRequest(url, parameters);
            string input = this.makeRequest(request);
            return (JsonObject) this.parseJson(input);
        }

        protected HttpWebRequest combineRequest(string url, List<Parameter> parameters)
        {
            url = url + "?";
            foreach (Parameter parameter in parameters)
            {
                if (!url.EndsWith("?"))
                {
                    url = url + "&";
                }
                url = url + parameter.toParamString();
            }
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = Verb.GET.ToString();
            return request;
        }

        public string getAuthUrl(bool isBind, string provider, string uid)
        {
            Provider provider2 = providers[provider];
            if (isBind && (uid == null))
            {
                throw new DengluException(1, "绑定时，需要传送用户ID");
            }
            if (isBind)
            {
                return (provider2.BindUrl + "?appid=" + this.appID + "&uid=" + uid);
            }
            return (provider2.AuthUrl + "?appid=" + this.appID);
        }

        public DataTable getDTByJson(string str)
        {
            str = str.Replace("{", "");
            str = str.Replace("}", "").Trim();
            string[] strArray = str.Split(new char[] { ',' });
            DataTable table = new DataTable("strDT");
            foreach (string str2 in strArray)
            {
                string[] strArray2 = str2.Trim().Split(new char[] { ':' });
                table.Columns.Add(strArray2[0].Replace("\"", ""), typeof(string));
            }
            DataRow row = table.NewRow();
            foreach (string str4 in strArray)
            {
                string[] strArray3 = str4.Trim().Split(new char[] { ':' });
                string str6 = "";
                for (int i = 1; i < strArray3.Length; i++)
                {
                    if (i == (strArray3.Length - 1))
                    {
                        str6 = str6 + strArray3[i].Trim();
                    }
                    else
                    {
                        str6 = str6 + strArray3[i].Trim() + ":";
                    }
                }
                row[strArray3[0].Replace("\"", "")] = str6.Trim().Replace("\"", "");
            }
            table.Rows.Add(row);
            return table;
        }

        public DataTable getDTByJson_s(string str)
        {
            str = str.Replace("{", "");
            str = str.Replace("}", "").Trim();
            string[] strArray = str.Split(new char[] { ',' });
            DataTable table = new DataTable("strDT");
            foreach (string str2 in strArray)
            {
                string[] strArray2 = str2.Trim().Replace("\"", "").Split(new char[] { ':', ' ' });
                table.Columns.Add(strArray2[0], typeof(string));
            }
            DataRow row = table.NewRow();
            foreach (string str4 in strArray)
            {
                string[] strArray3 = str4.Trim().Replace("\"", "").Split(new char[] { ':', ' ' });
                row[strArray3[0]] = strArray3[2].Trim();
            }
            table.Rows.Add(row);
            return table;
        }

        public JsonArray getMedia()
        {
            string url = apiPath["get_media"];
            List<Parameter> parameters = new List<Parameter> {
                new Parameter("appid", this.appID)
            };
            parameters = this.signRequest(parameters);
            HttpWebRequest request = this.combineRequest(url, parameters);
            string input = this.makeRequest(request);
            return (JsonArray) this.parseJson(input);
        }

        public JsonObject getUserInfoByToken(string token)
        {
            string url = apiPath["user_info"];
            List<Parameter> parameters = new List<Parameter> {
                new Parameter("appid", this.AppID),
                new Parameter("sign_type", this.signatureMethod)
            };
            long num = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 0x2710L;
            parameters.Add(new Parameter("timestamp", num.ToString()));
            parameters.Add(new Parameter("token", token));
            parameters.Add(new Parameter("version", version.ToString()));
            parameters = this.signRequest(parameters);
            HttpWebRequest request = this.combineRequest(url, parameters);
            string input = this.makeRequest(request);
            return (JsonObject) this.parseJson(input);
        }

        public string getUserInfoByToken_(string token)
        {
            string str = apiPath["user_info"];
            List<Parameter> parameters = new List<Parameter> {
                new Parameter("appid", this.appID),
                new Parameter("token", token)
            };
            long num = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 0x2710L;
            parameters.Add(new Parameter("timestamp", num.ToString()));
            parameters.Add(new Parameter("sign_type", this.SignatureMethod));
            parameters = this.signRequest(parameters);
            string str2 = ((DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 0x2710L).ToString();
            string str4 = FormsAuthentication.HashPasswordForStoringInConfigFile(("appid=" + this.AppID + "sign_type=" + this.SignatureMethod + "timestamp=" + str2 + "token=" + token + "version=" + version.ToString()) + this.ApiKey, "MD5").ToLower();
            string str5 = "appid=" + this.AppID + "&sign_type=" + this.SignatureMethod + "&timestamp=" + str2 + "&token=" + token + "&version=" + version.ToString() + "&sign=" + str4;
            return (str + "?" + str5);
        }

        protected string makeRequest(HttpWebRequest request)
        {
            Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string str = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();
            return str;
        }

        protected object parseJson(string input)
        {
            if (!input.Trim().StartsWith("{"))
            {
                return JsonConvert.Import<JsonArray>(input);
            }
            JsonObject obj2 = JsonConvert.Import<JsonObject>(input);
            if (obj2["errorCode"] != null)
            {
                throw new DengluException(int.Parse(obj2["errorCode"].ToString()), obj2["errorDescription"].ToString());
            }
            return obj2;
        }

        public void sendLoginFeed(string mediaUID)
        {
            string url = apiPath["send_login_feed"];
            List<Parameter> parameters = new List<Parameter> {
                new Parameter("appid", this.appID),
                new Parameter("muid", mediaUID)
            };
            parameters = this.signRequest(parameters);
            HttpWebRequest request = this.combineRequest(url, parameters);
            this.makeRequest(request);
        }

        public void share(string mediaUserID, string content, string url, string uid, string imageurl = new string(), string videourl = new string())
        {
            string str = apiPath["share"];
            List<Parameter> parameters = new List<Parameter> {
                new Parameter("appid", this.appID),
                new Parameter("muid", mediaUserID),
                new Parameter("uid", uid),
                new Parameter("content", content),
                new Parameter("url", url)
            };
            if (!string.IsNullOrWhiteSpace(imageurl))
            {
                parameters.Add(new Parameter("imageurl", imageurl));
            }
            if (!string.IsNullOrWhiteSpace(videourl))
            {
                parameters.Add(new Parameter("videourl", videourl));
            }
            parameters.Add(new Parameter("version", version.ToString()));
            long num2 = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 0x2710L;
            parameters.Add(new Parameter("timestamp", num2.ToString()));
            parameters.Add(new Parameter("sign_type", this.signatureMethod));
            parameters = this.signRequest(parameters);
            HttpWebRequest request = this.combineRequest(str, parameters);
            this.makeRequest(request);
        }

        protected List<Parameter> signRequest(List<Parameter> parameters)
        {
            parameters.Sort(new ParameterComparer());
            string str = "";
            foreach (Parameter parameter in parameters)
            {
                str = str + parameter.toParamString();
            }
            string str2 = FormsAuthentication.HashPasswordForStoringInConfigFile(str + this.apiKey, "MD5").ToLower();
            parameters.Add(new Parameter("sign", str2));
            return parameters;
        }

        public JsonObject unbind(string mediaUID)
        {
            string url = apiPath["unbind"];
            List<Parameter> parameters = new List<Parameter> {
                new Parameter("appid", this.appID),
                new Parameter("muid", mediaUID)
            };
            parameters = this.signRequest(parameters);
            HttpWebRequest request = this.combineRequest(url, parameters);
            string input = this.makeRequest(request);
            return (JsonObject) this.parseJson(input);
        }

        protected string ApiKey
        {
            get
            {
                return this.apiKey;
            }
            set
            {
                this.apiKey = value;
            }
        }

        protected string AppID
        {
            get
            {
                return this.appID;
            }
            set
            {
                this.appID = value;
            }
        }

        protected string SignatureMethod
        {
            get
            {
                return this.signatureMethod;
            }
            set
            {
                this.signatureMethod = value;
            }
        }

        public enum Verb
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}

