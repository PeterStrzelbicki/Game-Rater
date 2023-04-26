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
using System.ComponentModel;

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
        public Game editItem;
        private bool sortByName;

        public MainWindow()
        {
            InitializeComponent();
            searchBox.Foreground = Brushes.Gray;

            sortByName = true;
            Games.Add(new Game {Name = "Game 1", Score = 10});
            Games.Add(new Game { Name = "Game 2", Score = 5});
            Games.Add(new Game { Name = "Persona 5", Score = 5 });
            Games.Add(new Game { Name = "Call of Duty 4", Score = 8 });
            Games.Add(new Game { Name = "Minecraft", Score = 10 });
            Games.Add(new Game { Name = "Stardew Valley", Score = 6 });
            SortList();
            gameList.ItemsSource = Games;
            listSize.Content = Games.Count.ToString();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            editItem = gameList.SelectedItem as Game;
            EditWindow editWindow = new EditWindow(editItem, this);
            editWindow.ShowDialog();

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var removed = gameList.SelectedItem as Game;
            Games.Remove(removed);
            listSize.Content = Games.Count.ToString();
            gameList.ItemsSource = Games;
        }

        public void GameUpdated(Game game)
        {
            int index = Games.IndexOf(editItem);
            if (index == -1)
            {
                Games.Add(game);
                listSize.Content = Games.Count.ToString();
                SortList();
            }
            else
            {
                Games.RemoveAt(index);
                Games.Insert(index, game); 
                SortList();
            }
            gameList.ItemsSource = Games;
        }

        private void SortList()
        {
            if(sortByName)
            {
                Games = new ObservableCollection<Game>(Games.OrderBy(x => x.Name));
            }
            else
            {
                Games = new ObservableCollection<Game>(Games.OrderBy(x => x.Score));
            }
        }

        private void NameHeader_Click(object sender, MouseButtonEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(gameList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            sortByName = true;
        }

        private void ScoreHeader_Click(object sender, MouseButtonEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(gameList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Score", ListSortDirection.Descending));
            sortByName = false;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchBox.Text.Trim();

            if (gameList != null)
            {
                if (searchText != "Search" && !string.IsNullOrEmpty(searchText))
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
            AddWindow addWindow = new AddWindow(this);
            addWindow.ShowDialog();
        }
    }
}
