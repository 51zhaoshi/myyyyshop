namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class AdmPolicy
    {
        private string keyName;
        internal const string KeyNameTemplate = "\t\t\tKEYNAME \"Software\\Policies\\{0}\"";
        private string name;
        private List<AdmPart> parts;
        internal const string PolicyEndTemplate = "\tEND POLICY";
        internal const string PolicyStartTemplate = "\tPOLICY \"{0}\"";
        private string valueName;
        internal const string ValueNameTemplate = "\t\t\tVALUENAME \"{0}\" VALUEON NUMERIC 1 VALUEOFF NUMERIC 0";

        internal AdmPolicy(string name, string keyName, string valueName)
        {
            this.keyName = keyName;
            this.name = name;
            this.valueName = valueName;
            this.parts = new List<AdmPart>();
        }

        internal void AddPart(AdmPart part)
        {
            this.parts.Add(part);
        }

        internal void Write(TextWriter writer)
        {
            writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\tPOLICY \"{0}\"", new object[] { this.name }));
            writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tKEYNAME \"Software\\Policies\\{0}\"", new object[] { this.keyName }));
            if (this.valueName != null)
            {
                writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\tVALUENAME \"{0}\" VALUEON NUMERIC 1 VALUEOFF NUMERIC 0", new object[] { this.valueName }));
            }
            foreach (AdmPart part in this.parts)
            {
                part.Write(writer);
            }
            writer.WriteLine("\tEND POLICY");
        }

        public string KeyName
        {
            get
            {
                return this.keyName;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public IEnumerable<AdmPart> Parts
        {
            get
            {
                return this.parts;
            }
        }

        public string ValueName
        {
            get
            {
                return this.valueName;
            }
        }
    }
}

