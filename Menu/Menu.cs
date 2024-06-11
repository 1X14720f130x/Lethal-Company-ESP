
using UnityEngine;


// Show cursors 

namespace LC_Internal.Menu
{
    internal class LCmenu : MonoBehaviour
    {

        private Rect menuWindow = new Rect(0, 0, 300, 800);
        public int currentTab = 0;
        public string[] toolbarStrings = new string[] { "ESP", "CHAMS", "MISC" };


        void OnGUI()
        {
            if (!Settings.MENU.ShowMenu) return;

            menuWindow = GUI.Window(0, menuWindow, MenuWindow, "LC_Internal");
        }

        void MenuWindow(int windowID)
        {

            GUI.color = new Color(1f, 1f, 1f, 0.9f);

            currentTab = GUI.Toolbar(new Rect(25, 25, 250, 30), currentTab, toolbarStrings);


            GUILayout.BeginArea(new Rect(25, 80, 180, 800));
            switch (currentTab)
            {
                case 0:
                    DisplayESPMenu();
                    break;
                case 1:
                    DisplayCHAMSMenu();
                    break;
                case 2:
                    DisplayMISCMenu();
                    break;
            }
            GUILayout.EndArea();
            GUI.DragWindow();

        }

        void DisplayESPMenu()
        {
            GUILayout.Space(10f);

            if (GUILayout.Button(string.Format("Enable for enemies: {0}", Settings.ESP.Enemy.Enable ? "ON" : "OFF")))
                Settings.ESP.Enemy.Enable = !Settings.ESP.Enemy.Enable;

            if (GUILayout.Button(string.Format("Enemies names: {0}", Settings.ESP.Enemy.DoText ? "ON" : "OFF")))
                Settings.ESP.Enemy.DoText = !Settings.ESP.Enemy.DoText;

            if (GUILayout.Button(string.Format("Enemies snaplines: {0}", Settings.ESP.Enemy.DoSnapline ? "ON" : "OFF")))
                Settings.ESP.Enemy.DoSnapline = !Settings.ESP.Enemy.DoSnapline;

            if (GUILayout.Button(string.Format("Enemies toplines: {0}", Settings.ESP.Enemy.DoTopline ? "ON" : "OFF")))
                Settings.ESP.Enemy.DoTopline = !Settings.ESP.Enemy.DoTopline;

            if (GUILayout.Button(string.Format("Enemies aimlines: {0}", Settings.ESP.Enemy.DoAimline ? "ON" : "OFF")))
                Settings.ESP.Enemy.DoAimline = !Settings.ESP.Enemy.DoAimline;

            if (GUILayout.Button(string.Format("Enemies 3D Boxes: {0}", Settings.ESP.Enemy.DoShowBoundingBox ? "ON" : "OFF")))
                Settings.ESP.Enemy.DoShowBoundingBox = !Settings.ESP.Enemy.DoShowBoundingBox;

            GUILayout.Space(10f);


            if (GUILayout.Button(string.Format("Enable for players: {0}", Settings.ESP.Player.Enable ? "ON" : "OFF")))
                Settings.ESP.Player.Enable = !Settings.ESP.Player.Enable;

            if (GUILayout.Button(string.Format("Players names: {0}", Settings.ESP.Player.DoText ? "ON" : "OFF")))
                Settings.ESP.Player.DoText = !Settings.ESP.Player.DoText;

            if (GUILayout.Button(string.Format("Players skeletons: {0}", Settings.ESP.Player.DoSkeleton ? "ON" : "OFF")))
                Settings.ESP.Player.DoSkeleton = !Settings.ESP.Player.DoSkeleton;

            if (GUILayout.Button(string.Format("Players 3D Boxes: {0}", Settings.ESP.Player.DoShowBoundingBox ? "ON" : "OFF")))
                Settings.ESP.Player.DoShowBoundingBox = !Settings.ESP.Player.DoShowBoundingBox;

            if (GUILayout.Button(string.Format("Players snaplines: {0}", Settings.ESP.Player.DoSnapline ? "ON" : "OFF")))
                Settings.ESP.Player.DoSnapline = !Settings.ESP.Player.DoSnapline;

            if (GUILayout.Button(string.Format("Players toplines: {0}", Settings.ESP.Player.DoTopline ? "ON" : "OFF")))
                Settings.ESP.Player.DoTopline = !Settings.ESP.Player.DoTopline;

            if (GUILayout.Button(string.Format("Players aimlines: {0}", Settings.ESP.Player.DoAimline ? "ON" : "OFF")))
                Settings.ESP.Player.DoAimline = !Settings.ESP.Player.DoAimline;

            GUILayout.Space(10f);

            if (GUILayout.Button(string.Format("Enable for Scraps: {0}", Settings.ESP.Scrap.Enable ? "ON" : "OFF")))
                Settings.ESP.Scrap.Enable = !Settings.ESP.Scrap.Enable;

            if (GUILayout.Button(string.Format("Scraps info: {0}", Settings.ESP.Scrap.DoText ? "ON" : "OFF")))
                Settings.ESP.Scrap.DoText = !Settings.ESP.Scrap.DoText;

            if (GUILayout.Button(string.Format("Scraps snaplines: {0}", Settings.ESP.Scrap.DoSnapline ? "ON" : "OFF")))
                Settings.ESP.Scrap.DoSnapline = !Settings.ESP.Scrap.DoSnapline;

            if (GUILayout.Button(string.Format("Scraps toplines: {0}", Settings.ESP.Scrap.DoTopline ? "ON" : "OFF")))
                Settings.ESP.Scrap.DoTopline = !Settings.ESP.Scrap.DoTopline;

            if (GUILayout.Button(string.Format("Scraps aimlines: {0}", Settings.ESP.Scrap.DoAimline ? "ON" : "OFF")))
                Settings.ESP.Scrap.DoAimline = !Settings.ESP.Scrap.DoAimline;

            GUILayout.Space(10f);

            if (GUILayout.Button(string.Format("Enable for Turrets: {0}", Settings.ESP.Turret.Enable ? "ON" : "OFF")))
                Settings.ESP.Turret.Enable = !Settings.ESP.Turret.Enable;

            if (GUILayout.Button(string.Format("Turrets aimpoint : {0}", Settings.ESP.Turret.DrawAimPoint ? "ON" : "OFF")))
                Settings.ESP.Turret.DrawAimPoint = !Settings.ESP.Turret.DrawAimPoint;

            if (GUILayout.Button(string.Format("Turrets 3D boxes : {0}", Settings.ESP.Turret.DoShowBoundingBox ? "ON" : "OFF")))
                Settings.ESP.Turret.DoShowBoundingBox = !Settings.ESP.Turret.DoShowBoundingBox;

            GUILayout.Space(10f);

            if (GUILayout.Button(string.Format("Enable for Entrance: {0}", Settings.ESP.Entrance.Enable ? "ON" : "OFF")))
                Settings.ESP.Entrance.Enable = !Settings.ESP.Entrance.Enable;

            if (GUILayout.Button(string.Format("Entrance position {0}", Settings.ESP.Entrance.DoText ? "ON" : "OFF")))
                Settings.ESP.Entrance.DoText = !Settings.ESP.Entrance.DoText;

            GUILayout.Space(10f);

            if (GUILayout.Button(string.Format("Enable for Landmines: {0}", Settings.ESP.Landmine.Enable ? "ON" : "OFF")))
                Settings.ESP.Landmine.Enable = !Settings.ESP.Landmine.Enable;

            if (GUILayout.Button(string.Format("Landmines position {0}", Settings.ESP.Landmine.DoText ? "ON" : "OFF")))
                Settings.ESP.Landmine.DoText = !Settings.ESP.Landmine.DoText;

            if (GUILayout.Button(string.Format("Landmines 3D boxes {0}", Settings.ESP.Landmine.DoShowBoundingBox ? "ON" : "OFF")))
                Settings.ESP.Landmine.DoShowBoundingBox = !Settings.ESP.Landmine.DoShowBoundingBox;


        }

