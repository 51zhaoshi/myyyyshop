namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Globalization;
    using System.IO;

    public class AdmEditTextPart : AdmPart
    {
        private string defaultValue;
        internal const string DefaultValueTemplate = "\t\t\tDEFAULT \"{0}\"";
        internal const string EditTextTemplate = "\t\t\tEDITTEXT";
        private int maxlen;
        internal const string MaxLengthTemplate = "\t\t\tMAXLEN {0}";
        private bool required;
        internal const string RequiredTemplate = "\t\t\tREQUIRED";

        protected internal AdmEditTextPart(string partName, string keyName, string valueName, string defaultValue, int maxlen, bool required) : base(partName, keyName, valueName)
        {
            this.defaultValue = defaultValue;
            this.maxlen = maxlen;
            this.required = required;
        }

        protected override void WritePart(TextWriter writer)
        {
            base.WritePart(writer);
            if (this.maxlen > 0)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tMAXLEN {0}", new object[] { this.maxlen }));
            }
            if (this.required)
            {
                writer.WriteLine("\t\t\tREQUIRED");
            }
            if (this.defaultValue != null)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tDEFAULT \"{0}\"", new object[] { this.defaultValue }));
            }
        }

        public string DefaultValue
        {
            get
            {
                return this.defaultValue;
            }
        }

        public int Maxlen
        {
            get
            {
                return this.maxlen;
            }
        }

        protected override string PartTypeTemplate
        {
            get
            {
                return "\t\t\tEDITTEXT";
            }
        }

        public bool Required
        {
            get
            {
                return this.required;
            }
        }
    }
}

