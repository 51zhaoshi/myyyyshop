namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class Regions
    {
        private int? _areaid;
        private int _depth;
        private int _displaysequence;
        private int? _parentid;
        private string _path;
        private int _regionid;
        private string _regionname;
        private string _spell;
        private string _spellshort;

        public int? AreaId
        {
            get
            {
                return this._areaid;
            }
            set
            {
                this._areaid = value;
            }
        }

        public int Depth
        {
            get
            {
                return this._depth;
            }
            set
            {
                this._depth = value;
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

        public int? ParentId
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

        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
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

        public string Spell
        {
            get
            {
                return this._spell;
            }
            set
            {
                this._spell = value;
            }
        }

        public string SpellShort
        {
            get
            {
                return this._spellshort;
            }
            set
            {
                this._spellshort = value;
            }
        }
    }
}

