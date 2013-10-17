namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    public class InstrumentationBaseAttribute : Attribute
    {
        private string subjectName;

        protected InstrumentationBaseAttribute(string subjectName)
        {
            if (string.IsNullOrEmpty(subjectName))
            {
                throw new ArgumentException("subjectName");
            }
            this.subjectName = subjectName;
        }

        public string SubjectName
        {
            get
            {
                return this.subjectName;
            }
        }
    }
}

