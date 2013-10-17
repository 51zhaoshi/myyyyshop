namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class PhotoTags
    {
        private DateTime? _createddate;
        private int? _isrecommand;
        private string _remark;
        private int? _status;
        private int _tagid;
        private string _tagname;

        public DateTime? CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public int? IsRecommand
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

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public int? Status
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

