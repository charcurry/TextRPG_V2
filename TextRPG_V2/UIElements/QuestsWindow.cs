using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2.Managers;

namespace TextRPG_V2.UIElements
{
    internal class QuestsWindow : UIWindow
    {
        private readonly QuestManager questManager;

        /// <summary>
        /// Quests window constructor
        /// </summary>
        /// <param name="questManager">The quest manager that stores and deals with the quests</param>

        public QuestsWindow(QuestManager questManager) : base(GlobalVariables.questsWindowWidth, GlobalVariables.questsWindowHeight)
        {
            this.questManager = questManager;
        }

        /// <summary>
        /// Updates quests status' so they show up in the quest window
        /// </summary>
        public void UpdateQuests()
        {
            if (questManager.GetAllQuests().Count == 0)
            {
                base.AddLine(1, "No Active Quests");
                return;
            }

            int i = 1;
            foreach (Quest quest in questManager.GetAllQuests())
            {
                base.AddLine(i, quest.name);
                base.AddLine(i + 1, quest.description + (quest.isCompleted ? " ✓" : ""));

                if (quest.questType != Quest.QuestType.FindExit)
                {
                    base.AddLine(i + 2, quest.numThingsDone + "/" + quest.maxNumThingsRequired);
                }

                // Adding 3 leaves enough of a gap between quests so they dont overwrite each other
                i += 3;
            }
        }
    }
}
