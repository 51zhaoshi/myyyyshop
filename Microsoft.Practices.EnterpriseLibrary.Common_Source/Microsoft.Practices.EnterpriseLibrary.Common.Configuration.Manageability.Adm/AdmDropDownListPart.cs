namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class AdmDropDownListPart : AdmPart
    {
        internal const string DefaultListItemTemplate = "\t\t\t\tNAME \"{0}\" VALUE \"{1}\" DEFAULT";
        private string defaultValue;
        internal const string DropDownListTemplate = "\t\t\tDROPDOWNLIST";
        internal const string ItemListEndTemplate = "\t\t\tEND ITEMLIST";
        internal const string ItemListStartTemplate = "\t\t\tITEMLIST";
        private IEnumerable<AdmDropDownListItem> items;
        internal const string ListItemTemplate = "\t\t\t\tNAME \"{0}\" VALUE \"{1}\"";

        internal AdmDropDownListPart(string partName, string keyName, string valueName, IEnumerable<AdmDropDownListItem> items, string defaultValue) : base(partName, keyName, valueName)
        {
            this.items = items;
            this.defaultValue = defaultValue;
        }

        protected override void WritePart(TextWriter writer)
        {
            base.WritePart(writer);
            writer.WriteLine("\t\t\tITEMLIST");
            foreach (AdmDropDownListItem item in this.items)
            {
                if (item.Name.Equals(this.defaultValue))
                {
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\t\tNAME \"{0}\" VALUE \"{1}\" DEFAULT", new object[] { item.Name, item.Value }));
                }
                else
                {
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "\t\t\t\tNAME \"{0}\" VALUE \"{1}\"", new object[] { item.Name, item.Value }));
                }
            }
            writer.WriteLine("\t\t\tEND ITEMLIST");
        }

        public string DefaultValue
        {
            get
            {
                return this.defaultValue;
            }
        }

        public IEnumerable<AdmDropDownListItem> Items
        {
            get
            {
                return this.items;
            }
        }

        protected override string PartTypeTemplate
        {
            get
            {
                return "\t\t\tDROPDOWNLIST";
            }
        }
    }
}

