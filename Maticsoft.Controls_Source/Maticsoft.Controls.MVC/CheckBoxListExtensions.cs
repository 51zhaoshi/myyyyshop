namespace Maticsoft.Controls.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class CheckBoxListExtensions
    {
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> listInfo)
        {
            return htmlHelper.CheckBoxList(name, listInfo, null, 0);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> listInfo, object htmlAttributes)
        {
            return htmlHelper.CheckBoxList(name, listInfo, new RouteValueDictionary(htmlAttributes), 0);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> listInfo, IDictionary<string, object> htmlAttributes, int number)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("No Tag Name", "name");
            }
            if (listInfo == null)
            {
                return null;
            }
            if (listInfo.Count<SelectListItem>() < 1)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (SelectListItem item in listInfo)
            {
                num++;
                TagBuilder builder2 = new TagBuilder("input");
                if (item.Selected)
                {
                    builder2.MergeAttribute("checked", "checked");
                }
                builder2.MergeAttributes<string, object>(htmlAttributes);
                builder2.MergeAttribute("type", "checkbox");
                builder2.MergeAttribute("value", item.Value);
                builder2.MergeAttribute("name", name);
                builder.Append(builder2.ToString(TagRenderMode.Normal));
                TagBuilder builder3 = new TagBuilder("label");
                builder3.MergeAttribute("for", name);
                builder3.InnerHtml = item.Text;
                builder.Append(builder3.ToString(TagRenderMode.Normal));
                if ((number == 0) || ((num % number) == 0))
                {
                    builder.Append("<br />");
                }
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> listInfo, IDictionary<string, object> htmlAttributes, Position position = 0, int number = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("No Tag Name", "name");
            }
            if (listInfo == null)
            {
                return null;
            }
            if (listInfo.Count<SelectListItem>() < 1)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            int num = 0;
            switch (position)
            {
                case Position.Horizontal:
                    foreach (SelectListItem item in listInfo)
                    {
                        num++;
                        builder.Append(CreateCheckBoxItem(item, name, htmlAttributes));
                        if ((number == 0) || ((num % number) == 0))
                        {
                            builder.Append("<br />");
                        }
                    }
                    break;

                case Position.Vertical:
                {
                    int num2 = listInfo.Count<SelectListItem>();
                    int num3 = Convert.ToInt32(Math.Ceiling((decimal) (Convert.ToDecimal(num2) / Convert.ToDecimal(number))));
                    if ((num2 <= number) || ((num2 - number) == 1))
                    {
                        num3 = num2;
                    }
                    TagBuilder builder2 = new TagBuilder("div");
                    builder2.MergeAttribute("style", "floatleft; light-height25px; padding-right5px;");
                    string str = builder2.ToString(TagRenderMode.StartTag);
                    string str2 = builder2.ToString(TagRenderMode.EndTag) + " <div style=\"clear:both;\"></div>";
                    string str3 = "</div>" + builder2.ToString(TagRenderMode.StartTag);
                    builder.Append(str);
                    foreach (SelectListItem item2 in listInfo)
                    {
                        num++;
                        builder.Append(CreateCheckBoxItem(item2, name, htmlAttributes));
                        if (num.Equals(num3))
                        {
                            builder.Append(str3);
                            num = 0;
                        }
                        else
                        {
                            builder.Append("<br/>");
                        }
                    }
                    builder.Append(str2);
                    break;
                }
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString CheckBoxListVertical(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> listInfo, IDictionary<string, object> htmlAttributes, int columnNumber = 1)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("No Tag Name", "name");
            }
            if (listInfo == null)
            {
                return null;
            }
            if (listInfo.Count<SelectListItem>() < 1)
            {
                return null;
            }
            int num = listInfo.Count<SelectListItem>();
            int num2 = Convert.ToInt32(Math.Ceiling((decimal) (Convert.ToDecimal(num) / Convert.ToDecimal(columnNumber))));
            if ((num <= columnNumber) || ((num - columnNumber) == 1))
            {
                num2 = num;
            }
            TagBuilder builder = new TagBuilder("div");
            builder.MergeAttribute("style", "floatleft; light-height25px; padding-right5px;");
            string str = builder.ToString(TagRenderMode.StartTag);
            string str2 = builder.ToString(TagRenderMode.EndTag) + " <div style=\"clear:both;\"></div>";
            string str3 = "</div>" + builder.ToString(TagRenderMode.StartTag);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(str);
            int num3 = 0;
            foreach (SelectListItem item in listInfo)
            {
                TagBuilder builder3 = new TagBuilder("input");
                if (item.Selected)
                {
                    builder3.MergeAttribute("checked", "checked");
                }
                builder3.MergeAttributes<string, object>(htmlAttributes);
                builder3.MergeAttribute("type", "checkbox");
                builder3.MergeAttribute("value", item.Value);
                builder3.MergeAttribute("name", name);
                builder2.Append(builder3.ToString(TagRenderMode.Normal));
                TagBuilder builder4 = new TagBuilder("label");
                builder4.MergeAttribute("for", name);
                builder4.InnerHtml = item.Text;
                builder2.Append(builder4.ToString(TagRenderMode.Normal));
                num3++;
                if (num3.Equals(num2))
                {
                    builder2.Append(str3);
                    num3 = 0;
                }
                else
                {
                    builder2.Append("<br/>");
                }
            }
            builder2.Append(str2);
            return MvcHtmlString.Create(builder2.ToString());
        }

        internal static string CreateCheckBoxItem(SelectListItem info, string name, IDictionary<string, object> htmlAttributes)
        {
            StringBuilder builder = new StringBuilder();
            TagBuilder builder2 = new TagBuilder("input");
            if (info.Selected)
            {
                builder2.MergeAttribute("checked", "checked");
            }
            builder2.MergeAttributes<string, object>(htmlAttributes);
            builder2.MergeAttribute("type", "checkbox");
            builder2.MergeAttribute("value", info.Value);
            builder2.MergeAttribute("name", name);
            builder.Append(builder2.ToString(TagRenderMode.Normal));
            TagBuilder builder3 = new TagBuilder("label");
            builder3.MergeAttribute("for", name);
            builder3.InnerHtml = info.Text;
            builder.Append(builder3.ToString(TagRenderMode.Normal));
            return builder.ToString();
        }
    }
}

