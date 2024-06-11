using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


/* Cheats definied settings here */
static internal class Settings
{

    static internal class MENU
    {
        public static bool ShowMenu = true;

    }

    static internal class ESP
    {

        static internal class Scrap
        {
            public static bool Enable = true;
            public static bool DoText = true;
            public static bool DoSnapline = false;
            public static bool DoTopline = false;
            public static bool DoAimline = false;
            public static Color Color = Color.yellow;
        }

        static internal class Turret
        {
            public static bool Enable = true;
            public static bool DrawAimPoint = true;
            public static bool DoShowBoundingBox = true;
            public static Color Color = Color.red;
        }

        static internal class Landmine
        {
            public static bool Enable = true;
            public static bool DoText = true;
            public static bool DoShowBoundingBox = true;
            public static Color Color = Color.red;
        }

        static internal class Entrance
        {
            public static bool Enable = true;
            public static Color Color = Color.grey;
            public static bool DoText = true;

        }

        static internal class Enemy
        {
            public static bool Enable = true; // Do not work ??
            public static bool DoSnapline = false;
            public static bool DoTopline = false;
            public static bool DoAimline = false;
            public static bool DoShowBoundingBox = true;
            public static bool DoText = true;
            public static Color Color = Color.red;
        }

        static internal class Player
        {
            public static bool Enable = true;
            public static bool DoSnapline = false;
            public static bool DoTopline = false;
            public static bool DoAimline = false;
            public static bool DoShowBoundingBox = false;
            public static bool DoSkeleton = true;
            public static bool DoText = true;
            public static Color Color = Color.green;

        }
    }

    static internal class MISC
    {
        public static bool HasAntiFlashProtection = false;
        public static bool HasInfiniteStamina = false;
    }

    static internal class CHAMS
    {
        public static Color HostileColor = new Color(0.125f, 0.573f, 0.631f, 0.3f); // magenta transparent

        public static bool ShowBreakerBoxes = false;
        public static bool ShowPlayers = false;
        public static bool ShowLockDoors = false;
        public static bool ShowLandmines = false;
        public static bool ShowSpikeRoofTraps = false;
        public static bool ShowEnemies = false;
        public static bool ShowScraps = false;
        public static bool ShowTurrets = false;


    }

}

