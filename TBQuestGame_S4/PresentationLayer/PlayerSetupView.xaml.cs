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
    /// Interaction logic for PlayerSetupView.xaml
    /// </summary>
    public partial class PlayerSetupView : Window
    {
        private Player _player;

        public PlayerSetupView(Player player)
        {
            _player = player;

            InitializeComponent();

            SetUpWindow();

            nameTextBox.Focus();
        }

        /// <summary>
        /// setup window
        /// </summary>
        private void SetUpWindow()
        {
            List<string> startStyle = Enum.GetNames(typeof(Player.StartStyle)).ToList();
            List<string> gender = Enum.GetNames(typeof(Player.Gender)).ToList();

            startStyleComboBox.ItemsSource = startStyle;
            genderComboBox.ItemsSource = gender;

            errorMessageTextBlock.Visibility = Visibility.Hidden;
        }

        private bool IsValidInput(out string errorMessage)
        {
            errorMessage = "";

            if (nameTextBox.Text == "")
            {
                errorMessage = "Name is required.\n";
            }
            else
            {
                _player.Name = nameTextBox.Text;
            }
            if (legionNameTextBox.Text == "")
            {
                errorMessage = "A legion name is required.\n";
            }
            else
            {
                _player.LegionName = legionNameTextBox.Text;
            }

            return errorMessage == "" ? true : false;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;

            if (IsValidInput(out errorMessage))
            {
                Enum.TryParse(genderComboBox.SelectionBoxItem.ToString(), out Player.Gender gender);
                Enum.TryParse(startStyleComboBox.SelectionBoxItem.ToString(), out Player.StartStyle startStyle);

                _player.PlayerGender = gender;
                _player.PlayerStartStyle = startStyle;

                if (_player.PlayerStartStyle == Player.StartStyle.neutral)
                {
                    _player.Power = 500;
                    _player.Rank = 1;
                }

                if (_player.PlayerStartStyle == Player.StartStyle.offensive)
                {
                    _player.Power = 600;
                    _player.Rank = 1;
                }

                if (_player.PlayerStartStyle == Player.StartStyle.deffensive)
                {
                    _player.Power = 400;
                    _player.Rank = 1;
                }

                Visibility = Visibility.Hidden;
            }
            else
            {
                errorMessageTextBlock.Visibility = Visibility.Visible;
                errorMessageTextBlock.Text = errorMessage;
            }
        }
    }
}
