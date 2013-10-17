namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class AdmCategory
    {
        private List<AdmCategory> categories;
        internal const string CategoryEndTemplate = "END CATEGORY\t; \"{0}\"";
        internal const string CategoryStartTemplate = "CATEGORY \"{0}\"";
        private string name;
        private List<AdmPolicy> policies;

        internal AdmCategory(string categoryName)
        {
            this.name = categoryName;
            this.categories = new List<AdmCategory>();
            this.policies = new List<AdmPolicy>();
        }

        internal void AddCategory(AdmCategory category)
        {
            this.categories.Add(category);
        }

        internal void AddPolicy(AdmPolicy policy)
        {
            this.policies.Add(policy);
        }

        internal void Write(TextWriter writer)
        {
            writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "CATEGORY \"{0}\"", new object[] { this.name }));
            foreach (AdmCategory category in this.categories)
            {
                category.Write(writer);
            }
            foreach (AdmPolicy policy in this.policies)
            {
                policy.Write(writer);
            }
            writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "END CATEGORY\t; \"{0}\"", new object[] { this.name }));
        }

        public IEnumerable<AdmCategory> Categories
        {
            get
            {
                return this.categories;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public IEnumerable<AdmPolicy> Policies
        {
            get
            {
                return this.policies;
            }
        }
    }
}

