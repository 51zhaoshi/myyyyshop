namespace Maticsoft.Web
{
    using System;
    using System.Text;
    using System.Web;

    public class FusionCharts
    {
        private static int boolToNum(bool value)
        {
            if (!value)
            {
                return 0;
            }
            return 1;
        }

        public static string EncodeDataURL(string dataURL, bool noCacheStr)
        {
            string str = dataURL;
            if (noCacheStr)
            {
                str = (str + ((dataURL.IndexOf("?") != -1) ? "&" : "?")) + "FCCurrTime=" + DateTime.Now.ToString().Replace(":", "_");
            }
            return HttpUtility.UrlEncode(str);
        }

        public static string RenderChart(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS)
        {
            return RenderChart(chartSWF, strURL, strXML, chartId, chartWidth, chartHeight, debugMode, registerWithJS, false, "", "noScale", "EN");
        }

        public static string RenderChart(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool transparent)
        {
            return RenderChartALL(chartSWF, strURL, strXML, chartId, chartWidth, chartHeight, debugMode, registerWithJS, transparent, "", "noScale", "EN");
        }

        public static string RenderChart(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool transparent, string bgColor, string scaleMode, string language)
        {
            return RenderChartALL(chartSWF, strURL, strXML, chartId, chartWidth, chartHeight, debugMode, registerWithJS, transparent, bgColor, scaleMode, language);
        }

        private static string RenderChartALL(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool transparent, string bgColor, string scaleMode, string language)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<!-- START Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<div id='{0}Div' >" + Environment.NewLine, chartId);
            builder.Append("Chart." + Environment.NewLine);
            builder.Append("</div>" + Environment.NewLine);
            builder.Append("<script type=\"text/javascript\">" + Environment.NewLine);
            builder.AppendFormat("var chart_{0} = new FusionCharts(\"{1}\", \"{0}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\" );" + Environment.NewLine, new object[] { chartId, chartSWF, chartWidth, chartHeight, boolToNum(debugMode), boolToNum(registerWithJS), bgColor, scaleMode, language });
            if (strXML.Length == 0)
            {
                builder.AppendFormat("chart_{0}.setDataURL(\"{1}\");" + Environment.NewLine, chartId, strURL);
            }
            else
            {
                builder.AppendFormat("chart_{0}.setDataXML(\"{1}\");" + Environment.NewLine, chartId, strXML);
            }
            if (transparent)
            {
                builder.AppendFormat("chart_{0}.setTransparent({1});" + Environment.NewLine, chartId, "true");
            }
            builder.AppendFormat("chart_{0}.render(\"{1}Div\");" + Environment.NewLine, chartId, chartId);
            builder.Append("</script>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            return builder.ToString();
        }

        public static string RenderChartHTML(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode)
        {
            return RenderChartHTMLALL(chartSWF, strURL, strXML, chartId, chartWidth, chartHeight, debugMode, false, false, "", "noScale", "EN");
        }

        public static string RenderChartHTML(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS)
        {
            return RenderChartHTMLALL(chartSWF, strURL, strXML, chartId, chartWidth, chartHeight, debugMode, registerWithJS, false, "", "noScale", "EN");
        }

        public static string RenderChartHTML(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool transparent)
        {
            return RenderChartHTMLALL(chartSWF, strURL, strXML, chartId, chartWidth, chartHeight, debugMode, registerWithJS, transparent, "", "noScale", "EN");
        }

        public static string RenderChartHTML(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool transparent, string bgColor, string scaleMode, string language)
        {
            return RenderChartHTMLALL(chartSWF, strURL, strXML, chartId, chartWidth, chartHeight, debugMode, registerWithJS, transparent, bgColor, scaleMode, language);
        }

        private static string RenderChartHTMLALL(string chartSWF, string strURL, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool transparent, string bgColor, string scaleMode, string language)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            string str = "";
            string str2 = "";
            if (strXML.Length == 0)
            {
                builder2.AppendFormat("&chartWidth={0}&chartHeight={1}&debugMode={2}&registerWithJS={3}&DOMId={4}&dataURL={5}", new object[] { chartWidth, chartHeight, boolToNum(debugMode), boolToNum(registerWithJS), chartId, strURL });
            }
            else
            {
                builder2.AppendFormat("&chartWidth={0}&chartHeight={1}&debugMode={2}&registerWithJS={3}&DOMId={4}&dataXML={5}", new object[] { chartWidth, chartHeight, boolToNum(debugMode), boolToNum(registerWithJS), chartId, strXML });
            }
            builder2.AppendFormat("&scaleMode={0}&lang={1}", scaleMode, language);
            string str3 = HttpContext.Current.Request.ServerVariables["HTTPS"];
            bool flag = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].Contains("MSIE");
            string str4 = "http";
            if (str3.ToLower() == "on".ToLower())
            {
                str4 = "https";
            }
            if (transparent)
            {
                str2 = "wmode=\"transparent\"";
            }
            builder.AppendFormat("<!-- START Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            if (flag)
            {
                builder.AppendFormat("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"{3}://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\" width=\"{0}\" height=\"{1}\" name=\"{2}\" id=\"{2}\" >" + Environment.NewLine, new object[] { chartWidth, chartHeight, chartId, str4 });
                builder.Append("<param name=\"allowScriptAccess\" value=\"always\" />" + Environment.NewLine);
                builder.AppendFormat("<param name=\"movie\" value=\"{0}\"/>" + Environment.NewLine, chartSWF);
                builder.AppendFormat("<param name=\"FlashVars\" value=\"{0}\" />" + Environment.NewLine, builder2.ToString());
                builder.Append("<param name=\"quality\" value=\"high\" />" + Environment.NewLine);
                if (bgColor != "")
                {
                    builder.AppendFormat("<param name=\"bgcolor\" value=\"{0}\" />" + Environment.NewLine, bgColor);
                }
                if (transparent)
                {
                    builder.Append("<param name=\"wmode\" value=\"transparent\" />" + Environment.NewLine);
                }
                builder.Append("</object>" + Environment.NewLine);
            }
            else
            {
                if (bgColor != "")
                {
                    str = "bgcolor=\"" + bgColor + "\"";
                }
                if (transparent)
                {
                    str2 = "wmode=\"transparent\"";
                }
                builder.AppendFormat("<embed src=\"{0}\" FlashVars=\"{1}\" quality=\"high\" width=\"{2}\" height=\"{3}\" name=\"{4}\" id=\"{4}\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"{7}://www.macromedia.com/go/getflashplayer\" {5} {6} />" + Environment.NewLine, new object[] { chartSWF, builder2.ToString(), chartWidth, chartHeight, chartId, str2, str, str4 });
            }
            builder.AppendFormat("<!-- END Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            StringBuilder builder3 = new StringBuilder();
            builder3.AppendFormat("<div id='{0}Div' >", chartId);
            builder3.AppendFormat("{0}</div>", builder.ToString());
            return builder3.ToString();
        }
    }
}

