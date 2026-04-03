using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(AprilFoolsMode))]
public static class AprilFoolsModePatches
{
    // Warning, this might be removed or inlined eventually.
    [HarmonyPatch(nameof(AprilFoolsMode.ShouldClassicMode)), HarmonyPrefix]
    public static bool AlwaysClassic(out bool __result)
    {
        __result = true;
        return false;
    }
}
