using Library;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace wfa_FilipFolnegović
{
    public partial class SettingsForm : Form
    {
        public bool IsMenWorldCup { get; private set; }

        private static readonly Color SoftPurple = Color.FromArgb(200, 162, 200);
        private static readonly Color SoftYellow = Color.FromArgb(255, 255, 204);
        private Button btnCancel;
        private Button btnSave;
        private RadioButton rdoWomen;
        private RadioButton rdoMen;
        private static readonly Color BackgroundColor = Color.FromArgb(240, 240, 245);

        public SettingsForm(bool currentChoice)
        {
            InitializeComponent();
            this.IsMenWorldCup = currentChoice;
            this.BackColor = BackgroundColor;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            rdoMen.Checked = IsMenWorldCup;
            rdoWomen.Checked = !IsMenWorldCup;

            btnSave.BackColor = SoftPurple;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;

            btnCancel.BackColor = SoftYellow;
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IsMenWorldCup = rdoMen.Checked;

            // Save the setting to a configuration file.
            try
            {
                string settingsFilePath = Path.Combine(Information.relativePath, "settings.txt");
                File.WriteAllText(settingsFilePath, IsMenWorldCup.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            btnCancel = new Button();
            btnSave = new Button();
            rdoWomen = new RadioButton();
            rdoMen = new RadioButton();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(165, 175);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(24, 175);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // rdoWomen
            // 
            rdoWomen.AutoSize = true;
            rdoWomen.Location = new Point(28, 89);
            rdoWomen.Name = "rdoWomen";
            rdoWomen.Size = new Size(164, 24);
            rdoWomen.TabIndex = 5;
            rdoWomen.TabStop = true;
            rdoWomen.Text = "Women's World Cup";
            rdoWomen.UseVisualStyleBackColor = true;
            // 
            // rdoMen
            // 
            rdoMen.AutoSize = true;
            rdoMen.Location = new Point(28, 48);
            rdoMen.Name = "rdoMen";
            rdoMen.Size = new Size(142, 24);
            rdoMen.TabIndex = 4;
            rdoMen.TabStop = true;
            rdoMen.Text = "Men's World Cup";
            rdoMen.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            ClientSize = new Size(282, 253);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(rdoWomen);
            Controls.Add(rdoMen);
            Name = "SettingsForm";
            ResumeLayout(false);
            PerformLayout();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}