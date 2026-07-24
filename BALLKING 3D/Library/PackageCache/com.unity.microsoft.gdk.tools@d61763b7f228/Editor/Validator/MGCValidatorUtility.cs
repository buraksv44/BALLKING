using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;

using XmlGameConfig = Unity.Microsoft.GDK.Game;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal static class MGCValidatorUtility
    {
        private const int k_MGCElementMaxLength = 256;
        private static readonly string k_DefaultTitleId = "FFFFFFFF";
        private static readonly string k_DefaultSCID = "00000000-0000-0000-0000-0000FFFFFFFF";
        private static readonly string k_DefaultMSAAppId = "PleaseChangeMe";

        internal static bool ValidateAll(GameConfigAsset gameConfigAsset, out string validationMessages)
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool success = ValidateXmlGameConfig(AssetDatabase.GetAssetPath(gameConfigAsset), out validationMessages);
            if (!string.IsNullOrEmpty(validationMessages))
            {
                stringBuilder.AppendLine(validationMessages);
            }
            success |= ValidateElements(gameConfigAsset, out validationMessages);
            if (!string.IsNullOrEmpty(validationMessages))
            {
                stringBuilder.AppendLine(validationMessages);
            }
            success |= ValidateLocalizationResources(gameConfigAsset, out validationMessages);
            if (!string.IsNullOrEmpty(validationMessages))
            {
                stringBuilder.AppendLine(validationMessages);
            }

            validationMessages = string.Empty;
            if (stringBuilder.Length > 0)
            {
                validationMessages = stringBuilder.ToString();
            }

            return success;
        }

        internal static bool ValidateXmlGameConfig(string xmlGameConfigPath, out string validationMessages)
        {
            validationMessages = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            XmlGameConfig xmlGameConfig = GameConfigAsset.LoadAsXmlGameConfig(xmlGameConfigPath);

            if (xmlGameConfig == null)
            {
                stringBuilder.AppendLine($"Error: Failed to load '{xmlGameConfigPath}'.");
                validationMessages = stringBuilder.ToString();
                return false;
            }

            if (xmlGameConfig.Identity == null)
            {
                stringBuilder.AppendLine($"Error: Identity element is missing from '{xmlGameConfigPath}'.");
            }

            if (string.IsNullOrEmpty(xmlGameConfig.TitleId))
            {
                stringBuilder.AppendLine($"Error: TitleId element is missing from '{xmlGameConfigPath}'.");
            }

            if (string.IsNullOrEmpty(xmlGameConfig.MSAAppId))
            {
                stringBuilder.AppendLine($"Error: MSAAppId element is missing from '{xmlGameConfigPath}'.");
            }

            bool success = stringBuilder.Length == 0;
            if (!success)
            {
                validationMessages = stringBuilder.ToString();
            }

            return success;
        }

        internal static bool ValidateElements(GameConfigAsset gameConfigAsset, out string validationMessages)
        {
            bool success = true;
            StringBuilder stringBuilder = new StringBuilder();

            success &= ValidateElement("Version", gameConfigAsset.IdentityVersion, IsVersionStringValid, stringBuilder);
            success &= ValidateElement("Title ID", gameConfigAsset.TitleId, IsTitleIDFormatValid, stringBuilder);
            success &= ValidateElement("SCID", gameConfigAsset.SCID, IsSCIDFormatValid, stringBuilder);
            success &= ValidateElement("MSA App Id", gameConfigAsset.MSAAppId, IsMSAAppIdFormatValid, stringBuilder);
            success &= ValidateElement("Store Id", gameConfigAsset.StoreId, IsStoreIDFormatValid, stringBuilder);
            success &= ValidateElement("Content ID Override", gameConfigAsset.ContentIdOverride, IsContentIdOverrideFormatValid, stringBuilder);
            success &= ValidateElement("EKBID Override", gameConfigAsset.EKBIDOverride, IsEKBIDOverrideFormatValid, stringBuilder);
            success &= ValidateElements("Sandboxes", gameConfigAsset.m_Sandboxes, IsSandboxesFormatValid, stringBuilder);

            validationMessages = string.Empty;
            if (stringBuilder.Length > 0)
            {
                validationMessages = stringBuilder.ToString();
            }

            return success;
        }

        internal static bool ValidateLocalizationResources(GameConfigAsset gameConfigAsset, out string validationMessages)
        {
            GameConfigLocalizationTool gameConfigLocalizationTool = new GameConfigLocalizationTool(gameConfigAsset);
            StringBuilder stringBuilder = new StringBuilder();

            bool success = gameConfigLocalizationTool.ValidateLocalizationResources(out string localizationValidationMessages);

            // localization can validate without error but still have warnings.
            if (!string.IsNullOrEmpty(localizationValidationMessages))
            {
                stringBuilder.AppendLine(localizationValidationMessages);
            }

            validationMessages = string.Empty;
            if (stringBuilder.Length > 0)
            {
                validationMessages = stringBuilder.ToString();
            }

            return success;
        }

        private static bool ValidateElement(string name, string value, Func<string, ValueTuple<bool, string>> predicate, StringBuilder message)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            if (value.Length > k_MGCElementMaxLength)
            {
                message.AppendLine($"Error: {name} '{value.Substring(0, k_MGCElementMaxLength)}...' is too long and invalid.");
                return false;
            }

            var result = predicate.Invoke(value);
            if (!result.Item1)
            {
                message.AppendLine("Error: " + result.Item2);
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(result.Item2))
                {
                    message.AppendLine("Warning: " + result.Item2);
                }

                return true;
            }
        }

        private static bool ValidateElements(string name, string[] values, Func<string[], ValueTuple<bool, string>> predicate, StringBuilder message)
        {
            var result = predicate.Invoke(values);
            if (!result.Item1)
            {
                message.AppendLine("Error: " + result.Item2);
                return false;
            }
            else
            {
                if(!string.IsNullOrEmpty(result.Item2))
                {
                    message.AppendLine("Warning: " + result.Item2);
                }
                return true;
            }
        }

        internal static bool CanUpdateElement(string name, string valueToApply, string originalValue, Func<string, ValueTuple<bool, string>> predicate, StringBuilder message)
        {
            // In case we try to set a value to empty string.
            if (string.IsNullOrEmpty(valueToApply))
            {
                return !string.IsNullOrEmpty(originalValue);
            }

            if (valueToApply.Length > k_MGCElementMaxLength)
            {
                message.AppendLine($"Error: {name} '{valueToApply.Substring(0, k_MGCElementMaxLength)}...' is too long and invalid.");
                return false;
            }

            var result = predicate.Invoke(valueToApply);
            if (!result.Item1)
            {
                message.AppendLine("Error: " + result.Item2);
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(result.Item2))
                {
                    message.AppendLine("Warning: " + result.Item2);
                }

                // We exit only here to allow any warnings to be registered.
                if (valueToApply == originalValue)
                {
                    return false;
                }

                return true;
            }
        }

        internal static bool CanUpdateElements(string name, string[] valuesToApply, string[] originalValues, Func<string[], ValueTuple<bool, string>> predicate, StringBuilder message)
        {
            var result = predicate.Invoke(valuesToApply);
            if (!result.Item1)
            {
                message.Append("Error: " + result.Item2);
                return false;
            }

            return true;
        }

        internal static ValueTuple<bool, string> IsVersionStringValid(string input)
        {
            Regex regex = new Regex(@"^\d+\.\d+\.\d+\.\d+$");
            if (!regex.IsMatch(input))
            {
                return (false, $"Version '{input}' is not in the correct format.  Expecting a string in the format 'x.x.x.x'.");
            }

            return (true, string.Empty);
        }

        internal static ValueTuple<bool, string> IsStoreIDFormatValid(string storeId)
        {
            Regex regex = new Regex(@"^[0-9a-zA-Z]{12}$");
            if (!regex.IsMatch(storeId))
            {
                return (false, $"Store ID '{storeId}' is not in the correct format. Expecting 12 alphanumeric characters.");
            }

            return (true, string.Empty);
        }

        internal static ValueTuple<bool, string> IsContentIdOverrideFormatValid(string contentIdOverride)
        {
            return IsSHA256FormatValid(contentIdOverride, "ContentIdOverride");
        }

        internal static ValueTuple<bool, string> IsEKBIDOverrideFormatValid(string EKBIDOverride)
        {
            return IsSHA256FormatValid(EKBIDOverride, "EKBIDOverride");
        }

        internal static ValueTuple<bool, string> IsSCIDFormatValid(string scid)
        {
            if (scid == k_DefaultSCID)
            {
                return (true, $"SCID '{scid}' is a placeholder value.");
            }

            return IsSHA256FormatValid(scid, "SCID");
        }

        internal static ValueTuple<bool, string> IsTitleIDFormatValid(string titleId)
        {
            if (titleId == k_DefaultTitleId)
            {
                return (true, $"Title ID '{titleId}' is a placeholder value.");
            }

            Regex regex = new Regex(@"^[0-9a-fA-F]{8}$");
            if (!regex.IsMatch(titleId))
            {
                return (false, $"Title ID '{titleId}' is not in the correct format. Expecting 8 hexadecimal characters.");
            }

            return (true, string.Empty);
        }

        internal static ValueTuple<bool, string> IsMSAAppIdFormatValid(string msaAppId)
        {
            if (msaAppId == k_DefaultMSAAppId)
            {
                return (true, $"MSA App ID '{msaAppId}' is a placeholder value.");
            }

            if (string.IsNullOrEmpty(msaAppId))
            {
                return (false, $"MSA App ID '{msaAppId}' is not in the correct format. Expecting non-empty string. Typical value is a 16 character string of integers.");
            }
            else if (msaAppId.Length != 16)
            {
                return (true, $"MSA App ID '{msaAppId}' might be invalid. Typical value is a 16 character string of integers.");
            }

            return (true, string.Empty);
        }

        internal static ValueTuple<bool, string> IsSandboxesFormatValid(string[] sandboxes)
        {
            if(sandboxes.Length == 1 && string.IsNullOrWhiteSpace(sandboxes[0]))
            {
                return (false, $"Sandbox cannot be empty. Either remove it or enter a character string for the Sandbox.\n");
            }

            if (sandboxes.Length == 0)
            {
                return (true, $"Warning: Sandboxes are currently empty. To retrieve configured sandboxes from Microsoft Partner Center, use the \"Associate with the Microsoft Store\" button in the Game Config Editor.\n");
            }

            foreach(var sandbox in sandboxes)
            {
                if(XblPCSandbox.XblPCSandbox.IsValidSandbox(sandbox) == false)
                {
                    return (false, $"Invalid Sandbox name: {sandbox}\n");
                }
            }

            return (true, string.Empty);
        }

        private static ValueTuple<bool, string> IsSHA256FormatValid(string guid, string name)
        {
            Regex regex = new Regex(@"^([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})$");
            if (!regex.IsMatch(guid))
            {
                return (false, $"{name} '{guid}' is not in the correct format. Expecting SHA-256 hash format (00000000-0000-0000-0000-000000000000).");
            }

            return (true, string.Empty);
        }
    }
}