using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2.Managers;

namespace TextRPG_V2
{
    public class QuestManager
    {

        private List<Quest> quests;

        /// <summary>
        /// Quest manager constructor
        /// </summary>
        public QuestManager()
        {
            quests = new List<Quest>();
            CreateQuests();
        }

        /// <summary>
        /// Adds a quest to the master list of quests
        /// </summary>
        /// <param name="quest">quest to be added</param>
        public void AddQuest(Quest quest)
        {
            quests.Add(quest);
        }

        /// <summary>
        /// Removes a quest from the master list of quests
        /// </summary>
        /// <param name="quest">quest to be removed</param>
        public void RemoveQuest(Quest quest)
        {
            if (quests.Contains(quest))
            {
                quests.Remove(quest);
            }
        }

        /// <summary>
        /// Gets a list of all completed quests
        /// </summary>
        /// <returns>A list of all completed quests</returns>
        public List<Quest> GetCompletedQuests()
        {
            return quests.Where(i => i.CheckCompletion()).ToList();
        }

        /// <summary>
        /// Gets a list of all active quests
        /// </summary>
        /// <returns>A list of all active quests</returns>
        public List<Quest> GetActiveQuests()
        {
            return quests.Where(i => !i.CheckCompletion()).ToList();
        }

        /// <summary>
        /// Gets a list of all quests
        /// </summary>
        /// <returns>A list of all quests</returns>
        public List<Quest> GetAllQuests()
        {
            return quests;
        }

        /// <summary>
        /// Updates the relevant quest
        /// </summary>
        /// <param name="uIManager">The UI manager</param>
        /// <param name="questType">The type of quest that is being updated at the time</param>
        public void UpdateReleventQuest(UIManager uIManager, Quest.QuestType questType)
        {
            foreach (Quest quest in GetActiveQuests())
            {
                if (quest.questType == questType)
                {
                    quest.numThingsDone += 1;
                }
                if (quest.numThingsDone == quest.maxNumThingsRequired)
                {
                    quest.isCompleted = true;
                    uIManager.AddEventToLog(quest.name + " Completed.");
                }
            }
        }

        /// <summary>
        /// Creates the quests
        /// </summary>
        public void CreateQuests()
        {
            Quest quest1 = new Quest("Quest 1:", "Defeat 10 Enemies", Quest.QuestType.KillEnemies, 0, 10);
            Quest quest2 = new Quest("Quest 2:", "Buy 3 Items", Quest.QuestType.PurchaseItems, 0, 3);
            Quest quest3 = new Quest("Quest 3:", "Escape the Dungeon", Quest.QuestType.FindExit, 0, 13);

            AddQuest(quest1);
            AddQuest(quest2);
            AddQuest(quest3);
        }
    }
}
