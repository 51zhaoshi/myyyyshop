namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class GroupTags
    {
        private int _isrecommand;
        private int _status;
        private int _tagid;
        private string _tagname;

        public int IsRecommand
        {
            get
            {
                return this._isrecommand;
            }
            set
            {
                this._isrecommand = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public int TagID
        {
            get
            {
                return this._tagid;
            }
            set
            {
                this._tagid = value;
            }
        }

        public string TagName
        {
            get
            {
                return this._tagname;
            }
            set
            {
                this._tagname = value;
            }
        }
    }
}

