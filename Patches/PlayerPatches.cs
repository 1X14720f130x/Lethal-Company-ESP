using GameNetcodeStuff;
using HarmonyLib;



[HarmonyPatch]
internal class PlayerPatches
{


    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerControllerB), "Update")]
    static void PlayerInfiniteStaminaPatch(PlayerControllerB __instance)
    {
        if (Settings.MISC.HasInfiniteStamina) __instance.sprintMeter = 1.0f;
    }


    [HarmonyPrefix]
    [HarmonyPatch(typeof(SoundManager), "SetEarsRinging")]
    static bool SkipEarsRingingPatch() => !Settings.MISC.HasAntiFlashProtection; // skip if HasAntiFlashProtection is enabled


    [HarmonyPrefix]
    [HarmonyPatch(typeof(HUDManager), "Update")]
    static void NoFlashPatch(HUDManager __instance)
    {
        if (Settings.MISC.HasAntiFlashProtection) __instance.flashFilter = 0.0f;
    }
}


