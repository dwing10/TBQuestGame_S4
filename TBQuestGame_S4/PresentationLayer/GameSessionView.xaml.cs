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
    /// Interaction logic for GameSessionView.xaml
    /// </summary>
    public partial class GameSessionView : Window
    {
        GameSessionViewModel _gameSessionViewModel;

        public GameSessionView(GameSessionViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;

            InitializeComponent();
        }

        private void Map_Location_Button_Click(object sender, RoutedEventArgs e)
        {
            Button windowButton = sender as Button;
            //_gameSessionViewModel.Timer();
            _gameSessionViewModel.Move(windowButton.Tag.ToString());
        }

        private void Barracks_Button_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.ShowBarracks();
        }

        private void Recruit_Button_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.DisplayRecruitment();
        }

        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.ShowInfo();
        }


        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Use_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerInventoryTabs.SelectedItem != null)
            {
                _gameSessionViewModel.OnUseGameItem();
            }
        }

        private void Drop_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerInventoryTabs.SelectedItem != null)
            {
                _gameSessionViewModel.RemoveItemFromInventory();
            }
        }

        private void Attack_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LocationNpcsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerAttack();
            }
        }

        private void Speak_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LocationNpcsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerTalkTo();
            }
        }

        private void Retreat_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LocationNpcsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerRetreat();
            }
        }
    }
}
