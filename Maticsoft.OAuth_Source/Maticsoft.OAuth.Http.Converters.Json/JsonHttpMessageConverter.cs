namespace Maticsoft.OAuth.Http.Converters.Json
{
    using System;

    [Obsolete("This class has been renamed to DataContractJsonHttpMessageConverter for better consistency.")]
    public class JsonHttpMessageConverter : DataContractJsonHttpMessageConverter
    {
        public JsonHttpMessageConverter()
        {
        }

        public JsonHttpMessageConverter(bool requiresAttribute) : base(requiresAttribute)
        {
        }
    }
}

