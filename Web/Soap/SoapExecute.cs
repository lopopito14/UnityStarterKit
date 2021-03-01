//-----------------------------------------------------------------------
// <copyright file="SoapExecute.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Soap
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using UnityStarterKit.ExtensionMethods;

    public abstract class SoapExecute
    {
        /// <summary>
        /// Returns the type of the web request envelope.
        /// </summary>
        protected abstract Type RequestEnvelopeType { get; }

        /// <summary>
        /// Returns the type of the web response envelope.
        /// </summary>
        protected abstract Type ResponseEnvelopeType { get; }

        /// <summary>
        /// Defines all namespace conversions of the web request.
        /// </summary>
        protected virtual IDictionnary<string, string> Namespaces => new Dictionnary<string, string>();

        /// <summary>
        /// Serializes a ISoapRequestEnvelope object in string.
        /// </summary>
        /// <param name="obj">The object to serialized.</param>
        /// <returns>The string serialization of the ISoapRequestEnvelope object.</returns>
        protected string SerializeObject(ISoapRequestEnvelope obj)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                foreach (var @namespace in Namespaces)
                {
                    namespaces.Add(@namespace.Key, @namespace.Value);
                }

                XmlSerializer serializer = new XmlSerializer(RequestEnvelopeType);
                serializer.Serialize(stringWriter, obj, namespaces);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Executes a SOAP web request.
        /// </summary>
        /// <param name="soapBody">The soap body in string format.</param>
        /// <returns>The webresponse task.</returns>
        protected async Task<WebResponse> SoapRequestExecute(SoapExecuteInput soapExecuteInput, string soapBody)
        {
            HttpWebRequest request = WebRequest.CreateHttp(soapExecuteInput.Uri);
            request.Method = "POST";
            request.ContentType = "text/xml;charset=UTF-8";
            using (Stream requestStream = request.GetRequestStream())
            {
                byte[] bodyBytes = Encoding.UTF8.GetBytes(soapBody);
                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Close();
            }

            return await request.GetResponseAsync(soapExecuteInput.Timeout);
        }

        /// <summary>
        /// Deserializes the WebResponse in an ISoapResponseEnvelope object.
        /// </summary>
        /// <param name="webResponse">The WebResponse.</param>
        /// <returns>The ISoapResponseEnvelope object.</returns>
        protected ISoapResponseEnvelope DeserializeObject(WebResponse webResponse)
        {
            using (HttpWebResponse response = (HttpWebResponse)webResponse)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        return new XmlSerializer(ResponseEnvelopeType).Deserialize(myStreamReader) as ISoapResponseEnvelope;
                    }
                }
            }
        }
    }

    public abstract class SoapExecute<_IN_, _OUT_> : SoapExecute
    {
        /// <summary>
        /// Entry point to execute a web request.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The SOAP web response.</returns>
        protected async Task<SoapExecuteOutput<_OUT_>> _Execute_(SoapExecuteInput<_IN_> input)
        {
            ISoapRequestEnvelope soapRequestEnvelope = BuildRequest(input.RequestObject);

            string soapBody = SerializeObject(soapRequestEnvelope);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            WebResponse webResponse = await SoapRequestExecute(input, soapBody);

            stopwatch.Stop();

            ISoapResponseEnvelope soapResponseEnvelope = DeserializeObject(webResponse);

            return new SoapExecuteOutput<_OUT_>(stopwatch.ElapsedMilliseconds, BuildResponse(soapResponseEnvelope));
        }

        /// <summary>
        /// Build the SOAP web request.
        /// </summary>
        /// <param name="input">The request.</param>
        /// <returns>The SOAP envelope request.</returns>
        protected abstract ISoapRequestEnvelope BuildRequest(_IN_ input);

        /// <summary>
        /// Format the response and return only the usefull content.
        /// </summary>
        /// <param name="soapResponseEnvelope">The soap response envelope.</param>
        /// <returns>The usefull content.</returns>
        protected abstract _OUT_ BuildResponse(ISoapResponseEnvelope soapResponseEnvelope);
    }

    public abstract class SoapExecute<_OUT_> : SoapExecute
    {
        /// <summary>
        /// Entry point to execute a web request.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The SOAP web response.</returns>
        protected async Task<SoapExecuteOutput<_OUT_>> _Execute_(SoapExecuteInput input)
        {
            ISoapRequestEnvelope soapRequestEnvelope = BuildRequest();

            string soapBody = SerializeObject(soapRequestEnvelope);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            WebResponse webResponse = await SoapRequestExecute(input, soapBody);

            stopwatch.Stop();

            ISoapResponseEnvelope soapResponseEnvelope = DeserializeObject(webResponse);

            return new SoapExecuteOutput<_OUT_>(stopwatch.ElapsedMilliseconds, BuildResponse(soapResponseEnvelope));
        }

        /// <summary>
        /// Build the SOAP web request.
        /// </summary>
        /// <param name="input">The request.</param>
        /// <returns>The SOAP envelope request.</returns>
        protected abstract ISoapRequestEnvelope BuildRequest();

        /// <summary>
        /// Format the response and return only the usefull content.
        /// </summary>
        /// <param name="soapResponseEnvelope">The soap response envelope.</param>
        /// <returns>The usefull content.</returns>
        protected abstract _OUT_ BuildResponse(ISoapResponseEnvelope soapResponseEnvelope);
    }
}
