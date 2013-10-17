namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple=false)]
    public sealed class EventLogDefinitionAttribute : Attribute
    {
        private int categoryCount;
        private string categoryResourceFile;
        private string logName;
        private string messageResourceFile;
        private string parameterResourceFile;
        private string sourceName;

        public EventLogDefinitionAttribute(string logName, string sourceName)
        {
            this.logName = logName;
            this.sourceName = sourceName;
        }

        public int CategoryCount
        {
            get
            {
                return this.categoryCount;
            }
            set
            {
                this.categoryCount = value;
            }
        }

        public string CategoryResourceFile
        {
            get
            {
                return this.categoryResourceFile;
            }
            set
            {
                this.categoryResourceFile = value;
            }
        }

        public string LogName
        {
            get
            {
                return this.logName;
            }
        }

        public string MessageResourceFile
        {
            get
            {
                return this.messageResourceFile;
            }
            set
            {
                this.messageResourceFile = value;
            }
        }

        public string ParameterResourceFile
        {
            get
            {
                return this.parameterResourceFile;
            }
            set
            {
                this.parameterResourceFile = value;
            }
        }

        public string SourceName
        {
            get
            {
                return this.sourceName;
            }
        }
    }
}

