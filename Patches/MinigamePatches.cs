using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(Minigame))]
public static class MinigamePatches
{
    [HarmonyPatch(nameof(Minigame.Begin)), HarmonyPostfix]
    public static void ChangeFontMinigame(HudManager __instance)
    {
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
