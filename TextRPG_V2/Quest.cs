using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2.Managers
{
    public class Quest
    {
        public string name;
        public string description;
        public bool isCompleted;

        //bool doTask;

        public int numThingsDone;
        public int maxNumThingsRequired;

        public enum QuestType
        {
            PurchaseItems,
            KillEnemies,
            FindExit
        }

        public QuestType questType;

        /// <summary>
        /// Quest constructor
        /// </summary>
        /// <param name="name">name of quest</param>
        /// <param name="description">description of quest</param>
        /// <param name="questType">type of quest</param>
        /// <param name="numThingsDone">progress on quest</param>
        /// <param name="maxNumThingsRequired">max required progress on quest</param>
        public Quest(string name, string description, QuestType questType, int maxNumThingsRequired = 1, int numThingsDone = 0)
        {
            this.questType = questType;
            this.name = name;
            this.description = description;
            this.maxNumThingsRequired = maxNumThingsRequired;
            this.numThingsDone = numThingsDone;
            isCompleted = false;
        }

        /// <summary>
        /// Checks for quest completion
        /// </summary>
        /// <returns>the completion status of the quest</returns>
        public bool CheckCompletion()
        {
            return isCompleted;
        }
    }
}
