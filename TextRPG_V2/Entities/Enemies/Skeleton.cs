using System;

namespace TextRPG_V2
{
    public class Skeleton : Entity
    {
        private bool moveRight;

        /// <summary>
        /// Empty constructor for a "Skeleton" enemy type.
        /// </summary>
        public Skeleton() : base()
        {
            SetName("Skeleton");
            SetSymbol('S');
            SetColor(ConsoleColor.White);
            SetFaction(Faction.undead);
            SetMagic(false);

            health = new HealthSystem(13);
            atk = new Stat(8);
            def = new Stat(14);
            mag = new Stat(2);
            res = new Stat(4);
            spd = new Stat(4);
            skl = new Stat(5);
            luc = new Stat(2);
            gld = new Stat(5);
        }

        /// <summary>
        /// Method for the "Skeleton" type enemy to choose its action.
        /// </summary>
        /// <param name="map">The map on which the skeleton exists</param>
        /// <param name="startPos">The position on which the skeleton starts its turn</param>
        /// <param name="uiManager">The manager for UI class objects</param>
        /// <param name="itemManager">The manager for Item class objects</param>
        /// <returns>String containing a description of the action</returns>
        public override string ChooseAction(Map map, int[] startPos, UIManager uiManager, ItemManager itemManager, QuestManager questManager)
        {
            //check move right
            if (moveRight)
            {
                int[] rightPos = { startPos[0], startPos[1] + 1 };

                //check for impassible tile or entity with same faction
                if (map.GetTile(rightPos).GetImpassable() || map.GetEntity(rightPos) != null && map.GetEntity(rightPos).GetFaction() == Faction.undead)
                {
                    moveRight = false;
                    int[] leftPos = { startPos[0], startPos[1] - 1 };

                    //attempt move left
                    return Move(map, startPos, leftPos, uiManager, itemManager, questManager);
                }
                else
                {
                    //attempt move right
                    return Move(map, startPos, rightPos, uiManager, itemManager, questManager);
                }
            }
            //check move left
            else
            {
                int[] leftPos = { startPos[0], startPos[1] - 1 };

                //check for impassible tile or entity with same faction
                if (map.GetTile(leftPos).GetImpassable() || map.GetEntity(leftPos) != null && map.GetEntity(leftPos).GetFaction() == Faction.undead)
                {
                    moveRight = true;
                    int[] rightPos = { startPos[0], startPos[1] + 1 };

                    //attempt move right
                    return Move(map, startPos, rightPos, uiManager, itemManager, questManager);
                }
                else
                {
                    //attempt move left
                    return Move(map, startPos, leftPos, uiManager, itemManager, questManager);
                }
            }
        }
    }
}
