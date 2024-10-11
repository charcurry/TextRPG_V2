using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2.Managers
{
    internal class QuestManager
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
    }
}
