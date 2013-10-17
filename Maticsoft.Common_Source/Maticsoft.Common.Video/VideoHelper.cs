namespace Maticsoft.Common.Video
{
    using Maticsoft.Common;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Xml;

    public class VideoHelper
    {
        private static string ku6XmlDataApiUrl = "http://v.ku6.com/repaste.htm?url={0}";
        private static string youKuJsonDataApiUrl = "http://v.youku.com/player/getPlayList/VideoIDS/{0}/version/5/source/out?onData=%5Btype%20Function%5D&n=3";

        public static Ku6Info GetKu6Info(string url)
        {
            Ku6Info info = new Ku6Info();
            string xml = PageLoader.Download(string.Format(ku6XmlDataApiUrl, url));
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            XmlNode node = document.SelectSingleNode("root/result");
            int num = -1;
            if ((node != null) && (node.Attributes.GetNamedItem("type") != null))
            {
                num = Convert.ToInt32(node.Attributes.GetNamedItem("type").Value);
            }
            else
            {
                return null;
            }
            if (num == -1)
            {
                return null;
            }
            info.type = num;
            XmlNode node2 = document.SelectSingleNode("root/result/vid");
            if (node2 != null)
            {
                info.vid = node2.InnerText;
            }
            XmlNode node3 = document.SelectSingleNode("root/result/coverurl");
            if (node3 != null)
            {
                info.coverurl = node3.InnerText;
            }
            XmlNode node4 = document.SelectSingleNode("root/result/flash");
            if (node4 != null)
            {
                info.flash = node4.InnerText;
            }
            XmlNode node5 = document.SelectSingleNode("root/result/title");
            if (node5 != null)
            {
                info.title = node5.InnerText;
            }
            XmlNode node6 = document.SelectSingleNode("root/result/desc");
            if (node6 != null)
            {
                info.desc = node6.InnerText;
            }
            return info;
        }

        public static YouKuInfo GetYouKuInfo(string url)
        {
            string youKuVideoId = GetYouKuVideoId(url);
            if (StringPlus.IsNullOrEmpty(youKuVideoId))
            {
                return null;
            }
            string target = PageLoader.Download(string.Format(youKuJsonDataApiUrl, youKuVideoId));
            if (StringPlus.IsNullOrEmpty(target))
            {
                return null;
            }
            string str3 = "{\"data\":[";
            if (target.StartsWith(str3))
            {
                target = target.Replace(str3, "");
            }
            string str4 = "}";
            if (target.EndsWith(str4))
            {
                int length = target.LastIndexOf(str4);
                if (length > 0)
                {
                    target = target.Substring(0, length);
                }
            }
            return (YouKuInfo) JsonConvert.Import(typeof(YouKuInfo), target);
        }

        public static string GetYouKuVideoId(string url)
        {
            string target = "";
            if (StringPlus.IsNullOrEmpty(url))
            {
                return "";
            }
            target = StringPlus.TrimStart(url, "http://v.youku.com/v_show/id_");
            if (StringPlus.IsNullOrEmpty(target))
            {
                return "";
            }
            int index = target.IndexOf(".html");
            if (index > 0)
            {
                target = target.Substring(0, index);
            }
            return target;
        }

        public static bool IsKu6VideoUrl(string url)
        {
            string str = PageLoader.Download(string.Format(ku6XmlDataApiUrl, url));
            if (!string.IsNullOrEmpty(str))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(str);
                XmlNode node = document.SelectSingleNode("root/result");
                int num = -1;
                if ((node != null) && (node.Attributes.GetNamedItem("type") != null))
                {
                    num = Convert.ToInt32(node.Attributes.GetNamedItem("type").Value);
                    if (num == -1)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public static bool IsYouKuVideoUrl(string url)
        {
            return url.StartsWith("http://v.youku.com/v_show/id_");
        }
    }
}

