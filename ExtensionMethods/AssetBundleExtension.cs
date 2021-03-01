//-----------------------------------------------------------------------
// <copyright file="AssetBundleExtension.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.ExtensionMethods
{
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class AssetBundleExtension
    {
        public static T LoadAsset_<T>(this AssetBundle assetBundle, string assetName) where T : Object
        {
            string[] assetPaths = assetBundle.GetAllAssetNames();

            string assetPath = assetPaths.FirstOrDefault(a => a.EndsWith(assetName, System.StringComparison.OrdinalIgnoreCase));
            if (assetPath == null)
            {
                return default;
            }

            return assetBundle.LoadAsset<T>(assetPath);
        }

        public static AsyncOperation LoadSceneAsync(this AssetBundle assetBundle, string sceneName)
        {
            string[] scenePaths = assetBundle.GetAllScenePaths();

            string scenePath = scenePaths.FirstOrDefault(a => a.EndsWith($"{sceneName}.unity", System.StringComparison.OrdinalIgnoreCase));
            if (scenePath != null)
            {
                return SceneManager.LoadSceneAsync(scenePath);
            }

            return null;
        }
    }
}
