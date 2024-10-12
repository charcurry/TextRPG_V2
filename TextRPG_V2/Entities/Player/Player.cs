using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_V2.Managers;

namespace TextRPG_V2
{
    public class Player : Entity
    {
        
        /// <summary>
        /// Empty constructor for a "Player" class entity.
        /// </summary>
        public Player() : base() 
        {
            SetName("Player");
            SetSymbol('@');
            SetColor(ConsoleColor.Yellow);
            SetFaction(Faction.player);
            base.health = new HealthSystem(15);
            base.atk = new Stat(5);
            base.def = new Stat(5);
            base.mag = new Stat(5);
            base.res = new Stat(5);
            base.spd = new Stat(10);
            base.skl = new Stat(10);
            base.luc = new Stat(10);
            base.gld = new Stat(10);

        }

        /// <summary>
        /// Method to read player input to determine player actions
        /// </summary>
        /// <param name="map">The map on which the player exists</param>
        /// <param name="startPos">The position at which the player starts their turn</param>
        /// <param name="uiManager">The manager for UI class objects</param>
        /// <param name="itemManager">The manager for Item class objects</param>
        /// <returns>String containing a description of the action</returns>
        public override string ChooseAction(Map map, int[] startPos, UIManager uiManager, ItemManager itemManager, QuestManager questManager)
        {
            //declaring variables
            int[] endPos = { startPos[0], startPos[1] };

            //getting input from player
            while (Console.KeyAvailable) { Console.ReadKey(true); }
            
            bool gotInput = false;
            while (!gotInput)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                //switch statement to determine which tile to move to
                switch (input.Key)
                {
                    //move up
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        endPos[0]--;
                        gotInput = true;
                        break;

                    //move down
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        endPos[0]++;
                        gotInput = true;
                        break;

                    //move leftw
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        endPos[1]--;
                        gotInput = true;
                        break;

                    //move right
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        endPos[1]++;
                        gotInput = true;
                        break;

                    //switch stance
                    case ConsoleKey.E:
                        return SwitchStance();

                    //close the game
                    case ConsoleKey.Escape:
                        System.Environment.Exit(0);
                        break;

                    default: break;
                }
            }

            return Move(map, startPos, endPos, uiManager, itemManager, questManager);
        }

        /// <summary>
        /// Method for a moving action performed by the player that hides the entity move action
        /// </summary>
        /// <param name="map">The map on which the player exists</param>
        /// <param name="startPos">The position at which the player starts their turn</param>
        /// <param name="endPos">The position at which the player will end their turn</param>
        /// <param name="uIManager">The manager for UI class objects</param>
        /// <param name="itemManager">The manager for Item class objects</param>
        /// <param name="questManager">The manager for Quest class objects</param>
        /// <returns>String containing a description of the action</returns>
        new public string Move(Map map, int[] startPos, int[] endPos, UIManager uIManager, ItemManager itemManager, QuestManager questManager)
        {
            //check desired position if within bounds of map
            if (endPos[0] < 0 || endPos[0] >= map.GetHeight() || endPos[1] < 0 || endPos[1] >= map.GetWidth())
            {
                return null; //fail to move
            }

            //checks if there is an Entity to attack
            else if (map.GetEntity(endPos) != null)
            {
                uIManager.SetEnemyStats(map.GetEntity(endPos));
                return Attack(map.GetEntity(endPos)); //attacks entity
            }

            //uses item if item is available
            else if (map.GetItem(endPos) != null)
            {
                if (gld.GetStat() < map.GetItem(endPos).cost && map.GetItem(endPos).cost > 0)
                {
                    Debug.WriteLine(gld.GetStat());
                    Debug.WriteLine(map.GetItem(endPos).cost);
                    string message = map.GetItem(endPos).Check(this);
                    return message;
                }
                else if (gld.GetStat() > map.GetItem(endPos).cost && map.GetItem(endPos).cost > 0)
                {
                    //Debug.WriteLine(gld.GetStat());
                    //Debug.WriteLine(map.GetItem(endPos).cost);
                    gld.ModStat(-map.GetItem(endPos).cost);
                    string purchaseMessage = map.GetItem(endPos).Purchase(this);
                    Debug.WriteLine(questManager.GetActiveQuests().Count());
                    questManager.UpdateReleventQuest(uIManager, Quest.QuestType.PurchaseItems);
                    map.GetItem(endPos).Use(this);
                    itemManager.RemoveItem(map.GetItem(endPos));
                    map.RemoveItem(endPos);
                    return purchaseMessage;
                }
                else
                {
                    Debug.WriteLine(gld.GetStat());
                    Debug.WriteLine(map.GetItem(endPos).cost);
                    string message = map.GetItem(endPos).Use(this);
                    itemManager.RemoveItem(map.GetItem(endPos));
                    map.RemoveItem(endPos);
                    return message;
                }
            }

            //check if Tile is impassableS
            else if (map.GetTile(endPos).GetImpassable())
            {
                return null; //fail to move
            }

            //moves
            else
            {
                //deal damage to player standing on dangerous tile
                if (map.GetTile(endPos).GetDangerous())
                {
                    map.GetTile(endPos).DealDamage(this);
                }

                map.AddEntity(map.GetEntity(startPos), endPos); //puts entity into new location
                map.RemoveEntity(startPos); //removes entity from old location

                return null;
            }
        }

        /// <summary>
        /// Method to allow the player to switch stances
        /// </summary>
        /// <returns>Description of the player changing stances</returns>
        public string SwitchStance()
        {
            string message = "Player switched to ";

            SetMagic(!GetMagic());
            ChangePlayerColor();

            if (GetMagic())
            {
                message += "magic stance.";
            }
            else
            {
                message += "physical stance.";
            }

            return message;
        }

        /// <summary>
        /// Method that changes the color of the player character
        /// </summary>
        private void ChangePlayerColor()
        {
            if (GetMagic())
            {
                SetColor(ConsoleColor.DarkCyan);
            }
            else
            {
                SetColor(ConsoleColor.Yellow);
            }
        }
    }
}
