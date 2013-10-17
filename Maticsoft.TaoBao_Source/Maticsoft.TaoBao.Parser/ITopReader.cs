namespace Maticsoft.TaoBao.Parser
{
    using System;
    using System.Collections;

    public interface ITopReader
    {
        IList GetListObjects(string listName, string itemName, Type type, DTopConvert convert);
        object GetPrimitiveObject(object name);
        object GetReferenceObject(object name, Type type, DTopConvert convert);
        bool HasReturnField(object name);
    }
}

