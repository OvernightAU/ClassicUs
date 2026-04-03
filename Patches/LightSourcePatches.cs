using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(LightSource))]
public static class LightSourcePatches
{
    [HarmonyPatch(nameof(LightSource.Initialize)), HarmonyPrefix]
    public static void LightSourcePatch(LightSource __instance)
    {
        __instance.rendererType = LightSourceRendererType.Raycast;
    }
}
