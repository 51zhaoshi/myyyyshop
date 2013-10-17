namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class MsgTip
    {
        private int _count;
        private int _msgtype;

        public int _MsgType
        {
            get
            {
                return this._msgtype;
            }
            set
            {
                this._msgtype = value;
            }
        }

        public int Count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }
    }
}

