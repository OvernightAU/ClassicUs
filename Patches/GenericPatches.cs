using AmongUs.Data;
using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using Hazel;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using InnerNet;
using PowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ClassicUs.Patches;

// Warning, this might be removed or inlined eventually.
[HarmonyPatch(typeof(AprilFoolsMode), nameof(AprilFoolsMode.ShouldClassicMode))]
public static class AlwaysClassic
{
    public static bool Prefix(out bool __result)
    {
        __result = true;
        return false;
    }
}
[HarmonyPatch(typeof(PlayerAnimations), nameof(PlayerAnimations.SetBodyType))]
public static class PlayerAnimationsSetBodyType
{
    public static void Prefix(PlayerAnimations __instance, [HarmonyArgument(0)] PlayerBodyTypes type)
    {
        var group = (int)type;

        // Not needed anymore, we just replace normal with Classic
        if (type == PlayerBodyTypes.Seeker)
        {
            //__instance.animationGroups[group].NodeSyncs = __instance.animationGroups[(int)PlayerBodyTypes.Classic].NodeSyncs;
            __instance.animationGroups[group].IdleAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].IdleAnim;
            __instance.animationGroups[group].RunAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].RunAnim;
            __instance.animationGroups[group].SpawnAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].SpawnAnim;
            __instance.animationGroups[group].GhostIdleAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].GhostIdleAnim;
            __instance.animationGroups[group].GhostGuardianAngelAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].GhostGuardianAngelAnim;
            __instance.animationGroups[group].EnterVentAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].EnterVentAnim;
            __instance.animationGroups[group].ExitVentAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].ExitVentAnim;
            __instance.animationGroups[group].ClimbUpAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].ClimbUpAnim;
            __instance.animationGroups[group].ClimbDownAnim = __instance.animationGroups[(int)PlayerBodyTypes.Classic].ClimbDownAnim;
            __instance.animationGroups[group].defaultPlayerScale = __instance.animationGroups[(int)PlayerBodyTypes.Classic].defaultPlayerScale;
            __instance.transform.GetParent().GetComponent<PlayerControl>().cosmetics.currentBodySprite.BodySprite.material.SetColor("_VisorColor", Color.green);
        }
    }
}
[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.SetBodyType))]
public static class PlayerPhysicsSetBodyType
{
    public static void Prefix(PlayerPhysics __instance, [HarmonyArgument(0)] ref PlayerBodyTypes type)
    {
        if (type == PlayerBodyTypes.Normal)
        {
            type = PlayerBodyTypes.Classic;
        }
    }

