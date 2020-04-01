using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    public abstract class GameItem
    {
        #region Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

        public string Information
        {
            get { return InformationString(); }
        }
        #endregion

        #region Constructors
        public GameItem(string id, string name, int value, string description)
        {
            Id = id;
            Name = name;
            Value = value;
            Description = description;
        }
        #endregion

        #region Methods
        public abstract string InformationString();
        #endregion
    }
}
