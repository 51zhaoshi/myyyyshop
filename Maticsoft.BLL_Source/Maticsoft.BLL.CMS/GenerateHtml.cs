namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;

    public class GenerateHtml
    {
        public void CreateHtml(string temPath, Maticsoft.Model.CMS.Content model, string createPath)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            try
            {
                str2 = HttpContext.Current.Server.MapPath(createPath);
                StreamReader reader = new StreamReader(System.IO.File.Open(HttpContext.Current.Server.MapPath(temPath), FileMode.Open, FileAccess.Read), Encoding.GetEncoding("utf-8"));
                StringBuilder builder = new StringBuilder(reader.ReadToEnd());
                reader.Close();
                reader.Dispose();
                string str4 = builder.ToString().Replace("$id$", model.ContentID.ToString()).Replace("$content$", model.Description);
                this.CreatFolder(createPath);
                str = "ContentDetail-" + model.ContentID + ".html";
                StreamWriter writer = new StreamWriter(System.IO.File.Create(str2 + "/" + str), Encoding.GetEncoding("utf-8"));
                writer.Write(str4);
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreatFolder(string path)
        {
            string str = string.Empty;
            try
            {
                str = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DelHtml(string id, string HtmlName)
        {
            string str = ConfigurationManager.AppSettings["SaveHtmlUrl"].ToString();
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(str + HtmlName + "-" + id + ".html")))
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(str + HtmlName + "-" + id + ".html"));
            }
        }

        public void DelHtmlList(string DelHtmlList, string HtmlName)
        {
            string str = ConfigurationManager.AppSettings["SaveHtmlUrl"].ToString();
            foreach (string str2 in DelHtmlList.Split(new char[] { ',' }))
            {
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(str + HtmlName + "-" + str2 + ".html")))
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(str + HtmlName + "-" + str2 + ".html"));
                }
            }
        }

        public static bool GenImageJs()
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            StreamWriter writer = null;
            string str = null;
            string path = "/Scripts/maticsoft.collection.min.js";
            string str3 = "(function(){function t(e,t,n){e.attachEvent?(e[\"e\"+t+n]=n,e[t+n]=function(){e[\"e\"+t+n](window.event)},e.attachEvent(\"on\"+t,e[t+n])):e.addEventListener(t,n,!1)}function n(e,t){return e.className.match(RegExp(\"(\\\\s|^)\"+t+\"(\\\\s|$)\"))}function r(e,t){n(e,t)||(e.className+=\" \"+t)}function i(e,t){n(e,t)&&(e.className=e.className.replace(RegExp(\"(\\\\s|^)\"+t+\"(\\\\s|$)\"),\" \"))}var e=\"";
            string str4 = "\";var s=!!window.ActiveXObject,o=s&&!window.XMLHttpRequest,u=function(e){for(var e=e.split(\",\"),t=e.length,n=[],r=0;r<t;r++){var i=document.getElementById(e[r]);i&&n.push(i)}return n},a=function(e,t){if(document.getElementsByClassName)return(t||document).getElementsByClassName(e);t=t||document,tag=\"*\";for(var n=[],r=tag===\"*\"&&t.all?t.all:t.getElementsByTagName(tag),i=r.length,e=e.replace(/\\-/g,\"\\\\-\"),s=RegExp(\"(^|\\\\s)\"+e+\"(\\\\s|$)\");--i>=0;)s.test(r[i].className)&&n.push(r[i]);return n},l=function(){for(var e=u(\"maticsoftShareBg,maticsoftShareToolBar,maticsoftShareBtn,maticsoftShareContent,maticsoftShareScript,maticsoftShareStyle\"),t=e.length,n=0;n<t;n++){var r=e[n],i=r.parentNode;i&&i.removeChild(r)}};if(u(\"maticsoftShareToolBar\").length!=0||u(\"maticsoftShareBtn\").length!=0)l();else{var c=location.hostname,h=new RegExp(c,\"i\");if(h.test(e))alert(\"您就在本站，不能采集本站的图片\");else{var p=function(){for(var e=[{name:\"duitang\",r:/duitang.com/i},{name:\"meilishuo\",r:/meilishuo.com/i},{name:\"huaban\",r:/huaban.com/i},{name:\"pinterest\",r:/pinterest.com/i}],t=0;t<e.length;t++)if(e[t].r.test(c))return e[t].name;return!1};if(!function(e){var t=/tmall.com/i,n=/auction\\d?.paipai.com/i,r=/buy.caomeipai.com\\/goods/i,i=/www.360buy.com\\/product/i,s=/product.dangdang.com\\/Product.aspx\\?product_id=/i,o=/book.360buy.com/i,u=/www.vancl.com\\/StyleDetail/i,a=/www.vancl.com\\/Product/i,f=/vt.vancl.com\\/item/i,l=/item.vancl.com\\/\\d+/i,c=/mbaobao.com\\/pshow/i,h=/[www|us].topshop.com\\/webapp\\/wcs\\/stores\\/servlet\\/ProductDisplay/i,p=/quwan.com\\/goods/i,d=/nala.com.cn\\/product/i,v=/maymay.cn\\/pitem/i,m=/asos.com/i;return/item(.lp)?.taobao.com\\/(.?)[item.htm|item_num_id|item_detail|itemID|item_id|default_item_id]/i.test(e)||t.test(e)||o.test(e)||i.test(e)||n.test(e)||r.test(e)||s.test(e)||u.test(e)||a.test(e)||f.test(e)||l.test(e)||c.test(e)||h.test(e)||p.test(e)||d.test(e)||v.test(e)||m.test(e)}(location.href)){var d=\"#maticsoftShareBg {background-color:#f2f2f2; height:100%; width:100%; left:0px; top:0px; zoom:1; position:fixed; z-index:100000; opacity:0.8; FILTER:alpha(opacity=80); } #maticsoftShareContent {position:absolute; top:66px; left:0; z-index:100001; } #maticsoftShareContent .mgsFeed {width:200px; height:200px; border-right:1px solid #e7e7e7; border-bottom:1px solid #e7e7e7; float:left; cursor:pointer; text-align:center; background-color:#FFF; overflow:hidden; position:relative; } #maticsoftShareContent .mgsPic {max-height:200px; max-width:200px; } #maticsoftShareContent .mgsSize {position:absolute; bottom:5px; left:0; width:200px; text-align:center; } #maticsoftShareContent .mgsSize span {display:inline-block; background-color:#FFF; border-radius:4px; padding:0 2px; } #maticsoftShareContent .mgsSelect {position:absolute; right:12px; bottom:10px; width:28px; height:28px; background:url(\"+e+\"/images/select.png) 0 0 no-repeat; }  #maticsoftShareContent .selected {background-position:0 -50px;} #maticsoftShareToolBar {position:fixed; top:0; left:0; z-index:100002; height:75px; width:100%; overflow:hidden; background:url(\"+e+\"/images/mgs_bar_bg.png) top repeat-x; } #maticsoftShareToolBar .maticsoftShadow {position:absolute; width:100%; height:9px; overflow:hidden; top:65px; left:0; background:url(\"+e+\"/images/mgs_bar_bg_sd.png) repeat-x; } #maticsoftShareToolBar .maticsoftLogo {position:absolute; right:25px; top:15px; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:25px; top:8px; width:156px; height:49px; background:url(\"+e+\"/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:190px; top:8px; width:156px; height:49px; background:url(\"+e+\"/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftCancel {position:absolute; right:25px; top:16px; width:69px; height:31px; background:url(\"+e+\"/images/cancel.png) no-repeat; }#maticsoftShareToolBar .maticsoftNotice{position: absolute;font-size:14px;top:23px;left:360px;color:#555}\",v='body{background-attachment:fixed; background-image:url(\"about:blank\");}#maticsoftShareBg {background-color:#f2f2f2; height:expression(document.body.clientHeight); width:100%; left:0px; zoom:1; z-index:100000; FILTER:alpha(opacity=80); position:absolute; top:expression(document.compatMode && document.compatMode==\"CSS1Compat\" ? documentElement.scrollTop:document.body.scrollTop ); } #maticsoftShareContent {position:absolute; top:66px; left:0; z-index:100001; } #maticsoftShareContent .mgsFeed {width:200px; height:200px; border-right:1px solid #e7e7e7; border-bottom:1px solid #e7e7e7; float:left; cursor:pointer; text-align:center; background-color:#FFF; overflow:hidden; position:relative; } #maticsoftShareContent .mgsPic {max-height:200px; max-width:200px; } #maticsoftShareContent .mgsSize {position:absolute; bottom:5px; left:0; width:200px; text-align:center; } #maticsoftShareContent .mgsSize span {display:inline-block; background-color:#FFF; border-radius:4px; padding:0 2px; } #maticsoftShareContent .mgsSelect {position:absolute; right:12px; bottom:10px; width:28px; height:28px; background:url('+e+'/images/select.png) 0 0 no-repeat; }  #maticsoftShareContent .selected {background-position:0 -50px;} #maticsoftShareToolBar {position:absolute; top:expression(document.compatMode && document.compatMode==\"CSS1Compat\" ? documentElement.scrollTop:document.body.scrollTop ); left:0; z-index:100002; height:75px; width:100%; overflow:hidden; background:url('+e+'/images/mgs_bar_bg.png) top repeat-x; } #maticsoftShareToolBar .maticsoftShadow {position:absolute; width:100%; height:9px; overflow:hidden; top:65px; left:0; FILTER:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\"'+e+'/images/mgs_bar_bg_sd.png\",sizingMethod=\"scale\"); background-image:none } #maticsoftShareToolBar .maticsoftLogo {position:absolute; right:25px; top:15px; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:25px; top:8px; width:156px; height:49px; background:url('+e+\"/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:190px; top:8px; width:156px; height:49px; background:url(\"+e+\"/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftCancel {position:absolute; right:25px; top:16px; width:69px; height:31px; background:url(\"+e+\"/images/cancel.png) no-repeat; }#maticsoftShareToolBar .maticsoftNotice{position: absolute;font-size:14px;top:23px;left:360px;color:#555}\",m='<div class=\"mgsFeed\"><img class=\"mgsPic\" alt=\"{alt}\" style=\"{style}\" src=\"{picUrl}\" osrc=\"{opicUrl}\" ><div class=\"mgsSize\"><span>{width}x{height}</span></div><i class=\"mgsSelect\"></i></div>',g=[],b=p();b&&(m='<div class=\"mgsFeed\"><img class=\"mgsPic\" alt=\"{alt}\" style=\"{style}\" src=\"{picUrl}\" osrc=\"{opicUrl}\"><div class=\"mgsSize\"></div><i class=\"mgsSelect\"></i></div>');for(var p=function(e){var t=new Image;t.src=e.src;var n=t.height,r=t.width,i=t.src,t=t.src,e=e.alt;if(b){var s=t=i;switch(b){case\"duitang\":s=/\\.thumb.200_0\\./,s=t.replace(s,\".\");break;case\"meilishuo\":s=/\\/pic\\/r\\//,s=t.replace(s,\"/pic/_o/\");break;case\"huaban\":s=/_fw192|_fw554/,s=t.replace(s,\"\");break;case\"pinterest\":s=/_b\\.|_c\\./,s=t.replace(s,\".\")}t=s,b==\"huaban\"&&(i=t)}return{w:r,h:n,src:i,osrc:t,alt:e}},w=function(e){var t=\"\";return o&&(t+=\"width:\"+e.w+\"px;height:\"+e.h+\"px;\"),Math.max(e.h,e.w)>199?e.h<e.w&&(t+=\"margin-top: \"+parseInt(100-100*(e.h/e.w))+\"px;\"):t+=\"margin-top: \"+parseInt(100-e.h/2)+\"px;\",t},E=0;E<document.images.length;E++){var S=document.images[E],S=p(S);S.w>80&&S.h>80&&(S.h>109||S.w>109)&&(S=m.replace(/{style}/,w(S)).replace(/{picUrl}/,S.src).replace(/{opicUrl}/,S.osrc).replace(/{width}/,S.w).replace(/{height}/,S.h).replace(/{alt}/,S.alt),g.push(S))}m='<div id=\"maticsoftShareBg\"></div><div id=\"maticsoftShareToolBar\"><a class=\"maticsoftPub\" href=\"javascript:;\"></a><a class=\"maticsoftCancel\" href=\"javascript:;\"></a><span class=\"maticsoftNotice\"><span class=\"maticsoftNoticeText\" >请选择要发表的图片（可多选）</span><b style=\"margin:0 5px;\" ><input id=\"select_all\" type=\"checkbox\" />全选</b></span><div class=\"maticsoftShadow\"></div></div>'+'<div id=\"maticsoftShareContent\">{content}</div>'.replace(/{content}/,g.join(\"\")),g=document.createElement(\"div\"),g.innerHTML=m,document.body.appendChild(g),s?(f=document.createElement(\"style\"),f.type=\"text/css\",f.media=\"screen\",f.id=\"maticsoftShareStyle\",f.styleSheet.cssText=o?v:d,document.getElementsByTagName(\"head\")[0].appendChild(f)):(f=document.createElement(\"style\"),f.id=\"maticsoftShareStyle\",f.innerHTML=d,document.body.appendChild(f)),window.scrollTo(0,0),s=a(\"maticsoftCancel\",u(\"maticsoftShareToolBar\")[0],\"a\"),t(s[0],\"click\",function(){l()});for(var x=a(\"mgsFeed\",u(\"maticsoftShareContent\")[0]),T=x.length,E=0;E<T;E++)t(x[E],\"click\",function(){if(n(this,\"checked\")){var e=a(\"mgsSelect\",this);i(e[0],\"selected\"),i(this,\"checked\")}else r(this,\"checked\"),e=a(\"mgsSelect\",this),r(e[0],\"selected\");N()});var N=function(){var e=a(\"checked\",u(\"maticsoftShareContent\")[0]).length;a(\"maticsoftNoticeText\",u(\"maticsoftShareToolBar\")[0])[0].innerHTML=e==0?\"请选择要发表的图片（可多选）\":'已选择<em style=\"color:#690;font-weight: bold;padding:0 2px;\">'+e+\"</em>张图片\"};t(u(\"select_all\")[0],\"click\",function(){if(this.checked)for(e=0;e<T;e++)r(x[e],\"checked\"),t=a(\"mgsSelect\",x[e]),r(t[0],\"selected\");else for(var e=0;e<T;e++){var t=a(\"mgsSelect\",x[e]);i(t[0],\"selected\"),i(x[e],\"checked\")}N()});var s=a(\"maticsoftPub\",u(\"maticsoftShareToolBar\")[0]),C=function(e){var t=[];t.push(e),t.push(\"?\");var e=a(\"checked\",u(\"maticsoftShareContent\")[0]),n=e.length;if(n<1)alert(\"请选择至少一张图片。\");else if(n>10)alert(\"一次最多只能分享10张\");else{for(var r=0;r<n;r++){var i=a(\"mgsPic\",e[r]),s=i[0].getAttribute(\"osrc\"),i=i[0].alt;t.push(\"pics[]=\"+encodeURIComponent(s)+\"----\"+i+\"&\")}t.push(\"type=img\"),window.open(t.join(\"\"),\"maticsoftShare\"+(new Date).getTime(),\"status=no,resizable=no,scrollbars=yes,personalbar=no,directories=no,location=no,toolbar=no,menubar=no,left=0,top=0\"),l()}};t(s[0],\"click\",function(){C(e+\"/home/ShareImage/\")}),t(d[0],\"click\",function(){C(e+\"/home/ShareImage/\")})}else{var d=\"#maticsoftShareBg {background-color:#f2f2f2; height:100%; width:100%; left:0px; top:0px; zoom:1; position:fixed; z-index:100000; opacity:0.8; FILTER:alpha(opacity=80); } #maticsoftShareBtn{position:absolute;top:50%;left:50%;width:480px;height:160px;margin:-80px 0 0 -240px;z-index: 100001;background:url(\"+e+\"/images/publish.gif) no-repeat;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:80px;top:79px;display:block;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:255px;top:79px;display:block;} #maticsoftShareBtn .maticsoftCancel{height:38px;width:80px;position:absolute;top:0;right:0;}\",v='body{background-attachment:fixed; background-image:url(\"about:blank\");}#maticsoftShareBg {background-color:#f2f2f2; height:expression(document.body.clientHeight); width:100%; left:0px; zoom:1; z-index:100000; FILTER:alpha(opacity=80); position:absolute; top:expression(document.compatMode && document.compatMode==\"CSS1Compat\" ? documentElement.scrollTop:document.body.scrollTop ); }  #maticsoftShareBtn{position:absolute;top:50%;left:50%;width:480px;height:160px;margin:-80px 0 0 -240px;z-index: 100001; background:url('+e+\"/images/publish.gif) no-repeat;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:80px;top:79px;display:block;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:255px;top:79px;display:block;} #maticsoftShareBtn .maticsoftCancel{height:38px;width:80px;position:absolute;top:0;right:0;}\",m='<div id=\"maticsoftShareBg\"></div><div id=\"maticsoftShareBtn\"><a class=\"maticsoftPub\" href=\"javascript:;\"></a><a class=\"maticsoftPub\" href=\"javascript:;\"></a><a class=\"maticsoftCancel\" href=\"javascript:;\"></a></div>',g=document.createElement(\"div\");g.innerHTML=m,document.body.appendChild(g),s?(f=document.createElement(\"style\"),f.type=\"text/css\",f.media=\"screen\",f.id=\"maticsoftShareStyle\",f.styleSheet.cssText=o?v:d,document.getElementsByTagName(\"head\")[0].appendChild(f)):(f=document.createElement(\"style\"),f.id=\"maticsoftShareStyle\",f.innerHTML=d,document.body.appendChild(f)),window.scrollTo(0,0);var s=a(\"maticsoftPub\",u(\"maticsoftShareToolBar\")[0]),y=function(e){var t=[];return t.push(e),t.push(\"?\"),t.push(\"type=goods&\"),t.push(\"goods=\"+encodeURIComponent(location.href)),t.join(\"\")};s=a(\"maticsoftCancel\",u(\"maticsoftShareToolBar\")[0],\"a\"),t(s[0],\"click\",function(){l()})}}}})();";
            try
            {
                str = str3 + "http://" + Globals.DomainFullName + str4;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                if (!info.Directory.Exists)
                {
                    info.Directory.Create();
                }
                writer = new StreamWriter(HttpContext.Current.Server.MapPath(path), false, encoding);
                writer.Write(str);
                writer.Flush();
                return true;
            }
            catch (Exception exception)
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                    OPTime = DateTime.Now,
                    Loginfo = "请求路径为【" + path + "】的静态化写入失败",
                    Url = "http://" + Globals.DomainFullName + path,
                    StackTrace = exception.StackTrace
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            }
            finally
            {
                writer.Close();
            }
            return false;
        }

        public void HtmlJSRedirect(string Filename, string RedirectUrl)
        {
            string str = "<script language=javascript>this.location = '" + RedirectUrl + "';</script>";
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Filename)))
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                try
                {
                    StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(Filename));
                    str = str + reader.ReadToEnd();
                    reader.Close();
                }
                catch (Exception exception)
                {
                    Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                        OPTime = DateTime.Now,
                        Loginfo = "读取文件【" + Filename + "】失败",
                        Url = "",
                        StackTrace = exception.Message
                    };
                    Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                    throw exception;
                }
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(Filename));
                try
                {
                    FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(Filename));
                    if (!info.Directory.Exists)
                    {
                        info.Directory.Create();
                    }
                    StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath(Filename), false, encoding);
                    writer.Write(str);
                    writer.Flush();
                }
                catch (Exception exception2)
                {
                    Maticsoft.Model.SysManage.ErrorLog log2 = new Maticsoft.Model.SysManage.ErrorLog {
                        OPTime = DateTime.Now,
                        Loginfo = "写入文件【" + Filename + "】失败",
                        Url = "",
                        StackTrace = exception2.Message
                    };
                    Maticsoft.BLL.SysManage.ErrorLog.Add(log2);
                    throw exception2;
                }
            }
        }

        public void HttpToRender(string VirtualRequestUrl)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            StreamReader reader = null;
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath == "/")
            {
                applicationPath = "";
            }
            WebResponse response = WebRequest.Create(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + applicationPath + VirtualRequestUrl).GetResponse();
            reader = new StreamReader(response.GetResponseStream(), encoding);
            try
            {
                reader = new StreamReader(response.GetResponseStream(), encoding);
                reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                reader.Close();
            }
        }

        public static bool HttpToStatic(string VirtualRequestUrl, string SaveVirtualPath)
        {
            bool flag;
            Encoding encoding = Encoding.GetEncoding("utf-8");
            StreamReader reader = null;
            StreamWriter writer = null;
            string str = null;
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath == "/")
            {
                applicationPath = "";
            }
            string requestUriString = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + applicationPath + VirtualRequestUrl;
            if (VirtualRequestUrl.Contains("http://"))
            {
                requestUriString = VirtualRequestUrl;
            }
            try
            {
                reader = new StreamReader(WebRequest.Create(requestUriString).GetResponse().GetResponseStream(), encoding);
                str = reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                    OPTime = DateTime.Now,
                    Loginfo = "请求路径为【" + VirtualRequestUrl + "】的文章静态化读取失败",
                    Url = "http://" + Globals.DomainFullName + VirtualRequestUrl,
                    StackTrace = exception.Message
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                return false;
            }
            finally
            {
                reader.Close();
            }
            try
            {
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(SaveVirtualPath));
                if (!info.Directory.Exists)
                {
                    info.Directory.Create();
                }
                writer = new StreamWriter(HttpContext.Current.Server.MapPath(SaveVirtualPath), false, encoding);
                writer.Write(str);
                writer.Flush();
                flag = true;
            }
            catch (Exception exception2)
            {
                Maticsoft.Model.SysManage.ErrorLog log2 = new Maticsoft.Model.SysManage.ErrorLog {
                    OPTime = DateTime.Now,
                    Loginfo = "请求路径为【" + VirtualRequestUrl + "】的文章静态化写入失败",
                    Url = "http://" + Globals.DomainFullName + VirtualRequestUrl,
                    StackTrace = exception2.Message
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(log2);
                flag = false;
            }
            finally
            {
                writer.Close();
            }
            return flag;
        }
    }
}

