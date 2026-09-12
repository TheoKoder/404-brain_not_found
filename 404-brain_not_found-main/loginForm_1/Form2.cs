using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class Form2 : Form
    {
        private string selectedImagePath = "";
        private HomePage _parentHome;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(HomePage parent) : this()
        {
            _parentHome = parent;
        }

        private void picCreate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Playlist Cover Image";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;

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

            var mainHome = _parentHome ?? Application.OpenForms.OfType<HomePage>().FirstOrDefault();

            if (mainHome != null)
            {
                // Create a full PlaylistModel instance with title, image, and file path
                PlaylistModel newPlaylist = new PlaylistModel(playlistTitle, picCreate.Image, selectedImagePath);

                // Pass the model to HomePage so it gets saved to list AND serialized to JSON
                mainHome.AddPlaylistFromForm2(newPlaylist);

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