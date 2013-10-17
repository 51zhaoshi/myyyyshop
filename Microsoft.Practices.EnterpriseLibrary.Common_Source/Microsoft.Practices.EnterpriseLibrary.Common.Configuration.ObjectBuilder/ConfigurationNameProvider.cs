namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using System;

    public static class ConfigurationNameProvider
    {
        private const string nameSuffix = "___";

        public static bool IsMadeUpName(string name)
        {
            if (name == null)
            {
                return false;
            }
            return name.EndsWith("___");
        }

        public static string MakeUpName()
        {
            return (Guid.NewGuid().ToString() + "___");
        }
    }
}

