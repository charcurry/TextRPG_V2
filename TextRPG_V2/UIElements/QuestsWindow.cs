using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2.Managers;

namespace TextRPG_V2.UIElements
{
    internal class QuestsWindow : UIWindow
    {
        private QuestManager questManager;


        public QuestsWindow() : base(GlobalVariables.questsWindowWidth, GlobalVariables.questsWindowHeight)
        {
            questManager = new QuestManager();

            
            base.AddLine(1, "Quest 1:");
            base.AddLine(2, "Defeat 10 Enemies");
            base.AddLine(4, "Quest 2:");
            base.AddLine(5, "Buy 3 Items");
            base.AddLine(7, "Quest 3:");
            base.AddLine(8, "Escape the Dungeon");
        }

        public void CreateQuests()
        {
            Quest quest1 = new Quest("Quest 1:", "Defeat 10 Enemies");
            Quest quest2 = new Quest("Quest 2:", "Buy 3 Items");
            Quest quest3 = new Quest("Quest 3:", "Escape the Dungeon");

            questManager.AddQuest(quest1);
            questManager.AddQuest(quest2);
            questManager.AddQuest(quest3);
        }
    }
}
