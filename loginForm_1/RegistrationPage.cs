using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace loginForm_1
{
    public partial class RegistrationPage : Form
    {
        private readonly Form1 _loginPage;

        public RegistrationPage(Form1 loginPage)
        {
            InitializeComponent();
            _loginPage = loginPage ?? throw new ArgumentNullException(nameof(loginPage));
            lblErrorPassword.Hide();

            ApplyCustomStyling();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblErrorPassword.Hide();
            string newUserName = txtNewUserN.Text.Trim();
            string newUserPass = txtNewUserP.Text.Trim();

            // Validate format using regex
            if (!isLegitRegistration(newUserName, newUserPass))
            {
                return;
            }

            try
            {
                // 1. Load existing users from the serialized file into a C# List<User>
                List<User> currentUsers = DataManager.LoadUsers();

                // 2. Verify that the username does not already exist in the list
                if (currentUsers.Any(u => u.Username.Equals(newUserName, StringComparison.OrdinalIgnoreCase)))
                {
                    ShowValidationError("Username already exists. Please choose another one.");
                    return;
                }

                // 3. Create a new User object and add it to our List<User>
                currentUsers.Add(new User(newUserName, newUserPass));

                // 4. Save the updated list using C# Serialization
                if (DataManager.SaveUsers(currentUsers))
                {
                    MessageBox.Show("Registration successful! You can now log in.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _loginPage.RefreshUserData();
                    _loginPage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save user credentials.", "Save Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Registration Error: {ex.Message}");
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isLegitRegistration(string regUser, string regPass)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(regUser) || string.IsNullOrWhiteSpace(regPass))
                {
                    ShowValidationError("Please enter both Username and Password.");
                    return false;
                }

                string userPattern = @"^[a-zA-Z0-9]{3,15}$";
                string passPattern = @"^(?=.*[*&^%@!#]).{3,15}$";

                if (!Regex.IsMatch(regUser, userPattern))
                {
                    ShowValidationError("Invalid Username. Must be 3 - 15 alphanumeric characters.");
                    return false;
                }

                if (!Regex.IsMatch(regPass, passPattern))
                {
                    lblErrorPassword.Text = "Password must be 3-15 characters long and contain at least one special character (*&^%@!#).";
                    lblErrorPassword.Show();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Validation Exception: {ex.Message}");
                return false;
            }
        }

        private void ShowValidationError(string m)
        {
            MessageBox.Show(m, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ApplyCustomStyling()
        {
            this.BackColor = Color.FromArgb(18, 12, 32);

            if (lblRegister != null)
            {
                lblRegister.Text = "Create Account";
                lblRegister.Font = new Font("Segoe UI", 28, FontStyle.Bold | FontStyle.Italic);
                lblRegister.ForeColor = Color.FromArgb(190, 130, 255);
                lblRegister.AutoSize = true;
            }

            txtNewUserN.BackColor = Color.FromArgb(32, 26, 52);
            txtNewUserN.ForeColor = Color.White;
            txtNewUserN.BorderStyle = BorderStyle.FixedSingle;
            txtNewUserN.Font = new Font("Segoe UI", 11);

            lblUserName.ForeColor = Color.FromArgb(220, 220, 240);
            lblUserName.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblPassword.ForeColor = Color.FromArgb(220, 220, 240);
            lblPassword.Font = new Font("Segoe UI", 11, FontStyle.Regular);

            txtNewUserP.BackColor = Color.FromArgb(32, 26, 52);
            txtNewUserP.ForeColor = Color.White;
            txtNewUserP.BorderStyle = BorderStyle.FixedSingle;
            txtNewUserP.Font = new Font("Segoe UI", 11);

            btnRegister.BackColor = Color.FromArgb(130, 50, 210);
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            lblErrorPassword.ForeColor = Color.FromArgb(255, 100, 100);
            lblErrorPassword.Font = new Font("Segoe UI", 9, FontStyle.Italic);
        }
    }
}