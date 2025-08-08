using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace wfa_FilipFolnegović
{
    public partial class PlayerSettingsForm : Form
    {
        private ISet<Player> players;
        private Player selectedPlayer;

        // Use the same color palette for a consistent look
        private static readonly Color SoftPurple = Color.FromArgb(200, 162, 200);
        private static readonly Color SoftYellow = Color.FromArgb(255, 255, 204);
        private static readonly Color BackgroundColor = Color.FromArgb(240, 240, 245);

        // Declare controls as private fields
        private ListBox lstPlayers;
        private PictureBox pbPlayerImage;
        private CheckBox chkFavorite;
        private Button btnSelectImage;
        private Button btnSave;
        private Button btnCancel;

        public PlayerSettingsForm(ISet<Player> players)
        {
            InitializeComponent();
            this.players = players;
            this.BackColor = BackgroundColor;
        }

        private void PlayerSettingsForm_Load(object sender, EventArgs e)
        {
            // Populate a listbox with player names
            lstPlayers.DataSource = players.OrderBy(p => p.Name).ToList();
            lstPlayers.DisplayMember = "Name";

            // Set styles for a modern feel
            btnSave.BackColor = SoftPurple;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;

            btnCancel.BackColor = SoftYellow;
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;

            btnSelectImage.BackColor = SoftYellow;
            btnSelectImage.ForeColor = Color.Black;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.FlatAppearance.BorderSize = 0;

            // Initially disable edit controls
            DisableEditControls();
        }

        private void lstPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlayers.SelectedItem is Player player)
            {
                selectedPlayer = player;
                EnableEditControls();
                LoadPlayerDetails(player);
            }
            else
            {
                DisableEditControls();
            }
        }

        private void LoadPlayerDetails(Player player)
        {
            pbPlayerImage.BorderStyle = player.Favorite ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;

            if (player.ImagePath != null && File.Exists(player.ImagePath))
            {
                pbPlayerImage.Image = Image.FromFile(player.ImagePath);
            }
            else
            {
                pbPlayerImage.Image = Properties.Resources.DefaultPlayerImage;
            }

            chkFavorite.Checked = player.Favorite;
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string destinationPath = Path.Combine(Information.relativePath, "Images", Path.GetFileName(ofd.FileName));
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                    File.Copy(ofd.FileName, destinationPath, true);

                    selectedPlayer.ImagePath = destinationPath;
                    pbPlayerImage.Image = Image.FromFile(destinationPath);
                }
            }
        }

        private void chkFavorite_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.Favorite = chkFavorite.Checked;
                LoadPlayerDetails(selectedPlayer);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Path.Combine(Information.relativePath, "players.txt");
                var lines = new List<string>();
                lines.Add(players.Count.ToString());
                lines.AddRange(players.Select(p => Player.Format(p)));
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("Player settings saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving players: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DisableEditControls()
        {
            btnSelectImage.Enabled = false;
            chkFavorite.Enabled = false;
        }

        private void EnableEditControls()
        {
            btnSelectImage.Enabled = true;
            chkFavorite.Enabled = true;
        }

        private void InitializeComponent()
        {
            // Dynamically create all controls and their layout
            this.lstPlayers = new ListBox();
            this.pbPlayerImage = new PictureBox();
            this.chkFavorite = new CheckBox();
            this.btnSelectImage = new Button();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            // Layout with a TableLayoutPanel for better organization
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(10)
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            // Controls for the left panel (Player List)
            this.lstPlayers.Dock = DockStyle.Fill;
            mainLayout.Controls.Add(this.lstPlayers, 0, 0);

            // Controls for the right panel (Player Details)
            TableLayoutPanel detailsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            this.pbPlayerImage.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbPlayerImage.BorderStyle = BorderStyle.FixedSingle;
            this.pbPlayerImage.Dock = DockStyle.Fill;
            detailsLayout.Controls.Add(this.pbPlayerImage, 0, 0);

            this.chkFavorite.Text = "Favorite Player";
            this.chkFavorite.Dock = DockStyle.Top;
            this.chkFavorite.Anchor = AnchorStyles.None;
            detailsLayout.Controls.Add(this.chkFavorite, 0, 1);

            this.btnSelectImage.Text = "Select Image";
            this.btnSelectImage.Dock = DockStyle.Top;
            detailsLayout.Controls.Add(this.btnSelectImage, 0, 2);

            mainLayout.Controls.Add(detailsLayout, 1, 0);

            // Controls for the bottom panel (Buttons)
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(5)
            };

            this.btnSave.Text = "Save";
            this.btnSave.AutoSize = true;
            this.btnSave.Margin = new Padding(5);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.AutoSize = true;
            this.btnCancel.Margin = new Padding(5);

            buttonPanel.Controls.Add(this.btnCancel);
            buttonPanel.Controls.Add(this.btnSave);
            mainLayout.SetColumnSpan(buttonPanel, 2);
            mainLayout.Controls.Add(buttonPanel, 0, 1);

            // Add the main layout to the form
            this.Controls.Add(mainLayout);

            // Hook up event handlers
            this.lstPlayers.SelectedIndexChanged += lstPlayers_SelectedIndexChanged;
            this.btnSelectImage.Click += btnSelectImage_Click;
            this.chkFavorite.CheckedChanged += chkFavorite_CheckedChanged;
            this.btnSave.Click += btnSave_Click;
            this.btnCancel.Click += btnCancel_Click;
            this.Load += PlayerSettingsForm_Load;
        }
    }
}
