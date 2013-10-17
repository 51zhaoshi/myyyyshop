namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    public class PerformanceCounterInstanceName
    {
        private int maxPrefixLength;
        private const int MaxPrefixLength = 0x20;
        private int maxSuffixLength;
        private const int MaxSuffixLength = 0x20;
        private string prefix;
        private string suffix;

        public PerformanceCounterInstanceName(string prefix, string suffix) : this(prefix, suffix, 0x20, 0x20)
        {
        }

        internal PerformanceCounterInstanceName(string prefix, string suffix, int maxPrefixLength, int maxSuffixLength)
        {
            this.maxPrefixLength = maxPrefixLength;
            this.maxSuffixLength = maxSuffixLength;
            this.prefix = this.NormalizeStringLength(prefix, maxPrefixLength);
            this.suffix = this.NormalizeStringLength(suffix, maxSuffixLength);
        }

        private string NormalizeStringLength(string namePart, int namePartMaxLength)
        {
            if (namePart.Length <= namePartMaxLength)
            {
                return namePart;
            }
            return namePart.Substring(0, namePartMaxLength);
        }

        public override string ToString()
        {
            string str = "";
            if (this.prefix.Length > 0)
            {
                str = str + this.prefix + " - ";
            }
            return (str + this.suffix);
        }
    }
}

