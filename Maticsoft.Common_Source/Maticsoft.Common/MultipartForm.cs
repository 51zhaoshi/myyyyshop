namespace Maticsoft.Common
{
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Text;

    public class MultipartForm
    {
        private string boundary = string.Format("--{0}--", Guid.NewGuid());
        private Encoding encoding = Encoding.Default;
        private byte[] formData;
        private MemoryStream ms = new MemoryStream();

        public void AddFlie(string name, string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("尝试添加不存在的文件。", filename);
            }
            FileStream stream = null;
            byte[] buffer = new byte[0];
            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                this.AddFlie(name, Path.GetFileName(filename), buffer, buffer.Length);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public void AddFlie(string name, string filename, byte[] fileData, int dataLength)
        {
            if ((dataLength <= 0) || (dataLength > fileData.Length))
            {
                dataLength = fileData.Length;
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("--{0}\r\n", this.boundary);
            builder.AppendFormat("Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n", name, filename);
            builder.AppendFormat("Content-Type: {0}\r\n", this.GetContentType(filename));
            builder.Append("\r\n");
            byte[] bytes = this.encoding.GetBytes(builder.ToString());
            this.ms.Write(bytes, 0, bytes.Length);
            this.ms.Write(fileData, 0, dataLength);
            byte[] buffer = this.encoding.GetBytes("\r\n");
            this.ms.Write(buffer, 0, buffer.Length);
        }

        public void AddString(string name, string value)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("--{0}\r\n", this.boundary);
            builder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n", name);
            builder.Append("\r\n");
            builder.AppendFormat("{0}\r\n", value);
            byte[] bytes = this.encoding.GetBytes(builder.ToString());
            this.ms.Write(bytes, 0, bytes.Length);
        }

        private string GetContentType(string filename)
        {
            RegistryKey key = null;
            string defaultValue = "application/stream";
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(Path.GetExtension(filename));
                defaultValue = key.GetValue("Content Type", defaultValue).ToString();
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
            return defaultValue;
        }

        public string ContentType
        {
            get
            {
                return string.Format("multipart/form-data; boundary={0}", this.boundary);
            }
        }

        public byte[] FormData
        {
            get
            {
                if (this.formData == null)
                {
                    byte[] bytes = this.encoding.GetBytes("--" + this.boundary + "--\r\n");
                    this.ms.Write(bytes, 0, bytes.Length);
                    this.formData = this.ms.ToArray();
                }
                return this.formData;
            }
        }

        public Encoding StringEncoding
        {
            get
            {
                return this.encoding;
            }
            set
            {
                this.encoding = value;
            }
        }
    }
}

