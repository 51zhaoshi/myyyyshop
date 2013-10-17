namespace Maticsoft.Payment.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Xml;

    public class GatewayProvider
    {
        private string displayName;
        private string name;
        private string notifyType;
        private NameValueCollection providerAttributes;
        private string requestType;
        private IList<string> supportedCurrencys;

        public GatewayProvider(XmlAttributeCollection attributes)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException("attributes");
            }
            this.name = attributes["name"].Value.ToLower();
            this.requestType = attributes["requestType"].Value;
            this.notifyType = attributes["notifyType"].Value;
            this.displayName = attributes["displayName"].Value;
            string[] strArray = attributes["supportedCurrency"].Value.Split(new char[] { ',' });
            this.supportedCurrencys = new List<string>();
            foreach (string str in strArray)
            {
                this.supportedCurrencys.Add(str);
            }
            this.providerAttributes = new NameValueCollection();
            foreach (XmlAttribute attribute in attributes)
            {
                if ((((attribute.Name != "name") && (attribute.Name != "displayName")) && ((attribute.Name != "requestType") && (attribute.Name != "notifyType"))) && (attribute.Name != "supportedCurrency"))
                {
                    this.providerAttributes.Add(attribute.Name, attribute.Value);
                }
            }
        }

        public NameValueCollection Attributes
        {
            get
            {
                return this.providerAttributes;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string NotifyType
        {
            get
            {
                return this.notifyType;
            }
        }

        public string RequestType
        {
            get
            {
                return this.requestType;
            }
        }

        public IList<string> SupportedCurrencys
        {
            get
            {
                return this.supportedCurrencys;
            }
        }
    }
}

