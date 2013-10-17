namespace Maticsoft.Payment.Configuration
{
    using Maticsoft.Payment.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;

    public class PayConfiguration
    {
        private Dictionary<string, string> _supportedCurrencies = new Dictionary<string, string>();
        public const string CacheKey = "Maticsoft_PayConfiguration";
        private IList<string> keys = new List<string>();
        private Hashtable providers = new Hashtable();
        private XmlDocument XmlDoc;

        public PayConfiguration(XmlDocument doc)
        {
            this.XmlDoc = doc;
            this.LoadValuesFromConfigurationXml();
        }

        public static PayConfiguration GetConfig()
        {
            PayConfiguration configuration = DataCache.Get("Maticsoft_PayConfiguration") as PayConfiguration;
            if (configuration == null)
            {
                string path = null;
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    path = current.Request.MapPath("~/Gateway.config");
                }
                else
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gateway.config");
                }
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("THE GATEWAY FILE NOT FOUND! PATH: " + path);
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                configuration = new PayConfiguration(doc);
                DataCache.Max("Maticsoft_PayConfiguration", configuration, new CacheDependency(path));
            }
            return configuration;
        }

        public XmlNode GetConfigSection(string nodePath)
        {
            return this.XmlDoc.SelectSingleNode(nodePath);
        }

        internal void GetCurrencies(XmlNode node)
        {
            foreach (XmlNode node2 in node.ChildNodes)
            {
                if ((string.Compare(node2.Attributes["enabled"].Value, "true", false, CultureInfo.InvariantCulture) == 0) && !this._supportedCurrencies.ContainsKey(node2.Attributes["code"].Value))
                {
                    this._supportedCurrencies.Add(node2.Attributes["code"].Value, node2.Attributes["symbol"].Value);
                }
            }
        }

        internal void GetProviders(XmlNode node, Hashtable table)
        {
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                XmlNode node2 = node.ChildNodes[i];
                switch (node2.Name)
                {
                    case "add":
                        table.Add(node2.Attributes["name"].Value.ToLower(), new GatewayProvider(node2.Attributes));
                        this.keys.Add(node2.Attributes["name"].Value.ToLower());
                        break;

                    case "remove":
                        table.Remove(node2.Attributes["name"].Value.ToLower());
                        this.keys.Remove(node2.Attributes["name"].Value.ToLower());
                        break;

                    case "clear":
                        table.Clear();
                        this.keys.Clear();
                        break;
                }
            }
        }

        internal void LoadValuesFromConfigurationXml()
        {
            foreach (XmlNode node in this.GetConfigSection("Gateway").ChildNodes)
            {
                if (node.Name == "currencies")
                {
                    this.GetCurrencies(node);
                }
                if (node.Name == "providers")
                {
                    this.GetProviders(node, this.providers);
                }
            }
        }

        public IList<string> Keys
        {
            get
            {
                return this.keys;
            }
        }

        public Hashtable Providers
        {
            get
            {
                return this.providers;
            }
        }

        public Dictionary<string, string> SupportedCurrencies
        {
            get
            {
                return this._supportedCurrencies;
            }
        }
    }
}

