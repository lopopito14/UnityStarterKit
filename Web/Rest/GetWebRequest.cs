//-----------------------------------------------------------------------
// <copyright file="GetWebRequest.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Rest
{
    using System.Threading.Tasks;
    using UnityEngine.Networking;

    public class GetWebRequest<_OUT_> : WebRequest<object, _OUT_> where _OUT_ : class
    {
        private GetWebRequest(string uri) : base(uri, UnityWebRequest.kHttpVerbGET) { }

        private async Task<_OUT_> Execute()
        {
            return await _Execute_();
        }

        public static Task<OUT> Execute<OUT>(string uri) where OUT : class
        {
            return new GetWebRequest<OUT>(uri).Execute();
        }
    }
}
