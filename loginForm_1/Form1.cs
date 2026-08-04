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
        RegistrationPage registrationPage= new RegistrationPage(this);

        registrationPage.Show();
        this.Hide();
    }
}
