namespace Maticsoft.Email.EmailJob
{
    using System;
    using System.Xml;

    public interface IJob
    {
        void Execute(XmlNode node);
    }
}

