//-----------------------------------------------------------------------
// <copyright file="DeleteWebRequest.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Rest
{
    using System.Threading.Tasks;
    using UnityEngine.Networking;

    public class DeleteWebRequest<_OUT_> : WebRequest<object, _OUT_> where _OUT_ : class
    {
        private DeleteWebRequest(string uri) : base(uri, UnityWebRequest.kHttpVerbDELETE) { }

        private async Task<_OUT_> Execute()
        {
            return await _Execute_();
        }

        public static Task<OUT> Execute<OUT>(string uri) where OUT : class
        {
            return new DeleteWebRequest<OUT>(uri).Execute();
        }
    }
}
