using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(Minigame))]
public static class MinigamePatches
{
    [HarmonyPatch(nameof(Minigame.Begin)), HarmonyPostfix]
    public static void ChangeFontMinigame(HudManager __instance)
    {
        var font = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("Arial");

        foreach (var text in __instance.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text.font != null && text.font.name.ToLowerInvariant().Contains("liberationsans"))
            {
                text.font = font;
                //text.fontSharedMaterial = font.material;
                text.ForceMeshUpdate();
            }
        }
    }
}
