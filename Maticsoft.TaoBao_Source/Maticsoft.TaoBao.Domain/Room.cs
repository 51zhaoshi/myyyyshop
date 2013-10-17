namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Room : TopObject
    {
        [XmlElement("area")]
        public string Area { get; set; }

        [XmlElement("bbn")]
        public string Bbn { get; set; }

        [XmlElement("bed_type")]
        public string BedType { get; set; }

        [XmlElement("breakfast")]
        public string Breakfast { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("deposit")]
        public long Deposit { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("fee")]
        public long Fee { get; set; }

        [XmlElement("gid")]
        public long Gid { get; set; }

        [XmlElement("guide")]
        public string Guide { get; set; }

        [XmlElement("hid")]
        public long Hid { get; set; }

        [XmlElement("hotel")]
        public Maticsoft.TaoBao.Domain.Hotel Hotel { get; set; }

        [XmlElement("iid")]
        public long Iid { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("multi_room_quotas")]
        public string MultiRoomQuotas { get; set; }

        [XmlElement("payment_type")]
        public string PaymentType { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price_type")]
        public string PriceType { get; set; }

        [XmlElement("rid")]
        public long Rid { get; set; }

        [XmlElement("room_quotas")]
        public string RoomQuotas { get; set; }

        [XmlElement("room_type")]
        public Maticsoft.TaoBao.Domain.RoomType RoomType { get; set; }

        [XmlElement("service")]
        public string Service { get; set; }

        [XmlElement("size")]
        public string Size { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }

        [XmlElement("storey")]
        public string Storey { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

