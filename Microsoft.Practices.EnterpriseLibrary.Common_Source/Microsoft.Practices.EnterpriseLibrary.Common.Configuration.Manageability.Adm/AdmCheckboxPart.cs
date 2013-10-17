namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.IO;

    public class AdmCheckboxPart : AdmPart
    {
        internal const string CheckBoxCheckedTemplate = "\t\t\tCHECKBOX DEFCHECKED";
        internal const string CheckBoxTemplate = "\t\t\tCHECKBOX";
        private bool checkedByDefault;
        internal const string DefaultCheckBoxOffTemplate = "\t\t\tVALUEOFF NUMERIC 0";
        internal const string DefaultCheckBoxOnTemplate = "\t\t\tVALUEON NUMERIC 1";
        private bool valueForOff;
        private bool valueForOn;

        internal AdmCheckboxPart(string partName, string keyName, string valueName, bool checkedByDefault, bool valueForOn, bool valueForOff) : base(partName, keyName, valueName)
        {
            this.checkedByDefault = checkedByDefault;
            this.valueForOn = valueForOn;
            this.valueForOff = valueForOff;
        }

        protected override void WritePart(TextWriter writer)
        {
            base.WritePart(writer);
            if (this.valueForOn)
            {
                writer.WriteLine("\t\t\tVALUEON NUMERIC 1");
            }
            if (this.valueForOff)
            {
                writer.WriteLine("\t\t\tVALUEOFF NUMERIC 0");
            }
        }

        public bool CheckedByDefault
        {
            get
            {
                return this.checkedByDefault;
            }
        }

        protected override string PartTypeTemplate
        {
            get
            {
                if (!this.checkedByDefault)
                {
                    return "\t\t\tCHECKBOX";
                }
                return "\t\t\tCHECKBOX DEFCHECKED";
            }
        }

        public bool ValueForOff
        {
            get
            {
                return this.valueForOff;
            }
        }

        public bool ValueForOn
        {
            get
            {
                return this.valueForOn;
            }
        }
    }
}