        void DisplayCHAMSMenu()
        {
            GUILayout.Space(10f);

            if (GUILayout.Button(string.Format("Show Players: {0}", Settings.CHAMS.ShowPlayers ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowPlayers = !Settings.CHAMS.ShowPlayers;

                if (GameListener.InGame)
                {
                    if (!Settings.CHAMS.ShowPlayers) ChamsMod.Instance.RestoreChams(Helper.Players);
                    else ChamsMod.Instance.ApplyChams(Helper.Players);
                }

            }

            if (GUILayout.Button(string.Format("Show Scraps: {0}", Settings.CHAMS.ShowScraps ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowScraps = !Settings.CHAMS.ShowScraps;

                if (GameListener.InGame)
                {
                    if (!Settings.CHAMS.ShowScraps) ChamsMod.Instance.RestoreChams(Helper.Grabbables);
                    else ChamsMod.Instance.ApplyChams(Helper.Grabbables);
                }

            }

            if (GUILayout.Button(string.Format("Show Enemies: {0}", Settings.CHAMS.ShowEnemies ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowEnemies = !Settings.CHAMS.ShowEnemies;


                if (GameListener.IsMapLoaded)
                {
                    if (!Settings.CHAMS.ShowEnemies) ChamsMod.Instance.RestoreChams(Helper.Enemies.ToArray());
                    else ChamsMod.Instance.ApplyChams(Helper.Enemies.ToArray());
                }

            }

            if (GUILayout.Button(string.Format("Show Turrets: {0}", Settings.CHAMS.ShowTurrets ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowTurrets = !Settings.CHAMS.ShowTurrets;

                if (GameListener.IsMapLoaded)
                {
                    if (!Settings.CHAMS.ShowTurrets) ChamsMod.Instance.RestoreChams(Helper.Turrets);
                    else ChamsMod.Instance.ApplyChams(Helper.Turrets);
                }
            }

            if (GUILayout.Button(string.Format("Show Breaker Boxes: {0}", Settings.CHAMS.ShowBreakerBoxes ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowBreakerBoxes = !Settings.CHAMS.ShowBreakerBoxes;


                if (GameListener.IsMapLoaded)
                {
                    if (!Settings.CHAMS.ShowBreakerBoxes) ChamsMod.Instance.RestoreChams(Helper.BreakerBoxes);
                    else ChamsMod.Instance.ApplyChams(Helper.BreakerBoxes);
                }

            }

            if (GUILayout.Button(string.Format("Show Spike Roof Traps: {0}", Settings.CHAMS.ShowSpikeRoofTraps ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowSpikeRoofTraps = !Settings.CHAMS.ShowSpikeRoofTraps;

                if (GameListener.IsMapLoaded)
                {
                    if (!Settings.CHAMS.ShowSpikeRoofTraps) ChamsMod.Instance.RestoreChams(Helper.SpikeRoofTraps);
                    else ChamsMod.Instance.ApplyChams(Helper.SpikeRoofTraps);
                }

            }

            if (GUILayout.Button(string.Format("Show Landmines: {0}", Settings.CHAMS.ShowLandmines ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowLandmines = !Settings.CHAMS.ShowLandmines;

                if (GameListener.IsMapLoaded)
                {
                    if (!Settings.CHAMS.ShowLandmines) ChamsMod.Instance.RestoreChams(Helper.Landmines);
                    else ChamsMod.Instance.ApplyChams(Helper.Landmines);
                }

            }

            if (GUILayout.Button(string.Format("Show Doors: {0}", Settings.CHAMS.ShowLockDoors ? "ON" : "OFF")))
            {
                Settings.CHAMS.ShowLockDoors = !Settings.CHAMS.ShowLockDoors;

                if (GameListener.IsMapLoaded)
                {
                    if (!Settings.CHAMS.ShowLockDoors) ChamsMod.Instance.RestoreChams(Helper.LockDoors);
                    else ChamsMod.Instance.ApplyChams(Helper.LockDoors);
                }

            }


        }

        void DisplayMISCMenu()
        {
            GUILayout.Space(10f);

            if (GUILayout.Button(string.Format("Infinite Stamina: {0}", Settings.MISC.HasInfiniteStamina ? "ON" : "OFF")))
                Settings.MISC.HasInfiniteStamina = !Settings.MISC.HasInfiniteStamina;

            if (GUILayout.Button(string.Format("Anti-Flash Protection: {0}", Settings.MISC.HasAntiFlashProtection ? "ON" : "OFF")))
                Settings.MISC.HasAntiFlashProtection = !Settings.MISC.HasAntiFlashProtection;
        }
    }
}
