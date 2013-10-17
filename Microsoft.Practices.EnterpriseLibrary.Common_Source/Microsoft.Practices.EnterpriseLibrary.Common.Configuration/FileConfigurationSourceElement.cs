namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;

    public class FileConfigurationSourceElement : ConfigurationSourceElement
    {
        private const string filePathProperty = "filePath";

        public FileConfigurationSourceElement() : this(Resources.FileConfigurationSourceName, string.Empty)
        {
        }

        public FileConfigurationSourceElement(string name, string filePath) : base(name, typeof(FileConfigurationSource))
        {
            this.FilePath = filePath;
        }

        protected internal override IConfigurationSource CreateSource()
        {
            return new FileConfigurationSource(this.FilePath);
        }

        [ConfigurationProperty("filePath", IsRequired=true)]
        public string FilePath
        {
            get
            {
                return (string) base["filePath"];
            }
            set
            {
                base["filePath"] = value;
            }
        }
    }
}

