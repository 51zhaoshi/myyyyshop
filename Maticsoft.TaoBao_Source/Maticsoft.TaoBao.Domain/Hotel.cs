namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Hotel : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("alias_name")]
        public string AliasName { get; set; }

        [XmlElement("city")]
        public long City { get; set; }

        [XmlElement("city_str")]
        public string CityStr { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("country_str")]
        public string CountryStr { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("decorate_time")]
        public string DecorateTime { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("district")]
        public long District { get; set; }

        [XmlElement("district_str")]
        public string DistrictStr { get; set; }

        [XmlElement("hid")]
        public long Hid { get; set; }

        [XmlElement("level")]
        public string Level { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("opening_time")]
        public string OpeningTime { get; set; }

        [XmlElement("orientation")]
        public string Orientation { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("province")]
        public long Province { get; set; }

        [XmlElement("province_str")]
        public string ProvinceStr { get; set; }

        [XmlElement("rooms")]
        public long Rooms { get; set; }

        [XmlArray("room_types"), XmlArrayItem("room_type")]
        public List<RoomType> RoomTypes { get; set; }

        [XmlElement("service")]
        public string Service { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }

        [XmlElement("storeys")]
        public long Storeys { get; set; }

        [XmlElement("tel")]
        public string Tel { get; set; }
    }
}

