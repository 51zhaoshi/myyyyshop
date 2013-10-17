namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct AdmDropDownListItem
    {
        private string name;
        private string value;
        public AdmDropDownListItem(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

