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
    /// Interaction logic for InfoUserWindow.xaml
    /// </summary>
    public partial class InfoUserWindow : Window
    {

        public InfoUserWindow(Player player)
        {
            InitializeComponent();

            FillLabels(player);
        }
        private void FillLabels(Player player)
        {
            lbCaptain.Text = player.Captain == true ? "YES" : "NO";
            lbGoalsScored.Text = player.NoGoals.ToString();
            lbName.Text = player.Name.ToString();
            lbPosition.Text = player.Position.ToString();
            lbShirtNumber.Text = player.ShirtNumber.ToString();
            lbYellowCards.Text = player.NoYellowCards.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var slideIn = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = -this.Height,
                To = this.Top,
                Duration = TimeSpan.FromSeconds(0.5),
                DecelerationRatio = 0.9
            };
            this.BeginAnimation(Window.TopProperty, slideIn);
        }
    }
}
