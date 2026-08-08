using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(MapBehaviour))]
public static class MapBehaviourPatches
{
    [HarmonyPatch(nameof(MapBehaviour.Awake)), HarmonyPostfix]
    public static void ChangeFontMinimap(HudManager __instance)
    {
        var arial = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("ARIAL SDF");
        var fallback = ClassicAssets.ClassicBundle.LoadAsset<Material>("ARIAL SDF RadialMenu Material");

        foreach (var text in __instance.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text.font != null)
            {
                FontHelper.Replace(text, arial, fallback);
            }
        }
    }
}
