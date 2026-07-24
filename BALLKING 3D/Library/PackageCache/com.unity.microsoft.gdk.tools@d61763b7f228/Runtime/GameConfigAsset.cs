using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;


// Allows GameConfigEditor to access internal members of GameConfigAsset.
[assembly: InternalsVisibleTo("Unity.Microsoft.GDK.Tools.Editor")]

namespace Unity.Microsoft.GDK.Tools
{
    /// <summary>
    /// The GameConfigAsset class is mainly used to track common user configurable elements of a
    /// Microsoft Game Config (MGC) file.
    /// It can be referenced and used at runtime to access the game configuration data.
    /// However, it is not intended to be manually modified by code by users.
    /// Instead, the asset can be edited either in the Unity Editor using the Inspector (GameConfigEditor) or by using the
    /// <see href="https://learn.microsoft.com/en-us/gaming/gdk/_content/gc/system/overviews/microsoft-game-config/microsoftgameconfig-editor">
    /// Microsoft Game Config Editor</see>.
    /// </summary>
    public class GameConfigAsset : ScriptableObject
    {
        /// <summary>
        /// Expected file extension (without the dot) for a Microsoft Game Configuration file in XML format.
        /// </summary>
        /// <description>
        /// Asset files with this extension will automatically be imported and treated as a Microsoft Game
        /// Configuration file asset providing it contains valid XML.
        /// </description>
        /// <value> File extension for Microsoft Game Configuration files. </value>
        public const string k_FileExtension = "mgc";

        // Any Serialized Fields/TextArea/etc which are not hidden will show in the preview area

        [SerializeField, HideInInspector]
        internal string m_IdentityName = "";

        [SerializeField, HideInInspector]
        internal string m_IdentityPublisher = "";

        [SerializeField, HideInInspector]
        internal string m_IdentityVersion = "1.0.0.0";

        [SerializeField, HideInInspector]
        internal string m_TitleId = "";

        [SerializeField, HideInInspector]
        internal string m_SCID = "";

        [SerializeField, HideInInspector]
        internal string m_MSAAppId = "";

        [SerializeField, HideInInspector]
        internal string m_StoreId = "";

        [SerializeField, HideInInspector]
        internal string m_ContentIdOverride = "";

        [SerializeField, HideInInspector]
        internal string m_EKBIDOverride = "";

        [SerializeField, HideInInspector]
        internal string[] m_Sandboxes = { };

        [SerializeField, HideInInspector]
        // This will be marked 'internal' in future, externally callers should use LanguagePairs or
        //  See package documentation 'Modify GDK settings with C# script'
        public LanguagePairs m_LanguagePairs = new LanguagePairs();

        public string IdentityName => m_IdentityName;

        public string IdentityPublisher => m_IdentityPublisher;

        public string IdentityVersion => m_IdentityVersion;

        public string TitleId
        {
            get => m_TitleId;

            [Obsolete("Please do not use. This will be removed in a future release. See package documentation 'Modify GDK settings with C# script'", false)]
            set => m_TitleId = value;
        }

        public string SCID
        {
            get => m_SCID;

            [Obsolete("Please do not use. This will be removed in a future release. See package documentation 'Modify GDK settings with C# script'", false)]
            set => m_SCID = value;
        }

        public string MSAAppId
        {
            get => m_MSAAppId;

            [Obsolete("Please do not use. This will be removed in a future release. See package documentation 'Modify GDK settings with C# script'", false)]
            set => m_MSAAppId = value;
        }

        public string StoreId
        {
            get => m_StoreId;

            [Obsolete("Please do not use. This will be removed in a future release. See package documentation 'Modify GDK settings with C# script'", false)]
            set => m_StoreId = value;
        }

        public string ContentIdOverride
        {
            get => m_ContentIdOverride;

            [Obsolete("Please do not use. This will be removed in a future release. See package documentation 'Modify GDK settings with C# script'", false)]
            set => m_ContentIdOverride = value;
        }

        public string EKBIDOverride
        {
            get => m_EKBIDOverride;

            [Obsolete("Please do not use. This will be removed in a future release. See package documentation 'Modify GDK settings with C# script'", false)]
            set => m_EKBIDOverride = value;
        }

        public string[] Sandboxes => m_Sandboxes;

        public LanguagePairs LanguagePairs
        {
            get => m_LanguagePairs;
        }

