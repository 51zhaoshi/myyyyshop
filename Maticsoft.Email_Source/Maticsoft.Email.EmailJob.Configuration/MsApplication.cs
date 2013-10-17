namespace Maticsoft.Email.EmailJob.Configuration
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    internal class MsApplication
    {
        private Maticsoft.Email.EmailJob.Configuration.ApplicationType _appType = Maticsoft.Email.EmailJob.Configuration.ApplicationType.All;
        private string _name;
        private Regex _regex;

        internal MsApplication(string pattern, string name, Maticsoft.Email.EmailJob.Configuration.ApplicationType appType)
        {
            this._name = name.ToLower(CultureInfo.InvariantCulture);
            this._appType = appType;
            this._regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public bool IsMatch(string url)
        {
            return this._regex.IsMatch(url);
        }

        public Maticsoft.Email.EmailJob.Configuration.ApplicationType ApplicationType
        {
            get
            {
                return this._appType;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}

