namespace Maticsoft.Payment.Model
{
    using System;

    public class GatewayInfo
    {
        private string currency_Type;
        private string gateway_Type;
        private string input_charset;
        private string language;
        private string notify_url;
        private string return_url;

        public virtual string CurrencyType
        {
            get
            {
                return this.currency_Type;
            }
            set
            {
                this.currency_Type = value;
            }
        }

        public virtual string GatewayType
        {
            get
            {
                return this.gateway_Type;
            }
            set
            {
                this.gateway_Type = value;
            }
        }

        public virtual string Inputcharset
        {
            get
            {
                return this.input_charset;
            }
            set
            {
                this.input_charset = value;
            }
        }

        public virtual string Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;
            }
        }

        public virtual string NotifyUrl
        {
            get
            {
                return this.notify_url;
            }
            set
            {
                this.notify_url = value;
            }
        }

        public virtual string ReturnUrl
        {
            get
            {
                return this.return_url;
            }
            set
            {
                this.return_url = value;
            }
        }
    }
}

