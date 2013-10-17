namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;

    [ConfigurationElementType(typeof(FileConfigurationSourceElement))]
    public class FileConfigurationSource : IConfigurationSource, IProtectedConfigurationSource
    {
        private string configurationFilepath;
        private static Dictionary<string, FileConfigurationSourceImplementation> implementationByFilepath = new Dictionary<string, FileConfigurationSourceImplementation>(StringComparer.OrdinalIgnoreCase);
        private static object lockObject = new object();

        public FileConfigurationSource(string configurationFilepath)
        {
            if (string.IsNullOrEmpty(configurationFilepath))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "configurationFilepath");
            }
            this.configurationFilepath = RootConfigurationFilePath(configurationFilepath);
            if (!File.Exists(this.configurationFilepath))
            {
                throw new FileNotFoundException(string.Format(Resources.Culture, Resources.ExceptionConfigurationLoadFileNotFound, new object[] { this.configurationFilepath }));
            }
            EnsureImplementation(this.configurationFilepath);
        }

        public void Add(IConfigurationParameter saveParameter, string sectionName, ConfigurationSection configurationSection)
        {
            FileConfigurationParameter parameter = saveParameter as FileConfigurationParameter;
            if (parameter == null)
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionUnexpectedType, new object[] { typeof(FileConfigurationParameter).Name }), "saveParameter");
            }
            this.Save(parameter.FileName, sectionName, configurationSection);
        }

        public void Add(IConfigurationParameter saveParameter, string sectionName, ConfigurationSection configurationSection, string protectionProviderName)
        {
            FileConfigurationParameter parameter = saveParameter as FileConfigurationParameter;
            if (parameter == null)
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionUnexpectedType, new object[] { typeof(FileConfigurationParameter).Name }), "saveParameter");
            }
            this.Save(parameter.FileName, sectionName, configurationSection, protectionProviderName);
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            implementationByFilepath[this.configurationFilepath].AddSectionChangeHandler(sectionName, handler);
        }

        private static void EnsureImplementation(string rootedConfigurationFile)
        {
            if (!implementationByFilepath.ContainsKey(rootedConfigurationFile))
            {
                lock (lockObject)
                {
                    if (!implementationByFilepath.ContainsKey(rootedConfigurationFile))
                    {
                        FileConfigurationSourceImplementation implementation = new FileConfigurationSourceImplementation(rootedConfigurationFile);
                        implementationByFilepath.Add(rootedConfigurationFile, implementation);
                    }
                }
            }
        }

        internal static BaseFileConfigurationSourceImplementation GetImplementation(string configurationFilepath)
        {
            string rootedConfigurationFile = RootConfigurationFilePath(configurationFilepath);
            EnsureImplementation(rootedConfigurationFile);
            return implementationByFilepath[rootedConfigurationFile];
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            return implementationByFilepath[this.configurationFilepath].GetSection(sectionName);
        }

        private void InternalSave(string fileName, string section, ConfigurationSection configurationSection, string protectionProvider)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {
                ExeConfigFilename = fileName
            };
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            if (typeof(ConnectionStringsSection) == configurationSection.GetType())
            {
                config.Sections.Remove(section);
                this.UpdateConnectionStrings(section, configurationSection, config, protectionProvider);
            }
            else if (typeof(AppSettingsSection) == configurationSection.GetType())
            {
                this.UpdateApplicationSettings(section, configurationSection, config, protectionProvider);
            }
            else
            {
                config.Sections.Remove(section);
                config.Sections.Add(section, configurationSection);
                ProtectConfigurationSection(configurationSection, protectionProvider);
            }
            config.Save();
            UpdateImplementation(fileName);
        }

        private static void ProtectConfigurationSection(ConfigurationSection configurationSection, string protectionProvider)
        {
            if (!string.IsNullOrEmpty(protectionProvider))
            {
                if ((configurationSection.SectionInformation.ProtectionProvider == null) || (configurationSection.SectionInformation.ProtectionProvider.Name != protectionProvider))
                {
                    configurationSection.SectionInformation.ProtectSection(protectionProvider);
                }
            }
            else if (configurationSection.SectionInformation.ProtectionProvider != null)
            {
                configurationSection.SectionInformation.UnprotectSection();
            }
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
            if (configuration.Sections.Get(section) != null)
            {
                configuration.Sections.Remove(section);
                configuration.Save();
                UpdateImplementation(fileName);
            }
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            implementationByFilepath[this.configurationFilepath].RemoveSectionChangeHandler(sectionName, handler);
        }

        public static void ResetImplementation(string configurationFilepath, bool refreshing)
        {
            string key = RootConfigurationFilePath(configurationFilepath);
            FileConfigurationSourceImplementation implementation = null;
            implementationByFilepath.TryGetValue(key, out implementation);
            implementationByFilepath[key] = new FileConfigurationSourceImplementation(key, refreshing);
            if (implementation != null)
            {
                implementation.Dispose();
            }
        }

        private static string RootConfigurationFilePath(string configurationFile)
        {
            string path = (string) configurationFile.Clone();
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
            return path;
        }

        public void Save(string fileName, string section, ConfigurationSection configurationSection)
        {
            ValidateArgumentsAndFileExists(fileName, section, configurationSection);
            this.InternalSave(fileName, section, configurationSection, string.Empty);
        }

        public void Save(string fileName, string section, ConfigurationSection configurationSection, string protectionProvider)
        {
            ValidateArgumentsAndFileExists(fileName, section, configurationSection);
            if (string.IsNullOrEmpty(protectionProvider))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "protectionProvider");
            }
            this.InternalSave(fileName, section, configurationSection, protectionProvider);
        }

        private void UpdateApplicationSettings(string section, ConfigurationSection configurationSection, System.Configuration.Configuration config, string protectionProvider)
        {
            AppSettingsSection appSettings = config.AppSettings;
            if (appSettings == null)
            {
                config.Sections.Add(section, configurationSection);
                ProtectConfigurationSection(configurationSection, protectionProvider);
            }
            else
            {
                AppSettingsSection section3 = configurationSection as AppSettingsSection;
                if (appSettings.File != section3.File)
                {
                    appSettings.File = section3.File;
                }
                List<string> list = new List<string>(section3.Settings.AllKeys);
                List<string> list2 = new List<string>(appSettings.Settings.AllKeys);
                foreach (string str in list2)
                {
                    if (!list.Contains(str))
                    {
                        appSettings.Settings.Remove(str);
                    }
                }
                foreach (string str2 in list)
                {
                    if (!list2.Contains(str2))
                    {
                        appSettings.Settings.Add(str2, section3.Settings[str2].Value);
                    }
                    else if (appSettings.Settings[str2].Value != section3.Settings[str2].Value)
                    {
                        appSettings.Settings[str2].Value = section3.Settings[str2].Value;
                    }
                }
                ProtectConfigurationSection(appSettings, protectionProvider);
            }
        }

        private void UpdateConnectionStrings(string section, ConfigurationSection configurationSection, System.Configuration.Configuration config, string protectionProvider)
        {
            ConnectionStringsSection connectionStrings = config.ConnectionStrings;
            if (connectionStrings == null)
            {
                config.Sections.Add(section, configurationSection);
                ProtectConfigurationSection(configurationSection, protectionProvider);
            }
            else
            {
                ConnectionStringsSection section3 = (ConnectionStringsSection) configurationSection;
                foreach (ConnectionStringSettings settings in section3.ConnectionStrings)
                {
                    if (connectionStrings.ConnectionStrings[settings.Name] == null)
                    {
                        connectionStrings.ConnectionStrings.Add(settings);
                    }
                }
                ProtectConfigurationSection(connectionStrings, protectionProvider);
            }
        }

        private static void UpdateImplementation(string fileName)
        {
            FileConfigurationSourceImplementation implementation;
            implementationByFilepath.TryGetValue(fileName, out implementation);
            if (implementation != null)
            {
                implementation.UpdateCache();
            }
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
                throw new FileNotFoundException(string.Format(Resources.Culture, Resources.ExceptionConfigurationFileNotFound, new object[] { section }), fileName);
            }
        }

        internal BaseFileConfigurationSourceImplementation Implementation
        {
            get
            {
                return implementationByFilepath[this.configurationFilepath];
            }
        }
    }
}

