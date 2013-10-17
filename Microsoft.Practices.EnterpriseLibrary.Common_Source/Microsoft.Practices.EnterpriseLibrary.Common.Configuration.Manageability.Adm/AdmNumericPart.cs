namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Globalization;
    using System.IO;

    public class AdmNumericPart : AdmPart
    {
        private int? defaultValue;
        internal const string DefaultValueTemplate = "\t\t\tDEFAULT {0}";
        private int? maxValue;
        internal const string MaxValueTemplate = "\t\t\tMAX {0}";
        private int? minValue;
        internal const string MinValueTemplate = "\t\t\tMIN {0}";
        internal const string NumericTemplate = "\t\t\tNUMERIC";

        internal AdmNumericPart(string partName, string keyName, string valueName, int? defaultValue, int? minValue, int? maxValue) : base(partName, keyName, valueName)
        {
            this.defaultValue = defaultValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        protected override void WritePart(TextWriter writer)
        {
            base.WritePart(writer);
            if (this.defaultValue.HasValue)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tDEFAULT {0}", new object[] { this.defaultValue.Value }));
            }
            if (this.minValue.HasValue)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tMIN {0}", new object[] { this.minValue.Value }));
            }
            if (this.maxValue.HasValue)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tMAX {0}", new object[] { this.maxValue.Value }));
            }
        }

        public int? DefaultValue
        {
            get
            {
                return this.defaultValue;
            }
        }

        public int? MaxValue
        {
            get
            {
                return this.maxValue;
            }
        }

        public int? MinValue
        {
            get
            {
                return this.minValue;
            }
        }

        protected override string PartTypeTemplate
        {
            get
            {
                return "\t\t\tNUMERIC";
            }
        }
    }
}

