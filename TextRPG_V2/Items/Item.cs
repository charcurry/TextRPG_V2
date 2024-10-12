using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{
    public abstract class Item
    {
        private string name; //name of the item
        private ConsoleColor color; //color of the Item respresentation
        private char symbol; //the graphical representation of the Item
        public int cost;

        public int costSeed;

        /// <summary>
        /// Constructor method for an abstract item object
        /// </summary>
        /// <param name="name">name of the item</param>
        public Item(string name)
        {
            this.name = name;
            color = ConsoleColor.Yellow;
            symbol = '?';
            cost = 0;
        }

        /// <summary>
        /// Empty Constructor for an Item object
        /// </summary>
        public Item()
        {
            name = null;
            color = ConsoleColor.Yellow;
            symbol = '?';
        }

        /// <summary>
        /// Method for using an item
        /// </summary>
        /// <param name="target">The entity that is using the item</param>
        /// <returns>A string for the event log</returns>
        public abstract string Use(Entity target);

        public string Check(Entity target)
        {
            string message = target.GetName() + " cannot afford " + GetName() + ". Price: " + cost;
            return message;
        }

        public string Purchase(Entity target)
        {
            string message = target.GetName() + " has purchased and used " + GetName() + " for " + cost + " gold.";
            return message;
        }

        /// <summary>
        /// Accessor method for the name of the item
        /// </summary>
        /// <returns>name of the item</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Mutator
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /*
         * Mutator method that sets the graphical representation of the Item
         * Input: (char) symbol: the graphical representation of the Item
         */
        public void SetSymbol(char symbol)
        {
            this.symbol = symbol;
        }

        /*
         * Accessor method that returns the graphical representation of the Item
         * Output: (char) symbol: the graphical representation of the Item
         */
        public char GetSymbol()
        {
            return symbol;
        }

        /*
         * Mutator method that sets the color of the graphical representation of the Item
         * Input: (ConsoleColor) color: the color of the graphical representation of the Item
         */
        public void SetColor(ConsoleColor color)
        {
            this.color = color;
        }

        /*
         * Accessor method that returns the color of the graphical representation of the Item
         * Output: (ConsoleColor) color: the color of the graphical representation of the Item
         */
        public ConsoleColor GetColor()
        {
            return color;
        }


    }
}
