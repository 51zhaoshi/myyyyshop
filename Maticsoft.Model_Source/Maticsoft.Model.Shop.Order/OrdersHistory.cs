namespace Maticsoft.Model.Shop.Order
{
    using System;

    [Serializable]
    public class OrdersHistory
    {
        private decimal? _activityfreeamount;
        private string _activityname;
        private int _activitystatus;
        private decimal _amount;
        private string _buyercellphone;
        private string _buyeremail;
        private int _buyerid;
        private string _buyername;
        private decimal? _couponamount;
        private string _couponcode;
        private string _couponname;
        private decimal? _couponvalue;
        private int? _couponvaluetype;
        private DateTime _createddate;
        private decimal? _discountadjusted;
        private decimal? _discountamount;
        private string _discountname;
        private decimal? _discountvalue;
        private int? _discountvaluetype;
        private string _expresscompanyabb;
        private string _expresscompanyname;
        private decimal? _freight;
        private decimal? _freightactual;
        private decimal? _freightadjusted;
        private string _gatewayorderid;
        private int? _groupbuyid;
        private decimal? _groupbuyprice;
        private int _groupbuystatus;
        private string _ordercode;
        private decimal? _ordercostprice;
        private long _orderid;
        private string _orderip;
        private decimal? _orderoptionprice;
        private decimal? _orderothercost;
        private int _orderpoint;
        private decimal? _orderprofit;
        private int _orderstatus;
        private decimal _ordertotal;
        private int _ordertype;
        private long _parentorderid;
        private string _paycurrencycode;
        private string _paycurrencyname;
        private decimal? _paymentfee;
        private decimal? _paymentfeeadjusted;
        private string _paymentgateway;
        private int _paymentstatus;
        private int _paymenttypeid;
        private string _paymenttypename;
        private int? _realshippingmodeid;
        private string _realshippingmodename;
        private string _referid;
        private string _referurl;
        private int _refundstatus;
        private int? _regionid;
        private string _remark;
        private string _sellercellphone;
        private string _selleremail;
        private int? _sellerid;
        private string _sellername;
        private string _shipaddress;
        private string _shipcellphone;
        private string _shipemail;
        private string _shipname;
        private string _shipordernumber;
        private string _shipperaddress;
        private string _shippercellphone;
        private int? _shipperid;
        private string _shippername;
        private int? _shippingmodeid;
        private string _shippingmodename;
        private int _shippingstatus;
        private string _shipregion;
        private string _shiptelphone;
        private string _shipzipcode;
        private int? _supplierid;
        private string _suppliername;
        private DateTime? _updateddate;
        private int? _weight;

        public decimal? ActivityFreeAmount
        {
            get
            {
                return this._activityfreeamount;
            }
            set
            {
                this._activityfreeamount = value;
            }
        }

        public string ActivityName
        {
            get
            {
                return this._activityname;
            }
            set
            {
                this._activityname = value;
            }
        }

        public int ActivityStatus
        {
            get
            {
                return this._activitystatus;
            }
            set
            {
                this._activitystatus = value;
            }
        }

        public decimal Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public string BuyerCellPhone
        {
            get
            {
                return this._buyercellphone;
            }
            set
            {
                this._buyercellphone = value;
            }
        }

        public string BuyerEmail
        {
            get
            {
                return this._buyeremail;
            }
            set
            {
                this._buyeremail = value;
            }
        }

        public int BuyerID
        {
            get
            {
                return this._buyerid;
            }
            set
            {
                this._buyerid = value;
            }
        }

        public string BuyerName
        {
            get
            {
                return this._buyername;
            }
            set
            {
                this._buyername = value;
            }
        }

        public decimal? CouponAmount
        {
            get
            {
                return this._couponamount;
            }
            set
            {
                this._couponamount = value;
            }
        }

        public string CouponCode
        {
            get
            {
                return this._couponcode;
            }
            set
            {
                this._couponcode = value;
            }
        }

        public string CouponName
        {
            get
            {
                return this._couponname;
            }
            set
            {
                this._couponname = value;
            }
        }

        public decimal? CouponValue
        {
            get
            {
                return this._couponvalue;
            }
            set
            {
                this._couponvalue = value;
            }
        }

        public int? CouponValueType
        {
            get
            {
                return this._couponvaluetype;
            }
            set
            {
                this._couponvaluetype = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public decimal? DiscountAdjusted
        {
            get
            {
                return this._discountadjusted;
            }
            set
            {
                this._discountadjusted = value;
            }
        }

        public decimal? DiscountAmount
        {
            get
            {
                return this._discountamount;
            }
            set
            {
                this._discountamount = value;
            }
        }

        public string DiscountName
        {
            get
            {
                return this._discountname;
            }
            set
            {
                this._discountname = value;
            }
        }

        public decimal? DiscountValue
        {
            get
            {
                return this._discountvalue;
            }
            set
            {
                this._discountvalue = value;
            }
        }

        public int? DiscountValueType
        {
            get
            {
                return this._discountvaluetype;
            }
            set
            {
                this._discountvaluetype = value;
            }
        }

        public string ExpressCompanyAbb
        {
            get
            {
                return this._expresscompanyabb;
            }
            set
            {
                this._expresscompanyabb = value;
            }
        }

        public string ExpressCompanyName
        {
            get
            {
                return this._expresscompanyname;
            }
            set
            {
                this._expresscompanyname = value;
            }
        }

        public decimal? Freight
        {
            get
            {
                return this._freight;
            }
            set
            {
                this._freight = value;
            }
        }

        public decimal? FreightActual
        {
            get
            {
                return this._freightactual;
            }
            set
            {
                this._freightactual = value;
            }
        }

        public decimal? FreightAdjusted
        {
            get
            {
                return this._freightadjusted;
            }
            set
            {
                this._freightadjusted = value;
            }
        }

        public string GatewayOrderId
        {
            get
            {
                return this._gatewayorderid;
            }
            set
            {
                this._gatewayorderid = value;
            }
        }

        public int? GroupBuyId
        {
            get
            {
                return this._groupbuyid;
            }
            set
            {
                this._groupbuyid = value;
            }
        }

        public decimal? GroupBuyPrice
        {
            get
            {
                return this._groupbuyprice;
            }
            set
            {
                this._groupbuyprice = value;
            }
        }

        public int GroupBuyStatus
        {
            get
            {
                return this._groupbuystatus;
            }
            set
            {
                this._groupbuystatus = value;
            }
        }

        public string OrderCode
        {
            get
            {
                return this._ordercode;
            }
            set
            {
                this._ordercode = value;
            }
        }

        public decimal? OrderCostPrice
        {
            get
            {
                return this._ordercostprice;
            }
            set
            {
                this._ordercostprice = value;
            }
        }

        public long OrderId
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }

        public string OrderIP
        {
            get
            {
                return this._orderip;
            }
            set
            {
                this._orderip = value;
            }
        }

        public decimal? OrderOptionPrice
        {
            get
            {
                return this._orderoptionprice;
            }
            set
            {
                this._orderoptionprice = value;
            }
        }

        public decimal? OrderOtherCost
        {
            get
            {
                return this._orderothercost;
            }
            set
            {
                this._orderothercost = value;
            }
        }

        public int OrderPoint
        {
            get
            {
                return this._orderpoint;
            }
            set
            {
                this._orderpoint = value;
            }
        }

        public decimal? OrderProfit
        {
            get
            {
                return this._orderprofit;
            }
            set
            {
                this._orderprofit = value;
            }
        }

        public int OrderStatus
        {
            get
            {
                return this._orderstatus;
            }
            set
            {
                this._orderstatus = value;
            }
        }

        public decimal OrderTotal
        {
            get
            {
                return this._ordertotal;
            }
            set
            {
                this._ordertotal = value;
            }
        }

        public int OrderType
        {
            get
            {
                return this._ordertype;
            }
            set
            {
                this._ordertype = value;
            }
        }

        public long ParentOrderId
        {
            get
            {
                return this._parentorderid;
            }
            set
            {
                this._parentorderid = value;
            }
        }

        public string PayCurrencyCode
        {
            get
            {
                return this._paycurrencycode;
            }
            set
            {
                this._paycurrencycode = value;
            }
        }

        public string PayCurrencyName
        {
            get
            {
                return this._paycurrencyname;
            }
            set
            {
                this._paycurrencyname = value;
            }
        }

        public decimal? PaymentFee
        {
            get
            {
                return this._paymentfee;
            }
            set
            {
                this._paymentfee = value;
            }
        }

        public decimal? PaymentFeeAdjusted
        {
            get
            {
                return this._paymentfeeadjusted;
            }
            set
            {
                this._paymentfeeadjusted = value;
            }
        }

        public string PaymentGateway
        {
            get
            {
                return this._paymentgateway;
            }
            set
            {
                this._paymentgateway = value;
            }
        }

        public int PaymentStatus
        {
            get
            {
                return this._paymentstatus;
            }
            set
            {
                this._paymentstatus = value;
            }
        }

        public int PaymentTypeId
        {
            get
            {
                return this._paymenttypeid;
            }
            set
            {
                this._paymenttypeid = value;
            }
        }

        public string PaymentTypeName
        {
            get
            {
                return this._paymenttypename;
            }
            set
            {
                this._paymenttypename = value;
            }
        }

        public int? RealShippingModeId
        {
            get
            {
                return this._realshippingmodeid;
            }
            set
            {
                this._realshippingmodeid = value;
            }
        }

        public string RealShippingModeName
        {
            get
            {
                return this._realshippingmodename;
            }
            set
            {
                this._realshippingmodename = value;
            }
        }

        public string ReferID
        {
            get
            {
                return this._referid;
            }
            set
            {
                this._referid = value;
            }
        }

        public string ReferURL
        {
            get
            {
                return this._referurl;
            }
            set
            {
                this._referurl = value;
            }
        }

        public int RefundStatus
        {
            get
            {
                return this._refundstatus;
            }
            set
            {
                this._refundstatus = value;
            }
        }

        public int? RegionId
        {
            get
            {
                return this._regionid;
            }
            set
            {
                this._regionid = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public string SellerCellPhone
        {
            get
            {
                return this._sellercellphone;
            }
            set
            {
                this._sellercellphone = value;
            }
        }

        public string SellerEmail
        {
            get
            {
                return this._selleremail;
            }
            set
            {
                this._selleremail = value;
            }
        }

        public int? SellerID
        {
            get
            {
                return this._sellerid;
            }
            set
            {
                this._sellerid = value;
            }
        }

        public string SellerName
        {
            get
            {
                return this._sellername;
            }
            set
            {
                this._sellername = value;
            }
        }

        public string ShipAddress
        {
            get
            {
                return this._shipaddress;
            }
            set
            {
                this._shipaddress = value;
            }
        }

        public string ShipCellPhone
        {
            get
            {
                return this._shipcellphone;
            }
            set
            {
                this._shipcellphone = value;
            }
        }

        public string ShipEmail
        {
            get
            {
                return this._shipemail;
            }
            set
            {
                this._shipemail = value;
            }
        }

        public string ShipName
        {
            get
            {
                return this._shipname;
            }
            set
            {
                this._shipname = value;
            }
        }

        public string ShipOrderNumber
        {
            get
            {
                return this._shipordernumber;
            }
            set
            {
                this._shipordernumber = value;
            }
        }

        public string ShipperAddress
        {
            get
            {
                return this._shipperaddress;
            }
            set
            {
                this._shipperaddress = value;
            }
        }

        public string ShipperCellPhone
        {
            get
            {
                return this._shippercellphone;
            }
            set
            {
                this._shippercellphone = value;
            }
        }

        public int? ShipperId
        {
            get
            {
                return this._shipperid;
            }
            set
            {
                this._shipperid = value;
            }
        }

        public string ShipperName
        {
            get
            {
                return this._shippername;
            }
            set
            {
                this._shippername = value;
            }
        }

        public int? ShippingModeId
        {
            get
            {
                return this._shippingmodeid;
            }
            set
            {
                this._shippingmodeid = value;
            }
        }

        public string ShippingModeName
        {
            get
            {
                return this._shippingmodename;
            }
            set
            {
                this._shippingmodename = value;
            }
        }

        public int ShippingStatus
        {
            get
            {
                return this._shippingstatus;
            }
            set
            {
                this._shippingstatus = value;
            }
        }

        public string ShipRegion
        {
            get
            {
                return this._shipregion;
            }
            set
            {
                this._shipregion = value;
            }
        }

        public string ShipTelPhone
        {
            get
            {
                return this._shiptelphone;
            }
            set
            {
                this._shiptelphone = value;
            }
        }

        public string ShipZipCode
        {
            get
            {
                return this._shipzipcode;
            }
            set
            {
                this._shipzipcode = value;
            }
        }

        public int? SupplierId
        {
            get
            {
                return this._supplierid;
            }
            set
            {
                this._supplierid = value;
            }
        }

        public string SupplierName
        {
            get
            {
                return this._suppliername;
            }
            set
            {
                this._suppliername = value;
            }
        }

        public DateTime? UpdatedDate
        {
            get
            {
                return this._updateddate;
            }
            set
            {
                this._updateddate = value;
            }
        }

        public int? Weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                this._weight = value;
            }
        }
    }
}

