namespace loginForm_1
{
    partial class Form2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblCreatePlaylist = new Label();
            txtCreatePlaylist = new TextBox();
            btnDone = new Button();
            picCreate = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picCreate).BeginInit();
            SuspendLayout();
            // 
            // lblCreatePlaylist
            // 
            lblCreatePlaylist.AutoSize = true;
            lblCreatePlaylist.Location = new Point(309, 55);
            lblCreatePlaylist.Name = "lblCreatePlaylist";
            lblCreatePlaylist.Size = new Size(81, 15);
            lblCreatePlaylist.TabIndex = 0;
            lblCreatePlaylist.Text = "Create Playlist";
            // 
            // txtCreatePlaylist
            // 
            txtCreatePlaylist.Location = new Point(299, 90);
            txtCreatePlaylist.Name = "txtCreatePlaylist";
            txtCreatePlaylist.Size = new Size(100, 23);
            txtCreatePlaylist.TabIndex = 1;
            // 
            // btnDone
            // 
            btnDone.Location = new Point(315, 239);
            btnDone.Name = "btnDone";
            btnDone.Size = new Size(75, 23);
            btnDone.TabIndex = 2;
            btnDone.Text = "Done";
            btnDone.UseVisualStyleBackColor = true;
            btnDone.Click += btnDone_Click;
            // 
            // picCreate
            // 
            picCreate.BackColor = SystemColors.ControlDark;
            picCreate.Location = new Point(299, 146);
            picCreate.Name = "picCreate";
            picCreate.Size = new Size(100, 50);
            picCreate.SizeMode = PictureBoxSizeMode.StretchImage;
            picCreate.TabIndex = 3;
            picCreate.TabStop = false;
            picCreate.Click += picCreate_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(picCreate);
            Controls.Add(btnDone);
            Controls.Add(txtCreatePlaylist);
            Controls.Add(lblCreatePlaylist);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)picCreate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCreatePlaylist;
        private TextBox txtCreatePlaylist;
        private Button btnDone;
        private PictureBox picCreate;
    }
}