        /// <summary>
        /// Static helper reads in the specified XML formatted Microsoft Game Config file.
        /// </summary>
        /// <param name="xmlGameConfigPath"> Path to the XML formatted Microsoft Game Config file to be read. </param>
        /// <returns> On success an instance of Game, otherwise null. </returns>
        public static Game LoadAsXmlGameConfig(string xmlGameConfigPath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Game));
                using (var stream = new StreamReader(xmlGameConfigPath))
                    return (Game)serializer.Deserialize(stream);
            }
            catch (Exception /*e*/)
            {
                return null;
            }
        }

        /// <summary>
        /// Initializes a GameConfigAsset from an XML formatted Microsoft Game Config file.
        /// </summary>
        /// <param name="xmlGameConfigPath"> File path including, name and extension. </param>
        public void LoadXmlGameConfig(string xmlGameConfigPath)
        {
            // Load the XML from the .mgc file
            Game xmlGameConfig = LoadAsXmlGameConfig(xmlGameConfigPath);

            // Pull out any details of interest to the user or build processing

            if (xmlGameConfig.Identity.Name != null)
                m_IdentityName = xmlGameConfig.Identity.Name;

            if (xmlGameConfig.Identity.Publisher != null)
                m_IdentityPublisher = xmlGameConfig.Identity.Publisher;

            if (xmlGameConfig.Identity.Version != null)
                m_IdentityVersion = xmlGameConfig.Identity.Version;

            // Title ID
            if (xmlGameConfig.TitleId != null)
                m_TitleId = xmlGameConfig.TitleId;

            // MSAAppId
            if (xmlGameConfig.MSAAppId != null)
                m_MSAAppId = xmlGameConfig.MSAAppId;

            // SCID
            if (xmlGameConfig.ExtendedAttributeList != null)
            {
                // The query breaks if the list length is 0 ...
                if (xmlGameConfig.ExtendedAttributeList.ExtendedAttribute.Length > 0)
                {
                    CT_ExtendedAttributeListExtendedAttribute scidExtendedAttribute =
                        (from extendedAttribute in xmlGameConfig.ExtendedAttributeList.ExtendedAttribute
                         where extendedAttribute.Name == "Scid"
                         select extendedAttribute).First();
                    m_SCID = scidExtendedAttribute.Value ?? "";

                    
                    var sandboxIds = from extendedAttribute in xmlGameConfig.ExtendedAttributeList.ExtendedAttribute
                             where extendedAttribute.Name == "SandboxIds"
                             select extendedAttribute;
                    if(sandboxIds != null && sandboxIds.Count() > 0)
                    {
                        CT_ExtendedAttributeListExtendedAttribute sandboxesExtendedAttribute = sandboxIds.First();
                        m_Sandboxes = sandboxesExtendedAttribute.Value.Split(",") ?? null; 
                    }
                }
                else
                {
                    m_SCID = "";
                    m_Sandboxes = new string[] { };
                }
            }

            // Store ID
            if (xmlGameConfig.StoreId != null)
                m_StoreId = xmlGameConfig.StoreId;

            // ShellVisuals and Language Resources
            m_LanguagePairs.ImportFromXml(xmlGameConfig);

            // DEVELOPMENT ONLY VALUES
            // Content ID Override
            if (xmlGameConfig.DevelopmentOnly != null)
            {
                m_ContentIdOverride = xmlGameConfig.DevelopmentOnly.ContentIdOverride;
                m_EKBIDOverride = xmlGameConfig.DevelopmentOnly.EKBIDOverride;
            }
        }

        /// <summary>
        /// Static helper writes out an instance of the `Game` class as an XML formatted Microsoft Game Config file.
        /// </summary>
        /// <param name="xmlGameConfigPath"> Path of the file to be written. </param>
        /// <param name="xmlGameConfig"> Game instance configured with the XML elements being written. </param>
        public static void SaveXmlGameConfig(string xmlGameConfigPath, Game xmlGameConfig)
        {
            try
            {
                string directory = Path.GetDirectoryName(xmlGameConfigPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Write back out (this will touch the asset and cause a reload)
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
                {
                    Indent = true,
                    NewLineOnAttributes = true
                };

                XmlSerializer serializer = new XmlSerializer(typeof(Game));
                using (XmlWriter xmlWriter = XmlWriter.Create(xmlGameConfigPath, xmlWriterSettings))
                    serializer.Serialize(xmlWriter, xmlGameConfig);
            }
            catch (Exception e)
            {
                Debug.LogError($"GameConfigAsset: Save FAILED - Could not write Microsoft Game Config file ({e.GetType()}). Error: '{e.Message}'");
            }
        }

        /// <summary>
        /// Writes a GameConfigAsset to a XML formatted Microsoft Game Config file.
        /// </summary>
        /// <param name="xmlGameConfigPath"> File path including, name and extension. </param>
        public void SaveXmlGameConfig(string xmlGameConfigPath)
        {
            //TODO: We need to handle the '.mgc' file being write protected, for example Source Safe SCM defaults unchecked out files to read-only

            // Load the existing Xml Game Config
            Game xmlGameConfig = LoadAsXmlGameConfig(xmlGameConfigPath);

            if (xmlGameConfig == null)
            {
                xmlGameConfig = new Game();

                // The config will not be valid without something here
                xmlGameConfig.configVersion = "1";
            }

            // Apply updates ... NOTE: The MS Game Config Editor does not like empty string values!
            xmlGameConfig.TitleId = string.IsNullOrEmpty(m_TitleId) ? "FFFFFFFF" : m_TitleId;
            xmlGameConfig.MSAAppId = string.IsNullOrEmpty(m_MSAAppId) ? "PleaseChangeMe" : m_MSAAppId;
            xmlGameConfig.StoreId = string.IsNullOrEmpty(m_StoreId) ? null : m_StoreId;

            if (xmlGameConfig.Identity == null)
                xmlGameConfig.Identity = new CT_Identity();

            xmlGameConfig.Identity.Name = string.IsNullOrEmpty(m_IdentityName) ? "TitleName" : m_IdentityName;
            xmlGameConfig.Identity.Version = string.IsNullOrEmpty(m_IdentityVersion) ? "1.0.0.0" : m_IdentityVersion;
            xmlGameConfig.Identity.Publisher = string.IsNullOrEmpty(m_IdentityPublisher) ? "CN=A4954634-DF4B-47C7-AB70-D3215D246AF1" : m_IdentityPublisher;

            // SCID and Sandboxes
            if (xmlGameConfig.ExtendedAttributeList == null)
            {
                CT_ExtendedAttributeList extendedAttributeList = new CT_ExtendedAttributeList();

                CT_ExtendedAttributeListExtendedAttribute newScidExtendedAttribute = new();

                if (!string.IsNullOrEmpty(m_SCID))
                {
                    newScidExtendedAttribute = new CT_ExtendedAttributeListExtendedAttribute
                    {
                        Name = "Scid",
                        Value = m_SCID
                    };
                }

                if (m_Sandboxes != null && m_Sandboxes.Length > 0)
                {
                    CT_ExtendedAttributeListExtendedAttribute newSandboxesExtendedAttribute = new();
                    newSandboxesExtendedAttribute = new CT_ExtendedAttributeListExtendedAttribute
                    {
                        Name = "SandboxIds",
                        Value = string.Join(",", m_Sandboxes)
                    };
                    extendedAttributeList.ExtendedAttribute = new[] { newScidExtendedAttribute, newSandboxesExtendedAttribute }; // Only add sandboxIds if count > 0 to avoid errors
                }
                else
                {
                    extendedAttributeList.ExtendedAttribute = new[] { newScidExtendedAttribute };
                }

                xmlGameConfig.ExtendedAttributeList = extendedAttributeList;
            }
            else
            {
                CT_ExtendedAttributeListExtendedAttribute[] newExtendedAttributeListExtendedAttribute =
                    new CT_ExtendedAttributeListExtendedAttribute[xmlGameConfig.ExtendedAttributeList.ExtendedAttribute.Length + 2];

                for (int index = 0;
                     index < xmlGameConfig.ExtendedAttributeList.ExtendedAttribute.Length;
                     index++)
                {
                    if (xmlGameConfig.ExtendedAttributeList.ExtendedAttribute[index].Name.ToUpper() == "SCID")
                    {
                        // Skip any pre-existing SCID entries
                    }
                    else if (xmlGameConfig.ExtendedAttributeList.ExtendedAttribute[index].Name.ToUpper() == "SANDBOXIDS")
                    {
                        // Skip any pre-existing Sandbox entries
                    }
                    else
                    {
                        newExtendedAttributeListExtendedAttribute[index] =
                            xmlGameConfig.ExtendedAttributeList.ExtendedAttribute[index];
                    }
                }

                CT_ExtendedAttributeListExtendedAttribute newScidExtendedAttribute = new CT_ExtendedAttributeListExtendedAttribute
                {
                    Name = "Scid",
                    Value = string.IsNullOrEmpty(m_SCID) ? null : m_SCID
                };
                newExtendedAttributeListExtendedAttribute[^2] = newScidExtendedAttribute;

                if (m_Sandboxes.Length > 0)
                {
                    CT_ExtendedAttributeListExtendedAttribute newSandboxesExtendedAttribute = new CT_ExtendedAttributeListExtendedAttribute
                    {
                        Name = "SandboxIds",
                        Value = m_Sandboxes != null ? string.Join(",", m_Sandboxes) : null
                    };
                    newExtendedAttributeListExtendedAttribute[^1] = newSandboxesExtendedAttribute;
                }

                xmlGameConfig.ExtendedAttributeList.ExtendedAttribute = newExtendedAttributeListExtendedAttribute;
            }

            // Resources - Language Pairs
            m_LanguagePairs.ExportToXml(xmlGameConfig);

            // Development Only
            if (xmlGameConfig.DevelopmentOnly != null)
            {
                xmlGameConfig.DevelopmentOnly.ContentIdOverride = string.IsNullOrEmpty(m_ContentIdOverride) ? null : m_ContentIdOverride;
                xmlGameConfig.DevelopmentOnly.EKBIDOverride = string.IsNullOrEmpty(m_EKBIDOverride) ? null : m_EKBIDOverride;
            }
            else
            {
                CT_DevelopmentOnly developmentOnly = new CT_DevelopmentOnly();
                xmlGameConfig.DevelopmentOnly = developmentOnly;

                xmlGameConfig.DevelopmentOnly.ContentIdOverride = string.IsNullOrEmpty(m_ContentIdOverride) ? null : m_ContentIdOverride;
                xmlGameConfig.DevelopmentOnly.EKBIDOverride = string.IsNullOrEmpty(m_EKBIDOverride) ? null : m_EKBIDOverride;
            }

            SaveXmlGameConfig(xmlGameConfigPath, xmlGameConfig);
        }

        /// <summary>
        /// Static helper writes out an instance of the `Game` class as an XML formatted Microsoft Game Config string.
        /// </summary>
        /// <param name="xmlGameConfig"> Game instance configured with the XML elements being written. </param>
        public static string GameConfigToString(Game xmlGameConfig)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "      ",
                NewLineChars = "\r\n",
                OmitXmlDeclaration = true,
                NewLineOnAttributes = true
            };

            using (StringWriter stream = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                    serializer.Serialize(xmlWriter, xmlGameConfig);

                return stream.ToString();
            }
        }

        /// <summary>
        /// Use to enable/disable a specific Shell Visual language pair on this GameConfigAsset.
        /// </summary>
        /// <param name="language">The GameConfigLanguages language pair being set. </param>
        /// <param name="isActive">True to enable this language pair, False to disable. </param>
        /// <returns> True if set, otherwise False. </returns>
        public void SetLanguagePair(GameConfigLanguages language, bool isActive)
        {
            m_LanguagePairs.SetLanguagePair(language, isActive);
        }

        /// <summary>
        /// Use to test if a specific Shell Visual language pair has been configured on this GameConfigAsset.
        /// </summary>
        /// <param name="language">The GameConfigLanguages language pair being tested. </param>
        /// <returns> True if set, otherwise False. </returns>
        public bool IsLanguagePairSet(GameConfigLanguages language)
        {
            return m_LanguagePairs.IsLanguagePairSet(language);
        }

        /// <summary>
        /// Use to test if and Shell Visual language pairs have been configured on this GameConfigAsset.
        /// </summary>
        /// <returns> True if 1 or more language pairs are configured, otherwise False. </returns>
        public bool HasLanguagePairSet()
        {
            foreach (GameConfigLanguages lang in Enum.GetValues(typeof(GameConfigLanguages)))
            {
                if (IsLanguagePairSet(lang))
                    return true;
            }

            return false;
        }
    }
}