using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
   public abstract class Character
    {
        #region Enums
        public enum CharacterTitle
        {
            Imperator,
            Praetor,
            Centurion,
            Praetorian,
            Cavalry,
            Archer,
            Legionnaire,
            Enemy,
            Trader,
            Wanderer
        }
        #endregion

        #region Fields
        private string _id;
        private string _name;
        private CharacterTitle _title;
        private int _rank;
        private int _power;

        protected Random random = new Random();
        #endregion

        #region Properties
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public CharacterTitle Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        #endregion

        #region Constructors

        public Character()
        {

        }

        public Character(string id, string name, CharacterTitle title, int rank, int power)
        {
            _name = name;
            _title = title;
            _rank = rank;
            _power = power;
        }

        #endregion

        #region Methods

        public virtual string CharacterDescription()
        {
            string description = "";

            return description;
        }

        #endregion
    }
}
