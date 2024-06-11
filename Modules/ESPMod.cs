
using System;
using System.Collections.Generic;
using System.Linq;
using GameNetcodeStuff;
using LC_Internal;
using LC_Internal.Core;
using UnityEngine;
using static Settings.ESP;





sealed class ESPMod : MonoBehaviour
{

    void OnGUI()
    {

        if (!GameListener.InGame) return;

        if (Helper.CurrentCamera is not Camera camera) return;

        if (Settings.ESP.Scrap.Enable)
            drawGrabbalesESP(camera, Settings.ESP.Scrap.Color);

        if (Settings.ESP.Player.Enable)
            drawPlayersESP(camera, Settings.ESP.Player.Color);


        if (!GameListener.IsMapLoaded) return;

        // ----- After the map is loaded ----- //
        if (Settings.ESP.Enemy.Enable)
            drawEnemiesESP(camera, Settings.ESP.Enemy.Color);

        if (Settings.ESP.Turret.Enable)
            drawTurretsESP(camera, Settings.ESP.Turret.Color);

        if (Settings.ESP.Landmine.Enable)
            drawLandminesESP(camera, Settings.ESP.Landmine.Color);

        if (Settings.ESP.Entrance.Enable)
            drawEntranceDoorsESP(camera, Settings.ESP.Entrance.Color);

    }

