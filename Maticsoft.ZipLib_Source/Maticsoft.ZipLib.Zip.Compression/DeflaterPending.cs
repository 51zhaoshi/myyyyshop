namespace Maticsoft.ZipLib.Zip.Compression
{
    using System;

    public class DeflaterPending : PendingBuffer
    {
        public DeflaterPending() : base(0x10000)
        {
        }
    }
}

