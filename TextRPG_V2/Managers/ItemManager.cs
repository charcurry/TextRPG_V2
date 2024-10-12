using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2.Items;

namespace TextRPG_V2
{

    public class ItemManager
    {
        private List<Item> items; //list of all items in the game

        /// <summary>
        /// Constructor method for a "Item Manager" object.
        /// </summary>
        public ItemManager() { 
        
            items = new List<Item>();
        }

        /// <summary>
        /// Mutator method that adds an input item to the list of items
        /// </summary>
        /// <param name="item">The Item that will be put in to the list of items</param>
        public void AddItem(Item item)
        {
            if (item != null)
            {
                items.Add(item);
            }
        }

        /// <summary>
        /// Mutator method that removes a specified item from the list of items
        /// </summary>
        /// <param name="item">Item that will be removed from the list</param>
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        /// <summary>
        /// Method to update all items in the item list
        /// </summary>
        public void UpdateItems()
        {
            //do things with items if necessary
        }

        /// <summary>
        /// Method that initializes an Item from its character input
        /// </summary>
        /// <param name="input">character representing that item to be initialized</param>
        /// <param name="position">position of item (y, x)</param>
        /// <param name="map">map object</param>
        /// <returns>Item object that was initialized</returns>
        public Item InitializeItem(char input, int[] position, Map map)
        {
            Item newItem;

            switch (input)
            {
                case 'p':
                    newItem = new HealingPotion();
                    break;
                case 's':
                    newItem =  new Sword();
                    break;
                case 'b':
                    newItem = new BootsOfSpeed();
                    break;
                default:
                    return null; 
            }

            // Gives the item a cost if it is near the shop
            if (IsNearShop(position,map))
            {
                Random rnd = new Random(newItem.costSeed + Environment.TickCount);
                newItem.cost = rnd.Next(1, 5); // Sets cost of item
            }

            return newItem;
        }

        /// <summary>
        /// Checks if an item is near a shop tile
        /// </summary>
        /// <param name="position">Position of the item</param>
        /// <param name="map">Map object</param>
        /// <returns></returns>
        private bool IsNearShop(int[] position, Map map)
        {
            int x = position[1];
            int y = position[0];

            // Loops through a 3x3 area around it for a shop tile.
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int[] checkPos = { y + i, x + j };

                    // Checks if checkPos is within boundaries of map
                    if (checkPos[0] >= 0 && checkPos[0] < map.GetHeight() && checkPos[1] >= 0 && checkPos[1] < map.GetWidth())
                    {
                        Tile tile = map.GetTile(checkPos);
                        if (tile.GetShop())
                        {
                            return true; // Item is near shop
                        }
                    }
                }
            }
            return false; // Item is not near shop
        }
    }
}
