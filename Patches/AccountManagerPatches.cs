using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(AccountManager))]
public static class AccountManagerPatches
{
    [HarmonyPatch(nameof(AccountManager.OnSceneLoaded)), HarmonyPostfix]
    public static void OnSceneLoadedPatch(AccountManager __instance)
    {
        //__instance.accountTab.gameObject.SetActive(false);
        __instance.accountTab.transform.GetChild(1).gameObject.SetActive(false);
    }
}
