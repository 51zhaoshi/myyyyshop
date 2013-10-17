namespace Maticsoft.Model.Settings
{
    using System;

    [Serializable]
    public class FriendlyLink
    {
        private string _contactperson;
        private string _email;
        private int _id;
        private int? _imgheight;
        private string _imgurl;
        private int? _imgwidth;
        private bool _isdisplay = true;
        private string _linkdesc;
        private string _linkurl;
        private string _name;
        private int _orderid;
        private int _state;
        private string _telphone;
        private int _typeid;

        public string ContactPerson
        {
            get
            {
                return this._contactperson;
            }
            set
            {
                this._contactperson = value;
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
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

        public int? ImgHeight
        {
            get
            {
                return this._imgheight;
            }
            set
            {
                this._imgheight = value;
            }
        }

        public string ImgUrl
        {
            get
            {
                return this._imgurl;
            }
            set
            {
                this._imgurl = value;
            }
        }

        public int? ImgWidth
        {
            get
            {
                return this._imgwidth;
            }
            set
            {
                this._imgwidth = value;
            }
        }

        public bool IsDisplay
        {
            get
            {
                return this._isdisplay;
            }
            set
            {
                this._isdisplay = value;
            }
        }

        public string LinkDesc
        {
            get
            {
                return this._linkdesc;
            }
            set
            {
                this._linkdesc = value;
            }
        }

        public string LinkUrl
        {
            get
            {
                return this._linkurl;
            }
            set
            {
                this._linkurl = value;
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

        public int OrderID
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }

        public int State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
            }
        }

        public string TelPhone
        {
            get
            {
                return this._telphone;
            }
            set
            {
                this._telphone = value;
            }
        }

        public int TypeID
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }
    }
}

