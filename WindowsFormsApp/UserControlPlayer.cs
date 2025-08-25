using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class UserControlPlayer : UserControl
    {
        public long? ShirtNumber { get; }
        public string Surname { get; }
        public string PlayerName { get; }
        public Player Player { get; set; }
        public Control pnlBackground { get; }
        public Panel pnlPlayerImage { get; set; }
        public UserControlPlayer(Player player)
        {
            InitializeComponent();
            this.Player = player;
            string[] values = Player.Name.Split(" ");
            PlayerName = values[0];
            Surname = values[1];
            ShirtNumber = Player.ShirtNumber;
            pnlBackground = pnlContainer;
            if (!string.IsNullOrEmpty(Player.ImagePath))
            {
                pnlImage.BackgroundImage = Image.FromFile(Player.ImagePath);
            }
            pnlPlayerImage = pnlImage;
            AssignValues();
        }


        private void AssignValues()
        {
            lbName.Parent = pnlContainer;
            lbName.Text = PlayerName;
            lbSurname.Parent = pnlContainer;
            lbSurname.Text = Surname;
            lbShirtNumber.Parent = pnlContainer;
            lbShirtNumber.Text = ShirtNumber.ToString();
        }
        public override string ToString() => $"{PlayerName} {Surname} {ShirtNumber}";
    }
}
