//-----------------------------------------------------------------------
// <copyright file="SoapExecuteInput.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Soap
{
    public class SoapExecuteInput
    {
        /// <summary>
        /// Endpoint.
        /// </summary>
        public string Uri { get; }

        /// <summary>
        /// Timeout.
        /// </summary>
        public int Timeout { get; }

        /// <summary>
        /// Initializes a new SoapExecuteInput instance.
        /// </summary>
        /// <param name="uri">The endpoint.</param>
        /// <param name="timeout">The timeout</param>
        public SoapExecuteInput(string uri, int timeout)
        {
            Uri = uri;
            Timeout = timeout;
        }
    }

    public class SoapExecuteInput<T> : SoapExecuteInput
    {
        /// <summary>
        /// The SOAP request object.
        /// </summary>
        public T RequestObject { get; }

        public SoapExecuteInput(SoapExecuteInput input, T requestObject) : base(input.Uri, input.Timeout)
        {
            RequestObject = requestObject;
        }
    }
}
