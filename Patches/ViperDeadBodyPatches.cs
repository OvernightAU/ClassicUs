using AmongUs.Data;
using HarmonyLib;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(ViperDeadBody))]
public static class ViperDeadBodyPatches
{
    [HarmonyPatch(nameof(ViperDeadBody.FixedUpdate)), HarmonyPrefix]
    public static bool ViperFixedUpdate(ViperDeadBody __instance)
    {
        if (__instance.victimDissolving && __instance.dissolveCurrentTime > 0f)
        {
            __instance.dissolveCurrentTime -= Time.fixedDeltaTime;
            if (__instance.dissolveCurrentTime <= 0f)
            {
                __instance.myController.DisableCurrentTrackers();
                __instance.victimDissolving = false;
                __instance.myCollider.enabled = false;
                __instance.spriteAnim.gameObject.SetActive(false);
                __instance.myController.cosmetics.HidePetViper();
                if (PlayerControl.LocalPlayer == __instance.myKiller)
                {
                    DataManager.Player.Stats.IncrementStat(StatID.Role_Viper_BodiesDissolved);
                    return false;
                }
            }
            else
            {
                float num = __instance.dissolveCurrentTime / __instance.maxDissolveTime;
                if (num <= 0.8f && num > 0.3f)
                {
                    if (__instance.dissolveStage == 1)
                    {
                        return false;
                    }
                    __instance.dissolveStage = 1;
                    return false;
                }
                else if (num <= 0.3f)
                {
                    if (__instance.dissolveStage == 2)
                    {
                        return false;
                    }
                    __instance.dissolveStage = 2;
                }
            }
        }
        return false;
    }
}
