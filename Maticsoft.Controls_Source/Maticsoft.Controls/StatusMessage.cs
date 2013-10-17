namespace Maticsoft.Controls
{
    using Maticsoft.Common;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;

    public class StatusMessage : LiteralControl
    {
        private bool isWarning;
        private bool success = true;

        public StatusMessage()
        {
            this.Visible = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Visible)
            {
                if (!this.isWarning)
                {
                    if (this.success)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "CommonMessageSuccess");
                    }
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "CommonMessageError");
                    }
                }
                else if (this.success)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "CommonMessageSuccess");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "CommonWarningMessage");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "CommonMessageSuccess");
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "padding-right: 8px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "padding-right: 8px;");
                if (this.success)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.ApplicationPath + "/Images/pics/status-green.gif");
                }
                else if (this.isWarning)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.ApplicationPath + "/Images/pics/status-yellow.gif");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.ApplicationPath + "/Images/pics/status-red.gif");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "absmiddle");
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                if (string.IsNullOrWhiteSpace(this.ResourceName))
                {
                    writer.Write(this.Text);
                }
                else
                {
                    writer.Write(HttpContext.GetGlobalResourceObject(this.ResourceFile, this.ResourceName, CultureInfo.InvariantCulture));
                }
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
        }

        public bool IsWarning
        {
            get
            {
                return this.isWarning;
            }
            set
            {
                this.isWarning = value;
            }
        }

        [Category("Appearance"), Description("Gets or sets the name of the resource file."), DefaultValue((string) null), Bindable(true)]
        public virtual string ResourceFile
        {
            get
            {
                object obj2 = this.ViewState["ResourceFile"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["ResourceFile"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), Description("Gets or sets the name of the resource to dispaly."), DefaultValue("")]
        public virtual string ResourceName
        {
            get
            {
                object obj2 = this.ViewState["ResourceName"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["ResourceName"] = value;
            }
        }

        public bool Success
        {
            get
            {
                return this.success;
            }
            set
            {
                this.success = value;
            }
        }
    }
}