    void drawPlayersESP(Camera camera, Color color)
    {
        Helper.PlayerRendererPairs?.ForEach(playerRendererPair =>
        {

            Vector3 rendererCentrePoint = camera.WorldToScreen(playerRendererPair.GameObject.transform.position);

            if (rendererCentrePoint.z < 2.00f) return;

            if (Settings.ESP.Player.DoText)
                DrawUtilities.DrawLabel(rendererCentrePoint, $"{playerRendererPair.GameObject.playerUsername} : HP {playerRendererPair.GameObject.health}", color);

            if (Settings.ESP.Player.DoSnapline)
                DrawUtilities.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);

            if (Settings.ESP.Player.DoTopline)
                DrawUtilities.DrawLine(new Vector2(Screen.width / 2, 0), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);

            if (Settings.ESP.Player.DoAimline)
                DrawUtilities.DrawLine(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);

            if (Settings.ESP.Player.DoShowBoundingBox)
            {
                Vector3[] corners = DrawUtilities.BoundsToCorners(playerRendererPair.Renderer.bounds);

                // Convert corners to 2D position
                for (int i = 0; i < corners.Length; i++)
                    corners[i] = camera.WorldToScreen(corners[i]);


                // Draw Bottom rectangle
                DrawUtilities.DrawLine(corners[0], corners[1], color, 2.00f); //  Bottom-Front-Left -> Bottom-Back-Left
                DrawUtilities.DrawLine(corners[1], corners[2], color, 2.00f); //  Bottom-Back-Left -> Bottom-Back-Right
                DrawUtilities.DrawLine(corners[2], corners[3], color, 2.00f); //  Bottom-Back-Right -> Bottom-Front-Right
                DrawUtilities.DrawLine(corners[3], corners[0], color, 2.00f); //  Bottom-Front-Right -> Bottom-Front-Left

                // Draw Top rectangle 
                DrawUtilities.DrawLine(corners[4], corners[5], color, 2.00f); //  Top-Front-Left -> Top-Back-Left
                DrawUtilities.DrawLine(corners[5], corners[6], color, 2.00f); //  Top-Back-Left -> Top-Back-Right
                DrawUtilities.DrawLine(corners[6], corners[7], color, 2.00f); //  Top-Back-Right -> Top-Front-Right
                DrawUtilities.DrawLine(corners[7], corners[4], color, 2.00f); //  Top-Front-Right -> Top-Front-Left

                // Link rectangles 
                DrawUtilities.DrawLine(corners[0], corners[4], color, 2.00f); //  Bottom-Front-Left -> Top-Back-Left
                DrawUtilities.DrawLine(corners[1], corners[5], color, 2.00f); //  Bottom-Back-Left -> Top-Back-Right
                DrawUtilities.DrawLine(corners[2], corners[6], color, 2.00f); //  Bottom-Back-Right -> Top-Front-Right
                DrawUtilities.DrawLine(corners[3], corners[7], color, 2.00f); //  Bottom-Front-Right -> Top-Front-Left

            }

            if (Settings.ESP.Player.DoSkeleton)
            {
                Transform[] bones = playerRendererPair.Renderer.bones;
                Vector3[] bonesPosition = new Vector3[bones.Length];

                // Convert to 2D positions
                for (int i = 0; i < bones.Length; i++)
                {
                    bonesPosition[i] = bones[i].position;
                    bonesPosition[i] = camera.WorldToScreen(bonesPosition[i]);
                }

                // Spine drawing
                DrawUtilities.DrawLine(bonesPosition[0], bonesPosition[1], color, 1.00f); //  spine -> spine.001
                DrawUtilities.DrawLine(bonesPosition[1], bonesPosition[2], color, 1.00f); //  spine.001 -> spine.002
                DrawUtilities.DrawLine(bonesPosition[2], bonesPosition[3], color, 1.00f); //  spine.002 -> spine.003
                DrawUtilities.DrawLine(bonesPosition[3], bonesPosition[32], color, 1.00f); //  spine.003 -> spine.004

                // Connect Shoulders
                DrawUtilities.DrawLine(bonesPosition[3], bonesPosition[4], color, 1.00f); //  spine.003 -> shoulder.L
                DrawUtilities.DrawLine(bonesPosition[3], bonesPosition[18], color, 1.00f); //  spine.003 -> shoulder.R

                // Shoulder left drawing
                DrawUtilities.DrawLine(bonesPosition[4], bonesPosition[5], color, 1.00f); //  shoulder.L -> arm.L_upper
                DrawUtilities.DrawLine(bonesPosition[5], bonesPosition[6], color, 1.00f); //  arm.L_upper -> arm.L_lower
                DrawUtilities.DrawLine(bonesPosition[6], bonesPosition[7], color, 1.00f); //  arm.L_lower -> hand.L

                // Shoulder right drawing
                DrawUtilities.DrawLine(bonesPosition[18], bonesPosition[19], color, 1.00f); //  shoulder.R -> arm.R_upper
                DrawUtilities.DrawLine(bonesPosition[19], bonesPosition[20], color, 1.00f); //  arm.R_upper -> arm.R_lower
                DrawUtilities.DrawLine(bonesPosition[20], bonesPosition[21], color, 1.00f); //  arm.R_lower -> hand.R

                // Hand left drawing
                DrawUtilities.DrawLine(bonesPosition[7], bonesPosition[8], color, 1.00f); //  hand.L -> finger1.L
                DrawUtilities.DrawLine(bonesPosition[8], bonesPosition[9], color, 1.00f); //  finger1.L -> finger1.L.001
                DrawUtilities.DrawLine(bonesPosition[7], bonesPosition[10], color, 1.00f); //  hand.L -> finger2.L
                DrawUtilities.DrawLine(bonesPosition[10], bonesPosition[11], color, 1.00f); //  finger2.L -> finger2.L.001
                DrawUtilities.DrawLine(bonesPosition[7], bonesPosition[12], color, 1.00f); //  hand.L -> finger3.L
                DrawUtilities.DrawLine(bonesPosition[12], bonesPosition[13], color, 1.00f); //  finger3.L -> finger3.L.001
                DrawUtilities.DrawLine(bonesPosition[7], bonesPosition[14], color, 1.00f); //  hand.L -> finger4.L
                DrawUtilities.DrawLine(bonesPosition[14], bonesPosition[15], color, 1.00f); //  finger4.L -> finger4.L.001
                DrawUtilities.DrawLine(bonesPosition[7], bonesPosition[16], color, 1.00f); //  hand.L -> finger5.L
                DrawUtilities.DrawLine(bonesPosition[16], bonesPosition[17], color, 1.00f); //  finger5.L -> finger5.L.001

                // Hand right drawing
                DrawUtilities.DrawLine(bonesPosition[21], bonesPosition[22], color, 1.00f); //  hand.R -> finger1.R
                DrawUtilities.DrawLine(bonesPosition[22], bonesPosition[23], color, 1.00f); //  finger1.R -> finger1.R.001
                DrawUtilities.DrawLine(bonesPosition[21], bonesPosition[24], color, 1.00f); //  hand.R -> finger2.R
                DrawUtilities.DrawLine(bonesPosition[24], bonesPosition[25], color, 1.00f); //  finger2.R -> finger2.R.001
                DrawUtilities.DrawLine(bonesPosition[21], bonesPosition[26], color, 1.00f); //  hand.R -> finger3.R
                DrawUtilities.DrawLine(bonesPosition[26], bonesPosition[27], color, 1.00f); //  finger3.R -> finger3.R.001
                DrawUtilities.DrawLine(bonesPosition[21], bonesPosition[28], color, 1.00f); //  hand.R -> finger4.R
                DrawUtilities.DrawLine(bonesPosition[28], bonesPosition[29], color, 1.00f); //  finger4.R -> finger4.R.001
                DrawUtilities.DrawLine(bonesPosition[21], bonesPosition[30], color, 1.00f); //  hand.R -> finger5.R
                DrawUtilities.DrawLine(bonesPosition[30], bonesPosition[31], color, 1.00f); //  finger5.R -> finger5.R.001

                // Draw Left Leg
                DrawUtilities.DrawLine(bonesPosition[33], bonesPosition[34], color, 1.00f); //  thigh.L -> shin.L
                DrawUtilities.DrawLine(bonesPosition[34], bonesPosition[35], color, 1.00f); //  shin.L -> foot.L
                DrawUtilities.DrawLine(bonesPosition[35], bonesPosition[36], color, 1.00f); //  foot.L -> toe.L
                DrawUtilities.DrawLine(bonesPosition[36], bonesPosition[37], color, 1.00f); //  toe.L -> heel.02.L
                DrawUtilities.DrawLine(bonesPosition[37], bonesPosition[35], color, 1.00f); //  heel.02.L -> foot.L


                // Draw Right Leg
                DrawUtilities.DrawLine(bonesPosition[38], bonesPosition[39], color, 1.00f); //  thigh.R -> shin.R
                DrawUtilities.DrawLine(bonesPosition[39], bonesPosition[40], color, 1.00f); //  shin.R -> foot.R
                DrawUtilities.DrawLine(bonesPosition[40], bonesPosition[41], color, 1.00f); //  foot.R -> toe.R
                DrawUtilities.DrawLine(bonesPosition[41], bonesPosition[42], color, 1.00f); //  toe.R -> heel.02.R
                DrawUtilities.DrawLine(bonesPosition[42], bonesPosition[40], color, 1.00f); //  heel.02.R -> foot.R

                // Connect Legs to Spine
                DrawUtilities.DrawLine(bonesPosition[33], bonesPosition[0], color, 1.00f); //  thigh.L -> spine
                DrawUtilities.DrawLine(bonesPosition[38], bonesPosition[0], color, 1.00f); //  thigh.R -> spine
            }

        });
    }

    void drawGrabbalesESP(Camera camera, Color color)
    {
        List<Material> Materials = new List<Material>();

        Helper.Grabbables?
            .WhereIsNotNull()
            .Where(scrap => scrap.IsSpawned && !scrap.isHeld || !scrap.isHeldByEnemy)
            .ForEach(scrap =>
        {

            Vector3 rendererCentrePoint = camera.WorldToScreen(scrap.transform.position);

            if (rendererCentrePoint.z < 1.00f) return;

            if (Settings.ESP.Scrap.DoText)
                DrawUtilities.DrawLabel(rendererCentrePoint, $"{scrap.itemProperties.itemName} : {scrap.scrapValue}$", color);

            if (Settings.ESP.Scrap.DoSnapline)
                DrawUtilities.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);

            if (Settings.ESP.Scrap.DoTopline)
                DrawUtilities.DrawLine(new Vector2(Screen.width / 2, 0), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);

            if (Settings.ESP.Scrap.DoAimline)
                DrawUtilities.DrawLine(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);


        });



        return;


    }

    void drawEnemiesESP(Camera camera, Color color)
    {
        Helper.Enemies?
            .WhereIsNotNull()
            .Where(enemy => enemy.IsSpawned && !enemy.isEnemyDead)
            .ForEach(enemy =>
            {

                Renderer mesh = enemy.meshRenderers.FirstOrDefault();

                Vector3 rendererCentrePoint = camera.WorldToScreen(mesh?.transform?.position ?? enemy.transform.position);

                if (rendererCentrePoint.z < 3.00f) return;

                if (Settings.ESP.Enemy.DoText)
                    DrawUtilities.DrawLabel(rendererCentrePoint, $"{enemy.enemyType.enemyName} : HP {enemy.enemyHP}", color);

                if (Settings.ESP.Enemy.DoSnapline)
                    DrawUtilities.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);

                if (Settings.ESP.Enemy.DoTopline)
                    DrawUtilities.DrawLine(new Vector2(Screen.width / 2, 0), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);

                if (Settings.ESP.Enemy.DoAimline)
                    DrawUtilities.DrawLine(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(rendererCentrePoint.x, rendererCentrePoint.y), color, 2.00f);


                if (Settings.ESP.Enemy.DoShowBoundingBox)
                {
                    SkinnedMeshRenderer renderer = enemy.skinnedMeshRenderers.FirstOrDefault();

                    if (renderer != null)
                    {
                        Vector3[] corners = DrawUtilities.BoundsToCorners(renderer.bounds);

                        // Convert corners to 2D position
                        for (int i = 0; i < corners.Length; i++)
                            corners[i] = camera.WorldToScreen(corners[i]);

                        // Draw Bottom rectangle
                        DrawUtilities.DrawLine(corners[0], corners[1], color, 2.00f); //  Bottom-Front-Left -> Bottom-Back-Left
                        DrawUtilities.DrawLine(corners[1], corners[2], color, 2.00f); //  Bottom-Back-Left -> Bottom-Back-Right
                        DrawUtilities.DrawLine(corners[2], corners[3], color, 2.00f); //  Bottom-Back-Right -> Bottom-Front-Right
                        DrawUtilities.DrawLine(corners[3], corners[0], color, 2.00f); //  Bottom-Front-Right -> Bottom-Front-Left

                        // Draw Top rectangle 
                        DrawUtilities.DrawLine(corners[4], corners[5], color, 2.00f); //  Top-Front-Left -> Top-Back-Left
                        DrawUtilities.DrawLine(corners[5], corners[6], color, 2.00f); //  Top-Back-Left -> Top-Back-Right
                        DrawUtilities.DrawLine(corners[6], corners[7], color, 2.00f); //  Top-Back-Right -> Top-Front-Right
                        DrawUtilities.DrawLine(corners[7], corners[4], color, 2.00f); //  Top-Front-Right -> Top-Front-Left

                        // Link rectangles 
                        DrawUtilities.DrawLine(corners[0], corners[4], color, 2.00f); //  Bottom-Front-Left -> Top-Back-Left
                        DrawUtilities.DrawLine(corners[1], corners[5], color, 2.00f); //  Bottom-Back-Left -> Top-Back-Right
                        DrawUtilities.DrawLine(corners[2], corners[6], color, 2.00f); //  Bottom-Back-Right -> Top-Front-Right
                        DrawUtilities.DrawLine(corners[3], corners[7], color, 2.00f); //  Bottom-Front-Right -> Top-Front-Left

                    }

                }

            });

    }

    void drawTurretsESP(Camera camera, Color color)
    {
        Helper.Turrets?
            .WhereIsNotNull()
            .ForEach(turret =>
            {
                Vector3 rendererCentrePoint = camera.WorldToScreen(turret.transform.position);

                if (rendererCentrePoint.z < 1.00f) return;

                DrawUtilities.DrawLabel(rendererCentrePoint, $"Turret ({turret.turretActive})", color);

                if (Settings.ESP.Turret.DrawAimPoint)
                {
                    Vector3 centrePoint = camera.WorldToScreen(turret.centerPoint.position);
                    Vector3 aimPoint = camera.WorldToScreen(turret.aimPoint.position);

                    if (aimPoint.z > 1.00f)
                        DrawUtilities.DrawLine(centrePoint, aimPoint, color, 1.00f);
                }

                if (Settings.ESP.Turret.DoShowBoundingBox)
                {
                    Renderer renderer = turret.GetComponent<Renderer>();

                    Vector3[] corners = DrawUtilities.BoundsToCorners(renderer.bounds);

                    // Convert corners to 2D position
                    for (int i = 0; i < corners.Length; i++)
                        corners[i] = camera.WorldToScreen(corners[i]);

                    // Draw Bottom rectangle
                    DrawUtilities.DrawLine(corners[0], corners[1], color, 2.00f); //  Bottom-Front-Left -> Bottom-Back-Left
                    DrawUtilities.DrawLine(corners[1], corners[2], color, 2.00f); //  Bottom-Back-Left -> Bottom-Back-Right
                    DrawUtilities.DrawLine(corners[2], corners[3], color, 2.00f); //  Bottom-Back-Right -> Bottom-Front-Right
                    DrawUtilities.DrawLine(corners[3], corners[0], color, 2.00f); //  Bottom-Front-Right -> Bottom-Front-Left

                    // Draw Top rectangle 
                    DrawUtilities.DrawLine(corners[4], corners[5], color, 2.00f); //  Top-Front-Left -> Top-Back-Left
                    DrawUtilities.DrawLine(corners[5], corners[6], color, 2.00f); //  Top-Back-Left -> Top-Back-Right
                    DrawUtilities.DrawLine(corners[6], corners[7], color, 2.00f); //  Top-Back-Right -> Top-Front-Right
                    DrawUtilities.DrawLine(corners[7], corners[4], color, 2.00f); //  Top-Front-Right -> Top-Front-Left

                    // Link rectangles 
                    DrawUtilities.DrawLine(corners[0], corners[4], color, 2.00f); //  Bottom-Front-Left -> Top-Back-Left
                    DrawUtilities.DrawLine(corners[1], corners[5], color, 2.00f); //  Bottom-Back-Left -> Top-Back-Right
                    DrawUtilities.DrawLine(corners[2], corners[6], color, 2.00f); //  Bottom-Back-Right -> Top-Front-Right
                    DrawUtilities.DrawLine(corners[3], corners[7], color, 2.00f); //  Bottom-Front-Right -> Top-Front-Left
                }



            });
    }

    void drawLandminesESP(Camera camera, Color color)
    {
        Helper.Landmines?
            .WhereIsNotNull()
            .Where(landmine => !landmine.hasExploded)
            .ForEach(landmine =>
            {
                Vector3 rendererCentrePoint = camera.WorldToScreen(landmine.transform.position);

                if (rendererCentrePoint.z < 1.00f) return;

                if (Settings.ESP.Landmine.DoText)
                    DrawUtilities.DrawLabel(rendererCentrePoint, "Landmine", color);

                if (Settings.ESP.Landmine.DoShowBoundingBox)
                {
                    Renderer renderer = landmine.GetComponent<Renderer>();

                    Vector3[] corners = DrawUtilities.BoundsToCorners(renderer.bounds);

                    // Convert corners to 2D position
                    for (int i = 0; i < corners.Length; i++)
                        corners[i] = camera.WorldToScreen(corners[i]);

                    // Draw Bottom rectangle
                    DrawUtilities.DrawLine(corners[0], corners[1], color, 2.00f); //  Bottom-Front-Left -> Bottom-Back-Left
                    DrawUtilities.DrawLine(corners[1], corners[2], color, 2.00f); //  Bottom-Back-Left -> Bottom-Back-Right
                    DrawUtilities.DrawLine(corners[2], corners[3], color, 2.00f); //  Bottom-Back-Right -> Bottom-Front-Right
                    DrawUtilities.DrawLine(corners[3], corners[0], color, 2.00f); //  Bottom-Front-Right -> Bottom-Front-Left

                    // Draw Top rectangle 
                    DrawUtilities.DrawLine(corners[4], corners[5], color, 2.00f); //  Top-Front-Left -> Top-Back-Left
                    DrawUtilities.DrawLine(corners[5], corners[6], color, 2.00f); //  Top-Back-Left -> Top-Back-Right
                    DrawUtilities.DrawLine(corners[6], corners[7], color, 2.00f); //  Top-Back-Right -> Top-Front-Right
                    DrawUtilities.DrawLine(corners[7], corners[4], color, 2.00f); //  Top-Front-Right -> Top-Front-Left

                    // Link rectangles 
                    DrawUtilities.DrawLine(corners[0], corners[4], color, 2.00f); //  Bottom-Front-Left -> Top-Back-Left
                    DrawUtilities.DrawLine(corners[1], corners[5], color, 2.00f); //  Bottom-Back-Left -> Top-Back-Right
                    DrawUtilities.DrawLine(corners[2], corners[6], color, 2.00f); //  Bottom-Back-Right -> Top-Front-Right
                    DrawUtilities.DrawLine(corners[3], corners[7], color, 2.00f); //  Bottom-Front-Right -> Top-Front-Left
                }

            });
    }

    void drawEntranceDoorsESP(Camera camera, Color color)
    {
        Helper.EntranceDoors?
            .WhereIsNotNull()
            .ForEach(door =>
            {
                Vector3 rendererCentrePoint = camera.WorldToScreen(door.entrancePoint.position);

                if (rendererCentrePoint.z < 1.00f) return;

                if (Settings.ESP.Entrance.DoText)
                    DrawUtilities.DrawLabel(rendererCentrePoint, "Entrance", color);
            });
    }
}

