using UnityEngine;

namespace ClassicUs.Assets;

public static class ClassicAssets
{
    public static readonly AssetBundle ClassicBundle = AssetBundleManager.Load("classicus");
    public static readonly AssetBundle ClassicScenesBundle = AssetBundleManager.Load("classic_scenes");
}