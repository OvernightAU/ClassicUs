using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(MeetingHud))]
public static class MeetingHudPatches
{
    [HarmonyPatch(nameof(MeetingHud.Start)), HarmonyPostfix]
    public static void RemoveMeetingBG(MeetingHud __instance)
    {
        __instance.BlackBackground.sprite = null;
    }
}
