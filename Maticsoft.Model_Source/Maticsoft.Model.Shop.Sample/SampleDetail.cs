namespace Maticsoft.Model.Shop.Sample
{
    using System;

    [Serializable]
    public class SampleDetail
    {
        private DateTime? _createddate;
        private int _id;
        private string _imageurl;
        private string _normalimageurl;
        private string _pdfurl;
        private string _remark;
        private int _sampleid;
        private int? _status;
        private string _thumbimageurl;
        private string _title;
        private int _type;

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

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return this._imageurl;
            }
            set
            {
                this._imageurl = value;
            }
        }

        public string NormalImageUrl
        {
            get
            {
                return this._normalimageurl;
            }
            set
            {
                this._normalimageurl = value;
            }
        }

        public string PdfUrl
        {
            get
            {
                return this._pdfurl;
            }
            set
            {
                this._pdfurl = value;
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

        public int SampleId
        {
            get
            {
                return this._sampleid;
            }
            set
            {
                this._sampleid = value;
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

        public string ThumbImageUrl
        {
            get
            {
                return this._thumbimageurl;
            }
            set
            {
                this._thumbimageurl = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

