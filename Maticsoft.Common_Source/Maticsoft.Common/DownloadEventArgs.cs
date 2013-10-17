namespace Maticsoft.Common
{
    using System;

    public class DownloadEventArgs : EventArgs
    {
        private int bytesReceived;
        private byte[] receivedData;
        private int totalBytes;

        public int BytesReceived
        {
            get
            {
                return this.bytesReceived;
            }
            set
            {
                this.bytesReceived = value;
            }
        }

        public byte[] ReceivedData
        {
            get
            {
                return this.receivedData;
            }
            set
            {
                this.receivedData = value;
            }
        }

        public int TotalBytes
        {
            get
            {
                return this.totalBytes;
            }
            set
            {
                this.totalBytes = value;
            }
        }
    }
}

