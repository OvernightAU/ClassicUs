using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(ChatNotification))]
public static class ChatNotificationPatches
{
    [HarmonyPatch(nameof(ChatNotification.SetUp)), HarmonyPrefix]
    public static bool DisableChatNotification()
    {
        return false;
    }
}
