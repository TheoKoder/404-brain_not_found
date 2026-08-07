using System;
using System.Diagnostics;
using System.Drawing;
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

            // Apply theme automatically when constructor executes
            ApplyCustomStyling();
        }

        private void ApplyCustomStyling()
        {
            // 1. Form Window Background
            this.BackColor = Color.FromArgb(18, 12, 32);

            // 2. Big Title Label (lblRegister)
            if (lblRegister != null)
            {
                lblRegister.Text = "Create Account";
                lblRegister.Font = new Font("Segoe UI", 28, FontStyle.Bold | FontStyle.Italic);
                lblRegister.ForeColor = Color.FromArgb(190, 130, 255); // Bright Neon Lavender
                lblRegister.AutoSize = true;
            }

            // 3. TextBoxes (txtNewUserN & txtNewUserP)
            txtNewUserN.BackColor = Color.FromArgb(32, 26, 52);
            txtNewUserN.ForeColor = Color.White;
            txtNewUserN.BorderStyle = BorderStyle.FixedSingle;
            txtNewUserN.Font = new Font("Segoe UI", 11);

            txtNewUserP.BackColor = Color.FromArgb(32, 26, 52);
            txtNewUserP.ForeColor = Color.White;
            txtNewUserP.BorderStyle = BorderStyle.FixedSingle;
            txtNewUserP.Font = new Font("Segoe UI", 11);

            // 4. Register Button
            btnRegister.BackColor = Color.FromArgb(130, 50, 210);
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // 5. Error Label Styling
            lblErrorPassword.ForeColor = Color.FromArgb(255, 100, 100); // Soft Red for readability on dark background
            lblErrorPassword.Font = new Font("Segoe UI", 9, FontStyle.Italic);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblErrorPassword.Hide();

            if (this.isLegitRegistration(txtNewUserN.Text, txtNewUserP.Text))
            {
                _loginPage.Show();
                this.Close();
            }
        }

        private bool isLegitRegistration(string regUser, string regPass)
        {
            try
            {
                // Check for empty fields
                if (string.IsNullOrWhiteSpace(regUser) || string.IsNullOrWhiteSpace(regPass))
                {
                    ShowValidationError("Please Enter Username & Password values");
                    return false;
                }

                // Regex patterns
                string userPattern = @"^[a-zA-Z0-9]{3,15}$";
                string passPattern = @"^(?=.*[*&^%@!#]).{3,15}$";

                // Username validation
                if (!Regex.IsMatch(regUser.Trim(), userPattern))
                {
                    ShowValidationError("Invalid Username. Must be 3 - 15 alphanumeric characters.");
                    return false;
                }

                // Password validation
                if (!Regex.IsMatch(regPass, passPattern))
                {
                    lblErrorPassword.Text = "Password must be 3-15 characters long and contain at least one special character (*,&^%@!#).";
                    lblErrorPassword.Show();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Validation Error: {ex.Message}");
                return false;
            }
        }

        private void ShowValidationError(string m)
        {
            MessageBox.Show(m, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void RegistrationPage_Load(object sender, EventArgs e)
        {
        }

        private void lblErrorPassword_Click(object sender, EventArgs e)
        {
        }
    }
}