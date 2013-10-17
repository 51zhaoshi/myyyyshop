namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class RegionAreas
    {
        private int _areaid;
        private string _name;

        public int AreaId
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

