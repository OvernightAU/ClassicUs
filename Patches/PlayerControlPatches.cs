using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerControl))]
public static class PlayerControlPatches
{
    [HarmonyPatch(nameof(PlayerControl.Awake)), HarmonyPostfix]
    public static void ChangeFontPlayer(PlayerControl __instance)
    {
        var arial = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("ARIAL SDF");
        var fallback = ClassicAssets.ClassicBundle.LoadAsset<Material>("ARIAL Atlas Material");

        foreach (var text in __instance.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text.font != null)
            {
                FontHelper.Replace(text, arial, fallback);
                text.fontSize *= 1.10f;
                text.fontStyle = FontStyles.Normal;
            }
        }
    }
}
