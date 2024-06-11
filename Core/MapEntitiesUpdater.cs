using System.Linq;
using GameNetcodeStuff;
using UnityEngine;

namespace LC_Internal.Core
{
    internal class MapEntitiesUpdater : MonoBehaviour
    {

        void OnEnable()
        {

            // Injection after the map loaded
            if (GameListener.IsMapLoaded) this.initializeMapEntities();

            GameListener.OnLevelGenerated += this.initializeMapEntities;
            GameListener.OnGameStart += this.initializeMapEntities;
            GameListener.OnGameEnd += this.ClearMapRenderers;
            GameListener.OnPlayerDisconnect += this.ClearMapRenderers;
            GameListener.OnEnnemyJoin += AddEnemyRenderer;
        }

        void OnDisable()
        {
            GameListener.OnLevelGenerated -= this.initializeMapEntities;
            GameListener.OnGameStart -= this.initializeMapEntities;
            GameListener.OnGameEnd -= this.ClearMapRenderers;
            GameListener.OnPlayerDisconnect -= this.ClearMapRenderers;
            GameListener.OnEnnemyJoin -= AddEnemyRenderer;

        }


        internal void AddEnemyRenderer(EnemyAI __instance)
        {

            Helper.Enemies.Add(__instance);

            if (__instance is DocileLocustBeesAI || __instance is RedLocustBees || __instance is ButlerBeesEnemyAI)
                return;

            ChamsMod.Instance.StoreRenderer(__instance);

            if (Settings.CHAMS.ShowEnemies)
                ChamsMod.Instance.ApplyChams(Helper.Enemies.ToArray());


        }

        internal void ClearMapRenderers()
        {


            ChamsMod.Instance.RestoreChams();
            ChamsMod.Instance.ClearRenderers();

            Helper.Enemies.Clear();

            Helper.Landmines = [];
            Helper.EntranceDoors = [];
            Helper.Turrets = [];
            Helper.LockDoors = [];
            Helper.BreakerBoxes = [];
            Helper.SpikeRoofTraps = [];
        }

        internal void initializeMapEntities()
        {

            Helper.PlayerRendererPairs = Helper.ActivePlayers
                .Where(player => player != Helper.LocalPlayer)
                .Select(player => new PlayerRendererPair<PlayerControllerB, SkinnedMeshRenderer>(player, player.thisPlayerModel))
                        .ToArray();


            Helper.Grabbables = GameListener.InGame ? Helper.FindObjects<GrabbableObject>() : [];
            Helper.Landmines = GameListener.IsMapLoaded ? Helper.FindObjects<Landmine>() : [];
            Helper.EntranceDoors = GameListener.IsMapLoaded ? Helper.FindObjects<EntranceTeleport>() : [];
            Helper.Turrets = GameListener.IsMapLoaded ? Helper.FindObjects<Turret>() : [];
            Helper.LockDoors = GameListener.IsMapLoaded ? Helper.FindObjects<DoorLock>() : [];
            Helper.BreakerBoxes = GameListener.IsMapLoaded ? Helper.FindObjects<BreakerBox>() : [];
            Helper.SpikeRoofTraps = GameListener.IsMapLoaded ? Helper.FindObjects<SpikeRoofTrap>() : [];


            this.registerMapRenderers();



        }


        internal void registerMapRenderers()
        {

            ChamsMod.Instance.StoreRenderers(Helper.Landmines);
            ChamsMod.Instance.StoreRenderers(Helper.Turrets);
            ChamsMod.Instance.StoreRenderers(Helper.LockDoors);
            ChamsMod.Instance.StoreRenderers(Helper.BreakerBoxes);
            ChamsMod.Instance.StoreRenderers(Helper.SpikeRoofTraps);
            ChamsMod.Instance.StoreRenderers(Helper.Grabbables);
            ChamsMod.Instance.StoreRenderers(Helper.PlayerRendererPairs.Select(pair => pair.GameObject).ToArray());
            ChamsMod.Instance.ApplyChams();
        }


    }
}