    public static void Postfix(PlayerPhysics __instance)
    {
        __instance.myPlayer.cosmetics.SetScale(__instance.Animations.DefaultPlayerScale, __instance.myPlayer.defaultCosmeticsScale);
    }
}
[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.AnimateCustom))]
public static class PlayerPhysicsAnimateCustom
{
    public static bool Prefix(PlayerPhysics __instance)
    {
        return false;
    }
}
[HarmonyPatch(typeof(GameManager), nameof(GameManager.Awake))]
public static class NormalGameManagerAwake
{
    public static void Postfix(NormalGameManager __instance)
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
[HarmonyPatch(typeof(ViperDeadBody), nameof(ViperDeadBody.FixedUpdate))]
public static class ViperFixedUpdate
{
    public static bool Prefix(ViperDeadBody __instance)
    {
        if (__instance.victimDissolving && __instance.dissolveCurrentTime > 0f)
        {
            __instance.dissolveCurrentTime -= Time.fixedDeltaTime;
            if (__instance.dissolveCurrentTime <= 0f)
            {
                __instance.myController.DisableCurrentTrackers();
                __instance.victimDissolving = false;
                __instance.myCollider.enabled = false;
                __instance.spriteAnim.gameObject.SetActive(false);
                __instance.myController.cosmetics.HidePetViper();
                if (PlayerControl.LocalPlayer == __instance.myKiller)
                {
                    DataManager.Player.Stats.IncrementStat(StatID.Role_Viper_BodiesDissolved);
                    return false;
                }
            }
            else
            {
                float num = __instance.dissolveCurrentTime / __instance.maxDissolveTime;
                if (num <= 0.8f && num > 0.3f)
                {
                    if (__instance.dissolveStage == 1)
                    {
                        return false;
                    }
                    __instance.dissolveStage = 1;
                    return false;
                }
                else if (num <= 0.3f)
                {
                    if (__instance.dissolveStage == 2)
                    {
                        return false;
                    }
                    __instance.dissolveStage = 2;
                }
            }
        }
        return false;
    }
}
[HarmonyPatch(typeof(LightSource), nameof(LightSource.Initialize))]
public static class LightSourcePatch
{
    public static void Prefix(LightSource __instance)
    {
        __instance.rendererType = LightSourceRendererType.Raycast;
    }
}
[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.ActivateMainMenuUI))]
public static class DisableActivateMainMenuUI
{
    public static bool Prefix()
    {
        return false;
    }
}
[HarmonyPatch(typeof(AccountManager), nameof(AccountManager.OnSceneLoaded))]
public static class OnSceneLoadedPatch
{
    public static void Postfix(AccountManager __instance)
    {
        //__instance.accountTab.gameObject.SetActive(false);
        __instance.accountTab.transform.GetChild(1).gameObject.SetActive(false);
    }
}
[HarmonyPatch(typeof(AssetReference), nameof(AssetReference.RuntimeKeyIsValid))]
public static class PreviewDataRuntimeKeyPatch
{
    public static bool Prefix(AssetReference __instance, ref bool __result)
    {
        /*
        if (__instance.AssetGUID.StartsWith("corsac."))
        {
            __result = true;
            return false;
        }
        */

        __result = true;
        return false;
    }
}
/*
[HarmonyPatch(typeof(PoolablePlayer), nameof(PoolablePlayer.InitBody))]
public static class InitBodyPatch
{
    public static void Prefix(PoolablePlayer __instance)
    {
        var bodyForms = __instance.transform.Find("BodyForms");
        if (bodyForms != null)
        {
            var normal = bodyForms.Find("Normal");
            if (normal != null)
            {
                if (normal.TryGetComponent<SpriteRenderer>(out var rend))
                {
                    if (rend.sprite.name == "idleNoshadow")
                    {
                        rend.sprite = ClassicAssets.ClassicBundle.LoadAsset<Sprite>("idle_old_sprite");
                        rend.gameObject.transform.localScale = rend.gameObject.transform.localScale * 2f;
                    }
                }
            }
        }
    }
}
*/
[HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
public static class HudInitPatch
{
    public static void Postfix(HudManager __instance)
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
    }
}
[HarmonyPatch(typeof(FriendsListManager), nameof(FriendsListManager.ReparentUI))]
public static class FriendListButtonPatch
{
    public static void Postfix(FriendsListManager __instance)
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
[HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Start))]
public static class LobbyInfoPaneOff
{
    public static void Postfix(GameStartManager __instance)
    {
        // less cpu cycles
        __instance.LobbyInfoPane.transform.GetChild(0).gameObject.SetActive(false);
    }
}
/*
[HarmonyPatch(typeof(HatManager), nameof(HatManager.Initialize))]
public static class HatManagerChangeHats
{
    public static void Prefix(HatManager __instance)
    {
        AssetBundle bundle = ClassicAssets.ClassicBundle;
        if (bundle == null) return;

        List<HatData> bundleHats = [];

        foreach (string assetPath in bundle.GetAllAssetNames())
        {
            if (!assetPath.StartsWith("cosmetics/"))
                continue;

            HatData hat = bundle.LoadAsset<HatData>(assetPath);
            if (hat != null)
                bundleHats.Add(hat);
        }

        if (bundleHats.Count == 0) return;

        Dictionary<string, HatData> existing = __instance.allHats
            .ToDictionary(h => h.ProductId, h => h);

        foreach (HatData newHat in bundleHats)
        {
            if (existing.TryGetValue(newHat.ProductId, out HatData oldHat))
            {
                int index = __instance.allHats.IndexOf(oldHat);
                __instance.allHats[index] = newHat;
            }
        }
    }
}
*/
[HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
public static class RemoveMeetingBG
{
    public static void Postfix(MeetingHud __instance)
    {
        __instance.BlackBackground.sprite = null;
    }
}
[HarmonyPatch(typeof(CheckClassicText), nameof(CheckClassicText.OnEnable))]
public static class CheckClassicTextFix
{
    // This fixes broken Innersloth code that makes the meeting text appear twice in non-english.

    public static void Postfix(CheckClassicText __instance)
    {
        if (DataManager.Settings.Language.CurrentLanguage != SupportedLangs.English)
        {
            __instance.reportedImage.SetActive(false);
            __instance.reportedText.SetActive(true);
        }
    }
}
[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Awake))]
public static class ChangeTaskSprites
{
    public static void Postfix(ShipStatus __instance)
    {
        if (!__instance.TryCast<SkeldShipStatus>())
            return;

        Sprite targetSprite = null;

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