//-----------------------------------------------------------------------
// <copyright file="PatchWebRequest.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Rest
{
    using System.Threading.Tasks;
    using UnityEngine.Networking;

    public class PatchWebRequest<_IN_, _OUT_> : WebRequest<_IN_, _OUT_> where _IN_ : class where _OUT_ : class
    {
        private PatchWebRequest(string uri) : base(uri, UnityWebRequest.kHttpVerbPOST) { }

        private async Task<_OUT_> Execute(_IN_ input)
        {
            return await _Execute_(input);
        }

        public static Task<OUT> Execute<IN, OUT>(string uri, IN input) where IN : class where OUT : class
        {
            return new PatchWebRequest<IN, OUT>(uri).Execute(input);
        }
    }
}
