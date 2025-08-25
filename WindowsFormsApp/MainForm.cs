using Library;
using Newtonsoft.Json;
using RestSharp;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using File = System.IO.File;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        //Globals
        private UserControlPlayer? currentPlayer = null;
        private static IList<UserControlPlayer> selectedPlayers = new List<UserControlPlayer>();
        private static ISet<Player> AllPlayers = new HashSet<Player>();
        private static IList<Match> AllMatches = new List<Match>();
        private static Team? Favorite_Team;

        private Point mouseDownLocation;
        private bool isMouseDown = false;
        private int? index = null;
        public MainForm()
        {
            InitializeComponent();
            cbLanguage.SelectedIndex = 0;
            ReadFileAsync();
            HideTabs();
            Reset.Enabled = true;
        }

        private void HideTabs()
        {
            tcbPages.Appearance = TabAppearance.FlatButtons;
            tcbPages.ItemSize = new Size(0, 1);
            tcbPages.SizeMode = TabSizeMode.Fixed;
        }

        private void ReadFileAsync()
        {

            try
            {
                string line = System.IO.File.ReadAllText(Info.relativePath + "\\settings.txt");
                string[] splits = line.Split(Player.Delimiter);
                if (splits[0] == "English")
                {
                    cbLanguage.SelectedIndex = 1;
                }
                else
                {
                    cbLanguage.SelectedIndex = 0;
                }
                if (splits[1] == "Mens")
                {
                    rbMens.Checked = true;
                    rbWomens.Checked = false;
                }
                else
                {
                    rbMens.Checked = false;
                    rbWomens.Checked = true;
                }
                index = int.Parse(splits[2]);
            }
            catch
            {
                tcbPages.SelectedIndex = 0;
                return;
            }
            GenderSubmitButton(this, new EventArgs());
        }
        private bool _hasConfirmedClose = false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (_hasConfirmedClose)
            {
                return;
            }
            e.Cancel = true;

            DialogResult result = MessageBox.Show(
                "Do you want to save?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                WriteFile();
                WritePlayersToFile();
                _hasConfirmedClose = true;
                Close();
            }
            else if (result == DialogResult.No)
            {
                _hasConfirmedClose = true;
                Close();
            }
        }

        private void WriteFile()
        {
            string line = "";
            line += cbLanguage.Text;
            line += Player.Delimiter + CheckSelectedGroup();
            if (index is not null)
            {
                line += Player.Delimiter + index.ToString();
            }
            System.IO.File.WriteAllText(Info.relativePath + "\\settings.txt", line);
        }

        private string CheckSelectedGroup()
        {
            if (rbMens.Checked)
            {
                return "Mens";
            }
            else
            {
                return "Womens";
            }
        }

        private void GenderSubmitButton(object sender, EventArgs e)
        {
            LoadDropDownTeams();
            int number = 0;
            if (rbMens.Checked)
            {
                number = 1;
            }
            if (cbLanguage.SelectedIndex == 0)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("hr");

                Controls.Clear();
                InitializeComponent();
                HideTabs();
            }
            else if (cbLanguage.SelectedIndex == 1 && Thread.CurrentThread.CurrentCulture == new CultureInfo("hr"))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

                Controls.Clear();
                InitializeComponent();
                HideTabs();
            }
            tcbPages.SelectedIndex = 1;
            Reset.Enabled = true;
            if (number == 1)
            {
                rbMens.Checked = true;
            }
            else
            {
                rbWomens.Checked = true;
            }
        }

        private async void LoadDropDownTeams()
        {
            string url = "";
            if (CheckSelectedGroup() == "Mens")
            {
                url = "https://worldcup-vua.nullbit.hr/men/teams";
            }
            else
            {
                url = "https://worldcup-vua.nullbit.hr/women/teams";

            }
            var teams = await Info.GetTeams(url);

            ddlTeams.DataSource = teams;
            if (index != null)
            {
                ddlTeams.SelectedIndex = (int)index;
                btnTeamSubmit_Click(this, new EventArgs());
            }
        }

        private void btnTeamSubmit_Click(object sender, EventArgs e)
        {
            if (File.Exists(Info.relativePath + "\\players.txt"))
            {
                try
                {
                    LoadPLayresFromFile();
                }
                catch (Exception)
                {
                    LoadPlayersFromApi();
                }

            }
            else
            {
                LoadPlayersFromApi();
            }
            tcbPages.SelectedIndex = 2;
            btnEditPicture.Enabled = false;
            index = ddlTeams.SelectedIndex;
            Favorite_Team = (Team?)ddlTeams.SelectedItem;

        }
        private void MakeUserControls()
        {
            foreach (Player player in AllPlayers)
            {
                UserControlPlayer ba = new UserControlPlayer(player);
                ApplyEvents(ba);
            }
        }

        private async void LoadPLayresFromFile()
        {
            try
            {
                AllPlayers = Info.GetPlayersFromFile(@$"{Info.relativePath}\players.txt");
            }
            catch (Exception e)
            {
            }
            finally
            {
                LoadPlayersFromApi();
                await FillMatches();
            }
        }


        private async void LoadPlayersFromApi()
        {
            if ((Team?)ddlTeams.SelectedItem == null) return;
            Team something = (Team)ddlTeams.SelectedItem;


            try
            {
                await Info.GetPlayersFromApiAsync(something, AllMatches, CheckSelectedGroup() == "Mens", AllPlayers);
            }
            catch (Exception e)
            {
                tcbPages.SelectedIndex = 1;
            }
            MakeUserControls();
        }

        private async Task FillMatches()
        {
            if ((Team?)ddlTeams.SelectedItem == null) return;
            Team something = (Team)ddlTeams.SelectedItem;
            string url = "";
            if (CheckSelectedGroup() == "Mens")
            {
                url = $"https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code={something.FifaCode}";
            }
            else
            {
                url = $"https://worldcup-vua.nullbit.hr/women/matches/country?fifa_code={something.FifaCode}";

            }
            List<Match> matches = await Info.GetMatches(url);
            AllMatches = matches;
        }

        private void ApplyEvents(UserControlPlayer player)
        {
            player.MouseDown += pnlPlayers_MouseDown;
            player.MouseMove += pnlPlayers_MouseMove;
            player.MouseUp += pnlPlayers_MouseUp;
            player.ContextMenuStrip = cmsPlayers;
            player.pnlBackground.MouseDown += pnlPlayers_MouseDown;
            player.pnlBackground.MouseMove += pnlPlayers_MouseMove;
            player.pnlBackground.MouseUp += pnlPlayers_MouseUp;
            foreach (Control item in player.pnlBackground.Controls)
            {
                item.MouseUp += pnlPlayers_MouseUp;
                item.MouseDown += pnlPlayers_MouseDown;
                item.MouseMove += pnlPlayers_MouseMove;
            }
            if (player.Player.Favorite)
            {
                pnlFavorites.Controls.Add(player);
            }
            else if (!player.Player.Favorite)
            {
                pnlPlayers.Controls.Add(player);
            }
        }

        public static Match? GetMatchByCountry(List<Match> matches, string country) => matches.FirstOrDefault(m => m.HomeTeam.Country == country);
        private void RemovePlayerFromList(UserControlPlayer player)
        {
            selectedPlayers.Remove(player);
            player.BackColor = Color.White;
            if (selectedPlayers.Count != 1)
            {
                btnEditPicture.Enabled = false;
            }
            else
            {
                btnEditPicture.Enabled = true;
            }
        }

        private void AddPlayerToList(UserControlPlayer player)
        {
            selectedPlayers.Add(player);
            player.BackColor = Color.MidnightBlue;
            if (selectedPlayers.Count != 1)
            {
                btnEditPicture.Enabled = false;
            }
            else
            {
                btnEditPicture.Enabled = true;
            }
        }

        private UserControlPlayer GetUserControl(object? sender)
        {
            Control? control = sender as Control;
            while (control != null && !(control is UserControlPlayer))
            {
                control = control.Parent;
            }
            if (control is UserControlPlayer player)
            {

                return player;
            }
            return null;
        }


        public void pnlPlayers_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseDownLocation = e.Location;
                currentPlayer = GetUserControl(sender);
                currentPlayer.BackColor = Color.MidnightBlue;
            }
        }

        private void pnlPlayers_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isMouseDown && e.Button == MouseButtons.Left)
            {
                var distance = Math.Abs(e.X - mouseDownLocation.X) + Math.Abs(e.Y - mouseDownLocation.Y);

                if (distance > 20)
                {
                    isMouseDown = false;
                    if (selectedPlayers.Count == 0 && currentPlayer is not null)
                    {
                        StartDragDrop(currentPlayer);
                    }
                    else if (currentPlayer is not null)
                    {
                        AddPlayerToList(currentPlayer);
                    }
                    foreach (UserControlPlayer player in selectedPlayers.ToList())
                    {
                        StartDragDrop(player);
                    }
                }
            }
        }

        private void pnlPlayers_MouseUp(object? sender, MouseEventArgs e)
        {

            if (isMouseDown && e.Button == MouseButtons.Left)
            {
                UserControlPlayer player = GetUserControl(sender);
                if (player is null) return;
                if (!selectedPlayers.Contains(player))
                {
                    AddPlayerToList(player);
                }
                else
                {
                    RemovePlayerFromList(player);
                }
            }
        }


        private void ClearList()
        {

            foreach (UserControlPlayer control in selectedPlayers.ToList())
            {
                selectedPlayers.Remove(control);
            }
            selectedPlayers.Clear();
            foreach (UserControlPlayer control in pnlFavorites.Controls.Cast<Control>().Concat(pnlPlayers.Controls.Cast<Control>()))
            {
                control.BackColor = Color.White;
                control.Refresh();
            }
            btnEditPicture.Enabled = false;
        }

        private void StartDragDrop(UserControlPlayer? player)
        {
            if (player is null) return;
            DoDragDrop(player, DragDropEffects.Move);
        }
        private void Player_DragDrop(object sender, DragEventArgs e)
        {
            int count = selectedPlayers.Count;
            FlowLayoutPanel? panel = sender as FlowLayoutPanel;
            UserControlPlayer? player = e?.Data?.GetData(typeof(UserControlPlayer)) as UserControlPlayer;
            if (player == null) return;
            if (panel?.Name == pnlFavorites.Name)
            {
                pnlFavorites.Controls.Add(player);
                pnlPlayers.Controls.Remove(player);
                player.ContextMenuStrip = cmsFavorites;
            }
            else if (panel?.Name == pnlPlayers.Name)
            {
                pnlFavorites.Controls.Remove(player);
                pnlPlayers.Controls.Add(player);
                player.ContextMenuStrip = cmsPlayers;
            }
            RemovePlayerFromList(player);
            ClearList();
        }

        private void Player_DragEnter(object sender, DragEventArgs e)
        {
            UserControlPlayer? player = e?.Data?.GetData(typeof(UserControlPlayer)) as UserControlPlayer;
            FlowLayoutPanel? pnl = sender as FlowLayoutPanel;
            if (player is null || pnl is null) return;
            foreach (Control item in pnl.Controls)
            {
                if (item == player)
                {
                    return;
                }
            }
            selectedPlayers.Clear();
            e.Effect = DragDropEffects.Move;
        }

        private void unFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmsFavorites.SourceControl is UserControlPlayer player)
            {
                pnlFavorites.Controls.Remove(player);
                pnlPlayers.Controls.Add(player);
                player.ContextMenuStrip = cmsPlayers;

            }
        }

        private void favoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmsPlayers.SourceControl is UserControlPlayer player)
            {
                pnlFavorites.Controls.Add(player);
                pnlPlayers.Controls.Remove(player);
                player.ContextMenuStrip = cmsFavorites;

            }
        }

        private void btnPage3_Click(object sender, EventArgs e)
        {
            ImageListShenanigans();
            ddlMatches.DataSource = AllMatches;
            tcbPages.SelectedIndex = 3;

        }

        private static ImageList imagesPlayersList = new ImageList();
        private static Dictionary<Player, int> playerImages = new Dictionary<Player, int>();
        private void ImageListShenanigans()
        {
            foreach (Player player in AllPlayers)
            {
                if (player.ImagePath is not null)
                {
                    imagesPlayersList.Images.Add(player.Name, Image.FromFile(player.ImagePath));
                    playerImages.Add(player, imagesPlayersList.Images.IndexOfKey(player.Name));
                }
            }
        }
        private void InsertPlayersIntoList()
        {
            lwPlayers.SmallImageList = imagesPlayersList;
            foreach (UserControlPlayer control in pnlFavorites.Controls.Cast<Control>().Concat(pnlPlayers.Controls.Cast<Control>()))
            {
                ListViewItem item = new ListViewItem(control.PlayerName);
                if (playerImages.TryGetValue(control.Player, out int keyindex))
                {
                    item.ImageIndex = keyindex;
                }
                item.SubItems.Add(control.Surname);
                item.SubItems.Add(control.ShirtNumber.ToString());
                item.SubItems.Add(control.Player.NoGoals.ToString());
                item.SubItems.Add(control.Player.NoYellowCards.ToString());
                lwPlayers.Items.Add(item);
            }
        }

        private void WritePlayersToFile()
        {
            if (AllPlayers is null || AllPlayers.Count == 0 || (pnlFavorites.Controls.Count == 0 && pnlPlayers.Controls.Count == 0)) return;
            string line = $"{AllPlayers.Count}" + "\n";

            foreach (UserControlPlayer player in pnlFavorites.Controls)
            {
                player.Player.Favorite = true;
                line += Player.Format(player.Player) + "\n";
            }
            foreach (UserControlPlayer player in pnlPlayers.Controls)
            {
                player.Player.Favorite = false;
                line += Player.Format(player.Player) + "\n";
            }

            System.IO.File.WriteAllText(Info.relativePath + "\\players.txt", line);

        }

        private void Calculate(Match match)
        {
            if (match.HomeTeam.Code == Favorite_Team?.FifaCode)
            {
                foreach (Event events in match.HomeTeamEvents)
                {
                    Player? player;
                    switch (events.TypeOfEvent)
                    {
                        case TypeOfEvent.Goal:
                            player = AllPlayers.FirstOrDefault(p => p.Name == events.Player);
                            player.NoGoals++;
                            break;
                        case TypeOfEvent.YellowCard:
                            player = AllPlayers.FirstOrDefault(p => p.Name == events.Player);
                            player.NoYellowCards++;
                            break;
                        case TypeOfEvent.Unknown:
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (match.AwayTeam.Code == Favorite_Team?.FifaCode)
            {
                foreach (Event events in match.AwayTeamEvents)
                {
                    Player player;
                    switch (events.TypeOfEvent)
                    {
                        case TypeOfEvent.Goal:
                            player = AllPlayers.FirstOrDefault(p => p.Name == events.Player);
                            player.NoGoals++;
                            break;
                        case TypeOfEvent.YellowCard:
                            player = AllPlayers.FirstOrDefault(p => p.Name == events.Player);
                            player.NoYellowCards++;
                            break;
                        case TypeOfEvent.Unknown:
                            break;
                        default:
                            break;
                    }
                }

            }
            InsertPlayersIntoList();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            ClearList();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {

            Button? button = sender as Button;
            if (button == null) return;
            if (button.Tag is null) return;
            var taggers = button.Tag.ToString();
            if (taggers is null) return;
            if (int.Parse(taggers) == 2)
            {
                foreach (UserControlPlayer control in pnlFavorites.Controls)
                {
                    AddPlayerToList(control);
                }
            }
            else
            {
                foreach (UserControlPlayer control in pnlPlayers.Controls)
                {
                    AddPlayerToList(control);
                }
            }

        }

        private void btnEditPicture_Click(object sender, EventArgs e)
        {

            OpenFileDialog filedialog = new OpenFileDialog()
            {
                Filter = "Slike|*.jpg;*.png|Sve datoteke|*.*"
            };

            DialogResult dialogResult = filedialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                Image img = Image.FromFile(filedialog.FileName);
                currentPlayer.pnlPlayerImage.BackgroundImage = img;
                currentPlayer.Player.ImagePath = filedialog.FileName;
            }

        }

        private void toolStripMenuIReset_Click(object sender, EventArgs e)
        {
            index = null;
            ddlMatches.DataSource = null;
            ddlTeams.DataSource = null;
            tcbPages.SelectedIndex = 0;
            lwPlayers.Items.Clear();
            pnlPlayers.Controls.Clear();
            pnlFavorites.Controls.Clear();
            ddlMatches.Items.Clear();
            AllPlayers.Clear();
            ddlTeams.Items.Clear();
            selectedPlayers.Clear();
            File.Delete(Info.relativePath + "\\settings.txt");
            File.Delete(Info.relativePath + "\\players.txt");


        }

        private void cbMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            lwPlayers.Items.Clear();
            foreach (Player player in AllPlayers)
            {
                player.NoGoals = 0;
                player.NoYellowCards = 0;
            }

            Calculate((Match)ddlMatches.SelectedItem);
            ChangeStats((Match)ddlMatches.SelectedItem);

        }

        private void ChangeStats(Match selectedItem)
        {
            lbACode.Text = selectedItem.AwayTeam.Code;
            lbACountry.Text = selectedItem.AwayTeam.Country;
            lbAGoals.Text = selectedItem.AwayTeam.Goals.ToString();
            lbAPenalties.Text = selectedItem.AwayTeam.Penalties.ToString();
            lbHCode.Text = selectedItem.HomeTeam.Code;
            lbHCountry.Text = selectedItem.HomeTeam.Country;
            lbHGoals.Text = selectedItem.HomeTeam.Goals.ToString();
            lbHPenalties.Text = selectedItem.HomeTeam.Penalties.ToString();

            lbAttendance.Text = selectedItem.Attendance.ToString();
            lbLocation.Text = selectedItem.Location;
            lbVenue.Text = selectedItem.Venue;
            lbHumidity.Text = selectedItem.Weather.Description;
            lbTemperature.Text = selectedItem.Weather.TempCelsius.ToString();
            lbWindSpeed.Text = selectedItem.Weather.WindSpeed.ToString();
            lbDescription.Text = selectedItem.Weather.Description;

            lbWinner.Text = selectedItem.Winner;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog.ShowDialog();
        }

        private int cow = 0;
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ddlMatches.SelectedIndex = cow;
            if (cow >= ddlMatches.Items.Count - 1)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
                cow++;
            }
            Bitmap bitmap = new Bitmap(this.Width, this.Height);
            Rectangle rectangle = new Rectangle
            {
                Width = bitmap.Width,
                Height = bitmap.Height,
                X = 0,
                Y = 0
            };

            this.DrawToBitmap(bitmap, rectangle);

            e.Graphics?.DrawImage(bitmap, e.MarginBounds.X / 4, e.MarginBounds.Y);
        }

        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MessageBox.Show("Printing Finished");
        }
    }
}
