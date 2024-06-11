using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DunGen;
using GameNetcodeStuff;
using UnityEngine;

namespace LC_Internal
{
    readonly struct ComponentRendererPair<T> where T : UnityEngine.Component
    {
        public readonly T Component;
        public readonly Renderer[] Renderers;

        public ComponentRendererPair(T component, Renderer[] renderer)
        {
            Component = component;
            Renderers = renderer;
        }

    }

    sealed class ChamsMod : MonoBehaviour
    {


        private Dictionary<int, Material[]> _materials = new Dictionary<int, Material[]>();
        private HashSet<ComponentRendererPair<UnityEngine.Component>> _componentsRenderersHashSet = new HashSet<ComponentRendererPair<UnityEngine.Component>>();
        public static ChamsMod Instance { get; private set; }

        private static Material _material;

        void Awake()
        {
            if (ChamsMod.Instance == null)
            {
                ChamsMod.Instance = this;
                return;
            }

            ChamsMod.Destroy(ChamsMod.Instance.gameObject);
            ChamsMod.Instance = this;
        }


        void Start()
        {

            _material = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                // Prevents the material from being saved when the scene is saved in the editor.
                // Hides the material from the hierarchy view in the editor.
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };


            // Determines how the source color (the color of the material) is factored into the final color.
            _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            // Determines how the destination color (the color already on the screen) is factored into the final color.
            _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Disabling culling means that both the front and back faces of the polygons will be rendered
            _material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Always be drawn, regardless of depth buffer => render on top of everything 
            _material.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
            // Depth writing means that rendering this material will not affect the depth buffer. 
            _material.SetInt("_ZWrite", 0);

            _material.SetColor("_Color", Settings.CHAMS.HostileColor);

        }

        public void ApplyChams()
        {

            _componentsRenderersHashSet
                .ForEach(pair =>
                {
                    var component = pair.Component;

                    // Verify if the registerd component can be processed depending on the user settings
                    if (component is Turret && !Settings.CHAMS.ShowTurrets) return;
                    if (component is EnemyAI && !Settings.CHAMS.ShowEnemies) return;
                    if (component is Landmine && !Settings.CHAMS.ShowLandmines) return;
                    if (component is SpikeRoofTrap && !Settings.CHAMS.ShowSpikeRoofTraps) return;
                    if (component is PlayerControllerB && !Settings.CHAMS.ShowPlayers) return;
                    if (component is DoorLock && !Settings.CHAMS.ShowLockDoors) return;
                    if (component is GrabbableObject && !Settings.CHAMS.ShowScraps) return;
                    if (component is BreakerBox && !Settings.CHAMS.ShowBreakerBoxes) return;

                    pair.Renderers.ForEach(renderer =>
                    {

                        var instanceID = renderer.GetInstanceID();

                        // Material already changed
                        if (_materials.ContainsKey(instanceID)) return;

                        // Save each material
                        _materials.Add(instanceID, renderer.materials);

                        // Modify material
                        renderer.SetMaterials(Enumerable.Repeat(_material, renderer.materials.Length).ToList());


                    });

                });

        }

        public void RestoreChams()
        {


            _componentsRenderersHashSet
                .ForEach(pair =>
                {
                    // Verify the component 
                    var component = pair.Component;

                    pair.Renderers.ForEach(renderer =>
                    {

                        var instanceID = renderer.GetInstanceID();

                        // check the previous material 
                        if (!_materials.ContainsKey(instanceID)) return;

                        // Modify material with the saved one
                        renderer.SetMaterials(_materials[instanceID].ToList());

                        // remove material
                        _materials.Remove(instanceID);


                    });

                });

        }

        public void StoreRenderer<T>(T component) where T : UnityEngine.Component
        {
            var renderers = component.GetComponentsInParent<Renderer>()
                .Concat(component.GetComponentsInChildren<Renderer>())
                .Distinct();

            _componentsRenderersHashSet.Add(new ComponentRendererPair<UnityEngine.Component>(component, renderers.ToArray()));
        }

#nullable enable


        public void StoreRenderers<T>(IEnumerable<T?> components) where T : UnityEngine.Component
        {

            GameObject gameObject;

            components?
            .WhereIsNotNull()
            .ForEach(component =>
            {

                gameObject = component.gameObject;

                if (component is Turret) gameObject = component.transform.parent.gameObject;

                var renderers = gameObject.GetComponentsInParent<Renderer>()
                .Concat(gameObject.GetComponentsInChildren<Renderer>())
                .Distinct();


                _componentsRenderersHashSet.Add(new ComponentRendererPair<UnityEngine.Component>(component, renderers.ToArray()));

            });



        }


        public void ApplyChams<T>(T[]? components) where T : UnityEngine.Component
        {

            _componentsRenderersHashSet
                .Where(pair => components.Contains(pair.Component))
               .ForEach(pair =>
               {
                   pair.Renderers.ForEach(renderer =>
                   {

                       var instanceID = renderer.GetInstanceID();

                       // Material already changed
                       if (_materials.ContainsKey(instanceID)) return;

                       // Save each material
                       _materials.Add(instanceID, renderer.materials);

                       // Modify material
                       renderer.SetMaterials(Enumerable.Repeat(_material, renderer.materials.Length).ToList());


                   });
               });

        }



        public void RestoreChams<T>(T[]? components) where T : UnityEngine.Component
        {

            _componentsRenderersHashSet
                .Where(pair => components.Contains(pair.Component))
               .ForEach(pair =>
               {

                   pair.Renderers?.ForEach(renderer =>
                   {
                       var instanceID = renderer.GetInstanceID();

                       // check the previous material 
                       if (!_materials.ContainsKey(instanceID)) return;

                       // Modify material with the saved one
                       renderer.SetMaterials(_materials[instanceID].ToList());

                       // remove material
                       _materials.Remove(instanceID);

                   });

               });


        }

#nullable disable


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearRenderers()
        {
            this._componentsRenderersHashSet.Clear();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool isRenderersEmpty()
        {
            return this._componentsRenderersHashSet.Count == 0;
        }

        void OnDestroy()
        {

            this.RestoreChams();

            if (_material != null) Destroy(_material);

        }


    }
}
