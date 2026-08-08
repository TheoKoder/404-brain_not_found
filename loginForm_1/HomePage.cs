using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.IO;

namespace loginForm_1
{


    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();

            ApplyCustomStyling();

        }



        // Call this method whenever a user creates a new playlist
        public void AddNewPlaylistCard(string playlistTitle, Image coverImage)
        {
            // 1. Create outer Panel
            Panel cardPanel = new Panel();
            cardPanel.Size = new Size(226, 150);
            cardPanel.BackColor = Color.FromArgb(40, 25, 60); // Matches dark theme
            cardPanel.Margin = new Padding(10);
            //cardPanel.Location = new Point(853, 288); // Adjust as needed for layout

            // 2. Create PictureBox inside panel
            PictureBox picBox = new PictureBox();
            picBox.Size = new Size(134, 109);
            picBox.Location = new Point(53, 3);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;

            // Use default image if user didn't upload one
            if (coverImage != null)
            {
                picBox.Image = coverImage;
            }
            else
            {
                // Optional: set a default fallback image or colored background
                picBox.BackColor = Color.Gray;
            }

            // 3. Create Button inside panel
            Button btnOpen = new Button();
            btnOpen.Text = playlistTitle;
            btnOpen.Size = new Size(75, 23);
            btnOpen.Location = new Point(70, 117);
            btnOpen.FlatStyle = FlatStyle.Flat;
            btnOpen.ForeColor = Color.White;
            btnOpen.BackColor = Color.BlueViolet;

            // 4. Attach Click Event to open playlist form
            btnOpen.Click += (s, ev) =>
            {
                Playlist playlistWindow = new Playlist(playlistTitle);
                playlistWindow.ShowDialog();
            };

            // 5. Add controls into the Panel
            cardPanel.Controls.Add(picBox);
            cardPanel.Controls.Add(btnOpen);


            // If you DON'T have a FlowLayoutPanel, add directly to Form:
            //this.Controls.Add(cardPanel);
            flowLayoutPanel1.Controls.Add(cardPanel);
        }


        private void ApplyCustomStyling()
        {
            // 1. Main Form Background (Dark Midnight Purple)
            this.BackColor = Color.FromArgb(18, 12, 32);

            // 2. Welcome Header Label (lblWelcomeUser)
            if (lblWelcomeUser != null)
            {
                lblWelcomeUser.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                lblWelcomeUser.ForeColor = Color.FromArgb(190, 130, 255); // Bright Neon Lavender
            }

            // 3. Style all card panels, buttons, and sub-labels automatically
            StyleControlTree(this);
        }

        private void StyleControlTree(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Panel panel)
                {
                    // Card background fill for container panels
                    panel.BackColor = Color.FromArgb(32, 26, 52);
                }
                else if (ctrl is Button btn)
                {
                    // Glowing Purple Buttons
                    btn.BackColor = Color.FromArgb(130, 50, 210);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (ctrl is Label lbl && lbl != lblWelcomeUser)
                {
                    // Light Silver/White text for all item labels
                    lbl.ForeColor = Color.FromArgb(220, 220, 240);
                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }

                // Recursively style nested controls inside panels
                if (ctrl.HasChildren)
                {
                    StyleControlTree(ctrl);
                }
            }
        }

        private void HomePage_Load(object sender, EventArgs e)
        {


        }
        //stop the program if ever the user closes the homepage form window
        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //set which user is currently logged in
        public void setCurrentUserloggedin(string username)
        {
            lblWelcomeUser.Text = $"Live: {username}🚀";
            //label6.Text = $" {username} ";
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFavourites_Click(object sender, EventArgs e)
        {
            Playlist playlistWindow = new Playlist("Favourites");
            playlistWindow.ShowDialog();
        }

        private void btnVibes_Click(object sender, EventArgs e)
        {
            Playlist playlistWindow = new Playlist("Vibes");
            playlistWindow.ShowDialog();
        }

        private void btnSadSongs_Click(object sender, EventArgs e)
        {
            Playlist playlistWindow = new Playlist("Sad Songs");
            playlistWindow.ShowDialog();
        }

        private void btnStudy_Click(object sender, EventArgs e)
        {
            Playlist playlistWindow = new Playlist("Study");
            playlistWindow.ShowDialog();
        }

        private void btnCreatePlaylist_Click(object sender, EventArgs e)
        {
            Form2 createPlaylistForm = new Form2();
            createPlaylistForm.ShowDialog();
        }
    }
}
