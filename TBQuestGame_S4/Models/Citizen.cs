using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    class Citizen : NPC, ISpeak
    {

        #region Properties

        Random rand = new Random();

        public List<string> Messages { get; set; }

        #endregion

        #region Constructors

        public Citizen()
        {

        }

        public Citizen(string id, string name, CharacterTitle title, int rank, int power, string description, List<string> messages) :
            base(id, name, title, rank, power, description)
        {
            Messages = messages;
        }

        #endregion

        #region Methods

        public string Speak()
        {
            if (this.Messages != null)
            {
                return GetMessage();
            }
            else
            {
                return "";
            }
        }

        private string GetMessage()
        {
            int messageIndex = rand.Next(0, Messages.Count());
            return Messages[messageIndex];
        }

        protected override string InformationText()
        {
            return $"{Name} - {Description}";
        }

        #endregion
    }
}
