using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(ShipStatus))]
public static class ShipStatusPatches
{
    [HarmonyPatch(nameof(ShipStatus.Awake)), HarmonyPostfix]
    public static void ChangeTaskSprites(ShipStatus __instance)
    {
        if (!__instance.TryCast<SkeldShipStatus>())
            return;

        Sprite targetSprite = null;

        foreach (var rend in GameObject.FindObjectsOfType<SpriteRenderer>())
        {
            if (rend.sprite != null && rend.sprite.name == "room_med")
            {
                rend.sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("room_med_old");
            }
        }

        foreach (var console in __instance.AllConsoles)
        {
            var renderer = console.GetComponent<SpriteRenderer>();

            if (renderer != null && renderer.sprite != null && renderer.sprite.name.ToLowerInvariant() == "panel_datatransfer")
            {
                targetSprite = renderer.sprite;
                break;
            }

            if (targetSprite != null)
                break;
        }

        if (targetSprite == null)
            return;

        foreach (var console in __instance.AllConsoles)
        {
            if (console.TaskTypes.Count > 0 && console.TaskTypes[0] == TaskTypes.UploadData)
            {
                var renderer = console.GetComponent<SpriteRenderer>();

                if (renderer != null)
                {
                    renderer.sprite = targetSprite;
                    console.transform.localScale = Vector3.one;
                }
            }
        }
    }
}
