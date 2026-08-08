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
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)picCreate).BeginInit();
            SuspendLayout();
            // 
            // lblCreatePlaylist
            // 
            lblCreatePlaylist.AutoSize = true;
            lblCreatePlaylist.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCreatePlaylist.ForeColor = Color.Purple;
            lblCreatePlaylist.Location = new Point(299, 26);
            lblCreatePlaylist.Name = "lblCreatePlaylist";
            lblCreatePlaylist.Size = new Size(119, 21);
            lblCreatePlaylist.TabIndex = 0;
            lblCreatePlaylist.Text = "Create Playlist";
            // 
            // txtCreatePlaylist
            // 
            txtCreatePlaylist.Location = new Point(303, 92);
            txtCreatePlaylist.Name = "txtCreatePlaylist";
            txtCreatePlaylist.Size = new Size(100, 23);
            txtCreatePlaylist.TabIndex = 1;
            // 
            // btnDone
            // 
            btnDone.ForeColor = Color.Purple;
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
            picCreate.Size = new Size(119, 64);
            picCreate.SizeMode = PictureBoxSizeMode.StretchImage;
            picCreate.TabIndex = 3;
            picCreate.TabStop = false;
            picCreate.Click += picCreate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Purple;
            label1.Location = new Point(295, 126);
            label1.Name = "label1";
            label1.Size = new Size(123, 17);
            label1.TabIndex = 4;
            label1.Text = "Select Playlist Icon";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Purple;
            label2.Location = new Point(310, 72);
            label2.Name = "label2";
            label2.Size = new Size(93, 17);
            label2.TabIndex = 5;
            label2.Text = "Playlist Name";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
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
        private TextBox txtCreatePlaylist;
        private Button btnDone;
        private PictureBox picCreate;
        private Label label1;
        private Label label2;
        private Label lblCreatePlaylist;
    }
}