using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DunGen;
using GameNetcodeStuff;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace LC_Internal
{
    public readonly struct PlayerRendererPair<T, R> where T : UnityEngine.Object where R : Renderer
    {
        public T GameObject { get; init; }
        public R Renderer { get; init; }

        public PlayerRendererPair(T gameObject, R renderer)
        {
            GameObject = gameObject;
            Renderer = renderer;
        }

    }
    static partial class Helper
    {
#nullable enable
        internal static PlayerControllerB? LocalPlayer => Helper.StartOfRound?.localPlayerController.Unfake();
        internal static PlayerControllerB[] Players => Helper.StartOfRound?.allPlayerScripts ?? [];
        internal static PlayerControllerB[] ActivePlayers => Helper.Players.Where(player => player.isPlayerControlled && !player.isPlayerDead).ToArray();
        internal static List<EnemyAI>? Enemies = new List<EnemyAI>();
        internal static PlayerRendererPair<PlayerControllerB, SkinnedMeshRenderer>[]? PlayerRendererPairs;
        internal static Landmine[]? Landmines;
        internal static EntranceTeleport[]? EntranceDoors;
        internal static DoorLock[]? LockDoors;
        internal static BreakerBox[]? BreakerBoxes;
        internal static SpikeRoofTrap[]? SpikeRoofTraps;
        internal static Turret[]? Turrets;
        internal static GrabbableObject[]? Grabbables;
#nullable disable

    }
}
