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
        private string currentPlaylistName;



        // DECLARE IT HERE:
        private string playlistMetadataFile;
        // Constructor accepting the playlist title string
        public Playlist(string playlistName)
        {
            InitializeComponent();
            
            // ASSIGN IT HERE:
            
            this.Load += new System.EventHandler(this.playlist_Load);

            this.currentPlaylistName = playlistName;
            this.Text = $"Playlist - {playlistName}";
            this.playlistMetadataFile = $"{currentPlaylistName}_info.txt";
        }


        // Default constructor
        public Playlist() : this("My Playlist")
        {
        }
        // Default constructor



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

            // If there are no songs yet, keep the list empty. This is a safe placeholder.
            // In a real implementation, populate lstSongs.Items from your data source.
            if (lstSongs.Items.Count == 0)
            {
                // Optionally add a placeholder item to indicate an empty playlist
                // lstSongs.Items.Add("(No songs)");
            }
        }

        private void UpdateTrackCount()
        {
            if (lblTrackCount == null || lstSongs == null) return;
            lblTrackCount.Text = $"{lstSongs.Items.Count} track" + (lstSongs.Items.Count == 1 ? "" : "s");
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

        private void LoadPlaylistMetadata()
        {
            try
            {
                // Check if the metadata text file for this specific playlist already exists on disk
                if (File.Exists(playlistMetadataFile))
                {
                    // StreamReader reads the existing file line by line
                    using (StreamReader reader = new StreamReader(playlistMetadataFile))
                    {
                        string creationDate = reader.ReadLine(); // Line 1: Saved timestamp
                        string imagePath = reader.ReadLine();    // Line 2: Saved cover art path

                        // If a valid creation date was found in the text file, use it
                        if (!string.IsNullOrEmpty(creationDate))
                        {
                            lblCreationDate.Text = "Created: " + creationDate;
                        }
                        else
                        {
                            // Fallback to today's date if the line is blank
                            lblCreationDate.Text = "Created: " + DateTime.Now.ToShortDateString();
                        }

                        // Restore the cover art image if the path is valid and file exists
                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            picCoverArt.ImageLocation = imagePath;
                        }
                    }
                }
                else
                {
                    // If the text file doesn't exist yet, this is a brand-new playlist.
                    // DateTime.Now.ToShortDateString() gets today's current date as a string timestamp (e.g., "2026/08/09").
                    string todayTimestamp = DateTime.Now.ToShortDateString();
                    lblCreationDate.Text = "Created: " + todayTimestamp;

                    // StreamWriter creates the text file and writes the new timestamp to Line 1
                    using (StreamWriter writer = new StreamWriter(playlistMetadataFile, false))
                    {
                        writer.WriteLine(todayTimestamp); // Save the timestamp
                        writer.WriteLine("");             // Leave Line 2 empty for now (no cover art yet)
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

                    // Set picture box
                    if (picCoverArt != null)
                    {
                        picCoverArt.ImageLocation = selectedImagePath;
                        picCoverArt.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    // Save image path to text file
                    try
                    {
                        string creationDate = lblCreationDate != null ? lblCreationDate.Text.Replace("Created: ", "").Trim() : DateTime.Now.ToShortDateString();
                        using (StreamWriter writer = new StreamWriter(playlistMetadataFile, false))
                        {
                            writer.WriteLine(creationDate);     // Line 1: Date
                            writer.WriteLine(selectedImagePath); // Line 2: Image Path
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
