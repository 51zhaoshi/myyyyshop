namespace Maticsoft.Model.Settings
{
    using System;

    [Serializable]
    public class FilterWords
    {
        private int _actiontype;
        private int _filterid;
        private string _repalceword;
        private string _wordpattern;

        public int ActionType
        {
            get
            {
                return this._actiontype;
            }
            set
            {
                this._actiontype = value;
            }
        }

        public int FilterId
        {
            get
            {
                return this._filterid;
            }
            set
            {
                this._filterid = value;
            }
        }

        public string RepalceWord
        {
            get
            {
                return this._repalceword;
            }
            set
            {
                this._repalceword = value;
            }
        }

        public string WordPattern
        {
            get
            {
                return this._wordpattern;
            }
            set
            {
                this._wordpattern = value;
            }
        }
    }
}

