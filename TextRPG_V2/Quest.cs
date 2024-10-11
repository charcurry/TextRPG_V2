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
            AcquireItems,
            KillEnemies,
            DoTask
        }

        public QuestType questType;

        public Quest(string name, string description, QuestType questType, int numThingsDone, int maxNumThingsRequired)
        {
            this.questType = questType;
            this.name = name;
            this.description = description;
            this.maxNumThingsRequired = maxNumThingsRequired;
            this.numThingsDone = numThingsDone;
            isCompleted = false;
        }

        public bool CheckCompletion()
        {
            return isCompleted;
        }
    }
}
