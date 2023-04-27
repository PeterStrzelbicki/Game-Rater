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
using System.Net.Http;
using System.Text.Json;

namespace Game_Rater
{

    /// <summary>
    /// Interaction logic for ExtractLibraryWindow.xaml
    /// </summary>
    public partial class ExtractLibraryWindow : Window
    {        
        private ObservableCollection<Game> games;
        private readonly string apiKey;
        private MainWindow mainWindow;

        public ExtractLibraryWindow(int platform, MainWindow main)
        {
            InitializeComponent();

            games = main.Games;
            mainWindow = main;

            if (platform == 1)
            {
                infoLbl.Content = "Enter your Steam ID";
                apiKey = "3197651F48AA9357A3E0EA1912ED566F";
            }
            else if (platform == 2) 
            {
                infoLbl.Content = "Enter your PlayStation Online ID";
            }
            else if (platform == 3) 
            {
                infoLbl.Content = "Enter your Xbox Live ID";
            }
            else if (platform == 4) 
            {
                infoLbl.Content = "Enter your Nintendo ID";
            }
        }

        public async Task<ObservableCollection<Game>> ExtractSteamLibrary()
        {
            var client = new HttpClient();
            var url = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={apiKey}&steamid={IDBox.Text}&format=json&include_appinfo=1&include_played_free_games=1";
            var response = await client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseObject = JsonSerializer.Deserialize<JsonElement>(responseContent, jsonOptions);
            if (responseObject.TryGetProperty("response", out var responseElement) && responseElement.TryGetProperty("games", out var gamesList))
            {
                var games = new ObservableCollection<Game>();
                foreach (var game in gamesList.EnumerateArray())
                {
                    games.Add(new Game
                    {
                        Name = game.GetProperty("name").GetString(),
                        Score = 0
                    });
                }
                return games;
            }
            else
            {
                MessageBox.Show("Something went wrong.", "Error");
            }

            return null;
        }

        private async void importBtn_Click(object sender, RoutedEventArgs e)
        {
            games = await ExtractSteamLibrary();

            mainWindow.Games = games;
            mainWindow.LibraryExtracted();

            this.Close();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
