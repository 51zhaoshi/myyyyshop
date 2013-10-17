namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    public class FileConfigurationParameter : IConfigurationParameter
    {
        private readonly string fileName;

        public FileConfigurationParameter(string fileName)
        {
            this.fileName = fileName;
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }
    }
}

