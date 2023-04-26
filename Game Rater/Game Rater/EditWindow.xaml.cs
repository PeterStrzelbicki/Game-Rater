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
}
