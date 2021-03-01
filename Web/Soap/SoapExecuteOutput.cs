//-----------------------------------------------------------------------
// <copyright file="SoapExecuteOutput.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Soap
{
    public class SoapExecuteOutput
    {
        /// <summary>
        /// The response time.
        /// </summary>
        public long ResponseTime { get; }

        /// <summary>
        /// Initializes a new SoapExecuteOutput instance.
        /// </summary>
        /// <param name="responseTime">The response time.</param>
        public SoapExecuteOutput(long responseTime)
        {
            ResponseTime = responseTime;
        }
    }

    public class SoapExecuteOutput<T> : SoapExecuteOutput
    {
        /// <summary>
        /// The SOAP response object.
        /// </summary>
        public T ResponseObject { get; }

        /// <summary>
        /// Initializes a new SoapExecuteOutput instance.
        /// </summary>
        /// <param name="responseTime">The response time.</param>
        /// <param name="responseObject">The SOAP response object</param>
        public SoapExecuteOutput(long responseTime, T responseObject) : base(responseTime)
        {
            ResponseObject = responseObject;
        }
    }
}
