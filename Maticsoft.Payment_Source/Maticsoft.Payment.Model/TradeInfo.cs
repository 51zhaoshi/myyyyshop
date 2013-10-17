namespace Maticsoft.Payment.Model
{
    using System;

    public class TradeInfo
    {
        private string body;
        private string buyerEmailAddress;
        private DateTime date;
        private string orderId;
        private string show_url;
        private string subject;
        private string token;
        private decimal totalMoney;

        public virtual string Body
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
            }
        }

        public virtual string BuyerEmailAddress
        {
            get
            {
                return this.buyerEmailAddress;
            }
            set
            {
                this.buyerEmailAddress = value;
            }
        }

        public virtual DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

        public virtual string OrderId
        {
            get
            {
                return this.orderId;
            }
            set
            {
                this.orderId = value;
            }
        }

        public virtual string Showurl
        {
            get
            {
                return this.show_url;
            }
            set
            {
                this.show_url = value;
            }
        }

        public virtual string Subject
        {
            get
            {
                return this.subject;
            }
            set
            {
                this.subject = value;
            }
        }

        public virtual string Token
        {
            get
            {
                return this.token;
            }
            set
            {
                this.token = value;
            }
        }

        public virtual decimal TotalMoney
        {
            get
            {
                return this.totalMoney;
            }
            set
            {
                this.totalMoney = value;
            }
        }
    }
}

