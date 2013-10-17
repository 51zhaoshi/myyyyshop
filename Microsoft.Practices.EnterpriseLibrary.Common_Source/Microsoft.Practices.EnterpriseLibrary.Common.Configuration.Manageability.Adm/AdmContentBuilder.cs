namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class AdmContentBuilder
    {
        public const string AvailableValueName = "Available";
        private Stack<AdmCategory> categoriesStack;
        private AdmContent content;
        private AdmPolicy currentPolicy;
        private static Regex identifierRe = new Regex("^[^\"]*$");
        private static Regex keyRe = new Regex("^([^\\\"]+)(?:\\\\([^\\\"]+))*$");
        private const int MaxNumericConstraint = 0x3b9ac9ff;
        private const int MinNumericConstraint = 0;
        public const string NoneListItem = "__none__";

        internal AdmContentBuilder() : this(new AdmContent())
        {
        }

        protected AdmContentBuilder(AdmContent content)
        {
            this.content = content;
            this.categoriesStack = new Stack<AdmCategory>();
        }

        public void AddCheckboxPart(string partName, string valueName, bool checkedByDefault)
        {
            this.AddCheckboxPart(partName, valueName, checkedByDefault, true, true);
        }

        public void AddCheckboxPart(string partName, string valueName, bool checkedByDefault, bool valueForOn, bool valueForOff)
        {
            this.AddCheckboxPart(partName, null, valueName, checkedByDefault, valueForOn, valueForOff);
        }

        public void AddCheckboxPart(string partName, string keyName, string valueName, bool checkedByDefault, bool valueForOn, bool valueForOff)
        {
            CheckPartParameters(partName, keyName, valueName);
            this.AddPart(new AdmCheckboxPart(partName, keyName, valueName, checkedByDefault, valueForOn, valueForOff));
        }

        public void AddComboBoxPart(string partName, string valueName, string defaultValue, int maxlen, bool required, params string[] suggestions)
        {
            this.AddComboBoxPart(partName, null, valueName, defaultValue, maxlen, required, suggestions);
        }

        public void AddComboBoxPart(string partName, string keyName, string valueName, string defaultValue, int maxlen, bool required, params string[] suggestions)
        {
            maxlen = 0;
            CheckPartParameters(partName, keyName, valueName);
            this.CheckDefaultValue(defaultValue, maxlen, partName);
            CheckSuggestions(suggestions);
            this.CheckMaxlen(maxlen, partName);
            this.AddPart(new AdmComboBoxPart(partName, keyName, valueName, defaultValue, suggestions, maxlen, required));
        }

        public void AddDropDownListPart(string partName, string valueName, IEnumerable<AdmDropDownListItem> items, string defaultValue)
        {
            this.AddDropDownListPart(partName, null, valueName, items, defaultValue);
        }

        public void AddDropDownListPart(string partName, string keyName, string valueName, IEnumerable<AdmDropDownListItem> items, string defaultValue)
        {
            CheckPartParameters(partName, keyName, valueName);
            this.CheckDefaultValue(defaultValue, 0xff, partName);
            this.AddPart(new AdmDropDownListPart(partName, keyName, valueName, items, defaultValue));
        }

        public void AddDropDownListPartForEnumeration<T>(string partName, string valueName, T defaultValue) where T: struct
        {
            this.AddDropDownListPartForEnumeration<T>(partName, null, valueName, defaultValue);
        }

        public void AddDropDownListPartForEnumeration<T>(string partName, string keyName, string valueName, T defaultValue) where T: struct
        {
            List<AdmDropDownListItem> items = new List<AdmDropDownListItem>();
            foreach (T local in Enum.GetValues(typeof(T)))
            {
                items.Add(new AdmDropDownListItem(local.ToString(), local.ToString()));
            }
            this.AddDropDownListPart(partName, keyName, valueName, items, defaultValue.ToString());
        }

        public void AddDropDownListPartForNamedElementCollection<T>(string partName, string valueName, NamedElementCollection<T> elements, string defaultElementName, bool allowNone) where T: NamedConfigurationElement, new()
        {
            this.AddDropDownListPartForNamedElementCollection<T>(partName, null, valueName, elements, defaultElementName, allowNone);
        }

        public void AddDropDownListPartForNamedElementCollection<T>(string partName, string keyName, string valueName, NamedElementCollection<T> elements, string defaultElementName, bool allowNone) where T: NamedConfigurationElement, new()
        {
            List<AdmDropDownListItem> items = new List<AdmDropDownListItem>();
            if (allowNone)
            {
                items.Add(new AdmDropDownListItem(Resources.NoneListItem, "__none__"));
            }
            foreach (T local in elements)
            {
                items.Add(new AdmDropDownListItem(local.Name, local.Name));
            }
            this.AddDropDownListPart(partName, keyName, valueName, items, !string.IsNullOrEmpty(defaultElementName) ? defaultElementName : Resources.NoneListItem);
        }

        public void AddEditTextPart(string partName, string valueName, string defaultValue, int maxlen, bool required)
        {
            this.AddEditTextPart(partName, null, valueName, defaultValue, maxlen, required);
        }

        public void AddEditTextPart(string partName, string keyName, string valueName, string defaultValue, int maxlen, bool required)
        {
            CheckPartParameters(partName, keyName, valueName);
            this.CheckDefaultValue(defaultValue, maxlen, partName);
            this.CheckMaxlen(maxlen, partName);
            this.AddPart(new AdmEditTextPart(partName, keyName, valueName, defaultValue, maxlen, required));
        }

        public void AddNumericPart(string partName, string valueName, int? defaultValue)
        {
            this.AddNumericPart(partName, null, valueName, defaultValue, 0, null);
        }

        public void AddNumericPart(string partName, string keyName, string valueName, int? defaultValue)
        {
            this.AddNumericPart(partName, keyName, valueName, defaultValue, 0, null);
        }

        public void AddNumericPart(string partName, string keyName, string valueName, int? defaultValue, int? minValue, int? maxValue)
        {
            CheckPartParameters(partName, keyName, valueName);
            this.CheckMinMaxValues(minValue, maxValue, partName);
            this.CheckDefaultNumericValue(defaultValue, minValue, maxValue, partName);
            this.AddPart(new AdmNumericPart(partName, keyName, valueName, defaultValue, minValue, maxValue));
        }

        internal void AddPart(AdmPart part)
        {
            this.CheckStartedPolicy();
            this.currentPolicy.AddPart(part);
        }

        public void AddTextPart(string partName)
        {
            CheckPartName(partName);
            this.AddPart(new AdmTextPart(partName));
        }

        private static void CheckCategoryName(string categoryName)
        {
            CheckName(categoryName, "categoryName");
        }

        private void CheckCurrentCategory()
        {
            if (this.categoriesStack.Count == 0)
            {
                throw new InvalidOperationException(Resources.ExceptionAdmBuildingNoCurrentCategory);
            }
        }

        private void CheckDefaultNumericValue(int? defaultValue, int? minValue, int? maxValue, string partName)
        {
            if (defaultValue.HasValue)
            {
                if (minValue.HasValue)
                {
                    int? nullable = defaultValue;
                    int? nullable2 = minValue;
                    if ((nullable.GetValueOrDefault() < nullable2.GetValueOrDefault()) && (nullable.HasValue & nullable2.HasValue))
                    {
                        throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmDefaultValueBelowMinValue, new object[] { partName, this.currentPolicy.Name, defaultValue, minValue }), "defaultValue");
                    }
                }
                if (maxValue.HasValue)
                {
                    int? nullable3 = defaultValue;
                    int? nullable4 = maxValue;
                    if ((nullable3.GetValueOrDefault() > nullable4.GetValueOrDefault()) && (nullable3.HasValue & nullable4.HasValue))
                    {
                        throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmDefaultValueAboveMaxValue, new object[] { partName, this.currentPolicy.Name, defaultValue, maxValue }), "defaultValue");
                    }
                }
            }
        }

        private void CheckDefaultValue(string defaultValue, int maxlen, string partName)
        {
            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }
            if (!identifierRe.IsMatch(defaultValue))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmInvalidDefaultValue, new object[] { defaultValue }), "defaultValue");
            }
            if (defaultValue.Length > ((maxlen != 0) ? maxlen : 0x400))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmDefaultValueLongerThanMaxlen, new object[] { partName, this.currentPolicy.Name, maxlen }), "default value");
            }
        }

        private static void CheckKeyName(string policyKey)
        {
            CheckKeyName(policyKey, false);
        }

        private static void CheckKeyName(string policyKey, bool keyMustBeNotNull)
        {
            if (policyKey == null)
            {
                if (keyMustBeNotNull)
                {
                    throw new ArgumentNullException("policyKey");
                }
            }
            else
            {
                Match match = keyRe.Match(policyKey);
                if (!match.Success)
                {
                    throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmInvalidCharactersInRegistryKey, new object[] { policyKey }), "policyKey");
                }
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    Group group = match.Groups[i];
                    for (int j = 0; j < group.Captures.Count; j++)
                    {
                        string str = group.Captures[j].Value;
                        if (str.Length > 0xff)
                        {
                            throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmRegistryKeyPathSegmentTooLong, new object[] { str }), "policyKey");
                        }
                    }
                }
            }
        }

        private void CheckMaxlen(int maxlen, string partName)
        {
            if ((maxlen < 0) || (maxlen > 0x400))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmInvalidMaxlen, new object[] { partName, this.currentPolicy.Name, maxlen }), "maxlen");
            }
        }

        private void CheckMinMaxValues(int? minValue, int? maxValue, string partName)
        {
            if (minValue.HasValue && ((minValue < 0) || (minValue > 0x3b9ac9ff)))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmNumericConstraintOutsideRange, new object[] { partName, this.currentPolicy.Name, minValue }), "minValue");
            }
            if (maxValue.HasValue && ((maxValue < 0) || (maxValue > 0x3b9ac9ff)))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmNumericConstraintOutsideRange, new object[] { partName, this.currentPolicy.Name, maxValue }), "maxValue");
            }
            if ((minValue.HasValue && maxValue.HasValue) && (minValue.Value > maxValue.Value))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmMinValueLargerThanMaxValue, new object[] { partName, this.currentPolicy.Name, minValue, maxValue }), "maxValue");
            }
        }

        private static void CheckName(string name, string parameterName)
        {
            if (name == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if ((name.Length == 0) || !identifierRe.IsMatch(name))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmInvalidName, new object[] { name }), parameterName);
            }
        }

        private void CheckNoStartedPolicy()
        {
            if (this.currentPolicy != null)
            {
                throw new InvalidOperationException(Resources.ExceptionAdmBuildingPolicyNotFinished);
            }
        }

        private static void CheckPartName(string partName)
        {
            CheckName(partName, "partName");
        }

        private static void CheckPartParameters(string partName, string keyName, string valueName)
        {
            CheckPartName(partName);
            CheckKeyName(keyName);
            CheckValueName(valueName);
        }

        private static void CheckPolicyName(string policyName)
        {
            CheckName(policyName, "policyName");
        }

        private void CheckStartedPolicy()
        {
            if (this.currentPolicy == null)
            {
                throw new InvalidOperationException(Resources.ExceptionAdmBuildingNoCurrentPolicy);
            }
        }

        private static void CheckSuggestions(string[] suggestions)
        {
            foreach (string str in suggestions)
            {
                if (!identifierRe.IsMatch(str))
                {
                    throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmInvalidSuggestion, new object[] { str }), "suggestions");
                }
            }
        }

        private static void CheckValueName(string valueName)
        {
            CheckName(valueName, "valueName");
            if (valueName.Length > 0xff)
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionAdmRegistryValueNameTooLong, new object[] { valueName }), "valueName");
            }
        }

        public void EndCategory()
        {
            this.CheckCurrentCategory();
            this.CheckNoStartedPolicy();
            AdmCategory category = this.categoriesStack.Pop();
            if (this.categoriesStack.Count == 0)
            {
                this.content.AddCategory(category);
            }
            else
            {
                this.categoriesStack.Peek().AddCategory(category);
            }
        }

        public void EndPolicy()
        {
            this.CheckStartedPolicy();
            this.categoriesStack.Peek().AddPolicy(this.currentPolicy);
            this.currentPolicy = null;
        }

        internal AdmContent GetContent()
        {
            if (this.categoriesStack.Count > 0)
            {
                throw new InvalidOperationException(Resources.ExceptionAdmBuildingProcessNotFinished);
            }
            return this.content;
        }

        public void StartCategory(string categoryName)
        {
            CheckCategoryName(categoryName);
            this.CheckNoStartedPolicy();
            this.categoriesStack.Push(new AdmCategory(categoryName));
        }

        public void StartPolicy(string policyName, string policyKey)
        {
            CheckPolicyName(policyName);
            CheckKeyName(policyKey, true);
            this.CheckCurrentCategory();
            this.CheckNoStartedPolicy();
            this.currentPolicy = new AdmPolicy(policyName, policyKey, "Available");
        }
    }
}

