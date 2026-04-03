using HarmonyLib;
using UnityEngine.AddressableAssets;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(AssetReference))]
public static class AssetReferencePatches
{

    [HarmonyPatch(nameof(AssetReference.RuntimeKeyIsValid)), HarmonyPrefix]
    public static bool PreviewDataRuntimeKeyPatch(AssetReference __instance, ref bool __result)
    {
        /*
        if (__instance.AssetGUID.StartsWith("corsac."))
        {
            __result = true;
            return false;
        }
        */

        __result = true;
        return false;
    }
}
