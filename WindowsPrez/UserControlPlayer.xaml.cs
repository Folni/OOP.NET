using Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WindowsPrez
{
    /// <summary>
    /// Interaction logic for UserControlPlayer.xaml
    /// </summary>
    public partial class UserControlPlayer : UserControl
    {
        public Player? Player { get; set; }
        public UserControlPlayer()
        {

        }
        public UserControlPlayer(Player player)
        {
            this.Player = player;
            InitializeComponent();
            if (player.ImagePath is not null) playerimg.Source = new BitmapImage(new Uri(player.ImagePath));
            lblNumber.Content = Player?.ShirtNumber;
        }
    }
}
