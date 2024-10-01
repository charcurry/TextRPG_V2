using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{
    public class EnemyStatWindow : UIWindow
    {
        private Entity entity; //entity that the stat window tracks

        /// <summary>
        /// Constructor method for a StatWindow object
        /// </summary>
        /// <param name="entity">The target Entity for the StatWindow to track</param>
        public EnemyStatWindow(Entity entity) : base(GlobalVariables.enemyStatWindowWidth, GlobalVariables.enemyStatWindowHeight)
        {
            this.entity = entity;
        }

        /// <summary>
        /// Method that updates the displayed info of the target in the StatWindow
        /// </summary>
        public void SetTargetInfo()
        {
            if (entity != null)
            {
                base.ClearContents();

                base.AddLine(1, "NAME: " + entity.GetName());
                base.AddLine(2, "HP: " + entity.health.GetHp() + "/" + entity.health.GetMaxHp());
                base.AddLine(3, "ATK: " + entity.atk.GetStat());
                base.AddLine(4, "DEF: " + entity.def.GetStat());
                base.AddLine(5, "MAG: " + entity.mag.GetStat());
                base.AddLine(6, "RES: " + entity.res.GetStat());
                base.AddLine(7, "SPD: " + entity.spd.GetStat());
                base.AddLine(8, "SKL: " + entity.skl.GetStat());
                base.AddLine(9, "LUC: " + entity.luc.GetStat());
            } 
        }

        /// <summary>
        /// Mutator Method that changes the target of the Stat Window
        /// </summary>
        /// <param name="entity"></param>
        public void ChangeTarget(Entity entity)
        {
            this.entity = entity;
        }
    }
}
