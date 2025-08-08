using Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfa_FilipFolnegović
{
    public partial class MainForm : Form
    {
        // Data collections
        private List<Team> allTeams = new List<Team>();
        private ISet<string> favoritePlayerNames = new HashSet<string>();
        private ISet<Player> currentTeamPlayers = new HashSet<Player>();
        private List<Match> currentTeamMatches = new List<Match>();
        private bool isMenWorldCup = true;

        // Colors for modern design
        private static readonly Color SoftPurple = Color.FromArgb(200, 162, 200);
        private static readonly Color CardColor = Color.FromArgb(255, 255, 255);
        private static readonly Color BackgroundColor = Color.FromArgb(240, 240, 245);

        public MainForm()
        {
            InitializeComponent();
            this.BackColor = BackgroundColor;
            this.Text = "World Cup Player Viewer";

            // Assumes you have a ComboBox named 'cbTeams' in the designer
            this.cbTeams.SelectedIndexChanged += new EventHandler(cbTeams_SelectedIndexChanged);
            this.Load += new EventHandler(MainForm_Load);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await ReloadAllDataAsync();
        }

        /// <summary>
        /// Central method to clear all data and reload everything.
        /// </summary>
        private async Task ReloadAllDataAsync()
        {
            try
            {
                ClearAllData();
                ShowLoadingState(true);

                await LoadSettings();
                LoadFavoritePlayersFromFile();

                allTeams = await Information.GetTeams(isMenWorldCup);

                cbTeams.DataSource = allTeams.Where(t => !string.IsNullOrEmpty(t.FifaCode)).OrderBy(t => t.Country).ToList();
                cbTeams.DisplayMember = "Country";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A critical error occurred while loading application data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoadingState(false);
            }
        }

        /// <summary>
        /// Handles the user selecting a new team from the ComboBox.
        /// </summary>
        private async void cbTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTeams.SelectedItem == null) return;

            try
            {
                ShowLoadingState(true);
                var selectedTeam = cbTeams.SelectedItem as Team;

                currentTeamPlayers.Clear();
                currentTeamMatches.Clear();

                var teamData = await Information.GetTeamDataAsync(selectedTeam, isMenWorldCup);
                currentTeamMatches = teamData.TeamMatches;
                currentTeamPlayers = teamData.TeamPlayers;

                ApplyFavoriteStatus();
                PopulatePlayerDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data for {((Team)cbTeams.SelectedItem).Country}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoadingState(false);
            }
        }

        private void PopulatePlayerDisplay()
        {
            flowLayoutPanelPlayers.Controls.Clear();

            if (!currentTeamPlayers.Any())
            {
                Label lblNoData = new Label()
                {
                    Text = "No players to display for the selected team.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(20)
                };
                flowLayoutPanelPlayers.Controls.Add(lblNoData);
                return;
            }

            var sortedPlayers = currentTeamPlayers.OrderByDescending(p => p.Favorite).ThenBy(p => p.Name);
            foreach (Player player in sortedPlayers)
            {
                Panel playerCard = CreatePlayerCard(player);
                flowLayoutPanelPlayers.Controls.Add(playerCard);
            }
        }

        private Panel CreatePlayerCard(Player player)
        {
            Panel card = new Panel
            {
                Width = 220,
                Height = 130,
                Margin = new Padding(10),
                BackColor = CardColor,
                BorderStyle = BorderStyle.FixedSingle,
            };

            PictureBox playerPic = new PictureBox
            {
                Width = 80,
                Height = 80,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(15, 15),
                BorderStyle = BorderStyle.FixedSingle,
            };

            // This logic for getting a player image would need to be implemented
            // if (!string.IsNullOrEmpty(player.ImagePath) && File.Exists(player.ImagePath))
            //     playerPic.Image = Image.FromFile(player.ImagePath);
            // else
            //     playerPic.Image = Properties.Resources.DefaultPlayerImage;

            Label lblName = new Label
            {
                Text = player.Name,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = false,
                Width = 100,
                Height = 45,
                TextAlign = ContentAlignment.TopLeft,
                Location = new Point(110, 15),
                ForeColor = SoftPurple
            };

            Label lblPosition = new Label
            {
                Text = $"#{player.ShirtNumber} | {player.Position}",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(110, 60)
            };

            Label lblStatus = new Label
            {
                Text = (player.Captain ?? false ? "CAPTAIN " : "") + (player.Favorite ? "⭐" : ""),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                TextAlign = ContentAlignment.BottomLeft,
                Location = new Point(110, 90),
                ForeColor = player.Favorite ? Color.Gold : Color.Gray,
            };

            card.Controls.Add(playerPic);
            card.Controls.Add(lblName);
            card.Controls.Add(lblPosition);
            card.Controls.Add(lblStatus);
            return card;
        }

        // === Helper Methods for State, Files, and UI ===

        private void ClearAllData()
        {
            cbTeams.DataSource = null;
            allTeams.Clear();
            currentTeamPlayers.Clear();
            currentTeamMatches.Clear();
            favoritePlayerNames.Clear();
            flowLayoutPanelPlayers.Controls.Clear();
        }

        private void ShowLoadingState(bool isLoading)
        {
            this.Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            flowLayoutPanelPlayers.Visible = !isLoading;
        }

        private async Task LoadSettings()
        {
            try
            {
                string settingsFilePath = Path.Combine(Information.relativePath, "settings.txt");
                if (File.Exists(settingsFilePath))
                {
                    string[] settings = await File.ReadAllLinesAsync(settingsFilePath);
                    if (settings.Length > 0 && bool.TryParse(settings[0], out bool result))
                    {
                        isMenWorldCup = result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
            }
        }

        private void LoadFavoritePlayersFromFile()
        {
            try
            {
                string favoritesFilePath = Path.Combine(Information.relativePath, "favorites.txt");
                if (File.Exists(favoritesFilePath))
                {
                    var names = File.ReadAllLines(favoritesFilePath);
                    favoritePlayerNames = new HashSet<string>(names);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not load favorites file: {ex.Message}");
            }
        }

        private void ApplyFavoriteStatus()
        {
            foreach (var player in currentTeamPlayers)
            {
                if (favoritePlayerNames.Contains(player.Name))
                {
                    player.Favorite = true;
                }
            }
        }

        // === Button Event Handlers ===

        private async void btnSettings_Click(object sender, EventArgs e)
        {
            // Assuming you have a SettingsForm
            // var settingsForm = new SettingsForm(isMenWorldCup);
            // if (settingsForm.ShowDialog() == DialogResult.OK)
            // {
            //     if (isMenWorldCup != settingsForm.IsMenWorldCup)
            //     {
            //         isMenWorldCup = settingsForm.IsMenWorldCup;
            //         await ReloadAllDataAsync();
            //     }
            // }
        }

        private void btnPlayerSettings_Click(object sender, EventArgs e)
        {
            // Assuming you have a PlayerSettingsForm
            // var playerSettingsForm = new PlayerSettingsForm(currentTeamPlayers);
            // if (playerSettingsForm.ShowDialog() == DialogResult.OK)
            // {
            //    // Refresh favorites and update display
            //    LoadFavoritePlayersFromFile();
            //    ApplyFavoriteStatus();
            //    PopulatePlayerDisplay();
            // }
        }
    }
}