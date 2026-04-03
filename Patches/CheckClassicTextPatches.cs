using AmongUs.Data;
using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(CheckClassicText))]
public static class CheckClassicTextPatches
{
    // This fixes broken Innersloth code that makes the meeting text appear twice in non-english.
    [HarmonyPatch(nameof(CheckClassicText.OnEnable)), HarmonyPostfix]
    public static void CheckClassicTextFix(CheckClassicText __instance)
    {
        if (DataManager.Settings.Language.CurrentLanguage != SupportedLangs.English)
        {
            __instance.reportedImage.SetActive(false);
            __instance.reportedText.SetActive(true);
        }
    }
}
