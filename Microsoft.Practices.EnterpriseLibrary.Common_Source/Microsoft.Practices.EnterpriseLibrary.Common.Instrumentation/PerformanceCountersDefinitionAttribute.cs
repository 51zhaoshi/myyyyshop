namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Diagnostics;

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple=false)]
    public sealed class PerformanceCountersDefinitionAttribute : Attribute
    {
        private string categoryHelp;
        private string categoryName;
        private PerformanceCounterCategoryType categoryType;

        public PerformanceCountersDefinitionAttribute(string categoryName, string categoryHelp) : this(categoryName, categoryHelp, PerformanceCounterCategoryType.MultiInstance)
        {
        }

        public PerformanceCountersDefinitionAttribute(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType)
        {
            this.categoryName = categoryName;
            this.categoryHelp = categoryHelp;
            this.categoryType = categoryType;
        }

        public string CategoryHelp
        {
            get
            {
                return this.categoryHelp;
            }
        }

        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
        }

        public PerformanceCounterCategoryType CategoryType
        {
            get
            {
                return this.categoryType;
            }
        }
    }
}

