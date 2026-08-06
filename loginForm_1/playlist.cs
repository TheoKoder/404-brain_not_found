using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class playlist : Form
    {
        // Holds the current playlist's name. Default provided so constructor can safely use it.
        private string PlaylistName = "Untitled Playlist";

        // ... you can replace this with real data-loading logic later.
        private void LoadPlaylistMetadata()
        {
            if (lblCreationDate != null)
            {
                // Example: show today's date as creation date when real metadata is not available
                lblCreationDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            }
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

        public playlist()
        {
            InitializeComponent();

            ApplyTheme(); // Applies HertzPlay theme automatically when opened

            // Use the PlaylistName field (can be set before showing the form) and
            // initialize display data. These methods are implemented above.
            lblPlaylistTitle.Text = PlaylistName;
            LoadPlaylistMetadata();
            LoadSongsList();
            UpdateTrackCount();
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

        }

        private void lblTrackCount_Click(object sender, EventArgs e)
        {

        }

        private void playlist_Load_1(object sender, EventArgs e)
        {

        }
    }
}
