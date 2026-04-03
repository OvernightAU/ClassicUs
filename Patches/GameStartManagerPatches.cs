using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(GameStartManager))]
public static class GameStartManagerPatches
{
    [HarmonyPatch(nameof(GameStartManager.Start)), HarmonyPostfix]
    public static void LobbyInfoPaneOff(GameStartManager __instance)
    {
        // less cpu cycles
        __instance.LobbyInfoPane.transform.GetChild(0).gameObject.SetActive(false);
    }
}
