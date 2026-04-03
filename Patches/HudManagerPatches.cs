using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(HudManager))]
public static class HudManagerPatches
{
    [HarmonyPatch(nameof(HudManager.Start)), HarmonyPostfix]
    public static void HudInitPatch(HudManager __instance)
    {
        // Settings Button
        var ap_settingsButton = __instance.SettingsButton.GetComponent<AspectPosition>();
        ap_settingsButton.DistanceFromEdge = new Vector3(0.4f, 0.4f, -400f);
        ap_settingsButton.updateAlways = true;

        __instance.SettingsButton.transform.Find("Background").gameObject.SetActive(false);

        var pb_settingsButton = __instance.SettingsButton.GetComponent<PassiveButton>();
        pb_settingsButton.OnMouseOver = new();
        pb_settingsButton.OnMouseOut = new();
        pb_settingsButton.OnUpGraphic = false;
        pb_settingsButton.OnDownGraphic = false;
        pb_settingsButton.inactiveSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("settings_old");
        pb_settingsButton.inactiveSprites.transform.localPosition = Vector3.zero;
        pb_settingsButton.inactiveSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        pb_settingsButton.activeSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("settings_old");
        pb_settingsButton.activeSprites.transform.localPosition = Vector3.zero;
        pb_settingsButton.activeSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        pb_settingsButton.selectedSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("settings_old");
        pb_settingsButton.selectedSprites.transform.localPosition = Vector3.zero;
        pb_settingsButton.selectedSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

        // Map Button
        var ap_mapButton = __instance.MapButton.GetComponent<AspectPosition>();
        ap_mapButton.DistanceFromEdge = new Vector3(0.4f, 1.2f, -20f);
        ap_mapButton.updateAlways = true;

        __instance.MapButton.transform.Find("Background").gameObject.SetActive(false);

        var pb_mapButton = __instance.MapButton.GetComponent<PassiveButton>();
        pb_mapButton.OnMouseOver = new();
        pb_mapButton.OnMouseOut = new();
        pb_mapButton.OnUpGraphic = false;
        pb_mapButton.OnDownGraphic = false;
        pb_mapButton.inactiveSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("mapButtonDark_old");
        pb_mapButton.inactiveSprites.transform.localPosition = Vector3.zero;
        pb_mapButton.inactiveSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        pb_mapButton.activeSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("mapButtonDark_old");
        pb_mapButton.activeSprites.transform.localPosition = Vector3.zero;
        pb_mapButton.activeSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        pb_mapButton.selectedSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("mapButtonDark_old");
        pb_mapButton.selectedSprites.transform.localPosition = Vector3.zero;
        pb_mapButton.selectedSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

        // Chat Button
        var chatButton = __instance.Chat.chatButton.gameObject;
        var ap_chatButton = chatButton.GetComponent<AspectPosition>();
        ap_chatButton.DistanceFromEdge = new Vector3(1.25f, 0.42f, -20f);
        ap_chatButton.updateAlways = true;

        chatButton.transform.Find("Background").gameObject.SetActive(false);

        var pb_chatButton = chatButton.GetComponent<PassiveButton>();
        pb_chatButton.OnMouseOver = new();
        pb_chatButton.OnMouseOut = new();
        pb_chatButton.OnUpGraphic = false;
        pb_chatButton.OnDownGraphic = false;
        pb_chatButton.inactiveSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("chatIcon_old");
        pb_chatButton.inactiveSprites.transform.localPosition = Vector3.zero;
        pb_chatButton.inactiveSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        pb_chatButton.activeSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("chatIcon_old");
        pb_chatButton.activeSprites.transform.localPosition = Vector3.zero;
        pb_chatButton.activeSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        pb_chatButton.selectedSprites.GetComponent<SpriteRenderer>().sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("chatIcon_old");
        pb_chatButton.selectedSprites.transform.localPosition = Vector3.zero;
        pb_chatButton.selectedSprites.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

        var nf_chatButton = chatButton.transform.Find("chatIconNotification");
        nf_chatButton.localPosition = new Vector3(-0.24f, 0.17f, -1f);
        nf_chatButton.localScale = new Vector3(0.35f, 0.35f, 1f);

        // Game Options
        var opts = GameObject.Instantiate(ClassicAssets.ClassicBundle.LoadAsset<GameObject>("GameSettings"), __instance.transform);
        opts.GetComponent<GameSettingsShower>().Target.Value.fontSize = 1.4f;
        opts.GetComponent<GameSettingsShower>().Target.Value.font = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("Arial");
        opts.GetComponent<GameSettingsShower>().Target.Value.ForceMeshUpdate();
    }

    // Font Changes Experiment
    [HarmonyPatch(nameof(HudManager.Start)), HarmonyPostfix]
    public static void ChangeTaskFont(HudManager __instance)
    {
        var font = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("Arial");

        foreach (var text in __instance.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text.font != null && text.font.name.ToLowerInvariant().Contains("liberationsans"))
            {
                if (text == __instance.TaskPanel.taskText || text == __instance.roomTracker.text)
                {
                    text.font = font;

                    var mat = UnityEngine.Object.Instantiate(font.material);

                    mat.SetFloat(ShaderUtilities.ID_FaceDilate, 0.16f);

                    mat.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.22f);
                    mat.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0.17f);

                    mat.SetFloat(ShaderUtilities.ID_WeightNormal, 0f);
                    mat.SetFloat(ShaderUtilities.ID_WeightBold, 0.5f);

                    mat.SetFloat(ShaderUtilities.ID_ScaleRatio_A, 1.0f);

                    text.fontSharedMaterial = mat;
                    text.characterSpacing = -1.5f;
                    text.ForceMeshUpdate();

                    continue;
                }

                text.font = font;
                text.ForceMeshUpdate();
            }
        }
    }
}
