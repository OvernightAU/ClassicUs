using HarmonyLib;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerAnimations))]
public static class PlayerAnimationsPatches
{
    [HarmonyPatch(nameof(PlayerAnimations.SetBodyType)), HarmonyPrefix]
    public static void PlayerAnimationsSetBodyType(PlayerAnimations __instance, [HarmonyArgument(0)] PlayerBodyTypes type)
    {
        var group = (int)type;

        // Not needed anymore, we just replace normal with Classic
        if (type == PlayerBodyTypes.Seeker)
        {
            //__instance.animationGroups[group].NodeSyncs = __instance.animationGroups[(int)PlayerBodyTypes.Classic].NodeSyncs;
            __instance.animationGroups[group].IdleAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].IdleAnim;
            __instance.animationGroups[group].RunAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].RunAnim;
            __instance.animationGroups[group].SpawnAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].SpawnAnim;
            __instance.animationGroups[group].GhostIdleAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].GhostIdleAnim;
            __instance.animationGroups[group].GhostGuardianAngelAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].GhostGuardianAngelAnim;
            __instance.animationGroups[group].EnterVentAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].EnterVentAnim;
            __instance.animationGroups[group].ExitVentAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].ExitVentAnim;
            __instance.animationGroups[group].ClimbUpAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].ClimbUpAnim;
            __instance.animationGroups[group].ClimbDownAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].ClimbDownAnim;
            __instance.animationGroups[group].defaultPlayerScale = __instance.animationGroups[(int)PlayerBodyTypes.Classic].defaultPlayerScale;
            __instance.transform.GetParent().GetComponent<PlayerControl>().cosmetics.currentBodySprite.BodySprite.material.SetColor("_VisorColor", Color.green);
        }
    }
}
