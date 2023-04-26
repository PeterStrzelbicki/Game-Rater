using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Game_Rater
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private MainWindow mainWindow;

        public EditWindow(Game game, MainWindow main)
        {
            InitializeComponent();

            nameBox.Text = game.Name;
            scoreBox.Text = game.Score.ToString();
            mainWindow = main;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(IsValid()) 
            {
                Game game = new Game();
                // Get the text entered in the nameBox and scoreBox
                string name = nameBox.Text;
                int score = int.Parse(scoreBox.Text);

                // Update the game object with the new values
                game.Name = name;
                game.Score = score;            
                mainWindow.GameUpdated(game);

                // Close the child window and return the updated game object to the parent window
                this.Close();
            }
        }

        private bool IsValid()
        {
            int number;

            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Name can not be empty.", "Error");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(scoreBox.Text))
            {
                MessageBox.Show("Score can not be empty.", "Error");
                return false;
            }
            else if (int.TryParse(scoreBox.Text, out number) == false)
            {
                MessageBox.Show("Score must be an integer from 1-10.", "Error");
                return false;
            }
            else if (number < 1 || number > 10)
            {
                MessageBox.Show("Score must be an integer from 1-10.", "Error");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
