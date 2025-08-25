namespace WindowsFormsApp
{
    partial class UserControlPlayer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlPlayer));
            pnlImage = new Panel();
            lbName = new Label();
            lbSurname = new Label();
            lbShirtNumber = new Label();
            pnlContainer = new Panel();
            pnlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // pnlImage
            // 
            pnlImage.BackgroundImage = (Image)resources.GetObject("pnlImage.BackgroundImage");
            pnlImage.BackgroundImageLayout = ImageLayout.Zoom;
            pnlImage.Location = new Point(82, 4);
            pnlImage.Margin = new Padding(3, 4, 3, 4);
            pnlImage.Name = "pnlImage";
            pnlImage.Size = new Size(57, 67);
            pnlImage.TabIndex = 0;
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.BackColor = Color.Transparent;
            lbName.Font = new Font("Segoe UI", 8F);
            lbName.Location = new Point(3, 4);
            lbName.Name = "lbName";
            lbName.Size = new Size(45, 19);
            lbName.TabIndex = 0;
            lbName.Text = "label1";
            // 
            // lbSurname
            // 
            lbSurname.AutoSize = true;
            lbSurname.BackColor = Color.Transparent;
            lbSurname.Font = new Font("Segoe UI", 8F);
            lbSurname.Location = new Point(3, 21);
            lbSurname.Name = "lbSurname";
            lbSurname.Size = new Size(45, 19);
            lbSurname.TabIndex = 1;
            lbSurname.Text = "label2";
            // 
            // lbShirtNumber
            // 
            lbShirtNumber.AutoSize = true;
            lbShirtNumber.BackColor = Color.Transparent;
            lbShirtNumber.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbShirtNumber.Location = new Point(25, 49);
            lbShirtNumber.Margin = new Padding(0);
            lbShirtNumber.Name = "lbShirtNumber";
            lbShirtNumber.Size = new Size(55, 23);
            lbShirtNumber.TabIndex = 2;
            lbShirtNumber.Text = "label3";
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(lbName);
            pnlContainer.Controls.Add(lbSurname);
            pnlContainer.Controls.Add(lbShirtNumber);
            pnlContainer.Controls.Add(pnlImage);
            pnlContainer.Location = new Point(2, 3);
            pnlContainer.Margin = new Padding(0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(144, 75);
            pnlContainer.TabIndex = 3;
            // 
            // UserControlPlayer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pnlContainer);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UserControlPlayer";
            Size = new Size(151, 83);
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlImage;
        private Label lbName;
        private Label lbSurname;
        private Label lbShirtNumber;
        private Panel pnlContainer;
    }
}
