using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerPhysics))]
public static class PlayerPhysicsPatches
{
    [HarmonyPatch(nameof(PlayerPhysics.AnimateCustom)), HarmonyPrefix]
    public static bool PlayerPhysicsAnimateCustom(PlayerPhysics __instance)
    {
        return false;
    }

    [HarmonyPatch(nameof(PlayerPhysics.SetBodyType)), HarmonyPostfix]
    public static void PlayerPhysicsSetBodyTypePostfix(PlayerPhysics __instance)
    {
        __instance.myPlayer.cosmetics.SetScale(__instance.Animations.DefaultPlayerScale, __instance.myPlayer.defaultCosmeticsScale);
    }
}
