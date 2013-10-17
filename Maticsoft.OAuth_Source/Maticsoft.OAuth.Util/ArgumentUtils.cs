namespace Maticsoft.OAuth.Util
{
    using System;
    using System.Globalization;

    public sealed class ArgumentUtils
    {
        public static void AssertHasText(string argument, string name)
        {
            if (!StringUtils.HasText(argument))
            {
                throw new ArgumentNullException(name, string.Format(CultureInfo.InvariantCulture, "String argument '{0}' must have text; it must not be null, empty, or blank.", new object[] { name }));
            }
        }

        public static void AssertNotNull(object argument, string name)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name, string.Format(CultureInfo.InvariantCulture, "Argument '{0}' must not be null.", new object[] { name }));
            }
        }
    }
}

