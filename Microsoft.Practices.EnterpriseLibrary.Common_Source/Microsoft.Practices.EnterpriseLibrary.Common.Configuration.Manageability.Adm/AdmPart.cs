namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Globalization;
    using System.IO;

    public abstract class AdmPart
    {
        private string keyName;
        internal const string KeyNameTemplate = "\t\t\tKEYNAME \"Software\\Policies\\{0}\"";
        internal const string PartEndTemplate = "\t\tEND PART";
        private string partName;
        internal const string PartStartTemplate = "\t\tPART \"{0}\"";
        private string valueName;
        internal const string ValueNameTemplate = "\t\t\tVALUENAME \"{0}\"";

        protected AdmPart(string partName, string keyName, string valueName)
        {
            this.keyName = keyName;
            this.partName = partName;
            this.valueName = valueName;
        }

        internal void Write(TextWriter writer)
        {
            writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\tPART \"{0}\"", new object[] { this.partName }));
            this.WritePart(writer);
            writer.WriteLine("\t\tEND PART");
        }

        protected virtual void WritePart(TextWriter writer)
        {
            writer.WriteLine(this.PartTypeTemplate);
            if (this.valueName != null)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tVALUENAME \"{0}\"", new object[] { this.valueName }));
            }
            if (this.keyName != null)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tKEYNAME \"Software\\Policies\\{0}\"", new object[] { this.keyName }));
            }
        }

        public string KeyName
        {
            get
            {
                return this.keyName;
            }
        }

        public string PartName
        {
            get
            {
                return this.partName;
            }
        }

        protected abstract string PartTypeTemplate { get; }

        public string ValueName
        {
            get
            {
                return this.valueName;
            }
        }
    }
}

