using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2.Managers
{
    internal class Quest
    {
        string name;
        string description;
        bool isCompleted;

        public Quest(string name, string description)
        {
            this.name = name;
            this.description = description;
            isCompleted = false;
        }

        public bool CheckCompletion()
        {
            return isCompleted;
        }
    }
}
