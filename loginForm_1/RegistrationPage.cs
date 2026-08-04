using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace loginForm_1
{
    public partial class RegistrationPage : Form
    {
        private readonly Form1 _loginPage;
        public RegistrationPage(Form1 loginPage)
        {
            InitializeComponent();
            _loginPage = loginPage?? throw new ArgumentNullException(nameof(loginPage));
            lblErrorPassword.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (this.isLegitRegistration(txtNewUserN.Text,txtNewUserP.Text))
            {
                _loginPage.Show();
                this.Hide();
            }
            else
            {
                lblErrorPassword.Show();
                lblErrorPassword.Text = "password must be between 3-15 characters long with special keys *,&^%@!# inclusive!";
            }
           
        }

        private bool isLegitRegistration( string regUser, string regPass)
        {
            try
            {
                //Fields must never be left empty validation
                //Check for empty textfield login attempt
                if (string.IsNullOrWhiteSpace(regUser) || string.IsNullOrWhiteSpace(regPass))
                {
                    MessageBox.Show("Please Enter Username & Password values", "Error!", MessageBoxButtons.OK);
                }
                //Regex patterns
                string userPattern = @"^[a-zA-Z0-9]{3,15}$";
                string passPattern = @"^(?=.*[A-Za-z])(?=.*\d).{6,}$";
                //patten match check
                if (!Regex.IsMatch(regUser.Trim(), userPattern) ||
                    !Regex.IsMatch(regPass.Trim(), passPattern))
                {
                    MessageBox.Show("Invalid Username or Password format", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return true;

            }
            catch (Exception ex)
            {

                //Print exact issue to Visual studio output window for better debugging
                Debug.WriteLine($"Validation Error: {ex.Message}");
                
                return false;
            }
        }


    }
}
