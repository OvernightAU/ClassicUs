using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using Sentry.Unity.NativeUtils;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerAnimations), nameof(PlayerAnimations.SetBodyType))]
public static class PlayerAnimationsPatches
{
    public static void Prefix(PlayerAnimations __instance, [HarmonyArgument(0)] PlayerBodyTypes type)
    {
        var group = (int)type;
        if (type == PlayerBodyTypes.Normal)
        {
            __instance.animationGroups[group].SpriteAnimator.m_defaultAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicIdle");
            __instance.animationGroups[group].IdleAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicIdle");
            __instance.animationGroups[group].RunAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicWalk");
            __instance.animationGroups[group].SpawnAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicSpawn");
            __instance.animationGroups[group].GhostIdleAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicGhost");
            __instance.animationGroups[group].GhostGuardianAngelAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicGhost");
            __instance.animationGroups[group].EnterVentAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicEnterVent");
            __instance.animationGroups[group].ExitVentAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicExitVent");
            __instance.animationGroups[group].ClimbUpAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicClimbUp");
            __instance.animationGroups[group].ClimbDownAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicClimbDown");
            __instance.animationGroups[group].SpawnGlowAnim = null;
            __instance.animationGroups[group].defaultPlayerScale = Vector3.one;
        }
        else if (type == PlayerBodyTypes.Seeker)
        {
            __instance.animationGroups[group].SpriteAnimator.m_defaultAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicIdle");
            __instance.animationGroups[group].IdleAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicIdle");
            __instance.animationGroups[group].RunAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicWalk");
            __instance.animationGroups[group].SpawnAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicSpawn");
            __instance.animationGroups[group].GhostIdleAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicGhost");
            __instance.animationGroups[group].GhostGuardianAngelAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicGhost");
            __instance.animationGroups[group].EnterVentAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicEnterVent");
            __instance.animationGroups[group].ExitVentAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicExitVent");
            __instance.animationGroups[group].ClimbUpAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicClimbUp");
            __instance.animationGroups[group].ClimbDownAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("ClassicClimbDown");
            __instance.animationGroups[group].SpawnGlowAnim = null;
            __instance.animationGroups[group].defaultPlayerScale = Vector3.one;
            __instance.transform.GetParent().GetComponent<PlayerControl>().cosmetics.currentBodySprite.BodySprite.material.SetColor("_VisorColor", Color.green);
        }
    }
}