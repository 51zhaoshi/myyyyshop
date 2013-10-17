namespace Maticsoft.OAuth.IO
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Globalization;
    using System.IO;

    public class FileResource : AbstractResource
    {
        private FileInfo fileInfo;
        private System.Uri fileUri;

        public FileResource(string resourceName)
        {
            ArgumentUtils.AssertHasText(resourceName, "resourceName");
            string resourceNameWithoutProtocol = AbstractResource.GetResourceNameWithoutProtocol(resourceName);
            if (((resourceNameWithoutProtocol.Length > 2) && (resourceNameWithoutProtocol[0] == '/')) && (resourceNameWithoutProtocol[2] == ':'))
            {
                resourceNameWithoutProtocol = resourceNameWithoutProtocol.Substring(1);
            }
            this.fileInfo = new FileInfo(resourceNameWithoutProtocol);
            this.fileUri = new System.Uri(this.fileInfo.FullName);
        }

        public override Stream GetStream()
        {
            return new FileStream(this.fileUri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "File resource [{0}]", new object[] { this.fileInfo.FullName });
        }

        public FileInfo File
        {
            get
            {
                return this.fileInfo;
            }
        }

        public override System.Uri Uri
        {
            get
            {
                return this.fileUri;
            }
        }
    }
}

