using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools
{
    [System.Serializable]
    public class LanguagePairs : ISerializationCallbackReceiver
    {
        private Dictionary<string, bool> _dict = new();

        [SerializeField] private List<string> keys = new List<string>();
        [SerializeField] private List<bool> values = new List<bool>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in _dict)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            _dict.Clear();

            for (int i = 0; i < keys.Count; i++)
            {
                _dict[keys[i]] = values[i];
            }
        }

        public LanguagePairs()
        { Clear(); }

        public void Clear()
        {
            _dict.Clear();

            foreach (GameConfigLanguages lang in Enum.GetValues(typeof(GameConfigLanguages)))
                _dict[Enum.GetName(typeof(GameConfigLanguages), lang)] = false;
        }

        public void SetLanguagePair(GameConfigLanguages lang, bool isActive)
        { SetLanguagePair(EnumName(lang), isActive); }

        public void SetLanguagePair(string langName, bool isActive)
        {
            if (_dict.ContainsKey(langName))
            {
                _dict[langName] = isActive;
                return;
            }

            // Unknown language!
        }

        public bool IsLanguagePairSet(GameConfigLanguages lang)
        { return IsLanguagePairSet(EnumName(lang)); }

        public bool IsLanguagePairSet(string langName)
        {
            if (_dict.TryGetValue(langName, out var value))
                return value;

            return false;       // Unknown language!
        }

        public void ImportFromXml(Game xmlGameConfig)
        {
            Clear();

            if (xmlGameConfig.Resources != null)
            {
                foreach (var resource in xmlGameConfig.Resources)
                    SetLanguagePair(resource.Language.Replace("-", "_"), true);
            }
        }

        public void ExportToXml(Game xmlGameConfig)
        {
            CT_ResourcesResource[] newResources = new CT_ResourcesResource[Enum.GetValues(typeof(GameConfigLanguages)).Length];

            int appendCount = 0;
            foreach (GameConfigLanguages lang in Enum.GetValues(typeof(GameConfigLanguages)))
            {
                if (IsLanguagePairSet(lang))
                {
                    CT_ResourcesResource newResourceLang = new CT_ResourcesResource
                    {
                        Language = Enum.GetName(typeof(GameConfigLanguages), lang).Replace("_", "-")
                    };

                    newResources[appendCount] = newResourceLang;
                    appendCount++;
                }
            }

            xmlGameConfig.Resources = (appendCount > 0) ? newResources : null;
        }

        private string EnumName(GameConfigLanguages lang)
        { return Enum.GetName(typeof(GameConfigLanguages), lang); }
    }
}