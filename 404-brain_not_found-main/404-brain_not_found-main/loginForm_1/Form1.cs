using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class Form1 : Form
    {
        private readonly HomePage _homePage;
        private List<User> _usersList;

        public Form1(HomePage homePage)
        {
            InitializeComponent();
            ApplyCustomStyling();
            _homePage = homePage ?? throw new ArgumentNullException(nameof(homePage));

            // Load persistent user data using Serialization on start
            _usersList = DataManager.LoadUsers();
        }

        // This method fixes the CS1061 error
        public void RefreshUserData()
        {
            _usersList = DataManager.LoadUsers();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsLoginValid(txtUsername.Text, txtPassword.Text, out string finalUser))
                {
                    MessageBox.Show($"Welcome to HertzPlay, {finalUser}! 😁", "Welcome 🎶🎙️",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _homePage.setCurrentUserloggedin(finalUser);
                    _homePage.Show();
                    this.Hide();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"An unexpected error occurred during login: {err.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsLoginValid(string username, string password, out string fullName)
        {
            fullName = string.Empty;

            try
            {
                string inputUser = username.Trim().ToLower();
                string inputPass = password.Trim();

                if (string.IsNullOrWhiteSpace(inputUser) || string.IsNullOrWhiteSpace(inputPass))
                {
                    MessageBox.Show("Please fill in both fields.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Checks the loaded List<User> from persistent json file
                User matchingUser = _usersList.FirstOrDefault(u =>
                    u.Username.Equals(inputUser, StringComparison.OrdinalIgnoreCase) &&
                    u.Password.Equals(inputPass, StringComparison.Ordinal));

                if (matchingUser != null)
                {
                    fullName = matchingUser.Username;
                    return true;
                }

                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Validation Error: {err.Message}");
                return false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage(this);
            registrationPage.Show();
            this.Hide();
        }

        // Designer event handlers 
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Intentionally left blank - kept for designer event wiring
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Intentionally left blank - kept for designer event wiring
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // If initialization on load is required, place it here.
            // For now, ensure user data is loaded in case the form was constructed without calling RefreshUserData.
            if (_usersList == null)
            {
                _usersList = DataManager.LoadUsers();
            }
        }

        private void ApplyCustomStyling()
        {
            this.BackColor = Color.FromArgb(18, 12, 32);

            if (lblTitle != null)
            {
                lblTitle.Text = "Login";
                lblTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold | FontStyle.Italic);
                lblTitle.ForeColor = Color.FromArgb(190, 130, 255);
                lblTitle.AutoSize = true;
            }

            if (lblUserN != null)
            {
                lblUserN.ForeColor = Color.FromArgb(220, 220, 240);
                lblUserN.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            }

            if (lblUserPass != null)
            {
                lblUserPass.ForeColor = Color.FromArgb(220, 220, 240);
                lblUserPass.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            }

            if (txtUsername != null)
            {
                txtUsername.BackColor = Color.FromArgb(32, 26, 52);
                txtUsername.ForeColor = Color.White;
                txtUsername.BorderStyle = BorderStyle.FixedSingle;
                txtUsername.Font = new Font("Segoe UI", 11);
            }

            if (lblRegister != null)
            {
                lblRegister.BackColor = Color.FromArgb(32, 26, 52);
                lblRegister.ForeColor = Color.White;
                lblRegister.BorderStyle = BorderStyle.FixedSingle;
                lblRegister.Font = new Font("Segoe UI", 11);
            }

            if (txtPassword != null)
            {
                txtPassword.BackColor = Color.FromArgb(32, 26, 52);
                txtPassword.ForeColor = Color.White;
                txtPassword.BorderStyle = BorderStyle.FixedSingle;
                txtPassword.Font = new Font("Segoe UI", 11);
            }

            if (btnLogin != null)
            {
                btnLogin.BackColor = Color.FromArgb(130, 50, 210);
                btnLogin.ForeColor = Color.White;
                btnLogin.FlatStyle = FlatStyle.Flat;
                btnLogin.FlatAppearance.BorderSize = 0;
                btnLogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            }

            if (btnRegister != null)
            {
                btnRegister.BackColor = Color.FromArgb(130, 50, 210);
                btnRegister.ForeColor = Color.White;
                btnRegister.FlatStyle = FlatStyle.Flat;
                btnRegister.FlatAppearance.BorderSize = 0;
                btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                if (btnLogin != null) btnRegister.Size = btnLogin.Size;
            }
        }
    }
}