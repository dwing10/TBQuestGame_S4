using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    public class Treasure : GameItem
    {
        #region Enums

        public enum TreasureType
        {
            Coin,
            Gem
        }

        #endregion

        #region Properties

        public TreasureType Type { get; set; }

        #endregion

        #region Constructors

        public Treasure(string id, string name, int value, string description, TreasureType type) : base(id, name, value, description)
        {
            Type = type;
        }

        #endregion

        #region Methods
        public override string InformationString()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
