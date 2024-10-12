using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2;

namespace TextRPG_V2
{
    //Enumerator that contains the factions list of entities
    public enum Faction
    {
        player,
        undead,
        wizards,
        warriors
    }

    public abstract class Entity
    {
        //Entity variables
        private string name; //the name of the Entity
        private ConsoleColor color; //color of the Entity respresentation
        private char symbol; //the graphical representation of the Entity
        private Faction faction;
        private bool magic;
        public bool killedByPlayer;

        //Stats
        public HealthSystem health; //the health value of the entity
        public Stat atk; //the damage the entity does with a physical attack
        public Stat def; //the reduction in damage the entity takes from physical attacks
        public Stat mag; //the damage the entity does with a magical attack
        public Stat res; //the reduction in damage the entity takes from magical attacks 
        public Stat spd; //the rate at which the entity can take turns (also used in avoid and hit calcs)
        public Stat skl; //Value used in hit, crit and avoid calculations
        public Stat luc; //Value used in hit, crit and avoid calculations
        public Stat gld; //Value used for purchasing items for the player or the amount of gold dropped by enemies

        //derived values

        /*
         * Avoid: The difficulty added to hit calculations (50 + 2*Spd + 2*Skl + Luc)
         * Hit: The gain added to hit calculations (3*Skl + Spd + Luc)
         * Crit: The chance to land a critical hit in an attack (0.4*Luc + 0.1*Skl)
         */

        //random number generation for Entity
        public Random rnd;

        /// <summary>
        /// Constructor for an Entity
        /// </summary>
        /// <param name="name">the name of the Entity</param>
        /// <param name="symbol">the graphical representation of the Entity</param>
        /// <param name="color">color of the Entity</param>
        /// <param name="atk">the damage the entity does with a physical attack</param>
        /// <param name="def">the reduction in damage the entity takes from physical attacks</param>
        /// <param name="mag">the damage the entity does with a magical attack</param>
        /// <param name="res">the reduction in damage the entity takes from magical attacks</param>
        /// <param name="spd">the rate at which the entity can take turns</param>
        /// <param name="skl">Value used in hit, crit and avoid calculations</param>
        /// <param name="luc">Value used in hit, crit and avoid calculations</param>
        public Entity(string name, char symbol, ConsoleColor color, int atk, int def, int mag, int res, int spd, int skl, int luc, int gld)
        {
            //Setting entity variables
            this.name = name;
            this.symbol = symbol;
            this.color = color;

            //Setting stat
            health = null;
            this.atk = new Stat(atk);
            this.def = new Stat(def);
            this.mag = new Stat(mag);
            this.res = new Stat(res);
            this.spd = new Stat(spd);
            this.skl = new Stat(skl);
            this.luc = new Stat(luc);
            this.gld = new Stat(gld);

            rnd = new Random();
        }

        /// <summary>
        /// Empty constructor for an Entity
        /// </summary>
        public Entity()
        {
            name = null;
            symbol = '?';
            color = ConsoleColor.Gray;
            health = null;
            atk = null;
            def = null;
            mag = null;
            res = null;
            spd = null;
            skl = null;
            luc = null;
            gld = null;

            rnd = new Random();
        }

        /// <summary>
        /// Abstract method called for having an Entity choose its action
        /// </summary>
        /// <param name="map">The map on which the entity exists</param>
        /// <param name="startPos">The position on which the entity starts its turn</param>
        /// <param name="uiManager">The manager for UI class objects</param>
        /// <param name="itemManager">The manager for Item class objects</param>
        /// <param name="questManager">The manager for Quest class objects</param>
        /// <returns>String containing a description of the action</returns>
        public abstract string ChooseAction(Map map, int[] startPos, UIManager uiManager, ItemManager itemManager, QuestManager questManager);

