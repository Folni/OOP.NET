namespace wfa_FilipFolnegović
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowLayoutPanelPlayers = new FlowLayoutPanel();
            cbTeams = new ComboBox();
            flowLayoutPanelPlayers.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanelPlayers
            // 
            flowLayoutPanelPlayers.Controls.Add(cbTeams);
            flowLayoutPanelPlayers.Dock = DockStyle.Fill;
            flowLayoutPanelPlayers.Location = new Point(0, 0);
            flowLayoutPanelPlayers.Name = "flowLayoutPanelPlayers";
            flowLayoutPanelPlayers.Size = new Size(800, 450);
            flowLayoutPanelPlayers.TabIndex = 0;
            // 
            // cbTeams
            // 
            cbTeams.FormattingEnabled = true;
            cbTeams.Location = new Point(3, 3);
            cbTeams.Name = "cbTeams";
            cbTeams.Size = new Size(221, 28);
            cbTeams.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flowLayoutPanelPlayers);
            Name = "MainForm";
            Text = "Form1";
            flowLayoutPanelPlayers.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelPlayers;
        private ComboBox cbTeams;
    }
}
