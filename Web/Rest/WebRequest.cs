//-----------------------------------------------------------------------
// <copyright file="WebRequest.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Rest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using UnityStarterKit.ExtensionMethods;

    public abstract class WebRequest<_IN_, _OUT_> where _IN_ : class where _OUT_ : class
    {
        private readonly string uri;
        private readonly string unityWebRequest;
        private readonly Dictionary<HttpRequestHeader, string> requestHeaders;

        protected WebRequest(string uri, string unityWebRequest)
        {
            this.uri = uri;
            this.unityWebRequest = unityWebRequest;

            this.requestHeaders = new Dictionary<HttpRequestHeader, string>();
            AddRequestHeader(HttpRequestHeader.ContentType, "text/json;charset=UTF-8");
        }

        public void AddRequestHeader(HttpRequestHeader key, string value)
        {
            requestHeaders.Add(key, value);
        }

        protected async Task<_OUT_> _Execute_(_IN_ input)
        {
            string restBody = SerializeObject<_IN_>(input);

            WebResponse webResponse = await RestRequestExecute(restBody);

            return DeserializeObject<_OUT_>(webResponse);
        }

        protected async Task<_OUT_> _Execute_()
        {
            WebResponse webResponse = await RestRequestExecute(null);

            return DeserializeObject<_OUT_>(webResponse);
        }

        private string SerializeObject<T>(T obj)
        {
            return obj.ToJson();
        }
        
        private async Task<WebResponse> RestRequestExecute(string restBody)
        {
            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.Method = unityWebRequest;
            request.Headers = new WebHeaderCollection();

            if (requestHeaders.Any())
            {
                foreach (var requestHeader in requestHeaders)
                {
                    request.Headers.Set(requestHeader.Key, requestHeader.Value);
                }
            }

            if (!string.IsNullOrEmpty(restBody))
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] bodyBytes = Encoding.UTF8.GetBytes(restBody);
                    requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                    requestStream.Close();
                }
            }

            // execute the WS call
            return await request.GetResponseAsync();
        }

        private T DeserializeObject<T>(WebResponse webResponse) where T : class
        {
            using (HttpWebResponse response = (HttpWebResponse)webResponse)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        return myStreamReader.ReadToEnd().GetJson<T>();
                    }
                }
            }
        }
    }
}
