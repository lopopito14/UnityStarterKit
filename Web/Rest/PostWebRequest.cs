//-----------------------------------------------------------------------
// <copyright file="PostWebRequest.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Web.Rest
{
    using System.Threading.Tasks;
    using UnityEngine.Networking;

    public class PostWebRequest<_IN_, _OUT_> : WebRequest<_IN_, _OUT_> where _IN_ : class where _OUT_ : class
    {
        private PostWebRequest(string uri) : base(uri, UnityWebRequest.kHttpVerbPOST) { }

        private async Task<_OUT_> Execute(_IN_ input)
        {
            return await _Execute_(input);
        }

        public static Task<OUT> Execute<IN, OUT>(string uri, IN input) where IN : class where OUT : class
        {
            return new PostWebRequest<IN, OUT>(uri).Execute(input);
        }
    }
}
