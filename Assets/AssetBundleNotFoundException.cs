using System.IO;

namespace ClassicUs.Assets;

/// <summary>
/// The exception that is thrown when an asset bundle is not found.
/// </summary>
public class AssetBundleNotFoundException : IOException
{
    internal AssetBundleNotFoundException(string name) : base("Couldn't find an assetbundle named " + name)
    {
    }
}
