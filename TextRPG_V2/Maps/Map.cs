using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2;
using System.IO;
using System.Diagnostics;

namespace TextRPG_V2
{
    public class Map
    {
        private string mapName; //name of the map
        private Tile[,] background; //2d Array of tiles that represent the terrain of the map
        private Entity[,] entities; //2d Array of entities that represent the entities on the map
        private Item[,] items; //2d Array of items that represent the items on the map
        private int height; //the height (y axis) of the map
        private int width; //the width (x axis) of the map

        /// <summary>
        /// Constructor method for a Map type object
        /// </summary>
        /// <param name="path">The location of the map file</param>
        /// <param name="entityManager">The manager for Entity type objects</param>
        /// <param name="itemManager">The manager for Item type objects</param>
        public Map(string path, EntityManager entityManager, ItemManager itemManager)
        {
            //declaring some variables (default values added to handle no value error)
            int startIndex = 0, endIndex = 0;

            //check if file exists
            if (!File.Exists(@path))
            {
                //throw an error
            }

            //getting input from file
            string[] input = File.ReadAllLines(@path);

            //parsing through file for basic information
            //this section needs to be changed to fit JSON file format later
            mapName = input[0];

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == "{")
                {
                    startIndex = i + 1; //sets start of map below open bracket
                }

                if (input[i] == "}")
                {
                    endIndex = i - 1; //sets end of map above closed bracket
                    break;
                }
            }

            if (!(endIndex >= startIndex))
            {
                //throw error
            }

            height = endIndex - startIndex + 1; //gets height of map
            width = input[startIndex].Length; //gets width of map

            //initializing Tile map
            background = new Tile[height, width];
            items = new Item[height, width];

