namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public abstract class CustomProviderDataManageabilityProvider<T> : ConfigurationElementManageabilityProviderBase<T> where T: NameTypeConfigurationElement, ICustomProviderData
    {
        protected internal const string AttributesPropertyName = "attributes";
        private string policyTemplate;
        protected internal const string ProviderTypePropertyName = "type";

        protected CustomProviderDataManageabilityProvider(string policyTemplate)
        {
            this.policyTemplate = policyTemplate;
        }

        protected override void AddElementAdministrativeTemplateParts(AdmContentBuilder contentBuilder, T configurationObject, IConfigurationSource configurationSource, string elementPolicyKeyName)
        {
            contentBuilder.AddEditTextPart(Resources.CustomProviderTypePartName, "type", configurationObject.Type.AssemblyQualifiedName, 0x400, true);
            contentBuilder.AddEditTextPart(Resources.CustomProviderAttributesPartName, "attributes", CustomProviderDataManageabilityProvider<T>.GenerateAttributesString(configurationObject.Attributes), 0x400, false);
        }

        protected abstract NamedConfigurationSetting CreateSetting(T configurationObject, string[] attributes);
        private static string[] GenerateAttributesArray(NameValueCollection attributes)
        {
            string[] strArray = new string[attributes.Count];
            int num = 0;
            foreach (string str in attributes.AllKeys)
            {
                strArray[num++] = KeyValuePairEncoder.EncodeKeyValuePair(str, attributes.Get(str));
            }
            return strArray;
        }

        protected static string GenerateAttributesString(NameValueCollection attributes)
        {
            KeyValuePairEncoder encoder = new KeyValuePairEncoder();
            foreach (string str in attributes.AllKeys)
            {
                encoder.AppendKeyValuePair(str, attributes[str]);
            }
            return encoder.GetEncodedKeyValuePairs();
        }

        protected override void GenerateWmiObjects(T configurationObject, ICollection<ConfigurationSetting> wmiSettings)
        {
            string[] attributes = CustomProviderDataManageabilityProvider<T>.GenerateAttributesArray(configurationObject.Attributes);
            wmiSettings.Add(this.CreateSetting(configurationObject, attributes));
        }

        protected override void OverrideWithGroupPolicies(T configurationObject, IRegistryKey policyKey)
        {
            Type typeValue = policyKey.GetTypeValue("type");
            string stringValue = policyKey.GetStringValue("attributes");
            configurationObject.Type = typeValue;
            configurationObject.Attributes.Clear();
            Dictionary<string, string> attributesDictionary = new Dictionary<string, string>();
            KeyValuePairParser.ExtractKeyValueEntries(stringValue, attributesDictionary);
            foreach (KeyValuePair<string, string> pair in attributesDictionary)
            {
                configurationObject.Attributes.Add(pair.Key, pair.Value);
            }
        }

        protected override string ElementPolicyNameTemplate
        {
            get
            {
                return this.policyTemplate;
            }
        }
    }
}

