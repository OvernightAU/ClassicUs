using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(MainMenuManager))]
public static class MainMenuManagerPatches
{
    [HarmonyPatch(nameof(MainMenuManager.ActivateMainMenuUI)), HarmonyPrefix]
    public static bool DisableActivateMainMenuUI()
    {
        return false;
    }
}
