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

    [HarmonyPatch(nameof(PlayerPhysics.SetBodyType)), HarmonyPrefix]
    public static void PlayerPhysicsSetBodyTypePrefix(PlayerPhysics __instance, [HarmonyArgument(0)] ref PlayerBodyTypes type)
    {
        if (type == PlayerBodyTypes.Normal)
        {
            type = PlayerBodyTypes.Classic;
        }
    }

    [HarmonyPatch(nameof(PlayerPhysics.SetBodyType)), HarmonyPostfix]
    public static void PlayerPhysicsSetBodyTypePostfix(PlayerPhysics __instance)
    {
        __instance.myPlayer.cosmetics.SetScale(__instance.Animations.DefaultPlayerScale, __instance.myPlayer.defaultCosmeticsScale);
    }
}
