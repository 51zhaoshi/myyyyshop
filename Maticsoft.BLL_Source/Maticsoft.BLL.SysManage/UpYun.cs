namespace Maticsoft.BLL.SysManage
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    public class UpYun
    {
        private string api_domain = "v0.api.upyun.com";
        private bool auto_mkdir;
        private string bucketname;
        private string content_md5;
        private string DL = "/";
        private string file_secret;
        private string password;
        private Hashtable tmp_infos = new Hashtable();
        private bool upAuth;
        private string username;

        public UpYun(string bucketname, string username, string password)
        {
            this.bucketname = bucketname;
            this.username = username;
            this.password = password;
        }

        private bool delete(string path, Hashtable headers)
        {
            byte[] postData = null;
            HttpWebResponse response = this.newWorker("DELETE", this.DL + this.bucketname + path, postData, headers);
            if (response == null)
            {
                return true;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.Close();
                return true;
            }
            response.Close();
            return false;
        }

        public bool deleteFile(string path)
        {
            Hashtable headers = new Hashtable();
            return this.delete(path, headers);
        }

        public int getBucketUsage()
        {
            return this.getFolderUsage("");
        }

        public Hashtable getFileInfo(string file)
        {
            Hashtable hashtable2;
            Hashtable headers = new Hashtable();
            byte[] postData = null;
            HttpWebResponse response = this.newWorker("HEAD", this.DL + this.bucketname + file, postData, headers);
            if (response == null)
            {
                return null;
            }
            response.Close();
            try
            {
                hashtable2 = new Hashtable();
                hashtable2.Add("type", this.tmp_infos["x-upyun-file-type"]);
                hashtable2.Add("size", this.tmp_infos["x-upyun-file-size"]);
                hashtable2.Add("date", this.tmp_infos["x-upyun-file-date"]);
            }
            catch (Exception)
            {
                hashtable2 = null;
            }
            return hashtable2;
        }

        public int getFolderUsage(string url)
        {
            Hashtable headers = new Hashtable();
            byte[] postData = null;
            HttpWebResponse response = this.newWorker("GET", this.DL + this.bucketname + url + "?usage", postData, headers);
            if (response == null)
            {
                return 0;
            }
            try
            {
                string s = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                response.Close();
                return int.Parse(s);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public object getWritedFileInfo(string key)
        {
            if (this.tmp_infos == new Hashtable())
            {
                return "";
            }
            return this.tmp_infos[key];
        }

        private string md5(string str)
        {
            MD5 md = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "").ToLower();
        }

        public static string md5_file(string pathName)
        {
            string str = "";
            FileStream inputStream = null;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            try
            {
                inputStream = new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] buffer = provider.ComputeHash(inputStream);
                inputStream.Close();
                str = BitConverter.ToString(buffer).Replace("-", "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str.ToLower();
        }

        public bool mkDir(string path, bool auto_mkdir)
        {
            this.auto_mkdir = auto_mkdir;
            Hashtable headers = new Hashtable();
            headers.Add("folder", "create");
            byte[] postData = new byte[0];
            HttpWebResponse response = this.newWorker("POST", this.DL + this.bucketname + path, postData, headers);
            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    response.Close();
                    return true;
                }
                response.Close();
            }
            return false;
        }

        private HttpWebResponse newWorker(string method, string Url, byte[] postData, Hashtable headers)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://" + this.api_domain + Url);
            request.Method = method;
            if (this.auto_mkdir)
            {
                headers.Add("mkdir", "true");
                this.auto_mkdir = false;
            }
            if (postData != null)
            {
                request.ContentLength = postData.Length;
                request.KeepAlive = true;
                if (this.content_md5 != null)
                {
                    request.Headers.Add("Content-MD5", this.content_md5);
                    this.content_md5 = null;
                }
                if (this.file_secret != null)
                {
                    request.Headers.Add("Content-Secret", this.file_secret);
                    this.file_secret = null;
                }
            }
            if (this.upAuth)
            {
                this.upyunAuth(headers, method, Url, request);
            }
            else
            {
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(this.username + ":" + this.password)));
            }
            foreach (DictionaryEntry entry in headers)
            {
                request.Headers.Add(entry.Key.ToString(), entry.Value.ToString());
            }
            if (postData != null)
            {
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                this.tmp_infos = new Hashtable();
                foreach (object obj2 in response.Headers)
                {
                    string key = (string) obj2;
                    if ((key.Length > 7) && (key.Substring(0, 7) == "x-upyun"))
                    {
                        this.tmp_infos.Add(key, response.Headers[key]);
                    }
                }
            }
            catch (Exception)
            {
            }
            return response;
        }

        public ArrayList readDir(string url)
        {
            Hashtable headers = new Hashtable();
            byte[] postData = null;
            HttpWebResponse response = this.newWorker("GET", this.DL + this.bucketname + url, postData, headers);
            ArrayList list = new ArrayList();
            if (response != null)
            {
                string str = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                response.Close();
                string[] strArray = str.Replace("\t", @"\").Replace("\n", @"\").Split(new char[] { '\\' });
                for (int i = 0; i < strArray.Length; i += 4)
                {
                    FolderItem item = new FolderItem(strArray[i], strArray[i + 1], int.Parse(strArray[i + 2]), int.Parse(strArray[i + 3]));
                    list.Add(item);
                }
            }
            return list;
        }

        public byte[] readFile(string path)
        {
            Hashtable headers = new Hashtable();
            byte[] postData = null;
            HttpWebResponse response = this.newWorker("GET", this.DL + this.bucketname + path, postData, headers);
            if (response == null)
            {
                return postData;
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            byte[] buffer2 = new BinaryReader(reader.BaseStream).ReadBytes(0x6400000);
            response.Close();
            return buffer2;
        }

        public bool rmDir(string path)
        {
            Hashtable headers = new Hashtable();
            return this.delete(path, headers);
        }

        public void setApiDomain(string domain)
        {
            this.api_domain = domain;
        }

        public void setAuthType(bool upAuth)
        {
            this.upAuth = upAuth;
        }

        public void setContentMD5(string str)
        {
            this.content_md5 = str;
        }

        public void setFileSecret(string str)
        {
            this.file_secret = str;
        }

        private void upyunAuth(Hashtable headers, string method, string uri, HttpWebRequest request)
        {
            string str2;
            DateTime utcNow = DateTime.UtcNow;
            string str = utcNow.ToString("ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));
            request.Date = utcNow;
            if (request.ContentLength != -1L)
            {
                str2 = this.md5(string.Concat(new object[] { method, '&', uri, '&', str, '&', request.ContentLength, '&', this.md5(this.password) }));
            }
            else
            {
                str2 = this.md5(string.Concat(new object[] { method, '&', uri, '&', str, '&', 0, '&', this.md5(this.password) }));
            }
            headers.Add("Authorization", string.Concat(new object[] { "UpYun ", this.username, ':', str2 }));
        }

        public string version()
        {
            return "1.0.1";
        }

        public bool writeFile(string path, byte[] data, bool auto_mkdir)
        {
            Hashtable headers = new Hashtable();
            this.auto_mkdir = auto_mkdir;
            HttpWebResponse response = this.newWorker("POST", this.DL + this.bucketname + path, data, headers);
            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    response.Close();
                    return true;
                }
                response.Close();
            }
            return false;
        }
    }
}

