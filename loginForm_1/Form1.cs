using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace loginForm_1;
//TEST PASSWORD: bopPass2026! & Username:u2080426
public partial class Form1 : Form
{
    private readonly HomePage _homePage;
    public Form1(HomePage homePage)
    {
        InitializeComponent();

        ApplyCustomStyling();

        _homePage = homePage ?? throw new ArgumentNullException(nameof(homePage));
    }



    private void btnLogin_Click(object sender, EventArgs e)
    {
        if (this.IsLoginValid(txtUsername.Text, txtPassword.Text, out string finalUser))
        {
            MessageBox.Show($"Welcome to HertzPlay!{finalUser}😁", "Welcome🎶🎙️", MessageBoxButtons.OK);
            //Set the current user to Homepage screen
            _homePage.setCurrentUserloggedin(finalUser);
            //Homepage window will show
            _homePage.Show();
            //Hide current window
            this.Hide();
        }
    }
    private bool IsLoginValid(string username, string password, out string fullName)
    {
        try
        {
            //Check for empty textfield login attempt
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please input Username & Password", "Error!", MessageBoxButtons.OK);
            }
            //Regex patterns
            string userPattern = @"^[a-zA-Z0-9]{3,15}$";
            string passPattern = @"^(?=.*[A-Za-z])(?=.*\d).{6,}$";

            //patten match check
            if (!Regex.IsMatch(username.Trim(), userPattern) ||
                !Regex.IsMatch(password.Trim(), passPattern))
            {
                MessageBox.Show("Invalid Username or Password format", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            fullName = username;
            return true;
        }
        catch (Exception err)
        {
            //Print exact issue to Visual studio output window for better debugging
            Debug.WriteLine($"Validation Error: {err.Message}");
            fullName = err.Message;
            return false;
        }
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        RegistrationPage registrationPage = new RegistrationPage(this);

        registrationPage.Show();
        this.Hide();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        
    }

    private void txtPassword_TextChanged(object sender, EventArgs e)
    {

    }

    private void Form1_Load_1(object sender, EventArgs e)
    {

    }

    private void ApplyCustomStyling()
    {
        // 1. Form Window Background
        this.BackColor = Color.FromArgb(18, 12, 32);

        // 2. Big Title Label (lblTitle)
        lblTitle.Text = "Login";
        lblTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold | FontStyle.Italic);
        lblTitle.ForeColor = Color.FromArgb(190, 130, 255); // Bright Neon Lavender
        lblTitle.AutoSize = true;

        // 3. Field Labels Customization
        lblUserN.ForeColor = Color.FromArgb(220, 220, 240);
        lblUserN.Font = new Font("Segoe UI", 11, FontStyle.Regular);

        lblUserPass.ForeColor = Color.FromArgb(220, 220, 240);
        lblUserPass.Font = new Font("Segoe UI", 11, FontStyle.Regular);

        // 4. TextBoxes Fill & Borders
        txtUsername.BackColor = Color.FromArgb(32, 26, 52);
        txtUsername.ForeColor = Color.White;
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 11);

        txtPassword.BackColor = Color.FromArgb(32, 26, 52);
        txtPassword.ForeColor = Color.White;
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 11);

        // 5. Primary Login Button Styling
        btnLogin.BackColor = Color.FromArgb(130, 50, 210);
        btnLogin.ForeColor = Color.White;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);

        // 6. Register Button Styling (Matches Login Button)
        btnRegister.BackColor = Color.FromArgb(130, 50, 210);
        btnRegister.ForeColor = Color.White;
        btnRegister.FlatStyle = FlatStyle.Flat;
        btnRegister.FlatAppearance.BorderSize = 0;
        btnRegister.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        btnRegister.Size = btnLogin.Size;
    }
}
