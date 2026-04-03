using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerVoteArea))]
public static class PlayerVoteAreaPatches
{
    [HarmonyPatch(nameof(PlayerVoteArea.Start)), HarmonyPrefix]
    public static void ChangeVoteAreaFont(PlayerVoteArea __instance)
    {
        var font = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("Arial");

        __instance.NameText.font = font;

        var mat2 = UnityEngine.Object.Instantiate(font.material);

        // Thickness
        mat2.SetFloat(ShaderUtilities.ID_FaceDilate, 0.14f);

        mat2.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.23f);
        mat2.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0.18f);

        mat2.SetFloat(ShaderUtilities.ID_WeightNormal, 0f);
        mat2.SetFloat(ShaderUtilities.ID_WeightBold, 0.5f);

        mat2.SetFloat(ShaderUtilities.ID_ScaleRatio_A, 1.0f);

        __instance.NameText.fontSharedMaterial = mat2;
        __instance.NameText.characterSpacing = -4.2f;
        __instance.NameText.ForceMeshUpdate();
    }
}
