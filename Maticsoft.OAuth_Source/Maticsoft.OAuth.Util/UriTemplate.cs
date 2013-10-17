namespace Maticsoft.OAuth.Util
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class UriTemplate
    {
        private const string BRACE_LEFT = "{";
        private const string BRACE_RIGHT = "}";
        private Regex matchRegex;
        private string uriTemplate;
        private string[] variableNames;
        private static Regex VARIABLENAMES_REGEX = new Regex(@"\{([^/]+?)\}", RegexOptions.Compiled);
        private static string VARIABLEVALUE_PATTERN = "(?<{0}>.*)";

        public UriTemplate(string uriTemplate)
        {
            this.uriTemplate = uriTemplate;
            Parser parser = new Parser(uriTemplate);
            this.variableNames = parser.GetVariableNames();
            this.matchRegex = parser.GetMatchRegex();
        }

        public Uri Expand(IDictionary<string, object> uriVariables)
        {
            if (uriVariables.Count != this.variableNames.Length)
            {
                throw new ArgumentException(string.Format("Invalid amount of variables values in '{0}': expected {1}; got {2}", this.uriTemplate, this.variableNames.Length, uriVariables.Count));
            }
            string uriTemplate = this.uriTemplate;
            foreach (string str2 in this.variableNames)
            {
                if (!uriVariables.ContainsKey(str2))
                {
                    throw new ArgumentException(string.Format("'uriVariables' dictionary has no value for '{0}'", str2));
                }
                uriTemplate = Replace(uriTemplate, str2, uriVariables[str2]);
            }
            return new Uri(uriTemplate, UriKind.RelativeOrAbsolute);
        }

        public Uri Expand(params object[] uriVariableValues)
        {
            if (uriVariableValues.Length != this.variableNames.Length)
            {
                throw new ArgumentException(string.Format("Invalid amount of variables values in '{0}': expected {1}; got {2}", this.uriTemplate, this.variableNames.Length, uriVariableValues.Length));
            }
            string uriTemplate = this.uriTemplate;
            for (int i = 0; i < this.variableNames.Length; i++)
            {
                uriTemplate = Replace(uriTemplate, this.variableNames[i], uriVariableValues[i]);
            }
            return new Uri(uriTemplate, UriKind.RelativeOrAbsolute);
        }

        public IDictionary<string, string> Match(string uri)
        {
            ArgumentUtils.AssertNotNull(uri, "uri");
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            System.Text.RegularExpressions.Match match = this.matchRegex.Match(uri);
            for (int i = 1; i < match.Groups.Count; i++)
            {
                dictionary.Add(this.matchRegex.GroupNameFromNumber(i), match.Groups[i].Value);
            }
            return dictionary;
        }

        public bool Matches(string uri)
        {
            if (uri == null)
            {
                return false;
            }
            return this.matchRegex.IsMatch(uri);
        }

        private static string Replace(string uriTemplate, string token, object value)
        {
            string oldValue = "{" + token + "}";
            return uriTemplate.Replace(oldValue, (value == null) ? string.Empty : value.ToString());
        }

        public override string ToString()
        {
            return this.uriTemplate;
        }

        public string[] VariableNames
        {
            get
            {
                return this.variableNames;
            }
        }

        private class Parser
        {
            private StringBuilder patternBuilder = new StringBuilder();
            private List<string> variableNames = new List<string>();

            public Parser(string uriTemplate)
            {
                ArgumentUtils.AssertNotNull(uriTemplate, "uriTemplate");
                int start = 0;
                this.patternBuilder.Append("^");
                foreach (Match match in Maticsoft.OAuth.Util.UriTemplate.VARIABLENAMES_REGEX.Matches(uriTemplate))
                {
                    string item = match.Groups[1].Value;
                    if (!this.variableNames.Contains(item))
                    {
                        this.variableNames.Add(item);
                    }
                    this.patternBuilder.Append(Escape(uriTemplate, start, match.Index - start));
                    this.patternBuilder.Append(string.Format(Maticsoft.OAuth.Util.UriTemplate.VARIABLEVALUE_PATTERN, item));
                    start = match.Index + match.Length;
                }
                this.patternBuilder.Append(Escape(uriTemplate, start, uriTemplate.Length - start));
                this.patternBuilder.Append("$");
            }

            private static string Escape(string fullPath, int start, int end)
            {
                if (start == end)
                {
                    return "";
                }
                return Regex.Escape(fullPath.Substring(start, end));
            }

            public Regex GetMatchRegex()
            {
                return new Regex(this.patternBuilder.ToString());
            }

            public string[] GetVariableNames()
            {
                return this.variableNames.ToArray();
            }
        }
    }
}

