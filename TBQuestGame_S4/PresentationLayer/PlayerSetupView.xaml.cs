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

            missionBox.Text = "You have been tasked by your Emperor and the High Council to lay seige on enemy lands. " +
                "Along with the title of Imperator, you have been given a legion and a starting sum of gold. " +
                "Use your newfound status and wealth wisely. The Emperor will not tolerate failure. ";
        }

        /// <summary>
        /// setup window
        /// </summary>
        private void SetUpWindow()
        {
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
