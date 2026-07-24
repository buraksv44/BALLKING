using System.Collections.Generic;
using System.Linq;
using Unity.Microsoft.GDK.Discovery;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class GdkEditionsSelector
    {
        private int[] m_DropDownEditionsList;
        private GUIContent[] m_DropDownEditionsStringsList;

        private bool BuildEditionLists(int trackedEdition, string editionNotSpecifiedString = "Not Tracked")
        {
            var gdkList = new List<int>();
            var gdkStringList = new List<GUIContent>();

            // Add all installed GDK versions
            var installedGdKs = GdkEnumerator.DiscoveredGdks.OrderByDescending(data => data.Edition).ToArray();
            foreach (var gdk in installedGdKs)
            {
                gdkList.Add(gdk.Edition);
                gdkStringList.Add(new GUIContent(GdkVersion.Format(gdk)));
            }

            // Add the user overridden version to the list if is not installed
            bool trackedEditionInstalled = true;
            if (trackedEdition!=-1 && !gdkList.Contains(trackedEdition))
            {
                trackedEditionInstalled = false;
                gdkList.Add(trackedEdition);
                gdkStringList.Add(new GUIContent(GdkVersion.Format(trackedEdition)));
            }

            // Sort both lists together using indices
            var indices = Enumerable.Range(0, gdkList.Count).ToList();
            indices.Sort((a, b) => gdkList[b].CompareTo(gdkList[a]));

            gdkList = indices.Select(i => gdkList[i]).ToList();
            gdkStringList = indices.Select(i => gdkStringList[i]).ToList();

            // Add an option for no override
            gdkList.Insert(0, -1);
            gdkStringList.Insert(0, new GUIContent(editionNotSpecifiedString));

            m_DropDownEditionsList = gdkList.ToArray();
            m_DropDownEditionsStringsList = gdkStringList.ToArray();

            return trackedEditionInstalled;
        }

        private static T BuildEnumPopup<T>(GUIContent uiString, T selected, T[] options, GUIContent[] optionNames)
        {
            // Display dropdown
            int idx = 0; // pick the first property when not found
            for (int i = 1; i < options.Length; ++i)
            {
                if (selected.Equals(options[i]))
                {
                    idx = i;
                    break;
                }
            }

            int newIdx = EditorGUILayout.Popup(uiString, idx, optionNames);

            return options[newIdx];
        }

        public void Draw(BuildTargetGroup buildTargetGroup)
        {
            var currentEdition = GDKPlatformManager.BuildTargetGroupGetSelectedGdkEdition(buildTargetGroup);
            if (buildTargetGroup != BuildTargetGroup.Standalone && currentEdition < 0)
            {
                return;
            }

            if (buildTargetGroup == BuildTargetGroup.Standalone && GdkUtility.IsMicrosoftGdkApiPackageInstalled() == false)
            {
                return;
            }

            var editionInstalled = BuildEditionLists(currentEdition,
                buildTargetGroup == BuildTargetGroup.Standalone ? "Not Tracked" : "No Override");

            using (new EditorGUI.DisabledScope(buildTargetGroup != BuildTargetGroup.Standalone))
            {
                EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

                var newGDKSelection = BuildEnumPopup(
                    UIContent.k_MicrosoftGdkVersion,
                    currentEdition,
                    m_DropDownEditionsList,
                    m_DropDownEditionsStringsList);

                if (newGDKSelection != currentEdition)
                {
                    GDKPlatformManager.BuildTargetGroupSetSelectedGdkEdition(buildTargetGroup, newGDKSelection);
                }
            }

            if (editionInstalled == false)
            {
                EditorGUILayout.HelpBox(
                    $"The selected GDK Edition, {GdkVersion.Format(currentEdition)}, is not found on the local machine.",
                    MessageType.Warning);
            }

            if (buildTargetGroup != BuildTargetGroup.Standalone)
            {
#if UNITY_6000_1_OR_NEWER
                EditorGUILayout.HelpBox(
                    $"Please use the player settings for this platform to configure the required Microsoft GDK edition.",
                    MessageType.Info);
#endif
            }
        }
    }
}