using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();

            ApplyCustomStyling();
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
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
