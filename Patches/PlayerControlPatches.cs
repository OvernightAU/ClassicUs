using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerControl))]
public static class PlayerControlPatches
{
    [HarmonyPatch(nameof(PlayerControl.Awake)), HarmonyPostfix]
    public static void ChangeFontPlayer(PlayerControl __instance)
    {
        var font = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("Arial");

        foreach (var text in __instance.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text.font != null)
            {
                text.font = font;

                var mat = UnityEngine.Object.Instantiate(font.material);

                mat.SetFloat(ShaderUtilities.ID_FaceDilate, 0.17f);

                mat.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.24f);
                mat.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0.19f);

                mat.SetFloat(ShaderUtilities.ID_WeightNormal, 0f);
                mat.SetFloat(ShaderUtilities.ID_WeightBold, 0.5f);

                mat.SetFloat(ShaderUtilities.ID_ScaleRatio_A, 1.0f);

                text.fontSharedMaterial = mat;
                text.characterSpacing = -6.5f;
                text.fontSize *= 1.10f;
                text.ForceMeshUpdate();
            }
        }
    }
}
