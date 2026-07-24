using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Imports a <see cref="GameConfigAsset"/>.
    /// </summary>
    [ScriptedImporter(k_Version, GameConfigAsset.k_FileExtension)]
    internal class GameConfigImporter : ScriptedImporter
    {
        private const int k_Version = 1;

        [SerializeField, HideInInspector, Tooltip("The Titles Xbox Live Title ID, used to identity with Xbox Live services. It is generated from Partner Center.")]
        private string m_TitleId;

        [SerializeField, HideInInspector, Tooltip("The Service Configuration ID for the application to be used for Save Game Storage.")]
        private string m_SCID;

        [SerializeField, HideInInspector, Tooltip("The Titles MSA App ID, used with Xbox Live Title ID to identity with Xbox Live services. It is generated from Partner Center.")]
        private string m_MSAAppId;

        [Header("Identity")]
        [SerializeField, HideInInspector, Tooltip("The name of the product. It is generated from Partner Center.")]
        private string m_IdentityName;

        [SerializeField, HideInInspector, Tooltip("To acquire the appropriate Publisher attribute value, please see your product information in Partner Center.")]
        private string m_IdentityPublisher;

        [SerializeField, HideInInspector, Tooltip("Default version is '1.0.0.0'. Additional packages for subsequent console generations must increment package major version beyond existing package versions.")]
        private string m_IdentityVersion;

        [Header("Store")]
        [SerializeField, HideInInspector, Tooltip("The StoreId element, in your MicrosoftGame.config, must match the value in Partner Center (12 characters long (alphanumeric values)).")]
        private string m_StoreId;

        [SerializeField, HideInInspector, Tooltip("A list of language-locale pairs used to localize Shell Visuals. Apply the changes to Generate/Update/Build Shell Visual Resources.")]
        public LanguagePairs m_LanguagePairResources;

        [SerializeField, HideInInspector, Tooltip("Specifies the Assets folder containing the resources needed for Shell Visual and to build the Resources.pri file. It is recommended to use the same folder that contains the MGC asset.")]
        private UnityEngine.Object m_ShellVisualsResourcePath;

        [SerializeField, HideInInspector, Tooltip("Specifies the Assets folder that stores built Resources.pri after Build Shell Visual Resources successfully executes Makepri.exe.")]
        private UnityEngine.Object m_BuiltResourcesPriPath;

        [SerializeField, HideInInspector, Tooltip("The development sandboxes listed within your MicrosoftGame.config. These are populated when you associate with the Microsoft Store using the MicrosoftGame.config Editor.")]
        private string[] m_Sandboxes;

        // Removing for now as we are unsure if we want to support this
        // [SerializeField, HideInInspector, Tooltip("The folder that will contain the Shell Visuals resources post deployment.")]
        // private string m_ResourcesDeploymentPath;

        [Header("Development Only")]
        [SerializeField, HideInInspector, Tooltip("An override, for DEVELOPMENT-ONLY, to a games ContentID. Default value is a SHA-256 hash created based on the PackageFamilyName of your title (if a value is not supplied). Allowed value is a GUID.")]
        private string m_ContentIdOverride;

        [SerializeField, HideInInspector, Tooltip("An override, for DEVELOPMENT-ONLY, to a games EKBID (trial version ID). Allowed value is a GUID.")]
        private string m_EKBIDOverride;

        public string ShellVisualsResourcePath
        {
            get
            {
                if (m_ShellVisualsResourcePath != null)
                    return AssetDatabase.GetAssetPath(m_ShellVisualsResourcePath);

                return string.Empty;
            }
        }

        public string BuiltResourcesPriPath
        {
            get
            {
                if (m_BuiltResourcesPriPath != null)
                    return AssetDatabase.GetAssetPath(m_BuiltResourcesPriPath);

                return string.Empty;
            }
        }

        public void SetShellVisualsResourcePath(UnityEngine.Object assetPathAsObject)
        {
            m_ShellVisualsResourcePath = assetPathAsObject;
        }

        public void SetBuiltResourcesPriPath(UnityEngine.Object assetPathAsObject)
        {
            m_BuiltResourcesPriPath = assetPathAsObject;
        }

        public override void OnImportAsset(AssetImportContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));

            // Create a ScriptableObject and initialize
            GameConfigAsset gameConfigAsset = ScriptableObject.CreateInstance<GameConfigAsset>();

            try
            {
                gameConfigAsset.LoadXmlGameConfig(ctx.assetPath);

                m_IdentityName = gameConfigAsset.IdentityName;
                m_IdentityPublisher = gameConfigAsset.IdentityPublisher;
                m_IdentityVersion = gameConfigAsset.IdentityVersion;
                m_TitleId = gameConfigAsset.TitleId;
                m_SCID = gameConfigAsset.SCID;
                m_MSAAppId = gameConfigAsset.MSAAppId;
                m_Sandboxes = gameConfigAsset.Sandboxes;

                m_StoreId = gameConfigAsset.StoreId;

                m_LanguagePairResources = gameConfigAsset.LanguagePairs;

                m_ContentIdOverride = gameConfigAsset.ContentIdOverride;
                m_EKBIDOverride = gameConfigAsset.EKBIDOverride;

                if (!MGCValidatorUtility.ValidateXmlGameConfig(ctx.assetPath, out string message))
                {
                    throw new Exception("Microsoft Game Config XML validation failed:\n" + message);
                }
            }
            catch (Exception exception)
            {
                ctx.LogImportError($"Could not parse Microsoft Game Configuration file as XML format from '{ctx.assetPath}' ({exception})", this);
                DestroyImmediate(gameConfigAsset);
                return;
            }

            // We don't allow 'Shell Visuals Resource' or 'Built Resources' paths to be null or empty
            // if this happens default to the same location as the asset.s
            Object assetPathAsObject = AssetDatabase.LoadAssetAtPath<Object>(Path.GetDirectoryName(assetPath));

            if (String.IsNullOrEmpty(ShellVisualsResourcePath))
                SetShellVisualsResourcePath(assetPathAsObject);

            if (String.IsNullOrEmpty(BuiltResourcesPriPath))
                SetBuiltResourcesPriPath(assetPathAsObject);

            // Add the created asset to the AssetImportContext
            ctx.AddObjectToAsset("<root>", gameConfigAsset);
            ctx.SetMainObject(gameConfigAsset);

            // We can only validate the elements.
            // We can't validate localization as the assets is not yet added to the asset database.
            if (!MGCValidatorUtility.ValidateElements(gameConfigAsset, out string outMessage))
            {
                ctx.LogImportError($"Error(s) detected in {ctx.assetPath} during import! See details below.\n\n{outMessage}");
            }
        }

        public void ApplyEdits(GameConfigAsset gameConfigAsset, string assetPath, out string errorMessages)
        {
            errorMessages = string.Empty;

            if (gameConfigAsset is null)
            {
                errorMessages = UIContent.k_InvalidGameConfigAssetMessage;
                return;
            }

            bool success = true;

            // We don't allow 'Shell Visuals Resource' or 'Built Resources' paths to be null or empty
            // if this happens default to the same location as the asset.s
            Object assetPathAsObject = AssetDatabase.LoadAssetAtPath<Object>(Path.GetDirectoryName(assetPath));

            if (String.IsNullOrEmpty(ShellVisualsResourcePath))
                SetShellVisualsResourcePath(assetPathAsObject);

            if (String.IsNullOrEmpty(BuiltResourcesPriPath))
                SetBuiltResourcesPriPath(assetPathAsObject);

            StringBuilder stringBuilder = new StringBuilder();

            // Reflect any changes in the asset

            // Hard to validate IdentityName and IdentityPublisher. So just set them for now.
            if (m_IdentityName != gameConfigAsset.m_IdentityName)
            {
                if (string.IsNullOrEmpty(m_IdentityName))
                {
                    m_IdentityName = "TitleName";
                }

                AddApplyMessage(ref stringBuilder, "Name", gameConfigAsset.IdentityName, m_IdentityName, false);
                gameConfigAsset.m_IdentityName = m_IdentityName;
            }

            // Publisher attribute is a string that conforms to the LADPv3 Distinguished Names section in the RFC 2253 standard.
            if (m_IdentityPublisher != gameConfigAsset.m_IdentityPublisher)
            {
                if (string.IsNullOrEmpty(m_IdentityPublisher))
                {
                    m_IdentityPublisher = "CN=A4954634-DF4B-47C7-AB70-D3215D246AF1";
                }
                AddApplyMessage(ref stringBuilder, "Publisher", gameConfigAsset.IdentityPublisher, m_IdentityPublisher, false);
                gameConfigAsset.m_IdentityPublisher = m_IdentityPublisher;
            }

            string valueName = "Version";
            if (MGCValidatorUtility.CanUpdateElement(valueName, m_IdentityVersion, gameConfigAsset.IdentityVersion, MGCValidatorUtility.IsVersionStringValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, gameConfigAsset.IdentityVersion, m_IdentityVersion, false);
                gameConfigAsset.m_IdentityVersion = m_IdentityVersion;
            }

            valueName = "Title ID";
            if (MGCValidatorUtility.CanUpdateElement(valueName, m_TitleId, gameConfigAsset.TitleId, MGCValidatorUtility.IsTitleIDFormatValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, gameConfigAsset.TitleId, m_TitleId, false);
                gameConfigAsset.m_TitleId = m_TitleId;
            }

            valueName = "SCID";
            if (MGCValidatorUtility.CanUpdateElement(valueName, m_SCID, gameConfigAsset.SCID, MGCValidatorUtility.IsSCIDFormatValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, gameConfigAsset.SCID, m_SCID);
                gameConfigAsset.m_SCID = m_SCID;
            }

            valueName = "MSA App ID";
            if (MGCValidatorUtility.CanUpdateElement(valueName, m_MSAAppId, gameConfigAsset.MSAAppId, MGCValidatorUtility.IsMSAAppIdFormatValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, gameConfigAsset.MSAAppId, m_MSAAppId, false);
                gameConfigAsset.m_MSAAppId = m_MSAAppId;
            }

            valueName = "Store ID";
            if (MGCValidatorUtility.CanUpdateElement(valueName, m_StoreId, gameConfigAsset.StoreId, MGCValidatorUtility.IsStoreIDFormatValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, gameConfigAsset.StoreId, m_StoreId);
                gameConfigAsset.m_StoreId = m_StoreId;
            }

            valueName = "Content ID Override";
            if (MGCValidatorUtility.CanUpdateElement(valueName, m_ContentIdOverride, gameConfigAsset.ContentIdOverride, MGCValidatorUtility.IsContentIdOverrideFormatValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, gameConfigAsset.ContentIdOverride, m_ContentIdOverride);
                gameConfigAsset.m_ContentIdOverride = m_ContentIdOverride;
            }

            valueName = "EKBID Override";
            if (MGCValidatorUtility.CanUpdateElement(valueName, m_EKBIDOverride, gameConfigAsset.EKBIDOverride, MGCValidatorUtility.IsEKBIDOverrideFormatValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, gameConfigAsset.EKBIDOverride, m_EKBIDOverride);
                gameConfigAsset.m_EKBIDOverride = m_EKBIDOverride;
            }

            valueName = "Sandboxes";
            if(MGCValidatorUtility.CanUpdateElements(valueName, m_Sandboxes, gameConfigAsset.Sandboxes, MGCValidatorUtility.IsSandboxesFormatValid, stringBuilder))
            {
                AddApplyMessage(ref stringBuilder, valueName, string.Join(",", gameConfigAsset.Sandboxes), string.Join(",", m_Sandboxes), false);
                gameConfigAsset.m_Sandboxes = m_Sandboxes;
            }

            gameConfigAsset.m_LanguagePairs = m_LanguagePairResources;

            GameConfigLocalizationTool gameConfigLocalizationTool = new GameConfigLocalizationTool(gameConfigAsset);
            gameConfigLocalizationTool.ValidateLocalizationResources(out string localizationErrorMessages);

            // localization can validate without error but still have warnings.
            if (!string.IsNullOrEmpty(localizationErrorMessages))
            {
                stringBuilder.AppendLine(localizationErrorMessages);
            }

            // Write out the changes to the asset file (this will automatically cause a re-import)
            gameConfigAsset.SaveXmlGameConfig(assetPath);

            if (stringBuilder.Length > 0)
            {
                errorMessages = stringBuilder.ToString();
            }

            if (!success)
            {
                Debug.LogError($"Failed to apply element(s) to {assetPath}. See details below.\n\n{errorMessages}", this);
            }
        }

        [MenuItem("Assets/Create/GDK/Microsoft Game Config")]
        public static void CreateGameConfigAsset()
        {
            // Create the custom asset
            GameConfigAsset newMgcAsset = ScriptableObject.CreateInstance<GameConfigAsset>();

            newMgcAsset.m_TitleId = "FFFFFFFF";
            newMgcAsset.m_SCID = "00000000-0000-0000-0000-0000FFFFFFFF";

            // Get the path where the asset will be created, we require a folder but this could point at a pre-existing asset
            string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(selectedPath))
            {
                selectedPath = "Assets";
            }

            if (!AssetDatabase.IsValidFolder(selectedPath))
            {
                selectedPath = Path.GetDirectoryName(selectedPath);
            }

            string assetPath = AssetDatabase.GenerateUniqueAssetPath(
                selectedPath + "/MicrosoftGameConfig." + GameConfigAsset.k_FileExtension);

            // Create the asset in the project (we want to write this out as XML not meta!)
            //AssetDatabase.SaveAssets();
            newMgcAsset.SaveXmlGameConfig(assetPath);

            // Force import the asset then we can get hold of it's importer
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

            // Fix up a default Localization Path (Not sure if we are supposed to treat asset folders as objects but it works)
            UnityEngine.Object assetPathAsObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(selectedPath);
            GameConfigImporter assetImporter = GetAtPath(assetPath) as GameConfigImporter;

            if (assetImporter != null)
            {
                assetImporter.SetShellVisualsResourcePath(assetPathAsObject);
                assetImporter.SetBuiltResourcesPriPath(assetPathAsObject);
            }

            // Select the asset in the Project window.
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;

            // TODO: Can't figure out how to get Unity to put an asset into rename mode :(
        }

        private static void AddApplyMessage(ref StringBuilder stringBuilder, string name, string originalValue, string valueToApply, bool canBeNullOrEmpty = true)
        {
            if (string.IsNullOrEmpty(valueToApply))
            {
                // Some values can't be null and will be defaulted during XML save.
                stringBuilder.AppendLine(canBeNullOrEmpty ? $"{name} set to '{valueToApply}'." : $"{name} reset.\n");
            }
            else
            {
                // Limit the length of the apply message to avoid difficult to read messages
                if(originalValue.Length < 128 && valueToApply.Length < 128)
                {
                    stringBuilder.AppendLine($"{name} changed from '{originalValue}' to '{valueToApply}'.\n");
                }
                else
                {
                    stringBuilder.Append($"{name} changes have been applied.\n");
                }
            }
        }
    }
}