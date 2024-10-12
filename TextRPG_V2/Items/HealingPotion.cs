using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2.Items
{
    public class HealingPotion : Item
    {
        private int healing; //amount of healing done by the potion

        /// <summary>
        /// Empty constructor method for a "Healing Potion" Item.
        /// </summary>
        public HealingPotion() : base("Healing Potion")
        {
            healing = GlobalVariables.potionHealingValue;
            SetSymbol('p');
            SetColor(ConsoleColor.Green);
            costSeed = healing;
        }

        /// <summary>
        /// Method that uses (consumes) the Healing Potion
        /// </summary>
        /// <param name="target">The Entity that is using the item</param>
        /// <returns>message describing the result of using the item</returns>
        public override string Use(Entity target)
        {
            string message = target.GetName() + " used a " + GetName();
            int oldHp = target.health.GetHp();

            target.health.ModHp(healing);

            message += " and healed " + (target.health.GetHp() - oldHp) + " hp.";

            return message;
        }
    }
}
