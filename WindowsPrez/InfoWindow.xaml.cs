using Library;
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

namespace WindowsPrez
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow(Team team)
        {
            InitializeComponent();
            FillLabels(team);
        }

        private void FillLabels(Team team)
        {
            lbFifaCode.Text = team.FifaCode.ToString();
            lbGames.Text = (team.Wins + team.Losses + team.Ties).ToString();
            lbCountry.Text = team.Country.ToString();
            lbWinsLosses.Text = team.Wins + "/" + team.Losses+"/"+team.Ties;
            lbGoalsScored.Text = team.GoalsScored.ToString();
            lbGoalsTaken.Text = team.GoalsTaken.ToString();
            lbGoalsDifference.Text =(team.GoalsScored - team.GoalsTaken).ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
            };
            this.BeginAnimation(Window.OpacityProperty, fadeIn);
        }
    }
}
