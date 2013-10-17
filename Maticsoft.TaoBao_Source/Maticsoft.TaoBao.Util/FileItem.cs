namespace Maticsoft.TaoBao.Util
{
    using System;
    using System.IO;

    public class FileItem
    {
        private byte[] content;
        private FileInfo fileInfo;
        private string fileName;
        private string mimeType;

        public FileItem(FileInfo fileInfo)
        {
            if ((fileInfo == null) || !fileInfo.Exists)
            {
                throw new ArgumentException("fileInfo is null or not exists!");
            }
            this.fileInfo = fileInfo;
        }

        public FileItem(string filePath) : this(new FileInfo(filePath))
        {
        }

        public FileItem(string fileName, byte[] content)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            if ((content == null) || (content.Length == 0))
            {
                throw new ArgumentNullException("content");
            }
            this.fileName = fileName;
            this.content = content;
        }

        public FileItem(string fileName, byte[] content, string mimeType) : this(fileName, content)
        {
            if (string.IsNullOrEmpty(mimeType))
            {
                throw new ArgumentNullException("mimeType");
            }
            this.mimeType = mimeType;
        }

        public byte[] GetContent()
        {
            if (((this.content == null) && (this.fileInfo != null)) && this.fileInfo.Exists)
            {
                using (Stream stream = this.fileInfo.OpenRead())
                {
                    this.content = new byte[stream.Length];
                    stream.Read(this.content, 0, this.content.Length);
                }
            }
            return this.content;
        }

        public string GetFileName()
        {
            if (((this.fileName == null) && (this.fileInfo != null)) && this.fileInfo.Exists)
            {
                this.fileName = this.fileInfo.FullName;
            }
            return this.fileName;
        }

        public string GetMimeType()
        {
            if (this.mimeType == null)
            {
                this.mimeType = TopUtils.GetMimeType(this.GetContent());
            }
            return this.mimeType;
        }
    }
}

