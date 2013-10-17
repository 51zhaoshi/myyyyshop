namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class RegionRec
    {
        private int _displaysequence;
        private int _id;
        private int _regionid;
        private string _regionname;
        private int _type;

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

        public int RegionId
        {
            get
            {
                return this._regionid;
            }
            set
            {
                this._regionid = value;
            }
        }

        public string RegionName
        {
            get
            {
                return this._regionname;
            }
            set
            {
                this._regionname = value;
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

