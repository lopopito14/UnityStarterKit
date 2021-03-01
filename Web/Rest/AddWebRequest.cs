//-----------------------------------------------------------------------
// <copyright file="AddWebRequest.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Rest
{
    using System.Threading.Tasks;
    using UnityEngine.Networking;

    public class AddWebRequest<_IN_, _OUT_> : WebRequest<_IN_, _OUT_> where _IN_ : class where _OUT_ : class
    {
        private AddWebRequest(string uri) : base(uri, UnityWebRequest.kHttpVerbCREATE) { }

        private async Task<_OUT_> Execute(_IN_ input)
        {
            return await _Execute_(input);
        }

        public static Task<OUT> Execute<IN, OUT>(string uri, IN input) where IN : class where OUT : class
        {
            return new AddWebRequest<IN, OUT>(uri).Execute(input);
        }
    }
}
