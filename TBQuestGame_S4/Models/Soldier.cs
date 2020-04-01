using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    public class Soldier : GameItem
    {
        #region Properties

        public int SoldierAttack { get; set; }
        public int SoldierDefense { get; set; }

        #endregion

        #region Constructors

        public Soldier(string id, string name, int value, string description, int soldierAttack, int soldierDefense)
            : base(id, name, value, description)
        {
            SoldierAttack = soldierAttack;
            SoldierDefense = soldierDefense;
        }

        #endregion

        #region Methods

        public override string InformationString()
        {
            if (SoldierAttack != 0)
            {
                return $"{Name}: {Description}\nAttack: {SoldierAttack}";
            }
            if (SoldierDefense != 0)
            {
                return $"{Name}: {Description}\nAttack: {SoldierDefense}";
            }
            else
            {
                return $"{Name}: {Description}";
            }
        }

        #endregion
    }
}
