using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LC_Internal;
using UnityEngine;

internal class GameListener : MonoBehaviour
{
#nullable enable
    internal static event Action? OnLevelGenerated;
    internal static event Action? OnGameStart;
    internal static event Action? OnGameEnd;
    internal static event Action? OnPlayerDisconnect;
    internal static event Action<EnemyAI>? OnEnnemyJoin;
#nullable disable


    static public bool InGame => Helper.LocalPlayer is not null;
    static public bool InShipPhase => Helper.StartOfRound?.inShipPhase ?? false;
    static public bool shipIsLeaving => Helper.StartOfRound?.shipIsLeaving ?? false;
    static public bool IsMapLoaded => GameListener.InGame && !GameListener.InShipPhase;

    void OnEnable()
    {
        GameStatePatches.OnFinishLevelGeneration += () => GameListener.OnLevelGenerated?.Invoke();
        GameStatePatches.OnPlayerConnection += () => GameListener.OnGameStart?.Invoke();
        GameStatePatches.OnShipLeave += () => GameListener.OnGameEnd?.Invoke();
        GameStatePatches.OnDisconnection += () => GameListener.OnPlayerDisconnect?.Invoke();
        GameStatePatches.OnEnemyStart += (__instance) => GameListener.OnEnnemyJoin?.Invoke(__instance);
    }

    void OnDisable()
    {
        GameStatePatches.OnFinishLevelGeneration -= () => GameListener.OnLevelGenerated?.Invoke();
        GameStatePatches.OnPlayerConnection -= () => GameListener.OnGameStart?.Invoke();
        GameStatePatches.OnShipLeave -= () => GameListener.OnGameEnd?.Invoke();
        GameStatePatches.OnDisconnection -= () => GameListener.OnPlayerDisconnect?.Invoke();
        GameStatePatches.OnEnemyStart -= (__instance) => GameListener.OnEnnemyJoin?.Invoke(__instance);
    }


}
