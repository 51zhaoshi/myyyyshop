namespace Maticsoft.Web.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SortTip
    {
        private string _ascImg = "~/Images/up.JPG";
        private string _descImg = "~/Images/down.JPG";

        [NotifyParentProperty(true), DefaultValue(""), Category("扩展"), Description("升序提示图片"), Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
        public string AscImg
        {
            get
            {
                return this._ascImg;
            }
            set
            {
                this._ascImg = value;
            }
        }

        [NotifyParentProperty(true), Description("降序提示图片"), Category("扩展"), Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor)), DefaultValue("")]
        public string DescImg
        {
            get
            {
                return this._descImg;
            }
            set
            {
                this._descImg = value;
            }
        }

        public bool IsNotSet
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.AscImg))
                {
                    return string.IsNullOrWhiteSpace(this.DescImg);
                }
                return true;
            }
        }
    }
}

