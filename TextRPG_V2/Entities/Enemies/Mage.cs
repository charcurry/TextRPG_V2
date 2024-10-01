using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{
    internal class Mage : Entity
    {
        private int range = 5;

        /// <summary>
        /// Empty constructor for a "Mage" enemy type.
        /// </summary>
        public Mage() : base()
        {
            SetName("Mage");
            SetSymbol('M');
            SetColor(ConsoleColor.Cyan);
            SetFaction(Faction.wizards);
            SetMagic(true);

            health = new HealthSystem(5);
            atk = new Stat(2);
            def = new Stat(5);
            mag = new Stat(10);
            res = new Stat(20);
            spd = new Stat(5);
            skl = new Stat(10);
            luc = new Stat(5);
            gld = new Stat(12);
        }

        /// <summary>
        /// Method for the "Mage" type enemy to choose its action. The behaviour is to randomly wander unless an enemy of the mage is in range.
        /// </summary>
        /// <param name="map">The map on which the mage exists</param>
        /// <param name="startPos">The position on which the mage starts its turn</param>
        /// <param name="uiManager">The manager for UI class objects</param>
        /// <param name="itemManager">The manager for Item class objects</param>
        /// <returns>String containing a description of the action</returns>
        public override string ChooseAction(Map map, int[] startPos, UIManager uiManager, ItemManager itemManager)
        {
            //checking range for target
            for (int y = -range; y<= range; y++)
            {
                //height boudry check
                if (startPos[0] + y > 0 && startPos[0] + y < map.GetHeight())
                {
                    for (int x = -range; x<= range; x++)
                    {
                        //width boundry check
                        if (startPos[1] + x > 0 && startPos[1] + x < map.GetWidth())
                        {
                            //check for non wizard entity and attack if true
                            int[] index = new int[] { startPos[0] + y, startPos[1] + x };
                            if (map.GetEntity(index) != null && map.GetEntity(index).GetFaction() != GetFaction())
                            {
                                return Move(map, startPos, index, uiManager, itemManager);
                            }
                        }
                    }
                }
            }

            //setting potential positions
            int[] endPos = new int[2];

            int[] posNorth = { startPos[0] - 1, startPos[1] };
            int[] posSouth = { startPos[0] + 1, startPos[1] };
            int[] posEast = { startPos[0], startPos[1] - 1 };
            int[] posWest = { startPos[0], startPos[1] + 1 };

            //generate random choice
            int choice = rnd.Next(0, 4);

            //set end pos
            switch (choice)
            {
                case 0:
                    endPos = posNorth;
                    break;

                case 1:
                    endPos = posSouth;
                    break;

                case 2:
                    endPos = posEast;
                    break;

                case 3:
                    endPos = posWest;
                    break;
            }

            //check that no entity is in target position (would only be a mage)
            if (map.GetEntity(endPos) != null)
            {
                return null;
            }

            //move
            return Move(map, startPos, endPos, uiManager, itemManager);
        }
    }
}
