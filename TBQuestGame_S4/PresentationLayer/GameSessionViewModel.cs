using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame_S1.Models;
using TBQuestGame_S1.DataLayer;
using System.Collections.ObjectModel;

namespace TBQuestGame_S1.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {
        #region Fields
        private GameSessionViewModel _gameSessionViewModel;

        private Player _player;
        private List<string> _messages;

        private Map _gameMap;
        private Location _currentLocation;
        private string _currentLocationName;
        private ObservableCollection<Location> _accessobleLocations;
        private string _currentLocationInformation;

        private GameItemQuantity _currentGameItem;

        private NPC _currentNPC;

        private Random random = new Random();

        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        #endregion

        #region Properties
        public GameSessionViewModel gameSessionViewModel
        {
            get { return _gameSessionViewModel; }
            set { _gameSessionViewModel = value; }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return string.Join("\n\n", _messages); }
        }

        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                _currentLocationInformation = _currentLocation.Description;
                OnPropertyChange(nameof(CurrentLocation));
                OnPropertyChange(nameof(CurrentLocationInformation));
            }
        }

        public string CurrentLocationName
        {
            get { return _currentLocationName; }
            set
            {
                _currentLocationName = value;
                OnPropertyChange("CurrentLocation");
            }
        }

        public ObservableCollection<Location> AccessibleLocations
        {
            get { return _accessobleLocations; }
            set
            {
                _accessobleLocations = value;
                OnPropertyChange(nameof(AccessibleLocations));
            }
        }

        public string CurrentLocationInformation
        {
            get { return _currentLocationInformation; }
            set
            {
                _currentLocationInformation = value;
                OnPropertyChange(nameof(CurrentLocationInformation));
            }
        }

        public GameItemQuantity CurrentGameItem
        {
            get { return _currentGameItem; }
            set { _currentGameItem = value; }
        }

        public NPC CurrentNPC
        {
            get { return _currentNPC; }
            set
            {
                _currentNPC = value;
                OnPropertyChange(nameof(CurrentNPC));
            }
        }
        #endregion

        #region Constructors

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(Player player, List<string> initialMessage, Map gameMap, GameSessionViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;
            _player = player;
            _gameMap = gameMap;
            _currentLocation = _gameMap.CurrentLocation;
            _accessobleLocations = new ObservableCollection<Location>();
            _messages = initialMessage;

            InitializeView();
        }

        #endregion

        #region Methods

        /// <summary>
        /// initialize the view
        /// </summary>
        public void InitializeView()
        {
            _player.UpdateInventoryCategories();
            _currentLocationInformation = CurrentLocation.Description;
        }

        #region Game Item Methods

        /// <summary>
        /// add item from location to player's inventory
        /// </summary>
        public void AddItemToInventory()
        {
            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {
                GameItemQuantity selectedGameItemQuantity = _currentGameItem as GameItemQuantity;

                _currentLocation.RemoveGameItemQuantityFromLocation(selectedGameItemQuantity);
                _player.AddGameItemToInventory(selectedGameItemQuantity);

            }
        }

        public void RemoveItemFromInventory()
        {
            if (_currentGameItem != null)
            {
                GameItemQuantity selectedGameItemQuantity = _currentGameItem as GameItemQuantity;

                _player.RemoveGameItemFromInventory(selectedGameItemQuantity);
            }
        }

        /// <summary>
        /// calls other methods based on the items used
        /// </summary>
        public void OnUseGameItem()
        {
            switch (_currentGameItem.GameItem)
            {
                case Buff buff:
                    ProcessBuffUse(buff);
                    RemoveItemFromInventory();
                    break;
                case SeigeWeapon seigeWeapon:
                    ProcessSeigeWeaponUse(seigeWeapon);
                    RemoveItemFromInventory();
                    break;
                default:
                    break;
            }
        }

        private void ProcessBuffUse(Buff buff)
        {
            if (buff.Id == "INS")
            {
                Player.Power = Player.Power + 100;
            }
            if (buff.Id == "BOL")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "LEG");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity += 25;
                    Player.LegionnaireNumbers += 25;
                }
            }
            if (buff.Id == "TRI")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity += 1000;
                }
            }
        }

        private void ProcessSeigeWeaponUse(SeigeWeapon seigeWeapon)
        {
            if (seigeWeapon.Id == "CAT")
            {
                Player.Power += 200;
                Player.NumOfSeigeWeapons += 1;
            }
            if (seigeWeapon.Id == "BAL")
            {
                Player.Power += 100;
                Player.NumOfSeigeWeapons += 1;
            }
        }

        #endregion

        #region Movement methods

        /// <summary>
        /// update the accessible location
        /// </summary>
        private void UpdateAccessibleLocations()
        {
            _accessobleLocations.Clear();

            foreach (Location location in _gameMap.Locations)
            {
                if (location.IsAccessible == true)
                {
                    _accessobleLocations.Add(location);
                }
            }

            _accessobleLocations.Remove(_accessobleLocations.FirstOrDefault(l => l == _currentLocation));

            OnPropertyChange(nameof(AccessibleLocations));
        }

        /// <summary>
        /// timer that is used when traveling
        /// </summary>
        //public void Timer(string tagName)
        //{
        //    travelTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    travelTimer.Interval = new TimeSpan(0, 0, 5);
        //    travelTimer.Start();

        //    Move(tagName);

        //}


        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    travelTimer.Stop();
        //    travelTimer.IsEnabled = false;
        //}

        /// <summary>
        /// Binds buttons to locations
        /// </summary>
        public void Move(string tagName)
        {
            switch (tagName)
            {
                case "Alheimurrinn":
                    if (Player.NumOfSeigeWeapons > 5 && Player.PraetorNumbers == 1 && Player.PraetorianNumbers == 5 && Player.CenturionNumbers == 15)
                    {
                        _currentLocation.IsAccessible = true;
                        foreach (Location location in AccessibleLocations)
                        {
                            if (tagName == location.Name)
                            {
                                CurrentLocation = location;
                            }
                        }
                    }
                    else
                    {
                        _currentLocation.IsAccessible = false;
                        CurrentLocationInformation = "You lack the military might to travel to this location";
                    }

                    break;

                case "Qua Redi":
                    if (Player.NumOfSeigeWeapons > 3 && Player.CenturionNumbers > 3)
                    {
                        _currentLocation.IsAccessible = true;
                        foreach (Location location in AccessibleLocations)
                        {
                            if (tagName == location.Name)
                            {
                                CurrentLocation = location;
                            }
                        }
                    }
                    else
                    {
                        _currentLocation.IsAccessible = false;
                        CurrentLocationInformation = "Acquire more seige weaponry to tr and stronger troops avel to this location.";
                    }
                    break;

                case "Dore":
                    if (Player.ElkmireIsDefeated && Player.PlayerHasShips == true)
                    {
                        _currentLocation.IsAccessible = true;
                        foreach (Location location in AccessibleLocations)
                        {
                            if (tagName == location.Name)
                            {
                                CurrentLocation = location;
                            }
                        }
                    }
                    else
                    {
                        _currentLocation.IsAccessible = false;
                        CurrentLocationInformation = "You must secure the port of Elkmire and acquire ships " +
                            "before you can travel to Dore.";
                    }
                    break;

                case "North Bourg":
                    foreach (Location location in AccessibleLocations)
                    {
                        if (tagName == location.Name)
                        {
                            CurrentLocation = location;
                        }
                    }
                    break;

                case "South Bourg":
                    if (Player.NorthBourgIsDefeated == true)
                    {
                        _currentLocation.IsAccessible = true;
                        foreach (Location location in AccessibleLocations)
                        {
                            if (tagName == location.Name)
                            {
                                CurrentLocation = location;
                            }
                        }
                    }
                    else
                    {
                        _currentLocation.IsAccessible = false;
                        CurrentLocationInformation = "You must defeat the warlords of North Bourg before you can" +
                            "march your army to South Bourg";
                    }
                    break;

                case "Elkmire":
                    foreach (Location location in AccessibleLocations)
                    {
                        if (tagName == location.Name)
                        {
                            CurrentLocation = location;
                        }
                    }
                    break;
                default:
                    break;
            }

            UpdateAccessibleLocations();
        }

        #endregion

        #region Button methods

        /// <summary>
        /// Opens barracks window
        /// </summary>
        public void ShowBarracks()
        {
            BarracksView barracksView = new BarracksView(_player);
            barracksView.Show();
        }

        /// <summary>
        /// opens recruitment window
        /// </summary>
        public void DisplayRecruitment()
        {
            RecruitWindow recruitWindow = new RecruitWindow(_player);
            recruitWindow.ShowDialog();
        }

        /// <summary>
        /// opens information window
        /// </summary>
        public void ShowInfo()
        {
            InformationView info = new InformationView();
            info.Show();
        }

        /// <summary>
        /// handle the speak to event in the view
        /// </summary>
        public void OnPlayerTalkTo()
        {
            if (CurrentNPC != null && CurrentNPC is ISpeak)
            {
                ISpeak speakingNpc = CurrentNPC as ISpeak;
                CurrentLocationInformation = speakingNpc.Speak();
            }
        }

        /// <summary>
        /// handle attack in the view
        /// </summary>
        public void OnPlayerAttack()
        {
            _player.BattleMode = BattleModeName.ATTACK;
            Battle();
        }

        /// <summary>
        /// handle retreat in the view
        /// </summary>
        public void OnPlayerRetreat()
        {
            _player.BattleMode = BattleModeName.RETREAT;
            Battle();
        }

        #endregion

        #region Battle methods

        /// <summary>
        /// calculates player's hitpoints
        /// </summary>
        private int CalculatePlayerHitPoints()
        {
            int hitPoints = 0;

            switch (_player.BattleMode)
            {
                case BattleModeName.ATTACK:
                    hitPoints = _player.Power;
                    break;
                case BattleModeName.RETREAT:
                    hitPoints = _player.Retreat();
                    break;
                default:
                    break;
            }

            return hitPoints;
        }

        private int CalculateNPCHitPoints(IBattle battleNPC)
        {
            int battleNPCHitPoints = 0;

            //switch (NPCBattleResponse())
            //{
            //    case BattleModeName.ATTACK:
            //        battleNPCHitPoints = battleNPC.AttackCalc();
            //        break;
            //    case BattleModeName.RETREAT:
            //        battleNPCHitPoints = battleNPC.Retreat();
            //        break;
            //    default:
            //        break;
            //}

            if (_currentNPC.Rank == 1)
            {
                battleNPCHitPoints = random.Next(300, 499);
            }
            else if (_currentNPC.Rank == 2)
            {
                battleNPCHitPoints = random.Next(500, 749);
            }
            else if (_currentNPC.Rank == 3)
            {
                battleNPCHitPoints = random.Next(750, 999);
            }
            else if (_currentNPC.Rank == 4)
            {
                battleNPCHitPoints = random.Next(1000, 1249);
            }
            else if (_currentNPC.Rank == 5)
            {
                battleNPCHitPoints = random.Next(1249, 1500);
            }
            else
            {
                battleNPCHitPoints = 0;
            }

            return battleNPCHitPoints;
        }

        /// <summary>
        /// Rolls a die to determine npc's response
        /// </summary>
        private BattleModeName NPCBattleResponse()
        {
            BattleModeName npcBattleResponse = BattleModeName.RETREAT;

            switch (DieRoll(6))
            {
                case 1:
                    npcBattleResponse = BattleModeName.ATTACK;
                    break;
                case 2:
                    npcBattleResponse = BattleModeName.RETREAT;
                    break;
                case 3:
                    npcBattleResponse = BattleModeName.ATTACK;
                    break;
                case 4:
                    npcBattleResponse = BattleModeName.RETREAT;
                    break;
                case 5:
                    npcBattleResponse = BattleModeName.ATTACK;
                    break;
                case 6:
                    npcBattleResponse = BattleModeName.ATTACK;
                    break;
            }

            return npcBattleResponse;
        }

        /// <summary>
        /// battle method
        /// </summary>
        private void Battle()
        {
            if (_currentNPC is IBattle)
            {
                IBattle battleNPC = _currentNPC as IBattle;
                int playerHitPoints = 0;
                int battleNpcHitPoints = 0;
                playerHitPoints = CalculatePlayerHitPoints();
                battleNpcHitPoints = CalculateNPCHitPoints(battleNPC);

                //bool battling = false;
                //do
                //{

                //    CurrentLocationInformation =
                //    $"Player: {_player.BattleMode}     Hit Points: {playerHitPoints}" + Environment.NewLine +
                //    $"NPC: {battleNPC.BattleMode}     Hit Points: {battleNpcHitPoints}" + Environment.NewLine;
                //} while (!battling);

                if (playerHitPoints >= battleNpcHitPoints)
                {
                    _currentLocation.NPCS.Remove(_currentNPC);
                    DetermineRewards();
                }
                else
                {
                    DetermineLosses();
                }

            }
            else
            {
                CurrentLocationInformation = "It seems the enemy is not ready to meet us in battle, try again later.";
            }
        }

        /// <summary>
        /// Determine rewards for winning
        /// </summary>
        private void DetermineRewards()
        {
            int troopLosses = 0;

            //if location is North Bourg
            if (_currentLocation.Name == "North Bourg")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity += 100;
                }
                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(15, 25);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have won and have gained 100 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";

                Player.NorthBourgIsDefeated = true;
            }

            //if location is south bourg
            else if (_currentLocation.Name == "South Bourg")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity += 300;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(20, 35);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have won and have gained 300 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";
            }

            //if location is elkmire
            else if (_currentLocation.Name == "Elkmire")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity += 550;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(25, 40);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have won and have gained 550 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";

                Player.ElkmireIsDefeated = true;
            }

            //if location is dore
            else if (_currentLocation.Name == "Dore")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity += 700;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(55, 75);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have won and have gained 700 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";

            }

            //if location is qua redi
            else if (_currentLocation.Name == "Qua Redi")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity = 1500;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(100, 125);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have won and have gained 1500 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";

            }

            //if locaion is alheimurrinn
            else if (_currentLocation.Name == "Alheimurrinn")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity += 10000;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(150, 225);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have won and have gained 10000 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";

            }
        }

        /// <summary>
        /// Determines losses for player
        /// </summary>
        private void DetermineLosses()
        {
            int troopLosses = 0;

            //if location is north bourg
            if (_currentLocation.Name == "North Bourg")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity -= 100;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(90, 110);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have been defeated and have lost 100 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";
            }

            //if location is south bourg
            else if (_currentLocation.Name == "South Bourg")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity -= 300;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(100, 120);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have been defeated and have lost 300 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";
            }

            //if location is elkmire
            else if (_currentLocation.Name == "Elkmire")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity -= 550;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(120, 140);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have been defeated and have lost 550 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";
            }

            //if location is dore
            else if (_currentLocation.Name == "Dore")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity -= 700;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(150, 200);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have been defeated and have lost 700 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";
            }

            //if location is qua redi
            else if (_currentLocation.Name == "Qua Redi")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity -= 1500;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(200, 250);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have been defeated and have lost 1500 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";
            }

            //if location is alheimurrinn
            else if (_currentLocation.Name == "Alheimurrinn")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null)
                {
                    gameItemQuantity.Quantity -= 10000;
                }

                if (Player.LegionnaireNumbers > 50)
                {
                    troopLosses = random.Next(300, 400);
                    Player.LegionnaireNumbers -= troopLosses;
                    Player.Power -= troopLosses;
                }

                CurrentLocationInformation = "You have been defeated and have lost 10000 gold!" + Environment.NewLine +
                    "" + Environment.NewLine +
                    $"Legionnaire lost in the battle: {troopLosses}";
            }
        }

        #endregion

        #region Helper Methods

        private int DieRoll(int sides)
        {
            return random.Next(1, sides + 1);
        }

        #endregion

        #endregion
    }
}
