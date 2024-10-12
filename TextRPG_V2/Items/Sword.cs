using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG_V2.Items
{
    public class Sword : Item
    {
        int atkIncrease; //amount by which a sword increases attack

        /// <summary>
        /// Empty constructor method for a "Swords" Item.
        /// </summary>
        public Sword() : base("Sword") 
        {
            atkIncrease = GlobalVariables.swordAtkIncrease;
            SetSymbol('s');
            costSeed = atkIncrease;
        }

        /// <summary>
        /// Method that uses (equips) the sword
        /// </summary>
        /// <param name="target">The Entity that is using the item</param>
        /// <returns>message describing the result of using the item</returns>
        public override string Use(Entity target)
        {
            string message = target.GetName() + " used a " + GetName();
            int oldAtk = target.atk.GetStat();

            target.atk.ModStat(atkIncrease);

            message += " and increased their atk by " + (target.atk.GetStat() - oldAtk) + "."; 

            return message;
        }
    }
}
