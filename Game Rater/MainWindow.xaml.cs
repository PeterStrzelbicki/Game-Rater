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
//using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.IO;



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
        private const int STEAM = 1;
        private const int PLAYSTATION = 2;
        private const int XBOX = 3;
        private const int SWITCH = 4;

        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();
        public Game editItem;
        private bool sortByName;

        public MainWindow()
        {
            InitializeComponent();
            var imagePath = Path.Combine(Environment.CurrentDirectory, "images", "steam.png");
            steamImg.Source = new BitmapImage(new Uri(imagePath));

            imagePath = Path.Combine(Environment.CurrentDirectory, "images", "playstation.png");
            psImg.Source = new BitmapImage(new Uri(imagePath));

            imagePath = Path.Combine(Environment.CurrentDirectory, "images", "xbox.png");
            xboxImg.Source = new BitmapImage(new Uri(imagePath));

            imagePath = Path.Combine(Environment.CurrentDirectory, "images", "switch.png");
            switchImg.Source = new BitmapImage(new Uri(imagePath));

            sortByName = true;
            gameList.ItemsSource = Games;
            listSize.Content = Games.Count.ToString();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (gameList.SelectedItem != null)
            {
                if ((editItem = gameList.SelectedItem as Game) != null)
                {
                    EditWindow editWindow = new EditWindow(editItem, this);
                    editWindow.ShowDialog();
                }
            }

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var removed = gameList.SelectedItem as Game;

            if (removed != null)
            {
                Games.Remove(removed);
                listSize.Content = Games.Count.ToString();
                gameList.ItemsSource = Games;
            }
        }

        public void LibraryExtracted()
        {
            SortList();
            gameList.ItemsSource = Games;
            listSize.Content = Games.Count.ToString();
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

        private void steamBtn_Click(object sender, RoutedEventArgs e)
        {
            ExtractLibraryWindow libraryWindow = new ExtractLibraryWindow(STEAM, this);
            libraryWindow.ShowDialog();
        }

        private void psBtn_Click(object sender, RoutedEventArgs e)
        {
            ExtractLibraryWindow libraryWindow = new ExtractLibraryWindow(PLAYSTATION, this);
            libraryWindow.ShowDialog();
        }

        private void xboxBtn_Click(object sender, RoutedEventArgs e)
        {
            ExtractLibraryWindow libraryWindow = new ExtractLibraryWindow(XBOX, this);
            libraryWindow.ShowDialog();
        }

        private void switchBtn_Click(object sender, RoutedEventArgs e)
        {
            ExtractLibraryWindow libraryWindow = new ExtractLibraryWindow(SWITCH, this);
            libraryWindow.ShowDialog();
        }
    }
}
