namespace Maticsoft.Model.Settings
{
    using System;

    [Serializable]
    public class MainMenus
    {
        private bool _isused;
        private int _menuid;
        private string _menuname;
        private string _menutitle;
        private int? _menutype;
        private int _navarea;
        private string _navtheme;
        private string _navurl;
        private int _sequence;
        private int? _target;
        private int _urltype;
        private int _visible = 1;

        public bool IsUsed
        {
            get
            {
                return this._isused;
            }
            set
            {
                this._isused = value;
            }
        }

        public int MenuID
        {
            get
            {
                return this._menuid;
            }
            set
            {
                this._menuid = value;
            }
        }

        public string MenuName
        {
            get
            {
                return this._menuname;
            }
            set
            {
                this._menuname = value;
            }
        }

        public string MenuTitle
        {
            get
            {
                return this._menutitle;
            }
            set
            {
                this._menutitle = value;
            }
        }

        public int? MenuType
        {
            get
            {
                return this._menutype;
            }
            set
            {
                this._menutype = value;
            }
        }

        public int NavArea
        {
            get
            {
                return this._navarea;
            }
            set
            {
                this._navarea = value;
            }
        }

        public string NavTheme
        {
            get
            {
                return this._navtheme;
            }
            set
            {
                this._navtheme = value;
            }
        }

        public string NavURL
        {
            get
            {
                return this._navurl;
            }
            set
            {
                this._navurl = value;
            }
        }

        public int Sequence
        {
            get
            {
                return this._sequence;
            }
            set
            {
                this._sequence = value;
            }
        }

        public int? Target
        {
            get
            {
                return this._target;
            }
            set
            {
                this._target = value;
            }
        }

        public int URLType
        {
            get
            {
                return this._urltype;
            }
            set
            {
                this._urltype = value;
            }
        }

        public int Visible
        {
            get
            {
                return this._visible;
            }
            set
            {
                this._visible = value;
            }
        }
    }
}

