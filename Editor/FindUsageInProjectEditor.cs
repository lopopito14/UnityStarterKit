#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityStarterKit.Editor
{
    public class FindUsageInProjectEditor : EditorWindow
    {
        private const string MenuItemText = "Assets/Find References In Project";

        private static Dictionary<string, List<string>> referenceCache;
        private Object currentSelection;

        [MenuItem(MenuItemText)]
        public static void Init()
        {
            var window = GetWindow<FindUsageInProjectEditor>();
            window.Show();
            window.titleContent.text = "Find in project";
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Reload dependencies"))
            {
                referenceCache = null;
            }

            if (referenceCache == null)
            {
                referenceCache = new Dictionary<string, List<string>>();

                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                string[] guids = AssetDatabase.FindAssets("");
                foreach (string guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    string[] dependencies = AssetDatabase.GetDependencies(assetPath, false);

                    foreach (var dependency in dependencies)
                    {
                        if (referenceCache.ContainsKey(dependency))
                        {
                            if (!referenceCache[dependency].Contains(assetPath))
                            {
                                referenceCache[dependency].Add(assetPath);
                            }
                        }
                        else
                        {
                            referenceCache[dependency] = new List<string>() { assetPath };
                        }
                    }
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField(new GUIContent("Build index takes " + sw.ElapsedMilliseconds + " milliseconds"));
                EditorGUILayout.Space();
            }

            if (currentSelection != null)
            {
                string path = AssetDatabase.GetAssetPath(currentSelection);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Find: " + path);
                EditorGUILayout.Space();

                if (referenceCache != null)
                {
                    if (referenceCache.ContainsKey(path))
                    {
                        foreach (var reference in referenceCache[path])
                        {
                            EditorGUILayout.LabelField(reference);
                        }
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No references");
                    }
                }
            }
        }

        private void OnSelectionChange()
        {
            currentSelection = Selection.activeObject;
            Repaint();
        }

        private void OnEnable()
        {
            Repaint();
        }
    }
}

#endif