//-----------------------------------------------------------------------
// <copyright file="JsonExtensions.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.ExtensionMethods
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class JsonExtensions
    {
        public static T GetJson<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static IEnumerable<T> GetJsonArray<T>(this string json)
        {
            string newJson = "{ \"Array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Array;
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonUtility.ToJson(obj);
        }

        [Serializable]
        private class Wrapper<T>
        {
#pragma warning disable 0649
            public T[] Array;
#pragma warning restore 0649
        }
    }

}
