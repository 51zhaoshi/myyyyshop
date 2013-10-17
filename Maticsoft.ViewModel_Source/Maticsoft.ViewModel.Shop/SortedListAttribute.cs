namespace Maticsoft.ViewModel.Shop
{
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class SortedListAttribute : SortedList<AttributeInfo, SortedSet<SKUItem>>
    {
        public SortedListAttribute() : base(ComparerFactroy<AttributeInfo>.Create<int>(x => x.DisplaySequence))
        {
        }

        public void Add(AttributeInfo key, SKUItem value)
        {
            if (this.ContainsKey(key.AttributeId))
            {
                this[key.AttributeId].Add(value);
            }
            else
            {
                SortedSet<SKUItem> set = new SortedSet<SKUItem>(ComparerFactroy<SKUItem>.Create<int, int>(x => x.AB_DisplaySequence, x => x.AV_DisplaySequence)) {
                    value
                };
                base.Add(key, set);
            }
        }

        public bool ContainsKey(long attributeId)
        {
            return base.Keys.Any<AttributeInfo>(attributeInfo => (attributeInfo.AttributeId == attributeId));
        }

        public SortedSet<SKUItem> this[long attributeId]
        {
            get
            {
                foreach (KeyValuePair<AttributeInfo, SortedSet<SKUItem>> pair in this)
                {
                    if (pair.Key.AttributeId == attributeId)
                    {
                        if (pair.Value == null)
                        {
                            return new SortedSet<SKUItem>();
                        }
                        return pair.Value;
                    }
                }
                return null;
            }
            set
            {
                AttributeInfo key = null;
                foreach (KeyValuePair<AttributeInfo, SortedSet<SKUItem>> pair in this)
                {
                    if (pair.Key.AttributeId == attributeId)
                    {
                        key = pair.Key;
                        break;
                    }
                }
                if (key != null)
                {
                    base[key] = value;
                }
            }
        }
    }
}

