namespace Maticsoft.OAuth.IO
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Globalization;
    using System.IO;

    public class StreamResource : IResource
    {
        private Stream stream;

        public StreamResource(Stream stream)
        {
            ArgumentUtils.AssertNotNull(stream, "stream");
            this.stream = stream;
        }

        public virtual Stream GetStream()
        {
            if (this.stream == null)
            {
                throw new InvalidOperationException("Stream has already been read - do not use StreamSource if a stream needs to be read multiple times.");
            }
            Stream stream = this.stream;
            this.stream = null;
            return stream;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Stream resource [{0}]", new object[] { this.stream.ToString() });
        }

        public virtual bool IsOpen
        {
            get
            {
                return true;
            }
        }

        public virtual System.Uri Uri
        {
            get
            {
                return null;
            }
        }
    }
}

