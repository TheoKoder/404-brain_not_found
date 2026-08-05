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
