namespace Maticsoft.Model.Shop.Products
{
    using System;

    public class AttributeHelper
    {
        private long _attributeid;
        private string _attributename;
        private int _typeid;
        private int _usagemode;
        private bool _useattributeimage;
        private int _valueId;

        public long AttributeId
        {
            get
            {
                return this._attributeid;
            }
            set
            {
                this._attributeid = value;
            }
        }

        public string AttributeName
        {
            get
            {
                return this._attributename;
            }
            set
            {
                this._attributename = value;
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

        public int UsageMode
        {
            get
            {
                return this._usagemode;
            }
            set
            {
                this._usagemode = value;
            }
        }

        public bool UseAttributeImage
        {
            get
            {
                return this._useattributeimage;
            }
            set
            {
                this._useattributeimage = value;
            }
        }

        public int ValueId
        {
            get
            {
                return this._valueId;
            }
            set
            {
                this._valueId = value;
            }
        }
    }
}

