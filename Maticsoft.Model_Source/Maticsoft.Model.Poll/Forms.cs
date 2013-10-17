namespace Maticsoft.Model.Poll
{
    using System;

    [Serializable]
    public class Forms
    {
        private string _description;
        private int _formid;
        private bool _isActive;
        private string _name;

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public int FormID
        {
            get
            {
                return this._formid;
            }
            set
            {
                this._formid = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                this._isActive = value;
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
    }
}

