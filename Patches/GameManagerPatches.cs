using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using PowerTools;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(GameManager))]
public static class GameManagerPatches
{
    [HarmonyPatch(nameof(GameManager.Awake)), HarmonyPostfix]
    public static void NormalGameManagerAwake(NormalGameManager __instance)
    {
        foreach (var body in __instance.deadBodyPrefab)
        {
            body.bloodSplatter.material = DestroyableSingleton<HatManager>.Instance.DefaultShader;
            body.bloodSplatter.color = Color.red;

            var sprite = body.transform.Find("Sprite");
            sprite.GetComponent<SpriteAnim>().m_defaultAnim = ClassicAssets.ClassicBundle.LoadAsset<AnimationClip>("DeadA_Old");

            float oldScale = sprite.localScale.x;
            sprite.localScale = Vector3.one;

            for (int i = 0; i < sprite.childCount; i++)
            {
                var child = sprite.GetChild(i);
                child.localScale = child.localScale * oldScale;
            }
        }
    }
}
