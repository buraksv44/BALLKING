using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    [UnityEditor.CustomPropertyDrawer(typeof(LanguagePairs))]
    internal class LanguagePairsPropertyDrawer : PropertyDrawer
    {
        private static Vector2 m_LangScrollViewPosition = Vector2.zero;

        private static string FormatLanguageString(string language) { return language.Replace("_", "-"); }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var keys = property.FindPropertyRelative("keys");
            var values = property.FindPropertyRelative("values");

            EditorGUI.BeginChangeCheck();

            int count = Mathf.Min(keys.arraySize, values.arraySize);

            m_LangScrollViewPosition = GUILayout.BeginScrollView(m_LangScrollViewPosition, EditorStyles.helpBox, GUILayout.MinHeight(150));

            int layoutLoop = 0;
            int rowCount = count / 2;

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            for (int i = 0; i < count; i++)
            {
                if (layoutLoop - 1 == rowCount)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical();
                }

                var keyName = keys.GetArrayElementAtIndex(i).stringValue;
                bool previousState = values.GetArrayElementAtIndex(i).boolValue;
                bool newState = GUILayout.Toggle(
                    previousState,
                    FormatLanguageString(
                        keyName),
                    GUILayout.MinWidth(160));

                values.GetArrayElementAtIndex(i).boolValue = newState;

                if (previousState != newState)
                {
                    //dirty = true;
/*
                    if (gameConfigAsset.HasLanguagesSet(xmlGameConfig) == false)
                    {
                        if (EditorUtility.DisplayDialog("Localization", "Removing all languages might break the build if the MicrosoftGame.Config still references localized resources. Would you like to reset the ShellVisuals section?", "Yes", "No") == true)
                        {
                            //settings.InitialiseGameConfigShellVisuals();
                        }
                    }
*/
                }

                layoutLoop++;
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();

            if (EditorGUI.EndChangeCheck())
            {
                //serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.EndProperty();
        }
    }
}