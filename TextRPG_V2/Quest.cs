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
        bool isCompleted;

        bool doTask;

        int numThingsRequired;

        public enum QuestType
        {
            AcquireItems,
            KillEnemies,
            DoTask
        }

        public QuestType questType;

        public Quest(string name, string description, QuestType questType, bool doTask = false, int numThingsRequired = 0)
        {
            this.name = name;
            this.description = description;
            isCompleted = false;

            if (questType == QuestType.DoTask)
            {
                this.doTask = doTask;
            }
            else if (questType != QuestType.DoTask)
            {
                if (numThingsRequired != 0)
                {
                    this.numThingsRequired = numThingsRequired;
                }
            }
        }

        public bool CheckCompletion()
        {
            return isCompleted;
        }
    }
}
