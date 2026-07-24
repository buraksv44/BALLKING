using System.Globalization;
using System.IO;

using Unity.Microsoft.GDK.Discovery;

namespace Unity.Microsoft.GDK.Editor
{
    internal sealed class GdkAssemblyPathCache
    {
        GdkData m_CurrentGdkData;
        string m_CurrentGdkEdition;
        string m_GrdkInstallPath;

        internal GdkAssemblyPathCache(GdkData gdkData)
        {
            m_CurrentGdkData = gdkData;
        }

        internal int GdkEditionNum => m_CurrentGdkData.Edition;

        internal string GdkEdition
        {
            get
            {
                if (string.IsNullOrEmpty(m_CurrentGdkEdition))
                    m_CurrentGdkEdition = m_CurrentGdkData.Edition.ToString(CultureInfo.InvariantCulture);

                return m_CurrentGdkEdition;
            }
        }

        internal string GrdkInstallPath
        {
            get
            {
                if (string.IsNullOrEmpty(m_GrdkInstallPath))
                {
                    var gdkPath = m_CurrentGdkData.Path;
                    if (!string.IsNullOrEmpty(gdkPath))
                        m_GrdkInstallPath = Path.Combine(gdkPath, GdkEdition);
                }

                return m_GrdkInstallPath;
            }
        }
    }
}