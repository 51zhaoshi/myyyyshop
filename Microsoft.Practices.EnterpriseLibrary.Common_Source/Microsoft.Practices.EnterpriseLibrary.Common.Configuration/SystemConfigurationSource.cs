namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.Configuration;
    using System.IO;

    [ConfigurationElementType(typeof(SystemConfigurationSourceElement))]
    public class SystemConfigurationSource : IConfigurationSource
    {
        private static SystemConfigurationSourceImplementation implementation = new SystemConfigurationSourceImplementation(true);

        public void Add(IConfigurationParameter addParameter, string sectionName, ConfigurationSection configurationSection)
        {
            FileConfigurationParameter parameter = addParameter as FileConfigurationParameter;
            if (parameter == null)
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionUnexpectedType, new object[] { typeof(FileConfigurationParameter).Name }), "saveParameter");
            }
            this.Save(parameter.FileName, sectionName, configurationSection);
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            implementation.AddSectionChangeHandler(sectionName, handler);
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            return implementation.GetSection(sectionName);
        }

        public void Remove(IConfigurationParameter removeParameter, string sectionName)
        {
            FileConfigurationParameter parameter = removeParameter as FileConfigurationParameter;
            if (parameter == null)
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionUnexpectedType, new object[] { typeof(FileConfigurationParameter).Name }), "saveParameter");
            }
            this.Remove(parameter.FileName, sectionName);
        }

        public void Remove(string fileName, string section)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "fileName");
            }
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "section");
            }
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {
                ExeConfigFilename = fileName
            };
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            configuration.Sections.Remove(section);
            configuration.Save();
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            implementation.RemoveSectionChangeHandler(sectionName, handler);
        }

        internal static void ResetImplementation(bool refreshing)
        {
            SystemConfigurationSourceImplementation implementation = SystemConfigurationSource.implementation;
            SystemConfigurationSource.implementation = new SystemConfigurationSourceImplementation(refreshing);
            implementation.Dispose();
        }

        public void Save(string fileName, string section, ConfigurationSection configurationSection)
        {
            ValidateArgumentsAndFileExists(fileName, section, configurationSection);
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {
                ExeConfigFilename = fileName
            };
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            configuration.Sections.Remove(section);
            configuration.Sections.Add(section, configurationSection);
            configuration.Save();
        }

        private static void ValidateArgumentsAndFileExists(string fileName, string section, ConfigurationSection configurationSection)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "fileName");
            }
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "section");
            }
            if (configurationSection == null)
            {
                throw new ArgumentNullException("configurationSection");
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format(Resources.ExceptionConfigurationFileNotFound, section), fileName);
            }
        }

        internal static BaseFileConfigurationSourceImplementation Implementation
        {
            get
            {
                return implementation;
            }
        }
    }
}

