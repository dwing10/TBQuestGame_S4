using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TBQuestGame_S1.Models;

namespace TBQuestGame_S1.PresentationLayer
{
    /// <summary>
    /// Interaction logic for RecruitWindow.xaml
    /// </summary>
    public partial class RecruitWindow : Window
    {
        Player _player;
        public RecruitWindow(Player player)
        {
            _player = player;
            InitializeComponent();
        }

        /// <summary>
        /// fill text box with item descriptions
        /// </summary>
        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            string itemSelection = itemsBox.SelectionBoxItem as string;

            if (itemSelection == "50 Legionnaire -- 100 gold")
            {
                descriptionBox.Text = "Legionnaire are the backbone of any legion. They are cheap to train and " +
                    "loyal until the end. 50 Legionnaire will increase your power by 50.";
            }
            if (itemSelection == "50 Archers -- 150 gold")
            {
                descriptionBox.Text = "Archers are a vital resource for your legion. They allow an Imperator to " +
                    "decimate his/her enemies from afar. 50 archers will increase your power by 100.";
            }
            if (itemSelection == "20 Cavalry -- 300 gold")
            {
                descriptionBox.Text = "Cavalry are horse mounted soldiers equipped with javelins. 20 cavalry will " +
                    "increase your power by 250.";
            }
            if (itemSelection == "5 Centurion -- 500 gold")
            {
                descriptionBox.Text = "Centurions are highly experienced battlefield commanders. Adding 5 centurion" +
                    "to your ranks will increase your power by 350.";
            }
            if (itemSelection == "1 Praetor -- 1000 gold")
            {
                descriptionBox.Text = "A Praetor is your second in command. Acquiring a Praetor will increase your " +
                    "legion's power by 500.";
            }
            if (itemSelection == "2 Ballistae -- 1500 gold")
            {
                descriptionBox.Text = "Ballistae are seige weapons that fire large bolts. They cause severe damae " +
                    "to enemy forts. Building 2 Ballistae will increase your power by 700.";
            }
            if (itemSelection == "1 Catapult -- 2500 gold")
            {
                descriptionBox.Text = "Catapults are capable of hurling large bolders through the air, demolishing " +
                    "enemy forts. Building a single catapult will increase your legion's power by 1000";
            }
            if (itemSelection == "Small Naval Fleet -- 3000 gold")
            {
                descriptionBox.Text = "If you want to cross the Narrow Sea and defeat our enemies to the south, then " +
                    "you will need to build a naval fleet.";
            }
        }

        /// <summary>
        /// processes event for purchase button
        /// </summary>
        private void Purchase_Button_Click(object sender, RoutedEventArgs e)
        {
            string itemSelection = itemsBox.SelectionBoxItem as string;

            //legionnaire
            if (itemSelection == "50 Legionnaire -- 100 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 100)
                {
                    gameItemQuantity.Quantity -= 100;
                    _player.LegionnaireNumbers += 50;
                    _player.Power += 50;

                    descriptionBox.Text = "50 Legionnaire shall be trained at once Imperator.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
            //archer
            if (itemSelection == "50 Archers -- 150 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 150)
                {
                    gameItemQuantity.Quantity -= 150;
                    _player.ArcherNumbers += 50;
                    _player.Power += 100;

                    descriptionBox.Text = "50 Archers shall be trained at once Imperator.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
            //cavalry
            if (itemSelection == "20 Cavalry -- 300 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 300)
                {
                    gameItemQuantity.Quantity -= 300;
                    _player.CavalryNumbers += 20;
                    _player.Power += 250;

                    descriptionBox.Text = "20 Cavalry shall be trained at once Imperator.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
            //Centurion
            if (itemSelection == "5 Centurion -- 500 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 500)
                {
                    gameItemQuantity.Quantity -= 500;
                    _player.CenturionNumbers += 5;
                    _player.Power += 350;

                    descriptionBox.Text = "5 Centurion shall be trained at once Imperator.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
            //Praetor
            if (itemSelection == "1 Praetor -- 1000 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 1000)
                {
                    gameItemQuantity.Quantity -= 1000;
                    _player.PraetorNumbers += 1;
                    _player.Power += 500;

                    descriptionBox.Text = "1 Praetor shall be trained at once Imperator.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
            //Ballistae
            if (itemSelection == "2 Ballistae -- 1500 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 1500)
                {
                    gameItemQuantity.Quantity -= 1500;
                    _player.NumOfSeigeWeapons += 2;
                    _player.Power += 700;

                    descriptionBox.Text = "2 Ballistae shall be built at once Imperator.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
            //Catapult
            if (itemSelection == "1 Catapult -- 2500 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 2500)
                {
                    gameItemQuantity.Quantity -= 2500;
                    _player.NumOfSeigeWeapons += 1;
                    _player.Power += 1000;

                    descriptionBox.Text = "1 Catapult shall be built at once Imperator.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
            //Small naval fleet
            if (itemSelection == "Small Naval Fleet -- 3000 gold")
            {
                GameItemQuantity gameItemQuantity = _player.Inventory.FirstOrDefault(i => i.GameItem.Id == "GLD");
                if (gameItemQuantity != null && gameItemQuantity.Quantity >= 3000)
                {
                    gameItemQuantity.Quantity -= 3000;
                    _player.PlayerHasShips = true;

                    descriptionBox.Text = "Very good Imperator, We shall build you the finest fleet in the empire.";
                }
                else
                {
                    descriptionBox.Text = "My apologies Imperator, but it seems you lack the funds for that.";
                }
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
