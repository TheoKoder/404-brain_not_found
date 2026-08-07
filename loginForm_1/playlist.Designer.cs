namespace loginForm_1
{
    partial class Playlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Playlist));
            pnlHeader = new Panel();
            lblCreationDate = new Label();
            lblPlaylistTitle = new Label();
            pnlPlayback = new Panel();
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            btnPlaySong = new Button();
            btnDeletePlaylist = new Button();
            btnAddSong = new Button();
            btnSort = new Button();
            pnlLeft = new Panel();
            btnUploadCover = new Button();
            picCoverArt = new PictureBox();
            lblTrackCount = new Label();
            lstSongs = new ListBox();
            pnlHeader.SuspendLayout();
            pnlPlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCoverArt).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblCreationDate);
            pnlHeader.Controls.Add(lblPlaylistTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(800, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblCreationDate
            // 
            lblCreationDate.AutoSize = true;
            lblCreationDate.Location = new Point(690, 9);
            lblCreationDate.Name = "lblCreationDate";
            lblCreationDate.Size = new Size(51, 15);
            lblCreationDate.TabIndex = 3;
            lblCreationDate.Text = "Created:";
            // 
            // lblPlaylistTitle
            // 
            lblPlaylistTitle.AutoSize = true;
            lblPlaylistTitle.Location = new Point(12, 9);
            lblPlaylistTitle.Name = "lblPlaylistTitle";
            lblPlaylistTitle.Size = new Size(47, 15);
            lblPlaylistTitle.TabIndex = 0;
            lblPlaylistTitle.Text = "Playlist ";
            // 
            // pnlPlayback
            // 
            pnlPlayback.Controls.Add(axWindowsMediaPlayer1);
            pnlPlayback.Controls.Add(btnPlaySong);
            pnlPlayback.Controls.Add(btnDeletePlaylist);
            pnlPlayback.Controls.Add(btnAddSong);
            pnlPlayback.Controls.Add(btnSort);
            pnlPlayback.Dock = DockStyle.Bottom;
            pnlPlayback.Location = new Point(0, 320);
            pnlPlayback.Name = "pnlPlayback";
            pnlPlayback.Size = new Size(800, 130);
            pnlPlayback.TabIndex = 1;
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(255, 62);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(416, 46);
            axWindowsMediaPlayer1.TabIndex = 8;
            // 
            // btnPlaySong
            // 
            btnPlaySong.Location = new Point(475, 12);
            btnPlaySong.Name = "btnPlaySong";
            btnPlaySong.Size = new Size(110, 35);
            btnPlaySong.TabIndex = 7;
            btnPlaySong.Text = "Play Song";
            btnPlaySong.UseVisualStyleBackColor = true;
            // 
            // btnDeletePlaylist
            // 
            btnDeletePlaylist.Location = new Point(355, 12);
            btnDeletePlaylist.Name = "btnDeletePlaylist";
            btnDeletePlaylist.Size = new Size(110, 35);
            btnDeletePlaylist.TabIndex = 6;
            btnDeletePlaylist.Text = "Delete";
            btnDeletePlaylist.UseVisualStyleBackColor = true;
            // 
            // btnAddSong
            // 
            btnAddSong.Location = new Point(235, 12);
            btnAddSong.Name = "btnAddSong";
            btnAddSong.Size = new Size(110, 35);
            btnAddSong.TabIndex = 4;
            btnAddSong.Text = "Add";
            btnAddSong.UseVisualStyleBackColor = true;
            // 
            // btnSort
            // 
            btnSort.Location = new Point(595, 12);
            btnSort.Name = "btnSort";
            btnSort.Size = new Size(110, 35);
            btnSort.TabIndex = 5;
            btnSort.Text = "Sort";
            btnSort.UseVisualStyleBackColor = true;
            // 
            // pnlLeft
            // 
            pnlLeft.Controls.Add(btnUploadCover);
            pnlLeft.Controls.Add(picCoverArt);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 80);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(220, 240);
            pnlLeft.TabIndex = 2;
            // 
            // btnUploadCover
            // 
            btnUploadCover.Location = new Point(57, 152);
            btnUploadCover.Name = "btnUploadCover";
            btnUploadCover.Size = new Size(75, 23);
            btnUploadCover.TabIndex = 3;
            btnUploadCover.Text = "Upload Cover";
            btnUploadCover.UseVisualStyleBackColor = true;
            // 
            // picCoverArt
            // 
            picCoverArt.Location = new Point(45, 17);
            picCoverArt.Name = "picCoverArt";
            picCoverArt.Size = new Size(123, 117);
            picCoverArt.TabIndex = 0;
            picCoverArt.TabStop = false;
            // 
            // lblTrackCount
            // 
            lblTrackCount.AutoSize = true;
            lblTrackCount.Location = new Point(720, 95);
            lblTrackCount.Name = "lblTrackCount";
            lblTrackCount.Size = new Size(72, 15);
            lblTrackCount.TabIndex = 3;
            lblTrackCount.Text = "Total Tracks:";
            lblTrackCount.Click += lblTrackCount_Click;
            // 
            // lstSongs
            // 
            lstSongs.FormattingEnabled = true;
            lstSongs.Location = new Point(226, 85);
            lstSongs.Name = "lstSongs";
            lstSongs.Size = new Size(488, 229);
            lstSongs.TabIndex = 4;
            // 
            // playlist
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstSongs);
            Controls.Add(lblTrackCount);
            Controls.Add(pnlLeft);
            Controls.Add(pnlPlayback);
            Controls.Add(pnlHeader);
            Name = "playlist";
            Text = "Playlist";
            Load += playlist_Load_1;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlPlayback.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picCoverArt).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Panel pnlPlayback;
        private Panel pnlLeft;
        private PictureBox pictureBox1;
        private Label lblCreationDate;
        private Label lblPlaylistTitle;
        private PictureBox picCoverArt;
        private Button btnUploadCover;
        private Button btnDeletePlaylist;
        private Button btnAddSong;
        private Button btnSort;
        private Button btnPlaySong;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private Label lblTrackCount;
        private ListBox lstSongs;
    }
}