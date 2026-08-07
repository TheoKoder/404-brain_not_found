using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class Form2 : Form
    {
        // Store selected file path if you need to save it later
        private string selectedImagePath = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void picCreate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set window title
                openFileDialog.Title = "Select Playlist Cover Image";

                // Filter files to image formats only
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

                // Open file browser dialog
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save chosen path
                    selectedImagePath = openFileDialog.FileName;

                    // Load chosen image into PictureBox safely without locking file
                    using (FileStream stream = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read))
                    {
                        picCreate.Image = Image.FromStream(stream);
                    }
                }
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // 1. Get the playlist title from your TextBox (e.g., txtPlaylistName)
            string playlistTitle = txtCreatePlaylist.Text.Trim();

            // Validate that title is not empty
            if (string.IsNullOrEmpty(playlistTitle))
            {
                MessageBox.Show("Please enter a playlist title!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Get the uploaded image (can be null if none uploaded)
            Image coverImage = picCreate.Image;

            // 3. Find the open HomePage instance or pass reference
            HomePage mainHome = (HomePage)Application.OpenForms["HomePage"];

            if (mainHome != null)
            {
                // Call method to build and display the new panel on HomePage
                mainHome.AddNewPlaylistCard(playlistTitle, coverImage);

                MessageBox.Show($"Playlist '{playlistTitle}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close Create Playlist Form
                this.Close();
            }
            else
            {
                MessageBox.Show("Could not find the HomePage window.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