            //cycle through every char of the input map
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //sets each individual tile to their correct value
                    background[y, x] = new Tile(input[y + startIndex][x]);
                }

            }

            //find start and end indexes for entities
            for (int i = endIndex + 2; i < input.Length; i++)
            {
                if (input[i] == "{")
                {
                    startIndex = i + 1; //sets start of map below open bracket
                }

                if (input[i] == "}")
                {
                    endIndex = i - 1; //sets end of map above closed bracket
                    break;
                }
            }

            //initializing entity map
            entities = new Entity[height, width];

            //reading in and adding in entities
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    entities[y, x] = entityManager.InitializeEntity(input[y + startIndex][x]);
                    entityManager.AddEntity(entities[y, x]);
                }
            }

            //find start and end indexes for items
            for (int i = endIndex + 2; i < input.Length; i++)
            {
                if (input[i] == "{")
                {
                    startIndex = i + 1; //sets start of map below open bracket
                }

                if (input[i] == "}")
                {
                    endIndex = i - 1; //sets end of map above closed bracket
                    break;
                }
            }

            //initializing item map
            items = new Item[height, width];

            //reading in and adding to item manager
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int[] position = { y, x };
                    items[y, x] = itemManager.InitializeItem(input[y + startIndex][x], position, this);
                    itemManager.AddItem(items[y, x]);
                }
            }
        }

        /*
         * Accessor method for Height of the Map
         * Output: (int) height: the length of a column of the map array
         */
        public int GetHeight()
        {
            return height;
        }

        /*
         * Accessor method for the width of Map
         * Output: (int) width: the width of a row of the map array
         */
        public int GetWidth()
        {
            return width;
        }

        /*
         * Accessor method for specified Tile in map background
         * Input: (int[]) pos: position of the Tile on the map background
         *      pos[0]: the Y coordinate of the Tile
         *      pos[1]: the X coordinate of the Tile
         */
        public Tile GetTile(int[] pos)
        {
            return background[pos[0], pos[1]];
        }

        /*
         * Accessor method for specified Entity in map entities
         * Input: (int[]) pos: position of the Entity on the map entities
         *      pos[0]: the Y coordinate of the Entity
         *      pos[1]: the X coordinate of the Entity
         */
        public Entity GetEntity(int[] pos)
        {
            return entities[pos[0], pos[1]];
        }

        /*
         * Mutator method that sets an Entity's position on the Map entities
         * Input: (Entity) entity: the desired entity to be placed in Map entities
         * Input: (int[]) pos: position of the Entity on the map entities
         *      pos[0]: the Y coordinate of the Entity
         *      pos[1]: the X coordinate of the Entity
         */
        public void AddEntity(Entity entity, int[] pos)
        {
            entities[pos[0], pos[1]] = entity;
        }

        /*
         * Mutator method that removes an Entity from the Map entities
         * Input: (int[]) pos: position of the Entity on the map entities
         *      pos[0]: the Y coordinate of the Entity
         *      pos[1]: the X coordinate of the Entity
         */
        public void RemoveEntity(int[] pos)
        {
            entities[pos[0], pos[1]] = null;
        }

        /// <summary>
        /// Mutator method that removes an Entity from the Map entities given its memory location
        /// </summary>
        /// <param name="entity">The instance of the Entity being removed</param>
        public void RemoveEntity(Entity entity)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int[] index = { y, x };
                    if (entities[index[0], index[1]] == entity)
                    {
                        entities[index[0], index[1]] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Accessor method that returns the index for a specific instance of an Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Index of the input Entity on the map</returns>
        public int[] GetEntityIndex(Entity entity)
        {
            for (int y =0; y<height; y++)
            {
                for (int x =0; x<width; x++)
                {
                    int[] index = {y, x}; 
                    if (entities[index[0], index[1]] == entity)
                    {
                        return index;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Accessor method that returns an instance of an Item given its index on the map
        /// </summary>
        /// <param name="pos">The index of the item on the map</param>
        /// <returns>The instance of the Item</returns>
        public Item GetItem(int[] pos)
        {
            return items[pos[0], pos[1]];
        }

        /// <summary>
        /// Mutator method that that adds a given Item to a given index on the map
        /// </summary>
        /// <param name="item">Instance of an Item to be added to the map</param>
        /// <param name="pos">The index at which to put the item on the map</param>
        public void AddItem(Item item, int[] pos)
        {
            items[pos[0], pos[1]] = item;
        }

        /// <summary>
        /// Mutator method that removes an Item from a given index on the map
        /// </summary>
        /// <param name="pos">The index at which to remove an Item</param>
        public void RemoveItem(int[] pos)
        {
            items[pos[0], pos[1]] = null;
        }

        /// <summary>
        /// Method that returns the color of the top level layer of the map at a given position
        /// </summary>
        /// <param name="pos">The index that is being accessed</param>
        /// <returns>The color of the top level layer of map at position</returns>
        public ConsoleColor GetTopColor(int[] pos)
        {
            if (entities[pos[0], pos[1]] != null)
            {
                return entities[pos[0], pos[1]].GetColor();
            }
            else if (items[pos[0], pos[1]] != null)
            {
                return items[pos[0], pos[1]].GetColor();
            }
            else
            {
                return background[pos[0], pos[1]].GetColor();
            }
        }

        /// <summary>
        /// Method that returns the symbol of the top level layer of the map at a given position
        /// </summary>
        /// <param name="pos">The index that is being accessed</param>
        /// <returns>The symbol of the top level layer of map at position</returns>
        public char GetTopSymbol(int[] pos)
        {
            if (entities[pos[0], pos[1]] != null)
            {
                return entities[pos[0], pos[1]].GetSymbol();
            }
            else if (items[pos[0], pos[1]] != null)
            {
                return items[pos[0], pos[1]].GetSymbol();
            }
            else
            {
                return background[pos[0], pos[1]].GetSymbol();
            }
        }

        /// <summary>
        /// Method that returns the location of the exit of a map
        /// </summary>
        /// <returns>The index at which there is an exit on the map</returns>
        public int[] GetExitIndex()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int[] index = { y, x };
                    if (background[index[0], index[1]].GetExit())
                    {
                        return index;
                    }
                }
            }

            return null;
        }
    }
}

