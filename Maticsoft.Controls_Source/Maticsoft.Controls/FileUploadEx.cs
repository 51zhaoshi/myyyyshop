namespace Maticsoft.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:UpFile runat=server></{0}:UpFile>"), ToolboxItem(true), DefaultProperty("UpFilePath")]
    public class FileUploadEx : Maticsoft.Controls.WebControl
    {
        protected Button btnUpload = new Button();
        protected HtmlInputFile fileUpload = new HtmlInputFile();
        protected Label lblMsg = new Label();
        private string m_waterMarkText;
        protected TextBox txt = new TextBox();

        public FileUploadEx()
        {
            this.fileUpload.Size = 0x20;
            this.Controls.Add(this.fileUpload);
            this.btnUpload.Text = "上传";
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.lblMsg);
            this.fileUpload.Attributes.Add("onfocus", "this.className='colorfocus';");
            this.fileUpload.Attributes.Add("onblur", "this.className='colorblur';");
            this.fileUpload.Attributes.Add("Class", "colorblur");
            this.txt.TextMode = TextBoxMode.MultiLine;
            this.txt.Width = 0x11d;
            this.txt.Attributes.Add("onfocus", "this.className='colorfocus';");
            this.txt.Attributes.Add("onblur", "this.className='colorblur';");
            this.txt.Attributes.Add("rows", "2");
            this.txt.Attributes.Add("cols", "53");
            this.txt.CssClass = "colorblur";
            this.Controls.Add(this.txt);
            this.Width = 350;
            this.Height = 30;
            this.BorderStyle = BorderStyle.Dotted;
            this.BorderWidth = 0;
            this.btnUpload.Click += new EventHandler(this.UpFile_Click);
        }

        protected override void CreateChildControls()
        {
            this.btnUpload.Click += new EventHandler(this.UpFile_Click);
        }

        protected void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void GetThumbnailImage(int width, int height, int left, int right, string picpath, string picthumpath)
        {
            Bitmap image = new Bitmap(System.Drawing.Image.FromFile(picpath).GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(this.ThumbnailCallback), IntPtr.Zero));
            Graphics graphics = Graphics.FromImage(image);
            if ((this.WaterMarkText == null) || (this.WaterMarkText == ""))
            {
                graphics.DrawString(null, new Font("Courier New", 14f), new SolidBrush(Color.White), (float) left, (float) right);
            }
            else
            {
                graphics.DrawString(this.WaterMarkText, new Font("Courier New", 14f), new SolidBrush(Color.Black), (float) left, (float) right);
            }
            image.Save(picthumpath, ImageFormat.Jpeg);
            image.Dispose();
        }

        protected override void Render(HtmlTextWriter output)
        {
            this.CreateChildControls();
            if (base.HintInfo != "")
            {
                output.WriteBeginTag(string.Concat(new object[] { "span id=\"", this.ClientID, "\"  onmouseover=\"showhintinfo(this,", base.HintLeftOffSet, ",", base.HintTopOffSet, ",'", base.HintTitle, "','", base.HintInfo, "','", base.HintHeight, "','", base.HintShowType, "');\" onmouseout=\"hidehintinfo();\">" }));
            }
            base.Render(output);
            if (base.HintInfo != "")
            {
                output.WriteEndTag("span");
            }
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        public string UpdateFile()
        {
            string path = HttpContext.Current.Server.MapPath("/" + this.UpFilePath + "/");
            this.CreateFolder(path);
            if (this.fileUpload.PostedFile != null)
            {
                HttpPostedFile postedFile = this.fileUpload.PostedFile;
                int contentLength = postedFile.ContentLength;
                if (contentLength == 0)
                {
                    this.lblMsg.Text = "<br /><font color=red>没有选定被上传的文件！</font></b>";
                    return "";
                }
                if (this.FileType.IndexOf(Path.GetExtension(postedFile.FileName).ToLower()) < 0)
                {
                    this.lblMsg.Text = "<br /><font color=red>文件必须是以" + this.FileType.Replace("|", " , ") + "为扩展名的文件！</font></b>";
                    return "";
                }
                byte[] buffer = new byte[contentLength];
                postedFile.InputStream.Read(buffer, 0, contentLength);
                string str2 = Guid.NewGuid().ToString();
                string str3 = str2 + Path.GetExtension(postedFile.FileName).ToLower();
                int num2 = 0;
                while (File.Exists(path + str3))
                {
                    num2++;
                    str3 = Path.GetFileNameWithoutExtension(postedFile.FileName) + num2.ToString() + Path.GetExtension(postedFile.FileName).ToLower();
                }
                if (((Path.GetExtension(postedFile.FileName).ToLower() == ".jpg") || (Path.GetExtension(postedFile.FileName).ToLower() == ".gif")) || (Path.GetExtension(postedFile.FileName).ToLower() == ".png"))
                {
                    try
                    {
                        FileStream stream = new FileStream(path + str3, FileMode.Create);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Close();
                        Bitmap bitmap = new Bitmap(path + str3);
                        num2 = 0;
                        this.Text = this.HttpPath + str3;
                        if (this.IsThumbnailImage)
                        {
                            int width = bitmap.Width / 3;
                            int height = bitmap.Height / 3;
                            this.GetThumbnailImage(width, height, (width / 2) - 60, height - 20, path + str3, path + ("S_" + str2 + Path.GetExtension(postedFile.FileName).ToLower()));
                        }
                        bitmap.Dispose();
                        this.lblMsg.Text = "上传成功！";
                        return this.Text;
                    }
                    catch (ArgumentException exception)
                    {
                        this.lblMsg.Text = exception.ToString();
                        goto Label_02D0;
                    }
                }
                postedFile.SaveAs(path + str3);
                try
                {
                    this.Text = this.HttpPath + str3;
                    return this.Text;
                }
                catch (ArgumentException exception2)
                {
                    this.lblMsg.Text = exception2.ToString();
                    File.Delete(path + str3);
                    return "";
                }
            }
        Label_02D0:
            return "";
        }

        private void UpFile_Click(object sender, EventArgs e)
        {
            this.UpdateFile();
        }

        [Category("Appearance"), Bindable(true), DefaultValue("jpg,gif,png")]
        public string FileType
        {
            get
            {
                object obj2 = this.ViewState["FileType"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["FileType"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string HttpPath
        {
            get
            {
                object obj2 = this.ViewState["HttpPath"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["HttpPath"] = value;
            }
        }

        [DefaultValue("true"), Category("Appearance"), Bindable(true)]
        public bool IsShowTextArea
        {
            get
            {
                object obj2 = this.ViewState["IsShowTextArea"];
                return ((obj2 == null) || (obj2.ToString() == "True"));
            }
            set
            {
                this.ViewState["IsShowTextArea"] = value;
                this.txt.Visible = value;
            }
        }

        [Category("Behavior"), Bindable(true), DefaultValue("true")]
        public bool IsThumbnailImage
        {
            get
            {
                object obj2 = this.ViewState["IsThumbnailImage"];
                return ((obj2 == null) || (obj2.ToString() == "True"));
            }
            set
            {
                this.ViewState["IsThumbnailImage"] = value;
            }
        }

        [Category("Appearance"), DefaultValue(""), Bindable(true)]
        public string Text
        {
            get
            {
                return this.txt.Text;
            }
            set
            {
                this.txt.Text = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string UpFilePath
        {
            get
            {
                object obj2 = this.ViewState["RequiredFieldType"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["RequiredFieldType"] = value;
            }
        }

        [Bindable(true), DefaultValue((string) null), Category("Appearance")]
        public string WaterMarkText
        {
            get
            {
                return this.m_waterMarkText;
            }
            set
            {
                this.m_waterMarkText = value;
            }
        }
    }
}

