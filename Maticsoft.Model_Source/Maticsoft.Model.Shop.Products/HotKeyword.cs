namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class HotKeyword
    {
        private int? _categoryid;
        private int _id;
        private string _keywords;

        public int? CategoryId
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
            }
        }

        public int Id
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

        public string Keywords
        {
            get
            {
                return this._keywords;
            }
            set
            {
                this._keywords = value;
            }
        }
    }
}

