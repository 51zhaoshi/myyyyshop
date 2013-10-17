namespace Maticsoft.Model.Shop.Products
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class AttributeInfo
    {
        private long _attributeid;
        private string _attributename;
        private int _displaysequence;
        private int _typeid;
        private int _usagemode;
        private bool _useattributeimage;
        private bool _userDefinedPic;
        private List<string> _valueStr = new List<string>();
        private List<AttributeValue> listAttributeValues = new List<AttributeValue>();

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

        public List<AttributeValue> AttributeValues
        {
            get
            {
                return this.listAttributeValues;
            }
            set
            {
                this.listAttributeValues = value;
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

        public bool UserDefinedPic
        {
            get
            {
                return this._userDefinedPic;
            }
            set
            {
                this._userDefinedPic = value;
            }
        }

        public List<string> ValueStr
        {
            get
            {
                return this._valueStr;
            }
            set
            {
                this._valueStr = value;
            }
        }
    }
}

