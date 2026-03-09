using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClassicUs.Extensions;

public static class ClassicExtensions
{
    public static T? LoadAsset<T>(this AssetBundle bundle, string name) where T : Object
    {
        return bundle.LoadAsset(name, Il2CppType.Of<T>())?.Cast<T>();
    }

    public static unsafe System.Span<T> ToSpan<T>(this Il2CppStructArray<T> array) where T : unmanaged
    {
        return new System.Span<T>(System.IntPtr.Add(array.Pointer, System.IntPtr.Size * 4).ToPointer(), array.Length);
    }
}
