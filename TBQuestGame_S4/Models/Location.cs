using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TBQuestGame_S1.Models
{
    public class Location : ObservableObject
    {
        #region Fields
        private int _id;
        private string _name;
        private string _description;
        private string _message;
        private int _enemyRank;
        private bool _isAccessible;
        private ObservableCollection<GameItemQuantity> _gameItems;
        private ObservableCollection<NPC> _npcs;
        #endregion

        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public int EnemyRank
        {
            get { return _enemyRank; }
            set { _enemyRank = value; }
        }

        public bool IsAccessible
        {
            get { return _isAccessible; }
            set { _isAccessible = value; }
        }

        public ObservableCollection<GameItemQuantity> GameItems
        {
            get { return _gameItems; }
            set { _gameItems = value; }
        }

        public ObservableCollection<NPC> NPCS
        {
            get { return _npcs; }
            set { _npcs = value; }
        }
        #endregion

        #region Constructors

        public Location()
        {
            _gameItems = new ObservableCollection<GameItemQuantity>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// update list of items in a location
        /// </summary>
        public void UpdateLocationGameItems()
        {
            ObservableCollection<GameItemQuantity> updatedLocationGameItems = new ObservableCollection<GameItemQuantity>();

            foreach (GameItemQuantity gameItemQuantity in _gameItems)
            {
                updatedLocationGameItems.Add(gameItemQuantity);
            }

            GameItems.Clear();

            foreach (GameItemQuantity gameItemQuantity in updatedLocationGameItems)
            {
                GameItems.Add(gameItemQuantity);
            }
        }

        /// <summary>
        /// Add game item to location
        /// </summary>
        public void AddGameItemToLocation(GameItemQuantity selectedGameItem)
        {
            GameItemQuantity gameItemQuantity = _gameItems.FirstOrDefault(i => i.GameItem.Id == selectedGameItem.GameItem.Id);

            if (gameItemQuantity == null)
            {
                GameItemQuantity newGameItemQuantity = new GameItemQuantity();
                newGameItemQuantity.GameItem = selectedGameItem.GameItem;
                newGameItemQuantity.Quantity = 1;

                _gameItems.Add(newGameItemQuantity);
            }
            else
            {
                gameItemQuantity.Quantity++;
            }

            UpdateLocationGameItems();
        }

        /// <summary>
        /// remove items from inventory
        /// </summary>
        public void RemoveGameItemQuantityFromLocation(GameItemQuantity selectedGameItemQuantity)
        {
            GameItemQuantity gameItemQuantity = _gameItems.FirstOrDefault(i => i.GameItem.Id == selectedGameItemQuantity.GameItem.Id);

            if (gameItemQuantity != null)
            {
                if (selectedGameItemQuantity.Quantity == 1)
                {
                    _gameItems.Remove(gameItemQuantity);
                }
                else
                {
                    gameItemQuantity.Quantity--;
                }
            }

            UpdateLocationGameItems();
        }

        public override string ToString()
        {
            return _name;
        }

        #endregion
    }
}
