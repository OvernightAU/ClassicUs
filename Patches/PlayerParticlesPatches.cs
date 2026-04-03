using HarmonyLib;

namespace ClassicUs.Patches;

[HarmonyPatch(typeof(PlayerParticles))]
public static class PlayerParticlesPatches
{
    // Handle scenario where there's no april fools sprite.
    [HarmonyPatch(nameof(PlayerParticles.Start)), HarmonyPrefix]
    public static bool FixNoParticle(PlayerParticles __instance)
    {
        __instance.fill = new RandomFill<PlayerParticleInfo>();
        if (AprilFoolsMode.ShouldHorseAround() && __instance.HorseSprites != null && __instance.HorseSprites.Length > 0)
        {
            __instance.fill.Set(__instance.HorseSprites.TryCast<Il2CppSystem.Collections.Generic.IEnumerable<PlayerParticleInfo>>());
            __instance.pool.Prefab = __instance.HorsePrefab;
            __instance.scaleModifier = 0.7f;
        }
        else if (AprilFoolsMode.ShouldClassicMainMenuMode() && __instance.ClassicSprites != null && __instance.ClassicSprites.Length > 0)
        {
            __instance.fill.Set(__instance.ClassicSprites.TryCast<Il2CppSystem.Collections.Generic.IEnumerable<PlayerParticleInfo>>());
            __instance.scaleModifier = 4f;
        }
        else
        {
            __instance.fill.Set(__instance.Sprites.TryCast<Il2CppSystem.Collections.Generic.IEnumerable<PlayerParticleInfo>>());
            __instance.scaleModifier = 1f;
        }
        int num = 0;
        if (__instance.pool != null)
        {
            while (__instance.pool.NotInUse > 0)
            {
                PlayerParticle playerParticle = __instance.pool.Get<PlayerParticle>();
                PlayerMaterial.SetColors(num++, playerParticle.myRend);
                __instance.PlacePlayer(playerParticle, true);
            }
        }
        return false;
    }
}
