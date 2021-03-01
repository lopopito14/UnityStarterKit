//-----------------------------------------------------------------------
// <copyright file="WebRequestExtensions.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.ExtensionMethods
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public static class WebRequestExtensions
    {
        public static async Task<WebResponse> GetResponseAsync(this WebRequest webRequest, int timeout)
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                using (cancellationTokenSource.Token.Register(() => webRequest.Abort(), useSynchronizationContext: false))
                {
                    try
                    {
                        cancellationTokenSource.CancelAfter(timeout);
                        return await webRequest.GetResponseAsync();
                    }
                    catch (WebException ex)
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(ex.Message, ex, cancellationTokenSource.Token);
                        }

                        throw;
                    }
                }
            }
        }
    }
}
