namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class PicAd
    {
        private string _alt;
        private string _href;
        private int _id;
        private bool _isshow;
        private string _name;
        private int _orders;
        private string _src;
        private string _title;

        public string Alt
        {
            get
            {
                return this._alt;
            }
            set
            {
                this._alt = value;
            }
        }

        public string Href
        {
            get
            {
                return this._href;
            }
            set
            {
                this._href = value;
            }
        }

        public int Id
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

        public bool IsShow
        {
            get
            {
                return this._isshow;
            }
            set
            {
                this._isshow = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public int Orders
        {
            get
            {
                return this._orders;
            }
            set
            {
                this._orders = value;
            }
        }

        public string Src
        {
            get
            {
                return this._src;
            }
            set
            {
                this._src = value;
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
    }
}

