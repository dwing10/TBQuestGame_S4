using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    public abstract class NPC : Character
    {

        #region Properties

        public string Description { get; set; }
        public string Information
        {
            get { return InformationText(); }
            set { }
        }

        #endregion

        #region Constructors
        public NPC()
        {

        }

        public NPC(string id, string name, CharacterTitle title, int rank, int power, string description) : 
            base(id, name, title, rank, power)
        {
            ID = id;
            Name = name;
            Title = title;
            Rank = rank;
            Power = power;
            Description = description;
        }

        #endregion

        #region Methods

        protected abstract string InformationText();

        #endregion

    }
}
