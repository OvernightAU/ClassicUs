using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ClassicUs.Assets;

public static class ClassicAssets
{
    public static readonly AssetBundle ClassicBundle = AssetBundleManager.Load("classicus");
    public static readonly AssetBundle ClassicScenesBundle = AssetBundleManager.Load("classic_scenes");
}