namespace Maticsoft.Controls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class WebControl : System.Web.UI.WebControls.WebControl, IWebControl
    {
        private int _hintHeight = 50;
        private string _hintInfo = "";
        private int _hintLeftOffSet;
        private string _hintShowType = "up";
        private string _hintTitle = "";
        private int _hintTopOffSet;

        [DefaultValue(50), Bindable(true), Category("Appearance")]
        public int HintHeight
        {
            get
            {
                return this._hintHeight;
            }
            set
            {
                this._hintHeight = value;
            }
        }

        [Bindable(true), DefaultValue(""), Category("Appearance")]
        public string HintInfo
        {
            get
            {
                return this._hintInfo;
            }
            set
            {
                this._hintInfo = value;
            }
        }

        [DefaultValue(0), Bindable(true), Category("Appearance")]
        public int HintLeftOffSet
        {
            get
            {
                return this._hintLeftOffSet;
            }
            set
            {
                this._hintLeftOffSet = value;
            }
        }

        [DefaultValue("up"), Bindable(true), Category("Appearance")]
        public string HintShowType
        {
            get
            {
                return this._hintShowType;
            }
            set
            {
                this._hintShowType = value;
            }
        }

        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public string HintTitle
        {
            get
            {
                return this._hintTitle;
            }
            set
            {
                this._hintTitle = value;
            }
        }

        [DefaultValue(0), Bindable(true), Category("Appearance")]
        public int HintTopOffSet
        {
            get
            {
                return this._hintTopOffSet;
            }
            set
            {
                this._hintTopOffSet = value;
            }
        }
    }
}

