namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class AdmContent
    {
        private List<AdmCategory> categories = new List<AdmCategory>();

        protected internal AdmContent()
        {
        }

        internal void AddCategory(AdmCategory category)
        {
            this.categories.Add(category);
        }

        public void Write(TextWriter writer)
        {
            foreach (AdmCategory category in this.categories)
            {
                category.Write(writer);
            }
        }

        internal IEnumerable<AdmCategory> Categories
        {
            get
            {
                return this.categories;
            }
        }
    }
}

