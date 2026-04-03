using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(FriendsListManager))]
public static class FriendsListManagerPatches
{
    [HarmonyPatch(nameof(FriendsListManager.ReparentUI)), HarmonyPostfix]
    public static void FriendListButtonPatch(FriendsListManager __instance)
    {
        if (__instance.FriendsListButton != null)
        {
            var friendButton = __instance.FriendsListButton.Button;

            var ap_friendListButton = friendButton.GetComponent<AspectPosition>();
            ap_friendListButton.DistanceFromEdge = new Vector3(2.2f, 0.4f, -20f);
            ap_friendListButton.updateAlways = true;

            friendButton.transform.Find("NotifCount").localPosition = new Vector3(-1.2f, -0.4f, -1f);
            friendButton.transform.Find("background").gameObject.SetActive(false);

            var pb_friendButton = friendButton.GetComponent<PassiveButton>();
            pb_friendButton.OnMouseOver = new();
            pb_friendButton.OnMouseOut = new();
            pb_friendButton.OnUpGraphic = false;
            pb_friendButton.OnDownGraphic = false;
            pb_friendButton.inactiveSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("friendlist");
            pb_friendButton.inactiveSprites.transform.localPosition = Vector3.zero;
            pb_friendButton.inactiveSprites.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
            pb_friendButton.activeSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("friendlist");
            pb_friendButton.activeSprites.transform.localPosition = Vector3.zero;
            pb_friendButton.activeSprites.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
            pb_friendButton.selectedSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("friendlist");
            pb_friendButton.selectedSprites.transform.localPosition = Vector3.zero;
            pb_friendButton.selectedSprites.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
        }   
    }
}
