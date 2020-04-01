using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame_S1.Models;

namespace TBQuestGame_S1.Models
{
    public class EnemyMilitary : NPC, ISpeak, IBattle
    {

        #region Properties

        private Player _player;

        Random rand = new Random();

        private const int DEFENDER_DAMAGE_ADJUSTMENT = 10;
        private const int MAXIMUM_RETREAT_DAMAGE = 10;

        public List<string> Messages { get; set; }
        public BattleModeName BattleMode { get; set; }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        #endregion

        #region Constructors

        public EnemyMilitary()
        {

        }

        public EnemyMilitary(string id, string name, CharacterTitle title, int rank, int power, string description, List<string> messages) : 
            base (id, name, title, rank, power, description)
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

        #region Battle Methods

        /// <summary>
        /// checks to see if the retreat was successful
        /// </summary>
        public int Retreat()
        {
            int randomAttack = random.Next(2, 10);
            int damage = 500 / randomAttack;

            if (damage <= 100)
            {
                return damage;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #endregion
    }
}
