namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class FeedbackType
    {
        private string _description;
        private int _typeid;
        private string _typename;

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

        public int TypeId
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

        public string TypeName
        {
            get
            {
                return this._typename;
            }
            set
            {
                this._typename = value;
            }
        }
    }
}

