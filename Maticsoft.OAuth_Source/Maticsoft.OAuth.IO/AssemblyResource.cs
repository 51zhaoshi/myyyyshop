namespace Maticsoft.OAuth.IO
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    public class AssemblyResource : AbstractResource
    {
        private Assembly assembly;
        private string resourceName;
        private System.Uri resourceUri;

        public AssemblyResource(string resourceName)
        {
            ArgumentUtils.AssertHasText(resourceName, "resourceName");
            string[] strArray = AbstractResource.GetResourceNameWithoutProtocol(resourceName).Split(new char[] { '/' });
            if (strArray.Length != 3)
            {
                throw new UriFormatException(string.Format("Invalid resource name. Name has to be in 'assembly://<assemblyName>/<namespace>/<resourceName>' format.", resourceName));
            }
            Assembly assembly = Assembly.Load(strArray[0]);
            if (assembly == null)
            {
                throw new FileNotFoundException(string.Format("Unable to load assembly [{0}].", strArray[0]));
            }
            this.Initialize(strArray[2], strArray[1], assembly);
        }

        public AssemblyResource(string fileName, Type type)
        {
            ArgumentUtils.AssertHasText(fileName, "resourceName");
            ArgumentUtils.AssertNotNull(type, "type");
            this.Initialize(fileName, type.Namespace, type.Assembly);
        }

        public override Stream GetStream()
        {
            Stream manifestResourceStream = this.assembly.GetManifestResourceStream(this.resourceName);
            if (manifestResourceStream == null)
            {
                throw new FileNotFoundException(string.Format("Could not load resource [{0}] from assembly [{1}]. Maticsoft.OAuth.NET URI syntax is 'assembly://MyAssembly/MyNamespace/MyResource.ext'", this.resourceName, this.assembly));
            }
            return manifestResourceStream;
        }

        protected void Initialize(string fileName, string ns, Assembly assembly)
        {
            this.resourceName = ns + "." + fileName;
            this.assembly = assembly;
            string str = assembly.FullName.Split(new char[] { ',' })[0];
            this.resourceUri = new System.Uri(string.Format("assembly://{0}/{1}/{2}", str, ns, fileName));
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Resource [{0}] from assembly [{1}]", new object[] { this.resourceName, this.assembly.FullName });
        }

        public override System.Uri Uri
        {
            get
            {
                return this.resourceUri;
            }
        }
    }
}

