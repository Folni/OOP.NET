namespace WindowsFormsApp
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            groupBox1 = new GroupBox();
            rbWomens = new RadioButton();
            rbMens = new RadioButton();
            cbLanguage = new ComboBox();
            tcbPages = new TabControl();
            tabPage1 = new TabPage();
            label2 = new Label();
            label1 = new Label();
            btnPage1 = new Button();
            tabPage2 = new TabPage();
            btnPage2 = new Button();
            label3 = new Label();
            ddlTeams = new ComboBox();
            tabPage3 = new TabPage();
            button1 = new Button();
            btnSelectAll = new Button();
            label5 = new Label();
            btnDeselectAll = new Button();
            btnEditPicture = new Button();
            btnPage3 = new Button();
            pnlFavorites = new FlowLayoutPanel();
            pnlPlayers = new FlowLayoutPanel();
            label4 = new Label();
            tabPage4 = new TabPage();
            btnPrint = new Button();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            groupBox2 = new GroupBox();
            label14 = new Label();
            lbDescription = new Label();
            label15 = new Label();
            lbWindSpeed = new Label();
            label16 = new Label();
            lbTemperature = new Label();
            label17 = new Label();
            lbHumidity = new Label();
            Winner = new Label();
            gbAway_Team = new GroupBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            lbACountry = new Label();
            lbAPenalties = new Label();
            lbACode = new Label();
            lbAGoals = new Label();
            gbHome_Team = new GroupBox();
            label9 = new Label();
            label8 = new Label();
            label6 = new Label();
            label7 = new Label();
            lbHCountry = new Label();
            lbHCode = new Label();
            lbHGoals = new Label();
            lbHPenalties = new Label();
            lbWinner = new Label();
            lbAttendance = new Label();
            lbLocation = new Label();
            lbVenue = new Label();
            ddlMatches = new ComboBox();
            lwPlayers = new ListView();
            columnName = new ColumnHeader();
            columnSurname = new ColumnHeader();
            columnShirtNo = new ColumnHeader();
            columnGoals = new ColumnHeader();
            columnCards = new ColumnHeader();
            cmsFavorites = new ContextMenuStrip(components);
            unFavoriteToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            Reset = new ToolStripMenuItem();
            cmsPlayers = new ContextMenuStrip(components);
            favoriteToolStripMenuItem = new ToolStripMenuItem();
            printDocument = new System.Drawing.Printing.PrintDocument();
            printPreviewDialog = new PrintPreviewDialog();
            groupBox1.SuspendLayout();
            tcbPages.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            groupBox2.SuspendLayout();
            gbAway_Team.SuspendLayout();
            gbHome_Team.SuspendLayout();
            cmsFavorites.SuspendLayout();
            menuStrip1.SuspendLayout();
            cmsPlayers.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbWomens);
            groupBox1.Controls.Add(rbMens);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // rbWomens
            // 
            resources.ApplyResources(rbWomens, "rbWomens");
            rbWomens.Checked = true;
            rbWomens.Name = "rbWomens";
            rbWomens.TabStop = true;
            rbWomens.UseVisualStyleBackColor = true;
            // 
            // rbMens
            // 
            resources.ApplyResources(rbMens, "rbMens");
            rbMens.Name = "rbMens";
            rbMens.UseVisualStyleBackColor = true;
            // 
            // cbLanguage
            // 
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { resources.GetString("cbLanguage.Items"), resources.GetString("cbLanguage.Items1") });
            resources.ApplyResources(cbLanguage, "cbLanguage");
            cbLanguage.Name = "cbLanguage";
            // 
            // tcbPages
            // 
            tcbPages.Controls.Add(tabPage1);
            tcbPages.Controls.Add(tabPage2);
            tcbPages.Controls.Add(tabPage3);
            tcbPages.Controls.Add(tabPage4);
            resources.ApplyResources(tcbPages, "tcbPages");
            tcbPages.Name = "tcbPages";
            tcbPages.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(btnPage1);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(cbLanguage);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // btnPage1
            // 
            resources.ApplyResources(btnPage1, "btnPage1");
            btnPage1.Name = "btnPage1";
            btnPage1.UseVisualStyleBackColor = true;
            btnPage1.Click += GenderSubmitButton;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnPage2);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(ddlTeams);
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.Name = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnPage2
            // 
            resources.ApplyResources(btnPage2, "btnPage2");
            btnPage2.Name = "btnPage2";
            btnPage2.UseVisualStyleBackColor = true;
            btnPage2.Click += btnTeamSubmit_Click;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // ddlTeams
            // 
            ddlTeams.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlTeams.FormattingEnabled = true;
            resources.ApplyResources(ddlTeams, "ddlTeams");
            ddlTeams.Name = "ddlTeams";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(button1);
            tabPage3.Controls.Add(btnSelectAll);
            tabPage3.Controls.Add(label5);
            tabPage3.Controls.Add(btnDeselectAll);
            tabPage3.Controls.Add(btnEditPicture);
            tabPage3.Controls.Add(btnPage3);
            tabPage3.Controls.Add(pnlFavorites);
            tabPage3.Controls.Add(pnlPlayers);
            tabPage3.Controls.Add(label4);
            resources.ApplyResources(tabPage3, "tabPage3");
            tabPage3.Name = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.Tag = "2";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSelectAll_Click;
            // 
            // btnSelectAll
            // 
            resources.ApplyResources(btnSelectAll, "btnSelectAll");
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Tag = "1";
            btnSelectAll.UseVisualStyleBackColor = true;
            btnSelectAll.Click += btnSelectAll_Click;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // btnDeselectAll
            // 
            resources.ApplyResources(btnDeselectAll, "btnDeselectAll");
            btnDeselectAll.Name = "btnDeselectAll";
            btnDeselectAll.UseVisualStyleBackColor = true;
            btnDeselectAll.Click += btnDeselectAll_Click;
            // 
            // btnEditPicture
            // 
            resources.ApplyResources(btnEditPicture, "btnEditPicture");
            btnEditPicture.Name = "btnEditPicture";
            btnEditPicture.UseVisualStyleBackColor = true;
            btnEditPicture.Click += btnEditPicture_Click;
            // 
            // btnPage3
            // 
            resources.ApplyResources(btnPage3, "btnPage3");
            btnPage3.Name = "btnPage3";
            btnPage3.UseVisualStyleBackColor = true;
            btnPage3.Click += btnPage3_Click;
            // 
            // pnlFavorites
            // 
            pnlFavorites.AllowDrop = true;
            resources.ApplyResources(pnlFavorites, "pnlFavorites");
            pnlFavorites.BackColor = Color.Gainsboro;
            pnlFavorites.BorderStyle = BorderStyle.FixedSingle;
            pnlFavorites.Name = "pnlFavorites";
            pnlFavorites.DragDrop += Player_DragDrop;
            pnlFavorites.DragEnter += Player_DragEnter;
            pnlFavorites.MouseMove += pnlPlayers_MouseMove;
            // 
            // pnlPlayers
            // 
            pnlPlayers.AllowDrop = true;
            resources.ApplyResources(pnlPlayers, "pnlPlayers");
            pnlPlayers.BackColor = Color.Gainsboro;
            pnlPlayers.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayers.Name = "pnlPlayers";
            pnlPlayers.DragDrop += Player_DragDrop;
            pnlPlayers.DragEnter += Player_DragEnter;
            pnlPlayers.MouseMove += pnlPlayers_MouseMove;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(btnPrint);
            tabPage4.Controls.Add(label20);
            tabPage4.Controls.Add(label19);
            tabPage4.Controls.Add(label18);
            tabPage4.Controls.Add(groupBox2);
            tabPage4.Controls.Add(Winner);
            tabPage4.Controls.Add(gbAway_Team);
            tabPage4.Controls.Add(gbHome_Team);
            tabPage4.Controls.Add(lbWinner);
            tabPage4.Controls.Add(lbAttendance);
            tabPage4.Controls.Add(lbLocation);
            tabPage4.Controls.Add(lbVenue);
            tabPage4.Controls.Add(ddlMatches);
            tabPage4.Controls.Add(lwPlayers);
            resources.ApplyResources(tabPage4, "tabPage4");
            tabPage4.Name = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            resources.ApplyResources(btnPrint, "btnPrint");
            btnPrint.Name = "btnPrint";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(lbDescription);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(lbWindSpeed);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(lbTemperature);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(lbHumidity);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.Name = "label14";
            // 
            // lbDescription
            // 
            resources.ApplyResources(lbDescription, "lbDescription");
            lbDescription.Name = "lbDescription";
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.Name = "label15";
            // 
            // lbWindSpeed
            // 
            resources.ApplyResources(lbWindSpeed, "lbWindSpeed");
            lbWindSpeed.Name = "lbWindSpeed";
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            // 
            // lbTemperature
            // 
            resources.ApplyResources(lbTemperature, "lbTemperature");
            lbTemperature.Name = "lbTemperature";
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            // 
            // lbHumidity
            // 
            resources.ApplyResources(lbHumidity, "lbHumidity");
            lbHumidity.Name = "lbHumidity";
            // 
            // Winner
            // 
            resources.ApplyResources(Winner, "Winner");
            Winner.Name = "Winner";
            // 
            // gbAway_Team
            // 
            gbAway_Team.Controls.Add(label10);
            gbAway_Team.Controls.Add(label11);
            gbAway_Team.Controls.Add(label12);
            gbAway_Team.Controls.Add(label13);
            gbAway_Team.Controls.Add(lbACountry);
            gbAway_Team.Controls.Add(lbAPenalties);
            gbAway_Team.Controls.Add(lbACode);
            gbAway_Team.Controls.Add(lbAGoals);
            resources.ApplyResources(gbAway_Team, "gbAway_Team");
            gbAway_Team.Name = "gbAway_Team";
            gbAway_Team.TabStop = false;
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            // 
            // lbACountry
            // 
            resources.ApplyResources(lbACountry, "lbACountry");
            lbACountry.Name = "lbACountry";
            // 
            // lbAPenalties
            // 
            resources.ApplyResources(lbAPenalties, "lbAPenalties");
            lbAPenalties.Name = "lbAPenalties";
            // 
            // lbACode
            // 
            resources.ApplyResources(lbACode, "lbACode");
            lbACode.Name = "lbACode";
            // 
            // lbAGoals
            // 
            resources.ApplyResources(lbAGoals, "lbAGoals");
            lbAGoals.Name = "lbAGoals";
            // 
            // gbHome_Team
            // 
            gbHome_Team.Controls.Add(label9);
            gbHome_Team.Controls.Add(label8);
            gbHome_Team.Controls.Add(label6);
            gbHome_Team.Controls.Add(label7);
            gbHome_Team.Controls.Add(lbHCountry);
            gbHome_Team.Controls.Add(lbHCode);
            gbHome_Team.Controls.Add(lbHGoals);
            gbHome_Team.Controls.Add(lbHPenalties);
            resources.ApplyResources(gbHome_Team, "gbHome_Team");
            gbHome_Team.Name = "gbHome_Team";
            gbHome_Team.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // lbHCountry
            // 
            resources.ApplyResources(lbHCountry, "lbHCountry");
            lbHCountry.Name = "lbHCountry";
            // 
            // lbHCode
            // 
            resources.ApplyResources(lbHCode, "lbHCode");
            lbHCode.Name = "lbHCode";
            // 
            // lbHGoals
            // 
            resources.ApplyResources(lbHGoals, "lbHGoals");
            lbHGoals.Name = "lbHGoals";
            // 
            // lbHPenalties
            // 
            resources.ApplyResources(lbHPenalties, "lbHPenalties");
            lbHPenalties.Name = "lbHPenalties";
            // 
            // lbWinner
            // 
            resources.ApplyResources(lbWinner, "lbWinner");
            lbWinner.Name = "lbWinner";
            // 
            // lbAttendance
            // 
            resources.ApplyResources(lbAttendance, "lbAttendance");
            lbAttendance.Name = "lbAttendance";
            // 
            // lbLocation
            // 
            resources.ApplyResources(lbLocation, "lbLocation");
            lbLocation.Name = "lbLocation";
            // 
            // lbVenue
            // 
            resources.ApplyResources(lbVenue, "lbVenue");
            lbVenue.Name = "lbVenue";
            // 
            // ddlMatches
            // 
            ddlMatches.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlMatches.FormattingEnabled = true;
            resources.ApplyResources(ddlMatches, "ddlMatches");
            ddlMatches.Name = "ddlMatches";
            ddlMatches.SelectedIndexChanged += cbMatches_SelectedIndexChanged;
            // 
            // lwPlayers
            // 
            lwPlayers.Columns.AddRange(new ColumnHeader[] { columnName, columnSurname, columnShirtNo, columnGoals, columnCards });
            resources.ApplyResources(lwPlayers, "lwPlayers");
            lwPlayers.Name = "lwPlayers";
            lwPlayers.UseCompatibleStateImageBehavior = false;
            lwPlayers.View = View.Details;
            // 
            // columnName
            // 
            resources.ApplyResources(columnName, "columnName");
            // 
            // columnSurname
            // 
            resources.ApplyResources(columnSurname, "columnSurname");
            // 
            // columnShirtNo
            // 
            resources.ApplyResources(columnShirtNo, "columnShirtNo");
            // 
            // columnGoals
            // 
            resources.ApplyResources(columnGoals, "columnGoals");
            // 
            // columnCards
            // 
            resources.ApplyResources(columnCards, "columnCards");
            // 
            // cmsFavorites
            // 
            cmsFavorites.ImageScalingSize = new Size(20, 20);
            cmsFavorites.Items.AddRange(new ToolStripItem[] { unFavoriteToolStripMenuItem });
            cmsFavorites.Name = "contextMenuStrip1";
            resources.ApplyResources(cmsFavorites, "cmsFavorites");
            // 
            // unFavoriteToolStripMenuItem
            // 
            unFavoriteToolStripMenuItem.Name = "unFavoriteToolStripMenuItem";
            resources.ApplyResources(unFavoriteToolStripMenuItem, "unFavoriteToolStripMenuItem");
            unFavoriteToolStripMenuItem.Click += unFavoriteToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Reset });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // Reset
            // 
            Reset.Name = "Reset";
            resources.ApplyResources(Reset, "Reset");
            Reset.Click += toolStripMenuIReset_Click;
            // 
            // cmsPlayers
            // 
            cmsPlayers.ImageScalingSize = new Size(20, 20);
            cmsPlayers.Items.AddRange(new ToolStripItem[] { favoriteToolStripMenuItem });
            cmsPlayers.Name = "cmsPlayers";
            resources.ApplyResources(cmsPlayers, "cmsPlayers");
            // 
            // favoriteToolStripMenuItem
            // 
            favoriteToolStripMenuItem.Name = "favoriteToolStripMenuItem";
            resources.ApplyResources(favoriteToolStripMenuItem, "favoriteToolStripMenuItem");
            favoriteToolStripMenuItem.Click += favoriteToolStripMenuItem_Click;
            // 
            // printDocument
            // 
            printDocument.EndPrint += printDocument_EndPrint;
            printDocument.PrintPage += printDocument_PrintPage;
            // 
            // printPreviewDialog
            // 
            resources.ApplyResources(printPreviewDialog, "printPreviewDialog");
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.Name = "printPreviewDialog";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tcbPages);
            Controls.Add(menuStrip1);
            Name = "MainForm";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tcbPages.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            gbAway_Team.ResumeLayout(false);
            gbAway_Team.PerformLayout();
            gbHome_Team.ResumeLayout(false);
            gbHome_Team.PerformLayout();
            cmsFavorites.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            cmsPlayers.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private RadioButton rbWomens;
        private RadioButton rbMens;
        private ComboBox cbLanguage;
        private TabControl tcbPages;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Button btnPage1;
        private Label label2;
        private Label label1;
        private Button btnPage2;
        private Label label3;
        private ComboBox ddlTeams;
        private TabPage tabPage3;
        private Label label4;
        private ContextMenuStrip cmsFavorites;
        private FlowLayoutPanel pnlFavorites;
        private FlowLayoutPanel pnlPlayers;
        private ToolStripMenuItem unFavoriteToolStripMenuItem;
        private ContextMenuStrip cmsPlayers;
        private ToolStripMenuItem favoriteToolStripMenuItem;
        private TabPage tabPage4;
        private ListView lwPlayers;
        private ColumnHeader columnName;
        private Button btnPage3;
        private ColumnHeader columnSurname;
        private ColumnHeader columnShirtNo;
        private ColumnHeader columnGoals;
        private ColumnHeader columnCards;
        private Button btnEditPicture;
        private Button btnDeselectAll;
        private Label label5;
        private Button btnSelectAll;
        private Button button1;
        private ComboBox ddlMatches;
        private ToolStripMenuItem Reset;
        private Label lbAttendance;
        private Label lbHumidity;
        private Label lbAPenalties;
        private Label lbAGoals;
        private Label lbACode;
        private Label lbACountry;
        private Label lbLocation;
        private Label lbVenue;
        private Label lbHPenalties;
        private Label lbHGoals;
        private Label lbHCode;
        private Label lbHCountry;
        private Label lbWinner;
        private Label label7;
        private Label label6;
        private GroupBox gbHome_Team;
        private Label label8;
        private GroupBox gbAway_Team;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label9;
        private Label Winner;
        private Label lbDescription;
        private Label lbWindSpeed;
        private Label lbTemperature;
        private Label label18;
        private GroupBox groupBox2;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Button btnPrint;
        private Label label20;
        private Label label19;
        private System.Drawing.Printing.PrintDocument printDocument;
        private PrintPreviewDialog printPreviewDialog;
    }
}
