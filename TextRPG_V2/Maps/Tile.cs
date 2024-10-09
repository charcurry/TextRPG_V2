using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2;

namespace TextRPG_V2
{

    public class Tile
    {

        //variables
        private string name;
        private char symbol;
        private ConsoleColor color;
        private bool impassable;
        private bool dangerous;
        private bool exit;
        private bool hidden;
        private bool magic;
        private int damage;
        private bool isShop;

        /// <summary>
        /// Constructor method for a Tile type object
        /// </summary>
        /// <param name="tileType">Char that determines the type of the Tile that is created</param>
        public Tile(char tileType)
        {
            /*
             * Legend for tile types:
             * air          ' '
             * door         '/' or '\'
             * floor        '.'
             * lava         'l'
             * wall         '-' or '|' or '+'
             * water        'w'
             * deep water   'd'
             * magic flux   'f'
             * trap         't'
             * magic trap   'y'
             * exit         '>'
             * shop         '$'
             */

            //switch statement to set the correct values for the tile
            switch (tileType)
            {
                case ' ':
                    symbol = tileType;
                    name = "air";
                    color = ConsoleColor.White;
                    impassable = true;
                    dangerous = false;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = false;
                    break;

                case '/':
                case '\\':    
                    symbol = tileType;
                    name = "door";
                    color = ConsoleColor.White;
                    impassable = false;
                    dangerous = false;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = false;
                    break;

                case '.':
                    symbol = tileType;
                    name = "floor";
                    color = ConsoleColor.DarkGray;
                    impassable = false;
                    dangerous = false;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = false;
                    break;

                case '-':
                case '|':
                case '+':
                    symbol = tileType;
                    name = "wall";
                    color = ConsoleColor.Gray;
                    impassable = true;
                    dangerous = false;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = false;
                    break;

                case 'l':
                    symbol = '~';
                    name = "lava";
                    color = ConsoleColor.DarkRed;
                    impassable = false;
                    dangerous = true;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 7;
                    isShop = false;
                    break;

                case 'w':
                    symbol = '~';
                    name = "water";
                    color = ConsoleColor.Blue;
                    impassable = false;
                    dangerous = false;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = false;
                    break;

                case 'd':
                    symbol = '~';
                    name = "deep water";
                    color = ConsoleColor.DarkBlue;
                    impassable = true;
                    dangerous = false;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = false;
                    break;

                case 'f':
                    name = "flux";
                    color = ConsoleColor.DarkMagenta;
                    symbol = '░';
                    impassable = false;
                    dangerous = true;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 10;
                    isShop = false;
                    break;

                case 't':
                    symbol = 'T';
                    name = "trap";
                    color = ConsoleColor.DarkRed;
                    impassable = false;
                    dangerous = true;
                    exit = false;
                    hidden = true;
                    magic = false;
                    damage = 7;
                    isShop = false;
                    break;

                case 'y':
                    symbol = 'Y';
                    name = "magic trap";
                    color = ConsoleColor.DarkMagenta;
                    impassable = false;
                    dangerous = true;
                    exit = false;
                    hidden = true;
                    magic = false;
                    damage = 7;
                    isShop = false;
                    break;

                case '>':
                    symbol = '>';
                    name = "exit";
                    color = ConsoleColor.White;
                    impassable = false;
                    dangerous = false;
                    exit = true;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = false;
                    break;
                case '$':
                    symbol = '$';
                    name = "shop";
                    color = ConsoleColor.Green;
                    impassable = true;
                    dangerous = false;
                    exit = false;
                    hidden = false;
                    magic = false;
                    damage = 0;
                    isShop = true;
                    break;
            }
        }

        /// <summary>
        /// Method that deals damage to an input entity
        /// </summary>
        /// <param name="target">the entity that will take damage</param>
        /// <returns>a damage message that can be printed to the UI</returns>
        public string DealDamage(Entity target)
        {

            //generating message
            string damageMessage = target.GetName() + " finished their action on a " + name + " tile and took " + target.TakeDamage(damage, magic)*2 + " damage"; //for some reason does double damage.

            return damageMessage;
        }

