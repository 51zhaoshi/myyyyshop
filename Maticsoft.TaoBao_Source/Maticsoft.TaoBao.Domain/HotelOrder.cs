namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class HotelOrder : TopObject
    {
        [XmlElement("alipay_trade_no")]
        public string AlipayTradeNo { get; set; }

        [XmlElement("arrive_early")]
        public string ArriveEarly { get; set; }

        [XmlElement("arrive_late")]
        public string ArriveLate { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("checkin_date")]
        public string CheckinDate { get; set; }

        [XmlElement("checkout_date")]
        public string CheckoutDate { get; set; }

        [XmlElement("contact_name")]
        public string ContactName { get; set; }

        [XmlElement("contact_phone")]
        public string ContactPhone { get; set; }

        [XmlElement("contact_phone_bak")]
        public string ContactPhoneBak { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("gid")]
        public long Gid { get; set; }

        [XmlArrayItem("order_guest"), XmlArray("guests")]
        public List<OrderGuest> Guests { get; set; }

        [XmlElement("hid")]
        public long Hid { get; set; }

        [XmlElement("logistics_status")]
        public string LogisticsStatus { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nights")]
        public long Nights { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("out_oid")]
        public string OutOid { get; set; }

        [XmlElement("payment")]
        public long Payment { get; set; }

        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        [XmlElement("refund_status")]
        public string RefundStatus { get; set; }

        [XmlElement("rid")]
        public long Rid { get; set; }

        [XmlElement("room_number")]
        public long RoomNumber { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("total_room_price")]
        public long TotalRoomPrice { get; set; }

        [XmlElement("trade_status")]
        public string TradeStatus { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}

