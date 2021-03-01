//-----------------------------------------------------------------------
// <copyright file="AssetBundleManager.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.AssetBundles
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Instance of the <see cref="AssetBundleManager"/> class.
    /// </summary>
    public class AssetBundleManager
    {
        private static object _lock = new object();

        private static AssetBundleManager instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static AssetBundleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        instance = new AssetBundleManager();
                    }
                }

                return instance;
            }
        }

        private readonly Dictionary<string, AssetBundle> assetBundles;

        /// <summary>
        /// Prevents a default instance of the <see cref="AssetBundleManager"/> class from being created.
        /// </summary>
        private AssetBundleManager()
        {
            assetBundles = new Dictionary<string, AssetBundle>();
        }

        /// <summary>
        /// Gets the asset bundle.
        /// </summary>
        /// <param name="assetBundleName">Name of the asset bundle.</param>
        /// <returns></returns>
        public AssetBundle GetAssetBundle(string assetBundleName)
        {
            if (assetBundles.TryGetValue(assetBundleName, out AssetBundle assetBundle))
            {
                return assetBundle;
            }

            return null;
        }

        /// <summary>
        /// Stores the asset bundle.
        /// </summary>
        /// <param name="assetBundle">The asset bundle.</param>
        public void StoreAssetBundle(AssetBundle assetBundle)
        {
            if (!assetBundles.ContainsKey(assetBundle.name))
            {
                assetBundles.Add(assetBundle.name, assetBundle);
            }
        }
    }
}
