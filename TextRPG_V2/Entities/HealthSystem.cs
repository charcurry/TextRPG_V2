﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2.Entities
{
    internal class HealthSystem
    {
        //Health variables
        private int maxHp; //the current maximum value for hp
        private int currentHp; //the current hit points for the health system
        private int trueHp; //max hp for the system outside of external modification (potions, equipment, etc...)
        private bool unsyched; //bool tracking if trueHp and maxHp are synched

        /// <summary>
        /// Constructor method for generating a health object based on input value
        /// </summary>
        /// <param name="hp">The target value for Hp</param>
        public HealthSystem(int hp)
        {
            maxHp = hp;
            currentHp = maxHp;
            trueHp = currentHp;

            unsyched = false;
        }

        /// <summary>
        /// Mutator method that sets the max hp for the health system to input value
        /// </summary>
        /// <param name="maxHp">The maximum value hit points can reach</param>
        public void SetMaxHp(int maxHp)
        {
            int gapHp = this.maxHp - currentHp;
            this.maxHp = maxHp;
            currentHp = this.maxHp - gapHp;

            //sets current hp to 1 if changing maxHp would bring Entity's current Hp to less than 1 
            if (currentHp < 1 && maxHp >= 1)
            {
                currentHp = 1;
            }
        }

        /// <summary>
        /// Mutator method that modifies the max hp for the health system by the input value
        /// </summary>
        /// <param name="maxHpMod">The value by which max hp will be modified</param>
        public void ModMaxHp(int maxHpMod)
        {
            int gapHp = this.maxHp - currentHp;
            maxHp += maxHpMod;
            currentHp = this.maxHp - gapHp;

            //sets current hp to 1 if changing maxHp would bring Entity's current Hp to less than 1 
            if (currentHp < 1 && maxHp >= 1)
            {
                currentHp = 1;
            }
        }

        /// <summary>
        /// Accessor method for the max hp for the health system
        /// </summary>
        /// <returns>The maximum value hit points can reach</returns>
        public int GetMaxHp()
        {
            return maxHp;
        }

        /// <summary>
        /// Mutator method that sets hp for the health system to input value
        /// </summary>
        /// <param name="hp">the hp for the health system</param>
        public void SetHp(int hp)
        {
            currentHp = hp;
        }

        /// <summary>
        /// Mutator method that modifies the hp for the health system by input value
        /// </summary>
        /// <param name="modHp">the value by which hp will be modified</param>
        public void ModHp(int modHp)
        {
            currentHp += modHp;
        }

        /// <summary>
        /// Accessor method for the hp value of the health system
        /// </summary>
        /// <returns>The current hp for the health system</returns>
        public int GetHp()
        {
            return currentHp;
        }

        /// <summary>
        /// Accessor method for the max hp for the system outside of external modification (potions, equipment, etc...)
        /// </summary>
        /// <returns>max hp for the system outside of external modification (potions, equipment, etc...)</returns>
        public int getTrueHp()
        {
            return trueHp;
        }

        /// <summary>
        /// Mutator method that sets true hp equal to input value
        /// </summary>
        /// <param name="trueHpValue">target value for the true hp of the health system</param>
        public void SetTrueHp(int trueHpValue)
        {
            trueHp = trueHpValue;

            if (!unsyched)
            {
                SetMaxHp(trueHp);
            }
        }

        /// <summary>
        /// Mutator method that modifies the true hp by an input value
        /// </summary>
        /// <param name="trueHpMod">value by which true hp will be modified</param>
        public void ModTrueHp(int trueHpMod)
        {
            trueHp += trueHpMod;

            if (!unsyched)
            {
                SetMaxHp(trueHp);
            }
        }

        /// <summary>
        /// Mutator method that unsynchs trueHp and maxHp
        /// </summary>
        public void UnsynchHp()
        {
            unsyched = true;
        }

        /// <summary>
        /// Mutator method that synchs trueHp and maxHp
        /// </summary>
        public void SynchHp()
        {
            unsyched = false;

            SetMaxHp(trueHp);
        }
    }
}
