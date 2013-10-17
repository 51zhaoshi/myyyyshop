namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class KeyValuePairParser
    {
        private static Regex KeyValueEntryRegex = new Regex("\r\n\t\t\t\t\t\t(?<name>[^;=]+)= \t\t# match the value name - anything but ; or =, followed by =\r\n\t\t\t\t\t\t(?<value>.*?)\t\t\t# followed by anything as the value, but non greedy\r\n\t\t\t\t\t\t(\t\t\t\t\t\t# until either\r\n\t\t\t\t\t\t\t$\t\t\t\t\t# the string ends\r\n\t\t\t\t\t\t\t\t|\t\t\t\t# or\r\n\t\t\t\t\t\t\t(?<!;);$\t\t\t# the string ends after a non escaped ;\r\n\t\t\t\t\t\t\t\t|\t\t\t\t# or\r\n\t\t\t\t\t\t\t(?<!;);(?!;)) \t\t# a ; that is not before or after another ; (;;) is the escaped ;", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        public static void ExtractKeyValueEntries(string attributes, IDictionary<string, string> attributesDictionary)
        {
            foreach (Match match in KeyValueEntryRegex.Matches(attributes))
            {
                attributesDictionary.Add(match.Groups["name"].Value.Trim(), match.Groups["value"].Value.Replace(";;", ";"));
            }
        }
    }
}

