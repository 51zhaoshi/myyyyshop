namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.Collections.Generic;

    public class AcceptHeaderRequestCallback : IRequestCallback
    {
        protected IList<IHttpMessageConverter> messageConverters;
        protected Type responseType;

        public AcceptHeaderRequestCallback(Type responseType, IList<IHttpMessageConverter> messageConverters)
        {
            this.responseType = responseType;
            this.messageConverters = messageConverters;
        }

        public virtual void DoWithRequest(IClientHttpRequest request)
        {
            if (this.responseType != null)
            {
                List<MediaType> mediaTypes = new List<MediaType>();
                foreach (IHttpMessageConverter converter in this.messageConverters)
                {
                    if (converter.CanRead(this.responseType, null))
                    {
                        foreach (MediaType type in converter.SupportedMediaTypes)
                        {
                            if (type.CharSet != null)
                            {
                                mediaTypes.Add(new MediaType(type.Type, type.Subtype));
                            }
                            else
                            {
                                mediaTypes.Add(type);
                            }
                        }
                        continue;
                    }
                }
                if (mediaTypes.Count > 0)
                {
                    MediaType.SortBySpecificity(mediaTypes);
                    request.Headers.Accept = mediaTypes.ToArray();
                }
            }
        }
    }
}

