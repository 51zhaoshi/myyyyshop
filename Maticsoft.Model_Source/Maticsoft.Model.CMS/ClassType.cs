namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class ClassType
    {
        private int _classtypeid;
        private string _classtypename;

        public int ClassTypeID
        {
            get
            {
                return this._classtypeid;
            }
            set
            {
                this._classtypeid = value;
            }
        }

        public string ClassTypeName
        {
            get
            {
                return this._classtypename;
            }
            set
            {
                this._classtypename = value;
            }
        }
    }
}

