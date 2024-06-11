using GameNetcodeStuff;
using HarmonyLib;
using System;


[HarmonyPatch]
class GameStatePatches
{
#nullable enable
    internal static event Action? OnFinishLevelGeneration;
    internal static event Action? OnPlayerConnection;
    internal static event Action? OnShipLeave;
    internal static event Action? OnDisconnection;
    internal static event Action<EnemyAI>? OnEnemyStart;
#nullable disable

    [HarmonyPostfix]
    [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.FinishGeneratingNewLevelClientRpc))]
    static void FinishGeneratingNewLevelClientPatch() => GameStatePatches.OnFinishLevelGeneration?.Invoke();


    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.ConnectClientToPlayerObject))]
    static void ConnectClientToPlayerObjectPatch() => GameStatePatches.OnPlayerConnection?.Invoke();


    [HarmonyPostfix]
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.EndOfGameClientRpc))]
    static void EndOfGamePatch() => GameStatePatches.OnShipLeave?.Invoke();


    [HarmonyPostfix]
    [HarmonyPatch(typeof(GameNetworkManager), nameof(GameNetworkManager.Disconnect))]
    static void DisconnectPatch() => GameStatePatches.OnDisconnection?.Invoke();


    [HarmonyPostfix]
    [HarmonyPatch(typeof(EnemyAI), nameof(EnemyAI.Start))]
    static void StartPatch(EnemyAI __instance) => GameStatePatches.OnEnemyStart?.Invoke(__instance);

}