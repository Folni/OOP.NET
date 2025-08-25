using Library;
using System;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsPrez;
using Match = Library.Match;
using Path = System.IO.Path;

namespace WindowsPrez
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<Library.Match>? SomeMatches;
        private IList<Library.Match>? AllMatches;
        private ISet<Team>? AllTeams;
        private ISet<Player>? favs;
        private bool filereading = false;
        private string relativePathSettings = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\WindowsFormsApp\bin\Debug\settings.txt"));
        private string relativePathPlayers = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\WindowsFormsApp\bin\Debug\players.txt"));


        private int? index = null;
        public Dictionary<string, Team>? teams;
        public MainWindow()
        {
            InitializeComponent();
            cbGender.SelectedIndex = 1;
            favs = new HashSet<Player>();
            ReadFiles();

        }

        private void ReadFiles()
        {

            filereading = true;
            if (File.Exists(relativePathSettings) && File.Exists(relativePathPlayers))
            {
                favs = Info.GetPlayersFromFile(relativePathPlayers);
                string line = File.ReadAllText(relativePathSettings);
                string[] strings = line.Split(Player.Delimiter);
                if (strings[1] == "Mens")
                {
                    cbGender.SelectedIndex = 0;
                }
                else
                {
                    cbGender.SelectedIndex = 1;
                }

                index = int.Parse(strings[2]);

                Button_Click_1(this, null);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            tcPages.SelectedIndex = 2;
            string FavCode = Favorite.FifaCode;
            string OppCode = Opponent.FifaCode;
            Match? match = AllMatches.FirstOrDefault(t => ((t.AwayTeam.Code == FavCode && t.HomeTeam.Code == OppCode) || (t.HomeTeam.Code == FavCode && t.AwayTeam.Code == OppCode)));

            DateTimeOffset? datetime = match.Datetime;
            DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(datetime.ToString());
            MatchDate.Text = dateTimeOffset.Date.Day.ToString() + "/" + dateTimeOffset.Date.Month + "/" + dateTimeOffset.Date.Year;
            MatchTime.Text = dateTimeOffset.Hour.ToString() + ":00";
            HTCountry.Text = match.HomeTeam.Country;
            HTFormation.Text = match.HomeTeamStatistics.Tactics.ToString();
            ATCountry.Text = match.AwayTeam.Country;
            ATFormation.Text = match.AwayTeamStatistics.Tactics.ToString();

            List<Player> images = new List<Player>();

            foreach (Player player1 in favs)
            {
                if (player1.ImagePath is not null)
                {
                    images.Add(player1);
                }
            }

            Dictionary<string, Player> allplayers = match.HomeTeamStatistics.StartingEleven.Concat(match.AwayTeamStatistics.StartingEleven).ToDictionary(p => p.Name);


            foreach (Player player2 in images)
            {
                if (allplayers.TryGetValue(player2.Name, out var player))
                {
                    player.ImagePath = player2.ImagePath;
                }
            }

            CalculatePlayers(match);



            foreach (Player player in match.AwayTeamStatistics.StartingEleven)
            {
                UserControlPlayer userControlPlayer = new UserControlPlayer(player)
                {
                    Height = 75,
                    Width = 50,
                    Margin = new Thickness(5),
                };
                userControlPlayer.MouseDoubleClick += UserControlPlayer_MouseDoubleClick;

                TextBlock buh = new TextBlock()
                {
                    TextAlignment = TextAlignment.Center,
                    Height = 15,
                    Text = player.ShirtNumber.ToString() + ". " + player.Name,
                    Margin = new Thickness(5, 5, 5, 5),
                    FontSize = 13,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                LineupAT.Children.Add(buh);
                switch (player.Position)
                {
                    case Positions.Defender:
                        ATDefender.Children.Add(userControlPlayer);
                        break;
                    case Positions.Forward:
                        ATForward.Children.Add(userControlPlayer);
                        break;
                    case Positions.Goalie:
                        ATGoalie.Children.Add(userControlPlayer);
                        break;
                    case Positions.Midfield:
                        ATMidfield.Children.Add(userControlPlayer);
                        break;
                    case Positions.Null:
                        break;
                }
            }

            foreach (Player player in match.AwayTeamStatistics.Substitutes)
            {
                TextBlock pla = new TextBlock()
                {
                    TextAlignment = TextAlignment.Center,

                    Height = 15,
                    Text = player.ShirtNumber.ToString() + ". " + player.Name,
                    Margin = new Thickness(5),
                    FontSize = 13,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                BenchAT.Children.Add(pla);
            }



            foreach (Player player in match.HomeTeamStatistics.StartingEleven)
            {
                UserControlPlayer userControlPlayer = new UserControlPlayer(player)
                {
                    Height = 75,
                    Width = 50,
                    Margin = new Thickness(5),
                };
                userControlPlayer.MouseDoubleClick += UserControlPlayer_MouseDoubleClick;

                TextBlock buh = new TextBlock()
                {
                    TextAlignment = TextAlignment.Center,

                    Height = 15,
                    Text = player.ShirtNumber.ToString() + ". " + player.Name,
                    Margin = new Thickness(5, 5, 5, 5),
                    FontSize = 13,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                LineupHT.Children.Add(buh);
                switch (player.Position)
                {
                    case Positions.Defender:
                        HTDefending.Children.Add(userControlPlayer);
                        break;
                    case Positions.Forward:
                        HTForward.Children.Add(userControlPlayer);
                        break;
                    case Positions.Goalie:
                        HTGoalie.Children.Add(userControlPlayer);
                        break;
                    case Positions.Midfield:
                        HTMidfield.Children.Add(userControlPlayer);
                        break;
                    case Positions.Null:
                        break;
                }
            }

            foreach (Player player in match.HomeTeamStatistics.Substitutes)
            {
                TextBlock pla = new TextBlock()
                {
                    TextAlignment = TextAlignment.Center,
                    Height = 15,
                    Text = player.ShirtNumber.ToString() + ". " + player.Name,
                    Margin = new Thickness(5),
                    FontSize = 13,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                BenchHT.Children.Add(pla);
            }
        }

        private void CalculatePlayers(Match match)
        {
            Dictionary<string, Player> playerById = match.AwayTeamStatistics.StartingEleven.ToDictionary(p => p.Name);
            foreach (Event ent in match.AwayTeamEvents)
            {
                if (ent.TypeOfEvent == TypeOfEvent.Goal && playerById.TryGetValue(ent.Player, out var player))
                {
                    player.NoGoals++;
                }
                if (ent.TypeOfEvent == TypeOfEvent.Goal && playerById.TryGetValue(ent.Player, out var buh))
                {
                    buh.NoGoals++;
                }

            }
            playerById.Clear();
            playerById = match.HomeTeamStatistics.StartingEleven.ToDictionary(p => p.Name);
            foreach (Event ent in match.HomeTeamEvents)
            {
                if (ent.TypeOfEvent == TypeOfEvent.Goal && playerById.TryGetValue(ent.Player, out var player))
                {
                    player.NoGoals++;
                }
                else if (ent.TypeOfEvent == TypeOfEvent.YellowCard && playerById.TryGetValue(ent.Player, out var wah))
                {
                    wah.NoYellowCards++;
                }

            }
        }

        private void UserControlPlayer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserControlPlayer? clickedControl = sender as UserControlPlayer;

            InfoUserWindow window = new InfoUserWindow(clickedControl.Player);
            window.Show();


        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string url = "";
            AllTeams = new HashSet<Team>(); 
            if (cbGender.SelectedIndex == 0)
            {
                url = "https://worldcup-vua.nullbit.hr/men/teams";
            }
            else
            {
                url = "https://worldcup-vua.nullbit.hr/women/teams";
            }
            List<Team> tames = await Info.GetTeams(url);
            foreach (Team item in tames)
            {
                AllTeams.Add(item);
            }
            cbTeams.ItemsSource = AllTeams;
            await CalculateTeams();
            tcPages.SelectedIndex = 1;
            cbTeams.SelectedIndex = 0;
            if (index is not null && filereading == true)
            {
                cbTeams.SelectedIndex = (int)index;
                Button_Click_2(this, null);
            }
        }

        private async
        Task
CalculateTeams()
        {
            teams = AllTeams?.ToDictionary(t => t.FifaCode);
            AllMatches = await Info.GetMatches($"https://worldcup-vua.nullbit.hr/{(cbGender.SelectedIndex == 0 ? "men" : "women")}/matches");
            foreach (Library.Match match in AllMatches)
            {
                if (!teams.TryGetValue(match.AwayTeam.Code, out Team? team1) || !teams.TryGetValue(match.HomeTeam.Code, out Team? team2))
                    continue;
                if (team1.Opponents is null)
                {
                    team1.Opponents = new HashSet<string>();

                }
                if (team2.Opponents is null)
                {
                    team2.Opponents = new HashSet<string>();

                }

                team1.GoalsScored += (long)match.AwayTeam.Goals;
                team2.GoalsTaken += (long)match.AwayTeam.Goals;

                team1.GoalsTaken += (long)match.HomeTeam.Goals;
                team2.GoalsScored += (long)match.HomeTeam.Goals;

                team1.Opponents.Add(team2.Country + "(" + team2.FifaCode + ")");
                team2.Opponents.Add(team1.Country + "(" + team1.FifaCode + ")");

                if (team1.FifaCode == match.WinnerCode)
                {
                    team1.Wins++;
                    team2.Losses++;
                }
                else if (team2.FifaCode == match.WinnerCode)
                {
                    team2.Wins++;
                    team1.Losses++;
                }
                else
                {
                    team1.Ties++;
                    team2.Ties++;
                }
            }
        }

        private Team? Opponent;
        private Team? Favorite;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Team something = cbTeams.SelectedItem as Team;
            teams.TryGetValue(something.FifaCode, out Team team2);
            Favorite = team2;
            cbOpponents.ItemsSource = team2.Opponents;
            btnOpponents.IsEnabled = true;
            cbOpponents.SelectedIndex = 0;
            cbTeams.IsEnabled = false;
            btnFavoriteTeamInfo.IsEnabled = true;
            btnFavoriteTeamSubmit.IsEnabled = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            cbOpponents.IsEnabled = false;
            btnSubmit.IsEnabled = true;
            btnOpponentInfo.IsEnabled = true;
            string opponent = cbOpponents.SelectedItem.ToString();
            string[] strings = opponent.Split("(");
            teams.TryGetValue(strings[1].Substring(0, strings[1].Length - 1), out Team? team1);
            Opponent = team1;
            btnOpponents.IsEnabled = false;

            string FavCode = Favorite.FifaCode;
            string OppCode = Opponent.FifaCode;
            Match? match = AllMatches.FirstOrDefault(t => ((t.AwayTeam.Code == FavCode && t.HomeTeam.Code == OppCode) || (t.HomeTeam.Code == FavCode && t.AwayTeam.Code == OppCode)));
            if (match.HomeTeam.Code.ToString() == FavCode.ToString())
            {
                lbResult.Text = $"{match.HomeTeam.Goals} : {match.AwayTeam.Goals}";

            }
            else if (match.AwayTeam.Code.ToString() == FavCode.ToString())
            {
                lbResult.Text = $"{match.AwayTeam.Goals} : {match.HomeTeam.Goals}";
            }

            lbResult.Opacity = 1;
        }

        private void OpponentInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infowindow = new InfoWindow(Opponent);
            infowindow.Show();

        }

        private void FavoriteInfoClick(object sender, RoutedEventArgs e)
        {
            InfoWindow infowindow = new InfoWindow(Favorite);
            infowindow.Show();
        }

        private void btnSubmitClick(object sender, RoutedEventArgs e)
        {

        }

        private void cbSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = cbSize.SelectedItem as ComboBoxItem;
            if (cbSize.SelectedIndex != 2)
            {
                string[] strings = item.Content.ToString().Split("*");

                this.Width = int.Parse(strings[1]);
                this.Width = int.Parse(strings[0]);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WriteFile();
        }

        private void WriteFile()
        {
            string line = "";
            line += "English" + Player.Delimiter;
            line += cbGender.SelectedIndex == 1 ? "Womens" : "Mens";
            if (index is not null)
            {
                line += Player.Delimiter + index.ToString();
            }
            System.IO.File.WriteAllText(relativePathSettings, line);
        }

        private void tcPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}


