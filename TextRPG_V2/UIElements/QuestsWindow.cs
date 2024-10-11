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
            questManager.CreateQuests();
            
            base.AddLine(1, questManager.GetActiveQuests()[0].name);
            base.AddLine(2, questManager.GetActiveQuests()[0].description);
            base.AddLine(4, questManager.GetActiveQuests()[1].name);
            base.AddLine(5, questManager.GetActiveQuests()[1].description);
            base.AddLine(7, questManager.GetActiveQuests()[2].name);
            base.AddLine(8, questManager.GetActiveQuests()[2].description);
        }
    }
}
