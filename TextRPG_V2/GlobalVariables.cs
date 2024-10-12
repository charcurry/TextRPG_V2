using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{
    public static class GlobalVariables
    {
        //Entity Variables

        //Actions / Turns (TODO: fix AP system to make updates faster)
        public const int actionThreshold = 10;

        //dodge
        public const int baseDodge = 50;
        public const int dodgeSpdWeight = 2;
        public const int dodgeSklWeight = 2;
        public const int dodgeLecWeight = 1;

        //hit
        public const int hitSklWeight = 3;
        public const int hitSpdWeight = 1;
        public const int hitLucWeight = 1;

        //avoid
        public const float critLucWeight = 0.4f;
        public const float critSklWeight = 0.1f;



        //Items
        public const int potionHealingValue = 10;
        public const int swordAtkIncrease = 5;
        public const float goldLucWeight = 0.5f;
        public const int goldValue = 100;
        public const int minItemCost = 10;
        public const int maxItemCost = 50;

        //Quests
        public const int enemiesToKill = 10;
        public const int itemsToPurchase = 3;



        //Map Variables
        public const string filename = "Dungeon.txt";
        public const string directory = @"Maps\MapFiles\";



        //Camera
        public const int cameraHeight = 21;
        public const int cameraWidth = 31;



        //UIWindows
        public const int windowPadding = 1;

        public const int statWindowHeight = 12;
        public const int statWindowWidth = 20;

        public const int enemyStatWindowHeight = 11;
        public const int enemyStatWindowWidth = 20;

        public const int controlsWindowHeight = 12;
        public const int controlsWindowWidth = 22;

        public const int questsWindowHeight = 11;
        public const int questsWindowWidth = 22;

        public const int EventLogWindowHeight = 12;
        public const int EventLogWindowWidth = 77;
        public const int maxEventLogMessage = 10;
    }
}
