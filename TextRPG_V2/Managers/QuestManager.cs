using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2.Managers;

namespace TextRPG_V2
{
    public class QuestManager
    {
        private List<Quest> activeQuests = new List<Quest>();

        public QuestManager()
        {
            activeQuests = new List<Quest>();
        }

        public void AddQuest(Quest quest)
        {
            activeQuests.Add(quest);
        }

        public void RemoveQuest(Quest quest)
        {
            if (activeQuests.Contains(quest))
            {
                activeQuests.Remove(quest);
            }
        }

        public void UpdateQuestStatus()
        {
            foreach (Quest quest in activeQuests)
            {
                quest.CheckCompletion();
            }
        }

        public List<Quest> GetCompletedQuests()
        {
            return activeQuests.Where(i => i.CheckCompletion()).ToList();
        }

        public List<Quest> GetActiveQuests()
        {
            return activeQuests.Where(i => !i.CheckCompletion()).ToList();
        }

        public void CreateQuests()
        {
            Quest quest1 = new Quest("Quest 1:", "Defeat 10 Enemies", Quest.QuestType.KillEnemies, false, 10);
            Quest quest2 = new Quest("Quest 2:", "Buy 3 Items", Quest.QuestType.AcquireItems, false, 3);
            Quest quest3 = new Quest("Quest 3:", "Escape the Dungeon", Quest.QuestType.DoTask, true);

            AddQuest(quest1);
            AddQuest(quest2);
            AddQuest(quest3);
        }
    }
}
