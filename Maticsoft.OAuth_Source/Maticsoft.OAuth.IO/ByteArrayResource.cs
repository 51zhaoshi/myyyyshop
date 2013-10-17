namespace Maticsoft.OAuth.IO
{
    using System;
    using System.Globalization;
    using System.IO;

    public class ByteArrayResource : IResource
    {
        private byte[] bytes;

        public ByteArrayResource(byte[] bytes)
        {
            this.bytes = bytes ?? new byte[0];
        }

        public virtual Stream GetStream()
        {
            return new MemoryStream(this.bytes);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Byte array resource [Length:{0}]", new object[] { this.bytes.Length });
        }

        public byte[] Bytes
        {
            get
            {
                return this.bytes;
            }
        }

        public virtual bool IsOpen
        {
            get
            {
                return false;
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

