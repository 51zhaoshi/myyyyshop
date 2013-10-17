namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PromotionMealGetResponse : TopResponse
    {
        [XmlArrayItem("meal"), XmlArray("meal_list")]
        public List<Meal> MealList { get; set; }
    }
}

