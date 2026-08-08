using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(MeetingHud))]
public static class MeetingHudPatches
{
    [HarmonyPatch(nameof(MeetingHud.Start)), HarmonyPostfix]
    public static void RemoveMeetingBG(MeetingHud __instance)
    {
        __instance.BlackBackground.sprite = null;

        var arial = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("ARIAL SDF");
        var fallback = ClassicAssets.ClassicBundle.LoadAsset<Material>("ARIAL SDF RadialMenu Material");

        foreach (var text in __instance.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text.font != null && text.font.name.Equals(
                    "LiberationSans SDF",
                    System.StringComparison.OrdinalIgnoreCase) == true)
            {
                FontHelper.Replace(text, arial, fallback);
            }
        }
    }
}
