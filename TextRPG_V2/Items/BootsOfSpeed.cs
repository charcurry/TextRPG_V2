using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{
    public class BootsOfSpeed : Item
    {
        /// <summary>
        /// Constructor method for a "Boots of Speed" Item.
        /// </summary>
        public BootsOfSpeed() : base("Boots of Speed")
        {
            SetSymbol('b');
            SetColor(ConsoleColor.DarkCyan);
            costSeed = 159;
        }

        /// <summary>
        /// Method that uses (equips) the boots of speed
        /// </summary>
        /// <param name="target">The Entity that is using the item</param>
        /// <returns>message describing the result of using the item</returns>
        public override string Use(Entity target)
        {
            string message = target.GetName() + " used a " + GetName(); ;
            message += " and maxed their speed";

            target.spd.SetStat(target.spd.GetMaxStat());

            return message;
        }
    }
}