        /*
         * Mutator method that sets name of tile
         * Input: (string) name: the name of the tile
         */
        public void SetName(string name)
        {
            this.name = name;
        }

        /*
         * Accessor method that gets name of tile
         * Output: (string) name: the name of the tile
         */
        public string GetName()
        {
            return name;
        }

        /*
         * Mutator method that sets symbol of tile
         * Input: (char) symbol: the symbol you want to set the tile as
         */
        public void SetSymbol(char symbol)
        {
            this.symbol = symbol;
        }

        /*
         * Accessor method for symbol of tile
         * Output: (char) symbol: the symbol of the tile
         */
        public char GetSymbol()
        {
            return symbol;
        }

        /*
         * Mutator method that sets color of the tile
         * Input: (enum ConsoleColor) color: the color of the tile
         */
        public void SetColor(ConsoleColor color)
        {
            this.color = color;
        }

        /*
         * Accessor method for the color of the tile
         * Output: (enum ConsoleColor) color: the color of the tile
         */
        public ConsoleColor GetColor()
        {
            return color;
        }

        /*
         * Mutator method for the passability (letting player walk over / through tile) of the tile
         * Input: (bool) impassable: bool handling if the player can walk through tile
         */
        public void SetImpassable(bool impassable)
        {
            this.impassable = impassable;
        }

        /*
         * Accessor method for the passability (letting player walk over / through tile) of the tile
         * Output: (bool) impassable: bool handling if the player can walk through tile
         */
        public bool GetImpassable()
        {
            return impassable;
        }

        /*
         * Mutator method for if the tile deals damage when walked over
         * Input: (bool) dangerous: bool handling if the tile deals damage
         */
        public void SetDangerous(bool dangerous)
        {
            this.dangerous = dangerous;
        }

        /*
         * Accessor method for if the tile deals damage when walked over
         * Output: (bool) dangerous: bool handling if the tile deals damage
         */
        public bool GetDangerous()
        {
            return dangerous;
        }

        /// <summary>
        /// Mutator method for if the tile is an exit
        /// </summary>
        /// <param name="exit">desired exit state of the tile</param>
        public void SetExit(bool exit)
        {
            this.exit = exit;
        }

        /// <summary>
        /// Accessor method for if the tile is an exit
        /// </summary>
        /// <returns>exit state of the tile</returns>
        public bool GetExit()
        {
            return exit;
        }

        /// <summary>
        /// Mutator method for if the tile is hidden
        /// </summary>
        /// <param name="hidden">the desired hidden state of the tile</param>
        public void SetHidden(bool hidden)
        {
            this.hidden = hidden;
        }

        /// <summary>
        /// Accessor method for if the tile is hidden
        /// </summary>
        /// <returns>the hidden state of the tile</returns>
        public bool GetHidden()
        {
            return hidden;
        }

        /// <summary>
        /// Mutator method for the damage type done by the tile
        /// </summary>
        /// <param name="magic">desired damage type done by the tile (magic or physical)</param>
        public void SetDamageType(bool magic)
        {
            this.magic = magic;
        }

        /// <summary>
        /// Accessor method for the damage type done by the tile
        /// </summary>
        /// <returns>damage type done by the tile (magic or physical)</returns>
        public bool GetDamageType()
        {
            return magic;
        }

        /*
         * Mutator method for the damage the tile deals when walked on
         * Input: (int) damage: the damage value the tile outputs when walked on
         */
        public void SetDamage(int damage)
        {
            this.damage = damage;
        }

        /// <summary>
        /// Sets tiles to be a shop tile
        /// </summary>
        /// <param name="isShop">if the tile is a shop</param>
        public void SetShop(bool isShop)
        {
            this.isShop = isShop;
        }

        /// <summary>
        /// Gets the shop state of the tile
        /// </summary>
        /// <returns>the shop state of the tile</returns>
        public bool GetShop()
        {
            return isShop;
        }
    }
}
