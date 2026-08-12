using AmongUs.Data;
using ClassicUs.Components;
using HarmonyLib;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(FreeplayPopover))]
public static class FreeplayPopoverPatches
{
    public const float relativeX = 0f;
    public const float ySeparation = -0.65f;
    public const float startingYOffset = 3.5f;

    // This patch is meant to override any changes that Submerged may try to do in the FreePlay PopOver content.
    [HarmonyPatch(nameof(FreeplayPopover.Show)), HarmonyPostfix, HarmonyPriority(Priority.VeryLow)]
    public static void FixButtonPositions(FreeplayPopover __instance)
    {
        var content = __instance.transform.Find("Content");
        var childCount = content.childCount;

        for (int i = 0; i < childCount; i++)
        {
            var button = content.GetChild(i);

            if (button.name.ToLower().Contains("background")) continue;

            float targetY = startingYOffset + (i * ySeparation);
            button.transform.localPosition = new Vector3(relativeX, targetY, 0);
        }
    }
}
