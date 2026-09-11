using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json; // REQUIRED FOR SERIALIZATION
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class HomePage : Form
    {
        private List<PlaylistModel> userPlaylists = new List<PlaylistModel>();
        private readonly string savedPlaylistsFile = "saved_playlists.json";

        public HomePage()
        {
            InitializeComponent();
            InitializeDefaultPlaylists();
            LoadSavedPlaylists(); // Loads JSON playlists on app startup
            DisplayFavoritePlaylists();
            UpdateStatisticalInsights();
            ApplyCustomStyling();
        }

        private void InitializeDefaultPlaylists()
        {
            if (userPlaylists.Count == 0)
            {
                userPlaylists.Add(new PlaylistModel("Favourites", LoadImageSafely("Luxury fashion & independent designers _ SSENSE.jpg")));
                userPlaylists.Add(new PlaylistModel("Vibes", LoadImageSafely("ibiza feels.jpg")));
                userPlaylists.Add(new PlaylistModel("Sad Songs", LoadImageSafely("1688918606672529.jpg")));
                userPlaylists.Add(new PlaylistModel("Study", LoadImageSafely("60939401204208804.jpg")));
            }
        }

        // SERIALIZATION 
        public void SaveCustomPlaylists()
        {
            try
            {
                string[] defaults = { "Favourites", "Vibes", "Sad Songs", "Study" };
                var customPlaylists = userPlaylists
                    .Where(p => !defaults.Contains(p.Title, StringComparer.OrdinalIgnoreCase))
                    .ToList();

                string json = JsonSerializer.Serialize(customPlaylists, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(savedPlaylistsFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save playlists: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // SERIALIZATION 
        private void LoadSavedPlaylists()
        {
            try
            {
                if (File.Exists(savedPlaylistsFile))
                {
                    string json = File.ReadAllText(savedPlaylistsFile);
                    var customPlaylists = JsonSerializer.Deserialize<List<PlaylistModel>>(json);

                    if (customPlaylists != null)
                    {
                        foreach (var playlist in customPlaylists)
                        {
                            if (!string.IsNullOrEmpty(playlist.CoverImagePath) && File.Exists(playlist.CoverImagePath))
                            {
                                playlist.CoverImage = Image.FromFile(playlist.CoverImagePath);
                            }

                            if (!userPlaylists.Any(p => p.Title.Equals(playlist.Title, StringComparison.OrdinalIgnoreCase)))
                            {
                                userPlaylists.Add(playlist);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load saved playlists: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image LoadImageSafely(string fileName)
        {
            if (File.Exists(fileName))
            {
                return Image.FromFile(fileName);
            }

            string binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(binPath))
            {
                return Image.FromFile(binPath);
            }

            return null;
        }

        private void DisplayFavoritePlaylists()
        {
            if (flowLayoutPanel1 != null)
            {
                flowLayoutPanel1.Controls.Clear();
                AddCreatePlaylistCard();

                foreach (var playlist in userPlaylists)
                {
                    Image coverImg = playlist.CoverImage;
                    AddNewPlaylistCard(playlist.Title, coverImg);
                }
            }
        }

        private void AddCreatePlaylistCard()
        {
            Panel cardPanel = new Panel();
            cardPanel.Size = new Size(226, 150);
            cardPanel.BackColor = Color.FromArgb(40, 25, 60);
            cardPanel.Margin = new Padding(10);

            PictureBox picBox = new PictureBox();
            picBox.Size = new Size(134, 109);
            picBox.Location = new Point(53, 3);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            picBox.BackColor = Color.FromArgb(50, 35, 75);
            picBox.Image = LoadImageSafely("Beaverswood Warehouse Floor Markers _ Facilities Management _ BiGDUG.jpg");

            Button btnCreate = new Button();
            btnCreate.Text = "Create Playlist";
            btnCreate.Size = new Size(120, 25);
            btnCreate.Location = new Point(53, 117);
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.ForeColor = Color.White;
            btnCreate.BackColor = Color.BlueViolet;

            btnCreate.Click += (s, ev) =>
            {
                Form2 createPlaylistForm = new Form2();
                createPlaylistForm.ShowDialog();
            };

            cardPanel.Controls.Add(picBox);
            cardPanel.Controls.Add(btnCreate);

            flowLayoutPanel1.Controls.Add(cardPanel);
        }

        public void AddNewPlaylistCard(string playlistTitle, Image coverImage)
        {
            Panel cardPanel = new Panel();
            cardPanel.Size = new Size(226, 150);
            cardPanel.BackColor = Color.FromArgb(40, 25, 60);
            cardPanel.Margin = new Padding(10);

            PictureBox picBox = new PictureBox();
            picBox.Size = new Size(134, 109);
            picBox.Location = new Point(53, 3);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            picBox.Image = coverImage;
            if (coverImage == null) picBox.BackColor = Color.Gray;

            Button btnOpen = new Button();
            btnOpen.Text = playlistTitle;
            btnOpen.Size = new Size(120, 25);
            btnOpen.Location = new Point(53, 117);
            btnOpen.FlatStyle = FlatStyle.Flat;
            btnOpen.ForeColor = Color.White;
            btnOpen.BackColor = Color.BlueViolet;

            // Wire up click event to open playlist
            btnOpen.Click += (s, ev) => OpenPlaylistByName(playlistTitle);
            picBox.Click += (s, ev) => OpenPlaylistByName(playlistTitle);

            cardPanel.Controls.Add(picBox);
            cardPanel.Controls.Add(btnOpen);

            flowLayoutPanel1.Controls.Add(cardPanel);
        }

        public void AddPlaylistFromForm2(PlaylistModel newPlaylist)
        {
            if (userPlaylists.Any(p => p.Title.Equals(newPlaylist.Title, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("A playlist with this name already exists.", "Duplicate Playlist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            userPlaylists.Add(newPlaylist);
            SaveCustomPlaylists(); // TRIGGERS JSON SAVE IMMEDIATELY

            DisplayFavoritePlaylists();
            UpdateStatisticalInsights();
        }

        private void OpenForm2()
        {
            Form2 createPlaylistForm = new Form2();
            createPlaylistForm.ShowDialog();
        }

        private void OpenPlaylistForm(PlaylistModel targetPlaylist)
        {
            Playlist playlistWindow = new Playlist(targetPlaylist);
            playlistWindow.ShowDialog();
            UpdateStatisticalInsights();
        }

        public void UpdateStatisticalInsights()
        {
            int totalPlaylists = userPlaylists.Count;
            int totalTracks = userPlaylists.Sum(p => p.TrackCount);

            string topGenre = userPlaylists
                .Where(p => p.Songs != null)
                .SelectMany(p => p.Songs)
                .GroupBy(s => s.Genre)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault() ?? "N/A";

            if (lblTotalPlaylists != null)
                lblTotalPlaylists.Text = $"Total Playlists: {totalPlaylists}";

            if (lblTotalTracks != null)
                lblTotalTracks.Text = $"Total Tracks: {totalTracks}";

            if (lblTopGenre != null)
                lblTopGenre.Text = $"Top Genre: {topGenre}";
        }

        private void btnUploadSong_Click(object sender, EventArgs e)
        {
            string selectedFilePath = "";
            string defaultFileName = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Track from Your Device";
                openFileDialog.Filter = "Audio Files (*.mp3;*.wav;*.m4a;*.aac)|*.mp3;*.wav;*.m4a;*.aac|All Files (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    defaultFileName = Path.GetFileNameWithoutExtension(selectedFilePath);
                }
                else
                {
                    return;
                }
            }

            string name = PromptInput($"Enter Song Name:", "Upload Track", defaultFileName);
            if (string.IsNullOrWhiteSpace(name)) return;

            string artist = PromptInput("Enter Artist Name:", "Upload Track", "Unknown Artist");
            if (string.IsNullOrWhiteSpace(artist)) return;

            string album = PromptInput("Enter Album Name:", "Upload Track", "Single");
            if (string.IsNullOrWhiteSpace(album)) return;

            string genre = PromptInput("Enter Genre:", "Upload Track", "Pop");
            if (string.IsNullOrWhiteSpace(genre)) return;

            Song uploadedSong = new Song(name, artist, album, genre, selectedFilePath);

            string availableTitles = string.Join(", ", userPlaylists.Select(p => p.Title));
            string targetInput = PromptInput($"Available Playlists: [{availableTitles}]\n\nType target playlist name(s) (comma-separated):", "Assign to Playlist", "Favourites");

            if (!string.IsNullOrWhiteSpace(targetInput))
            {
                string[] selectedTitles = targetInput.Split(',');
                int addedCount = 0;

                foreach (string title in selectedTitles)
                {
                    string trimmed = title.Trim();
                    var targetPlaylist = userPlaylists.FirstOrDefault(p => p.Title.Equals(trimmed, StringComparison.OrdinalIgnoreCase));

                    if (targetPlaylist != null)
                    {
                        if (targetPlaylist.Songs == null)
                        {
                            targetPlaylist.Songs = new List<Song>();
                        }
                        targetPlaylist.Songs.Add(uploadedSong);
                        addedCount++;
                    }
                }

                if (addedCount > 0)
                {
                    MessageBox.Show($"'{uploadedSong.Name}' successfully uploaded from device and added to {addedCount} playlist(s)!", "Upload Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SaveCustomPlaylists(); // SAVE SONG ADDITIONS TO JSON
                    UpdateStatisticalInsights();
                    DisplayFavoritePlaylists();
                }
                else
                {
                    MessageBox.Show("No matching playlists found. The track was not assigned.", "Assignment Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private string PromptInput(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 440,
                Height = 210,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label() { Left = 20, Top = 15, Text = text, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 20, Top = 65, Width = 380, Text = defaultValue };
            Button confirmation = new Button() { Text = "Submit", Left = 300, Width = 100, Top = 110, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void ApplyCustomStyling()
        {
            this.BackColor = Color.FromArgb(18, 12, 32);

            if (lblWelcomeUser != null)
            {
                lblWelcomeUser.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                lblWelcomeUser.ForeColor = Color.FromArgb(190, 130, 255);
            }

            StyleControlTree(this);
        }

        private void StyleControlTree(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Panel panel)
                {
                    panel.BackColor = Color.FromArgb(32, 26, 52);
                }
                else if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(130, 50, 210);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (ctrl is Label lbl && lbl != lblWelcomeUser)
                {
                    lbl.ForeColor = Color.FromArgb(220, 220, 240);
                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }

                if (ctrl.HasChildren)
                {
                    StyleControlTree(ctrl);
                }
            }
        }

        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void setCurrentUserloggedin(string username)
        {
            lblWelcomeUser.Text = $"Live: {username}🚀";
        }

        private void btnFavourites_Click(object sender, EventArgs e) => OpenPlaylistByName("Favourites");
        private void btnVibes_Click(object sender, EventArgs e) => OpenPlaylistByName("Vibes");
        private void btnSadSongs_Click(object sender, EventArgs e) => OpenPlaylistByName("Sad Songs");
        private void btnStudy_Click(object sender, EventArgs e) => OpenPlaylistByName("Study");

        private void OpenPlaylistByName(string title)
        {
            var playlist = userPlaylists.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (playlist != null)
            {
                OpenPlaylistForm(playlist);
            }
        }

        private void btnCreatePlaylist_Click(object sender, EventArgs e)
        {
            OpenForm2();
        }
    }
}