        /// <summary>
        /// Method to have entity take a move action
        /// </summary>
        /// <param name="map">The map on which the entity exists</param>
        /// <param name="startPos">The position at which the entity starts their turn</param>
        /// <param name="endPos">The position at which the entity will end their turn</param>
        /// <param name="uiManager">The manager for UI class objects</param>
        /// <param name="itemManager">he manager for Item class objects</param>
        /// <param name="questManager">The manager for Quest class objects</param>
        /// <returns>String containing a result of the move action</returns>
        public string Move(Map map, int[] startPos, int[] endPos, UIManager uiManager, ItemManager itemManager, QuestManager questManager)
        {
            //check desired position if within bounds of map
            if (endPos[0] < 0 || endPos[0] >= map.GetHeight() || endPos[1] < 0 || endPos[1] >= map.GetWidth())
            {
                return null; //fail to move
            }

            //checks if there is an Entity to attack
            else if (map.GetEntity(endPos) != null)
            {
                return Attack(map.GetEntity(endPos)); //attacks entity
            }

            //check if Tile is impassable
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
        /// Method used to have one entity attack another
        /// </summary>
        /// <param name="target">Entity that is the target of the attack</param>
        /// <returns>A description of the result of the attack</returns>
        public string Attack(Entity target)
        {
            string attackMessage = name + " attacked " + target.name;
            int damage;

            //get random roll
            int roll = rnd.Next(1,100);

            //get derived values
            int crit = (int)(GlobalVariables.critLucWeight * luc.GetStat() + GlobalVariables.critSklWeight * skl.GetStat());
            int hit = GlobalVariables.hitSklWeight * skl.GetStat() + GlobalVariables.hitSpdWeight * spd.GetStat() + GlobalVariables.hitLucWeight * luc.GetStat();
            int avoid = GlobalVariables.baseDodge + GlobalVariables.dodgeSpdWeight * target.spd.GetStat() + GlobalVariables.dodgeSklWeight * target.skl.GetStat() + GlobalVariables.hitLucWeight * target.luc.GetStat();

            //check for critical hit
            if (roll >= 100 - crit)
            {
                damage = target.TakeDamage(GetAttackDamage() * 2, magic);

                attackMessage += GetAttackType() + "and CRITICALLY HIT for " + damage + " damage!!!";
            }

            //check for regular hit
            else if (roll + hit > avoid)
            {
                damage = target.TakeDamage(GetAttackDamage(), magic);

                attackMessage += GetAttackType() + "and hit for " + damage + " damage.";
            }
            //attack missed
            else
            {
                attackMessage += " and missed.";  
            }

            if (target.health.GetHp() <= 0 && name == "Player")
            {
                target.killedByPlayer = true;
            }

            return attackMessage;
        }

        /// <summary>
        /// Utility function to return the correct number for an attack's damage
        /// </summary>
        /// <returns>damage the entity does on a successful attack</returns>
        private int GetAttackDamage()
        {
            if (magic)
            {
                return mag.GetStat();
            }
            else
            {
                return atk.GetStat();
            }
        }

        /// <summary>
        /// Utility function to return the type of attack the entity does in a string
        /// </summary>
        /// <returns>string for the type of damage</returns>
        private string GetAttackType()
        {
            if (magic)
            {
                return " using magic ";
            }
            else
            {
                return " physically ";
            }
        }

        /// <summary>
        /// Method that reduced damage taken by the entity by the appropriate amount and returns the damage taken by the entity
        /// </summary>
        /// <param name="incomingDamage">the numerical value of the damage being applied</param>
        /// <param name="magic">whether or not the damage is magical</param>
        /// <returns>damage taken by the entity</returns>
        public int TakeDamage(int incomingDamage, bool magic)
        {
            int damage;

            if (magic)
            {
                damage = incomingDamage - res.GetStat();
            }
            else
            {
                damage = incomingDamage - def.GetStat();
            }

            //sets minimum damage amount
            if (damage < 0)
            {
                damage = 0;
            }

            //apply damage to health
            health.ModHp(- damage);

            //return the damage amount
            return damage;
        }

        //Accessor and Mutator Methods:

        /*
         * Mutator method that sets name of the Entity
         * Input: (string) name: the name of the Entity
         */
        public void SetName(string name)
        {
            this.name = name;
        }

        /*
         * Accessor method that gets the name of an Entity
         * Output: (string) name: the name of the Entity
         */
        public string GetName()
        {
            return name;
        }

        /*
         * Mutator method that sets the graphical representation of the Entity
         * Input: (char) symbol: the graphical representation of the Entity
         */
        public void SetSymbol(char symbol)
        {
            this.symbol = symbol;
        }

        /*
         * Accessor method that returns the graphical representation of the Entity
         * Output: (char) symbol: the graphical representation of the Entity
         */
        public char GetSymbol()
        {
            return symbol;
        }

        /*
         * Mutator method that sets the color of the graphical representation of the Entity
         * Input: (ConsoleColor) color: the color of the graphical representation of the Entity
         */
        public void SetColor(ConsoleColor color)
        {
            this.color = color;
        }

        /*
         * Accessor method that returns the color of the graphical representation of the Entity
         * Output: (ConsoleColor) color: the color of the graphical representation of the Entity
         */
        public ConsoleColor GetColor()
        {
            return color;
        }

        /// <summary>
        /// Mutator method that sets the faction of the Entity to the specified faction
        /// </summary>
        /// <param name="faction">the desired faction of the Entity</param>
        public void SetFaction(Faction faction)
        {
            this.faction = faction;
        }

        /// <summary>
        /// Accessor method that returns the faction of the Entity
        /// </summary>
        /// <returns>The faction type of entity</returns>
        public Faction GetFaction()
        {
            return this.faction;
        }

        /// <summary>
        /// Mutator method that sets the magic stat of an Entity
        /// </summary>
        /// <param name="magic">The desired value of the magic stat</param>
        public void SetMagic(bool magic)
        {
            this.magic = magic;
        }

        /// <summary>
        /// Accessor method that returns the magic stat of an Entity
        /// </summary>
        /// <returns>The magic stat of the Entity</returns>
        public bool GetMagic()
        {
            return magic;
        }
    }
}
