namespace Maticsoft.OAuth.IO
{
    using System;
    using System.IO;

    public abstract class AbstractResource : IResource
    {
        protected AbstractResource()
        {
        }

        protected static string GetResourceNameWithoutProtocol(string resourceName)
        {
            int index = resourceName.IndexOf(System.Uri.SchemeDelimiter);
            if (index == -1)
            {
                return resourceName;
            }
            return resourceName.Substring(index + System.Uri.SchemeDelimiter.Length);
        }

        public abstract Stream GetStream();

        public virtual bool IsOpen
        {
            get
            {
                return false;
            }
        }

        public abstract System.Uri Uri { get; }
    }
}

