using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    public interface IBattle
    {
        #region Properties

        BattleModeName BattleMode { get; set; }

        #endregion

        #region methods

        //int AttackCalc();
        ////int DefendCalc();
        //int Retreat();

        #endregion
    }
}
