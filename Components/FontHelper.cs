using ClassicUs.Assets;
using ClassicUs.Extensions;
using TMPro;
using UnityEngine;

namespace ClassicUs.Components;

public static class FontHelper
{
    public static void Replace(
        TMP_Text text,
        TMP_FontAsset replacement,
        Material fallback = null)
    {
        if (text?.font == null || replacement == null)
            return;

        var originalFontName = text.font.name;

        var material = GetMatchingMaterial(
            text.fontSharedMaterial,
            originalFontName,
            replacement.name,
            fallback ?? replacement.material);

        text.font = replacement;

        if (material != null)
            text.fontSharedMaterial = material;

        text.ForceMeshUpdate();
    }

    private static Material GetMatchingMaterial(
        Material originalMaterial,
        string originalFontName,
        string replacementFontName,
        Material fallback)
    {
        if (originalMaterial == null)
            return fallback;

        var materialName = originalMaterial.name;

        if (string.IsNullOrEmpty(materialName) ||
            !materialName.StartsWith(
                originalFontName,
                System.StringComparison.OrdinalIgnoreCase))
            return fallback;

        var suffix = materialName[originalFontName.Length..];
        var targetName = replacementFontName + suffix;

        return ClassicAssets.ClassicBundle.LoadAsset<Material>(targetName)
               ?? fallback;
    }
}