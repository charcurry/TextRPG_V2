using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{
    internal class Warrior : Entity
    {
        //an enumerator of the cardinal directions
        private enum Direction
        {
            north,
            east,
            south,
            west
        }

        private Direction direction;

        /// <summary>
        /// Empty constructor for a "Warrior" enemy type.
        /// </summary>
        public Warrior() {

            SetName("Warrior");
            SetSymbol('W');
            SetColor(ConsoleColor.Red);
            SetFaction(Faction.warriors);
            SetMagic(false);

            health = new HealthSystem(20);
            atk = new Stat(18);
            def = new Stat(10);
            mag = new Stat(1);
            res = new Stat(7);
            spd = new Stat(8);
            skl = new Stat(4);
            luc = new Stat(10);
            gld = new Stat(15);

            ChooseDirection();
        }

        /// <summary>
        /// Method for the "Warrior" type enemy to choose its action. Moves in a line until it finds an impassable tile or another entity which it will attack.
        /// </summary>
        /// <param name="map">The map on which the warrior exists</param>
        /// <param name="startPos">The position at which the warrior starts its turn</param>
        /// <param name="itemManager">The manager for UI class objects</param>
        /// <param name="uiManager">The manager for Item class objects</param>
        /// <returns>String containing a description of the action</returns>
        public override string ChooseAction(Map map, int[] startPos, UIManager uiManager, ItemManager itemManager)
        {
            bool foundDirection = false;
            int[] endPos = new int[2];

            int[] posNorth = { startPos[0] - 1, startPos[1] };
            int[] posSouth = { startPos[0] + 1, startPos[1] };
            int[] posEast = { startPos[0], startPos[1] - 1 };
            int[] posWest = { startPos[0], startPos[1] + 1 };

            //checks for entities
            if (map.GetEntity(posSouth) != null)
            {
                return Move(map, startPos, posSouth, uiManager, itemManager);
            }
            else if (map.GetEntity(posWest) != null)
            {
                return Move(map, startPos, posWest, uiManager, itemManager);
            }
            else if (map.GetEntity(posNorth) != null)
            {
                return Move(map, startPos, posNorth, uiManager, itemManager);
            }
            else if (map.GetEntity(posEast) != null)
            {
                return Move(map, startPos, posEast, uiManager, itemManager);
            }

            while (!foundDirection)
            {
                switch (direction)
                {
                    //try north
                    case Direction.north:
                        if (!map.GetTile(posNorth).GetImpassable())
                        {
                            endPos = posNorth;
                            foundDirection = true;
                        }
                        else
                        {
                            ChooseDirection();
                        }
                        break;

                    //try east
                    case Direction.east:
                        if (!map.GetTile(posEast).GetImpassable())
                        {
                            endPos = posEast;
                            foundDirection = true;
                        }
                        else
                        {
                            ChooseDirection();
                        }
                        break;

                    //try south
                    case Direction.south:
                        if (!map.GetTile(posSouth).GetImpassable())
                        {
                            endPos = posSouth;
                            foundDirection = true;
                        }
                        else
                        {
                            ChooseDirection();
                        }
                        break;

                    //try west
                    case Direction.west:
                        if (!map.GetTile(posWest).GetImpassable())
                        {
                            endPos = posWest;
                            foundDirection = true;
                        }
                        else
                        {
                            ChooseDirection();
                        }
                        break;
                    
                }
            }

            return Move(map, startPos, endPos, uiManager, itemManager);
        }

        /// <summary>
        /// Method that randomly choose a direction for the warrior to go in.
        /// </summary>
        private void ChooseDirection()
        {
            switch (rnd.Next(0, 4))
            {
                case 0:
                    direction = Direction.north;
                    break;

                case 1:
                    direction = Direction.east;
                    break;

                case 2:
                    direction = Direction.south;
                    break;

                case 3:
                    direction = Direction.west;
                    break;
            }
        }
    }
}
