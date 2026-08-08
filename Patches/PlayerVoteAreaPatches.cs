using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerVoteArea))]
public static class PlayerVoteAreaPatches
{
    [HarmonyPatch(nameof(PlayerVoteArea.Start)), HarmonyPrefix]
    public static void ChangeVoteAreaFont(PlayerVoteArea __instance)
    {
        __instance.NameText.transform.localScale = Vector3.one;
        __instance.NameText.fontStyle = FontStyles.Normal;
    }
}