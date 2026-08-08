using BepInEx;
using BepInEx.Unity.IL2CPP;
using ClassicUs.Assets;
using ClassicUs.Components;
using ClassicUs.Extensions;
using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using System;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClassicUs;

[BepInAutoPlugin("com.auad.classicus")]
[BepInProcess("Among Us.exe")]
public partial class ClassicUsPlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public static ClassicUsPlugin Instance { get; private set; }

    public override void Load()
    {
        Instance = this;
        Harmony.PatchAll();

        ClassInjector.RegisterTypeInIl2Cpp<GameSettingsShower>();
        ClassInjector.RegisterTypeInIl2Cpp<PopulateFreeplayPopover>();

        SceneManager.add_sceneLoaded((System.Action<Scene, LoadSceneMode>)((scene, _) =>
        {
            if (scene.name == "MainMenu")
            {

                GameObject.Find("MainUI").SetActive(false);
                GameObject.Find("PlayerParticles").SetActive(false);

                GameObject.Instantiate(ClassicAssets.ClassicBundle.LoadAsset<GameObject>("ClassicMenu"));

                /*
                                var announceButton = GameObject.Find("AnnounceButton").GetComponent<PassiveButton>();
                                announceButton.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
                                announceButton.OnClick.AddListener(new System.Action(() => GameObject.FindObjectOfType<MainMenuManager>().announcementPopUp.Show()));

                                var optionsButton = GameObject.Find("1OptionsButton").GetComponent<PassiveButton>();
                                optionsButton.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
                                optionsButton.OnClick.AddListener(new System.Action(() => GameObject.FindAnyObjectByType<OptionsMenuBehaviour>(FindObjectsInactive.Include).Open()));

                                var storeButton = GameObject.Find("StoreButton").GetComponent<PassiveButton>();
                                storeButton.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
                                storeButton.OnClick.AddListener(new System.Action(() => GameObject.FindObjectOfType<MainMenuManager>().TransitionToShop()));

                                var invButton = GameObject.Find("InventoryButton").GetComponent<PassiveButton>();
                                invButton.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
                                invButton.OnClick.AddListener(new System.Action(() => GameObject.FindObjectOfType<MainMenuManager>().TransitionToInventory()));
                    */

                StringBuilder logBuilder = new();
                ClassicAssets.ClassicScenesBundle.GetAllScenePaths().ToList().ForEach(e => logBuilder.AppendLine(e));
                ClassicAssets.ClassicScenesBundle.GetAllAssetNames().ToList().ForEach(e => logBuilder.AppendLine(e));
                Log.LogInfo(logBuilder);
            }

            if (scene.name != "OnlineGame" && scene.name != "Tutorial")
            {
                var font = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("ARIAL SDF");
                var fallbackMaterial = ClassicAssets.ClassicBundle.LoadAsset<Material>("ARIAL Atlas Material");

                var arialMaterials = ClassicAssets.ClassicBundle
                    .LoadAllAssets(Il2CppType.Of<Material>())
                    .Where(x => x != null)
                    .ToDictionary(x => x.name, x => x.TryCast<Material>(), StringComparer.OrdinalIgnoreCase);

                foreach (var text in GameObject.FindObjectsOfType<TMP_Text>(true))
                {
                    if (text.font == null ||
                        !text.font.name.Contains("LiberationSans", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var oldMaterialName = text.fontSharedMaterial?.name;

                    Material targetMaterial = fallbackMaterial;

                    if (!string.IsNullOrEmpty(oldMaterialName))
                    {
                        var liberationIndex = oldMaterialName.IndexOf(
                            "LiberationSans",
                            StringComparison.OrdinalIgnoreCase);

                        if (liberationIndex >= 0)
                        {
                            var suffix = oldMaterialName[
                                (liberationIndex + "LiberationSans".Length)..];

                            var targetName = "ARIAL" + suffix;

                            if (arialMaterials.TryGetValue(targetName, out var matchingMaterial))
                                targetMaterial = matchingMaterial;
                        }
                    }

                    text.font = font;
                    text.fontSharedMaterial = targetMaterial;
                    text.ForceMeshUpdate();
                }
            }
        }));
    }
}