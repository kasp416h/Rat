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
using BLL;
using BLL.Repository;
using DLL;
using DLL.Modles;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BLL.Maneger maneger { get; set; }

        public MainWindow()
        {
            InitializeComponent();

          SaveCenter save = maneger.GetSave();

            RaceManager raceManager = new RaceManager(save.Rats, save.Players, save.Tracks, save.Races);
            Bookmaker bookmaker = new Bookmaker(save.Bets);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            List<Player> players = maneger.RaceManager.Players;

            foreach(Player player in players)
            {
                if (player.Name == EnterNameTextBox.Text && player.Password == EnterPasswordTextBox.Text)
                {
                    this.Close();
                    RatGameWindow ratgame = new RatGameWindow();
                    ratgame.Show();
                    break;
                }
            }

            MainWindowErrorLabel.Content = "Wrong Name or Password.";
        }

        private void CreateAcountButton_Click(object sender, RoutedEventArgs e)
        {

            List<Player> players = maneger.RaceManager.Players;

            if (default != players.FirstOrDefault(p => p.Name == EnterNameTextBox.Text))
            {
               Player newplayer = maneger.RaceManager.CreatePlayer(EnterNameTextBox.Text, EnterPasswordTextBox.Text, 200);
                maneger.RaceManager.Players.Add(newplayer);
                MainWindowErrorLabel.Content = "You have succesful created an acount";
            }
            else
            {
                MainWindowErrorLabel.Content = "The name chosen by you is alredy in use";
            }
                
        }
    }
}
