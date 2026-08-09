using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            string playlistTitle = txtCreatePlaylist.Text.Trim();

            if (string.IsNullOrEmpty(playlistTitle))
            {
                MessageBox.Show("Please enter a playlist title!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            string metadataFile = $"{playlistTitle}_info.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(metadataFile, false))
                {
                    writer.WriteLine(DateTime.Now.ToShortDateString()); // Line 1: Creation Date
                    writer.WriteLine(selectedImagePath);                 // Line 2: Image Path (or empty if none)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving playlist date: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

            Image coverImage = picCreate.Image;
            HomePage mainHome = (HomePage)Application.OpenForms["HomePage"];

            if (mainHome != null)
            {
                mainHome.AddNewPlaylistCard(playlistTitle, coverImage);
                MessageBox.Show($"Playlist '{playlistTitle}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Could not find the HomePage window.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
