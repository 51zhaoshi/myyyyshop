namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class AttributeValue
    {
        private long _attributeid;
        private int _displaysequence;
        private string _imageurl;
        private long _valueid;
        private string _valuestr;

        public long AttributeId
        {
            get
            {
                return this._attributeid;
            }
            set
            {
                this._attributeid = value;
            }
        }

        public int DisplaySequence
        {
            get
            {
                return this._displaysequence;
            }
            set
            {
                this._displaysequence = value;
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

        public long ValueId
        {
            get
            {
                return this._valueid;
            }
            set
            {
                this._valueid = value;
            }
        }

        public string ValueStr
        {
            get
            {
                return this._valuestr;
            }
            set
            {
                this._valuestr = value;
            }
        }
    }
}

