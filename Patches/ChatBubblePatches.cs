using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(ChatBubble))]
public static class ChatBubblePatches
{
    [HarmonyPatch(nameof(ChatBubble.SetText)), HarmonyPrefix]
    public static void ChangeFontChat(ChatBubble __instance)
    {
        var arial = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("ARIAL SDF");
        var fallback = ClassicAssets.ClassicBundle.LoadAsset<Material>("ARIAL SDF - Chat Message Masked");

        foreach (var text in __instance.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text.font != null && text.font.name.Equals(
                    "LiberationSans SDF",
                    System.StringComparison.OrdinalIgnoreCase) == true)
            {
                FontHelper.Replace(text, arial, fallback);
            }
        }
        __instance.NameText.fontSize *= 1.12f;
        __instance.NameText.fontStyle = FontStyles.Normal;
    }
}
