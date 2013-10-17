namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Meal : TopObject
    {
        [XmlElement("item_list")]
        public string ItemList { get; set; }

        [XmlElement("meal_id")]
        public long MealId { get; set; }

        [XmlElement("meal_memo")]
        public string MealMemo { get; set; }

        [XmlElement("meal_name")]
        public string MealName { get; set; }

        [XmlElement("meal_price")]
        public string MealPrice { get; set; }

        [XmlElement("postage_id")]
        public long PostageId { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}

