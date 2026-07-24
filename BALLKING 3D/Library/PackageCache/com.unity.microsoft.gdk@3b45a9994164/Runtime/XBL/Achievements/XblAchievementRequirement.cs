using UnityEngine.Scripting.APIUpdating;
namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else // GAMECORE_UPGRADE_PATH
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblAchievementRequirement
    {
        internal XblAchievementRequirement(Interop.XblAchievementRequirement interopRequirement)
        {
            this.Id = interopRequirement.id.GetString();
            this.CurrentProgressValue = interopRequirement.currentProgressValue.GetString();
            this.TargetProgressValue = interopRequirement.targetProgressValue.GetString();
        }

        public string Id { get; private set; }
        public string CurrentProgressValue { get; private set; }
        public string TargetProgressValue { get; private set; }
    }
}
