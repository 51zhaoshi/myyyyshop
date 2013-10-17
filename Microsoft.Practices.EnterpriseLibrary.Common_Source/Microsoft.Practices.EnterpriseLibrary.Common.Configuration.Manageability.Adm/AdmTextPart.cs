namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;

    public class AdmTextPart : AdmPart
    {
        internal const string TextTemplate = "\t\t\tTEXT";

        internal AdmTextPart(string partName) : base(partName, null, null)
        {
        }

        protected override string PartTypeTemplate
        {
            get
            {
                return "\t\t\tTEXT";
            }
        }
    }
}

