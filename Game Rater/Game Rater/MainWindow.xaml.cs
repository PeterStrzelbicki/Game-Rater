/*
* FILE          :   MainWindow.xaml.cs
* PROJECT       :   Game Rater
* PROGRAMMER    :   Peter Strzelbicki
* FIRST VERSION :   April 24th, 2023
* DESCRIPTION   :   This files contains the code for the main application.
*/
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace Game_Rater
{        
    public class Game
        {
            public string Name { get; set; }
            public int Score { get; set; }
        }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            searchBox.Foreground = Brushes.Gray;
            
            Games.Add(new Game {Name = "Game 1", Score = 10});
            Games.Add(new Game { Name = "Game 2", Score = 5});
            Games.Add(new Game { Name = "Persona 5", Score = 11 });
            Games.Add(new Game { Name = "Call of Duty 4", Score = 8 });
            gameList.ItemsSource = Games;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchBox.Text.Trim();

            if (gameList != null)
            {
                if (searchText != "Search" || !string.IsNullOrEmpty(searchText))
                {
                    var viewSource = CollectionViewSource.GetDefaultView(gameList.ItemsSource);
                    viewSource.Filter = g => ((Game)g).Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                else
                {
                    var viewSource = CollectionViewSource.GetDefaultView(gameList.ItemsSource);
                    viewSource.Filter = null;
                }
            }
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchBox.Text == "Search")
            {
                searchBox.Text = "";
                searchBox.Foreground = Brushes.Black;
            }
        }

        private void searchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Search";
                searchBox.Foreground = Brushes.Gray;
            }
        }

        /*
        function     : aboutMenu_Click()
        description  : This function displays the "About" window.
        */
        private void aboutMenu_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
