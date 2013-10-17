namespace Maticsoft.Model.SysManage
{
    using System;

    [Serializable]
    public class SysNode
    {
        private string _comment;
        private bool _enabled = true;
        private string _imageurl;
        private int? _keshidm;
        private string _keshipublic;
        private string _location;
        private int? _moduleid;
        private int _nodeid;
        private int? _orderid;
        private int _parentid;
        private string _parentpath;
        private int _permissionid;
        private string _treetext;
        private int _treetype;
        private string _url;

        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
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

        public int? KeShiDM
        {
            get
            {
                return this._keshidm;
            }
            set
            {
                this._keshidm = value;
            }
        }

        public string KeshiPublic
        {
            get
            {
                return this._keshipublic;
            }
            set
            {
                this._keshipublic = value;
            }
        }

        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }

        public int? ModuleID
        {
            get
            {
                return this._moduleid;
            }
            set
            {
                this._moduleid = value;
            }
        }

        public int NodeID
        {
            get
            {
                return this._nodeid;
            }
            set
            {
                this._nodeid = value;
            }
        }

        public int? OrderID
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

        public int ParentID
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public string ParentPath
        {
            get
            {
                return this._parentpath;
            }
            set
            {
                this._parentpath = value;
            }
        }

        public int PermissionID
        {
            get
            {
                return this._permissionid;
            }
            set
            {
                this._permissionid = value;
            }
        }

        public string TreeText
        {
            get
            {
                return this._treetext;
            }
            set
            {
                this._treetext = value;
            }
        }

        public int TreeType
        {
            get
            {
                return this._treetype;
            }
            set
            {
                this._treetype = value;
            }
        }

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }
    }
}

