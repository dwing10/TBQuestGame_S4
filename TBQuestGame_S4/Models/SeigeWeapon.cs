using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    class SeigeWeapon : GameItem
    {
        #region Properties
        public int AttackIncrease { get; set; }
        #endregion

        #region Constructors
        public SeigeWeapon(string id, string name, int value, string description, int attackIncrease) 
            : base(id, name, value, description)
        {
            AttackIncrease = attackIncrease;
        }
        #endregion

        #region Methods
        public override string InformationString()
        {
            if (AttackIncrease != 0)
            {
                return $"{Name}: {Description}\nAttack: {AttackIncrease}";
            }
            else
            {
                return $"{Name}: {Description}";
            }
        }
        #endregion
    }
}
