using System;
using UnityEngine;
using HarmonyLib;
using LC_Internal.Core;
using LC_Internal.Menu;
using System.IO;
using System.Reflection;
using System.Linq;




namespace LC_Internal
{
    public class Loader : MonoBehaviour
    {
        const string HarmonyID = "lc_internal1.0";

        static bool HasLoaded => Harmony.HasAnyPatches(Loader.HarmonyID);

        static GameObject InternalGameObjects { get; } = new("Internal GameObjects");
        static GameObject InternalModules { get; } = new("Internal Modules");

        static void AddInternalModules<T>() where T : Component => Loader.InternalModules.AddComponent<T>();
        static void AddInternalGameObject<T>() where T : Component => Loader.InternalGameObjects.AddComponent<T>();

        static internal void LoadLibraries()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            var resourceNames =
                assembly.GetManifestResourceNames()
                        .Where(name => name.EndsWith(".dll"))
                        .ToArray();

            foreach (string resourceName in resourceNames)
            {
                using Stream stream = assembly.GetManifestResourceStream(resourceName);
                using MemoryStream memoryStream = new();
                stream.CopyTo(memoryStream);
                _ = AppDomain.CurrentDomain.Load(memoryStream.ToArray());
            }
        }


        static internal void Load()
        {
            LoadLibraries();
;

            if (Loader.HasLoaded) return;

            Loader.LoadHarmonyPatches();
            Loader.LoadInternalGameObjects();
            Loader.LoadInternalModules();
#if DEBUG
            Debug.Log("LC_INTERNAL IS LOADED !!");
#endif
        }

        static internal void LoadHarmonyPatches()
        {
            try
            {
                new Harmony(Loader.HarmonyID).PatchAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static void LoadInternalModules()
        {
            DontDestroyOnLoad(Loader.InternalModules);

            Loader.AddInternalModules<ESPMod>();
            Loader.AddInternalModules<ChamsMod>();
        }

        static void LoadInternalGameObjects()
        {
            DontDestroyOnLoad(Loader.InternalGameObjects);

            Loader.AddInternalGameObject<InputListener>();
            Loader.AddInternalGameObject<GameListener>();
            Loader.AddInternalGameObject<MapEntitiesUpdater>();
            Loader.AddInternalGameObject<LCmenu>();

        }

        internal static void Unload()
        {

#if DEBUG
            Debug.Log("LC_INTERNAL Unloading !!");
#endif

            Destroy(Loader.InternalModules);
            Destroy(Loader.InternalGameObjects);
            new Harmony(Loader.HarmonyID).UnpatchSelf();
        }
    }
}
