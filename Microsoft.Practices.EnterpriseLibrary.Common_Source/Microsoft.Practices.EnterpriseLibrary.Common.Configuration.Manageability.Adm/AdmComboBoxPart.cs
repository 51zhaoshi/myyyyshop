namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class AdmComboBoxPart : AdmEditTextPart
    {
        internal const string ComboBoxTemplate = "\t\t\tCOMBOBOX";
        private IEnumerable<string> suggestions;
        internal const string SuggestionsEndTemplate = "\t\t\tEND SUGGESTIONS";
        internal const string SuggestionsStartTemplate = "\t\t\tSUGGESTIONS";

        internal AdmComboBoxPart(string partName, string keyName, string valueName, string defaultValue, IEnumerable<string> suggestions, int maxlen, bool required) : base(partName, keyName, valueName, defaultValue, maxlen, required)
        {
            this.suggestions = suggestions;
        }

        protected override void WritePart(TextWriter writer)
        {
            base.WritePart(writer);
            writer.Write("\t\t\tSUGGESTIONS");
            foreach (string str in this.suggestions)
            {
                writer.Write(string.Format(CultureInfo.InvariantCulture, " \"{0}\"", new object[] { str }));
            }
            writer.Write(writer.NewLine);
            writer.WriteLine("\t\t\tEND SUGGESTIONS");
        }

        protected override string PartTypeTemplate
        {
            get
            {
                return "\t\t\tCOMBOBOX";
            }
        }

        public IEnumerable<string> Suggestions
        {
            get
            {
                return this.suggestions;
            }
        }
    }
}

