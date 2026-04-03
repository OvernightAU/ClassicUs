using ClassicUs.Assets;
using ClassicUs.Extensions;
using HarmonyLib;
using TMPro;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(ChatBubble))]
public static class ChatBubblePatches
{
    [HarmonyPatch(nameof(ChatBubble.SetText)), HarmonyPrefix]
    public static void ChangeFontChat(ChatBubble __instance)
    {
        var font = ClassicAssets.ClassicBundle.LoadAsset<TMP_FontAsset>("Arial");

        __instance.TextArea.font = font;

        var mat = UnityEngine.Object.Instantiate(font.material);

        mat.SetFloat(ShaderUtilities.ID_FaceDilate, 0.08f);
        mat.SetFloat(ShaderUtilities.ID_OutlineWidth, 0f);

        mat.SetFloat(ShaderUtilities.ID_WeightNormal, 0f);
        mat.SetFloat(ShaderUtilities.ID_WeightBold, 0.5f);

        __instance.TextArea.fontSharedMaterial = mat;

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
