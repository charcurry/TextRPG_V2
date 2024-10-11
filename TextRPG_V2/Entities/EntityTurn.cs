using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{
    // class to facilitate an action speed system in Entity Manager
    public class EntityTurn
    {
        public Entity entity { get; set; }
        public int turnBuildup { get; set; }

        /// <summary>
        /// A class to act as a global turn for an Entity
        /// </summary>
        /// <param name="entity">the entity who's turn this EntityTurn represents</param>
        public EntityTurn(Entity entity)
        {
            this.entity = entity;
            turnBuildup = 0;
        }

        /// <summary>
        /// Method that instructs the EntityTurn to go through its update process.
        /// </summary>
        /// <param name="map">the map the game is on</param>
        /// <param name="uIManager">the manager for the game UI</param>
        /// <param name="itemManager">the manager for the items on the map</param>
        /// <param name="entityManager">the manager for entities on the map</param>
        /// <returns>bool returning if game is ending</returns>
        public bool Update(Map map, UIManager uIManager, ItemManager itemManager, EntityManager entityManager, QuestManager questManager)
        {
            //adding speed to build up the Entity's turn
            turnBuildup += entity.spd.GetStat();

            while (turnBuildup >= GlobalVariables.actionThreshold)
            {
                //update UI for player
                if (entity == entityManager.GetPlayer())
                {
                    uIManager.DrawUI(map);
                }

                //checks if player is standing on the exit tile
                if (map.GetTile(map.GetEntityIndex(entityManager.GetPlayer())).GetExit())
                {
                    return true;
                }

                //adds events of the turn to the log (if the events were notable)
                uIManager.AddEventToLog(TakeAction(map, uIManager, itemManager));

                //check if entity takes damage from tile
                if (map.GetTile(map.GetEntityIndex(entity)).GetDangerous())
                {
                    uIManager.AddEventToLog(map.GetTile(map.GetEntityIndex(entity)).DealDamage(entity));
                }

                //gets the entity manager to check for dead enemies
                entityManager.CheckDeadEntities(map, uIManager, questManager);
            }

            return false;

        }

        /// <summary>
        /// Method that instructs the entity to take an action.
        /// </summary>
        /// <param name="map">the map the game is on</param>
        /// <param name="uIManager">the manager for the game UI</param>
        /// <param name="itemManager">the manager for the items on the map</param>
        /// <returns>Description of the action taken</returns>
        private string TakeAction(Map map, UIManager uIManager, ItemManager itemManager)
        {
            turnBuildup -= GlobalVariables.actionThreshold;
            return entity.ChooseAction(map, map.GetEntityIndex(entity), uIManager, itemManager);
        }
    }
}
