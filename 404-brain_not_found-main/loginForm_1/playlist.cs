using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class Playlist : Form
    {
        private PlaylistModel currentPlaylist;
        private string currentPlaylistName;

        // DECLARE IT HERE:
        private string playlistMetadataFile;

        //2d-Array
        private string[,] songsArray;
        //Songs count variable
        int currentSongCount;

        //ascending order variable
        private bool isAscending = true;

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
            LoadSongsList();
        }

        // Default constructor
        public Playlist() : this("My Playlist")
        {
        }


        private void Playlist_Load(object sender, EventArgs e)
        {
            // Reads text file layout entries into the List object structure first
            LoadPlaylistMetadata();

            // Maps the loaded data into your 2D Array matrix and DataGridView cells
            LoadSongsList();

            // Modifies metric labels        
            UpdateTrackCount();

            if (lblPlaylistTitle != null)
            {
                lblPlaylistTitle.Text = currentPlaylistName;
            }

            ApplyTheme();
        }

        private void LoadSongsList()
        {
            if (dgvSongs == null || currentPlaylist == null) return;
            dgvSongs.Rows.Clear();
            int totalSongs = currentPlaylist.Songs != null ? currentPlaylist.Songs.Count : 0;
            if (totalSongs == 0) return;

            //populate the array
            songsArray = new string[totalSongs, 4];
            for (int i = 0; i < totalSongs; i++)
            {
                Song currentSong = currentPlaylist.Songs[i];

                songsArray[i, 0] = currentSong.Name ?? "Unknown";
                songsArray[i, 1] = currentSong.Artist ?? "Unknown";
                songsArray[i, 2] = currentSong.Album ?? "Unknown";
                songsArray[i, 3] = currentSong.Genre ?? "Unknown";
            }
            //Populate the datagridview
            for (int j = 0; j < totalSongs; j++)
            {
                dgvSongs.Rows.Add(
                    songsArray[j, 0],
                    songsArray[j, 1],
                    songsArray[j, 2],
                    songsArray[j, 3]
                    );
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
                if (currentPlaylist != null && currentPlaylist.CoverImage != null && picCoverArt != null)
                {
                    picCoverArt.Image = currentPlaylist.CoverImage;
                    picCoverArt.SizeMode = PictureBoxSizeMode.Zoom;
                }

                if (File.Exists(playlistMetadataFile))
                {
                    using (StreamReader reader = new StreamReader(playlistMetadataFile))
                    {
                        // Read top level metadata components
                        string creationDate = reader.ReadLine();
                        string imagePath = reader.ReadLine();

                        if (!string.IsNullOrEmpty(creationDate) && lblCreationDate != null)
                        {
                            lblCreationDate.Text = "Created: " + creationDate;
                        }

                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath) && picCoverArt != null && picCoverArt.Image == null)
                        {
                            picCoverArt.ImageLocation = imagePath;
                            picCoverArt.SizeMode = PictureBoxSizeMode.Zoom;
                        }

                        // Read all tracks preserved in the text file layout
                        if (currentPlaylist != null)
                        {
                            // Avoid overwriting
                            if (currentPlaylist.Songs == null)
                            {
                                currentPlaylist.Songs = new List<Song>();
                            }

                            // Load saved tracks if memory model is currently empty
                            if (currentPlaylist.Songs.Count == 0)
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (string.IsNullOrWhiteSpace(line)) continue;

                                    // Split by the delimiter token
                                    string[] parts = line.Split('|');
                                    if (parts.Length >= 5)
                                    {
                                        Song loadedSong = new Song(
                                            name: parts[0],
                                            artist: parts[1],
                                            album: parts[2],
                                            genre: parts[3],
                                            filePath: parts[4]
                                        );
                                        currentPlaylist.Songs.Add(loadedSong);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Fallback generation for new file records
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
                        SavePlaylistDataToFile();
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
            System.Drawing.Color darkBackground = System.Drawing.Color.FromArgb(14, 11, 24);
            System.Drawing.Color darkPanel = System.Drawing.Color.FromArgb(24, 18, 40);
            System.Drawing.Color vibrantPurple = System.Drawing.Color.FromArgb(124, 45, 210);
            System.Drawing.Color deleteRed = System.Drawing.Color.FromArgb(140, 25, 45);

            System.Drawing.Color textWhite = System.Drawing.Color.White;
            System.Drawing.Color textMuted = System.Drawing.Color.FromArgb(200, 190, 220);

            // --- 2. FORM & PANEL BACKGROUNDS ---
            this.BackColor = darkBackground;
            if (pnlHeader != null) pnlHeader.BackColor = darkPanel;
            if (pnlLeft != null) pnlLeft.BackColor = darkBackground;
            if (pnlPlayback != null) pnlPlayback.BackColor = darkPanel;

            // --- 3. LABELS & TYPOGRAPHY ---
            if (lblPlaylistTitle != null)
            {
                lblPlaylistTitle.ForeColor = vibrantPurple;
                lblPlaylistTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            }

            if (lblCreationDate != null)
            {
                lblCreationDate.ForeColor = textMuted;
                lblCreationDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            }

            if (lblTrackCount != null)
            {
                lblTrackCount.ForeColor = textMuted;
                lblTrackCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            }

            // --- 4. LISTBOX / SONG LIST ---
            if (lstSongs != null)
            {
                lstSongs.BackColor = darkPanel;
                lstSongs.ForeColor = textWhite;
                lstSongs.BorderStyle = BorderStyle.FixedSingle;
                lstSongs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            }

            // --- 5. COVER ART ---
            if (picCoverArt != null)
            {
                picCoverArt.BackColor = darkPanel;
                picCoverArt.BorderStyle = BorderStyle.FixedSingle;
            }

            // --- 6. HERTZPLAY BUTTON STYLING ---
            if (btnUploadCover != null)
            {
                btnUploadCover.FlatStyle = FlatStyle.Flat;
                btnUploadCover.FlatAppearance.BorderSize = 0;
                btnUploadCover.BackColor = vibrantPurple;
                btnUploadCover.ForeColor = textWhite;
                btnUploadCover.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                btnUploadCover.Cursor = Cursors.Hand;
            }

            if (btnAddSong != null)
            {
                btnAddSong.FlatStyle = FlatStyle.Flat;
                btnAddSong.FlatAppearance.BorderSize = 0;
                btnAddSong.BackColor = vibrantPurple;
                btnAddSong.ForeColor = textWhite;
                btnAddSong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                btnAddSong.Cursor = Cursors.Hand;
            }

            if (btnSort != null)
            {
                btnSort.FlatStyle = FlatStyle.Flat;
                btnSort.FlatAppearance.BorderSize = 0;
                btnSort.BackColor = vibrantPurple;
                btnSort.ForeColor = textWhite;
                btnSort.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                btnSort.Cursor = Cursors.Hand;
            }

            if (btnPlaySong != null)
            {
                btnPlaySong.FlatStyle = FlatStyle.Flat;
                btnPlaySong.FlatAppearance.BorderSize = 0;
                btnPlaySong.BackColor = vibrantPurple;
                btnPlaySong.ForeColor = textWhite;
                btnPlaySong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                btnPlaySong.Cursor = Cursors.Hand;
            }

            if (btnDeletePlaylist != null)
            {
                btnDeletePlaylist.FlatStyle = FlatStyle.Flat;
                btnDeletePlaylist.FlatAppearance.BorderSize = 0;
                btnDeletePlaylist.BackColor = deleteRed;
                btnDeletePlaylist.ForeColor = textWhite;
                btnDeletePlaylist.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                btnDeletePlaylist.Cursor = Cursors.Hand;
            }
        }

        private void pnlLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDeletePlaylist_Click(object sender, EventArgs e)
        {

        }

        private void playlist_Load(object sender, EventArgs e)
        {
            // Set the heading label text
            if (lblPlaylistTitle != null)
            {
                lblPlaylistTitle.Text = currentPlaylistName;
            }

            // Reads {playlistTitle}_info.txt using StreamReader and sets lblCreationDate
            LoadPlaylistMetadata();

            // Maps loaded songs to matrix and grid
            LoadSongsList();

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

        private void btnAddSong_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Audio File";
                openFileDialog.Filter = "Audio Files (*.mp3; *.wav)|*.mp3; *.wav";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    // Extract just the file name without the path and extension
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedFilePath);

                    // Create a new Song object
                    Song newSong = new Song(
                        name: fileNameWithoutExtension,
                        artist: "Unknown Artist",
                        album: "Unknown Album",
                        genre: "Unknown Genre",
                        filePath: selectedFilePath
                    );

                    // Add it to dynamic list, 2D array, and DataGridView
                    AddSongToPlaylist(newSong);
                }
            }
        }

        public void AddSongToPlaylist(Song newSong)
        {
            if (newSong == null || currentPlaylist == null) return;

            if (currentPlaylist.Songs == null)
            {
                currentPlaylist.Songs = new List<Song>();
            }

            // Add to the core dynamic list
            currentPlaylist.Songs.Add(newSong);

            // Temporarily turn off the event to prevent layout loop crashes
            this.dgvSongs.CellValueChanged -= this.dgvSongs_CellValueChanged;

            currentSongCount = currentPlaylist.Songs.Count;
            string[,] newSongsArray = new string[currentSongCount, 4];

            // Migrate data from the old 2D array reference
            if (songsArray != null)
            {
                int oldSongCount = songsArray.GetLength(0);
                for (int i = 0; i < oldSongCount; i++)
                {
                    newSongsArray[i, 0] = songsArray[i, 0];
                    newSongsArray[i, 1] = songsArray[i, 1];
                    newSongsArray[i, 2] = songsArray[i, 2];
                    newSongsArray[i, 3] = songsArray[i, 3];
                }
            }

            // Inject the new song metadata text fields into the 2D array matrix
            int newRowIndex = currentSongCount - 1;
            newSongsArray[newRowIndex, 0] = newSong.Name ?? "Unknown Track";
            newSongsArray[newRowIndex, 1] = newSong.Artist ?? "Unknown Artist";
            newSongsArray[newRowIndex, 2] = newSong.Album ?? "Unknown Album";
            newSongsArray[newRowIndex, 3] = newSong.Genre ?? "Unknown Genre";

            songsArray = newSongsArray;

            // Push the updated array row details to the view layout
            dgvSongs.Rows.Add(
                songsArray[newRowIndex, 0],
                songsArray[newRowIndex, 1],
                songsArray[newRowIndex, 2],
                songsArray[newRowIndex, 3]
            );

            // Turn the listener back on
            this.dgvSongs.CellValueChanged += this.dgvSongs_CellValueChanged;

            // PERSIST NEWLY ADDED SONG TO DISK FILE IMMEDIATELY
            SavePlaylistDataToFile();

            UpdateTrackCount();
        }

        private void dgvSongs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header rows and invalid clicks
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Make sure the 2D array is loaded
            if (songsArray == null || e.RowIndex >= songsArray.GetLength(0) || e.ColumnIndex >= songsArray.GetLength(1)) return;

            // Get the new text from the DataGridView cell
            object cellValue = dgvSongs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string updatedText = cellValue != null ? cellValue.ToString() : "";

            // Save the change into the 2D array
            songsArray[e.RowIndex, e.ColumnIndex] = updatedText;

            // Update underlying object List<Song>
            if (currentPlaylist != null && currentPlaylist.Songs != null && e.RowIndex < currentPlaylist.Songs.Count)
            {
                Song targetedSong = currentPlaylist.Songs[e.RowIndex];

                switch (e.ColumnIndex)
                {
                    case 0: targetedSong.Name = updatedText; break;
                    case 1: targetedSong.Artist = updatedText; break;
                    case 2: targetedSong.Album = updatedText; break;
                    case 3: targetedSong.Genre = updatedText; break;
                }

                // SAVE EDITED CELL DATA TO DISK FILE
                SavePlaylistDataToFile();
            }
        }

        private void SavePlaylistDataToFile()
        {
            try
            {
                // Recreate the metadata file layout 
                string creationDate = lblCreationDate != null ? lblCreationDate.Text.Replace("Created: ", "").Trim() : DateTime.Now.ToShortDateString();

                // Get image path directly from PictureBox
                string coverPath = picCoverArt != null && !string.IsNullOrEmpty(picCoverArt.ImageLocation)
                    ? picCoverArt.ImageLocation
                    : "";

                using (var writer = new StreamWriter(playlistMetadataFile, false))
                {
                    writer.WriteLine(creationDate);
                    writer.WriteLine(coverPath);

                    // Loop through list to serialize each song attribute record
                    if (currentPlaylist != null && currentPlaylist.Songs != null)
                    {
                        foreach (Song song in currentPlaylist.Songs)
                        {
                            // Format: Name|Artist|Album|Genre|FilePath
                            string songLine = $"{song.Name}|{song.Artist}|{song.Album}|{song.Genre}|{song.FilePath}";
                            writer.WriteLine(songLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving song data: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletePlaylist_Click_1(object sender, EventArgs e)
        {
            if (dgvSongs.CurrentRow == null || dgvSongs.CurrentRow.Index < 0 || dgvSongs.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a track to remove from this playlist.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dgvSongs.CurrentRow.Index;

            var result = MessageBox.Show("Remove this song from the current playlist?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            // Detach event handler
            this.dgvSongs.CellValueChanged -= this.dgvSongs_CellValueChanged;

            // Remove from active playlist memory model
            if (currentPlaylist?.Songs != null && selectedIndex < currentPlaylist.Songs.Count)
            {
                currentPlaylist.Songs.RemoveAt(selectedIndex);
            }

            // Remove from UI DataGridView
            dgvSongs.Rows.RemoveAt(selectedIndex);

            // Re-attach event handler
            this.dgvSongs.CellValueChanged += this.dgvSongs_CellValueChanged;

            // Re-populate 2D array from scratch to keep indices aligned
            LoadSongsList();

            // Save modified playlist structure to file and update labels
            SavePlaylistDataToFile();
            UpdateTrackCount();

            MessageBox.Show("Track removed from current playlist.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void btnPlaySong_Click(object sender, EventArgs e)
        {
            if (dgvSongs.CurrentRow == null || dgvSongs.CurrentRow.Index < 0 || dgvSongs.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a song from the list to play.",
                    "Select Song", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = dgvSongs.CurrentRow.Index;

            if (currentPlaylist != null && currentPlaylist.Songs != null && selectedIndex < currentPlaylist.Songs.Count)
            {
                Song songToPlay = currentPlaylist.Songs[selectedIndex];

                if (File.Exists(songToPlay.FilePath))
                {
                    axWindowsMediaPlayer1.URL = songToPlay.FilePath;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                else
                {
                    MessageBox.Show($"Audio file not found at:\n{songToPlay.FilePath}",
                        "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SortSongsByProperty(string propertyName)
        {
            if (currentPlaylist?.Songs == null || currentPlaylist.Songs.Count <= 1) return;

            switch (propertyName.ToLower())
            {
                case "title":
                case "name":
                    currentPlaylist.Songs = isAscending
                        ? currentPlaylist.Songs.OrderBy(s => s.Name).ToList()
                        : currentPlaylist.Songs.OrderByDescending(s => s.Name).ToList();
                    break;

                case "artist":
                    currentPlaylist.Songs = isAscending
                        ? currentPlaylist.Songs.OrderBy(s => s.Artist).ToList()
                        : currentPlaylist.Songs.OrderByDescending(s => s.Artist).ToList();
                    break;

                case "album":
                    currentPlaylist.Songs = isAscending
                        ? currentPlaylist.Songs.OrderBy(s => s.Album).ToList()
                        : currentPlaylist.Songs.OrderByDescending(s => s.Album).ToList();
                    break;

                case "genre":
                    currentPlaylist.Songs = isAscending
                        ? currentPlaylist.Songs.OrderBy(s => s.Genre).ToList()
                        : currentPlaylist.Songs.OrderByDescending(s => s.Genre).ToList();
                    break;

                default:
                    return;
            }

            isAscending = !isAscending;

            if (this.dgvSongs != null)
            {
                this.dgvSongs.CellValueChanged -= this.dgvSongs_CellValueChanged;
            }

            LoadSongsList();

            if (this.dgvSongs != null)
            {
                this.dgvSongs.CellValueChanged += this.dgvSongs_CellValueChanged;
            }

            SavePlaylistDataToFile();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            ContextMenuStrip sortMenu = new ContextMenuStrip();

            sortMenu.Items.Add("Sort by Name", null, (s, args) => SortSongsByProperty("title"));
            sortMenu.Items.Add("Sort by Artist", null, (s, args) => SortSongsByProperty("artist"));
            sortMenu.Items.Add("Sort by Album", null, (s, args) => SortSongsByProperty("album"));
            sortMenu.Items.Add("Sort by Genre", null, (s, args) => SortSongsByProperty("genre"));

            if (sender is Control btn)
            {
                sortMenu.Show(btn, new Point(0, btn.Height));
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (dgvSongs.CurrentRow == null || dgvSongs.CurrentRow.Index < 0 || dgvSongs.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a song from the list to play.", "Select Song", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = dgvSongs.CurrentRow.Index;

            if (currentPlaylist != null && currentPlaylist.Songs != null && selectedIndex < currentPlaylist.Songs.Count)
            {
                Song songToPlay = currentPlaylist.Songs[selectedIndex];

                if (System.IO.File.Exists(songToPlay.FilePath))
                {
                    axWindowsMediaPlayer1.URL = songToPlay.FilePath;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                else
                {
                    MessageBox.Show($"Audio file not found at:\n{songToPlay.FilePath}",
                        "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUploadCover_Click_1(object sender, EventArgs e)
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
                    }

                    if (currentPlaylist != null)
                    {
                        try
                        {
                            currentPlaylist.CoverImage = Image.FromFile(selectedImagePath);
                        }
                        catch
                        {
                            currentPlaylist.CoverImage = null;
                        }
                        currentPlaylist.CoverImagePath = selectedImagePath;
                    }

                    SavePlaylistDataToFile();

                    var mainHome = Application.OpenForms.OfType<HomePage>().FirstOrDefault();
                    if (mainHome != null)
                    {
                        mainHome.SaveCustomPlaylists();
                    }

                    MessageBox.Show("Cover art updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}