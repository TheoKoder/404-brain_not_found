using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class Playlist : Form
    {
        private PlaylistModel currentPlaylist;
        private string currentPlaylistName;

        // DECLARE IT HERE:
        private string playlistMetadataFile;

        // Constructor accepting the full PlaylistModel from HomePage
        public Playlist(PlaylistModel playlist)
        {
            InitializeComponent();

            this.currentPlaylist = playlist ?? new PlaylistModel("Untitled Playlist");
            this.currentPlaylistName = this.currentPlaylist.Title;
            this.Text = $"Playlist - {this.currentPlaylistName}";
            this.playlistMetadataFile = $"{this.currentPlaylistName}_info.txt";

            // Attach Load Event
            this.Load += new System.EventHandler(this.playlist_Load);
        }

        public Playlist(string playlistName) : this(new PlaylistModel(playlistName))
        {
        }

        // Default constructor
        public Playlist() : this("My Playlist")
        {
        }


        private void Playlist_Load(object sender, EventArgs e)
        {
            // 1. Display the playlist name in the title label
            if (lblPlaylistTitle != null)
            {
                lblPlaylistTitle.Text = currentPlaylistName;
            }

            // 2. Load and apply custom colors and fonts
            ApplyTheme();

            // 3. Read or create the metadata file for date and cover image
            LoadPlaylistMetadata();

            // 4. Initialize song list and track count display
            LoadSongsList();
            UpdateTrackCount();
        }

        private void LoadSongsList()
        {
            if (lstSongs == null) return;
            lstSongs.Items.Clear();

            // If there are no songs yet, keep the list empty. This is a safe placeholder.
            // In a real implementation, populate lstSongs.Items from your data source.
            if (currentPlaylist != null && currentPlaylist.Songs != null && currentPlaylist.Songs.Count > 0)
            {
                foreach (var song in currentPlaylist.Songs)
                {
                    // Displays formatted song info in the list box
                    lstSongs.Items.Add($"{song.Name} - {song.Artist} ({song.Genre})");
                }
            }
        }

        private void UpdateTrackCount()
        {
            if (lblTrackCount == null || currentPlaylist == null) return;

            int count = currentPlaylist.Songs != null ? currentPlaylist.Songs.Count : 0;
            lblTrackCount.Text = $"{count} track" + (count == 1 ? "" : "s");
        }

        private void LoadPlaylistMetadata()
        {
            try
            {
                // If the model already has an image (e.g. assigned from Form2), load it first
                if (currentPlaylist != null && currentPlaylist.CoverImage != null && picCoverArt != null)
                {
                    picCoverArt.Image = currentPlaylist.CoverImage;
                    picCoverArt.SizeMode = PictureBoxSizeMode.Zoom;
                }

                // Check for saved text metadata
                if (File.Exists(playlistMetadataFile))
                {
                    using (StreamReader reader = new StreamReader(playlistMetadataFile))
                    {
                        string creationDate = reader.ReadLine();
                        string imagePath = reader.ReadLine();

                        if (!string.IsNullOrEmpty(creationDate) && lblCreationDate != null)
                        {
                            lblCreationDate.Text = "Created: " + creationDate;
                        }

                        // Restore cover art if path is valid and picture box isn't already set
                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath) && picCoverArt != null && picCoverArt.Image == null)
                        {
                            picCoverArt.ImageLocation = imagePath;
                            picCoverArt.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                }
                else
                {
                    string todayTimestamp = DateTime.Now.ToShortDateString();
                    if (lblCreationDate != null)
                    {
                        lblCreationDate.Text = "Created: " + todayTimestamp;
                    }

                    using (StreamWriter writer = new StreamWriter(playlistMetadataFile, false))
                    {
                        writer.WriteLine(todayTimestamp);
                        writer.WriteLine("");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading playlist details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUploadCover_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Cover Art";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;

                    if (picCoverArt != null)
                    {
                        picCoverArt.ImageLocation = selectedImagePath;
                        picCoverArt.SizeMode = PictureBoxSizeMode.Zoom;

                        // Also store in memory model
                        if (currentPlaylist != null)
                        {
                            currentPlaylist.CoverImage = Image.FromFile(selectedImagePath);
                        }
                    }

                    try
                    {
                        string creationDate = lblCreationDate != null ? lblCreationDate.Text.Replace("Created: ", "").Trim() : DateTime.Now.ToShortDateString();
                        using (StreamWriter writer = new StreamWriter(playlistMetadataFile, false))
                        {
                            writer.WriteLine(creationDate);
                            writer.WriteLine(selectedImagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ApplyTheme()
        {
            // --- 1. HERTZPLAY COLOR PALETTE ---
            // Deep dark violet/black background matching HertzPlay Login form
            System.Drawing.Color darkBackground = System.Drawing.Color.FromArgb(14, 11, 24);
            // Header & playback panel background (slightly elevated dark tone)
            System.Drawing.Color darkPanel = System.Drawing.Color.FromArgb(24, 18, 40);
            // HertzPlay signature vibrant purple for buttons and main accents
            System.Drawing.Color vibrantPurple = System.Drawing.Color.FromArgb(124, 45, 210);
            // Dark red accent specifically reserved for delete actions
            System.Drawing.Color deleteRed = System.Drawing.Color.FromArgb(140, 25, 45);

            System.Drawing.Color textWhite = System.Drawing.Color.White;
            System.Drawing.Color textMuted = System.Drawing.Color.FromArgb(200, 190, 220);

            // --- 2. FORM & PANEL BACKGROUNDS ---
            this.BackColor = darkBackground;
            if (pnlHeader != null) pnlHeader.BackColor = darkPanel;
            if (pnlLeft != null) pnlLeft.BackColor = darkBackground;
            if (pnlPlayback != null) pnlPlayback.BackColor = darkPanel;

            // --- 3. LABELS & TYPOGRAPHY ---
            // Title styled with the bold italic purple look from your Login page title
            lblPlaylistTitle.ForeColor = vibrantPurple;
            lblPlaylistTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);

            lblCreationDate.ForeColor = textMuted;
            lblCreationDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);

            lblTrackCount.ForeColor = textMuted;
            lblTrackCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            // --- 4. LISTBOX / SONG LIST ---
            lstSongs.BackColor = darkPanel;
            lstSongs.ForeColor = textWhite;
            lstSongs.BorderStyle = BorderStyle.FixedSingle;
            lstSongs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);

            // --- 5. COVER ART ---
            picCoverArt.BackColor = darkPanel;
            picCoverArt.BorderStyle = BorderStyle.FixedSingle;

            // --- 6. HERTZPLAY BUTTON STYLING ---
            Button[] buttons = {
        btnUploadCover, btnAddSong, btnSort,
        btnDeletePlaylist, btnPlaySong, btnDeletePlaylist, btnSort
    };

            foreach (Button btn in buttons)
            {
                if (btn != null)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = vibrantPurple;
                    btn.ForeColor = textWhite;
                    btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                }
            }

            // Optional: Keep Delete buttons visually distinct with dark red flat fill
            if (btnDeletePlaylist != null) btnDeletePlaylist.BackColor = deleteRed;
            if (btnDeletePlaylist != null) btnDeletePlaylist.BackColor = deleteRed;
        }

        private void pnlLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDeletePlaylist_Click(object sender, EventArgs e)
        {

        }

        private void playlist_Load(object sender, EventArgs e)
        {
            // TEST LINE: This popup tells us if Windows Forms is actually running this code
            //MessageBox.Show("Playlist Load Event Fired!", "Debug Test");

            // Set the heading label text
            if (lblPlaylistTitle != null)
            {
                lblPlaylistTitle.Text = currentPlaylistName;
            }

            // CALL THIS: Reads {playlistTitle}_info.txt using StreamReader and sets lblCreationDate
            LoadPlaylistMetadata();

            // Load visual styles & initial track count
            ApplyTheme();
            UpdateTrackCount();


        }

        private void lblTrackCount_Click(object sender, EventArgs e)
        {

        }

        private void playlist_Load_1(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUploadCover_Click_1(object sender, EventArgs e)
        {

        }
    }
}
